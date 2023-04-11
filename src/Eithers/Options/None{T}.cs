using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Ainsworth.Eithers;

/// <summary>
///   A realization of <see cref="IOption{T}"/> that represents a Option that does not value.
/// </summary>
/// <typeparam name="T">The type of the value contained by the this option.</typeparam>
[DebuggerDisplay("None")]
public sealed class None<T> : IOption<T> where T : notnull {

	#region Properties

	/// <inheritdoc/>
	public bool HasValue => false;

	/// <summary>
	///   A singleton representing a <see cref="IOption{T}"/> with no value.
	/// </summary>
	/// <value>
	///   The single instance of <see cref="None{T}"/>.
	/// </value>
	internal static readonly None<T> singleton = new();

	#endregion
	#region Constructors, Casts, and Conversions

	/// <summary>
	///   Initialize a new <see cref="None{T}"/> instance.
	/// </summary>
	/// <remarks>
	///   To ensure all <see cref="None{T}"/>s are singleton's, this constructor is private.
	///   The singletons are created by the <see cref="singleton"/> field.
	/// </remarks>
	private None() { }

	#endregion
	#region IEquatable<T> and IEquatable<IOption<T>> Implementations

	/// <summary>
	///   Determines whether the specified value equals this instance's wrapped value.
	/// </summary>
	/// <param name="other">A <typeparamref name="T"/> to compare with this instance.</param>
	/// <returns>
	///   <see langword="true"/> (a value of type <typeparamref name="T"/> cannot equal
	///   a <see cref="None{T}"/>.
	/// </returns>
	/// <remarks>
	///   Note: Since <see cref="None{T}"/> cannot have a value, this overload always returns false. 
	/// </remarks>
	[System.Diagnostics.CodeAnalysis.SuppressMessage(
		"Design", "CA1065:Do not raise exceptions in unexpected locations",
		Justification = "For this library, Equals(null) is invalid.")]
	public bool Equals(T other) {
		// Null check for callers that don't use code analyzers to catch errors
		_ = other ?? throw new ArgumentNullException(nameof(other));
		return false;
	}

	/// <summary>
	///   Determines whether the specified <see cref="IOption{T}"/> equals the current instance.
	/// </summary>
	/// <param name="other">A <see cref="IOption{T}"/> to compare with this instance.</param>
	/// <returns>
	///   <see langword="true"/> if <paramref name="other"/> is equal to this instance; otherwise,
	///   <see langword="false"/>.
	/// </returns>
	[System.Diagnostics.CodeAnalysis.SuppressMessage(
		"Design", "CA1065:Do not raise exceptions in unexpected locations",
		Justification = "For this library, Equals(null) is invalid.")]
	public bool Equals(IOption<T> other) {
		// Null check for callers that don't use code analyzers to catch errors
		_ = other ?? throw new ArgumentNullException(nameof(other));
		return other == singleton;
	}

	/// <summary>
	///   Determines whether the specified <see cref="None{T}"/> equals the current instance.
	/// </summary>
	/// <param name="other">A <see cref="None{T}"/> to compare with this instance.</param>
	/// <returns>
	///   <see langword="true"/> if this instance and <paramref name="other"/>
	///   are the same instance.
	/// </returns>
	[System.Diagnostics.CodeAnalysis.SuppressMessage(
		"Design", "CA1065:Do not raise exceptions in unexpected locations",
		Justification = "For this library, Equals(null) is invalid.")]
	public bool Equals(None<T> other) {
		// Null check for callers that don't use code analyzers to catch errors
		_ = other ?? throw new ArgumentNullException(nameof(other));
		return other == singleton;
	}

	/// <summary>
	///   Determines whether the specified <see cref="None{T}"/> equals the current instance.
	/// </summary>
	/// <param name="other">A <see cref="None{T}"/> to compare with this instance.</param>
	/// <returns>
	///   <see langword="false"/>; a <see cref="None{T}"/> and a <see cref="None{T}"/> can
	///   never be equal.
	/// </returns>
	[System.Diagnostics.CodeAnalysis.SuppressMessage(
		"Design", "CA1065:Do not raise exceptions in unexpected locations",
		Justification = "For this library, Equals(null) is invalid.")]
	public bool Equals(Some<T> other) {
		// Null check for callers that don't use code analyzers to catch errors
		_ = other ?? throw new ArgumentNullException(nameof(other));
		return false;
	}

	/// <summary>
	///   Determines whether the specified object equals the current <see cref="None{T}"/>
	///   instance.
	/// </summary>
	/// <param name="obj">An object to compare with this instance.</param>
	/// <returns>
	///   <see langword="true"/> if <paramref name="obj"/> is equal to this instance; otherwise,
	///   <see langword="false"/>.
	/// </returns>
	public override bool Equals(object obj) => obj == singleton;

	#endregion
	#region IEnumerable<T> Implementation

	/// <inheritdoc/>
	public IEnumerator<T> GetEnumerator() {
		yield break;
	}

	/// <summary>
	///   Returns an enumerator that threats this <see cref="IOption{T}"/> as a collection
	///   of zero or one values.
	/// </summary>
	/// <returns>
	///   An <see cref="IEnumerator"/> instance that can be used to iterate through this
	///   instance's zero or one values.
	/// </returns>
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	#endregion
	#region GetHasCode Implementation

	/// <summary>
	///   Returns the hash code for this <see cref="None{T}"/>.
	/// </summary>
	/// <returns>
	///   A 32-bit signed integer hash code.
	/// </returns>
	/// <remarks>
	///   The hash code is computed using <see cref="RuntimeHelpers.GetHashCode(object)"/>.
	/// </remarks>
	public override int GetHashCode() => RuntimeHelpers.GetHashCode(this);

	#endregion
	#region ToString Implementation

	/// <summary>
	///   Returns a string representation of this <see cref="None{T}"/>.
	/// </summary>
	/// <returns>
	///  A string that represents this <see cref="None{T}"/>.
	/// </returns>
	public override string ToString() =>
		$"{nameof(None<T>)}<{typeof(T).Name}>.{nameof(None<T>.singleton)}";

	#endregion
	#region TryGetValue

	/// <summary>
	///   Gets the value wrapped by this instance.
	/// </summary>
	/// <param name="value">When this method returns, contains the default value for
	///   type <typeparamref name="T"/>.</param>
	/// <returns>
	///   <see langword="false"/>; <see cref="None{T}"/> never wraps a value.
	/// </returns>
	public bool TryGetValue(out T value) {
		value = default!;
		return false;
	}

	#endregion
}
