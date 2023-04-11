namespace Ainsworth.Eithers;

/// <summary>
///   A Option Monad (a container that represents a value that might or might not exist).
/// </summary>
/// <typeparam name="T">The type of the value contained by this
///   <see cref="Option{T}"/>.</typeparam>
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
public static class Option<T> where T : notnull {

	/// <summary>
	///   A singleton representing a <see cref="Option{T}"/> with no value.
	/// </summary>
	/// <value>
	///   The single instance of <see cref="None{T}"/>.
	/// </value>
	public static readonly IOption<T> None = None<T>.NoneSingleton;
}