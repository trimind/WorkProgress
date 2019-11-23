// Copyright © 2018 Transeric Solutions.  All rights reserved.
// Author: Eric David Lynch
// License: https://www.codeproject.com/info/cpol10.aspx
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using Transeric.CommandLine.Properties;

namespace Transeric.CommandLine
{
	/// <summary>
	/// A parser to parse command line options.
	/// </summary>
	/// <remarks>
	/// All command line options can be specified, with any of the following formats:
	/// command /name1 value1, command /name1:value1, command /name1=value1, or command /?.
	/// 
	/// Some command line options can also be specified by virtue of the argument's position,
	/// for example: command value1.
	/// 
	/// It is not valid to specify a single option in both fashions, for example the following is
	/// not valid: command value1 /name1 value1.
	/// 
	/// To specify a value starting with a forward slash, use one of the following formats:
	/// command /name:/value, or command -name=/value.
	/// 
	/// By Parse methods will return true (indicating help should be displayed), when no
	/// options are present and none are required, for example: command.  To suppress help
	/// in these instances, set <see cref="NoEmptyOptionHelp"/> to true.
	/// </remarks>
	public class CommandLineParser
	{
		#region Constructor
		/// <summary>
		/// Create a parser for the specified options class instance.
		/// </summary>
		/// <param name="instance">The class instance to which results are parsed.</param>
		/// <remarks>
		/// This constructor can potentially throw a large number of different exception.
		/// Since the compiler simply ignores exceptions thrown by attribute classes,
		/// validation of the attribute parameters must be deferred until we reference
		/// them here.  That said, the most of the exceptions thrown by this constructor
		/// have a common ancestor, either <see cref="CommandLinePropertyException"/> or
		/// <see cref="CommandLineClassException"/>, which occur when validating the
		/// corresponding attributes.
		/// </remarks>
		/// <exception cref="System.ArgumentException">
		/// <paramref name="instance"/> specifies a class where no properties have an
		/// attached <see cref="CommandLinePropertyAttribute" /> attribute -OR-
		/// the attribute specifies a duplicate name -OR-
		/// the attribute specifies a position that creates gaps or repetitions -OR-
		/// the attribute is attached to a boolean property and is positional or required -OR-
		/// the attribute requires or excludes duplicate option names -OR-
		/// the attribute has an option name that appears in both the requires and excludes list.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// <paramref name="instance"/> has no public properties with an attached
		/// <see cref="CommandLinePropertyAttribute"/> attribute.
		/// </exception>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="instance"/> is null
		/// </exception>
		/// <exception cref="System.Reflection.AmbiguousMatchException">
		/// <paramref name="instance"/> contains a property with multiple 
		/// <see cref="CommandLinePropertyAttribute"/> attributes -OR-
		/// <paramref name="instance"/> has multiple <see cref="CommandLineClassAttribute"/>
		/// attributes.  Neither of these should be possible, since 
		/// <see cref="AttributeUsageAttribute.AllowMultiple"/> is set to true for both
		/// of these attributes.
		/// </exception>
		/// <exception cref="BooleanRequiredOrPositionalException">
		/// <paramref name="instance"/> includes a boolean property that specifies either
		/// <see cref="CommandLinePropertyAttribute.IsRequired"/> or
		/// <see cref="CommandLinePropertyAttribute.Position"/>.
		/// </exception>
		/// <exception cref="CommandLineClassException">
		/// <paramref name="instance"/> has a <see cref="CommandLineClassAttribute"/>
		/// that is not valid, most likely because 'resourceType' is null.
		/// </exception>
		/// <exception cref="DuplicatePropertyException">
		/// <paramref name="instance"/> includes multiple properties that
		/// specify the same option name.
		/// </exception>
		/// <exception cref="InvalidOptionNameException">
		/// <paramref name="instance"/> includes a property with an invalid
		/// <see cref="CommandLinePropertyAttribute.Name"/>
		/// </exception>
		/// <exception cref="IsRequiredExcludesException">
		/// <paramref name="instance"/>> includes a property that specifies both
		/// <see cref="CommandLinePropertyAttribute.IsRequired"/> and
		/// <see cref="CommandLinePropertyAttribute.Excludes" />
		/// </exception>
		/// <exception cref="NoSetterPropertyException">
		/// <paramref name="instance"/> includes a property with an attached
		/// <see cref="CommandLinePropertyAttribute"/> attribute, but without
		/// a public setter method
		/// </exception>
		/// <exception cref="PropertyPositionException">
		/// <paramref name="instance"/> includes a property that specifies
		///  a 'position' which creates a gap in the sequence of positions
		///  or duplicates a previous position.
		/// </exception>
		/// <exception cref="PropertyReferenceException">
		/// <paramref name="instance"/> contains a property that specifies
		/// an invalid option reference in <see cref="CommandLinePropertyAttribute.Excludes"/>
		/// or <see cref="CommandLinePropertyAttribute.Requires"/>.
		/// </exception>
		public CommandLineParser(object instance)
		{
			Type type = instance?.GetType() ?? throw new ArgumentNullException(nameof(instance));

			var attribute = type.GetCustomAttribute<CommandLineClassAttribute>();
			if (attribute != null && attribute.ResourceType == null)
				throw new CommandLineClassException(type.Name);

			Options = GetOptions(type, instance);
			if (Options.Count < 1)
				throw new ArgumentException(null, nameof(instance));

			CommandLineOption duplicate = Options.GetFirstDuplicate();
			if (duplicate != null)
				throw new DuplicatePropertyException(duplicate.PropertyName);

			requiredOptions = Options.Where(option => option.IsRequired).ToList();
			positionalOptions = GetPositionalOptions();

			HelpText = GetHelpText(type, attribute, out ResourceManager resourceManager);
			FinalizeOptions(resourceManager);
		}
		#endregion // Constructor

