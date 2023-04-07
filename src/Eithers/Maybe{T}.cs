using System;
using System.Collections;
using System.Collections.Generic;

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
[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Major Code Smell", "S4035:Classes implementing 'IEquatable<T>' should be sealed",
    Justification = "Constructor is protected and all subclasses are sealed.")]
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
    ///   Initialize a new <see cref="Maybe{T}"/> instance.
    /// </summary>
    protected Maybe() { }

    #endregion
    #region IEquatable<Maybe<T>>, IEquatable<T>, and IEquatable Implementations

    /// <summary>
    ///   Determines whether the specified value equals this instance's wrapped value.
    /// </summary>
    /// <param name="other">A <typeparamref name="T"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if this instance is a <see cref="Some{T}"/> and
    ///   <paramref name="other"/> equals this instance's wrapped
    ///   value (<c>other == this.Value</c>); otherwise <see langword="false"/>.
    /// </returns>
    public abstract bool Equals(T other);

    /// <summary>
    ///   Determines whether the specified <see cref="Maybe{T}"/> equals the current instance.
    /// </summary>
    /// <param name="other">A <see cref="Maybe{T}"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if <paramref name="other"/> is equal to this instance; otherwise,
    ///   <see langword="false"/>.  <paramref name="other"/> is equal to this instance if:
    ///   <list type="bullet">
    ///     <item>Both instances are type <see cref="None{T}"/> and are the same instance
    ///       (<c>other == this</c>).</item>
    ///     <item>Both instances are type <see cref="Some{T}"/> and their wrapped values
    ///       are equal (<c>other.Value == this.Value</c>).</item>
    ///   </list>
    /// </returns>
    public abstract bool Equals(Maybe<T> other);

    /// <summary>
    ///   Determines whether the specified <see cref="None{T}"/> equals the current instance.
    /// </summary>
    /// <param name="other">A <see cref="None{T}"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if this instance is a <see cref="None{T}"/> and
    ///   <paramref name="other"/> is the same instance.
    /// </returns>
    public abstract bool Equals(None<T> other);

    /// <summary>
    ///   Determines whether the specified <see cref="Some{T}"/> equals the current instance.
    /// </summary>
    /// <param name="other">A <see cref="Some{T}"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if this instance is a <see cref="Some{T}"/> and the
    ///   <paramref name="other"/>'s wrapped value equals to this instance's wrapped value
    ///   (<c>other.Value == this.Value</c>); otherwise, <see langword="false"/>.
    /// </returns>
    public abstract bool Equals(Some<T> other);

    /// <summary>
    ///   Determines whether the specified object equals the current <see cref="Maybe{T}"/>
    ///   instance.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if <paramref name="obj"/> is equal to this instance; otherwise,
    ///   <see langword="false"/>.  <paramref name="obj"/> is equal to this instance if:
    ///   <list type="bullet">
    ///     <item>Both instances are type <see cref="None{T}"/> and are the same instance
    ///       (<c>obj == this</c>).</item>
    ///     <item>Both instances are type <see cref="Some{T}"/> and their wrapped values
    ///       are equal (<c>obj.Value == this.Value</c>).</item>
    ///     <item><paramref name="obj"/> is the same type is this instance's wrapped value and
    ///       it equals this instance's wrapped value (<c>obj == this.Value</c>).</item>
    ///   </list>
    /// </returns>
    /// <seealso cref="None{T}.Equals(object)"/>
    /// <seealso cref="Some{T}.Equals(object)"/>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Design", "CA1065:Do not raise exceptions in unexpected locations",
        Justification = "Descendent classed must override!")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage()]
    // Justification = "This Equals method is not called by current code.
    // It exists solely to catch errant future subclasses.
    public override bool Equals(object obj) => throw new NotImplementedException(
        $"{typeof(Maybe<T>)}.{nameof(object.Equals)} must be overridden in descendent classes");

    #endregion
    #region Object Overrides

    /// <summary>
    ///   Computer the hash code for this instance.
    /// </summary>
    /// <returns>
    ///   A hash code for the this instance.
    /// </returns>
    /// <exception cref="NotImplementedException">Thrown when <see cref="GetHashCode"/>
    ///   has not been implemented in a descendent class and
    ///   <see cref="GetHashCode"/> is called.</exception>
    /// <remarks>
    ///   <strong>Warning</strong>: GetHashCodemust be implemented in every descendent class.
    ///   If not implemented, this default implementation throws a
    ///   <see cref="NotImplementedException"/>.
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Design", "CA1065:Do not raise exceptions in unexpected locations",
        Justification = "Descendent classed must override!")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage()]
    // Justification = "This Equals method is not called by current code.
    // It exists solely to catch errant future subclasses.
    public override int GetHashCode() => throw new NotImplementedException(
        $"{typeof(Maybe<T>)}.{nameof(object.GetHashCode)} must be overridden in descendent classes");

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
}
