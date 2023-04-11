using System;
using System.Collections.Generic;

namespace Ainsworth.Eithers;

/// <summary>
///   A Option Monad (a container that represents a value that might or might not exist).
/// </summary>
/// <typeparam name="T">The type of the value contained by this
///   <see cref="IOption{T}"/>.</typeparam>
/// <remarks>
///   *References*:
///   <list type="number">
///     <item><a href="https://en.wikipedia.org/wiki/Monad_(functional_programming)">
///       Wikipedia: Monad (functional programming)</a></item>
///     <item><a href="https://www.dotnetcurry.com/patterns-practices/1510/maybe-monad-csharp">
///       .NET Curry: The Option Monad (C#)</a></item>
///   </list>
/// </remarks>
[System.Diagnostics.CodeAnalysis.SuppressMessage(
	"Major Code Smell", "S4035:Classes implementing 'IEquatable<T>' should be sealed",
	Justification = "Constructor is protected and all subclasses are sealed.")]
public interface IOption<T> : IEquatable<IOption<T>>, IEquatable<None<T>>, IEquatable<Some<T>>,
	IEquatable<T>, IEnumerable<T> where T : notnull {

	#region Properties

	/// <summary>
	///   Determine if a <see cref="IOption{T}"/> wraps a value.
	/// </summary>
	/// <value>
	///   <see langword="true"/> if this instance wraps a value;
	///   otherwise <see langword="true"/>.
	/// </value>
	public abstract bool HasValue { get; }

	#endregion
	#region TryGetValue

	/// <summary>
	///   Gets the value wrapped by this instance.
	/// </summary>
	/// <param name="value">When this method returns, contains the value wrapped by this instance,
	///   if this instance has a value; otherwise, the default value for type
	///   <typeparamref name="T"/>.</param>
	/// <returns>
	///   <see langword="true"/> if this instance wraps a value; otherwise, <see langword="false"/>.
	/// </returns>
	public abstract bool TryGetValue(out T value);

	#endregion
}
