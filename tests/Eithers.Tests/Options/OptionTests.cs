using Ainsworth.Eithers;

using TestSupport;
using static TestSupport.TestRunner;

// Disable SonarLint S2699 because most assertions are in called subroutines.
#pragma warning disable S2699 // Test should include assertions
// Disable SonarLint S4144 because it does not consider type constraints.
#pragma warning disable S4144 // Methods should not have identical implementations

namespace Options_Options_Tests;

/// <summary>
///   Unit tests for <see cref="Option.From{T}(T?)"/> methods.
/// </summary>
[TestClass]
public class From_Tests {

	/// <summary>
	///   The <see cref="Option.From{T}(T?)"/> method returns a <see cref="Some{T}"/>
	///   wrapping the provided nullable value.
	/// </summary>
	[TestMethod]
	public void Options_From_creates_Some_from_nullable_value() =>
		RunUnitTests(new From_creates_Some_from_value());

	private sealed class From_creates_Some_from_value : IUnitTest1Split {
		public void RunTestOnReferenceType<T>(T value) where T : class {
			var option = Option.From((T?)value);
			Assert.IsInstanceOfType<IOption<T>>(option);
			Assert.IsInstanceOfType<Some<T>>(option);
			var some = (option as Some<T>)!;
			Assert.AreEqual(value, some.Value);
		}
		public void RunTestOnValueType<T>(T value) where T : struct {
			var option = Option.From((T?)value);
			Assert.IsInstanceOfType<IOption<T>>(option);
			Assert.IsInstanceOfType<Some<T>>(option);
			var some = (option as Some<T>)!;
			Assert.AreEqual(value, some.Value);
		}
	}

	/// <summary>
	///   The <see cref="Option.From{T}(T?)"/> method returns an instance of the correct
	///   <see cref="None{T}"/> type for null argument.
	/// </summary>
	[TestMethod]
	public void Options_From_returns_None_from_null() =>
		RunUnitTests(new From_returns_None_from_null());

	private sealed class From_returns_None_from_null : IUnitTest0Split {
		public void RunTestOnReferenceType<T>() where T : class {
			var option = Option.From((T?)null);
			Assert.IsInstanceOfType<IOption<T>>(option);
			Assert.IsInstanceOfType<None<T>>(option);
		}
		public void RunTestOnValueType<T>() where T : struct {
			var option = Option.From((T?)null);
			Assert.IsInstanceOfType<IOption<T>>(option);
			Assert.IsInstanceOfType<None<T>>(option);
		}
	}
}

/// <summary>
///   Unit tests for <see cref="Option.FromValue{T}(T)"/> methods.
/// </summary>
[TestClass]
public class FromValue_Tests {

	/// <summary>
	///   The <see cref="Option.FromValue{T}(T)"/> method returns a <see cref="Some{T}"/> of the
	///   correct type wrapping the provided primitive type value.
	/// </summary>
	[TestMethod]
	public void Options_FromValue_creates_Some_from_value() =>
		RunUnitTests(new FromValue_creates_Some_from_value());

	private sealed class FromValue_creates_Some_from_value : IUnitTest1Split {
		public void RunTestOnReferenceType<T>(T value) where T : class {
			var some = Option.FromValue(value);
			Assert.IsInstanceOfType<IOption<T>>(some);
			Assert.IsInstanceOfType<Some<T>>(some);
			Assert.AreEqual(value, some.Value);
		}
		public void RunTestOnValueType<T>(T value) where T : struct {
			var some = Option.FromValue(value);
			Assert.IsInstanceOfType<IOption<T>>(some);
			Assert.IsInstanceOfType<Some<T>>(some);
			Assert.AreEqual(value, some.Value);
		}
	}

	/// <summary>
	///   The <see cref="Option.FromValue{T}(T)"/> method throws an
	///   <see cref="ArgumentNullException"/> when a non-nullable reference type value
	///   is <see langword="null"/>.
	/// </summary>
	/// <remarks>
	///     <para>This condition will normally be caught by the C# compiler's null analysis,
	///   if nullable analysis is enabled (<c>#nullable enable</c>).  However, analysis
	///   can not prevent <see cref="Option.FromValue{T}(T)"/> from being called with a
	///   <see langword="null"/> value.</para>
	///     <para>*Note*: This cannot happen for primitive and value types.</para>
	/// </remarks>
	[TestMethod]
	public void Options_FromValue_throws_for_null_reference_types() =>
		RunUnitTests(new FromValue_throws_for_null());

	private sealed class FromValue_throws_for_null : IUnitTest0Split {
		public void RunTestOnReferenceType<T>() where T : class {
			var ex = Assert.ThrowsException<ArgumentNullException>(() => Option.FromValue<T>(null!));
			Assert.AreEqual("value", ex.ParamName);
		}
		public void RunTestOnValueType<T>() where T : struct {
			// This can only be executed for reference types
		}
	}
}

