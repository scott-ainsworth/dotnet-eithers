using System;
using System.Collections.Generic;

namespace Ainsworth.Eithers.Results;

/// <summary>
///   The subclass of <see cref="Result{T}"/> that represents an exception result.
/// </summary>
/// <typeparam name="T">The type of the value contained by the <see cref="Result{T}"/>
///   superclass.</typeparam>
public sealed class ErrorResult<T> : Result<T> where T : notnull {

    #region Properties

    /// <inheritdoc/>
    /// <remarks>
    ///   <see cref="IsValue"/> is always <see wrapped="false"/> for <see cref="ErrorResult{T}"/>
    ///   instances.
    /// </remarks>
    public override bool IsValue => false;

    private readonly Exception Exception;

    #endregion
    #region Constructors, Casts, and Conversions

    internal ErrorResult(Exception ex) {
#if DEBUG
        if (typeof(Exception).IsAssignableFrom(typeof(T))) {
            throw new ArgumentException(
                $"An {nameof(ErrorResult<T>)}<T> should never be constructed with T " +
                $"as {nameof(Exception)}. Is a type parameter missing?");
        }
#endif
        // Null check for callers that don't use code analyzers to catch errors
        _ = ex ?? throw new ArgumentNullException(nameof(ex));
        this.Exception = ex;
    }

    /// <summary>
    ///   Unwrap this <see cref="ErrorResult{T}"/>'s exception.
    /// </summary>
    /// <returns>
    ///   The wrapped exception.
    /// </returns>
    public Exception ToException() => Exception;

    #endregion
    #region IEquatable<Result<T>>, IEquatable<T>, and IEquatable Implementations

    /// <summary>
    ///   Determines whether the specified value equals this instance's exception value.
    /// </summary>
    /// <param name="other">A <typeparamref name="T"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="false"/>; a <typeparamref name="T"/> cannot equal
    ///   a <see cref="ErrorResult{T}"/>.
    /// </returns>
    public override bool Equals(T other) {
        _ = other ?? throw new ArgumentNullException(nameof(other));
        return false;
    }

    /// <summary>
    ///   Determines whether the specified exception equals this instance's wrapped exception.
    /// </summary>
    /// <param name="other">An <see cref="Exception"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if <paramref name="other"/> equals this instance's wrapped
    ///   exception (<c>other == this.ToException()</c>); otherwise <see langword="false"/>.
    /// </returns>
    public override bool Equals(Exception other) {
        _ = other ?? throw new ArgumentNullException(nameof(other));
        return other.Equals(this.Exception);
    }

    /// <summary>
    ///   Determines whether the specified <see cref="Result{T}"/> equals the current instance.
    /// </summary>
    /// <param name="other">A <see cref="Result{T}"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if <paramref name="other"/> is an <see cref="ErrorResult{T}"/>
    ///   its wrapped value exception this instances wrapped exception
    ///   (<c>other.ToException() == this.ToException()</c>).
    /// </returns>
    public override bool Equals(Result<T> other) {
        _ = other ?? throw new ArgumentNullException(nameof(other));
        return other is ErrorResult<T> r && Equals(r);
    }

    /// <summary>
    ///   Determines whether the specified <see cref="ErrorResult{T}"/> equals the current instance.
    /// </summary>
    /// <param name="other">An <see cref="ErrorResult{T}"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the two instance's wrapped exceptions are equal;
    ///   otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals(ErrorResult<T> other) {
        _ = other ?? throw new ArgumentNullException(nameof(other));
        return Equals(other.Exception);
    }

    /// <summary>
    ///   Determines whether the specified <see cref="ValueResult{T}"/> equals the current instance.
    /// </summary>
    /// <param name="other">An <see cref="ValueResult{T}"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="false"/>; A <see cref="ValueResult{T}"/> cannot equal
    ///   an <see cref="ErrorResult{T}"/>.
    /// </returns>
    public override bool Equals(ValueResult<T> other) {
        _ = other ?? throw new ArgumentNullException(nameof(other));
        return false;
    }

    /// <summary>
    ///   Determines whether the specified <see cref="object"/> equals the current instance.
    /// </summary>
    /// <param name="obj">An <see cref="object"/> to compare with this instance.</param>
    /// <returns>
    ///   <list type="bullet">
    ///     <item><see langword="true"/> if <paramref name="obj"/> is a <see cref="Exception"/>
    ///       and it equals this instance's wrapped value
    ///       (<c>other == this.ToException()</c>)</item>;
    ///     <item><see langword="true"/> if <paramref name="obj"/> is an
    ///       <see cref="ErrorResult{T}"/> and its wrapped exception equals this instance's
    ///       wrapped exception (<c>other.ToException() == this.ToException()</c>);</item>
    ///     <item>Otherwise, <see langword="false"/>.</item>
    ///   </list>
    /// </returns>
    public override bool Equals(object obj) => obj switch {
        ErrorResult<T> v => Equals(v),
        Exception c => c.Equals(Exception),
        _ => false
    };

    #endregion
    #region IEquatable<Result<T>>, IEquatable<T>, and IEquatable Implementations
    #region Object Overrides

    /// <summary>
    ///   Computes the hash code for this instance.
    /// </summary>
    /// <returns>
    ///   The hash code for this instance.
    /// </returns>
    /// <remarks>
    ///   The hash code for a <see cref="ErrorResult{T}"/> is the hash code of its wrapped
    ///   <see cref="System.Exception"/>.
    /// </remarks>
    public override int GetHashCode() => Exception.GetHashCode();

    #endregion

    /// <inheritdoc/>
    public override IEnumerator<T> GetEnumerator() {
        yield break;
    }

    /// <inheritdoc/>
    public override IEnumerator<Exception> GetErrorEnumerator() {
        yield return Exception;
    }

    #endregion
}
