// Copyright © 2018 Transeric Solutions.  All rights reserved.
// Author: Eric David Lynch
// License: https://www.codeproject.com/info/cpol10.aspx
using System;
using System.Runtime.Serialization;
using Transeric.CommandLine.Properties;

namespace Transeric.CommandLine
{
	/// <summary>
	/// This exception occurs when two different command line options conflict.
	/// </summary>
	[Serializable]
	public class ConflictOptionException : CommandLineOptionException
	{
		#region Constructors
		/// <summary>
		/// Create a new instance of this class, with the default message and no inner exception.
		/// </summary>
		/// <param name="optionName">The name of the option associated with this exception.</param>
		/// <param name="conflictOptionName">The name of the option that caused the conflict.</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="optionName"/> or <paramref name="conflictOptionName"/> is null
		/// </exception>
		public ConflictOptionException(string optionName, string conflictOptionName)
			: this(optionName, conflictOptionName, null, null)
		{
		}

		/// <summary>
		/// Create a new instance of this class, with the specified message and inner exception.
		/// </summary>
		/// <param name="optionName">The name of the option associated with this exception.</param>
		/// <param name="conflictOptionName">The name of the option that caused the conflict.</param>
		/// <param name="message">The error message that explains the reason for the exception (or null).</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="optionName"/> or <paramref name="conflictOptionName"/> is null
		/// </exception>
		public ConflictOptionException(string optionName, string conflictOptionName, string message)
			: this(optionName, conflictOptionName, message, null)
		{
		}

		/// <summary>
		/// Create a new instance of this class, with the specified message and inner exception.
		/// </summary>
		/// <param name="optionName">The name of the option associated with this exception.</param>
		/// <param name="conflictOptionName">The name of the option that caused the conflict.</param>
		/// <param name="message">The error message that explains the reason for the exception (or null).</param>
		/// <param name="innerException">The inner exception that is the cause of the current exception (or null).</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="optionName"/> or <paramref name="conflictOptionName"/> is null
		/// </exception>
		public ConflictOptionException(string optionName, string conflictOptionName,
			string message, Exception innerException)
			: base(optionName, message ?? string.Format(Resources.ConflictOptionException_Format,
				optionName ?? throw new ArgumentNullException(nameof(optionName)),
				conflictOptionName ?? throw new ArgumentNullException(nameof(conflictOptionName))),
				innerException) =>
			ConflictOptionName = conflictOptionName;

		/// <summary>
		/// Create a new instance of this class from serialized data.
		/// </summary>
		/// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		public ConflictOptionException(SerializationInfo info, StreamingContext context)
			: base(info, context) =>
			ConflictOptionName = info.GetString(nameof(ConflictOptionName));
		#endregion // Constructors

		#region Properties
		/// <summary>
		/// Gets the name of the option that caused the conflict.
		/// </summary>
		public string ConflictOptionName { get; }
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
			info.AddValue(nameof(ConflictOptionName), ConflictOptionName);
		}
		#endregion // Public methods
	}
}