/// <summary>
///   Unit tests for <see cref="Option.ToOption{T}(T?)"/> extension methods.
/// </summary>
[TestClass]
public class ToOption_Tests {

	/// <summary>
	///   Ensure the ToOption() extension method returns a <see cref="Some{T}"/> of the correct
	///   type wrapping the provided nullable value.
	/// </summary>
	[TestMethod]
	public void Options_ToOption_creates_Some_for_nullable_value_() =>
		RunUnitTests(new ToOption_creates_Some_from_nullable_value());

	private sealed class ToOption_creates_Some_from_nullable_value : IUnitTest1Split {
		public void RunTestOnReferenceType<T>(T value) where T : class {
			var option = value.ToOption();
			Assert.IsInstanceOfType<IOption<T>>(option);
			Assert.IsInstanceOfType<Some<T>>(option);
			var some = (Some<T>)option;
			Assert.AreEqual(value, some.Value);
		}
		public void RunTestOnValueType<T>(T value) where T : struct {
			var option = ((T?)value).ToOption();
			Assert.IsInstanceOfType<IOption<T>>(option);
			Assert.IsInstanceOfType<Some<T>>(option);
			var some = (Some<T>)option;
			Assert.AreEqual(value, some.Value);
		}
	}

	/// <summary>
	///   The <see cref="Option.ToOption{T}(T?)"/> method returns an instance of the correct
	///   <see cref="None{T}"/> type for the provided null value.
	/// </summary>
	[TestMethod]
	public void Options_ToOption_returns_None_for_null() =>
		RunUnitTests(new ToOption_returns_None_for_null());

	private sealed class ToOption_returns_None_for_null : IUnitTest0Split {
		public void RunTestOnReferenceType<T>() where T : class {
			var option = ((T?)null).ToOption();
			Assert.IsInstanceOfType<IOption<T>>(option);
			Assert.IsInstanceOfType<None<T>>(option);
		}
		public void RunTestOnValueType<T>() where T : struct {
			var option = ((T?)null).ToOption();
			Assert.IsInstanceOfType<IOption<T>>(option);
			Assert.IsInstanceOfType<None<T>>(option);
		}
	}
}

/// <summary>
///   Unit tests for <see cref="Option.ToOption{T}(T?)"/> extension methods.
/// </summary>
[TestClass]
public class ToSome_Tests {

	/// <summary>
	///   Ensure the ToSome() extension method returns a <see cref="Some{T}"/> of the correct
	///   type wrapping the provided nullable primitive value.
	/// </summary>
	[TestMethod]
	public void Options_ToSome_returns_Some_for_primitive_types() =>
		RunUnitTests(new ToSome_creates_Some_from_value());

	private sealed class ToSome_creates_Some_from_value : IUnitTest1Split {
		public void RunTestOnReferenceType<T>(T value) where T : class {
			var some = value.ToSome();
			Assert.IsInstanceOfType<IOption<T>>(some);
			Assert.IsInstanceOfType<Some<T>>(some);
			Assert.AreEqual(value, some.Value);
		}
		public void RunTestOnValueType<T>(T value) where T : struct {
			var some = value.ToSome();
			Assert.IsInstanceOfType<IOption<T>>(some);
			Assert.IsInstanceOfType<Some<T>>(some);
			Assert.AreEqual(value, some.Value);
		}
	}

	/// <summary>
	///   The <see cref="Option.ToSome{T}(T)"/> method throws an
	///   <see cref="ArgumentNullException"/> when a non-nullable reference type value
	///   is <see langword="null"/>.
	/// </summary>
	/// <remarks>
	///     <para>This condition will normally be caught by the C# compiler's null analysis,
	///   if nullable analysis is enabled (<c>#nullable enable</c>).  However, analysis
	///   can not prevent <see cref="Option.FromValue{T}(T)"/> from being called with a
	///   <see langword="null"/> value.</para>
	///     <para>*Note*: This cannot happen for primitive and value types.</para>
	/// </remarks>
	[TestMethod]
	public void Options_ToSome_throws_for_null_reference_types() =>
		RunUnitTests(new ToSome_throws_for_null());

	private sealed class ToSome_throws_for_null : IUnitTest0Split {
		public void RunTestOnReferenceType<T>() where T : class {
			var ex = Assert.ThrowsException<ArgumentNullException>(() => ((T)null!).ToSome());
			Assert.AreEqual("value", ex.ParamName);
		}
		public void RunTestOnValueType<T>() where T : struct {
			// This is not allowed by C# semantics
		}
	}
}
