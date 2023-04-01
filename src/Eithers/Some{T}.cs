using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Ainsworth.Eithers;

/// <summary>
///   The subclass of <see cref="Maybe{T}"/> that represents a Maybe monad that has a value.
/// </summary>
/// <typeparam name="T">The type of the value contained by the  <see cref="Maybe{T}"/>
///   superclass.</typeparam>
[DebuggerDisplay("Some({Value})")]
public class Some<T> : Maybe<T>
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

    /// <summary>
    ///   Defines an implicit conversion of a value of type <typeparamref name="T"/>
    ///   to a <see cref="Some{T}"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    [SuppressMessage(
        "Usage", "CA2225:Operator overloads have named alternates",
        Justification = "ToSome<T> provided in class Maybe")]
    public static implicit operator Some<T>(T value) => new(value);

    #endregion
    #region IEquatable<T> and IEquatable<Maybe<T>> Implementations

    /// <inheritdoc/>
    public override bool Equals(T other) => Value.Equals(other);

    /// <inheritdoc/>
    public override bool Equals(Maybe<T> other) => other is Some<T> s && Value.Equals(s.Value);

    /// <summary>
    ///   Returns a value indicating whether this instance's wrapped value is equal to
    ///   the specified object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if <paramref name="obj"/> is an instance of
    ///   <see cref="Maybe{T}"/> and <paramref name="obj"/>'s wrapped value equals this
    ///   instance's wrapped value, or <see langword="true"/> if <paramref name="obj"/> is an
    ///   instance of <typeparamref name="T"/> and equals this instance's wrapped value;
    ///   otherwise <see langword="false"/>.
    /// </returns>
    public override bool Equals(object obj) => obj switch {
        Maybe<T> other => Equals(other),
        T other => Equals(other),
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
