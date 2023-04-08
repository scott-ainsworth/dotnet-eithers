using System;
using System.Collections;
using System.Collections.Generic;

namespace Ainsworth.Monads.Results;

/// <summary>
///   The subclass of <see cref="IResult{T}"/> that represents a successful result
///   that has a value.
/// </summary>
/// <typeparam name="T">The type of the value contained by the <see cref="IResult{T}"/>
///   superclass.</typeparam>
public sealed class Ok<T> : IResult<T> where T : notnull {

    #region Properties

    /// <inheritdoc/>
    /// <remarks>
    ///   <see cref="IsValue"/> is always <see wrapped="true"/> for <see cref="Ok{T}"/>
    ///   instances.
    /// </remarks>
    public bool IsValue => true;

    /// <inheritdoc/>
    /// <remarks>
    ///   <see cref="IsError"/> is always <see wrapped="false"/> for <see cref="Ok{T}"/>
    ///   instances.
    /// </remarks>
    public bool IsError => false;

    private readonly T value;

    internal Ok(T value) {
        this.value = value;
    }

    #endregion
    #region Constructors, Casts, and Conversions

    /// <summary>
    ///   Unwrap this <see cref="Err{T}"/>'s value.
    /// </summary>
    /// <returns>
    ///   The wrapped value.
    /// </returns>
    public T ToValue() => value;

    #endregion
    #region IEquatable<IResult<T>>, IEquatable<T>, and IEquatable Implementations

    /// <summary>
    ///   Determines whether the specified value equals this instance's wrapped value.
    /// </summary>
    /// <param name="other">A <typeparamref name="T"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if <paramref name="other"/> equals this instance's wrapped
    ///   value (<c>other == this.ToValue()</c>); otherwise <see langword="false"/>.
    /// </returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Design", "CA1065:Do not raise exceptions in unexpected locations",
        Justification = "For this library, Equals(null) is invalid.")]
    public bool Equals(T other) {
        // Null check for callers that don't use code analyzers to catch errors
        _ = other ?? throw new ArgumentNullException(nameof(other));
        return other.Equals(value);
    }

    /// <summary>
    ///   Determines whether the specified exception equals this instance's wrapped exception.
    /// </summary>
    /// <param name="other">An <see cref="Exception"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="false"/>; an <see cref="Exception"/> cannot equal
    ///   a <see cref="Ok{T}"/>.
    /// </returns>
    public bool Equals(Exception other) {
        // Null check for callers that don't use code analyzers to catch errors
        _ = other ?? throw new ArgumentNullException(nameof(other));
        return false;
    }

    /// <summary>
    ///   Determines whether the specified <see cref="IResult{T}"/> equals the current instance.
    /// </summary>
    /// <param name="other">A <see cref="IResult{T}"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if <paramref name="other"/> is a <see cref="Ok{T}"/>
    ///   its wrapped value equals this instances wrapped value
    ///   (<c>other.ToValue() == this.ToValue()</c>).
    /// </returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Design", "CA1065:Do not raise exceptions in unexpected locations",
        Justification = "For this library, Equals(null) is invalid.")]
    public bool Equals(IResult<T> other) {
        // Null check for callers that don't use code analyzers to catch errors
        _ = other ?? throw new ArgumentNullException(nameof(other));
        return other is Ok<T> r && Equals(r);
    }

    /// <summary>
    ///   Determines whether the specified <see cref="Err{T}"/> equals the current instance.
    /// </summary>
    /// <param name="other">An <see cref="Err{T}"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="false"/>; an <see cref="Err{T}"/> cannot equal
    ///   a <see cref="Ok{T}"/>.
    /// </returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Design", "CA1065:Do not raise exceptions in unexpected locations",
        Justification = "For this library, Equals(null) is invalid.")]
    public bool Equals(Err<T> other) {
        // Null check for callers that don't use code analyzers to catch errors
        _ = other ?? throw new ArgumentNullException(nameof(other));
        return false;
    }

    /// <summary>
    ///   Determines whether the specified <see cref="IResult{T}"/> equals the current instance.
    /// </summary>
    /// <param name="other">An <see cref="Ok{T}"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the two instance's wrapped values are equal;
    ///   otherwise, <see langword="false"/>.
    /// </returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Design", "CA1065:Do not raise exceptions in unexpected locations",
        Justification = "For this library, Equals(null) is invalid.")]
    public bool Equals(Ok<T> other) {
        // Null check for callers that don't use code analyzers to catch errors
        _ = other ?? throw new ArgumentNullException(nameof(other));
        return Equals(other.value);
    }

    /// <summary>
    ///   Determines whether the specified <see cref="object"/> equals the current instance.
    /// </summary>
    /// <param name="obj">An <see cref="object"/> to compare with this instance.</param>
    /// <returns>
    ///   <list type="bullet">
    ///     <item><see langword="true"/> if <paramref name="obj"/> is a <typeparamref name="T"/>
    ///       and it equals this instance's wrapped value (<c>other == this.ToValue()</c>)</item>;
    ///     <item><see langword="true"/> if <paramref name="obj"/> is a <see cref="Ok{T}"/>
    ///       and its wrapped value equals this instance's wrapped value
    ///       (<c>other.ToValue() == this.ToValue()</c>);</item>
    ///     <item>Otherwise, <see langword="false"/>.</item>
    ///   </list>
    /// </returns>
    public override bool Equals(object obj) => obj switch {
        Ok<T> r => Equals(r),
        T v => Equals(v),
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
    ///   The hash code for a <see cref="Err{T}"/> is the hash code of its wrapped value.
    /// </remarks>
    public override int GetHashCode() => value.GetHashCode();

    #endregion
    #region IEnumerable Implementations

    /// <inheritdoc/>
    /// <summary>
    ///   Returns an enumerator that threats this <see cref="IResult{T}"/> as a collection
    ///   of zero or one values.
    /// </summary>
    /// <returns>
    ///   An <see cref="IEnumerator{T}"/> instance that can be used to iterate through
    ///   this instance's zero or one <see cref="Exception"/>s.
    /// </returns>
    public IEnumerator<T> GetEnumerator() {
        yield return value;
    }

    /// <inheritdoc/>
    public IEnumerator<Exception> GetErrorEnumerator() {
        yield break;
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion
    #region TryGetValue & TryGetException

    /// <summary>
    ///   Gets the value wrapped by this instance.
    /// </summary>
    /// <param name="value">When this method returns, contains the value wrapped by this
    ///   instance.</param>
    /// <returns>
    ///   <see langword="true"/>; <see cref="Ok{T}"/> always wraps a value.
    /// </returns>
    public bool TryGetValue(out T value) {
        value = this.value;
        return true;
    }

    /// <summary>
    ///   Gets the exception wrapped by this instance.
    /// </summary>
    /// <param name="ex">When this method returns, contains the default value for
    ///   type <see cref="Exception"/>.</param>
    /// <returns>
    ///   <see langword="false"/>.
    /// </returns>
    public bool TryGetException(out Exception ex) {
        ex = default!;
        return false;
    }

    #endregion
}
