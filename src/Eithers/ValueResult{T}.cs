using System;
using System.Collections.Generic;

namespace Ainsworth.Eithers.Results;

/// <summary>
///   The subclass of <see cref="Result{T}"/> that represents a successful result
///   that has a value.
/// </summary>
/// <typeparam name="T">The type of the value contained by the <see cref="Result{T}"/>
///   superclass.</typeparam>
public sealed class ValueResult<T> : Result<T> where T : notnull {

    #region Properties

    /// <inheritdoc/>
    /// <remarks>
    ///   <see cref="IsValue"/> is always <see wrapped="true"/> for <see cref="ValueResult{T}"/>
    ///   instances.
    /// </remarks>
    public override bool IsValue => true;

    private readonly T value;

    internal ValueResult(T value) {
        this.value = value;
    }

    #endregion
    #region Constructors, Casts, and Conversions

    /// <summary>
    ///   Unwrap this <see cref="ErrorResult{T}"/>'s value.
    /// </summary>
    /// <returns>
    ///   The wrapped value.
    /// </returns>
    public T ToValue() => value;

    #endregion
    #region IEquatable<Result<T>>, IEquatable<T>, and IEquatable Implementations

    /// <summary>
    ///   Determines whether the specified value equals this instance's wrapped value.
    /// </summary>
    /// <param name="other">A <typeparamref name="T"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if <paramref name="other"/> equals this instance's wrapped
    ///   value (<c>other == this.ToValue()</c>); otherwise <see langword="false"/>.
    /// </returns>
    public override bool Equals(T other) {
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
    ///   a <see cref="ValueResult{T}"/>.
    /// </returns>
    public override bool Equals(Exception other) {
        // Null check for callers that don't use code analyzers to catch errors
        _ = other ?? throw new ArgumentNullException(nameof(other));
        return false;
    }

    /// <summary>
    ///   Determines whether the specified <see cref="Result{T}"/> equals the current instance.
    /// </summary>
    /// <param name="other">A <see cref="Result{T}"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if <paramref name="other"/> is a <see cref="ValueResult{T}"/>
    ///   its wrapped value equals this instances wrapped value
    ///   (<c>other.ToValue() == this.ToValue()</c>).
    /// </returns>
    public override bool Equals(Result<T> other) {
        // Null check for callers that don't use code analyzers to catch errors
        _ = other ?? throw new ArgumentNullException(nameof(other));
        return other is ValueResult<T> r && Equals(r);
    }

    /// <summary>
    ///   Determines whether the specified <see cref="ErrorResult{T}"/> equals the current instance.
    /// </summary>
    /// <param name="other">An <see cref="ErrorResult{T}"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="false"/>; an <see cref="ErrorResult{T}"/> cannot equal
    ///   a <see cref="ValueResult{T}"/>.
    /// </returns>
    public override bool Equals(ErrorResult<T> other) {
        // Null check for callers that don't use code analyzers to catch errors
        _ = other ?? throw new ArgumentNullException(nameof(other));
        return false;
    }

    /// <summary>
    ///   Determines whether the specified <see cref="Result{T}"/> equals the current instance.
    /// </summary>
    /// <param name="other">An <see cref="ValueResult{T}"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the two instance's wrapped values are equal;
    ///   otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals(ValueResult<T> other) {
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
    ///     <item><see langword="true"/> if <paramref name="obj"/> is a <see cref="ValueResult{T}"/>
    ///       and its wrapped value equals this instance's wrapped value
    ///       (<c>other.ToValue() == this.ToValue()</c>);</item>
    ///     <item>Otherwise, <see langword="false"/>.</item>
    ///   </list>
    /// </returns>
    public override bool Equals(object obj) => obj switch {
        ValueResult<T> r => Equals(r),
        T v => Equals(v),
        _ => false
    };

    #endregion
    #region Object Overrides

    /// <summary>
    ///   Computes the hash code for this instance.
    /// </summary>
    /// <returns>
    ///   The hash code for this instance.
    /// </returns>
    /// <remarks>
    ///   The hash code for a <see cref="ErrorResult{T}"/> is the hash code of its wrapped value.
    /// </remarks>
    public override int GetHashCode() => value.GetHashCode();

    #endregion
    #region IEnumerator<T> Implementation

    /// <inheritdoc/>
    public override IEnumerator<T> GetEnumerator() {
        yield return value;
    }

    /// <inheritdoc/>
    public override IEnumerator<Exception> GetErrorEnumerator() {
        yield break;
    }

    #endregion
    #region TryGetValue

    /// <summary>
    ///   Gets the value wrapped by this instance.
    /// </summary>
    /// <param name="value">When this method returns, contains the value wrapped by this
    ///   instance.</param>
    /// <returns>
    ///   <see langword="true"/>; <see cref="Some{T}"/> always wraps a value.
    /// </returns>
    public override bool TryGetValue(out T value) {
        value = this.value;
        return true;
    }

    #endregion
}
