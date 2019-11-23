// Copyright © 2018 Transeric Solutions.  All rights reserved.
// Author: Eric David Lynch
// License: https://www.codeproject.com/info/cpol10.aspx
using System;

namespace Transeric.CommandLine
{
	/// <summary>
	/// An attribute for a class containing command line options (attached to associated propert(ies)).
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class CommandLineClassAttribute : Attribute
	{
		#region Constructor
		/// <summary>
		/// Create an instance for the specfied resource type and help key.
		/// </summary>
		/// <param name="resourceType">The type of resources containing help text.</param>
		/// <param name="helpKey">The name of the resource containing the help text.</param>
		public CommandLineClassAttribute(Type resourceType, string helpKey = null)
		{
			ResourceType = resourceType;
			HelpKey = helpKey;
		}
		#endregion // Constructor

		#region Properties
		/// <summary>
		/// Gets the name of the resource containing the help text.
		/// </summary>
		public string HelpKey { get; }

		/// <summary>
		/// Gets the type of resources containing help text.
		/// </summary>
		public Type ResourceType { get; }
		#endregion // Properties
	}
}
