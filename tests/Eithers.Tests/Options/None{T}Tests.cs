using System.Collections;
using System.Reflection;

using Ainsworth.Eithers;

using TestSupport;
using static TestSupport.TestRunner;

// Disable SonarLint S2699 because most assertions are in called subroutines.
#pragma warning disable S2699 // Test should include assertions
// Disable SonarLint S4144 because it does not consider type constraints.
#pragma warning disable S4144 // Methods should not have identical implementations

namespace Options_NoneT_Tests;

/// <summary>
///   Unit tests for <see cref="None{T}"/> constructors.
/// </summary>
[TestClass]
public class Constructor_Tests {

	/// <summary>
	///   The <see cref="None{T}"/> constructors' visibility is not public.
	/// </summary>
	[TestMethod]
	public void NoneT_constructors_are_private() =>
		RunUnitTests(new NoneT_Constructors_are_not_public());

	private sealed class NoneT_Constructors_are_not_public : IUnitTest0 {
		public void RunTest<T>() where T : notnull {
			var nonPrivateConstructors = typeof(None<T>).
				GetConstructors(
					BindingFlags.Public | BindingFlags.NonPublic |
					BindingFlags.Instance | BindingFlags.Static).
				Where(c => !c.IsPrivate).ToArray();
			Assert.IsFalse(
				nonPrivateConstructors.Any(),
				$"{typeof(T)} has at least 1 non-private constructor");
		}
	}
}

/// <summary>
///   Unit test for <see cref="None{T}.Equals(IOption{T})"/> tests.
/// </summary>
[TestClass]
public class EqualsOptionT_Tests {

	/// <summary>
	///  The <see cref="None{T}.Equals(IOption{T})"/> method returns <see langword="false"/>
	///  for other <see cref="None{T}"/> values.
	/// </summary>
	[TestMethod]
	public void NoneT_EqualsOptionT_returns_false_for_NoneT_of_different_type() =>
		RunUnitTests(new EqualsOptionT_returns_false_for_None_of_different_type());

	private sealed class EqualsOptionT_returns_false_for_None_of_different_type : IUnitTest0Split {
		public void RunTestOnReferenceType<T>() where T : class {
			var none = Option.From<T>(null!);
			var none2 = Option.From<bool>(null!);
			Assert.IsFalse(none.Equals(none2));
		}
		public void RunTestOnValueType<T>() where T : struct {
			var none = Option.From<T>(null!);
			var none2 = Option.From<bool>(null!);
			Assert.IsFalse(none.Equals(none2));
		}
	}

	/// <summary>
	///  The <see cref="None{T}.Equals(IOption{T})"/> method throws a
	///  <see cref="ArgumentNullException"/> for a <see langword="null"/> argument.
	/// </summary>
	/// <remarks>
	///   This can only happen if called from a "#nullable disable" environment.
	/// </remarks>
	[TestMethod]
	public void NoneT_EqualsOptionT_returns_throws_for_null_argument() =>
		RunUnitTests(new EqualsOptionT_returns_throws_for_null_argument());

	private sealed class EqualsOptionT_returns_throws_for_null_argument : IUnitTest0Split {
		public void RunTestOnReferenceType<T>() where T : class {
			var none = Option.From<T>(null!);
			// Use 'null!' to simulate call from '#nullable disable' environment
			var ex = Assert.ThrowsException<ArgumentNullException>(
				() => none.Equals((IOption<T>)null!));
			Assert.AreEqual("other", ex.ParamName);
		}
		public void RunTestOnValueType<T>() where T : struct {
			var none = Option.From<T>(null!);
			// Use 'null!' to simulate call from '#nullable disable' environment
			var ex = Assert.ThrowsException<ArgumentNullException>(
				() => none.Equals((IOption<T>)null!));
			Assert.AreEqual("other", ex.ParamName);
		}
	}

	/// <summary>
	///  The <see cref="None{T}.Equals(IOption{T})"/> method returns <see langword="true"/>
	///  for the same <see cref="None{T}"/> value.
	/// </summary>
	[TestMethod]
	public void NoneT_EqualsOptionT_returns_true_for_NoneT_of_same_type() =>
		RunUnitTests(new EqualsOptionT_returns_true_for_NoneT_of_same_type());

	private sealed class EqualsOptionT_returns_true_for_NoneT_of_same_type : IUnitTest0Split {
		public void RunTestOnReferenceType<T>() where T : class {
			var none = Option.From<T>(null!);
			var none2 = Option.From<T>(null!);
			Assert.IsTrue(none.Equals(none2));
		}
		public void RunTestOnValueType<T>() where T : struct {
			var none = Option.From<T>(null!);
			var none2 = Option.From<T>(null!);
			Assert.IsTrue(none.Equals(none2));
		}
	}

