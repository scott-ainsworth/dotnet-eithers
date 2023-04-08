using System;

namespace Ainsworth.Monads.Results;

/// <summary>
///   Methods to create <see cref="IResult{T}"/>s.
/// </summary>
public static class Result {

    #region From<T> Method

    /// <summary>
    ///   Create a new <see cref="IResult{T}"/> from a specified value.
    /// </summary>
    /// <typeparam name="T">The type of the value wrapped by the returned
    ///   <see cref="IResult{T}"/>.</typeparam>
    /// <param name="value">the value to wrap.</param>
    /// <returns>
    ///   A <see cref="Ok{T}"/> wrapping <paramref name="value"/> .
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/>
    ///   is <see langword="null"/>, which should not happen if null analysis
    ///   is enabled (<c>#nullable enable</c>).</exception>
    public static Ok<T> From<T>(T value) where T : notnull {
        // Null check for callers that don't use code analyzers to catch errors
        _ = value ?? throw new ArgumentNullException(nameof(value));
        return new Ok<T>(value);
    }

    /// <summary>
    ///   Create a new <see cref="IResult{T}"/> from a specified exception.
    /// </summary>
    /// <typeparam name="T">The type of the value wrapped by the returned
    ///   <see cref="IResult{T}"/>.</typeparam>
    /// <param name="ex">the <see cref="Exception"/> to wrap.</param>
    /// <returns>
    ///   An <see cref="Err{T}"/> wrapping <paramref name="ex"/> .
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="ex"/>
    ///   is <see langword="null"/>, which should not happen if null analysis
    ///   is enabled (<c>#nullable enable</c>).</exception>
    public static Err<T> From<T>(Exception ex) where T : notnull {
        // Null check for callers that don't use code analyzers to catch errors
        _ = ex ?? throw new ArgumentNullException(nameof(ex));
        return new Err<T>(ex);
    }

    #endregion
    #region From<T>(Func) Method

#pragma warning disable CA1715
    // CA1715: Prefix generic type parameter name E1 with 'T' — Shorter, simpler type parameters
    // just read easier when there are many of representing the type of element.

    /// <summary>
    ///   Creates a new <see cref="IResult{T}"/> from the results of executing a function.
    /// </summary>
    /// <typeparam name="T">The type of the value wrapped by the returned
    ///   <see cref="IResult{T}"/>.</typeparam>
    /// <typeparam name="E1">The type of an <see cref="Exception"/> descendent that
    ///   is an acceptable exception to wrap.</typeparam>
    /// <param name="func"></param>
    /// <returns>
    ///   A <see cref="IResult{T}"/> representing the result of executing
    ///   <paramref name="func"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///   Thrown when <paramref name="func"/> is <see langword="null"/>, which is a bug.
    /// </exception>
    /// <exception cref="Exception">
    ///   Any exception other than <typeparamref name="E1"/> is re-thrown.
    /// </exception>
    /// <remarks>
    ///   This method executes <paramref name="func"/> with three possible outcomes:
    ///   <list type="bullet">
    ///     <item><paramref name="func"/> returns a value; a <see cref="Ok{T}"/>
    ///       wrapping the value is returned.</item>
    ///     <item><paramref name="func"/> throws an exception of type <typeparamref name="E1"/>;
    ///       An <see cref="Err{T}"/> wrapping the exception is returned.</item>
    ///     <item><paramref name="func"/> throws any other exception; the exception is
    ///       re-thrown.</item>
    ///   </list>
    /// </remarks>
    public static IResult<T> From<T, E1>(Func<T> func)
            where T : notnull where E1 : Exception {
        // Null check for callers that don't use code analyzers to catch errors
        _ = func ?? throw new ArgumentNullException(nameof(func));
        try {
            return new Ok<T>(func());
        } catch (E1 ex) {
            return new Err<T>(ex);
        }
    }

    /// <summary>
    ///   Creates a new <see cref="IResult{T}"/> from the results of executing a function.
    /// </summary>
    /// <typeparam name="T">The type of the value wrapped by the returned
    ///   <see cref="IResult{T}"/>.</typeparam>
    /// <typeparam name="E1">The type of an <see cref="Exception"/> descendent that
    ///   is an acceptable exception to wrap.</typeparam>
    /// <typeparam name="E2">The type of another <see cref="Exception"/> descendent that
    ///   is an acceptable exception to wrap.</typeparam>
    /// <param name="func"></param>
    /// <returns>
    ///   A <see cref="IResult{T}"/> representing the result of executing
    ///   <paramref name="func"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///   Thrown when <paramref name="func"/> is <see langword="null"/>, which is a bug.
    /// </exception>
    /// <exception cref="Exception">
    ///   Any exception other than <typeparamref name="E1"/> or <typeparamref name="E2"/>
    ///   is re-thrown.
    /// </exception>
    /// <remarks>
    ///   This method executes <paramref name="func"/> with three possible outcomes:
    ///   <list type="bullet">
    ///     <item><paramref name="func"/> returns a value; a <see cref="Ok{T}"/>
    ///       wrapping the value is returned.</item>
    ///     <item><paramref name="func"/> throws an exception of type <typeparamref name="E1"/>
    ///       or <typeparamref name="E2"/>; An <see cref="Err{T}"/> wrapping the
    ///       exception is returned.</item>
    ///     <item><paramref name="func"/> throws any other exception; the exception is
    ///       re-thrown.</item>
    ///   </list>
    /// </remarks>
    public static IResult<T> From<T, E1, E2>(Func<T> func)
            where T : notnull where E1 : Exception where E2 : Exception {
        // Null check for callers that don't use code analyzers to catch errors
        _ = func ?? throw new ArgumentNullException(nameof(func));
        try {
            return new Ok<T>(func());
        } catch (Exception ex) when (ex is E1 or E2) {
            return new Err<T>(ex);
        }
    }

