// Copyright © 2018 Transeric Solutions.  All rights reserved.
// Author: Eric David Lynch
// License: https://www.codeproject.com/info/cpol10.aspx
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transeric.CommandLine
{
	/// <summary>
	/// An attribute for a single command line option (attached to an associated property).
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public sealed class CommandLinePropertyAttribute : Attribute
    {
		#region Constructors
		/// <summary>
		/// Create an instance with the specified name, help key, required co-present options,
		/// excluded co-present options, and the optional position of argument.
		/// </summary>
		/// <param name="name">The name of the command line option.</param>
		/// <param name="helpKey">The (optional) name of the resource that provides help text for this option.</param>
		/// <param name="requires">An (optional) comma-separated list of other option names, which must be present when this option is specified.</param>
		/// <param name="excludes">An (optional) comma-separated list of other option names, which must NOT be present when this option is specified.</param>
		/// <param name="position">The (optional) zero-based position of this argument, within the argument list, when not specified by name.</param>
		/// <param name="isRequired">An (optional) value indicating if this command line option is always required.</param>
		/// <param name="listSeparatorChar">An (optional) character used to separate items in a list.</param>
		public CommandLinePropertyAttribute(string name, int position,
			string helpKey = null, string requires = null, string excludes = null,
			bool isRequired = false, char listSeparatorChar = ',')
			: this(name, helpKey, requires, excludes, isRequired) =>
			Position = position;

		/// <summary>
		/// Create an instance with the specified name, help key, required co-present options,
		/// excluded co-present options, and the optional position of argument.
		/// </summary>
		/// <param name="name">The name of the command line option.</param>
		/// <param name="helpKey">The (optional) name of the resource that provides help text for this option.</param>
		/// <param name="requires">An (optional) comma-separated list of other option names, which must be present when this option is specified.</param>
		/// <param name="excludes">An (optional) comma-separated list of other option names, which must NOT be present when this option is specified.</param>
		/// <param name="isRequired">An (optional) value indicating if this command line option is always required.</param>
		/// <param name="listSeparatorChar">An (optional) character used to separate items in a list.</param>
		public CommandLinePropertyAttribute(string name, string helpKey = null,
			string requires = null, string excludes = null, bool isRequired = false,
			char listSeparatorChar = ',')
		{
			Name = name?.Trim();
			HelpKey = helpKey;
			Requires = CleanList(requires);
			Excludes = CleanList(excludes);
			IsRequired = isRequired;
			ListSeparatorChar = listSeparatorChar;
		}
		#endregion // Constructors

		#region Properties
		/// <summary>
		/// Gets an (optional) comma-separated list of other option names, which must NOT be present
		/// when this option is specified.
		/// </summary>
		public string Excludes { get; }

		/// <summary>
		/// Gets the (optional) name of the resource that provides help text for this option.
		/// </summary>
		public string HelpKey { get; }

		/// <summary>
		/// Gets an (optional) value indicating if this command line option is always required.
		/// </summary>
		public bool IsRequired { get; }

		/// <summary>
		/// Gets an (optional) character used to separate items in a list.
		/// </summary>
		public char ListSeparatorChar { get; }

		/// <summary>
		/// Gets the name of the command line option.
		/// </summary>
		/// <remarks>
		/// If the attribute is bound to a boolean property, the option can only be present or
		/// absent (it has no value); otherwise, it is assumed the option will be followed by a value.
		/// </remarks>
		public string Name { get; }

		/// <summary>
		/// Gets the (optional) zero-based position of this argument, within the argument list, when not specified by name.
		/// </summary>
		/// <remarks>
		/// An option can be specified either by argument position, or by name, never by both.
		/// 
		/// Position Example: command value.
		/// Name Example: command -name value.
		/// Invalid Example: command value1 -name1 value1.
		/// </remarks>
		public int? Position { get; }

		/// <summary>
		/// Gets an (optional) comma-separated list of other option names, which must be present
		/// when this option is specified.
		/// </summary>
		public string Requires { get; }
		#endregion // Properties

		#region Private methods
		private string CleanList(string list) =>
			list == null ? null : string.Join(",", PruneList(list));

		private IEnumerable<string> PruneList(string list) =>
			list.Split(',')
				.Where(item => !string.IsNullOrWhiteSpace(item))
				.Select(item => item.Trim());
		#endregion // Private methods
	}
}
