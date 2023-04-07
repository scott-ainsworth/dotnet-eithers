using System.Collections;
using System.Reflection;

using Ainsworth.Eithers;

using TestSupport;
using static TestSupport.TestRunner;

// Disable SonarLint S2699 because most assertions are in called subroutines.
#pragma warning disable S2699 // Test should include assertions

namespace MaybeT_Tests;

/// <summary>
///   Unit tests for <see cref="Maybe{T}"/> casts.
/// </summary>
[TestClass]
public class Constructor_Tests {

    /// <summary>
    ///   The <see cref="Maybe{T}"/> constructors' visibility is not public.
    /// </summary>
    [TestMethod]
    public void MaybeT_constructors_are_protected() =>
        RunUnitTests(new MaybeT_Constructors_are_not_public());

    private sealed class MaybeT_Constructors_are_not_public : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            var constructors = typeof(Maybe<T>).GetConstructors(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            Assert.IsFalse(constructors.Any(), $"{typeof(T)} has at least 1 public constructor");
        }
    }
}

/// <summary>
///   Unit tests for <see cref="Maybe{T}.GetEnumerator"/> tests.
/// </summary>
[TestClass]
public class GetEnumerator_Tests {

    /// <summary>
    ///   The <see cref="Maybe{T}.GetEnumerator"/> methods returns a correct
    ///   <see cref="IEnumerator"/> for a <see cref="Some{T}"/> cast to
    ///   <see cref="IEnumerable"/>.
    /// </summary>
    [TestMethod]
    public void MaybeT_IEnumeratorGetEnumerator_returns_correct_enumerator_for_Some_instance() =>
        RunUnitTests(new IEnumeratorGetEnumerator_returns_correct_enumerator_for_Some_instance());

    private sealed class IEnumeratorGetEnumerator_returns_correct_enumerator_for_Some_instance
            : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var enumerator = Maybe.FromValue(value).GetEnumerator();
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
        RunUnitTests(new IEnumeratorGetEnumerator_returns_correct_enumerator_for_None_instance());

    private sealed class IEnumeratorGetEnumerator_returns_correct_enumerator_for_None_instance
            : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
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
public class GetHashCode_Tests {

    /// <summary>
    ///   The <see cref="object.GetHashCode"/> method returns the correct value
    ///   for primitive type values.
    /// </summary>
    [TestMethod]
    public void MaybeT_GetHashCode_returns_correct_value_for_primitive_types() =>
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
///   Unit tests for <see cref="Maybe{T}"/> casts.
/// </summary>
[TestClass]
public class ToString_Tests {

    /// <summary>
    ///   The <see cref="Some{T}.ToString"/> methods creates the expected reprsentation.
    /// </summary>
    [TestMethod]
    public void MaybeT_ToString_creates_correct_representation_for_SomeTs() =>
        RunUnitTests(new ToString_creates_correct_representation_for_SomeT());

    private sealed class ToString_creates_correct_representation_for_SomeT : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull =>
            Assert.AreEqual($"Some<{typeof(T).Name}>({value})", Maybe.FromValue(value).ToString());
    }

    /// <summary>
    ///   The <see cref="None{T}.ToString"/> methods creates the expected reprsentation.
    /// </summary>
    [TestMethod]
    public void MaybeT_ToString_creates_correct_representation_for_NoneTs() =>
        RunUnitTests(new ToString_creates_correct_representation_for_NoneT());

    private sealed class ToString_creates_correct_representation_for_NoneT : IUnitTest0 {
        public void RunTest<T>() where T : notnull =>
            Assert.AreEqual($"Maybe<{typeof(T).Name}>.None", Maybe<T>.None.ToString());
    }
}
