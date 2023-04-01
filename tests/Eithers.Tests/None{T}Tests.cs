using System.Reflection;

using Ainsworth.Eithers;
using static Ainsworth.Eithers.Tests.TestData;

namespace NoneT_Tests;

/// <summary>
///   Unit tests for the <see cref="None{T}"/> constructors.
/// </summary>
[TestClass]
public class Constructor_Tests {

    /// <summary>
    ///   The <see cref="None{T}"/> constructors' visibility is not public.
    /// </summary>
    [TestMethod]
    public void NoneT_constructors_are_private() {
        var type = typeof(None<int>);
        var constructors = type.GetConstructors(
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.Static);
        foreach (var constructor in constructors) {
            Assert.IsTrue(
                constructor.IsPrivate,
                $"{constructor.DeclaringType!.Name} has at least 1 public constructor");
        }
    }
}

/// <summary>
///   Unit tests for the <see cref="None{T}.Equals(T)"/> methods.
/// </summary>
[TestClass]
public class EqualsT_Method_Tests {

    private readonly Maybe<int> none = Maybe<int>.None;

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="false"/>
    ///  for primitive type values.
    /// </summary>
    [TestMethod]
    public void NoneT_Equals_returns_false_for_primitive_type_values() {
        Assert.IsFalse(none.Equals(TestEnum.E11));
        Assert.IsFalse(none.Equals(111));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="false"/>
    ///  for value type values.
    /// </summary>
    [TestMethod]
    public void NoneT_Equals_returns_false_for_value_types_values() {
        Assert.IsFalse(none.Equals((111, "111")));
        Assert.IsFalse(none.Equals((decimal)111));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="false"/>
    ///  for reference type values.
    /// </summary>
    [TestMethod]
    public void NoneT_Equals_returns_false_for_reference_types_values() {
        Assert.IsFalse(none.Equals("111"));
        Assert.IsFalse(none.Equals(new int[] { 111 }));
        Assert.IsFalse(none.Equals(new TestClass(111, "111")));
    }
}

/// <summary>
///   Unit tests for the <see cref="None{T}.Equals(Maybe{T})"/> methods.
/// </summary>
[TestClass]
public class EqualsMaybeT_Method_Tests {

    /// <summary>
    ///   The <see cref="None{T}.Equals(Maybe{T})"/> method returns <see langword="true"/> for
    ///   <see cref="None{T}"/> of the same type.
    /// </summary>
    [TestMethod]
    public void NoneT_Equals_returns_true_for_NoneT_of_same_type() {
        Assert.IsTrue(Maybe<TestEnum>.None.Equals(Maybe<TestEnum>.None));
        Assert.IsTrue(Maybe<int>.None.Equals(Maybe<int>.None));
        Assert.IsTrue(Maybe<(int, string)>.None.Equals(Maybe<(int, string)>.None));
        Assert.IsTrue(Maybe<decimal>.None.Equals(Maybe<decimal>.None));
        Assert.IsTrue(Maybe<string>.None.Equals(Maybe<string>.None));
        Assert.IsTrue(Maybe<int[]>.None.Equals(Maybe<int[]>.None));
        Assert.IsTrue(Maybe<TestClass>.None.Equals(Maybe<TestClass>.None));
    }

    /// <summary>
    ///   The <see cref="None{T}.Equals(Maybe{T})"/> method returns <see langword="false"/> for
    ///   <see cref="None{T}"/> of the different types.
    /// </summary>
    [TestMethod]
    public void NoneT_Equals_returns_false_for_NoneT_of_different_types() {
        Assert.IsFalse(Maybe<TestEnum>.None.Equals(Maybe<int>.None));
        Assert.IsFalse(Maybe<int>.None.Equals(Maybe<TestEnum>.None));
        Assert.IsFalse(Maybe<(int, string)>.None.Equals(Maybe<decimal>.None));
        Assert.IsFalse(Maybe<decimal>.None.Equals(Maybe<(int, string)>.None));
        Assert.IsFalse(Maybe<string>.None.Equals(Maybe<int[]>.None));
        Assert.IsFalse(Maybe<int[]>.None.Equals(Maybe<TestClass>.None));
        Assert.IsFalse(Maybe<TestClass>.None.Equals(Maybe<string>.None));
    }

    /// <summary>
    ///   The <see cref="None{T}.Equals(Maybe{T})"/> method returns <see langword="false"/> for
    ///   <see cref="Some{T}"/>s.
    /// </summary>
    [TestMethod]
    public void NoneT_Equals_returns_false_for_SomeT_wrapping_primitive_types() {
        Assert.IsFalse(Maybe<TestEnum>.None.Equals(Maybe.Some(TestEnum.E11)));
        Assert.IsFalse(Maybe<int>.None.Equals(Maybe.Some(111)));
        Assert.IsFalse(Maybe<(int, string)>.None.Equals(Maybe.Some((111, "111"))));
        Assert.IsFalse(Maybe<decimal>.None.Equals(Maybe.Some((decimal)111)));
        Assert.IsFalse(Maybe<string>.None.Equals(Maybe.Some("111")));
        Assert.IsFalse(Maybe<int[]>.None.Equals(Maybe.Some(new int[] { 111 })));
        Assert.IsFalse(Maybe<TestClass>.None.Equals(Maybe.Some(new TestClass(111, "111"))));
    }
}

/// <summary>
///   Unit tests for the <see cref="Some{T}.GetEnumerator"/> method.
/// </summary>
[TestClass]
public class GetEnumerator_Method_Tests {

    #region test implementaions

    private static void GetEnumerator_returns_correct_enumerator<T>() where T : notnull {
        var enumerator = Maybe<T>.None.GetEnumerator();
        Assert.IsInstanceOfType<IEnumerator<T>>(enumerator);
        Assert.IsFalse(enumerator.MoveNext());
    }

    #endregion

    /// <summary>
    ///   The <see cref="None{T}.GetEnumerator"/> methods return a correct
    ///   <see cref="IEnumerator{T}"/> for all types.
    /// </summary>
    [TestMethod]
    public void SomeT_GetEnumerator_returns_correct_enumerator_for_primitive_types() {
        GetEnumerator_returns_correct_enumerator<int>();
        GetEnumerator_returns_correct_enumerator<TestEnum>();
        GetEnumerator_returns_correct_enumerator<(int, string)>();
        GetEnumerator_returns_correct_enumerator<decimal>();
        GetEnumerator_returns_correct_enumerator<string>();
        GetEnumerator_returns_correct_enumerator<int[]>();
        GetEnumerator_returns_correct_enumerator<TestClass>();
    }
}

/// <summary>
///   Unit tests for <see cref="Some{T}.HasValue"/> property.
/// </summary>
[TestClass]
public class HasValue_Property_Tests {

    /// <summary>
    ///   The <see cref="None{T}.HasValue"/> property returns <see langword="false"/>
    ///   for all types.
    /// </summary>
    [TestMethod]
    public void NoneT_HasValue_returns_false_for_primitive_types() {
        Assert.IsFalse(Maybe<int>.None.HasValue);
        Assert.IsFalse(Maybe<TestEnum>.None.HasValue);
        Assert.IsFalse(Maybe<(int, string)>.None.HasValue);
        Assert.IsFalse(Maybe<decimal>.None.HasValue);
        Assert.IsFalse(Maybe<string>.None.HasValue);
        Assert.IsFalse(Maybe<int[]>.None.HasValue);
        Assert.IsFalse(Maybe<TestClass>.None.HasValue);
    }
}

/// <summary>
///   Unit tests for the <see cref="None{T}.ToString"/> method.
/// </summary>
[TestClass]
public class ToString_Tests {

    /// <summary>
    ///   The <see cref="None{T}.ToString"/> method returns the expected represention.
    /// </summary>
    [TestMethod]
    public void NoneT_ToString_creates_expected_representation() {
        Assert.AreEqual("Maybe<TestEnum>.None", Maybe<TestEnum>.None.ToString());
        Assert.AreEqual("Maybe<Int32>.None", Maybe<int>.None.ToString());
        Assert.AreEqual("Maybe<ValueTuple`2>.None", Maybe<(int, string)>.None.ToString());
        Assert.AreEqual("Maybe<Decimal>.None", Maybe<decimal>.None.ToString());
        Assert.AreEqual("Maybe<String>.None", Maybe<string>.None.ToString());
        Assert.AreEqual("Maybe<Int32[]>.None", Maybe<int[]>.None.ToString());
        Assert.AreEqual("Maybe<TestClass>.None", Maybe<TestClass>.None.ToString());
    }
}