		#region Private data
		private static readonly char[] defaultOptionNameTerminators = new char[] { ':', '=' };
		private readonly List<CommandLineOption> positionalOptions;
		private readonly List<CommandLineOption> requiredOptions;
		private char[] optionNameTerminators = defaultOptionNameTerminators;
		#endregion // Private data

		#region Properties
		/// <summary>
		/// Gets the help text for the command line.
		/// </summary>
		public string HelpText { get; } = string.Empty;

		/// <summary>
		/// Gets or sets the option indicator character.
		/// </summary>
		public char OptionIndicator { get; set; } = '/';

		/// <summary>
		/// Gets or sets the set of characters that will terminate an option
		/// name to indicate that a value is part of a command line argument
		/// (e.g. command /name=value).
		/// </summary>
		/// <remarks>
		/// Results are unpredictable, if this set of characters includes any
		/// characters that are valid in an option name (see
		/// <see cref="CommandLineOption.IsValidName(string)"/>).
		/// </remarks>
		public char[] OptionNameTerminators
		{
			get => optionNameTerminators;
			set => optionNameTerminators = value ?? defaultOptionNameTerminators;
		}

		/// <summary>
		/// Gets a value indicating if help should be suppressed when
		/// no options are specified and none are required.
		/// </summary>
		public bool NoEmptyOptionHelp { get; set; }

		/// <summary>
		/// Gets the options recognized by this parser.
		/// </summary>
		public CommandLineOptionList Options { get; }
		#endregion // Properties

