#nullable enable

using System.Reflection;

using Ainsworth.Eithers;

using static Ainsworth.Eithers.Tests.TestData;

namespace SomeT_Tests;

/// <summary>
///   Unit tests for the <see cref="Some{T}"/> casts.
/// </summary>
[TestClass]
public class Cast_Tests {

    #region test implementations

    private static void SomeT_case_creates_Some_from_value<T>(T value) where T: notnull {

        // explicit cast
        var some = (Some<T>)value;
        Assert.IsInstanceOfType<Some<T>>(some);
        Assert.IsInstanceOfType<Maybe<T>>(some);
        Assert.AreEqual(value, some.Value);

        // implicit cast
        Some<T> some2 = value;
        Assert.IsInstanceOfType<Some<T>>(some2);
        Assert.IsInstanceOfType<Maybe<T>>(some2);
        Assert.AreEqual(value, some2.Value);
    }

    #endregion

    /// <summary>
    ///   The <see cref="Some{T}"/> cast creates the correct type
    ///   and wraps the correct value for primitive types.
    /// </summary>
    [TestMethod]
    public void SomeT_cast_creates_correct_SomeT_for_primitive_types() {
        SomeT_case_creates_Some_from_value(TestEnum.E11);
        SomeT_case_creates_Some_from_value(111);
    }

    /// <summary>
    ///   The <see cref="Some{T}"/> cast creates the correct type
    ///   and wraps the correct value for value types.
    /// </summary>
    [TestMethod]
    public void SomeT_cast_creates_correct_SomeT_for_value_types() {
        SomeT_case_creates_Some_from_value((111, "111"));
        SomeT_case_creates_Some_from_value((decimal)111);
    }

    /// <summary>
    ///   The <see cref="Some{T}"/> cast creates the correct type
    ///   and wraps the correct value for reference types.
    /// </summary>
    [TestMethod]
    public void SomeT_cast_creates_correct_SomeT_reference_types() {
        SomeT_case_creates_Some_from_value("111");
        SomeT_case_creates_Some_from_value(new int[] { 111 });
        SomeT_case_creates_Some_from_value(new TestClass(111, "111"));
    }
}

/// <summary>
///   Unit tests for the <see cref="Some{T}"/> constructors.
/// </summary>
[TestClass]
public class Constructor_Tests {

    #region test implementations

    private static void SomeT_constructor_creates_SomeT<T>(T value) where T: notnull {
        var some = new Some<T>(value);
        Assert.IsInstanceOfType<Some<T>>(some);
        Assert.IsInstanceOfType<Maybe<T>>(some);
        Assert.AreEqual(value, some.Value);
    }

    private static void SomeT_constructor_throws_for_null<T>() where T : class =>
        Assert.ThrowsException<ArgumentNullException>(() => new Some<T>(null!));

    #endregion

    /// <summary>
    ///   The <see cref="Some{T}"/> constructors' visibility is not public.
    /// </summary>
    [TestMethod]
    public void SomeT_constructors_are_protected_or_internal() {
        var type = typeof(Some<int>);
        var ctors = type.GetConstructors(
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.Static);
        foreach (var ctor in ctors) {
            // It
            Assert.IsFalse(
                ctor.IsPublic,
                $"{ctor.DeclaringType!.Name} has at least 1 public constructor");
        }
    }

    /// <summary>
    ///   The <see cref="Some{T}"/> constructor creates the correct <see cref="Some{T}"/>
    ///   and wraps the correct value for primitive types.
    /// </summary>
    [TestMethod]
    public void SomeT_constructor_creates_SomeT_for_primitive_types() {
        SomeT_constructor_creates_SomeT(TestEnum.E11);
        SomeT_constructor_creates_SomeT(111);
    }

    /// <summary>
    ///   The <see cref="Some{T}"/> constructor creates the correct <see cref="Some{T}"/>
    ///   and wraps the correct value for value types.
    /// </summary>
    [TestMethod]
    public void SomeT_constructor_creates_SomeT_for_value_types() {
        SomeT_constructor_creates_SomeT((111, "111"));
        SomeT_constructor_creates_SomeT((decimal)111);
    }

    /// <summary>
    ///   The <see cref="Some{T}"/> constructor creates the correct <see cref="Some{T}"/>
    ///   and wraps the correct value for reference types.
    /// </summary>
    [TestMethod]
    public void SomeT_constructor_creates_SomeT_for_reference_types() {
        SomeT_constructor_creates_SomeT("111");
        SomeT_constructor_creates_SomeT(new int[] { 111 });
        SomeT_constructor_creates_SomeT(new TestClass(111, "111"));
    }