	/// <summary>
	///  The <see cref="None{T}.Equals(IOption{T})"/> method returns <see langword="false"/>
	///  for <see cref="Some{T}"/>.
	/// </summary>
	[TestMethod]
	public void NoneT_EqualsOptionT_returns_true_for_SomeT() =>
		RunUnitTests(new EqualsOptionT_returns_true_for_SomeT());

	private sealed class EqualsOptionT_returns_true_for_SomeT : IUnitTest1Split {
		public void RunTestOnReferenceType<T>(T value) where T : class {
			var none = Option.From<T>(null!);
			var some = Option.FromValue(value);
			Assert.IsFalse(none.Equals(some));
		}
		public void RunTestOnValueType<T>(T value) where T : struct {
			var none = Option.From<T>(null!);
			var some = Option.FromValue(value);
			Assert.IsFalse(none.Equals(some));
		}
	}
}

/// <summary>
///   Unit test for <see cref="None{T}"/>.Equals(object) tests.
/// </summary>
[TestClass]
public class EqualsObject_Tests {

	/// <summary>
	///  The <see cref="None{T}.Equals(object)"/> method returns <see langword="false"/>
	///  for other <see cref="None{T}"/> values.
	/// </summary>
	[TestMethod]
	public void NoneT_EqualsObject_returns_false_for_NoneT_of_different_type() =>
		RunUnitTests(new EqualsObject_returns_false_for_None_of_different_type());

	private sealed class EqualsObject_returns_false_for_None_of_different_type : IUnitTest0Split {
		public void RunTestOnReferenceType<T>() where T : class {
			var none = Option.From<T>(null!);
			var none2 = Option.From<bool>(null!);
			Assert.IsFalse(none.Equals((object)none2));
		}
		public void RunTestOnValueType<T>() where T : struct {
			var none = Option.From<T>(null!);
			var none2 = Option.From<bool>(null!);
			Assert.IsFalse(none.Equals((object)none2));
		}
	}

	/// <summary>
	///  The <see cref="None{T}.Equals(object)"/> method returns <see langword="false"/>
	///  for <see langword="null"/>.
	/// </summary>
	[TestMethod]
	public void NoneT_EqualsObject_returns_false_for_null_argument() =>
		RunUnitTests(new EqualsObject_returns_false_for_null_argument());

	private sealed class EqualsObject_returns_false_for_null_argument : IUnitTest0Split {
		public void RunTestOnReferenceType<T>() where T : class {
			var none = Option.From<T>(null!);
			Assert.IsFalse(none.Equals((object)null!));
		}
		public void RunTestOnValueType<T>() where T : struct {
			var none = Option.From<T>(null!);
			Assert.IsFalse(none.Equals((object)null!));
		}
	}

	/// <summary>
	///  The <see cref="None{T}.Equals(object)"/> method returns <see langword="true"/>
	///  for the same <see cref="None{T}"/> value.
	/// </summary>
	[TestMethod]
	public void NoneT_EqualsObject_returns_true_for_NoneT_of_same_type() =>
		RunUnitTests(new EqualsObject_returns_true_for_NoneT_of_same_type());

	private sealed class EqualsObject_returns_true_for_NoneT_of_same_type : IUnitTest0Split {
		public void RunTestOnReferenceType<T>() where T : class {
			var none = Option.From<T>(null!);
			var none2 = Option.From<T>(null!);
			Assert.IsTrue(none.Equals((object)none2));
		}
		public void RunTestOnValueType<T>() where T : struct {
			var none = Option.From<T>(null!);
			var none2 = Option.From<T>(null!);
			Assert.IsTrue(none.Equals((object)none2));
		}
	}

	/// <summary>
	///  The <see cref="None{T}.Equals(object)"/> method returns <see langword="false"/>
	///  for <see cref="Some{T}"/>.
	/// </summary>
	[TestMethod]
	public void NoneT_EqualsObject_returns_false_for_SomeT() =>
		RunUnitTests(new EqualsObject_returns_false_for_SomeT());

	private sealed class EqualsObject_returns_false_for_SomeT : IUnitTest1Split {
		public void RunTestOnReferenceType<T>(T value) where T : class {
			var none = Option.From<T>(null!);
			var some = Option.FromValue(value);
			Assert.IsFalse(none.Equals((object)some));
		}
		public void RunTestOnValueType<T>(T value) where T : struct {
			var none = Option.From<T>(null!);
			var some = Option.FromValue(value);
			Assert.IsFalse(none.Equals((object)some));
		}
	}
}