		#region Public methods
		/// <summary>
		/// Parse the specified arguments.
		/// </summary>
		/// <param name="args">The arguments to parse.</param>
		/// <param name="presentOptions">A hash set of the options that were present.</param>
		/// <param name="noApplicationName">An (optional) value indicating if the application name is absent from the command line arguments.</param>
		/// <returns>True, if help should be displayed; otherwise, false.</returns>
		/// <remarks>
		/// Most of the exceptions thrown by this method are derived from the
		/// <see cref="CommandLineParseException"/> class.
		/// </remarks>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="args"/> is null
		/// </exception>
		/// <exception cref="ConflictOptionException">
		/// <paramref name="args"/> specifies an option that is excluded by another option
		/// that is also present.
		/// </exception>
		/// <exception cref="DuplicateOptionException">
		/// <paramref name="args"/> specifies the same option multiple times
		/// </exception>
		/// <exception cref="InvalidCommandLineArgumentException">
		/// <paramref name="args"/> specifies an option that is not recognized,
		/// or a boolean option with a value, or a positional value with no
		/// corresponding positional option.
		/// </exception>
		/// <exception cref="InvalidOptionValueException">
		/// <paramref name="args"/> specifies an option value that cannot be
		/// converted to the corresponding property type
		/// </exception>
		/// <exception cref="MissingRequiredOptionException">
		/// <paramref name="args"/> fails to specify a required option
		/// </exception>
		/// <exception cref="MissingOptionValueException">
		/// <paramref name="args"/> fails to specify the value for a non-boolean option
		/// </exception>
		public bool Parse(IEnumerable<string> args, out HashSet<string> presentOptions,
			bool noApplicationName = false)
		{
			HashSet<CommandLineOption> options = Parse(args, noApplicationName,	out bool showHelp);

			presentOptions = new HashSet<string>();
			foreach (CommandLineOption option in options)
				presentOptions.Add(option.Name);

			return showHelp;
		}

		/// <summary>
		/// Parse the specified arguments.
		/// </summary>
		/// <param name="args">The arguments to parse.</param>
		/// <param name="noApplicationName">An (optional) value indicating if the application name is absent from the command line arguments.</param>
		/// <returns>True, if help should be displayed; otherwise, false.</returns>
		/// <remarks>
		/// Most of the exceptions thrown by this method are derived from the
		/// <see cref="CommandLineParseException"/> class.
		/// </remarks>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="args"/> is null
		/// </exception>
		/// <exception cref="ConflictOptionException">
		/// <paramref name="args"/> specifies an option that is excluded by another option
		/// that is also present.
		/// </exception>
		/// <exception cref="DuplicateOptionException">
		/// <paramref name="args"/> specifies the same option multiple times
		/// </exception>
		/// <exception cref="InvalidCommandLineArgumentException">
		/// <paramref name="args"/> specifies an option that is not recognized,
		/// or a boolean option with a value, or a positional value with no
		/// corresponding positional option.
		/// </exception>
		/// <exception cref="InvalidOptionValueException">
		/// <paramref name="args"/> specifies an option value that cannot be
		/// converted to the corresponding property type
		/// </exception>
		/// <exception cref="MissingRequiredOptionException">
		/// <paramref name="args"/> fails to specify a required option
		/// </exception>
		/// <exception cref="MissingOptionValueException">
		/// <paramref name="args"/> fails to specify the value for a non-boolean option
		/// </exception>
		public bool Parse(IEnumerable<string> args, bool noApplicationName = false) =>
			Parse(args, out HashSet<string> presentOptions, noApplicationName);

		/// <summary>
		/// Parse the specfied command line.
		/// </summary>
		/// <param name="commandLine">The command line to parse.</param>
		/// <param name="noApplicationName">An (optional) value indicating if the application name is absent from the command line arguments.</param>
		/// <returns>True, if help should be displayed; otherwise, false.</returns>
		/// <remarks>
		/// Most of the exceptions thrown by this method are derived from the
		/// <see cref="CommandLineParseException"/> class.
		/// </remarks>
		/// <exception cref="System.ArgumentException">
		/// Error parsing <paramref name="commandLine"/>
		/// </exception>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="commandLine"/> is null
		/// </exception>
		/// <exception cref="ConflictOptionException">
		/// <paramref name="commandLine"/> specifies an option that is excluded by another option
		/// that is also present.
		/// </exception>
		/// <exception cref="DuplicateOptionException">
		/// <paramref name="commandLine"/> specifies the same option multiple times
		/// </exception>
		/// <exception cref="InvalidCommandLineArgumentException">
		/// <paramref name="commandLine"/> specifies an option that is not recognized,
		/// or a boolean option with a value, or a positional value with no
		/// corresponding positional option.
		/// </exception>
		/// <exception cref="InvalidOptionValueException">
		/// <paramref name="commandLine"/> specifies an option value that cannot be
		/// converted to the corresponding property type
		/// </exception>
		/// <exception cref="MissingRequiredOptionException">
		/// <paramref name="commandLine"/> fails to specify a required option
		/// </exception>
		/// <exception cref="MissingOptionValueException">
		/// <paramref name="commandLine"/> fails to specify the value for a non-boolean option
		/// </exception>
		public bool Parse(string commandLine, bool noApplicationName = false) =>
			Parse(commandLine, out HashSet<string> presentOptions, noApplicationName);

