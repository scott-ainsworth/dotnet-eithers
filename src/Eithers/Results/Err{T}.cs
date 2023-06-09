using System;
using System.Collections;
using System.Collections.Generic;

namespace Ainsworth.Monads.Results;

/// <summary>
///   The subclass of <see cref="IResult{T}"/> that represents an exception result.
/// </summary>
/// <typeparam name="T">The type of the value contained by the <see cref="IResult{T}"/>
///   superclass.</typeparam>
public sealed class Err<T> : IResult<T> where T : notnull {

	#region Properties

	/// <inheritdoc/>
	/// <remarks>
	///   <see cref="IsValue"/> is always <see wrapped="false"/> for <see cref="Err{T}"/>
	///   instances.
	/// </remarks>
	public bool IsValue => false;

	/// <inheritdoc/>
	/// <remarks>
	///   <see cref="IsError"/> is always <see wrapped="true"/> for <see cref="Err{T}"/>
	///   instances.
	/// </remarks>
	public bool IsError => true;

	private readonly Exception exception;

	#endregion
	#region Constructors, Casts, and Conversions

	internal Err(Exception ex) {
#if DEBUG
		if (typeof(Exception).IsAssignableFrom(typeof(T))) {
			throw new ArgumentException(
				$"An {nameof(Err<T>)}<T> should never be constructed with T " +
				$"as {nameof(Exception)}. Is a type parameter missing?");
		}
#endif
		// Null check for callers that don't use code analyzers to catch errors
		_ = ex ?? throw new ArgumentNullException(nameof(ex));
		exception = ex;
	}

	/// <summary>
	///   Unwrap this <see cref="Err{T}"/>'s exception.
	/// </summary>
	/// <returns>
	///   The wrapped exception.
	/// </returns>
	public Exception ToException() => exception;

	#endregion
	#region IEquatable<IResult<T>>, IEquatable<T>, and IEquatable Implementations

	/// <summary>
	///   Determines whether the specified value equals this instance's exception value.
	/// </summary>
	/// <param name="other">A <typeparamref name="T"/> to compare with this instance.</param>
	/// <returns>
	///   <see langword="false"/>; a <typeparamref name="T"/> cannot equal
	///   a <see cref="Err{T}"/>.
	/// </returns>
	/// <exception cref="ArgumentNullException">
	///   Thrown when <paramref name="other"/> is <see langword="null"/>.
	/// </exception>
	[System.Diagnostics.CodeAnalysis.SuppressMessage(
		"Design", "CA1065:Do not raise exceptions in unexpected locations",
		Justification = "For this library, Equals(null) is invalid.")]
	public bool Equals(T other) {
		_ = other ?? throw new ArgumentNullException(nameof(other));
		return false;
	}

	/// <summary>
	///   Determines whether the specified exception equals this instance's wrapped exception.
	/// </summary>
	/// <param name="other">An <see cref="exception"/> to compare with this instance.</param>
	/// <returns>
	///   <see langword="true"/> if <paramref name="other"/> equals this instance's wrapped
	///   exception (<c>other == this.ToException()</c>); otherwise <see langword="false"/>.
	/// </returns>
	public bool Equals(Exception other) {
		_ = other ?? throw new ArgumentNullException(nameof(other));
		return other.Equals(exception);
	}

	/// <summary>
	///   Determines whether the specified <see cref="IResult{T}"/> equals the current instance.
	/// </summary>
	/// <param name="other">A <see cref="IResult{T}"/> to compare with this instance.</param>
	/// <returns>
	///   <see langword="true"/> if <paramref name="other"/> is an <see cref="Err{T}"/>
	///   its wrapped value exception this instances wrapped exception
	///   (<c>other.ToException() == this.ToException()</c>).
	/// </returns>
	[System.Diagnostics.CodeAnalysis.SuppressMessage(
		"Design", "CA1065:Do not raise exceptions in unexpected locations",
		Justification = "For this library, Equals(null) is invalid.")]
	public bool Equals(IResult<T> other) {
		_ = other ?? throw new ArgumentNullException(nameof(other));
		return other is Err<T> r && Equals(r);
	}