/// <summary>
///   Unit test for <see cref="None{T}"/>.Equals(object) tests.
/// </summary>
[TestClass]
public class EqualsNoneT_Tests {

	/// <summary>
	///  The <see cref="None{T}.Equals(None{T})"/> method returns <see langword="false"/>
	///  for other <see cref="None{T}"/> values.
	/// </summary>
	[TestMethod]
	public void NoneT_EqualsNoneT_returns_false_for_NoneT_of_different_type() =>
		RunUnitTests(new EqualsNoneT_returns_false_for_None_of_different_type());

	private sealed class EqualsNoneT_returns_false_for_None_of_different_type : IUnitTest0Split {
		public void RunTestOnReferenceType<T>() where T : class {
			var none = Option.From<T>(null!);
			var none2 = Option.From<bool>(null!);
			Assert.IsFalse(none.Equals(none2));
		}
		public void RunTestOnValueType<T>() where T : struct {
			var none = Option.From<T>(null!);
			var none2 = Option.From<bool>(null!);
			Assert.IsFalse(none.Equals(none2));
		}
	}

	/// <summary>
	///  The <see cref="None{T}.Equals(None{T})"/> method throws a
	///  <see cref="ArgumentNullException"/> for a <see langword="null"/> argument.
	/// </summary>
	/// <remarks>
	///   This can only happen if called from a "#nullable disable" environment.
	/// </remarks>
	[TestMethod]
	public void NoneT_EqualsNoneT_returns_throws_for_null_argument() =>
		RunUnitTests(new EqualsNoneT_returns_throws_for_null_argument());

	private sealed class EqualsNoneT_returns_throws_for_null_argument : IUnitTest0Split {
		public void RunTestOnReferenceType<T>() where T : class {
			var none = Option.From<T>(null!);
			var ex = Assert.ThrowsException<ArgumentNullException>(
				() => none.Equals((None<T>)null!));
			Assert.AreEqual("other", ex.ParamName);
		}
		public void RunTestOnValueType<T>() where T : struct {
			var none = Option.From<T>(null!);
			var ex = Assert.ThrowsException<ArgumentNullException>(
				() => none.Equals((None<T>)null!));
			Assert.AreEqual("other", ex.ParamName);
		}
	}

	/// <summary>
	///  The <see cref="None{T}.Equals(None{T})"/> method returns <see langword="true"/>
	///  for the same <see cref="None{T}"/>.
	/// </summary>
	[TestMethod]
	public void NoneT_EqualsNoneT_returns_true_for_NoneT_of_same_type() =>
		RunUnitTests(new EqualsNoneT_returns_true_for_NoneT_of_same_type());

	private sealed class EqualsNoneT_returns_true_for_NoneT_of_same_type : IUnitTest0Split {
		public void RunTestOnReferenceType<T>() where T : class {
			var none = Option.From<T>(null!);
			var none2 = Option.From<T>(null!);
			Assert.IsTrue(none.Equals((None<T>)none2));
		}
		public void RunTestOnValueType<T>() where T : struct {
			var none = Option.From<T>(null!);
			var none2 = Option.From<T>(null!);
			Assert.IsTrue(none.Equals((None<T>)none2));
		}
	}
}

/// <summary>
///   Unit test for the <see cref="None{T}.Equals(object)"/> method.
/// </summary>
[TestClass]
public class EqualsSomeT_Tests {

	/// <summary>
	///  The <see cref="None{T}.Equals(Some{T})"/> method returns <see langword="false"/>.
	/// </summary>
	[TestMethod]
	public void NoneT_EqualsSomeT_returns_false_for_SomeT() =>
		RunUnitTests(new EqualsSomeT_returns_false_for_SomeT());

	private sealed class EqualsSomeT_returns_false_for_SomeT : IUnitTest1Split {
		public void RunTestOnReferenceType<T>(T value) where T : class {
			var none = Option.From<T>(null!);
			var some = Option.FromValue(value);
			Assert.IsFalse(none.Equals(some));
		}
		public void RunTestOnValueType<T>(T value) where T : struct {
			var none = Option.From<T>(null!);
			var some = Option.FromValue(value);
			Assert.IsFalse(none.Equals(some));
		}
	}

	/// <summary>
	///  The <see cref="None{T}.Equals(Some{T})"/> method throws a
	///  <see cref="ArgumentNullException"/> for a <see langword="null"/> argument.
	/// </summary>
	/// <remarks>
	///   This can only happen if called from a "#nullable disable" environment.
	/// </remarks>
	[TestMethod]
	public void NoneT_EqualsSomeT_returns_throws_for_null_argument() =>
		RunUnitTests(new EqualsSomeT_returns_throws_for_null_argument());