		/// <summary>
		/// Parse the specfied command line.
		/// </summary>
		/// <param name="commandLine">The command line to parse.</param>
		/// <param name="presentOptions">A hash set of the options that were present.</param>
		/// <param name="noApplicationName">An (optional) value indicating if the application name is absent from the command line arguments.</param>
		/// <returns>True, if help should be displayed; otherwise, false.</returns>
		/// <remarks>
		/// Most of the exceptions thrown by this method are derived from the
		/// <see cref="CommandLineParseException"/> class.
		/// </remarks>
		/// <exception cref="System.ArgumentException">
		/// Error parsing <paramref name="commandLine"/>
		/// </exception>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="commandLine"/> is null
		/// </exception>
		/// <exception cref="ConflictOptionException">
		/// <paramref name="commandLine"/> specifies an option that is excluded by another option
		/// that is also present.
		/// </exception>
		/// <exception cref="DuplicateOptionException">
		/// <paramref name="commandLine"/> specifies the same option multiple times
		/// </exception>
		/// <exception cref="InvalidCommandLineArgumentException">
		/// <paramref name="commandLine"/> specifies an option that is not recognized,
		/// or a boolean option with a value, or a positional value with no
		/// corresponding positional option.
		/// </exception>
		/// <exception cref="InvalidOptionValueException">
		/// <paramref name="commandLine"/> specifies an option value that cannot be
		/// converted to the corresponding property type
		/// </exception>
		/// <exception cref="MissingRequiredOptionException">
		/// <paramref name="commandLine"/> fails to specify a required option
		/// </exception>
		/// <exception cref="MissingOptionValueException">
		/// <paramref name="commandLine"/> fails to specify the value for a non-boolean option
		/// </exception>
		public bool Parse(string commandLine, out HashSet<string> presentOptions,
			bool noApplicationName = false) =>
			Parse(GetCommandLineArgs(commandLine), out presentOptions, noApplicationName);

		/// <summary>
		/// Write help to the specified text writer.
		/// </summary>
		/// <param name="writer">The text writer used to write help.</param>
		public void WriteHelp(TextWriter writer)
		{
			if (!string.IsNullOrWhiteSpace(HelpText))
			{
				writer.WriteLine(HelpText);
				writer.WriteLine();
			}

			foreach(CommandLineOption option in Options)
				if (!string.IsNullOrEmpty(option.HelpText))
				{
					writer.WriteLine(option.HelpText);
					writer.WriteLine();
				}
		}
		#endregion // Public methods

		#region Protected methods
		/// <summary>
		/// Create a command line option for the specified class instance,
		/// property, and attribute.
		/// </summary>
		/// <param name="instance">The class instance.</param>
		/// <param name="property">The property information.</param>
		/// <param name="attribute">The <see cref="CommandLinePropertyAttribute"/> attribute.</param>
		/// <returns>The newly created command line option.</returns>
		protected virtual CommandLineOption CreateOption(object instance, PropertyInfo property,
			CommandLinePropertyAttribute attribute) =>
			new CommandLineOption(instance, property, attribute);
		#endregion // Protected methods

