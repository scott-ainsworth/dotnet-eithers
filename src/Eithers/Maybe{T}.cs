#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Ainsworth.Eithers;

/// <summary>
///   A Maybe Monad (a container that represents a value that might or might not exist).
/// </summary>
/// <typeparam name="T">The type of the value contained by this
///   <see cref="Maybe{T}"/>.</typeparam>
/// <remarks>
///   *References*:
///   <list type="number">
///     <item><a href="https://en.wikipedia.org/wiki/Monad_(functional_programming)">
///       Wikipedia: Monad (functional programming)</a></item>
///     <item><a href="https://www.dotnetcurry.com/patterns-practices/1510/maybe-monad-csharp">
///       .NET Curry: The Maybe Monad (C#)</a></item>
///   </list>
/// </remarks>
public abstract class Maybe<T> : IEquatable<Maybe<T>>, IEquatable<T>, IEnumerable<T>
    where T : notnull {

    #region Properties

    /// <summary>
    ///   Determine if a <see cref="Maybe{T}"/> wraps a value.
    /// </summary>
    /// <value>
    ///   <see langword="true"/> if this instance wraps a value;
    ///   otherwise <see langword="true"/>.
    /// </value>
    public abstract bool HasValue { get; }

    /// <summary>
    ///   A singleton representing a <see cref="Maybe{T}"/> with no value.
    /// </summary>
    /// <value>
    ///   The single instance of <see cref="None{T}"/>.
    /// </value>
    public static readonly Maybe<T> None = None<T>.NoneSingleton;

    #endregion
    #region Constructors, Casts, and Conversions

    /// <summary>
    ///   Defines an implicit conversion of a possibly-<see langword="null"/> value of
    ///   type <typeparamref name="T"/> to a <see cref="Maybe{T}"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    [SuppressMessage(
        "Usage", "CA2225:Operator overloads have named alternates",
        Justification = "ToMaybe<T> provided in class Maybe")]
    public static implicit operator Maybe<T>(T? value) =>
        value is T v
            ? new Some<T>(v)
            : Maybe<T>.None;

    #endregion
    #region IEquateable<Maybe<T>>, IEquatable<T>, and IEquatable Implementions

    // WARNING: DO NOT implement Equals(T) and Equals(Maybe<T>) using subtypes
    // [e.g., bool Equals(Some<T>)] here to avoid breaking future subclasses of Maybe<T>.

    /// <summary>
    ///   Returns a value indicating whether this instance's wrapped value is equal to
    ///   the specified value.
    /// </summary>
    /// <param name="other">A <typeparamref name="T"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if <paramref name="other"/> equals this instance's wrapped
    ///   value; otherwise <see langword="false"/>.
    /// </returns>
    public abstract bool Equals(T other);

    /// <summary>
    ///   Returns a value indicating whether this instance's wrapped value is equal to
    ///   the specified <see cref="Maybe{T}"/>'s wrapped value.
    /// </summary>
    /// <param name="other">A <see cref="Maybe{T}"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if <paramref name="other"/>'s s wrapped value equals this
    ///   instance's wrapped value; otherwise <see langword="false"/>.
    /// </returns>
    public abstract bool Equals(Maybe<T> other);

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
    public override bool Equals(object? obj) => obj switch {
        Maybe<T> other => Equals(other),
        T other => Equals(other),
        _ => false
    };

    #endregion
    #region IEnumerator<T> Implementation

    /// <summary>
    ///   Returns a typed enumerator that threats this <see cref="Maybe{T}"/> as a collection
    ///   of zero or one values of type <typeparamref name="T"/>.
    /// </summary>
    /// <returns>
    ///   An <see cref="IEnumerator{T}"/> instance that can be used to iterate through this
    ///   instance's zero or one values.
    /// </returns>
    public abstract IEnumerator<T> GetEnumerator();

    /// <summary>
    ///   Returns an enumerator that threats this <see cref="Maybe{T}"/> as a collection
    ///   of zero or one values.
    /// </summary>
    /// <returns>
    ///   An <see cref="IEnumerator"/> instance that can be used to iterate through this
    ///   instance's zero or one values.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion
    #region GetHashCode overrides

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
    public override int GetHashCode() =>
        this is Some<T> some ? some.GetHashCode() : base.GetHashCode();

    #endregion
}
