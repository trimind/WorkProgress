// Copyright © 2018 Transeric Solutions.  All rights reserved.
// Author: Eric David Lynch
// License: https://www.codeproject.com/info/cpol10.aspx
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Transeric.CommandLine
{
	/// <summary>
	/// A sorted list of command list options.
	/// </summary>
	public class CommandLineOptionList : IReadOnlyList<CommandLineOption>
	{
		#region Constructors
		/// <summary>
		/// Creates an instance from the specified options.
		/// </summary>
		/// <param name="options">The options for the list.</param>
		public CommandLineOptionList(IEnumerable<CommandLineOption> options) =>
			this.options = new List<CommandLineOption>(options.OrderBy(option => option));

		/// <summary>
		/// Creates an empty list.
		/// </summary>
		internal CommandLineOptionList() =>
			options = new List<CommandLineOption>();
		#endregion // Constructors

		#region Private data
		private List<CommandLineOption> options;
		#endregion // Private data

		#region Properties
		/// <summary>
		/// Gets the number of options in this list.
		/// </summary>
		public int Count => options.Count;
		#endregion // Properties

		#region Indexers
		/// <summary>
		/// Gets the option at the specified zero-based index.
		/// </summary>
		/// <param name="index">The index of the option.</param>
		/// <returns>The option at the specified zero-based index.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// <paramref name="index"/> is less than zero or greater than or equal to <see cref="Count"/>.
		/// </exception>
		public CommandLineOption this[int index] =>
			options[index];
		#endregion // Indexers

		#region Public methods
		/// <summary>
		/// Get an enumerator for the options.
		/// </summary>
		/// <returns>An enumerator for the options.</returns>
		public IEnumerator<CommandLineOption> GetEnumerator() =>
			options.GetEnumerator();

		/// <summary>
		/// Try to get the specified option.
		/// </summary>
		/// <param name="nameOrPrefix">The name (or prefix) of the option to find.</param>
		/// <param name="canMatchPrefix">A value indicating if the name can be a prefix.</param>
		/// <returns>The index of the match, if the option was found; otherwise, -1.</returns>
		/// <exception cref="System.ArgumentException">
		/// <paramref name="nameOrPrefix"/> is empty or white space
		/// </exception>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="nameOrPrefix"/> is null
		/// </exception>
		public int IndexOf(string nameOrPrefix, bool canMatchPrefix = false) =>
			IndexOf(nameOrPrefix, out CommandLineOption option, canMatchPrefix);

		/// <summary>
		/// Try to get the specified option.
		/// </summary>
		/// <param name="nameOrPrefix">The name (or prefix) of the option to find.</param>
		/// <param name="option">The resulting option, if found, otherwise, null.</param>
		/// <param name="canMatchPrefix">A value indicating if the name can be a prefix.</param>
		/// <returns>The index of the match, if the option was found; otherwise, -1.</returns>
		/// <exception cref="System.ArgumentException">
		/// <paramref name="nameOrPrefix"/> is empty or white space
		/// </exception>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="nameOrPrefix"/> is null
		/// </exception>
		public int IndexOf(string nameOrPrefix, out CommandLineOption option, bool canMatchPrefix = false)
		{
			if (nameOrPrefix == null)
				throw new ArgumentNullException(nameof(nameOrPrefix));

			if (string.IsNullOrWhiteSpace(nameOrPrefix))
				throw new ArgumentException(null, nameof(nameOrPrefix));

			Func<CommandLineOption, string, int> comparison = canMatchPrefix ?
				(Func<CommandLineOption, string, int>)ComparePrefix : Compare;

			int high = Count - 1;
			int low = 0;

			while (low <= high)
			{
				int mid = (low + high) >> 1;
				CommandLineOption candidate = options[mid];

				int result = comparison(candidate, nameOrPrefix);
				if (result == 0)
				{
					if (!canMatchPrefix || low == mid)
					{
						option = candidate;
						return mid;
					}

					high = mid;
				}
				else
				{
					if (result > 0)
						high = mid - 1;
					else
						low = mid + 1;
				}
			}

			option = null;
			return -1;
		}

		/// <summary>
		/// Try to get the specified option.
		/// </summary>
		/// <param name="name">The name of the option to find.</param>
		/// <param name="option">The resulting option, if found, otherwise, null.</param>
		/// <returns>True, if found; otherwise, false.</returns>
		/// <exception cref="System.ArgumentException">
		/// <paramref name="name"/> is empty or white space
		/// </exception>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="name"/> is null
		/// </exception>
		public bool TryGetValue(string name, out CommandLineOption option) =>
			IndexOf(name, out option) >= 0 ? true : false;
		#endregion // Public methods

		#region Internal methods
		/// <summary>
		/// Adds the specified range of options to this list.
		/// </summary>
		/// <param name="options">The options to add to this list.</param>
		internal void AddRange(IEnumerable<CommandLineOption> options) =>
			this.options.AddRange(options.OrderBy(option => option));

		/// <summary>
		/// Gets the first duplicate value (if any) in this list.
		/// </summary>
		/// <returns>The first duplicate value, if found; otherwise, null.</returns>
		internal CommandLineOption GetFirstDuplicate()
		{
			int count = options.Count - 1;
			for (int index = 0; index < count; index++)
				if (options[index].Name == options[index + 1].Name)
					return options[index];

			return null;
		}

		/// <summary>
		/// Gets the first member (if any) of this list that is also present in the specified list.
		/// </summary>
		/// <param name="other">The other list.</param>
		/// <param name="option">The first common member (or null).</param>
		/// <returns>True, if a common member exists; otherwise, false.</returns>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="other"/> is null.
		/// </exception>
		internal bool GetFirstCommon(CommandLineOptionList other, out CommandLineOption option)
		{
			if (other == null)
				throw new ArgumentNullException(nameof(other));

			option = this.FirstOrDefault(thisOption =>
				other.IndexOf(thisOption.Name) >= 0);

			return option != null;
		}

		/// <summary>
		/// Get a value indicating if this list contains duplicate options.
		/// </summary>
		/// <returns>True, if duplicates are present; otherwise, false.</returns>
		internal bool HasDuplicates() =>
			GetFirstDuplicate() != null;
		#endregion // Internal methods

		#region Explicit interface methods
		IEnumerator IEnumerable.GetEnumerator() =>
			options.GetEnumerator();
		#endregion // Explicit interface methods

		#region Private methods
		private static int Compare(CommandLineOption option, string name) =>
			option.CompareTo(name);

		private static int ComparePrefix(CommandLineOption option, string prefix) =>
			option.CompareToPrefix(prefix);
		#endregion // Private methods
	}
}
