using System.Collections;
using System.Reflection;

using static Ainsworth.Eithers.Tests.TestRunner;

// Disable SonarLint S2699 because most assertions are in called subroutines.
#pragma warning disable S2699 // Test should include assertions

namespace Ainsworth.Eithers.Tests.SomeTTests;

/// <summary>
///   Unit tests for <see cref="Maybe{T}"/> casts.
/// </summary>
[TestClass]
public class SomeT_Constructor_Tests {

    /// <summary>
    ///   The <see cref="Some{T}"/> constructors' visibility is not public.
    /// </summary>
    [TestMethod]
    public void SomeT_constructors_are_all_protected() =>
        RunTestCases(new SomeT_Constructors_are_not_public());
    private sealed class SomeT_Constructors_are_not_public : ITestCase0 {
        public void RunTestCase<T>() where T : notnull {
            var constructors = typeof(Maybe<T>).GetConstructors(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            Assert.IsFalse(constructors.Any(), $"{typeof(T)} has at least 1 public constructor");
        }
    }
}

/// <summary>
///   Unit test for <see cref="Some{T}.Equals(Maybe{T})"/> tests.
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Blocker Code Smell", "S2187:TestCases should contain tests",
    Justification = "Tests are in subclasses. This is an audit trail.")]
[TestClass]
public class SomeT_EqualsMaybeT_Tests {

    /// <summary>
    ///  The <see cref="Maybe{T}.Equals(Maybe{T})"/> method returns <see langword="false"/>
    ///  not equal values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_false_for_not_equal_values() =>
        RunTestCases(new EqualsMaybeT_returns_false_for_not_equal_values());
    private sealed class EqualsMaybeT_returns_false_for_not_equal_values : ITestCase2 {
        public void RunTestCase<T>(T value, T value2) where T : notnull {
            Maybe<T> maybe = Maybe.Some(value);
            Maybe<T> maybe2 = Maybe.Some(value2);
            Assert.IsFalse(maybe.Equals(maybe2));
        }
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(Maybe{T})"/> method returns <see langword="false"/> None.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_false_None() =>
        RunTestCases(new EqualsMaybeT_returns_false_for_None());
    private sealed class EqualsMaybeT_returns_false_for_None : ITestCase1 {
        public void RunTestCase<T>(T value) where T : notnull {
            var maybe = Maybe.Some(value);
            Assert.IsFalse(maybe.Equals(Maybe<T>.None));
        }
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(Maybe{T})"/> method returns <see langword="false"/>
    ///  for <see langword="null"/>.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_false_for_null() =>
        RunTestCases(new EqualsMaybeT_returns_false_for_null());
    private sealed class EqualsMaybeT_returns_false_for_null : ITestCase1 {
        public void RunTestCase<T>(T value) where T : notnull {
            var maybe = Maybe.Some<T>(value);
            Assert.IsFalse(maybe.Equals(null!));
        }
    }

    /// <summary>
    ///  The <see cref="Maybe{T}.Equals(Maybe{T})"/> method returns <see langword="true"/>
    ///  for equal values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_true_for_equal_values() =>
        RunTestCases(new EqualsMaybeT_returns_true_for_equal_values());
    private sealed class EqualsMaybeT_returns_true_for_equal_values : ITestCase1 {
        public void RunTestCase<T>(T value) where T : notnull {
            Maybe<T> maybe = Maybe.Some(value);
            Maybe<T> maybe2 = Maybe.Some(value);
            Assert.IsTrue(maybe.Equals(maybe2));
        }
    }
}

/// <summary>
///   Unit test for <see cref="Some{T}"/>.Equals(object) tests.
/// </summary>
[TestClass]
public class SomeT_EqualsObject_Tests {

    /// <summary>
    ///  The <see cref="Maybe{T}.Equals(object)"/> method returns <see langword="false"/>
    ///  not equal values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsObject_returns_false_for_not_equal_SomeT_value() =>
        RunTestCases(new EqualsObject_returns_false_for_not_equal_SomeT_value());
    private sealed class EqualsObject_returns_false_for_not_equal_SomeT_value : ITestCase2 {
        public void RunTestCase<T>(T value, T value2) where T : notnull {
            Maybe<T> maybe = Maybe.Some(value);
            Maybe<T> maybe2 = Maybe.Some(value2);
            Assert.IsFalse(maybe.Equals((object)maybe2));
        }
    }

    /// <summary>
    ///  The <see cref="Maybe{T}.Equals(object)"/> method returns <see langword="false"/>
    ///  not equal values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsObject_returns_false_for_not_equal_value() =>
        RunTestCases(new EqualsObject_returns_false_for_not_equal_value());
    private sealed class EqualsObject_returns_false_for_not_equal_value : ITestCase2 {
        public void RunTestCase<T>(T value, T value2) where T : notnull {
            Maybe<T> maybe = Maybe.Some(value);
            Assert.IsFalse(maybe.Equals((object)value2));
        }
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(object)"/> method returns <see langword="false"/> None.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsObject_returns_false_None() =>
        RunTestCases(new EqualsObject_returns_false_for_None());
    private sealed class EqualsObject_returns_false_for_None : ITestCase1 {
        public void RunTestCase<T>(T value) where T : notnull {
            var maybe = Maybe.Some(value);
            Assert.IsFalse(maybe.Equals((Object)Maybe<T>.None));
        }
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(object)"/> method returns <see langword="false"/>
    ///  for <see langword="null"/>.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsObject_returns_false_for_null() =>
        RunTestCases(new EqualsObject_returns_false_for_null());
    private sealed class EqualsObject_returns_false_for_null : ITestCase1 {
        public void RunTestCase<T>(T value) where T : notnull {
            var maybe = Maybe.Some<T>(value);
            Assert.IsFalse(maybe.Equals((Object)null!));
        }
    }

