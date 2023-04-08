using System.Collections;
using System.Reflection;

using Ainsworth.Eithers;

using TestSupport;
using static TestSupport.TestRunner;

// Disable SonarLint S2699 because most assertions are in called subroutines.
#pragma warning disable S2699 // Test should include assertions

namespace SomeT_Tests;

/// <summary>
///   Unit tests for <see cref="Maybe{T}"/> casts.
/// </summary>
[TestClass]
public class Constructor_Tests {

    /// <summary>
    ///   The <see cref="Some{T}"/> constructors' visibility is not public.
    /// </summary>
    [TestMethod]
    public void SomeT_constructors_are_all_protected() =>
        RunUnitTests(new SomeT_Constructors_are_not_public());

    private sealed class SomeT_Constructors_are_not_public : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            var constructors = typeof(Maybe<T>).GetConstructors(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            Assert.IsFalse(constructors.Any(), $"{typeof(T)} has at least 1 public constructor");
        }
    }

    /// <summary>
    ///   The <see cref="Some{T}"/> constructor throws on <see langword="null"/> value.
    /// </summary>
    [TestMethod]
    public void SomeT_constructor_throws_on_null() =>
        RunUnitTests(new Constructor_throws_on_null());

    private sealed class Constructor_throws_on_null : IUnitTest0Split {
        public void RunTestOnReferenceType<T>() where T : class {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => Maybe.FromValue((T)null!));
            Assert.AreEqual("value", ex.ParamName);
        }
        public void RunTestOnValueType<T>() where T : struct {
            // This is not allowed by C# semantics
        }
    }
}

/// <summary>
///   Unit test for <see cref="Some{T}.Equals(Maybe{T})"/> tests.
/// </summary>
[TestClass]
public class EqualsMaybeT_Tests {

    /// <summary>
    ///  The <see cref="Maybe{T}.Equals(Maybe{T})"/> method returns <see langword="false"/>
    ///  not equal values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_false_for_not_equal_values() =>
        RunUnitTests(new EqualsMaybeT_returns_false_for_not_equal_values());

    private sealed class EqualsMaybeT_returns_false_for_not_equal_values : IUnitTest2 {
        public void RunTest<T>(T value, T value2) where T : notnull {
            var some = Maybe.FromValue(value);
            var some2 = Maybe.FromValue(value2);
            Assert.IsFalse(some.Equals((Maybe<T>)some2));
        }
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(Maybe{T})"/> method returns <see langword="false"/> None.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_false_None() =>
        RunUnitTests(new EqualsMaybeT_returns_false_for_None());

    private sealed class EqualsMaybeT_returns_false_for_None : IUnitTest1 {
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Minor Code Smell", "S1905:Redundant casts should not be used",
            Justification = "Cast is necessary to force correct Equals() calls")]
        public void RunTest<T>(T value) where T : notnull {
            var some = Maybe.FromValue(value);
            Assert.IsFalse(some.Equals((Maybe<T>)Maybe<T>.None));
        }
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(Maybe{T})"/> method throws a
    ///  <see cref="ArgumentNullException"/> for a <see langword="null"/> argument.
    /// </summary>
    /// <remarks>
    ///   This can only happen if called from a "#nullable disable" environment.
    /// </remarks>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_throws_for_null_argument() =>
        RunUnitTests(new EqualsMaybeT_returns_throws_for_null_argument());

    private sealed class EqualsMaybeT_returns_throws_for_null_argument : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            // Use 'null!' to simulate call from '#nullable disable' environment
            var ex = Assert.ThrowsException<ArgumentNullException>(
                () => Maybe<T>.None.Equals((Maybe<T>)null!));
            Assert.AreEqual("other", ex.ParamName);
        }
    }

    /// <summary>
    ///  The <see cref="Maybe{T}.Equals(Maybe{T})"/> method returns <see langword="true"/>
    ///  for equal values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_true_for_equal_values() =>
        RunUnitTests(new EqualsMaybeT_returns_true_for_equal_values());

    private sealed class EqualsMaybeT_returns_true_for_equal_values : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var some = Maybe.FromValue(value);
            var some2 = Maybe.FromValue(value);
            Assert.IsTrue(some.Equals((Maybe<T>)some2));
        }
    }
}

