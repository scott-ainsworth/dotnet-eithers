#nullable enable

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Ainsworth.Eithers;

/// <summary>
///   The subclass of <see cref="Maybe{T}"/> that represents a Maybe monad
///   that does not value.
/// </summary>
/// <typeparam name="T">The type of the value contained by the  <see cref="Maybe{T}"/>
///   superclass.</typeparam>
[DebuggerDisplay("None")]
public class None<T> : Maybe<T> 
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
    #region IEquatable<T> Implementation

    // Note: IEquatable implementation inherited from Maybe<T>

    /// <inheritdoc/>
    public override bool Equals(T other) => false;

    /// <inheritdoc/>
    public override bool Equals(Maybe<T> other) => other == Maybe<T>.None;

    #endregion
    #region IEnumerable<T> Implementation

    /// <inheritdoc/>
    public override IEnumerator<T> GetEnumerator() {
        yield break;
    }

    #endregion
    #region System.Object overrides

    /// <inheritdoc/>
    public override int GetHashCode() => base.GetHashCode();

    #endregion
}