	private sealed class EqualsSomeT_returns_throws_for_null_argument : IUnitTest0Split {
		public void RunTestOnReferenceType<T>() where T : class {
			var none = Option.From<T>(null!);
			// Use 'null!' to simulate call from '#nullable disable' environment
			var ex = Assert.ThrowsException<ArgumentNullException>(
				() => none.Equals((Some<T>)null!));
			Assert.AreEqual("other", ex.ParamName);
		}
		public void RunTestOnValueType<T>() where T : struct {
			var none = Option.From<T>(null!);
			// Use 'null!' to simulate call from '#nullable disable' environment
			var ex = Assert.ThrowsException<ArgumentNullException>(
				() => none.Equals((Some<T>)null!));
			Assert.AreEqual("other", ex.ParamName);
		}
	}
}

/// <summary>
///   Unit test for the <see cref="None{T}.Equals(T)"/> method.
/// </summary>
[TestClass]
public class EqualsT_Tests {

	/// <summary>
	///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="false"/>.
	/// </summary>
	[TestMethod]
	public void NoneT_EqualsT_returns_false_for_value() =>
		RunUnitTests(new EqualsT_returns_false_for_value());

	private sealed class EqualsT_returns_false_for_value : IUnitTest1Split {
		public void RunTestOnReferenceType<T>(T value) where T : class {
			var none = Option.From<T>(null!);
			// Use 'null!' to simulate call from #nullable disable environment
			Assert.IsFalse(none.Equals(value));
		}
		public void RunTestOnValueType<T>(T value) where T : struct {
			var none = Option.From<T>(null!);
			// Use 'null!' to simulate call from #nullable disable environment
			Assert.IsFalse(none.Equals(value));
		}
	}

	/// <summary>
	///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="false"/>.
	/// </summary>
	[TestMethod]
	public void NoneT_EqualsT_throws_for_null_argument() =>
		RunUnitTests(new EqualsT_throws_for_null_argument());

	private sealed class EqualsT_throws_for_null_argument : IUnitTest0Split {
		public void RunTestOnReferenceType<T>() where T : class {
			var none = Option.From<T>(null!);
			// Use 'null!' to simulate call from #nullable disable environment
			var ex = Assert.ThrowsException<ArgumentNullException>(
				() => none.Equals((T)null!));
			Assert.AreEqual("other", ex.ParamName);
		}
		public void RunTestOnValueType<T>() where T : struct {
			// This can only be executed for reference types
		}
	}
}

/// <summary>
///   Unit test for <see cref="None{T}.GetEnumerator"/> methods.
/// </summary>
[TestClass]
public class GetEnumerator_Tests {

	/// <summary>
	///   The <see cref="IEnumerable{T}.GetEnumerator"/> methods return a correct
	///   <see cref="IEnumerator{T}"/> for a <see cref="None{T}"/>.
	/// </summary>
	[TestMethod]
	public void NoneT_GetEnumerator_returns_correct_enumerator() =>
		RunUnitTests(new GetEnumerator_returns_correct_enumerator());

	private sealed class GetEnumerator_returns_correct_enumerator : IUnitTest0Split {
		public void RunTestOnReferenceType<T>() where T : class {
			var none = Option.From<T>(null!);
			var enumerator = none.GetEnumerator();
			Assert.IsInstanceOfType<IEnumerator<T>>(enumerator);
			Assert.IsFalse(enumerator.MoveNext());
		}
		public void RunTestOnValueType<T>() where T : struct {
			var none = Option.From<T>(null!);
			var enumerator = none.GetEnumerator();
			Assert.IsInstanceOfType<IEnumerator<T>>(enumerator);
			Assert.IsFalse(enumerator.MoveNext());
		}
	}

	/// <summary>
	///   The <see cref="IEnumerable{T}.GetEnumerator"/> methods return a correct
	///   <see cref="IEnumerator"/> for a <see cref="None{T}"/> cast to
	///   <see cref="IEnumerable"/>.
	/// </summary>
	[TestMethod]
	public void NoneT_IEnumeratorGetEnumerator_returns_correct_enumerator_for_None_instance() =>
		RunUnitTests(new IEnumeratorGetEnumerator_returns_correct_enumerator_for_None_instance());

