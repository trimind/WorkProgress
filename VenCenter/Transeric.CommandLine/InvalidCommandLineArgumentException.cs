// Copyright © 2018 Transeric Solutions.  All rights reserved.
// Author: Eric David Lynch
// License: https://www.codeproject.com/info/cpol10.aspx
using System;
using System.Runtime.Serialization;
using Transeric.CommandLine.Properties;

namespace Transeric.CommandLine
{
	/// <summary>
	/// This exception occurs when an invalid command line argument is encountered.
	/// </summary>
	[Serializable]
	public class InvalidCommandLineArgumentException : CommandLineParseException
	{
		#region Constructors
		/// <summary>
		/// Create a new instance of this class, with the default message and no inner exception.
		/// </summary>
		/// <param name="argument">The command line argument associated with this exception.</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="argument"/> is null
		/// </exception>
		public InvalidCommandLineArgumentException(string argument)
			: this(argument, null, null)
		{
		}

		/// <summary>
		/// Create a new instance of this class, with the specified message and inner exception.
		/// </summary>
		/// <param name="argument">The command line argument associated with this exception.</param>
		/// <param name="message">The error message that explains the reason for the exception (or null).</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="argument"/> is null
		/// </exception>
		public InvalidCommandLineArgumentException(string argument, string message)
			: this(argument, message, null)
		{
		}

		/// <summary>
		/// Create a new instance of this class, with the specified message and inner exception.
		/// </summary>
		/// <param name="argument">The command line argument associated with this exception.</param>
		/// <param name="message">The error message that explains the reason for the exception (or null).</param>
		/// <param name="innerException">The inner exception that is the cause of the current exception (or null).</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="argument"/> is null
		/// </exception>
		public InvalidCommandLineArgumentException(string argument, string message, Exception innerException)
			: base(message ?? string.Format(Resources.InvalidCommandLineArgumentException_Format,
				argument ?? throw new ArgumentNullException(nameof(argument))), innerException) =>
			Argument = argument;

		/// <summary>
		/// Create a new instance of this class from serialized data.
		/// </summary>
		/// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		public InvalidCommandLineArgumentException(SerializationInfo info, StreamingContext context)
			: base(info, context) =>
			Argument = info.GetString(nameof(Argument));
		#endregion // Constructors

		#region Properties
		/// <summary>
		/// Gets the command line argument that caused this exception.
		/// </summary>
		public string Argument { get; }
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
			info.AddValue(nameof(Argument), Argument);
		}
		#endregion // Public methods
	}
}