    /// <summary>
    ///   The <see cref="Some{T}"/> constructor throws <see cref="ArgumentNullException"/>
    ///   for null reference types.
    /// </summary>
    [TestMethod]
    public void SomeT_constructor_throws_for_null_reference_types() {
        SomeT_constructor_throws_for_null<string>();
        SomeT_constructor_throws_for_null<int[]>();
        SomeT_constructor_throws_for_null<TestClass>();
    }
}

/// <summary>
///   Unit tests for the <see cref="Some{T}.Equals(T)"/> methods.
/// </summary>
[TestClass]
public class EqualsT_Method_Tests {

    #region test implementations

    private static void EqualsT_returns_true_for_same_value<T>(T value)
            where T : notnull {
        var some = new Some<T>(value);
        Assert.IsTrue(some.Equals(value));
    }

    private static void EqualsT_returns_true_for_different_value<T>(T value, T value2)
            where T : notnull {
        var some = new Some<T>(value);
        Assert.IsFalse(some.Equals(value2));
    }

    #endregion

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for primitive types with the same value.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsT_returns_true_for_primitive_types_with_the_same_value() {
        EqualsT_returns_true_for_same_value(TestEnum.E11);
        EqualsT_returns_true_for_same_value(111);
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for value types with the same values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsT_returns_true_for_value_types_with_the_same_value() {
        EqualsT_returns_true_for_same_value((111, "111"));
        EqualsT_returns_true_for_same_value((decimal)111);
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for reference types with the same values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsT_returns_true_for_reference_types_with_the_same_value() {

        // Note: string overrides Equals to compare by value.
        // All other reference types compare by reference.

        EqualsT_returns_true_for_same_value("111");
        EqualsT_returns_true_for_same_value(new int[] { 111 });
        EqualsT_returns_true_for_same_value(new TestClass(111, "111"));
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for primitive types with the same values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsT_returns_false_for_primitive_types_with_different_value() {
        EqualsT_returns_true_for_different_value(111, 222);
        EqualsT_returns_true_for_different_value(TestEnum.E11, TestEnum.E22);
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for value types with the same values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsT_returns_false_for_value_types_with_the_different_value() {
        EqualsT_returns_true_for_different_value((111, "111"), (222, "222"));
        EqualsT_returns_true_for_different_value((decimal)111, (decimal)222);
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for reference types with the same values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsT_returns_false_for_reference_types_with_the_different_value() {

        // Note: string overrides Equals to compare by value.
        // All other reference types compare by reference.

        EqualsT_returns_true_for_different_value("111", "222");
        EqualsT_returns_true_for_different_value(new int[] { 111 }, new int[] { 222 });
        EqualsT_returns_true_for_different_value(
            new TestClass(111, "111"), new TestClass(222, "222"));
    }
}

/// <summary>
///   Unit tests for the <see cref="Some{T}.Equals(Maybe{T})"/> methods.
/// </summary>
[TestClass]
public class Equals_MaybeT_Method_Tests {

    #region test implementations

    private static void Equals_MaybeT_returns_true_for_same_value<T>(T value) where T : notnull {
        var maybe = new Some<T>(value);
        var maybe2 = new Some<T>(value);
        Assert.IsTrue(maybe.Equals(maybe2));
    }

    private static void Equals_MaybeT_returns_false_for_different_value<T>(T value, T value2)
            where T : notnull {
        var maybe = new Some<T>(value);
        var maybe2 = new Some<T>(value2);
        Assert.IsFalse(maybe.Equals(maybe2));
    }

    private static void Equals_MaybeT_returns_false_for_None<T>(T value)
            where T : notnull {
        var maybe = new Some<T>(value);
        Assert.IsFalse(maybe.Equals(Maybe<T>.None));
    }

    #endregion

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for primitive types with the same values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_true_for_primitive_type_with_the_same_value() {
        Equals_MaybeT_returns_true_for_same_value(111);
        Equals_MaybeT_returns_true_for_same_value(TestEnum.E11);
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for value types with the same values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_true_for_value_type_with_the_same_value() {
        Equals_MaybeT_returns_true_for_same_value((111, "111"));
        Equals_MaybeT_returns_true_for_same_value((decimal)111);
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for reference types with the same values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_true_for_reference_type_with_the_same_value() {

        // Note: string overrides Equals to compare by value.
        // All other reference types compare by reference.

        Equals_MaybeT_returns_true_for_same_value("111");
        Equals_MaybeT_returns_true_for_same_value(new int[] { 111 });
        Equals_MaybeT_returns_true_for_same_value(new TestClass(111, "111"));
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(Maybe{T})"/> method returns <see langword="false"/>
    ///  for primitive types with the different value.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_false_for_primitive_type_with_the_different_value() {
        Equals_MaybeT_returns_false_for_different_value(111, 222);
        Equals_MaybeT_returns_false_for_different_value(TestEnum.E11, TestEnum.E22);
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(Maybe{T})"/> method returns <see langword="false"/>
    ///  for value types with the different value.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_false_for_value_type_with_the_different_value() {
        Equals_MaybeT_returns_false_for_different_value((111, "111"), (222, "222"));
        Equals_MaybeT_returns_false_for_different_value((decimal)111, (decimal)222);
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(Maybe{T})"/> method returns <see langword="false"/>
    ///  for reference types with the different value.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_false_for_reference_type_with_the_different_value() {

        // Note: string overrides Equals to compare by value.
        // All other reference types compare by reference.

        Equals_MaybeT_returns_false_for_different_value("111", "222");
        Equals_MaybeT_returns_false_for_different_value(new int[] { 111 }, new int[] { 222 });
        Equals_MaybeT_returns_false_for_different_value(
            new TestClass(111, "111"), new TestClass(222, "222"));
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="false"/>
    ///  for primitive type <see cref="Maybe{T}.None"/>.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_false_for_primitive_types_with_no_value() {
        Equals_MaybeT_returns_false_for_None(111);
        Equals_MaybeT_returns_false_for_None(TestEnum.E11);
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="false"/>
    ///  for value type <see cref="Maybe{T}.None"/>.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_false_for_value_types_with_no_value() {
        Equals_MaybeT_returns_false_for_None((111, "111"));
        Equals_MaybeT_returns_false_for_None((decimal)111);
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="false"/>
    ///  for reference type <see cref="Maybe{T}.None"/>.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_false_for_reference_types_with_no_value() {

        // Note: string overrides Equals to compare by value.
        // All other reference types compare by reference.

        Equals_MaybeT_returns_false_for_None("111");
        Equals_MaybeT_returns_false_for_None(new int[] { 111 });
        Equals_MaybeT_returns_false_for_None(new TestClass(111, "111"));
    }
}

/// <summary>
///   Unit tests for the <see cref="Some{T}.GetEnumerator"/> method.
/// </summary>
[TestClass]
public class GetEnumerator_Method_Tests {

    #region test implementaions

    private static void GetEnumerator_returns_correct_enumerator<T>(T value) where T: notnull {
        var some = new Some<T>(value);
        var enumerator = some.GetEnumerator();
        Assert.IsInstanceOfType<IEnumerator<T>>(enumerator);
        Assert.IsTrue(enumerator.MoveNext());
        Assert.AreEqual(value, enumerator.Current);
        Assert.IsFalse(enumerator.MoveNext());
    }

    #endregion

    /// <summary>
    ///   The <see cref="Some{T}.GetEnumerator"/> methods return a correct
    ///   <see cref="IEnumerator{T}"/> for a primitive types.
    /// </summary>
    [TestMethod]
    public void SomeT_GetEnumerator_returns_correct_enumerator_for_primitive_types() {
        GetEnumerator_returns_correct_enumerator(111);
        GetEnumerator_returns_correct_enumerator(TestEnum.E11);
    }

    /// <summary>
    ///   The <see cref="Some{T}.GetEnumerator"/> methods return a correct
    ///   <see cref="IEnumerator{T}"/> for a primitive types.
    /// </summary>
    [TestMethod]
    public void SomeT_GetEnumerator_returns_correct_enumerator_for_value_types() {
        GetEnumerator_returns_correct_enumerator((111, "111"));
        GetEnumerator_returns_correct_enumerator((decimal)111);
    }

    /// <summary>
    ///   The <see cref="Some{T}.GetEnumerator"/> methods return a correct
    ///   <see cref="IEnumerator{T}"/> for a primitive types.
    /// </summary>
    [TestMethod]
    public void SomeT_GetEnumerator_returns_correct_enumerator_for_reference_types() {
        GetEnumerator_returns_correct_enumerator("111");
        GetEnumerator_returns_correct_enumerator(new int[] { 111 });
        GetEnumerator_returns_correct_enumerator(new TestClass(111, "111"));
    }
}

/// <summary>
///   Unit tests for <see cref="Some{T}"/> methods and extention methods.
/// </summary>
[TestClass]
public class GetHashCode_Method_Tests {

    #region teset implementations

    private static void GetHashCode_returns_hash_value_derived_from_wrapped_value<T>(
            T value, T value2)
            where T: notnull {
        var maybe = new Some<T>(value);
        Assert.AreEqual(value.GetHashCode(), maybe.GetHashCode());
        Assert.AreNotEqual(value2.GetHashCode(), maybe.GetHashCode());
    }

    #endregion

    /// <summary>
    ///   The <see cref="None{T}.GetHashCode"/> method returns a hash code computed
    ///   from the wrapped value for primtive type.
    /// </summary>
    [TestMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Globalization", "CA1307:Specify StringComparison for clarity",
        Justification = "StringComparison cannot be used within Maybe<T>")]
    public void SomeT_GetHashCode_returns_hash_value_derived_from_wrapped_value_for_primitive_types() {
        GetHashCode_returns_hash_value_derived_from_wrapped_value(111, 222);
        GetHashCode_returns_hash_value_derived_from_wrapped_value(TestEnum.E11, TestEnum.E22);
    }

    /// <summary>
    ///   The <see cref="None{T}.GetHashCode"/> method returns a hash code computed
    ///   from the wrapped value for value types.
    /// </summary>
    [TestMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Globalization", "CA1307:Specify StringComparison for clarity",
        Justification = "StringComparison cannot be used within Maybe<T>")]
    public void SomeT_GetHashCode_returns_hash_value_derived_from_wrapped_value_for_value_types() {
        GetHashCode_returns_hash_value_derived_from_wrapped_value((111, "111"), (222, "222"));
        GetHashCode_returns_hash_value_derived_from_wrapped_value((decimal)111, (decimal)222);
    }

    /// <summary>
    ///   The <see cref="None{T}.GetHashCode"/> method returns a hash code computed
    ///   from the wrapped value for reference types.
    /// </summary>
    [TestMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Globalization", "CA1307:Specify StringComparison for clarity",
        Justification = "StringComparison cannot be used within Maybe<T>")]
    public void SomeT_GetHashCode_returns_hash_value_derived_from_wrapped_value_for_reference_types() {
        GetHashCode_returns_hash_value_derived_from_wrapped_value("111", "222");
        GetHashCode_returns_hash_value_derived_from_wrapped_value(
            new int[] { 111 }, new int[] { 222 });
        GetHashCode_returns_hash_value_derived_from_wrapped_value(
            new TestClass(111, "111"), new TestClass(222, "222"));
    }
}

/// <summary>
///   Unit tests for the <see cref="Some{T}.HasValue"/> property.
/// </summary>
[TestClass]
public class HasValue_Property_Tests {

    #region test implementations

    private static void HasValue_returns_true<T>(T value) where T: notnull {
        var some = new Some<T>(value);
        Assert.IsTrue(some.HasValue);
    }

    #endregion

    /// <summary>
    ///   The <see cref="Some{T}.HasValue"/> property returns <see langword="true"/>
    ///   for primitive types.
    /// </summary>
    [TestMethod]
    public void SomeT_HasValue_returns_true_for_primitive_types() {
        HasValue_returns_true(111);
        HasValue_returns_true(TestEnum.E11);
    }

    /// <summary>
    ///   The <see cref="Some{T}.HasValue"/> property returns <see langword="true"/>
    ///   for value types.
    /// </summary>
    [TestMethod]
    public void SomeT_HasValue_returns_true_for_value_types() {
        HasValue_returns_true((111, "111"));
        HasValue_returns_true((decimal)111);
    }

    /// <summary>
    ///   The <see cref="Some{T}.HasValue"/> property returns <see langword="true"/>
    ///   for reference types.
    /// </summary>
    [TestMethod]
    public void SomeT_HasValue_returns_true_for_reference_types() {
        HasValue_returns_true("111");
        HasValue_returns_true(new int[] { 111 });
        HasValue_returns_true(new TestClass(111, "111"));
    }
}

/// <summary>
///   Unit tests for <see cref="Some{T}"/> methods and extention methods.
/// </summary>
[TestClass]
public class Value_Property_Tests {

    #region

    private static void Value_is_initialized_to_correct_value<T>(T value) where T : notnull {
        var some = new Some<T>(value);
        Assert.AreEqual(value, some.Value);
    }

    #endregion

    /// <summary>
    ///   The <see cref="Some{T}.Value"/> property returns the value provided to
    ///   the constructor for primitive types.
    /// </summary>
    [TestMethod]
    public void SomeT_Value_is_initialized_to_correct_value_for_primitive_types() {
        Value_is_initialized_to_correct_value(111);
        Value_is_initialized_to_correct_value(TestEnum.E11);
    }

    /// <summary>
    ///   The <see cref="Some{T}.Value"/> property returns the value provided to
    ///   the constructor for value types.
    /// </summary>
    [TestMethod]
    public void SomeT_Value_is_initialized_to_correct_value_for_value_types() {
        Value_is_initialized_to_correct_value((111, "111"));
        Value_is_initialized_to_correct_value((decimal)111);
    }

    /// <summary>
    ///   The <see cref="Some{T}.Value"/> property returns the value provided to
    ///   the constructor for reference types.
    /// </summary>
    [TestMethod]
    public void SomeT_Value_is_initialized_to_correct_value_for_reference_types() {
        Value_is_initialized_to_correct_value("111");
        Value_is_initialized_to_correct_value(new int[] { 111 });
        Value_is_initialized_to_correct_value(new TestClass(111, "111"));
    }
}