	private sealed class IEnumeratorGetEnumerator_returns_correct_enumerator_for_None_instance
			: IUnitTest0Split {
		public void RunTestOnReferenceType<T>() where T : class {
			var none = Option.From<T>(null!);
			var enumerator = ((IEnumerable)none).GetEnumerator();
			Assert.IsInstanceOfType<IEnumerator<T>>(enumerator);
			Assert.IsFalse(enumerator.MoveNext());
		}
		public void RunTestOnValueType<T>() where T : struct {
			var none = Option.From<T>(null!);
			var enumerator = ((IEnumerable)none).GetEnumerator();
			Assert.IsInstanceOfType<IEnumerator<T>>(enumerator);
			Assert.IsFalse(enumerator.MoveNext());
		}
	}
}

/// <summary>
///   Unit tests for <see cref="None{T}"/>.GetHashCode().
/// </summary>
[TestClass]
public class GetHashCode_Tests {

	/// <summary>
	///   The <see cref="object.GetHashCode"/> method returns the correct value.
	/// </summary>
	[TestMethod]
	public void NoneT_GetHashCode_returns_correct_value() =>
		RunUnitTests(new GetHashCode_returns_correct_value());

	private sealed class GetHashCode_returns_correct_value : IUnitTest1Split {
		public void RunTestOnReferenceType<T>(T value) where T : class {
			var none = Option.From<T>(null!);
			var none2 = Option.From<T>(null!);
			var some = Option.FromValue(value);
			Assert.AreEqual(none.GetHashCode(), none2.GetHashCode());
			Assert.AreNotEqual(none.GetHashCode(), some.GetHashCode());
		}
		public void RunTestOnValueType<T>(T value) where T : struct {
			var none = Option.From<T>(null!);
			var none2 = Option.From<T>(null!);
			var some = Option.FromValue(value);
			Assert.AreEqual(none.GetHashCode(), none2.GetHashCode());
			Assert.AreNotEqual(none.GetHashCode(), some.GetHashCode());
		}
	}
}

/// <summary>
///   Unit test for <see cref="IOption{T}.HasValue"/> tests.
/// </summary>
[TestClass]
public class HasValue_Property_Tests {

	/// <summary>
	///   The <see cref="None{T}.HasValue"/> property returns <see langword="false"/>,
	/// </summary>
	[TestMethod]
	public void NoneT_HasValue_returns_false() =>
		RunUnitTests(new HasValue_returns_false());

	private sealed class HasValue_returns_false : IUnitTest0Split {
		public void RunTestOnReferenceType<T>() where T : class {
			var none = Option.From<T>(null!);
			Assert.IsFalse(none.HasValue);
		}
		public void RunTestOnValueType<T>() where T : struct {
			var none = Option.From<T>(null!);
			Assert.IsFalse(none.HasValue);
		}
	}
}

/// <summary>
///   Unit tests for <see cref="None{T}.ToString"/>.
/// </summary>
[TestClass]
public class ToString_Tests {

	/// <summary>
	///   The <see cref="None{T}.ToString"/> methods creates the expected representation.
	/// </summary>
	[TestMethod]
	public void NoneT_ToString_creates_correct_representation_for_NoneTs() =>
		RunUnitTests(new ToString_creates_correct_representation_for_NoneT());

	private sealed class ToString_creates_correct_representation_for_NoneT : IUnitTest0Split {
		public void RunTestOnReferenceType<T>() where T : class {
			var none = Option.From<T>(null!);
			Assert.AreEqual($"None<{typeof(T).Name}>.singleton", none.ToString());
		}
		public void RunTestOnValueType<T>() where T : struct {
			var none = Option.From<T>(null!);
			Assert.AreEqual($"None<{typeof(T).Name}>.singleton", none.ToString());
		}
	}
}

/// <summary>
///   Unit tests for <see cref="Some{T}.TryGetValue(out T)"/>.
/// </summary>
[TestClass]
public class TryGetValue_Tests {

	/// <summary>
	///   The <see cref="None{T}.TryGetValue(out T)"/> method returns false.
	/// </summary>
	[TestMethod]
	public void NoneT_TryGetValue_returns_false_and_default_value() =>
		RunUnitTests(new TryGetValue_returns_false_and_default_value());

	private sealed class TryGetValue_returns_false_and_default_value : IUnitTest0Split {
		public void RunTestOnReferenceType<T>() where T : class {
			var none = Option.From<T>(null!);
			Assert.IsFalse(none.TryGetValue(out var returnedValue));
			Assert.AreEqual(default, returnedValue);
		}
		public void RunTestOnValueType<T>() where T : struct {
			var none = Option.From<T>(null!);
			Assert.IsFalse(none.TryGetValue(out var returnedValue));
			Assert.AreEqual(default, returnedValue);
		}
	}
}
