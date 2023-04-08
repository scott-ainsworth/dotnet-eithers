using System;
using System.Collections.Generic;

namespace Ainsworth.Monads.Results;

/// <summary>
///   A Result Monad (a container that represents a value or an exception).
/// </summary>
/// <typeparam name="T">The type of the value contained by this <see cref="IResult{T}"/>.</typeparam>
[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Minor Bug", "S1206:'Equals(Object)' and 'GetHashCode()' should be overridden in pairs",
    Justification = "Descendent class overrides are sufficient")]
[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Major Code Smell", "S4035:Classes implementing 'IEquatable<T>' should be sealed",
    Justification = "Constructor is protected and all subclasses are sealed.")]
public interface IResult<T>
    : IEquatable<IResult<T>>, IEquatable<Err<T>>, IEquatable<Ok<T>>,
    IEquatable<T>, IEnumerable<T>
    where T : notnull {

    #region Properties

    /// <summary>
    ///   Indicates whether or not this instance wraps a value.
    /// </summary>
    /// <value>
    ///   <see langword="true"/> if this instance wraps a value; otherwise, <see langword="false"/>
    /// </value>
    public bool IsValue { get; }

    /// <summary>
    ///   Indicates whether or not this instance wraps an exception.
    /// </summary>
    /// <value>
    ///   <see langword="true"/> if this instance wraps and exception;
    ///   otherwise, <see langword="false"/>
    /// </value>
    public bool IsError { get; }

    #endregion
    #region Constructors, Casts, and Conversions

    #endregion
    #region Equals Implementation

    // IEquatable<IResult<T>>, IEquatable<T>, and IEquatable Implementations

    /// <summary>
    ///   Determines whether the specified exception equals this instance's wrapped exception.
    /// </summary>
    /// <param name="other">An <see cref="Exception"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if this instance is an <see cref="Err{T}"/> and
    ///   <paramref name="other"/> equals this instance's wrapped
    ///   exception (<c>other == this.Exception</c>); otherwise <see langword="false"/>.
    /// </returns>
    public bool Equals(Exception other);

    #endregion
    // #region Object Overrides

    // #endregion
    #region IEnumerator<T> Implementation

    /// <summary>
    ///   Returns an enumerator that treats this <see cref="IResult{T}"/> as a collection
    ///   of zero or one <see cref="Exception"/>s.
    /// </summary>
    /// <returns>
    ///   An <see cref="IEnumerator{Exception}"/> instance that can be used to iterate through
    ///   this instance's zero or one <see cref="Exception"/>s.
    /// </returns>
    public IEnumerator<Exception> GetErrorEnumerator();
    #endregion
    #region TryGetValue & TryGetException

    /// <summary>
    ///   Gets the value wrapped by this instance.
    /// </summary>
    /// <param name="value">When this method returns, contains the value wrapped by this
    ///   instance, if this instance has a value; otherwise, contains the default value for
    ///   type <typeparamref name="T"/>.</param>
    /// <returns>
    ///   <see langword="true"/> if this instance wraps a value;
    ///   otherwise, <see langword="false"/>.
    /// </returns>
    public bool TryGetValue(out T value);

    /// <summary>
    ///   Gets the exception wrapped by this instance.
    /// </summary>
    /// <param name="ex">When this method returns, contains the exception wrapped by this
    ///   instance, if this instance has an exception; otherwise, the contains default value
    ///   for <see cref="Exception"/>.</param>
    /// <returns>
    ///   <see langword="true"/> if this instance wraps a value;
    ///   otherwise, <see langword="false"/>.
    /// </returns>
    public bool TryGetException(out Exception ex);

    #endregion
}
