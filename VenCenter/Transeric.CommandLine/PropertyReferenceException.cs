// Copyright © 2018 Transeric Solutions.  All rights reserved.
// Author: Eric David Lynch
// License: https://www.codeproject.com/info/cpol10.aspx
using System;
using System.Runtime.Serialization;
using Transeric.CommandLine.Properties;

namespace Transeric.CommandLine
{
	/// <summary>
	/// This exception occurs when a property specifies an invalid option reference in
	/// either the <see cref="CommandLinePropertyAttribute.Excludes"/> or
	/// <see cref="CommandLinePropertyAttribute.Requires"/> property of the
	/// associated <see cref="CommandLinePropertyAttribute"/> attribute.
	/// </summary>
	[Serializable]
	public class PropertyReferenceException : CommandLinePropertyException
	{
		#region Constructors
		/// <summary>
		/// Create a new instance of this class, with the default message and no inner exception.
		/// </summary>
		/// <param name="propertyName">The name of the property associated with this exception.</param>
		/// <param name="optionName">The name of the option (within 'requires' or 'excludes') associated with this exception.</param>
		/// <param name="parameterName">The name of parameter ('requires' or 'excludes') associated with this exception.</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="propertyName"/>, <paramref name="optionName"/>, or
		/// <paramref name="parameterName"/> is null
		/// </exception>
		public PropertyReferenceException(string propertyName, string optionName, string parameterName)
			: this(propertyName, optionName, parameterName, null, null)
		{
		}

		/// <summary>
		/// Create a new instance of this class, with the specified message and inner exception.
		/// </summary>
		/// <param name="propertyName">The name of the property associated with this exception.</param>
		/// <param name="optionName">The name of the option (within 'requires' or 'excludes') associated with this exception.</param>
		/// <param name="parameterName">The name of parameter ('requires' or 'excludes') associated with this exception.</param>
		/// <param name="message">The error message that explains the reason for the exception (or null).</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="propertyName"/>, <paramref name="optionName"/>, or
		/// <paramref name="parameterName"/> is null
		/// </exception>
		public PropertyReferenceException(string propertyName, string optionName,
			string parameterName, string message)
			: this(propertyName, optionName, parameterName, message, null)
		{
		}

		/// <summary>
		/// Create a new instance of this class, with the specified message and inner exception.
		/// </summary>
		/// <param name="propertyName">The name of the property associated with this exception.</param>
		/// <param name="optionName">The name of the option (within 'requires' or 'excludes') associated with this exception.</param>
		/// <param name="parameterName">The name of parameter ('requires' or 'excludes') associated with this exception.</param>
		/// <param name="message">The error message that explains the reason for the exception (or null).</param>
		/// <param name="innerException">The inner exception that is the cause of the current exception (or null).</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="propertyName"/>, <paramref name="optionName"/>, or
		/// <paramref name="parameterName"/> is null
		/// </exception>
		public PropertyReferenceException(string propertyName, string optionName, string parameterName,
			string message, Exception innerException)
			: base(propertyName, message ?? string.Format(Resources.PropertyReferenceException_Format,
				propertyName ?? throw new ArgumentNullException(nameof(propertyName)),
				optionName ?? throw new ArgumentNullException(nameof(optionName)),
				parameterName ?? throw new ArgumentNullException(nameof(parameterName))),
				innerException)
		{
			ParameterName = parameterName;
			OptionName = optionName;
		}

		/// <summary>
		/// Create a new instance of this class from serialized data.
		/// </summary>
		/// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		public PropertyReferenceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			OptionName = info.GetString(nameof(OptionName));
			ParameterName = info.GetString(nameof(ParameterName));
		}
		#endregion // Constructors

		#region Properties
		/// <summary>
		/// Gets the name of the option associated with this exception.
		/// </summary>
		public string OptionName { get; }

		/// <summary>
		/// Gets the name of the parameter associated with this exception.
		/// </summary>
		public string ParameterName { get; }
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
			info.AddValue(nameof(ParameterName), ParameterName);
		}
		#endregion // Public methods
	}
}
