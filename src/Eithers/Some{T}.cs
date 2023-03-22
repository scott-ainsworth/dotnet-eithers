#nullable enable

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
    internal Some(T value) {
        Value = value;
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

    // Note: IEquatable implementation inherited from Maybe<T>

    /// <inheritdoc/>
    public override bool Equals(T other) => Value.Equals(other);

    /// <inheritdoc/>
    public override bool Equals(Maybe<T> other) => other is Some<T> s && Value.Equals(s.Value);

    #endregion
    #region IEnumerable<T> Implementation

    /// <inheritdoc/>
    public override IEnumerator<T> GetEnumerator() {
        yield return Value;
    }

    #endregion
    #region GetHasCode Implementation

    /// <inheritdoc/>
    public override int GetHashCode() => Value.GetHashCode();

    #endregion
}
