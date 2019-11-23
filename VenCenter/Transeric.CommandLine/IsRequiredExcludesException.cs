// Copyright © 2018 Transeric Solutions.  All rights reserved.
// Author: Eric David Lynch
// License: https://www.codeproject.com/info/cpol10.aspx
using System;
using System.Runtime.Serialization;
using Transeric.CommandLine.Properties;

namespace Transeric.CommandLine
{
	/// <summary>
	/// This exception occurs when a property has a <see cref="CommandLinePropertyAttribute"/>
	/// attribute that specifies both 'isRequired' and 'excludes'.
	/// </summary>
	/// <remarks>
	/// When a required option attempts to exclude another option, it would prevent that option
	/// from ever being specified on the command line.
	/// </remarks>
	[Serializable]
	public class IsRequiredExcludesException : CommandLinePropertyException
	{
		#region Constructors
		/// <summary>
		/// Create a new instance of this class, with the default message and no inner exception.
		/// </summary>
		/// <param name="propertyName">The name of the property associated with this exception.</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="propertyName"/> is null
		/// </exception>
		public IsRequiredExcludesException(string propertyName)
			: this(propertyName, null, null)
		{
		}

		/// <summary>
		/// Create a new instance of this class, with the specified message and inner exception.
		/// </summary>
		/// <param name="propertyName">The name of the property associated with this exception.</param>
		/// <param name="message">The error message that explains the reason for the exception (or null).</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="propertyName"/> is null
		/// </exception>
		public IsRequiredExcludesException(string propertyName, string message)
			: this(propertyName, message, null)
		{
		}

		/// <summary>
		/// Create a new instance of this class, with the specified message and inner exception.
		/// </summary>
		/// <param name="propertyName">The name of the property associated with this exception.</param>
		/// <param name="message">The error message that explains the reason for the exception (or null).</param>
		/// <param name="innerException">The inner exception that is the cause of the current exception (or null).</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="propertyName"/> is null
		/// </exception>
		public IsRequiredExcludesException(string propertyName, string message, Exception innerException)
			: base(propertyName, message ?? string.Format(Resources.IsRequiredExcludesException_Format,
				propertyName ?? throw new ArgumentNullException(nameof(propertyName))), innerException)
		{
		}

		/// <summary>
		/// Create a new instance of this class from serialized data.
		/// </summary>
		/// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		public IsRequiredExcludesException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
		#endregion // Constructors
	}
}