	/// <summary>
	///   Determines whether the specified <see cref="Err{T}"/> equals the current instance.
	/// </summary>
	/// <param name="other">An <see cref="Err{T}"/> to compare with this instance.</param>
	/// <returns>
	///   <see langword="true"/> if the two instance's wrapped exceptions are equal;
	///   otherwise, <see langword="false"/>.
	/// </returns>
	[System.Diagnostics.CodeAnalysis.SuppressMessage(
		"Design", "CA1065:Do not raise exceptions in unexpected locations",
		Justification = "For this library, Equals(null) is invalid.")]
	public bool Equals(Err<T> other) {
		_ = other ?? throw new ArgumentNullException(nameof(other));
		return Equals(other.exception);
	}

	/// <summary>
	///   Determines whether the specified <see cref="Ok{T}"/> equals the current instance.
	/// </summary>
	/// <param name="other">An <see cref="Ok{T}"/> to compare with this instance.</param>
	/// <returns>
	///   <see langword="false"/>; A <see cref="Ok{T}"/> cannot equal
	///   an <see cref="Err{T}"/>.
	/// </returns>
	[System.Diagnostics.CodeAnalysis.SuppressMessage(
		"Design", "CA1065:Do not raise exceptions in unexpected locations",
		Justification = "For this library, Equals(null) is invalid.")]
	public bool Equals(Ok<T> other) {
		_ = other ?? throw new ArgumentNullException(nameof(other));
		return false;
	}

	/// <summary>
	///   Determines whether the specified <see cref="object"/> equals the current instance.
	/// </summary>
	/// <param name="obj">An <see cref="object"/> to compare with this instance.</param>
	/// <returns>
	///   <list type="bullet">
	///     <item><see langword="true"/> if <paramref name="obj"/> is a <see cref="exception"/>
	///       and it equals this instance's wrapped value
	///       (<c>other == this.ToException()</c>)</item>;
	///     <item><see langword="true"/> if <paramref name="obj"/> is an
	///       <see cref="Err{T}"/> and its wrapped exception equals this instance's
	///       wrapped exception (<c>other.ToException() == this.ToException()</c>);</item>
	///     <item>Otherwise, <see langword="false"/>.</item>
	///   </list>
	/// </returns>
	public override bool Equals(object obj) => obj switch {
		Err<T> v => Equals(v),
		Exception c => c.Equals(exception),
		_ => false
	};

	#endregion
	#region GetHashCode Implementation
	/// <summary>
	///   Computes the hash code for this instance.
	/// </summary>
	/// <returns>
	///   The hash code for this instance.
	/// </returns>
	/// <remarks>
	///   The hash code for a <see cref="Err{T}"/> is the hash code of its wrapped
	///   <see cref="Exception"/>.
	/// </remarks>
	public override int GetHashCode() => exception.GetHashCode();

	#endregion
	#region IEnumerable implementations

	/// <inheritdoc/>
	public IEnumerator<T> GetEnumerator() {
		yield break;
	}

	/// <inheritdoc/>
	public IEnumerator<Exception> GetErrorEnumerator() {
		yield return exception;
	}

	/// <inheritdoc/>
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	#endregion
	#region TryGetValue & TryGetException

	/// <summary>
	///   Gets the value wrapped by this instance.
	/// </summary>
	/// <param name="value">When this method returns, contains the default value for
	///   type <typeparamref name="T"/>.</param>
	/// <returns>
	///   <see langword="false"/>; <see cref="Err{T}"/> never wraps a value.
	/// </returns>
	public bool TryGetValue(out T value) {
		value = default!;
		return false;
	}

	/// <summary>
	///   Gets the exception wrapped by this instance.
	/// </summary>
	/// <param name="ex">When this method returns, contains the exception wrapped by
	///   this instance.</param>
	/// <returns>
	///   <see langword="true"/>.
	/// </returns>
	public bool TryGetException(out Exception ex) {
		ex = exception;
		return true;
	}

	#endregion
}
