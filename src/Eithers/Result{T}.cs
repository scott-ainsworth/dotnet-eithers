using System;
using System.Collections;
using System.Collections.Generic;

namespace Ainsworth.Eithers.Results;

/// <summary>
///   A Result Monad (a container that represents a value or an exception).
/// </summary>
/// <typeparam name="T">The type of the value contained by this <see cref="Result{T}"/>.</typeparam>
[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Minor Bug", "S1206:'Equals(Object)' and 'GetHashCode()' should be overridden in pairs",
    Justification = "Descendent class overrides are sufficient")]
[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Major Code Smell", "S4035:Classes implementing 'IEquatable<T>' should be sealed",
    Justification = "Constructor is protected and all subclasses are sealed.")]
public abstract class Result<T>
    : IEquatable<Result<T>>, IEquatable<ErrorResult<T>>, IEquatable<ValueResult<T>>,
    IEquatable<T>, IEnumerable<T>
    where T : notnull {

    #region Properties

    /// <summary>
    ///   Indicates whether or not this instance wraps a value.
    /// </summary>
    /// <value>
    ///   <see langword="true"/> if this instance wraps a value; otherwise, <see langword="false"/>
    /// </value>
    public abstract bool IsValue { get; }

    /// <summary>
    ///   Indicates whether or not this instance wraps an exception.
    /// </summary>
    /// <value>
    ///   <see langword="true"/> if this instance wraps and exception;
    ///   otherwise, <see langword="false"/>
    /// </value>
    public bool IsError => !IsValue;

    #endregion
    #region Constructors, Casts, and Conversions

    /// <summary>
    ///   Initialize a new <see cref="Result{T}"/>.
    /// </summary>
    protected Result() { }

    #endregion
    #region Equals Implementation

    // IEquatable<Result<T>>, IEquatable<T>, and IEquatable Implementations

    /// <summary>
    ///   Determines whether the specified value equals this instance's wrapped value.
    /// </summary>
    /// <param name="other">A <typeparamref name="T"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if this instance is a <see cref="ValueResult{T}"/> and
    ///   <paramref name="other"/> equals this instance's wrapped
    ///   value (<c>other == this.Value</c>); otherwise <see langword="false"/>.
    /// </returns>
    public abstract bool Equals(T other);

    /// <summary>
    ///   Determines whether the specified exception equals this instance's wrapped exception.
    /// </summary>
    /// <param name="other">An <see cref="Exception"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if this instance is an <see cref="ErrorResult{T}"/> and
    ///   <paramref name="other"/> equals this instance's wrapped
    ///   exception (<c>other == this.Exception</c>); otherwise <see langword="false"/>.
    /// </returns>
    public abstract bool Equals(Exception other);

    /// <summary>
    ///   Determines whether the specified <see cref="Result{T}"/> equals the current instance.
    /// </summary>
    /// <param name="other">A <see cref="Result{T}"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if <paramref name="other"/> is equal to this instance; otherwise,
    ///   <see langword="false"/>.  <paramref name="other"/> is equal to this instance if:
    ///   <list type="bullet">
    ///     <item>Both instances are type <see cref="ErrorResult{T}"/> and their wrapped exceptions
    ///       are equal (<c>other.Exception == this.Exception</c>).</item>
    ///     <item>Both instances are type <see cref="ValueResult{T}"/> and their wrapped values
    ///       are equal (<c>other.Value == this.Value</c>).</item>
    ///   </list>
    /// </returns>
    public abstract bool Equals(Result<T> other);

    /// <summary>
    ///   Determines whether the specified <see cref="Result{T}"/> equals the current instance.
    /// </summary>
    /// <param name="other">An <see cref="ErrorResult{T}"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if this instance is an <see cref="ErrorResult{T}"/> and
    ///   the two instance's wrapped exceptions are equal; otherwise, <see langword="false"/>.
    /// </returns>
    public abstract bool Equals(ErrorResult<T> other);

    /// <summary>
    ///   Determines whether the specified <see cref="Result{T}"/> equals the current instance.
    /// </summary>
    /// <param name="other">An <see cref="ValueResult{T}"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if this instance is an <see cref="ErrorResult{T}"/> and
    ///   the two instance's wrapped values are equal; otherwise, <see langword="false"/>.
    /// </returns>
    public abstract bool Equals(ValueResult<T> other);

    /// <summary>
    ///   Determines whether the specified <see cref="object"/> equals the current instance.
    /// </summary>
    /// <param name="obj">An <see cref="object"/> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if <paramref name="obj"/> is equal to this instance; otherwise,
    ///   <see langword="false"/>.  <paramref name="obj"/> is equal to this instance if:
    ///   <list type="bullet">
    ///     <item>Both instances are type <see cref="ErrorResult{T}"/> and their wrapped exceptions
    ///       are equal (<c>other.Exception == this.Exception</c>).</item>
    ///     <item>Both instances are type <see cref="ValueResult{T}"/> and their wrapped values
    ///       are equal (<c>other.Value == this.Value</c>).</item>
    ///   </list>
    /// </returns>
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
    ///   Computes the hash code for this instance.
    /// </summary>
    /// <returns>
    ///   The hash code for this instance.
    /// </returns>
    /// <exception cref="NotImplementedException">Thrown when <see cref="GetHashCode"/>
    ///   has not been implemented in a descendent class and
    ///   <see cref="Result{T}.GetHashCode"/> is called as a result.</exception>
    /// <remarks>
    ///   <strong>Warning</strong>: GetHashCode must be implemented in every descendent class.
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
        $"{typeof(Result<T>)}.{nameof(object.GetHashCode)} must be overridden in descendent classes");

    #endregion
    #region IEnumerator<T> Implementation

    /// <summary>
    ///   Returns an enumerator that threats this <see cref="Result{T}"/> as a collection
    ///   of zero or one values.
    /// </summary>
    /// <returns>
    ///   An <see cref="IEnumerator{T}"/> instance that can be used to iterate through
    ///   this instance's zero or one <see cref="Exception"/>s.
    /// </returns>
    public abstract IEnumerator<T> GetEnumerator();

    /// <summary>
    ///   Returns an enumerator that treats this <see cref="Result{T}"/> as a collection
    ///   of zero or more <see cref="Exception"/>s.
    /// </summary>
    /// <returns>
    ///   An <see cref="IEnumerator{Exception}"/> instance that can be used to iterate through
    ///   this instance's zero or one <see cref="Exception"/>s.
    /// </returns>
    public abstract IEnumerator<Exception> GetErrorEnumerator();

    /// <summary>
    ///   Returns an enumerator that threats this <see cref="Result{T}"/> as a collection
    ///   of zero or one values.
    /// </summary>
    /// <returns>
    ///   An <see cref="IEnumerator"/> instance that can be used to iterate through this
    ///   instance's zero or one values.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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
    public abstract bool TryGetValue(out T value);

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
    public abstract bool TryGetException(out Exception ex);

    #endregion
}