		#region Private methods
		private CommandLineOption CreateOption(object instance, PropertyInfo property)
		{
			var attribute = property.GetCustomAttribute<CommandLinePropertyAttribute>();
			return attribute == null ? null : CreateOption(instance, property, attribute);
		}

		private string[] GetCommandLineArgs(string commandLine)
		{
			if (commandLine == null)
				throw new ArgumentNullException(nameof(commandLine));

			IntPtr arrayOfNullTerminatedStrings = CommandLineToArgvW(commandLine, out int count);
			if (arrayOfNullTerminatedStrings == IntPtr.Zero)
				throw new ArgumentException(null, nameof(commandLine),
					new Win32Exception(Marshal.GetLastWin32Error()));

			try { return ToStringArray(arrayOfNullTerminatedStrings, count); }
			finally { LocalFree(arrayOfNullTerminatedStrings); }
		}

		private string GetHelpText(Type type, CommandLineClassAttribute attribute,
			out ResourceManager resourceManager)
		{
			try
			{
				resourceManager = attribute == null ? null :
					new ResourceManager(attribute.ResourceType.FullName,
						attribute.ResourceType.Assembly);

				return CommandLineOption.GetHelpText(resourceManager, attribute.HelpKey,
					type.Name);
			}
			catch
			{
				resourceManager = null;
				return string.Empty;
			}
		}

		private CommandLineOptionList GetOptions(Type type, object instance) =>
			new CommandLineOptionList(type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
				.Select(property => CreateOption(instance, property))
				.Where(option => option != null));

		private List<CommandLineOption> GetPositionalOptions()
		{
			List<CommandLineOption> positionalOptions = Options
				.Where(option => option.Position.HasValue)
				.OrderBy(option => option.Position.Value)
				.ToList();

			for (int index = 0; index < positionalOptions.Count; index++)
			{
				CommandLineOption option = positionalOptions[index];
				int? position = option.Position;

				if (position.HasValue && position.Value != index)
					throw new PropertyPositionException(option.PropertyName);
			}

			return positionalOptions;
		}

		private void FinalizeOptions(ResourceManager resourceManager)
		{
			foreach(CommandLineOption option in Options)
			{
				string name = option.Name;
				int nameLength = name.Length;

				int minimumLength;
				for (minimumLength = 1; minimumLength < nameLength; minimumLength++)
					if (!IsAmbiguous(name.Substring(0, minimumLength)))
						break;

				option.MinimumLength = minimumLength;
				option.AddExcludes(this);
				option.AddRequires(this);
				option.GetHelpText(resourceManager);

				if (option.Excludes.GetFirstCommon(option.Requires, out CommandLineOption common))
					throw option.CreatePropertyReferenceException(
						Resources.RequiredReferenceException_Format, common.Name, true);
			}
		}

		private HashSet<CommandLineOption> Parse(IEnumerable<string> args, bool noApplicationName,
			out bool showHelp)
		{
			if (args == null)
				throw new ArgumentNullException(nameof(args));

			// If the application name is present in command line arguments, skip it
			if (!noApplicationName)
				args = args.Skip(1);

			SetProperties(args, out showHelp, out HashSet<CommandLineOption> presentOptions, 
				out List<CommandLineOption> orderedOptions);

			if (presentOptions.Count <= 0 && !NoEmptyOptionHelp)
			{
				showHelp = true;
				return presentOptions;
			}

			foreach (CommandLineOption option in requiredOptions)
				if (!presentOptions.Contains(option))
					throw new MissingRequiredOptionException(option.Name);

			foreach (CommandLineOption option in orderedOptions)
			{
				foreach (CommandLineOption requiredOption in option.Requires)
					if (!presentOptions.Contains(requiredOption))
						throw new MissingRequiredOptionException(requiredOption.Name);

				foreach (CommandLineOption excludedOption in option.Excludes)
					if (presentOptions.Contains(excludedOption))
						throw new ConflictOptionException(excludedOption.Name, option.Name);
			}

			return presentOptions;
		}