/// <summary>
///   Unit test for <see cref="Some{T}.Equals(None{T})"/> tests.
/// </summary>
[TestClass]
public class EqualsNoneT_Tests {

    /// <summary>
    ///  The <see cref="Some{T}.Equals(None{T})"/> method returns <see langword="false"/>.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsNoneT_returns_false() =>
        RunUnitTests(new EqualsMaybeT_returns_false());

    private sealed class EqualsMaybeT_returns_false : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            Maybe<T> maybe = Maybe.FromValue(value);
            Assert.IsFalse(maybe.Equals((None<T>)Maybe<T>.None));
        }
    }
}

/// <summary>
///   Unit test for <see cref="Some{T}"/>.Equals(object) tests.
/// </summary>
[TestClass]
public class EqualsObject_Tests {

    /// <summary>
    ///  The <see cref="Maybe{T}.Equals(object)"/> method returns <see langword="false"/>
    ///  not equal values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsObject_returns_false_for_not_equal_SomeT_value() =>
        RunUnitTests(new EqualsObject_returns_false_for_not_equal_SomeT_value());

    private sealed class EqualsObject_returns_false_for_not_equal_SomeT_value : IUnitTest2 {
        public void RunTest<T>(T value, T value2) where T : notnull {
            Maybe<T> maybe = Maybe.FromValue(value);
            Maybe<T> maybe2 = Maybe.FromValue(value2);
            Assert.IsFalse(maybe.Equals((object)maybe2));
        }
    }

    /// <summary>
    ///  The <see cref="Maybe{T}.Equals(object)"/> method returns <see langword="false"/>
    ///  not equal values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsObject_returns_false_for_not_equal_value() =>
        RunUnitTests(new EqualsObject_returns_false_for_not_equal_value());

    private sealed class EqualsObject_returns_false_for_not_equal_value : IUnitTest2 {
        public void RunTest<T>(T value, T value2) where T : notnull {
            Maybe<T> maybe = Maybe.FromValue(value);
            Assert.IsFalse(maybe.Equals((object)value2));
        }
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(object)"/> method returns <see langword="false"/> None.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsObject_returns_false_None() =>
        RunUnitTests(new EqualsObject_returns_false_for_None());

    private sealed class EqualsObject_returns_false_for_None : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var maybe = Maybe.FromValue(value);
            Assert.IsFalse(maybe.Equals((object)Maybe<T>.None));
        }
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(object)"/> method returns <see langword="false"/>
    ///  for <see langword="null"/>.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsObject_returns_false_for_null_argument() =>
        RunUnitTests(new EqualsObject_returns_false_for_null_argument());

    private sealed class EqualsObject_returns_false_for_null_argument : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var maybe = Maybe.FromValue(value);
            Assert.IsFalse(maybe.Equals((object)null!));
        }
    }

    /// <summary>
    ///  The <see cref="Maybe{T}.Equals(object)"/> method returns <see langword="true"/>
    ///  for equal value wrapped in <see cref="Some{T}"/>.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsObject_returns_true_for_equal_SomeT_value() =>
        RunUnitTests(new EqualsObject_returns_true_for_equal_SomeT_value());

    private sealed class EqualsObject_returns_true_for_equal_SomeT_value : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            Maybe<T> maybe = Maybe.FromValue(value);
            Maybe<T> maybe2 = Maybe.FromValue(value);
            Assert.IsTrue(maybe.Equals((object)maybe2));
        }
    }

    /// <summary>
    ///  The <see cref="Maybe{T}.Equals(object)"/> method returns <see langword="true"/>
    ///  for equal values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsObject_returns_true_for_equal_value() =>
        RunUnitTests(new EqualsObject_returns_true_for_equal_value());

    private sealed class EqualsObject_returns_true_for_equal_value : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            Maybe<T> maybe = Maybe.FromValue(value);
            Assert.IsTrue(maybe.Equals((object)value));
        }
    }
}

