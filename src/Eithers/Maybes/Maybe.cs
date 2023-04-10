using System;

namespace Ainsworth.Eithers;

/// <summary>
///   Methods to create <see cref="IMaybe{T}"/>s.
/// </summary>
public static class Maybe {

	#region Properties

	#endregion
	#region From<T> Method

	/// <summary>
	///   Create a <see cref="IMaybe{T}"/> from a specified possibly-null value.
	/// </summary>
	/// <typeparam name="T">The type of the value wrapped by the returned
	///   <see cref="IMaybe{T}"/>.</typeparam>
	/// <param name="value">The possibly-null value to wrap.</param>
	/// <returns>
	///   A <see cref="Some{T}"/> wrapping <paramref name="value"/> if
	///   <paramref name="value"/> is not <see langword="null"/>; otherwise the
	///   <see cref="Maybe{T}.None"/> singleton is returned. 
	/// </returns>
	public static IMaybe<T> From<T>(T? value) where T : struct =>
		value is T v ? new Some<T>(v) : Maybe<T>.None;

	/// <summary>
	///   Create a <see cref="IMaybe{T}"/> from a specified possibly-null value.
	/// </summary>
	/// <typeparam name="T">The type of the value wrapped by the returned
	///   <see cref="IMaybe{T}"/>.</typeparam>
	/// <param name="value">The possibly-null value to wrap.</param>
	/// <returns>
	///   A <see cref="Some{T}"/> wrapping <paramref name="value"/> if
	///   <paramref name="value"/> is not <see langword="null"/>; otherwise the
	///   <see cref="Maybe{T}.None"/> singleton is returned. 
	/// </returns>
	public static IMaybe<T> From<T>(T? value) where T : class =>
		value is not null ? new Some<T>(value) : Maybe<T>.None;

	#endregion
	#region FromValue<T> Method

	/// <summary>
	///   Create a <see cref="IMaybe{T}"/> from a specified, non-<see langword="null"/> value.
	/// </summary>
	/// <param name="value">The non-<see langword="null"/> value to convert.</param>
	/// <returns>
	///   A <see cref="Some{T}"/> wrapping the <paramref name="value"/>.
	/// </returns>
	/// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/>
	///   is <see langword="null"/>.</exception>
	public static Some<T> FromValue<T>(T value) where T : notnull {
		// Null check for callers that don't use code analyzers to catch errors
		_ = value ?? throw new ArgumentNullException(nameof(value));
		return new Some<T>(value);
	}

	#endregion
	#region ToIMaybe<T> Extension Method

	/// <summary>
	///   Convert a possibly-<see langword="null"/> value to a <see cref="IMaybe{T}"/>.
	/// </summary>
	/// <param name="value">The non-<see langword="null"/> to convert.</param>
	/// <returns>
	///   A <see cref="Some{T}"/> wrapping <paramref name="value"/> if
	///   <paramref name="value"/> is not <see langword="null"/>; otherwise the
	///   <see cref="Maybe{T}.None"/> singleton is returned. 
	/// </returns>
	/// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/>
	///   is <see langword="null"/>.</exception>
	public static IMaybe<T> ToMaybe<T>(this T? value) where T : struct => From(value);

	/// <summary>
	///   Convert a possibly-<see langword="null"/> value to a <see cref="IMaybe{T}"/>.
	/// </summary>
	/// <param name="value">The non-<see langword="null"/> to convert.</param>
	/// <returns>
	///   A <see cref="Some{T}"/> wrapping <paramref name="value"/> if
	///   <paramref name="value"/> is not <see langword="null"/>; otherwise the
	///   <see cref="Maybe{T}.None"/> singleton is returned. 
	/// </returns>
	/// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/>
	///   is <see langword="null"/>.</exception>
	public static IMaybe<T> ToMaybe<T>(this T? value) where T : class => From(value);

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
	public static Some<T> ToSome<T>(this T value) where T : notnull {
		// Null check for callers that don't use code analyzers to catch errors
		_ = value ?? throw new ArgumentNullException(nameof(value));
		return new Some<T>(value);
	}

	#endregion
}
