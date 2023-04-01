using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Ainsworth.Eithers;

#pragma warning disable CS0659  // Covered by SonarLint S1206

/// <summary>
///   The subclass of <see cref="Maybe{T}"/> that represents a Maybe monad
///   that does not value.
/// </summary>
/// <typeparam name="T">The type of the value contained by the  <see cref="Maybe{T}"/>
///   superclass.</typeparam>
[DebuggerDisplay("None")]
[SuppressMessage(
    "Minor Bug", "S1206:\"Equals(Object)\" and \"GetHashCode()\" should be overridden in pairs",
    Justification = "base.GetHasCode() provides correct implementation")]
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
    #region IEquatable<T> and IEquatable<Maybe<T>> Implementations

    /// <inheritdoc/>
    public override bool Equals(T other) => false;

    /// <inheritdoc/>
    public override bool Equals(Maybe<T> other) => other == NoneSingleton;

    /// <summary>
    ///   Returns a value indicating whether this instance's wrapped value is equal to
    ///   the specified object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if <paramref name="obj"/> is the <see cref="None{T}"/>
    ///   singleton; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals(object obj) => obj == NoneSingleton;

    #endregion
    #region IEnumerable<T> Implementation

    /// <inheritdoc/>
    public override IEnumerator<T> GetEnumerator() {
        yield break;
    }

    #endregion
    #region System.Object overrides

    /// <summary>
    ///   Returns a string representation of this <see cref="None{T}"/>.
    /// </summary>
    /// <returns>
    ///  A string that represents this <see cref="None{T}"/>.
    /// </returns>
    public override string ToString() =>
        $"{nameof(Maybe<T>)}<{typeof(T).Name}>.{nameof(None)}";


    #endregion
}
