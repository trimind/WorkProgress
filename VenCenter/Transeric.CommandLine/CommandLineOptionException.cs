// Copyright © 2018 Transeric Solutions.  All rights reserved.
// Author: Eric David Lynch
// License: https://www.codeproject.com/info/cpol10.aspx
using System;
using System.Runtime.Serialization;
using Transeric.CommandLine.Properties;

namespace Transeric.CommandLine
{
	/// <summary>
	/// An unexpected exception occurred while parsing a specific command line option.
	/// </summary>
	[Serializable]
	public class CommandLineOptionException : CommandLineParseException
	{
		#region Constructors
		/// <summary>
		/// Create a new instance of this class, with the default message and no inner exception.
		/// </summary>
		/// <param name="optionName">The name of the option associated with this exception.</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="optionName"/> is null
		/// </exception>
		public CommandLineOptionException(string optionName)
			: this(optionName, null, null)
		{
		}

		/// <summary>
		/// Create a new instance of this class, with the specified message and inner exception.
		/// </summary>
		/// <param name="optionName">The name of the option associated with this exception.</param>
		/// <param name="message">The error message that explains the reason for the exception (or null).</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="optionName"/> is null
		/// </exception>
		public CommandLineOptionException(string optionName, string message)
			: this(optionName, message, null)
		{
		}

		/// <summary>
		/// Create a new instance of this class, with the specified message and inner exception.
		/// </summary>
		/// <param name="optionName">The name of the option associated with this exception.</param>
		/// <param name="message">The error message that explains the reason for the exception (or null).</param>
		/// <param name="innerException">The inner exception that is the cause of the current exception (or null).</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="optionName"/> is null
		/// </exception>
		public CommandLineOptionException(string optionName, string message, Exception innerException)
			: base(message ?? string.Format(Resources.CommandLineOptionException_Format,
				optionName ?? throw new ArgumentNullException(nameof(optionName))), innerException) =>
			OptionName = optionName;

		/// <summary>
		/// Create a new instance of this class from serialized data.
		/// </summary>
		/// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		public CommandLineOptionException(SerializationInfo info, StreamingContext context)
			: base(info, context) =>
			OptionName = info.GetString(nameof(OptionName));
		#endregion // Constructors

		#region Properties
		/// <summary>
		/// Gets the name of the option associated with this exception.
		/// </summary>
		public string OptionName { get; }
		#endregion // Properties

		#region Public methods
		/// <summary>
		/// Sets the <see cref="System.Runtime.Serialization.SerializationInfo"/> with information
		/// about the exception.
		/// </summary>
		/// <param name="info">The information about the exception.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="System.ArgumentNullException">info is null</exception>
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue(nameof(OptionName), OptionName);
		}
		#endregion // Public methods
	}
}