    /// <summary>
    ///  The <see cref="Maybe{T}.Equals(object)"/> method returns <see langword="true"/>
    ///  for equal value wrapped in <see cref="Some{T}"/>.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsObject_returns_true_for_equal_SomeT_value() =>
        RunTestCases(new EqualsObject_returns_true_for_equal_SomeT_value());
    private sealed class EqualsObject_returns_true_for_equal_SomeT_value : ITestCase1 {
        public void RunTestCase<T>(T value) where T : notnull {
            Maybe<T> maybe = Maybe.Some(value);
            Maybe<T> maybe2 = Maybe.Some(value);
            Assert.IsTrue(maybe.Equals((object)maybe2));
        }
    }

    /// <summary>
    ///  The <see cref="Maybe{T}.Equals(object)"/> method returns <see langword="true"/>
    ///  for equal values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsObject_returns_true_for_equal_value() =>
        RunTestCases(new EqualsObject_returns_true_for_equal_value());
    private sealed class EqualsObject_returns_true_for_equal_value : ITestCase1 {
        public void RunTestCase<T>(T value) where T : notnull {
            Maybe<T> maybe = Maybe.Some(value);
            Assert.IsTrue(maybe.Equals((object)value));
        }
    }
}

/// <summary>
///   Unit test for <see cref="Maybe{T}.Equals(T)"/> tests.
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Blocker Code Smell", "S2187:TestCases should contain tests",
    Justification = "Tests are in subclasses. This is an audit trail.")]
[TestClass]
public class SomeT_EqualsT_Tests {

    /// <summary>
    ///  The <see cref="Maybe{T}.Equals(T)"/> method returns <see langword="false"/>
    ///  not equal values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsT_returns_false_for_not_equal_values() =>
        RunTestCases(new EqualsT_returns_false_for_not_equal_values());
    private sealed class EqualsT_returns_false_for_not_equal_values : ITestCase2 {
        public void RunTestCase<T>(T value, T value2) where T : notnull {
            Maybe<T> maybe = Maybe.Some(value);
            Assert.IsFalse(maybe.Equals(value2));
        }
    }

    /// <summary>
    ///  The <see cref="Maybe{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for equal values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsT_returns_true_for_equal_values() =>
        RunTestCases(new EqualsT_returns_true_for_equal_values());
    private sealed class EqualsT_returns_true_for_equal_values : ITestCase1 {
        public void RunTestCase<T>(T value) where T : notnull {
            Maybe<T> maybe = Maybe.Some(value);
            Assert.IsTrue(maybe.Equals(value));
        }
    }
}

/// <summary>
///   Unit test for <see cref="Maybe{T}.GetEnumerator"/> tests.
/// </summary>
[TestClass]
public class SomeT_GetEnumerator_Tests {

    /// <summary>
    ///   The <see cref="Maybe{T}.GetEnumerator"/> methods return a correct
    ///   <see cref="IEnumerator{T}"/> for a <see cref="Some{T}"/>.
    /// </summary>
    [TestMethod]
    public void SomeT_GetEnumerator_returns_correct_enumerator() =>
        RunTestCases(new GetEnumerator_returns_correct_enumerator());
    private sealed class GetEnumerator_returns_correct_enumerator : ITestCase1 {
        public void RunTestCase<T>(T value) where T : notnull {
            var enumerator = Maybe.Some(value).GetEnumerator();
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
public class SomeT_GetHashCode_Tests {

    /// <summary>
    ///   The <see cref="object.GetHashCode"/> method returns the correct value
    ///   for primitive type values.
    /// </summary>
    [TestMethod]
    public void SomeT_GetHashCode_returns_correct_value() =>
        RunTestCases(new GetHashCode_returns_correct_value());
    private sealed class GetHashCode_returns_correct_value : ITestCase2 {
        public void RunTestCase<T>(T value, T value2) where T : notnull {
            var maybe = Maybe.Some(value);
            Assert.AreEqual(value.GetHashCode(), maybe.GetHashCode());
            Assert.AreNotEqual(value2.GetHashCode(), maybe.GetHashCode());
        }
    }
}

/// <summary>
///   Unit test for <see cref="Maybe{T}.HasValue"/> tests.
/// </summary>
[TestClass]
public class SomeT_HasValue_Property_Tests {

    /// <summary>
    ///   The <see cref="Some{T}.HasValue"/> property returns <see langword="true"/>.
    /// </summary>
    [TestMethod]
    public void SomeT_HasValue_returns_true() =>
        RunTestCases(new HasValue_returns_false());
    private sealed class HasValue_returns_false : ITestCase1 {
        public void RunTestCase<T>(T value) where T : notnull {
            var maybe = Maybe.Some(value);
            Assert.IsTrue(maybe.HasValue);
        }
    }
}

/// <summary>
///   Unit tests for <see cref="Maybe{T}"/> casts.
/// </summary>
[TestClass]
public class SomeT_ToString_Tests {

    /// <summary>
    ///   The <see cref="Some{T}.ToString"/> method creates the expected reprsentation.
    /// </summary>
    [TestMethod]
    public void MaybeT_ToString_creates_correct_representation_for_SomeTs() =>
        RunTestCases(new ToString_creates_correct_representation_for_SomeT());
    private sealed class ToString_creates_correct_representation_for_SomeT : ITestCase1 {
        public void RunTestCase<T>(T value) where T : notnull =>
            Assert.AreEqual($"Some<{typeof(T).Name}>({value})", Maybe.Some(value).ToString());
    }
}
