#nullable enable

using System.Collections;
using System.Reflection;

using Ainsworth.Eithers;
using static Ainsworth.Eithers.Tests.TestData;

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
    public void MaybeT_constructors_are_protected() {
        var type = typeof(Maybe<int>);
        var ctors = type.GetConstructors(
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.Static);
        foreach (var ctor in ctors) {
            Assert.IsFalse(
                ctor.IsPublic,
                $"{ctor.DeclaringType!.Name} has at least 1 public constructor");
        }
    }
}

/// <summary>
///   Unit test for <see cref="Maybe{T}.Equals(T)"/> tests.
/// </summary>
[TestClass]
public class EqualsT_Method_Tests {
    // Maybe<T>.Equals(T) is an abstract method. No tests needed. 
    // All tests implemented for the Some<T> and None<T> classes.
}

/// <summary>
///   Unit test for <see cref="Maybe{T}.Equals(Maybe{T})"/> tests.
/// </summary>
[TestClass]
public class EqualsMaybeT_Method_Tests {
    // Maybe<T>.Equals(Maybe<T>>) is an abstract method.
    // All tests implemented for the Some<T> and None<T> classes.
}

/// <summary>
///   Unit test for <see cref="Maybe{T}.Equals(object)"/> tests.
/// </summary>
[TestClass]
public class EqualsObject_Method_Tests {

    #region test implementions

    private static void MaybeT_EqualsObject_returns_true_if_same_value<T>(T value)
            where T : notnull {
        Maybe<T> maybe = new Some<T>(value);
        Assert.IsTrue(maybe.Equals((object)value));
    }

    private static void MaybeT_EqualsObject_returns_false_if_different_value<T>(T value, T value2)
            where T : notnull {
        Maybe<T> maybe = new Some<T>(value);
        Assert.IsFalse(maybe.Equals((object)value2));
    }

    private static void MaybeT_EqualsObject_returns_false_for_None_and_value<T>(T value2)
            where T : notnull =>
        Assert.IsFalse(Maybe<T>.None.Equals((object)value2));

    private static void MaybeT_EqualsObject_returns_false_for_None_and_null<T>()
            where T : struct =>
        Assert.IsFalse(Maybe<T>.None.Equals((object)(T?)null!));

    private static void MaybeT_EqualsObject_returns_false_for_None_and_null_reference<T>()
            where T : class =>
        Assert.IsFalse(Maybe<T>.None.Equals((object)(T)null!));

    private static void MaybeT_EqualsObject_returns_true_for_None_and_None<T>()
            where T : notnull =>
        Assert.IsTrue(Maybe<T>.None.Equals((object)Maybe<T>.None));

