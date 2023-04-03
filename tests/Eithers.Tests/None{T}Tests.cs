using System.Reflection;

using static Ainsworth.Eithers.Tests.TestRunner;

// Disable SonarLint S2699 because most assertions are in called subroutines.
#pragma warning disable S2699 // Test should include assertions

namespace Ainsworth.Eithers.Tests.NoneTTests;

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
        RunTestCases(new NoneT_Constructors_are_not_public());
    private sealed class NoneT_Constructors_are_not_public : ITestCase0 {
        public void RunTestCase<T>() where T : notnull {
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
///   Unit test for <see cref="None{T}.Equals(Maybe{T})"/> tests.
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Blocker Code Smell", "S2187:TestCases should contain tests",
    Justification = "Tests are in subclasses. This is an audit trail.")]
[TestClass]
public class EqualsMaybeT_Tests {

    /// <summary>
    ///  The <see cref="None{T}.Equals(Maybe{T})"/> method returns <see langword="false"/>
    ///  for other <see cref="Maybe{T}.None"/> values.
    /// </summary>
    [TestMethod]
    public void NoneT_EqualsMaybeT_returns_false_for_NoneT_of_different_type() =>
        RunTestCases(new EqualsMaybeT_returns_false_for_None_of_different_type());
    private sealed class EqualsMaybeT_returns_false_for_None_of_different_type : ITestCase0 {
        public void RunTestCase<T>() where T : notnull =>
            Assert.IsFalse(Maybe<T>.None.Equals(Maybe<bool>.None));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(Maybe{T})"/> method returns <see langword="false"/>
    ///  for <see langword="null"/>.
    /// </summary>
    [TestMethod]
    public void NoneT_EqualsMaybeT_returns_false_for_null() =>
        RunTestCases(new EqualsMaybeT_returns_false_for_null());
    private sealed class EqualsMaybeT_returns_false_for_null : ITestCase0 {
        public void RunTestCase<T>() where T : notnull =>
            Assert.IsFalse(Maybe<T>.None.Equals(null!));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(Maybe{T})"/> method returns <see langword="true"/>
    ///  for the same <see cref="Maybe{T}.None"/>.
    /// </summary>
    [TestMethod]
    public void NoneT_EqualsMaybeT_returns_true_for_NoneT_of_same_type() =>
        RunTestCases(new EqualsMaybeT_returns_true_for_NoneT_of_same_type());
    private sealed class EqualsMaybeT_returns_true_for_NoneT_of_same_type : ITestCase0 {
        public void RunTestCase<T>() where T : notnull =>
            Assert.IsTrue(Maybe<T>.None.Equals(Maybe<T>.None));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(Maybe{T})"/> method returns <see langword="false"/>
    ///  for <see cref="Some{T}"/>.
    /// </summary>
    [TestMethod]
    public void NoneT_EqualsMaybeT_returns_true_for_SomeT() =>
        RunTestCases(new EqualsMaybeT_returns_true_for_SomeT());
    private sealed class EqualsMaybeT_returns_true_for_SomeT : ITestCase1 {
        public void RunTestCase<T>(T value) where T : notnull {
            var some = Maybe.Some(value);
            Assert.IsFalse(Maybe<T>.None.Equals(some));
        }
    }
}

/// <summary>
///   Unit test for <see cref="None{T}"/>.Equals(object) tests.
/// </summary>
[TestClass]
public class NoneT_EqualsObject_Tests {

