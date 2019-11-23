// Copyright © 2018 Transeric Solutions.  All rights reserved.
// Author: Eric David Lynch
// License: https://www.codeproject.com/info/cpol10.aspx
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Resources;
using Transeric.CommandLine.Properties;

namespace Transeric.CommandLine
{
	/// <summary>
	/// A command line option attached to a specific property of a specific class instance.
	/// </summary>
	public class CommandLineOption : IComparable, IComparable<CommandLineOption>,
		IComparable<string>, IEquatable<CommandLineOption>
	{
		#region Constructor
		/// <summary>
		/// Create an option for the specified class instance, attached to the
		/// specified property, with the specified attribute.
		/// </summary>
		/// <param name="instance">The class instance to which this option is attached.</param>
		/// <param name="property">The property to which this option is attached.</param>
		/// <param name="attribute">The attribute that describes this option.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="instance"/>, <paramref name="property"/>, or <paramref name="attribute"/>
		/// is null
		/// </exception>
		/// <exception cref="BooleanRequiredOrPositionalException">
		/// <see cref="property"/> specifies a boolean property and
		/// <paramref name="attribute"/> specifies either
		/// <see cref="CommandLinePropertyAttribute.IsRequired"/> or
		/// <see cref="CommandLinePropertyAttribute.Position"/>.
		/// </exception>
		/// <exception cref="InvalidOptionNameException">
		/// <paramref name="attribute"/> specifies an invalid
		/// <see cref="CommandLinePropertyAttribute.Name"/>
		/// </exception>
		/// <exception cref="IsRequiredExcludesException">
		/// <paramref name="attribute"/> specifies both
		/// <see cref="CommandLinePropertyAttribute.IsRequired"/> and
		/// <see cref="CommandLinePropertyAttribute.Excludes" />
		/// </exception>
		/// <exception cref="NoSetterPropertyException">
		/// <see cref="property"/> specifies a property without a public setter method
		/// </exception>
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors",
			Justification = nameof(IsValidName) + "is called, but does not reference any class members.")]
		public CommandLineOption(object instance, PropertyInfo property,
			CommandLinePropertyAttribute attribute)
		{
			this.instance = instance ?? throw new ArgumentNullException(nameof(instance));
			this.property = property ?? throw new ArgumentNullException(nameof(property));
			this.attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));

			if (attribute.Name == null || !IsValidName(attribute.Name))
				throw new InvalidOptionNameException(property.Name);

			if (attribute.IsRequired && attribute.Excludes != null)
				throw new IsRequiredExcludesException(property.Name);

			IsBoolean = property.PropertyType.IsAssignableFrom(typeof(bool));
			if (IsBoolean && (attribute.IsRequired || attribute.Position.HasValue))
				throw new BooleanRequiredOrPositionalException(property.Name);

			if (property.GetSetMethod() == null)
				throw new NoPropertySetterException(nameof(instance));

			Type type = property.PropertyType;
			canBeSetToNull = !type.IsValueType || (Nullable.GetUnderlyingType(type) != null);

			memberType = GetMemberType(type);
			if (memberType != null)
				converter = TypeDescriptor.GetConverter(memberType);

			converter = converter ?? TypeDescriptor.GetConverter(type);
			if (converter == null)
				throw new ArgumentException(nameof(property));

			Excludes = new CommandLineOptionList();
			Requires = new CommandLineOptionList();
		}
		#endregion // Constructor

		#region Constants
		/// <summary>
		/// The suffix added to the property name to provide a default resource name for help text.
		/// </summary>
		public const string HelpSuffix = "_Help";
		#endregion // Constants

		#region Private data
		private readonly Type memberType;
		private readonly bool canBeSetToNull;
		private readonly TypeConverter converter;
		private readonly CommandLinePropertyAttribute attribute;
		private readonly object instance;
		private readonly PropertyInfo property;
		#endregion // Private data

		#region Properties
		/// <summary>
		/// Gets an (optional) list of other options, which must NOT be present when this option
		/// is specified.
		/// </summary>
		public CommandLineOptionList Excludes { get; }

		/// <summary>
		/// Gets the (optional) help text for this option.
		/// </summary>
		public string HelpText { get; private set; } = string.Empty;

		/// <summary>
		/// Gets a value indicating if this is a boolean option.
		/// </summary>
		public bool IsBoolean { get; }

		/// <summary>
		/// Gets an (optional) value indicating if this command line option is always required.
		/// </summary>
		public bool IsRequired => attribute.IsRequired;

		/// <summary>
		/// Gets an (optional) character used to separate items in a list.
		/// </summary>
		public char ListSeparatorChar => attribute.ListSeparatorChar;

		/// <summary>
		/// Gets the minimum name length required to avoid ambiguity.
		/// </summary>
		public int MinimumLength { get; internal set; } = 1;

		/// <summary>
		/// Gets the name of this command line option.
		/// </summary>
		public string Name => attribute.Name;

		/// <summary>
		/// Gets the (optional) zero-based position of this argument, within the argument list, when
		/// not specified by name.
		/// </summary>
		/// <remarks>
		/// An option can be specified either by argument position, or by name, never by both.
		/// 
		/// Position Example: command value1.
		/// Name Example: command -name1 value1.
		/// Invalid Example: command value1 -name1 value1.
		/// </remarks>
		public int? Position => attribute.Position;

		/// <summary>
		/// Gets the name of the property to which this option is attached.
		/// </summary>
		public string PropertyName => property.Name;

		/// <summary>
		/// Gets the type of property to which this option is attached.
		/// </summary>
		public Type PropertyType => property.PropertyType;

		/// <summary>
		/// Gets an (optional) list of other options, which must be present when this option
		/// is specified.
		/// </summary>
		public CommandLineOptionList Requires { get; }
		#endregion // Properties

		#region Public methods
		/// <summary>
		/// Compare this instance to another instance.
		/// </summary>
		/// <param name="other">The other instance to which this instance is compared.</param>
		/// <returns>
		/// negative, if this is less than <paramref name="other"/>; 
		/// zero, if this is equal to <paramref name="other"/>;
		/// positive, if this is greater than <paramref name="other"/>.
		/// </returns>
		public int CompareTo(CommandLineOption other) =>
			other == null ? -1 : CompareTo(other.Name);

		/// <summary>
		/// Compare this instance to the specified name.
		/// </summary>
		/// <param name="name">The name to which this instance is compared.</param>
		/// <returns>
		/// negative, if this is less than <paramref name="name"/>; 
		/// zero, if this is equal to <paramref name="name"/>;
		/// positive, if this is greater than <paramref name="name"/>.
		/// </returns>
		public int CompareTo(string name) =>
			name == null ? -1 : string.CompareOrdinal(Name, name);

		/// <summary>
		/// Compare this instance to another object.
		/// </summary>
		/// <param name="obj">The other object to which this instance is compared.</param>
		/// <returns>
		/// negative, if this is less than <paramref name="obj"/>; 
		/// zero, if this is equal to <paramref name="obj"/>;
		/// positive, if this is greater than <paramref name="obj"/>.
		/// </returns>
		public int CompareTo(object obj) =>
			obj is CommandLineOption other ? CompareTo(other) :
				(obj is string name ? CompareTo(name) : -1);

		/// <summary>
		/// Compare this instance to the specified prefix.
		/// </summary>
		/// <param name="prefix">The prefix to which this instance is compared.</param>
		/// <returns>
		/// negative, if this is less than <paramref name="prefix"/>; 
		/// zero, if this is equal to <paramref name="prefix"/>;
		/// positive, if this is greater than <paramref name="prefix"/>.
		/// </returns>
		public int CompareToPrefix(string prefix)
		{
			if (prefix == null)
				return -1;

			string name = Name;
			int nameLength = name.Length;
			int prefixLength = prefix.Length;

			for (int index = 0; index < prefixLength; index++)
			{
				int prefixChar = prefix[index];
				if (index >= nameLength)
					break;

				int nameChar = name[index];
				if (nameChar < prefixChar)
					return -1;

				if (nameChar > prefixChar)
					return 1;
			}

			return prefixLength <= nameLength ? 0 : -1;
		}

		/// <summary>
		/// Get a value indicating if this instance is equal to another instance.
		/// </summary>
		/// <param name="other">The other instance to which this instance is compared.</param>
		/// <returns>True, if this instance is equal to <paramref name="other"/>; otherwise, false.</returns>
		public bool Equals(CommandLineOption other) =>
			other != null && Name.Equals(other.Name);

		/// <summary>
		/// Get a value indicating if this instance is equal to another object.
		/// </summary>
		/// <param name="obj">The other object to which this instance is compared.</param>
		/// <returns>True, if this instance is equal to <paramref name="obj"/>; otherwise, false.</returns>
		public override bool Equals(object obj) =>
			obj is CommandLineOption other ? Equals(other) : false;

		/// <summary>
		/// Get a hash code for this instance.
		/// </summary>
		/// <returns>A hash code for this instance.</returns>
		public override int GetHashCode() =>
			Name.GetHashCode();

		/// <summary>
		/// Get the current value of this option.
		/// </summary>
		/// <returns>The current value of this option.</returns>
		public object GetValue() =>
			property.GetValue(instance);

		/// <summary>
		/// Set the value of this option.
		/// </summary>
		/// <param name="value">The new value of this option.</param>
		/// <exception cref="System.ArgumentException">
		/// <paramref name="value"/> value is not valid for this option.
		/// </exception>
		public void SetValue(object value)
		{
			if (value == null)
			{
				if (!canBeSetToNull)
					throw new ArgumentNullException(nameof(value));

				property.SetValue(instance, value);
				return;
			}

			Type type = value.GetType();
			if (property.PropertyType.IsAssignableFrom(type))
			{
				property.SetValue(instance, value);
				return;
			}

			if (converter == null || !converter.CanConvertFrom(type))
				throw new ArgumentException(nameof(value));

			if (value is string text)
			{
				if (memberType != null)
				{
					property.SetValue(instance, CreateList(text));
					return;
				}

				if (PropertyType != typeof(string))
					value = text = text.Trim();

				if (PropertyType.IsEnum && ListSeparatorChar != ',')
					value = text.Replace(ListSeparatorChar, ',');
			}

			property.SetValue(instance, converter.ConvertFrom(value));
		}

		/// <summary>
		/// Get a text representation of this instance.
		/// </summary>
		/// <returns>A text representation of this instance.</returns>
		public override string ToString() =>
			Name;
		#endregion // Public methods

		#region Protected methods
		/// <summary>
		/// Gets a value indicating if the specified option name is valid.
		/// </summary>
		/// <param name="name">The name to check.</param>
		/// <returns>True, if the name is valid; otherwise, false.</returns>
		/// <remarks>
		/// WARNING: This method must not reference any members of this class
		/// <see cref="CommandLineOption"/> class, or side-effects may result.
		/// </remarks>
		protected virtual bool IsValidName(string name) =>
			name.Length >= 1 && Char.IsLetter(name[0]) &&
				!name.Any(chr => !Char.IsLetterOrDigit(chr));
		#endregion // Protected methods

		#region Internal methods
		/// <summary>
		/// Add excluded options from property's <see cref="CommandLinePropertyAttributeAttribute"/> attribute.
		/// </summary>
		/// <param name="parser">The command line parser.</param>
		/// <exception cref="PropertyReferenceException">
		/// <paramref name="attribute"/> specifies an invalid option reference in either
		/// <see cref="CommandLinePropertyAttribute.Excludes"/> or
		/// <see cref="CommandLinePropertyAttribute.Requires"/>.
		/// </exception>
		internal void AddExcludes(CommandLineParser parser) =>
			Excludes.AddRange(GetOptions(parser, attribute.Excludes, true)
				.OrderBy(option => option));

		/// <summary>
		/// Add required options from property's <see cref="CommandLinePropertyAttributeAttribute"/> attribute.
		/// </summary>
		/// <param name="parser">The command line parser.</param>
		/// <exception cref="PropertyReferenceException">
		/// <paramref name="attribute"/> specifies an invalid option reference in either
		/// <see cref="CommandLinePropertyAttribute.Excludes"/> or
		/// <see cref="CommandLinePropertyAttribute.Requires"/>.
		/// </exception>
		internal void AddRequires(CommandLineParser parser) =>
			Requires.AddRange(GetOptions(parser, attribute.Requires, false)
				.OrderBy(option => option));

		/// <summary>
		/// Create a property reference exception for this property.
		/// </summary>
		/// <param name="format">The message format string.</param>
		/// <param name="isExcludes">A value indicating if the exception is related to the 'excludes' parameter.</param>
		/// <returns>The newly created property reference exception.</returns>
		internal PropertyReferenceException CreatePropertyReferenceException(
			string format, string optionName, bool isExcludes)
		{
			string parameterName = isExcludes ? nameof(CommandLinePropertyAttribute.Excludes) :
				nameof(CommandLinePropertyAttribute.Requires);
			return new PropertyReferenceException(PropertyName, optionName, parameterName,
				string.Format(format, PropertyName, optionName, parameterName));
		}

		/// <summary>
		/// Get the specified help key (if non-whitespace) or the default help key for the specified entity name.
		/// </summary>
		/// <param name="helpKey">The help key.</param>
		/// <param name="entityName">The entity name.</param>
		/// <returns>The help key.</returns>
		internal static string GetHelpText(ResourceManager resourceManager, string helpKey,
			string entityName)
		{
			helpKey = string.IsNullOrWhiteSpace(helpKey) ? entityName + HelpSuffix : helpKey;

			try { return resourceManager?.GetString(helpKey) ?? string.Empty; }
			catch { return string.Empty; }
		}

		/// <summary>
		/// Get the help text from the specified resource manager.
		/// </summary>
		/// <param name="resourceManager">The resource manager used to get the help text.</param>
		internal void GetHelpText(ResourceManager resourceManager) =>
			HelpText = GetHelpText(resourceManager, attribute.HelpKey, PropertyName);
		#endregion // Internal methods

		#region Private methods
		private object CreateInstance(int count) =>
			PropertyType.IsArray ? Activator.CreateInstance(PropertyType, new object[] { count }) :
				Activator.CreateInstance(PropertyType);

		private List<object> ConvertList(string text)
		{
			bool isString = memberType == typeof(string);
			return text.Split(ListSeparatorChar)
				.Select(value => converter.ConvertFrom(isString ? value : value.Trim()))
				.ToList();
		}

		private object CreateList(string text)
		{
			List<object> values = ConvertList(text);

			var list = CreateInstance(values.Count) as IList;
			var disposable = list as IDisposable;

			try
			{
				int index = 0;

				foreach (object value in values)
				{
					if (index < list.Count)
						list[index] = value;
					else
						list.Add(value);
					index++;
				}

				return list;
			}
			catch
			{
				if (disposable != null)
				{
					try { disposable.Dispose(); }
					catch { }
				}

				throw;
			}
		}

		private Type GetMemberType(Type type)
		{
			// If not a concreate array or list, with a well-defined type, do not allow it
			if (!type.IsClass || type.IsAbstract || (!type.IsArray && !type.IsGenericType) ||
				!typeof(IList).IsAssignableFrom(type))
				return null;

			try
			{
				// If we cannot instantiate it, do not allow it
				var instance = CreateInstance(0) as IList;
				if (instance == null)
					return null;

				if (instance is IDisposable disposable)
					disposable.Dispose();

				if (type.IsArray)
					return type.GetElementType();

				Type[] argumentTypes = type.GetGenericArguments();
				if (argumentTypes.Length != 1)
					throw new ArgumentException(null, nameof(property));

				return argumentTypes[0];
			}
			catch
			{
				return null;
			}
		}

		private IEnumerable<CommandLineOption> GetOptions(CommandLineParser parser,
			string text, bool isExcludes)
		{
			if (string.IsNullOrWhiteSpace(text))
				yield break;

			HashSet<string> optionNames = new HashSet<string>();

			foreach (string optionName in text.Split(','))
			{
				if (optionName == Name)
					throw CreatePropertyReferenceException(Resources.SelfReferenceException_Format,
						optionName, isExcludes);

				if (!parser.Options.TryGetValue(optionName, out CommandLineOption option))
					throw CreatePropertyReferenceException(Resources.NotFoundReferenceException_Format,
						optionName, isExcludes);

				if (isExcludes && option.IsRequired)
					throw CreatePropertyReferenceException(Resources.RequiredReferenceException_Format,
						optionName, isExcludes);

				if (!optionNames.Add(optionName))
					throw CreatePropertyReferenceException(Resources.DuplicateReferenceException_Format,
						optionName, isExcludes);

				yield return option;
			}
		}
		#endregion // Private methods
	}
}
