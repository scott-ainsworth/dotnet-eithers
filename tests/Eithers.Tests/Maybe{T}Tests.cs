using System.Collections;
using System.Reflection;

using static Ainsworth.Eithers.Tests.TestRunner;

// Disable SonarLint S2699 because most assertions are in called subroutines.
#pragma warning disable S2699 // Test should include assertions

namespace Ainsworth.Eithers.Tests.MaybeTTests;

/// <summary>
///   Unit tests for <see cref="Maybe{T}"/> casts.
/// </summary>
[TestClass]
public class MaybeT_Constructor_Tests {

    /// <summary>
    ///   The <see cref="Maybe{T}"/> constructors' visibility is not public.
    /// </summary>
    [TestMethod]
    public void MaybeT_constructors_are_protected() =>
        RunTestCases(new MaybeT_Constructors_are_not_public());
    private sealed class MaybeT_Constructors_are_not_public : ITestCase0 {
        public void RunTestCase<T>() where T : notnull {
            var constructors = typeof(Maybe<T>).GetConstructors(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            Assert.IsFalse(constructors.Any(), $"{typeof(T)} has at least 1 public constructor");
        }
    }
}

/// <summary>
///   Unit test for <see cref="Maybe{T}.Equals(Maybe{T})"/> tests.
/// </summary>
/// <remarks>
///   If implemented, these tests would duplicate the <see cref="Some{T}.Equals(Maybe{T})"/>
///   and <see cref="None{T}.Equals(Maybe{T})"/> tests.
/// </remarks>
[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Blocker Code Smell", "S2187:TestCases should contain tests",
    Justification = "Tests are in subclasses.")]
[TestClass]
public class MaybeT_EqualsMaybeT_Tests {
    // Intentially empy
}

/// <summary>
///   Unit test for <see cref="Some{T}"/>.Equals(object) tests.
/// </summary>
/// <remarks>
///   If implemented, these tests would duplicate the <see cref="Some{T}.Equals(object)"/>
///   and <see cref="None{T}.Equals(object)"/> tests.
/// </remarks>
[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Blocker Code Smell", "S2187:TestCases should contain tests",
    Justification = "Tests are in subclasses.")]
[TestClass]
public class MaybeT_EqualsObject_Tests {
    // Intentially empy
}

/// <summary>
///   Unit test for <see cref="Maybe{T}.Equals(T)"/> tests.
/// </summary>
/// <remarks>
///   If implemented, these tests would duplicate the <see cref="Some{T}.Equals(T)"/>
///   and <see cref="None{T}.Equals(T)"/> tests.
/// </remarks>
[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Blocker Code Smell", "S2187:TestCases should contain tests",
    Justification = "Tests are in subclasses.")]
[TestClass]
public class MaybeT_EqualsT_Tests {
    // Intentially empy
}

/// <summary>
///   Unit test for <see cref="Maybe{T}.GetEnumerator"/> tests.
/// </summary>
[TestClass]
public class MaybeT_GetEnumerator_Tests {

    /// <summary>
    ///   The <see cref="Maybe{T}.GetEnumerator"/> methods returns a correct
    ///   <see cref="IEnumerator"/> for a <see cref="Some{T}"/> cast to
    ///   <see cref="IEnumerable"/>.
    /// </summary>
    [TestMethod]
    public void MaybeT_IEnumeratorGetEnumerator_returns_correct_enumerator_for_Some_instance() =>
        RunTestCases(new IEnumeratorGetEnumerator_returns_correct_enumerator_for_Some_instance());
    private sealed class IEnumeratorGetEnumerator_returns_correct_enumerator_for_Some_instance
            : ITestCase1 {
        public void RunTestCase<T>(T value) where T : notnull {
            var enumerator = Maybe.Some(value).GetEnumerator();
            Assert.IsInstanceOfType<IEnumerator>(enumerator);
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(value, enumerator.Current);
            Assert.IsFalse(enumerator.MoveNext());
        }
    }

    /// <summary>
    ///   The <see cref="Maybe{T}.GetEnumerator"/> methods return a correct
    ///   <see cref="IEnumerator"/> for a <see cref="None{T}"/> cast to
    ///   <see cref="IEnumerable"/>.
    /// </summary>
    [TestMethod]
    public void MaybeT_IEnumeratorGetEnumerator_returns_correct_enumerator_for_None_instance() =>
        RunTestCases(new IEnumeratorGetEnumerator_returns_correct_enumerator_for_None_instance());
    private sealed class IEnumeratorGetEnumerator_returns_correct_enumerator_for_None_instance
            : ITestCase0 {
        public void RunTestCase<T>() where T : notnull {
            IEnumerable maybe = Maybe<T>.None;
            var enumerator = maybe.GetEnumerator();
            Assert.IsInstanceOfType<IEnumerator<T>>(enumerator);
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
public class MaybeT_GetHashCode_Tests {

    /// <summary>
    ///   The <see cref="object.GetHashCode"/> method returns the correct value
    ///   for primitive type values.
    /// </summary>
    [TestMethod]
    public void MaybeT_GetHashCode_returns_correct_value_for_primitive_types() =>
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
public class MaybeT_HasValue_Property_Tests {

    /// <summary>
    ///   The <see cref="Maybe{T}.HasValue"/> property returns <see langword="true"/>
    ///   for <see cref="Maybe{T}"/>s with a value (i.e., <see cref="Some{T}"/>s).
    /// </summary>
    [TestMethod]
    public void MaybeT_HasValue_returns_true_for_MaybeT_with_value() {

        var intMaybe = Maybe.From((int?)111);
        Assert.IsInstanceOfType<Maybe<int>>(intMaybe);
        Assert.IsTrue(intMaybe.HasValue);
    }

    /// <summary>
    ///   The <see cref="Maybe{T}.HasValue"/> property returns <see langword="false"/>
    ///   for <see cref="Maybe{T}"/>s without a value (i.e., <see cref="None{T}"/>s).
    /// </summary>
    [TestMethod]
    public void MaybeT_HasValue_returns_false_for_MaybeT_with_value() {

        var maybe = Maybe.From<int>(null);
        Assert.IsInstanceOfType<Maybe<int>>(maybe);
        Assert.IsFalse(maybe.HasValue);
    }
}

/// <summary>
///   Unit tests for <see cref="Maybe{T}"/> casts.
/// </summary>
[TestClass]
public class MaybeT_ToString_Tests {

    /// <summary>
    ///   The <see cref="Some{T}.ToString"/> methods creates the expected reprsentation.
    /// </summary>
    [TestMethod]
    public void MaybeT_ToString_creates_correct_representation_for_SomeTs() =>
        RunTestCases(new ToString_creates_correct_representation_for_SomeT());
    private sealed class ToString_creates_correct_representation_for_SomeT : ITestCase1 {
        public void RunTestCase<T>(T value) where T : notnull =>
            Assert.AreEqual($"Some<{typeof(T).Name}>({value})", Maybe.Some(value).ToString());
    }

    /// <summary>
    ///   The <see cref="None{T}.ToString"/> methods creates the expected reprsentation.
    /// </summary>
    [TestMethod]
    public void MaybeT_ToString_creates_correct_representation_for_NoneTs() =>
        RunTestCases(new ToString_creates_correct_representation_for_NoneT());
    private sealed class ToString_creates_correct_representation_for_NoneT : ITestCase0 {
        public void RunTestCase<T>() where T : notnull =>
            Assert.AreEqual($"Maybe<{typeof(T).Name}>.None", Maybe<T>.None.ToString());
    }
}