    /// <summary>
    ///  The <see cref="None{T}.Equals(object)"/> method returns <see langword="false"/>
    ///  for other <see cref="Maybe{T}.None"/> values.
    /// </summary>
    [TestMethod]
    public void NoneT_EqualsObject_returns_false_for_NoneT_of_different_type() =>
        RunTestCases(new EqualsObject_returns_false_for_None_of_different_type());
    private sealed class EqualsObject_returns_false_for_None_of_different_type : ITestCase0 {
        public void RunTestCase<T>() where T : notnull =>
            Assert.IsFalse(Maybe<T>.None.Equals((object)Maybe<bool>.None));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(object)"/> method returns <see langword="false"/>
    ///  for <see langword="null"/>.
    /// </summary>
    [TestMethod]
    public void NoneT_EqualsObject_returns_false_for_null() =>
        RunTestCases(new EqualsObject_returns_false_for_null());
    private sealed class EqualsObject_returns_false_for_null : ITestCase0 {
        public void RunTestCase<T>() where T : notnull =>
            Assert.IsFalse(Maybe<T>.None.Equals((object)null!));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(object)"/> method returns <see langword="true"/>
    ///  for the same <see cref="Maybe{T}.None"/>.
    /// </summary>
    [TestMethod]
    public void NoneT_EqualsObject_returns_true_for_NoneT_of_same_type() =>
        RunTestCases(new EqualsObject_returns_true_for_NoneT_of_same_type());
    private sealed class EqualsObject_returns_true_for_NoneT_of_same_type : ITestCase0 {
        public void RunTestCase<T>() where T : notnull =>
            Assert.IsTrue(Maybe<T>.None.Equals((object)Maybe<T>.None));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(object)"/> method returns <see langword="false"/>
    ///  for <see cref="Some{T}"/>.
    /// </summary>
    [TestMethod]
    public void NoneT_EqualsObject_returns_true_for_SomeT() =>
        RunTestCases(new EqualsObject_returns_true_for_SomeT());
    private sealed class EqualsObject_returns_true_for_SomeT : ITestCase1 {
        public void RunTestCase<T>(T value) where T : notnull {
            var some = Maybe.Some(value);
            Assert.IsFalse(Maybe<T>.None.Equals((object)some));
        }
    }
}

/// <summary>
///   Unit test for <see cref="None{T}.Equals(T)"/> tests.
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Blocker Code Smell", "S2187:TestCases should contain tests",
    Justification = "Tests are in subclasses. This is an audit trail.")]
[TestClass]
public class EqualsT_Tests {

    // TODO: NoneT_EqualsT_returns_false_for_null (reference value)

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="false"/>.
    /// </summary>
    [TestMethod]
    public void NoneT_EqualsT_returns_false_for_value() =>
        RunTestCases(new EqualsT_returns_false_for_null());
    private sealed class EqualsT_returns_false_for_null : ITestCase1 {
        public void RunTestCase<T>(T value) where T : notnull =>
            Assert.IsFalse(Maybe<T>.None.Equals(value));
    }
}

/// <summary>
///   Unit test for <see cref="None{T}.GetEnumerator"/> tests.
/// </summary>
[TestClass]
public class GetEnumerator_Tests {

    /// <summary>
    ///   The <see cref="Maybe{T}.GetEnumerator"/> methods return a correct
    ///   <see cref="IEnumerator{T}"/> for a <see cref="None{T}"/>.
    /// </summary>
    [TestMethod]
    public void NoneT_GetEnumerator_returns_correct_enumerator() =>
        RunTestCases(new GetEnumerator_returns_correct_enumerator());
    private sealed class GetEnumerator_returns_correct_enumerator : ITestCase0 {
        public void RunTestCase<T>() where T : notnull {
            var enumerator = Maybe<T>.None.GetEnumerator();
            Assert.IsInstanceOfType<IEnumerator<T>>(enumerator);
            Assert.IsFalse(enumerator.MoveNext());
        }
    }
}

/// <summary>
///   Unit tests for <see cref="None{T}"/>.GetHashCode().
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
    public void NoneT_GetHashCode_returns_correct_value_for_primitive_types() =>
        RunTestCases(new GetHashCode_returns_correct_value());
    private sealed class GetHashCode_returns_correct_value : ITestCase1 {
        public void RunTestCase<T>(T value) where T : notnull {
            var maybe = Maybe.Some(value);
            Assert.AreEqual(Maybe<T>.None.GetHashCode(), Maybe<T>.None.GetHashCode());
            Assert.AreNotEqual(Maybe<T>.None.GetHashCode(), maybe.GetHashCode());
        }
    }
}

/// <summary>
///   Unit test for <see cref="Maybe{T}.HasValue"/> tests.
/// </summary>
[TestClass]
public class HasValue_Property_Tests {

    /// <summary>
    ///   The <see cref="None{T}.HasValue"/> property returns <see langword="false"/>,
    /// </summary>
    [TestMethod]
    public void NoneT_HasValue_returns_false() =>
        RunTestCases(new HasValue_returns_false());
    private sealed class HasValue_returns_false : ITestCase0 {
        public void RunTestCase<T>() where T : notnull => Assert.IsFalse(Maybe<T>.None.HasValue);
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
    public void MaybeT_ToString_creates_correct_representation_for_NoneTs() =>
        RunTestCases(new ToString_creates_correct_representation_for_NoneT());
    private sealed class ToString_creates_correct_representation_for_NoneT : ITestCase0 {
        public void RunTestCase<T>() where T : notnull =>
            Assert.AreEqual($"Maybe<{typeof(T).Name}>.None", Maybe<T>.None.ToString());
    }
}
