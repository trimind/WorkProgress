// Copyright © 2018 Transeric Solutions.  All rights reserved.
// Author: Eric David Lynch
// License: https://www.codeproject.com/info/cpol10.aspx
using System;
using System.Runtime.Serialization;
using Transeric.CommandLine.Properties;

namespace Transeric.CommandLine
{
	/// <summary>
	/// This exception occurs when the value of an option is not valid.
	/// </summary>
	[Serializable]
	public class InvalidOptionValueException : CommandLineOptionException
	{
		#region Constructors
		/// <summary>
		/// Create a new instance of this class, with the default message and no inner exception.
		/// </summary>
		/// <param name="optionName">The name of the option associated with this exception.</param>
		/// <param name="optionValue">The value of the option that caused this exception.</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="optionName"/> or <paramref name="optionValue"/> is null
		/// </exception>
		public InvalidOptionValueException(string optionName, string optionValue)
			: this(optionName, optionValue, null, null)
		{
		}

		/// <summary>
		/// Create a new instance of this class, with the specified message and inner exception.
		/// </summary>
		/// <param name="optionName">The name of the option associated with this exception.</param>
		/// <param name="optionValue">The value of the option that caused this exception.</param>
		/// <param name="message">The error message that explains the reason for the exception (or null).</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="optionName"/> or <paramref name="optionValue"/> is null
		/// </exception>
		public InvalidOptionValueException(string optionName, string optionValue, string message)
			: this(optionName, optionValue, message, null)
		{
		}

		/// <summary>
		/// Create a new instance of this class, with the specified message and inner exception.
		/// </summary>
		/// <param name="optionName">The name of the option associated with this exception.</param>
		/// <param name="optionValue">The value of the option that caused this exception.</param>
		/// <param name="innerException">The inner exception that is the cause of the current exception (or null).</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="optionName"/> or <paramref name="optionValue"/> is null
		/// </exception>
		public InvalidOptionValueException(string optionName, string optionValue,
			Exception innerException)
			: this(optionName, optionValue, null, innerException)
		{
		}

		/// <summary>
		/// Create a new instance of this class, with the specified message and inner exception.
		/// </summary>
		/// <param name="optionName">The name of the option associated with this exception.</param>
		/// <param name="optionValue">The value of the option that caused this exception.</param>
		/// <param name="message">The error message that explains the reason for the exception (or null).</param>
		/// <param name="innerException">The inner exception that is the cause of the current exception (or null).</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="optionName"/> or <paramref name="optionValue"/> is null
		/// </exception>
		public InvalidOptionValueException(string optionName, string optionValue,
			string message, Exception innerException)
			: base(optionName, message ?? string.Format(Resources.InvalidOptionValueException_Format,
				optionName ?? throw new ArgumentNullException(nameof(optionName)),
				optionValue ?? throw new ArgumentNullException(nameof(optionValue))),
				innerException) =>
			OptionValue = optionValue;

		/// <summary>
		/// Create a new instance of this class from serialized data.
		/// </summary>
		/// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		public InvalidOptionValueException(SerializationInfo info, StreamingContext context)
			: base(info, context) =>
			OptionValue = info.GetString(nameof(OptionValue));
		#endregion // Constructors

		#region Properties
		/// <summary>
		/// Gets the value of the option that caused the exception.
		/// </summary>
		public string OptionValue { get; }
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
			info.AddValue(nameof(OptionValue), OptionValue);
		}
		#endregion // Public methods
	}
}