    /// <summary>
    ///   Creates a new <see cref="IResult{T}"/> from the results of executing a function.
    /// </summary>
    /// <typeparam name="T">The type of the value wrapped by the returned
    ///   <see cref="IResult{T}"/>.</typeparam>
    /// <typeparam name="E1">The type of an <see cref="Exception"/> descendent that
    ///   is an acceptable exception to wrap.</typeparam>
    /// <typeparam name="E2">The type of another <see cref="Exception"/> descendent that
    ///   is an acceptable exception to wrap.</typeparam>
    /// <typeparam name="E3">The third type of and <see cref="Exception"/> descendent that
    ///   is an acceptable exception to wrap.</typeparam>
    /// <param name="func"></param>
    /// <returns>
    ///   A <see cref="IResult{T}"/> representing the result of executing
    ///   <paramref name="func"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///   Thrown when <paramref name="func"/> is <see langword="null"/>, which is a bug.
    /// </exception>
    /// <exception cref="Exception">
    ///   Any exception other than <typeparamref name="E1"/>, <typeparamref name="E2"/>,
    ///   or <typeparamref name="E3"/> is re-thrown.
    /// </exception>
    /// <remarks>
    ///   This method executes <paramref name="func"/> with three possible outcomes:
    ///   <list type="bullet">
    ///     <item><paramref name="func"/> returns a value; a <see cref="Ok{T}"/>
    ///       wrapping the value is returned.</item>
    ///     <item><paramref name="func"/> throws an exception of type <typeparamref name="E1"/>,
    ///       <typeparamref name="E2"/>, or <typeparamref name="E3"/>; An
    ///       <see cref="Err{T}"/> wrapping the exception is returned.</item>
    ///     <item><paramref name="func"/> throws any other exception; the exception is
    ///       re-thrown.</item>
    ///   </list>
    /// </remarks>
    public static IResult<T> From<T, E1, E2, E3>(Func<T> func)
            where T : notnull where E1 : Exception where E2 : Exception where E3 : Exception {
        // Null check for callers that don't use code analyzers to catch errors
        _ = func ?? throw new ArgumentNullException(nameof(func));
        try {
            return new Ok<T>(func());
        } catch (Exception ex) when (ex is E1 or E2 or E3) {
            return new Err<T>(ex);
        }
    }

    /// <summary>
    ///   Creates a new <see cref="IResult{T}"/> from the results of executing a function.
    /// </summary>
    /// <typeparam name="T">The type of the value wrapped by the returned
    ///   <see cref="IResult{T}"/>.</typeparam>
    /// <typeparam name="E1">The type of an <see cref="Exception"/> descendent that
    ///   is an acceptable exception to wrap.</typeparam>
    /// <typeparam name="E2">The type of another <see cref="Exception"/> descendent that
    ///   is an acceptable exception to wrap.</typeparam>
    /// <typeparam name="E3">The third type of and <see cref="Exception"/> descendent that
    ///   is an acceptable exception to wrap.</typeparam>
    /// <typeparam name="E4">The fourth type of and <see cref="Exception"/> descendent that
    ///   is an acceptable exception to wrap.</typeparam>
    /// <param name="func"></param>
    /// <returns>
    ///   A <see cref="IResult{T}"/> representing the result of executing
    ///   <paramref name="func"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///   Thrown when <paramref name="func"/> is <see langword="null"/>, which is a bug.
    /// </exception>
    /// <exception cref="Exception">
    ///   Any exception other than <typeparamref name="E1"/> – <typeparamref name="E4"/>
    ///   is re-thrown.
    /// </exception>
    /// <remarks>
    ///   This method executes <paramref name="func"/> with three possible outcomes:
    ///   <list type="bullet">
    ///     <item><paramref name="func"/> returns a value; a <see cref="Ok{T}"/>
    ///       wrapping the value is returned.</item>
    ///     <item><paramref name="func"/> throws an exception of type <typeparamref name="E1"/> –
    ///     <typeparamref name="E4"/>; An <see cref="Err{T}"/> wrapping the exception
    ///       is returned.</item>
    ///     <item><paramref name="func"/> throws any other exception; the exception is
    ///       re-thrown.</item>
    ///   </list>
    /// </remarks>
    public static IResult<T> From<T, E1, E2, E3, E4>(Func<T> func)
            where T : notnull where E1 : Exception where E2 : Exception where E3 : Exception
            where E4 : Exception {
        // Null check for callers that don't use code analyzers to catch errors
        _ = func ?? throw new ArgumentNullException(nameof(func));
        try {
            return new Ok<T>(func());
        } catch (Exception ex) when (ex is E1 or E2 or E3 or E4) {
            return new Err<T>(ex);
        }
    }

