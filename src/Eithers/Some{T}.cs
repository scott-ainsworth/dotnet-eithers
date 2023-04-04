using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Ainsworth.Eithers;

/// <summary>
///   The subclass of <see cref="Maybe{T}"/> that represents a <see cref="Maybe{T}"/>
///   that has a value.
/// </summary>
/// <typeparam name="T">The type of the value contained by the  <see cref="Maybe{T}"/>
///   superclass.</typeparam>
[DebuggerDisplay("Some({Value})")]
public sealed class Some<T> : Maybe<T>, IEquatable<Some<T>>
    where T : notnull {

    #region Properties

    /// <inheritdoc/>
    public override bool HasValue => true;

    /// <summary>
    ///   Gets the value wrapped by this <see cref="Maybe{T}"/> instance.
    /// </summary>
    /// <value>
    ///   The value wrapped by this <see cref="Maybe{T}"/> instance.
    /// </value>
    public T Value { get; private init; }

    #endregion
    #region Constructors, Casts, and Conversions

    /// <summary>
    ///   Initialize a new <see cref="Some{T}"/> using a specified value.
    /// </summary>
    /// <param name="value">The value to wrap.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/>
    ///   is <see langword="null"/>.</exception>
    internal Some(T value) {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    #endregion
    #region IEquatable<T> and IEquatable<Maybe<T>> Implementations

    /// <summary>
    ///   Determines whether the specified value equals this instance's wrapped value.
    /// </summary>
    /// <param name="other">A <typeparamref name="T"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if <paramref name="other"/> equals this instance's wrapped
    ///   value (<c>other == this.Value</c>); otherwise <see langword="false"/>.
    /// </returns>
    public override bool Equals(T other) => Value.Equals(other);

    /// <summary>
    ///   Determines whether the specified <see cref="Maybe{T}"/> equals the current instance.
    /// </summary>
    /// <param name="other">A <see cref="Maybe{T}"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if <paramref name="other"/> is type <see cref="Some{T}"/> and
    ///   <paramref name="other"/>'s wrapped value equals to this instance's wrapped value
    ///   (<c>other.Value == this.Value</c>); otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals(Maybe<T> other) => other is Some<T> s && Equals(s.Value);

    /// <summary>
    ///   Determines whether the specified <see cref="Some{T}"/> equals the current instance.
    /// </summary>
    /// <param name="other">A <see cref="Some{T}"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if  <paramref name="other"/>'s wrapped value equals to this
    ///   instance's wrapped value (<c>other.Value == this.Value</c>); otherwise,
    ///   <see langword="false"/>.
    /// </returns>
    public bool Equals(Some<T> other) => other is not null && Value.Equals(other.Value);

    // /// <inheritdoc/>
    /// <summary>
    ///   Determines whether the specified object equals the current <see cref="Maybe{T}"/>
    ///   instance.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if <paramref name="obj"/> is equal to this instance; otherwise,
    ///   <see langword="false"/>.  <paramref name="obj"/> is equal to this instance if:
    ///   <list type="bullet">
    ///     <item>Both instances are type <see cref="Some{T}"/> and their wrapped values
    ///       are equal (<c>obj.Value == this.Value</c>).</item>
    ///     <item><paramref name="obj"/> is the same type is this instance's wrapped value and
    ///       it equals this instance's wrapped value (<c>obj == this.Value</c>).</item>
    ///   </list>
    /// </returns>
    public override bool Equals(object obj) => obj switch {
        Some<T> r => Equals(r),
        T v => Equals(v),
        _ => false
    };

    #endregion
    #region IEnumerable<T> Implementation

    /// <inheritdoc/>
    public override IEnumerator<T> GetEnumerator() {
        yield return Value;
    }

    #endregion
    #region GetHasCode Implementation

    /// <summary>
    ///   Returns the hash code for this <see cref="Maybe{T}"/>.
    /// </summary>
    /// <returns>
    ///   A 32-bit signed integer hash code.
    /// </returns>
    /// <remarks>
    ///   The hash code for <see cref="Some{T}"/> instances is the wrapped value's hash code.
    ///   The hash code for <see cref="None{T}"/> instances is computed using
    ///   <see cref="object.GetHashCode"/> (i.e., the default hash function).
    /// </remarks>
    public override int GetHashCode() => Value.GetHashCode();

    #endregion
    #region ToString Implementation

    /// <summary>
    ///   Returns a string representation of this <see cref="Some{T}"/>.
    /// </summary>
    /// <returns>
    ///  A string that represents this <see cref="Some{T}"/>.
    /// </returns>
    public override string ToString() =>
        $"{nameof(Some<T>)}<{typeof(T).Name}>({Value})";

    #endregion
}
