using System.Reflection;

using Ainsworth.Eithers;

using TestSupport;
using static TestSupport.TestRunner;

// Disable SonarLint S2699 because most assertions are in called subroutines.
#pragma warning disable S2699 // Test should include assertions

namespace NoneT_Tests;

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
///   Unit test for <see cref="None{T}.Equals(Maybe{T})"/> tests.
/// </summary>
[TestClass]
public class EqualsMaybeT_Tests {

    /// <summary>
    ///  The <see cref="None{T}.Equals(Maybe{T})"/> method returns <see langword="false"/>
    ///  for other <see cref="Maybe{T}.None"/> values.
    /// </summary>
    [TestMethod]
    public void NoneT_EqualsMaybeT_returns_false_for_NoneT_of_different_type() =>
        RunUnitTests(new EqualsMaybeT_returns_false_for_None_of_different_type());

    private sealed class EqualsMaybeT_returns_false_for_None_of_different_type : IUnitTest0 {
        public void RunTest<T>() where T : notnull =>
            Assert.IsFalse(Maybe<T>.None.Equals(Maybe<bool>.None));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(Maybe{T})"/> method throws a
    ///  <see cref="ArgumentNullException"/> for a <see langword="null"/> argument.
    /// </summary>
    /// <remarks>
    ///   This can only happen if called from a "#nullable disable" environment.
    /// </remarks>
    [TestMethod]
    public void NoneT_EqualsMaybeT_returns_throws_for_null_argument() =>
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
    ///  The <see cref="None{T}.Equals(Maybe{T})"/> method returns <see langword="true"/>
    ///  for the same <see cref="Maybe{T}.None"/>.
    /// </summary>
    [TestMethod]
    public void NoneT_EqualsMaybeT_returns_true_for_NoneT_of_same_type() =>
        RunUnitTests(new EqualsMaybeT_returns_true_for_NoneT_of_same_type());

    private sealed class EqualsMaybeT_returns_true_for_NoneT_of_same_type : IUnitTest0 {
        public void RunTest<T>() where T : notnull =>
            Assert.IsTrue(Maybe<T>.None.Equals(Maybe<T>.None));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(Maybe{T})"/> method returns <see langword="false"/>
    ///  for <see cref="Some{T}"/>.
    /// </summary>
    [TestMethod]
    public void NoneT_EqualsMaybeT_returns_true_for_SomeT() =>
        RunUnitTests(new EqualsMaybeT_returns_true_for_SomeT());

    private sealed class EqualsMaybeT_returns_true_for_SomeT : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var some = Maybe.FromValue(value);
            Assert.IsFalse(Maybe<T>.None.Equals(some));
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
    ///  for other <see cref="Maybe{T}.None"/> values.
    /// </summary>
    [TestMethod]
    public void NoneT_EqualsObject_returns_false_for_NoneT_of_different_type() =>
        RunUnitTests(new EqualsObject_returns_false_for_None_of_different_type());

    private sealed class EqualsObject_returns_false_for_None_of_different_type : IUnitTest0 {
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Style", "IDE0004:Remove Unnecessary Cast",
            Justification = "Cast ensures desired instance of Equals is used.")]
        public void RunTest<T>() where T : notnull =>
            Assert.IsFalse(Maybe<T>.None.Equals((object)Maybe<bool>.None));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(object)"/> method returns <see langword="false"/>
    ///  for <see langword="null"/>.
    /// </summary>
    [TestMethod]
    public void NoneT_EqualsObject_returns_false_for_null_argument() =>
        RunUnitTests(new EqualsObject_returns_false_for_null_argument());

    private sealed class EqualsObject_returns_false_for_null_argument : IUnitTest0 {
        public void RunTest<T>() where T : notnull =>
            Assert.IsFalse(Maybe<T>.None.Equals((object)null!));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(object)"/> method returns <see langword="true"/>
    ///  for the same <see cref="Maybe{T}.None"/>.
    /// </summary>
    [TestMethod]
    public void NoneT_EqualsObject_returns_true_for_NoneT_of_same_type() =>
        RunUnitTests(new EqualsObject_returns_true_for_NoneT_of_same_type());

    private sealed class EqualsObject_returns_true_for_NoneT_of_same_type : IUnitTest0 {
        public void RunTest<T>() where T : notnull =>
            Assert.IsTrue(Maybe<T>.None.Equals((object)Maybe<T>.None));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(object)"/> method returns <see langword="false"/>
    ///  for <see cref="Some{T}"/>.
    /// </summary>
    [TestMethod]
    public void NoneT_EqualsObject_returns_true_for_SomeT() =>
        RunUnitTests(new EqualsObject_returns_true_for_SomeT());