/// <summary>
///   Unit test for <see cref="Some{T}.Equals(Some{T})"/> tests.
/// </summary>
[TestClass]
public class EqualsSomeT_Tests {

    /// <summary>
    ///  The <see cref="Maybe{T}.Equals(Some{T})"/> method returns <see langword="false"/>
    ///  not equal values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsSomeT_returns_false_for_not_equal_values() =>
        RunUnitTests(new EqualsSomeT_returns_false_for_not_equal_values());

    private sealed class EqualsSomeT_returns_false_for_not_equal_values : IUnitTest2 {
        public void RunTest<T>(T value, T value2) where T : notnull {
            var some = Maybe.FromValue(value);
            var some2 = Maybe.FromValue(value2);
            Assert.IsFalse(some.Equals(some2));
        }
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(Some{T})"/> method returns <see langword="false"/> None.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsSomeT_returns_false_None() =>
        RunUnitTests(new EqualsSomeT_returns_false_for_None());

    private sealed class EqualsSomeT_returns_false_for_None : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var some = Maybe.FromValue(value);
            Assert.IsFalse(some.Equals(Maybe<T>.None));
        }
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(Some{T})"/> method throws a
    ///  <see cref="ArgumentNullException"/> for a <see langword="null"/> argument.
    /// </summary>
    /// <remarks>
    ///   This can only happen if called from a "#nullable disable" environment.
    /// </remarks>
    [TestMethod]
    public void SomeT_EqualsSomeT_returns_throws_for_null_argument() =>
        RunUnitTests(new EqualsSomeT_returns_throws_for_null_argument());

    private sealed class EqualsSomeT_returns_throws_for_null_argument : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            // Use 'null!' to simulate call from '#nullable disable' environment
            var ex = Assert.ThrowsException<ArgumentNullException>(
                () => Maybe<T>.None.Equals((Some<T>)null!));
            Assert.AreEqual("other", ex.ParamName);
        }
    }

    /// <summary>
    ///  The <see cref="Maybe{T}.Equals(Some{T})"/> method returns <see langword="true"/>
    ///  for equal values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsSomeT_returns_true_for_equal_values() =>
        RunUnitTests(new EqualsSomeT_returns_true_for_equal_values());

    private sealed class EqualsSomeT_returns_true_for_equal_values : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var some = Maybe.FromValue(value);
            var some2 = Maybe.FromValue(value);
            Assert.IsTrue(some.Equals(some2));
        }
    }
}

/// <summary>
///   Unit test for <see cref="Maybe{T}.Equals(T)"/> tests.
/// </summary>
[TestClass]
public class EqualsT_Tests {

    /// <summary>
    ///  The <see cref="Maybe{T}.Equals(T)"/> method returns <see langword="false"/>
    ///  not equal values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsT_returns_false_for_not_equal_values() =>
        RunUnitTests(new EqualsT_returns_false_for_not_equal_values());

    private sealed class EqualsT_returns_false_for_not_equal_values : IUnitTest2 {
        public void RunTest<T>(T value, T value2) where T : notnull {
            Maybe<T> maybe = Maybe.FromValue(value);
            Assert.IsFalse(maybe.Equals(value2));
        }
    }

    /// <summary>
    ///  The <see cref="Maybe{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for equal values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsT_returns_true_for_equal_values() =>
        RunUnitTests(new EqualsT_returns_true_for_equal_values());

    private sealed class EqualsT_returns_true_for_equal_values : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            Maybe<T> maybe = Maybe.FromValue(value);
            Assert.IsTrue(maybe.Equals(value));
        }
    }
}

/// <summary>
///   Unit test for <see cref="Maybe{T}.GetEnumerator"/> tests.
/// </summary>
[TestClass]
public class GetEnumerator_Tests {

