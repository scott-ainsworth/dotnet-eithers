using System;
using System.Runtime.CompilerServices;

namespace Ainsworth.Eithers;

/// <summary>
///   Methods to create <see cref="IOption{T}"/>s.
/// </summary>
public static class Option {

	#region From<T> Method

	/// <summary>
	///   Create an <see cref="IOption{T}"/> from a specified possibly-null value.
	/// </summary>
	/// <typeparam name="T">The type of the value wrapped by the returned
	///   <see cref="IOption{T}"/>.</typeparam>
	/// <param name="value">The possibly-null value to wrap.</param>
	/// <returns>
	///   A <see cref="Some{T}"/> wrapping <paramref name="value"/>, if <paramref name="value"/>
	///   is not <see langword="null"/>; otherwise the <see cref="None{T}"/> singleton is returned. 
	/// </returns>
	public static IOption<T> From<T>(T? value) where T : struct =>
		value is T v ? new Some<T>(v) : None<T>.singleton;

	/// <summary>
	///   Create an <see cref="IOption{T}"/> from a specified possibly-null value.
	/// </summary>
	/// <typeparam name="T">The type of the value wrapped by the returned
	///   <see cref="IOption{T}"/>.</typeparam>
	/// <param name="value">The possibly-null value to wrap.</param>
	/// <returns>
	///   A <see cref="Some{T}"/> wrapping <paramref name="value"/>, if <paramref name="value"/>
	///   is not <see langword="null"/>; otherwise the <see cref="None{T}"/> singleton is returned. 
	/// </returns>
	public static IOption<T> From<T>(T? value) where T : class =>
		value is not null ? new Some<T>(value) : None<T>.singleton;

	#endregion
	#region FromValue<T> Method

	/// <summary>
	///   Create a <see cref="IOption{T}"/> from a specified, non-<see langword="null"/> value.
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
	#region ToOption<T> Extension Method

	/// <summary>
	///   Convert a possibly-<see langword="null"/> value to an <see cref="IOption{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of the value wrapped by the returned
	///   <see cref="IOption{T}"/>.</typeparam>
	/// <param name="value">The possibly-<see langword="null"/> value to convert.</param>
	/// <returns>
	///   A <see cref="Some{T}"/> wrapping <paramref name="value"/>, if <paramref name="value"/>
	///   is not <see langword="null"/>; otherwise a <see cref="None{T}"/> singleton is returned. 
	/// </returns>
	public static IOption<T> ToOption<T>(this T? value) where T : struct => From(value);

	/// <summary>
	///   Convert a possibly-<see langword="null"/> value to an <see cref="IOption{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of the value wrapped by the returned
	///   <see cref="IOption{T}"/>.</typeparam>
	/// <param name="value">The possibly-<see langword="null"/> value to convert.</param>
	/// <returns>
	///   A <see cref="Some{T}"/> wrapping <paramref name="value"/>, if <paramref name="value"/>
	///   is not <see langword="null"/>; otherwise a <see cref="None{T}"/> singleton is returned. 
	/// </returns>
	/// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/>
	///   is <see langword="null"/>.</exception>
	public static IOption<T> ToOption<T>(this T? value) where T : class => From(value);

	#endregion
	#region ToSome<T> Extension Method

	/// <summary>
	///   Convert a non-<see langword="null"/> value to a <see cref="Some{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of the value wrapped by the returned
	///   <see cref="IOption{T}"/>.</typeparam>
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