    /// <summary>
    ///   Creates a new <see cref="IResult{T}"/> from the results of executing a function.
    /// </summary>
    /// <typeparam name="T">The type of the value wrapped by the returned
    ///   <see cref="IResult{T}"/>.</typeparam>
    /// <typeparam name="E1">The type of an <see cref="Exception"/> descendent that
    ///   is an acceptable exception to wrap.</typeparam>
    /// <typeparam name="E2">The type of another <see cref="Exception"/> descendent that
    ///   is an acceptable exception to wrap.</typeparam>
    /// <typeparam name="E3">The third type of and <see cref="Exception"/> descendent that
    ///   is an acceptable exception to wrap.</typeparam>
    /// <typeparam name="E4">The fourth type of and <see cref="Exception"/> descendent that
    ///   is an acceptable exception to wrap.</typeparam>
    /// <typeparam name="E5">The fifth type of and <see cref="Exception"/> descendent that
    ///   is an acceptable exception to wrap.</typeparam>
    /// <param name="func"></param>
    /// <returns>
    ///   A <see cref="IResult{T}"/> representing the result of executing
    ///   <paramref name="func"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///   Thrown when <paramref name="func"/> is <see langword="null"/>, which is a bug.
    /// </exception>
    /// <exception cref="Exception">
    ///   Any exception other than <typeparamref name="E1"/> – <typeparamref name="E5"/>
    ///   is re-thrown.
    /// </exception>
    /// <remarks>
    ///   This method executes <paramref name="func"/> with three possible outcomes:
    ///   <list type="bullet">
    ///     <item><paramref name="func"/> returns a value; a <see cref="Ok{T}"/>
    ///       wrapping the value is returned.</item>
    ///     <item><paramref name="func"/> throws an exception of type <typeparamref name="E1"/> –
    ///     <typeparamref name="E5"/>; An <see cref="Err{T}"/> wrapping the exception
    ///       is returned.</item>
    ///     <item><paramref name="func"/> throws any other exception; the exception is
    ///       re-thrown.</item>
    ///   </list>
    /// </remarks>
    public static IResult<T> From<T, E1, E2, E3, E4, E5>(Func<T> func)
            where T : notnull where E1 : Exception where E2 : Exception where E3 : Exception
            where E4 : Exception where E5 : Exception {
        // Null check for callers that don't use code analyzers to catch errors
        _ = func ?? throw new ArgumentNullException(nameof(func));
        try {
            return new Ok<T>(func());
        } catch (Exception ex) when (ex is E1 or E2 or E3 or E4 or E5) {
            return new Err<T>(ex);
        }
    }

#pragma warning restore CA1715

    #endregion
    #region ToResult<T> Method

    /// <summary>
    ///   Create a new <see cref="IResult{T}"/> from a specified value.
    /// </summary>
    /// <typeparam name="T">The type of the value wrapped by the returned
    ///   <see cref="IResult{T}"/>.</typeparam>
    /// <param name="value">the value to wrap.</param>
    /// <returns>
    ///   A <see cref="Ok{T}"/> wrapping <paramref name="value"/> .
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/>
    ///   is <see langword="null"/>, which should not happen if null analysis
    ///   is enabled (<c>#nullable enable</c>).</exception>
    public static IResult<T> ToResult<T>(this T value) where T : notnull {
        // Null check for callers that don't use code analyzers to catch errors
        _ = value ?? throw new ArgumentNullException(nameof(value));
        return new Ok<T>(value);
    }

    /// <summary>
    ///   Create a new <see cref="IResult{T}"/> from a specified exception.
    /// </summary>
    /// <typeparam name="T">The type of the value wrapped by the returned
    ///   <see cref="IResult{T}"/>.</typeparam>
    /// <param name="ex">the <see cref="Exception"/> to wrap.</param>
    /// <returns>
    ///   An <see cref="Err{T}"/> wrapping <paramref name="ex"/> .
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="ex"/>
    ///   is <see langword="null"/>, which should not happen if null analysis
    ///   is enabled (<c>#nullable enable</c>).</exception>
    public static IResult<T> ToResult<T>(this Exception ex) where T : notnull {
        // Null check for callers that don't use code analyzers to catch errors
        _ = ex ?? throw new ArgumentNullException(nameof(ex));
        return new Err<T>(ex);
    }

    #endregion
}
