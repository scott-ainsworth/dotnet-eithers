using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Ainsworth.Eithers;

/// <summary>
///   The subclass of <see cref="Maybe{T}"/> that represents a Maybe monad
///   that does not value.
/// </summary>
/// <typeparam name="T">The type of the value contained by the  <see cref="Maybe{T}"/>
///   superclass.</typeparam>
[DebuggerDisplay("None")]
public sealed class None<T> : Maybe<T>
    where T : notnull {

    #region Properties

    /// <inheritdoc/>
    public override bool HasValue => false;

    /// <summary>
    ///   A singleton representing a <see cref="Maybe{T}"/> with no value.
    /// </summary>
    /// <value>
    ///   The single instance of <see cref="None{T}"/>.
    /// </value>
    internal static readonly None<T> NoneSingleton = new();

    #endregion
    #region Constructors, Casts, and Conversions

    /// <summary>
    ///   Initialize a new <see cref="None{T}"/> instance.
    /// </summary>
    /// <remarks>
    ///   To ensure all <see cref="None{T}"/>s are singleton's, this constructor is private.
    ///   The singletons are created by the <see cref="NoneSingleton"/> field.
    /// </remarks>
    private None() { }

    #endregion
    #region IEquatable<T> and IEquatable<Maybe<T>> Implementations

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
    public override bool Equals(T other) {
        _ = other ?? throw new ArgumentNullException(nameof(other));
        return false;
    }

    /// <summary>
    ///   Determines whether the specified <see cref="Maybe{T}"/> equals the current instance.
    /// </summary>
    /// <param name="other">A <see cref="Maybe{T}"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if <paramref name="other"/> is equal to this instance; otherwise,
    ///   <see langword="false"/>.
    /// </returns>
    public override bool Equals(Maybe<T> other) {
        _ = other ?? throw new ArgumentNullException(nameof(other));
        return other == NoneSingleton;
    }

    /// <summary>
    ///   Determines whether the specified <see cref="None{T}"/> equals the current instance.
    /// </summary>
    /// <param name="other">A <see cref="None{T}"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if this instance and <paramref name="other"/>
    ///   are the same instance.
    /// </returns>
    public override bool Equals(None<T> other) {
        _ = other ?? throw new ArgumentNullException(nameof(other));
        return other == NoneSingleton;
    }

    /// <summary>
    ///   Determines whether the specified <see cref="Some{T}"/> equals the current instance.
    /// </summary>
    /// <param name="other">A <see cref="Some{T}"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="false"/>; a <see cref="None{T}"/> and a <see cref="Some{T}"/> can
    ///   never be equal.
    /// </returns>
    public override bool Equals(Some<T> other) {
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
    public override bool Equals(object obj) => obj == NoneSingleton;

    #endregion
    #region IEnumerable<T> Implementation

    /// <inheritdoc/>
    public override IEnumerator<T> GetEnumerator() {
        yield break;
    }

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
        $"{nameof(Maybe<T>)}<{typeof(T).Name}>.{nameof(None)}";

    #endregion
    #region TryGetValue

    /// <summary>
    ///   Gets the value wrapped by this instance.
    /// </summary>
    /// <param name="value">When this method returns, contains the default value for
    ///   type <typeparamref name="T"/>.</param>
    /// <returns>
    ///   <see langword="false"/>; <see cref="Some{T}"/> never wraps a value.
    /// </returns>
    public override bool TryGetValue(out T value) {
        value = default!;
        return false;
    }

    #endregion
}