		private KeyValuePair<string, string> ParseKeyValue(string arg)
		{
			if (arg.Length == 0 || arg[0] != OptionIndicator)
				return default(KeyValuePair<string, string>);

			int endIndex = arg.IndexOfAny(optionNameTerminators, 1);
			return new KeyValuePair<string, string>(
				endIndex < 0 ? arg.Substring(1) : arg.Substring(1, endIndex - 1),
				endIndex < 0 ? null : arg.Substring(endIndex + 1));
		}

		private bool IsAmbiguous(string prefix)
		{
			int index = Options.IndexOf(prefix, true);
			if (index < 0 || index == Options.Count - 1)
				return false;

			return Options[index + 1].CompareToPrefix(prefix) == 0;
		}

		private bool SetOption(KeyValuePair<string, string> keyValue, string arg, 
			out CommandLineOption option)
		{
			string name = keyValue.Key;
			string value = keyValue.Value;

			if (string.IsNullOrWhiteSpace(name) ||
				Options.IndexOf(name, out option, true) < 0 ||
				name.Length < option.MinimumLength)
				throw new InvalidCommandLineArgumentException(arg);

			name = option.Name;

			if (option.IsBoolean)
			{
				if (value != null)
					throw new InvalidCommandLineArgumentException(arg);

				option.SetValue(true);
				return true;
			}

			if (value == null)
				return false;

			SetOptionValue(option, value);
			return true;
		}

		private static void SetOptionValue(CommandLineOption option, string value)
		{
			try
			{
				option.SetValue(value);
			}
			catch (Exception ex)
			{
				throw new InvalidOptionValueException(option.Name, value, ex);
			}
		}

		private void SetPositionalOption(int? position, string arg, out CommandLineOption option)
		{
			if (!position.HasValue || position.Value >= positionalOptions.Count)
				throw new InvalidCommandLineArgumentException(arg);

			option = positionalOptions[position.Value];
			SetOptionValue(option, arg);
		}

		private void SetProperties(IEnumerable<string> args, out bool showHelp,
			out HashSet<CommandLineOption> presentOptions, 
			out List<CommandLineOption> orderedOptions)
		{
			presentOptions = new HashSet<CommandLineOption>();
			orderedOptions = new List<CommandLineOption>();
			int? position = 0;

			CommandLineOption pendingOption = null;
			showHelp = false;

			foreach (string arg in args)
			{
				var keyValue = ParseKeyValue(arg);
				CommandLineOption option;

				if (keyValue.Key == null)
				{
					if (pendingOption != null)
					{
						SetOptionValue(pendingOption, arg);
						pendingOption = null;
						continue;
					}

					SetPositionalOption(position, arg, out option);
					position++;
				}
				else
				{
					if (pendingOption != null)
						throw new MissingOptionValueException(pendingOption.Name);

					if (keyValue.Key == "?" && position.HasValue && position.Value == 1)
					{
						showHelp = true;
						return;
					}

					pendingOption = SetOption(keyValue, arg, out option) ? null : option;
					position = null;
				}

				if (!presentOptions.Add(option))
					throw new DuplicateOptionException(option.Name);

				orderedOptions.Add(option);
			}

			if (pendingOption != null)
				throw new MissingOptionValueException(pendingOption.Name);
		}

		private string[] ToStringArray(IntPtr arrayOfNullTerminatedStrings, int count)
		{
			var array = new string[count];

			for (int index = 0; index < count; index++)
				array[index] = Marshal.PtrToStringUni(Marshal.ReadIntPtr(
					arrayOfNullTerminatedStrings, index * IntPtr.Size));

			return array;
		}
		#endregion // Private methods

		#region Imports
		[DllImport("shell32.dll", SetLastError = true)]
		private static extern IntPtr CommandLineToArgvW(
			[MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine,
			 out int pNumArgs);

		[DllImport("kernel32.dll")]
		private static extern IntPtr LocalFree(IntPtr hMem);
		#endregion // Imports
	}
}