    #endregion

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for primitive types with the same values.
    /// </summary>
    [TestMethod]
    public void MaybeT_EqualsObject_returns_true_for_primitive_types_with_the_same_value() {
        MaybeT_EqualsObject_returns_true_if_same_value(TestEnum.E11);
        MaybeT_EqualsObject_returns_true_if_same_value(111);
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for value types with the same values.
    /// </summary>
    [TestMethod]
    public void MaybeT_EqualsObject_returns_true_for_value_types_with_the_same_value() {
        MaybeT_EqualsObject_returns_true_if_same_value((111, "111"));
        MaybeT_EqualsObject_returns_true_if_same_value((decimal)111);
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for reference types with the same values.
    /// </summary>
    [TestMethod]
    public void MaybeT_EqualsObject_returns_true_for_reference_types_with_the_same_value() {

        // Note: string overrides Equals to compare by value.
        // All other reference types compare by reference.

        MaybeT_EqualsObject_returns_true_if_same_value("111");
        MaybeT_EqualsObject_returns_true_if_same_value(new int[] { 111 });
        MaybeT_EqualsObject_returns_true_if_same_value(new TestClass(111, "111"));
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for primitive types with the same values.
    /// </summary>
    [TestMethod]
    public void MaybeT_EqualsObject_returns_false_for_primitive_types_with_the_different_value() {
        MaybeT_EqualsObject_returns_false_if_different_value(TestEnum.E11, TestEnum.E22);
        MaybeT_EqualsObject_returns_false_if_different_value(111, 222);
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for value types with the same values.
    /// </summary>
    [TestMethod]
    public void MaybeT_EqualsObject_returns_false_for_value_types_with_the_different_value() {
        MaybeT_EqualsObject_returns_false_if_different_value((111, "111"), (222, "222"));
        MaybeT_EqualsObject_returns_false_if_different_value((decimal)111, (decimal)222);
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for reference types with the same values.
    /// </summary>
    [TestMethod]
    public void MaybeT_EqualsObject_returns_false_for_reference_types_with_the_different_value() {

        // Note: string overrides Equals to compare by value.
        // All other reference types compare by reference.

        MaybeT_EqualsObject_returns_false_if_different_value("111", "222");
        MaybeT_EqualsObject_returns_false_if_different_value(new int[] { 111 }, new int[] { 222 });
        MaybeT_EqualsObject_returns_false_if_different_value(
            new TestClass(111, "111"), new TestClass(222, "222"));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="false"/>
    ///  for primitive type values.
    /// </summary>
    [TestMethod]
    public void MaybeT_without_value_EqualsObject_returns_false_for_primitive_type_values() {
        MaybeT_EqualsObject_returns_false_for_None_and_value(TestEnum.E22);
        MaybeT_EqualsObject_returns_false_for_None_and_value(222);
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="false"/>
    ///  for value type values.
    /// </summary>
    [TestMethod]
    public void MaybeT_without_value_EqualsObject_returns_false_for_value_types_values() {
        MaybeT_EqualsObject_returns_false_for_None_and_value((222, "222"));
        MaybeT_EqualsObject_returns_false_for_None_and_value((decimal)222);
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="false"/>
    ///  for reference type values.
    /// </summary>
    [TestMethod]
    public void MaybeT_without_value_EqualsObject_returns_false_for_reference_types_values() {
        MaybeT_EqualsObject_returns_false_for_None_and_value("222");
        MaybeT_EqualsObject_returns_false_for_None_and_value(new int[] { 222 });
        MaybeT_EqualsObject_returns_false_for_None_and_value(new TestClass(222, "222"));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="false"/>
    ///  for <see langword="null"/> primitive type values.
    /// </summary>
    [TestMethod]
    public void MaybeT_without_value_EqualsObject_returns_false_for_null_primitive_types() {
        MaybeT_EqualsObject_returns_false_for_None_and_null<TestEnum>();
        MaybeT_EqualsObject_returns_false_for_None_and_null<int>();
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="false"/>
    ///  for <see langword="null"/> value type values.
    /// </summary>
    [TestMethod]
    public void MaybeT_without_value_EqualsObject_returns_false_for_null_value_types() {
        MaybeT_EqualsObject_returns_false_for_None_and_null<(int, string)>();
        MaybeT_EqualsObject_returns_false_for_None_and_null<decimal>();
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="false"/>
    ///  for <see langword="null"/> reference type values.
    /// </summary>
    [TestMethod]
    public void MaybeT_without_value_EqualsObject_returns_false_for_null_reference_types() {
        MaybeT_EqualsObject_returns_false_for_None_and_null_reference<string>();
        MaybeT_EqualsObject_returns_false_for_None_and_null_reference<int[]>();
        MaybeT_EqualsObject_returns_false_for_None_and_null_reference<TestClass>();
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for primitive type <see cref="Maybe{T}.None"/> values.
    /// </summary>
    [TestMethod]
    public void MaybeT_without_value_EqualsObject_returns_true_for_primitive_type_None() {
        MaybeT_EqualsObject_returns_true_for_None_and_None<TestEnum>();
        MaybeT_EqualsObject_returns_true_for_None_and_None<int>();
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for value type <see cref="Maybe{T}.None"/> values.
    /// </summary>
    [TestMethod]
    public void MaybeT_without_value_EqualsObject_returns_true_for_value_type_None() {
        MaybeT_EqualsObject_returns_true_for_None_and_None<(int, string)>();
        MaybeT_EqualsObject_returns_true_for_None_and_None<decimal>();
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for reference type <see cref="Maybe{T}.None"/> values.
    /// </summary>
    [TestMethod]
    public void MaybeT_without_value_EqualsObject_returns_true_for_reference_type_None() {
        MaybeT_EqualsObject_returns_true_for_None_and_None<string>();
        MaybeT_EqualsObject_returns_true_for_None_and_None<int[]>();
        MaybeT_EqualsObject_returns_true_for_None_and_None<TestClass>();
    }
}

/// <summary>
///   Unit test for <see cref="Maybe{T}.GetEnumerator"/> tests.
/// </summary>
[TestClass]
public class GetEnumerator_Method_Tests {

    /// <summary>
    ///   The <see cref="Maybe{T}.GetEnumerator"/> methods return a correct
    ///   <see cref="IEnumerator{T}"/> for a <see cref="Some{T}"/>.
    /// </summary>
    [TestMethod]
    public void MaybeT_GetEnumerator_returns_correct_enumerator_for_Some_instance() {
        var maybe = Maybe.From<int>(111);
        var enumerator = maybe.GetEnumerator();
        Assert.IsInstanceOfType<IEnumerator<int>>(enumerator);
        Assert.IsTrue(enumerator.MoveNext());
        Assert.AreEqual(111, enumerator.Current);
        Assert.IsFalse(enumerator.MoveNext());
    }

    /// <summary>
    ///   The <see cref="Maybe{T}.GetEnumerator"/> methods return a correct
    ///   <see cref="IEnumerator{T}"/> for a <see cref="None{T}"/>.
    /// </summary>
    [TestMethod]
    public void MaybeT_GetEnumerator_returns_correct_enumerator_for_None_instance() {
        var maybe = Maybe<int>.None;
        var enumerator = maybe.GetEnumerator();
        Assert.IsInstanceOfType<IEnumerator<int>>(enumerator);
        Assert.IsFalse(enumerator.MoveNext());
    }

    /// <summary>
    ///   The <see cref="Maybe{T}.GetEnumerator"/> methods return a correct
    ///   <see cref="IEnumerator"/> for a <see cref="Some{T}"/> cast to
    ///   <see cref="IEnumerable"/>.
    /// </summary>
    [TestMethod]
    public void MaybeT_IEnumeratorGetEnumerator_returns_correct_enumerator_for_Some_instance() {
        IEnumerable maybe = Maybe.From<int>(111);
        var enumerator = maybe.GetEnumerator();
        Assert.IsInstanceOfType<IEnumerator>(enumerator);
        Assert.IsTrue(enumerator.MoveNext());
        Assert.AreEqual(111, enumerator.Current);
        Assert.IsFalse(enumerator.MoveNext());
    }

    /// <summary>
    ///   The <see cref="Maybe{T}.GetEnumerator"/> methods return a correct
    ///   <see cref="IEnumerator"/> for a <see cref="None{T}"/> cast to
    ///   <see cref="IEnumerable"/>.
    /// </summary>
    [TestMethod]
    public void MaybeT_IEnumeratorGetEnumerator_returns_correct_enumerator_for_None_instance() {
        IEnumerable maybe = Maybe<int>.None;
        var enumerator = maybe.GetEnumerator();
        Assert.IsInstanceOfType<IEnumerator<int>>(enumerator);
        Assert.IsFalse(enumerator.MoveNext());
    }
}

/// <summary>
///   Unit test for <see cref="Maybe{T}.GetHashCode"/> tests.
/// </summary>
[TestClass]
public class GetHashCode_Method_Tests {

    #region test implementaions

    private static void MaybeT_GetHashCode_returns_correct_value<T>(T value, T value2)
            where T: notnull {
        var maybe = Maybe.Some<T>(value);
        Assert.AreEqual(value.GetHashCode(), maybe.GetHashCode());
        Assert.AreNotEqual(value2.GetHashCode(), maybe.GetHashCode());
    }

    #endregion

    /// <summary>
    ///   The <see cref="Maybe{T}.GetHashCode"/> method returns the correct value
    ///   for primitive type values.
    /// </summary>
    [TestMethod]
    public void MaybeT_GetHashCode_returns_correct_value_for_primitive_types() {
        MaybeT_GetHashCode_returns_correct_value(TestEnum.E11, TestEnum.E22);
        MaybeT_GetHashCode_returns_correct_value(111, 222);
    }

    /// <summary>
    ///   The <see cref="Maybe{T}.GetHashCode"/> method returns the correct value
    ///   for value type values.
    /// </summary>
    [TestMethod]
    public void MaybeT_GetHashCode_returns_correct_value_for_value_types() {
        MaybeT_GetHashCode_returns_correct_value((111, "111"), (222, "222"));
        MaybeT_GetHashCode_returns_correct_value((decimal)111, (decimal)222);
    }

    /// <summary>
    ///   The <see cref="Maybe{T}.GetHashCode"/> method returns the correct value
    ///   for reference type values.
    /// </summary>
    [TestMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Globalization", "CA1307:Specify StringComparison for clarity",
        Justification = "StringComparison cannot be used within Maybe<T>")]
    public void MaybeT_GetHashCode_returns_correct_value_for_reference_types() {
        MaybeT_GetHashCode_returns_correct_value("111", "222");
        MaybeT_GetHashCode_returns_correct_value(new int[] { 111 }, new int[] { 222 });
        MaybeT_GetHashCode_returns_correct_value(
            new TestClass(111, "111"), new TestClass(222, "222"));
    }
}

/// <summary>
///   Unit test for <see cref="Maybe{T}.HasValue"/> tests.
/// </summary>
[TestClass]
public class HasValue_Property_Tests {

    /// <summary>
    ///   The <see cref="Maybe{T}.HasValue"/> property returns <see langword="true"/>
    ///   for <see cref="Maybe{T}"/>s with a value (i.e., <see cref="Some{T}"/>s).
    /// </summary>
    [TestMethod]
    public void MaybeT_HasValue_returns_true_for_MaybeT_with_value() {

        var intMaybe = Maybe.From(nullableInt111);
        Assert.IsInstanceOfType<Maybe<int>>(intMaybe);
        Assert.IsTrue(intMaybe.HasValue);
    }

    /// <summary>
    ///   The <see cref="Maybe{T}.HasValue"/> property returns <see langword="false"/>
    ///   for <see cref="Maybe{T}"/>s without a value (i.e., <see cref="None{T}"/>s).
    /// </summary>
    [TestMethod]
    public void MaybeT_HasValue_returns_false_for_MaybeT_with_value() {

        var nullIntMaybe = Maybe.From<int>(nullInt);
        Assert.IsInstanceOfType<Maybe<int>>(nullIntMaybe);
        Assert.IsFalse(nullIntMaybe.HasValue);
    }
}

/// <summary>
///   Unit tests for <see cref="Maybe{T}"/> casts.
/// </summary>
[TestClass]
public class MaybeT_Cast_Tests {

    #region test implementions

    private static void MaybeT_cast_creates_Some_from_value<T>(T value) where T : struct {
        var maybe = (Maybe<T>)value;
        Assert.IsInstanceOfType<Maybe<T>>(maybe);
        Assert.IsInstanceOfType<Some<T>>(maybe);
        var some = (maybe as Some<T>)!;
        Assert.AreEqual(value, some.Value);
    }

    private static void MaybeT_cast_creates_Some_from_nullable_value<T>(T value) where T : struct {
        var maybe = (Maybe<T>)(T?)value;
        Assert.IsInstanceOfType<Maybe<T>>(maybe);
        Assert.IsInstanceOfType<Some<T>>(maybe);
        var some = (maybe as Some<T>)!;
        Assert.AreEqual(value, some.Value);
    }

    private static void MaybeT_cast_creates_None_from_null<T>(T? value) where T : struct {
        var maybe = Maybe.From<T>(value);
        Assert.IsInstanceOfType<Maybe<T>>(maybe);
        Assert.IsInstanceOfType<None<T>>(maybe);
        Assert.AreSame(Maybe<T>.None, maybe);
    }

    private static void MaybeT_cast_creates_Some_from_reference_value<T>(T value) where T : class {
        var maybe = Maybe.From<T>(value);
        Assert.IsInstanceOfType<Maybe<T>>(maybe);
        Assert.IsInstanceOfType<Some<T>>(maybe);
        var some = (maybe as Some<T>)!;
        Assert.AreEqual(value, some.Value);
    }

    private static void MaybeT_cast_creates_None_from_null<T>(T? value) where T : class {
        var maybe = Maybe.From<T>(value);
        Assert.IsInstanceOfType<Maybe<T>>(maybe);
        Assert.IsInstanceOfType<None<T>>(maybe);
        Assert.AreSame(Maybe<T>.None, maybe);
    }

    #endregion

    /// <summary>
    ///   The <see cref="Maybe{T}"/> cast returns a <see cref="Some{T}"/> of the 
    ///   correct wrapping the provided primitive value.
    /// </summary>
    [TestMethod]
    public void MaybeT_cast_creates_Some_for_primitive_type_value() {
        MaybeT_cast_creates_Some_from_value(TestEnum.E11);
        MaybeT_cast_creates_Some_from_value(111);
    }

    /// <summary>
    ///   The <see cref="Maybe{T}"/> cast returns a <see cref="Some{T}"/> of the 
    ///   correct wrapping the provided nullable value type value.
    /// </summary>
    [TestMethod]
    public void MaybeT_cast_creates_Some_for_nullable_primitive_type_value() {
        MaybeT_cast_creates_Some_from_nullable_value(TestEnum.E11);
        MaybeT_cast_creates_Some_from_nullable_value(111);
    }

    /// <summary>
    ///   The <see cref="Maybe{T}"/> cast returns a <see cref="Some{T}"/> of the 
    ///   correct wrapping the provided value type value.
    /// </summary>
    [TestMethod]
    public void MaybeT_Cast_creates_Some_for_value_type_value() {
        MaybeT_cast_creates_Some_from_value((111, "111"));
        MaybeT_cast_creates_Some_from_value((decimal)111);
    }

    /// <summary>
    ///   The <see cref="Maybe{T}"/> cast returns a <see cref="Some{T}"/> of the 
    ///   correct wrapping the provided nullable value type value.
    /// </summary>
    [TestMethod]
    public void MaybeT_cast_creates_Some_for_nullable_value_type_value() {
        MaybeT_cast_creates_Some_from_nullable_value((111, "111"));
        MaybeT_cast_creates_Some_from_nullable_value((decimal)111);
    }

    /// <summary>
    ///   The <see cref="Maybe{T}"/> cast returns a <see cref="Some{T}"/> of the 
    ///   correct wrapping the provided nullable reference type value.
    /// </summary>
    [TestMethod]
    public void MaybeT_cast_creates_Some_for_reference_type_value() {
        MaybeT_cast_creates_Some_from_reference_value("111");
        MaybeT_cast_creates_Some_from_reference_value(new int[] { 111 });
        MaybeT_cast_creates_Some_from_reference_value(new TestClass(111, "111"));
    }

    /// <summary>
    ///   The <see cref="Maybe{T}"/> cast returns <see cref="Maybe{T}.None"/> singleton
    ///   for the provided primitive type null value.
    /// </summary>
    [TestMethod]
    public void MaybeT_cast_creates_None_for_primitive_type_null() {
        MaybeT_cast_creates_None_from_null((TestEnum?)null);
        MaybeT_cast_creates_None_from_null((int?)null);
    }

    /// <summary>
    ///   The <see cref="Maybe{T}"/> cast returns <see cref="Maybe{T}.None"/> singleton
    ///   for the provided value type null value.
    /// </summary>
    [TestMethod]
    public void MaybeT_cast_creates_None_for_value_type_null() {
        MaybeT_cast_creates_None_from_null(((int, string)?)null);
        MaybeT_cast_creates_None_from_null((decimal?)null);
    }

    /// <summary>
    ///   The <see cref="Maybe{T}"/> cast returns <see cref="Maybe{T}.None"/> singleton
    ///   for the provided reference type null value.
    /// </summary>
    [TestMethod]
    public void MaybeT_cast_creates_None_for_reference_type_null() {
        MaybeT_cast_creates_None_from_null((string?)null);
        MaybeT_cast_creates_None_from_null((int[]?)null);
        MaybeT_cast_creates_None_from_null((TestClass?)null);
    }
}