    /// <summary>
    ///   The <see cref="Maybe{T}.GetEnumerator"/> methods return a correct
    ///   <see cref="IEnumerator{T}"/> for a <see cref="Some{T}"/>.
    /// </summary>
    [TestMethod]
    public void SomeT_GetEnumerator_returns_correct_enumerator() =>
        RunUnitTests(new GetEnumerator_returns_correct_enumerator());

    private sealed class GetEnumerator_returns_correct_enumerator : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var enumerator = Maybe.FromValue(value).GetEnumerator();
            Assert.IsInstanceOfType<IEnumerator>(enumerator);
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(value, enumerator.Current);
            Assert.IsFalse(enumerator.MoveNext());
        }
    }
}

/// <summary>
///   Unit tests for <see cref="Maybe{T}"/>.GetHashCode().
/// </summary>
/// <remarks>
///   <see cref="Maybe{T}"/> does not override <see cref="object.GetHashCode"/>; however,
///   <see cref="Some{T}"/> and <see cref="None{T}"/> do.  These test cases show that
///   the <see cref="object.GetHashCode"/> overrides calculate the expected hash code.
/// </remarks>
[TestClass]
public class GetHashCode_Tests {

    /// <summary>
    ///   The <see cref="object.GetHashCode"/> method returns the correct value
    ///   for primitive type values.
    /// </summary>
    [TestMethod]
    public void SomeT_GetHashCode_returns_correct_value() =>
        RunUnitTests(new GetHashCode_returns_correct_value());

    private sealed class GetHashCode_returns_correct_value : IUnitTest2 {
        public void RunTest<T>(T value, T value2) where T : notnull {
            var maybe = Maybe.FromValue(value);
            Assert.AreEqual(value.GetHashCode(), maybe.GetHashCode());
            Assert.AreNotEqual(value2.GetHashCode(), maybe.GetHashCode());
        }
    }
}

/// <summary>
///   Unit test for <see cref="Maybe{T}.HasValue"/> tests.
/// </summary>
[TestClass]
public class HasValue_Property_Tests {

    /// <summary>
    ///   The <see cref="Some{T}.HasValue"/> property returns <see langword="true"/>.
    /// </summary>
    [TestMethod]
    public void SomeT_HasValue_returns_true() =>
        RunUnitTests(new HasValue_returns_false());

    private sealed class HasValue_returns_false : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var maybe = Maybe.FromValue(value);
            Assert.IsTrue(maybe.HasValue);
        }
    }
}

/// <summary>
///   Unit tests for <see cref="Some{T}.ToString()"/>.
/// </summary>
[TestClass]
public class ToString_Tests {

    /// <summary>
    ///   The <see cref="Some{T}.ToString"/> method creates the expected reprsentation.
    /// </summary>
    [TestMethod]
    public void SomeT_ToString_creates_correct_representation_for_SomeTs() =>
        RunUnitTests(new ToString_creates_correct_representation_for_SomeT());

    private sealed class ToString_creates_correct_representation_for_SomeT : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull =>
            Assert.AreEqual($"Some<{typeof(T).Name}>({value})", Maybe.FromValue(value).ToString());
    }
}

/// <summary>
///   Unit tests for <see cref="Some{T}.TryGetValue(out T)"/>.
/// </summary>
[TestClass]
public class TryGetValue_Tests {

    /// <summary>
    ///   The <see cref="Some{T}.TryGetValue(out T)"/> method returns true and the wrapped value.
    /// </summary>
    [TestMethod]
    public void SomeT_TryGetValue_returns_true_and_correct_value() =>
        RunUnitTests(new TryGetValue_returns_true_and_correct_value());

    private sealed class TryGetValue_returns_true_and_correct_value : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var some = Maybe.FromValue(value);
            Assert.IsTrue(some.TryGetValue(out var returnedValue));
            Assert.AreEqual(value, returnedValue);
        }
    }
}