    private sealed class EqualsObject_returns_true_for_SomeT : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var some = Maybe.FromValue(value);
            Assert.IsFalse(Maybe<T>.None.Equals((object)some));
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
    ///  for other <see cref="Maybe{T}.None"/> values.
    /// </summary>
    [TestMethod]
    public void NoneT_EqualsNoneT_returns_false_for_NoneT_of_different_type() =>
        RunUnitTests(new EqualsNoneT_returns_false_for_None_of_different_type());

    private sealed class EqualsNoneT_returns_false_for_None_of_different_type : IUnitTest0 {
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Style", "IDE0004:Remove Unnecessary Cast",
            Justification = "Cast ensures desired instance of Equals is used.")]
        public void RunTest<T>() where T : notnull {
            Assert.IsFalse(Maybe<T>.None.Equals(Maybe<bool>.None));
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

    private sealed class EqualsNoneT_returns_throws_for_null_argument : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            // Use 'null!' to simulate call from '#nullable disable' environment
            var ex = Assert.ThrowsException<ArgumentNullException>(
                () => Maybe<T>.None.Equals((None<T>)null!));
            Assert.AreEqual("other", ex.ParamName);
        }
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(None{T})"/> method returns <see langword="true"/>
    ///  for the same <see cref="Maybe{T}.None"/>.
    /// </summary>
    [TestMethod]
    public void NoneT_EqualsNoneT_returns_true_for_NoneT_of_same_type() =>
        RunUnitTests(new EqualsNoneT_returns_true_for_NoneT_of_same_type());

    private sealed class EqualsNoneT_returns_true_for_NoneT_of_same_type : IUnitTest0 {
        public void RunTest<T>() where T : notnull =>
            Assert.IsTrue(Maybe<T>.None.Equals((None<T>)Maybe<T>.None));
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
    public void NoneT_EqualsSomeT_returns_false_for_NoneT_of_different_type() =>
        RunUnitTests(new EqualsSomeT_returns_false_for_None_of_different_type());

    private sealed class EqualsSomeT_returns_false_for_None_of_different_type : IUnitTest1 {
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Style", "IDE0004:Remove Unnecessary Cast",
            Justification = "Cast ensures desired instance of Equals is used.")]
        public void RunTest<T>(T value) where T : notnull {
            var some = Maybe.FromValue(value);
            Assert.IsFalse(Maybe<T>.None.Equals(some));
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

    private sealed class EqualsSomeT_returns_throws_for_null_argument : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            // Use 'null!' to simulate call from '#nullable disable' environment
            var ex = Assert.ThrowsException<ArgumentNullException>(
                () => Maybe<T>.None.Equals((Some<T>)null!));
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

    private sealed class EqualsT_returns_false_for_value : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull =>
            // Use 'null!' to simulate call from #nullable disable environment
            Assert.IsFalse(Maybe<T>.None.Equals(value));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="false"/>.
    /// </summary>
    [TestMethod]
    public void NoneT_EqualsT_throws_for_null_argument() =>
        RunUnitTests(new EqualsT_throws_for_null_argument());

    private sealed class EqualsT_throws_for_null_argument : IUnitTest0Split {
        public void RunTestOnReferenceType<T>() where T : class {
            // Use 'null!' to simulate call from #nullable disable environment
            var ex = Assert.ThrowsException<ArgumentNullException>(
                () => Maybe<T>.None.Equals((T)null!));
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
    ///   The <see cref="Maybe{T}.GetEnumerator"/> methods return a correct
    ///   <see cref="IEnumerator{T}"/> for a <see cref="None{T}"/>.
    /// </summary>
    [TestMethod]
    public void NoneT_GetEnumerator_returns_correct_enumerator() =>
        RunUnitTests(new GetEnumerator_returns_correct_enumerator());

    private sealed class GetEnumerator_returns_correct_enumerator : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
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
        RunUnitTests(new GetHashCode_returns_correct_value());

    private sealed class GetHashCode_returns_correct_value : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var maybe = Maybe.FromValue(value);
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
        RunUnitTests(new HasValue_returns_false());

    private sealed class HasValue_returns_false : IUnitTest0 {
        public void RunTest<T>() where T : notnull => Assert.IsFalse(Maybe<T>.None.HasValue);
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

    private sealed class ToString_creates_correct_representation_for_NoneT : IUnitTest0 {
        public void RunTest<T>() where T : notnull =>
            Assert.AreEqual($"Maybe<{typeof(T).Name}>.None", Maybe<T>.None.ToString());
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

    private sealed class TryGetValue_returns_false_and_default_value : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            Assert.IsFalse(Maybe<T>.None.TryGetValue(out var returnedValue));
            Assert.AreEqual(default, returnedValue);
        }
    }
}
