using System;

namespace Ainsworth.Eithers;

/// <summary>
///   Methods to create <see cref="Maybe{T}"/>s.
/// </summary>
public static class Maybe {

    #region From<T> Method

    /// <summary>
    ///   Create a <see cref="Maybe{T}"/> from a specified value.
    /// </summary>
    /// <typeparam name="T">The type of the value wrapped by the returned
    ///   <see cref="Maybe{T}"/>.</typeparam>
    /// <param name="value">The possibly-null value to wrap.</param>
    /// <returns>
    ///   A <see cref="Some{T}"/> wrapping <paramref name="value"/> if
    ///   <paramref name="value"/> is not <see langword="null"/>; otherwise the
    ///   <see cref="Maybe{T}.None"/> singleton is returned. 
    /// </returns>
    public static Maybe<T> From<T>(T? value) where T : struct =>
        value is T v ? new Some<T>(v) : Maybe<T>.None;

    /// <summary>
    ///   Create a <see cref="Maybe{T}"/> from a specified value.
    /// </summary>
    /// <typeparam name="T">The type of the value wrapped by the returned
    ///   <see cref="Maybe{T}"/>.</typeparam>
    /// <param name="value">The possibly-null value to wrap.</param>
    /// <returns>
    ///   A <see cref="Some{T}"/> wrapping <paramref name="value"/> if
    ///   <paramref name="value"/> is not <see langword="null"/>; otherwise the
    ///   <see cref="Maybe{T}.None"/> singleton is returned. 
    /// </returns>
    public static Maybe<T> From<T>(T? value) where T : class =>
        value is not null ? new Some<T>(value) : Maybe<T>.None;

    #endregion
    #region Some<T> Method

    /// <summary>
    ///   Create a <see cref="Maybe{T}"/> from a specified, non-<see langword="null"/> value.
    /// </summary>
    /// <param name="value">The non-<see langword="null"/> value to convert.</param>
    /// <returns>
    ///   A <see cref="Some{T}"/> wrapping the <paramref name="value"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/>
    ///   is <see langword="null"/>.</exception>
    public static Some<T> Some<T>(T value) where T : notnull =>
        // Null check for callers that don't use code analyzers to catch errors
        value is null
            ? throw new ArgumentNullException(nameof(value))
            : new Some<T>(value);

    #endregion
    #region ToMaybe<T> Extension Method

    /// <summary>
    ///   Convert a possibly-<see langword="null"/> value to a <see cref="Maybe{T}"/>.
    /// </summary>
    /// <param name="value">The non-<see langword="null"/> to convert.</param>
    /// <returns>
    ///   A <see cref="Some{T}"/> wrapping <paramref name="value"/> if
    ///   <paramref name="value"/> is not <see langword="null"/>; otherwise the
    ///   <see cref="Maybe{T}.None"/> singleton is returned. 
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/>
    ///   is <see langword="null"/>.</exception>
    public static Maybe<T> ToMaybe<T>(this T? value) where T : struct => From(value);

    /// <summary>
    ///   Convert a possibly-<see langword="null"/> value to a <see cref="Maybe{T}"/>.
    /// </summary>
    /// <param name="value">The non-<see langword="null"/> to convert.</param>
    /// <returns>
    ///   A <see cref="Some{T}"/> wrapping <paramref name="value"/> if
    ///   <paramref name="value"/> is not <see langword="null"/>; otherwise the
    ///   <see cref="Maybe{T}.None"/> singleton is returned. 
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/>
    ///   is <see langword="null"/>.</exception>
    public static Maybe<T> ToMaybe<T>(this T? value) where T : class => From(value);

    #endregion
    #region ToSome<T> Extension Method

    /// <summary>
    ///   Convert a non-<see langword="null"/> value to a <see cref="Some{T}"/>.
    /// </summary>
    /// <param name="value">The non-<see langword="null"/> value to convert.</param>
    /// <returns>
    ///   A <see cref="Some{T}"/> wrapping <paramref name="value"/>. 
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/>
    ///   is <see langword="null"/>.</exception>
    public static Some<T> ToSome<T>(this T value) where T : notnull =>
        value is null // Null check for callers that don't use code analyzers to catch errors
            ? throw new ArgumentNullException(nameof(value))
            : new Some<T>(value);

    #endregion
}
