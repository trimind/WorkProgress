// Copyright © 2018 Transeric Solutions.  All rights reserved.
// Author: Eric David Lynch
// License: https://www.codeproject.com/info/cpol10.aspx
using System;
using System.Runtime.Serialization;
using Transeric.CommandLine.Properties;

namespace Transeric.CommandLine
{
	/// <summary>
	/// This exception occurs when a command line option is missing a required value.
	/// </summary>
	[Serializable]
	public class MissingOptionValueException : CommandLineOptionException
	{
		#region Constructors
		/// <summary>
		/// Create a new instance of this class, with the default message and no inner exception.
		/// </summary>
		/// <param name="optionName">The name of the option associated with this exception.</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="optionName"/> is null
		/// </exception>
		public MissingOptionValueException(string optionName)
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
		public MissingOptionValueException(string optionName, string message)
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
		public MissingOptionValueException(string optionName, string message, Exception innerException)
			: base(optionName, message ?? string.Format(Resources.MissingOptionValueException_Format,
				optionName ?? throw new ArgumentNullException(nameof(optionName))), innerException)
		{
		}

		/// <summary>
		/// Create a new instance of this class from serialized data.
		/// </summary>
		/// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		public MissingOptionValueException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
		#endregion // Constructors
	}
}
