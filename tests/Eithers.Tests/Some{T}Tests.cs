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

    /// <summary>
    ///   The <see cref="Some{T}"/> cast creates the correct type
    ///   and wraps the correct value.
    /// </summary>
    [TestMethod]
    public void SomeT_SomeT_cast_creates_correct_SomeT_type_and_value() {

        // explicit cast
        var some = (Some<int>)111;
        Assert.IsInstanceOfType<Some<int>>(some);
        Assert.IsInstanceOfType<Maybe<int>>(some);
        Assert.AreEqual(111, some.Value);

        // implicit cast
        Some<int> some2 = 111;
        Assert.IsInstanceOfType<Some<int>>(some2);
        Assert.IsInstanceOfType<Maybe<int>>(some2);
        Assert.AreEqual(111, some2.Value);

    }
}

/// <summary>
///   Unit tests for the <see cref="Some{T}"/> constructors.
/// </summary>
[TestClass]
public class Constructor_Tests {

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
    ///   The <see cref="Some{T}"/> constructor creates the correct type
    ///   and wraps the correct value.
    /// </summary>
    [TestMethod]
    public void SomeT_constructor_creates_correct_SomeT_type_and_value() {
        var some = new Some<int>(111);
        Assert.IsInstanceOfType<Some<int>>(some);
        Assert.IsInstanceOfType<Maybe<int>>(some);
        Assert.AreEqual(111, some.Value);
    }
}

/// <summary>
///   Unit tests for the <see cref="Some{T}.Equals(T)"/> methods.
/// </summary>
[TestClass]
public class EqualsT_Method_Tests {

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for primitive types with the same values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsT_returns_true_for_primitive_types_with_the_same_value() {
        var intSome = new Some<int>(111);
        Assert.IsTrue(intSome.Equals(111));
        var enumSome = new Some<TestEnum>(TestEnum.E11);
        Assert.IsTrue(enumSome.Equals(TestEnum.E11));
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for value types with the same values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsT_returns_true_for_value_types_with_the_same_value() {
        var tupleSome = new Some<(int, string)>((111, "111"));
        Assert.IsTrue(tupleSome.Equals((111, "111")));
        var decimalSome = new Some<Decimal>(111);
        Assert.IsTrue(decimalSome.Equals(111));
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for reference types with the same values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsT_returns_true_for_reference_types_with_the_same_value() {

        // Note: string overrides Equals to compare by value.
        // All other reference types compare by reference.

        var stringSome = new Some<string>("111");
        Assert.IsTrue(stringSome.Equals("111"));

        var arrayValue = new int[] { 111 };
        var arraySome = new Some<int[]>(arrayValue);
        Assert.IsTrue(arraySome.Equals(arrayValue));

        var classValue = new TestClass(111, "111");
        var classSome = new Some<TestClass>(classValue);
        Assert.IsTrue(classSome.Equals(classValue));
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for primitive types with the same values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsT_returns_false_for_primitive_types_with_the_different_value() {
        var intSome = new Some<int>(111);
        Assert.IsFalse(intSome.Equals(222));
        var enumSome = new Some<TestEnum>(TestEnum.E11);
        Assert.IsFalse(enumSome.Equals(TestEnum.E22));
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for value types with the same values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsT_returns_false_for_value_types_with_the_different_value() {
        var tupleSome = new Some<(int, string)>((111, "111"));
        Assert.IsFalse(tupleSome.Equals((222, "222")));
        var decimalSome = new Some<Decimal>(111);
        Assert.IsFalse(decimalSome.Equals(222));
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for reference types with the same values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsT_returns_false_for_reference_types_with_the_different_value() {

        // Note: string overrides Equals to compare by value.
        // All other reference types compare by reference.

        var stringSome = new Some<string>("111");
        Assert.IsFalse(stringSome.Equals("222"));

        var arrayValue = new int[] { 111 };
        var arraySome = new Some<int[]>(arrayValue);
        Assert.IsFalse(arraySome.Equals(new int[] { 222 }));

        var classValue = new TestClass(111, "111");
        var classSome = new Some<TestClass>(classValue);
        Assert.IsFalse(classSome.Equals(new TestClass(222, "222")));
    }
}

/// <summary>
///   Unit tests for the <see cref="Some{T}.Equals(Maybe{T})"/> methods.
/// </summary>
[TestClass]
public class EqualsMaybeT_Method_Tests {

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for primitive types with the same values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_true_for_primitive_types_with_the_same_value() {
        var intSome = new Some<int>(111);
        Assert.IsTrue(intSome.Equals(new Some<int>(111)));
        var enumSome = new Some<TestEnum>(TestEnum.E11);
        Assert.IsTrue(enumSome.Equals(new Some<TestEnum>(TestEnum.E11)));
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for value types with the same values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_true_for_value_types_with_the_same_value() {
        var tupleSome = new Some<(int, string)>((111, "111"));
        Assert.IsTrue(tupleSome.Equals(new Some<(int, string)>((111, "111"))));
        var decimalSome = new Some<Decimal>(111);
        Assert.IsTrue(decimalSome.Equals(new Some<Decimal>(111)));
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for reference types with the same values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_true_for_reference_types_with_the_same_value() {

        // Note: string overrides Equals to compare by value.
        // All other reference types compare by reference.

        var stringSome = new Some<string>("111");
        Assert.IsTrue(stringSome.Equals(new Some<string>("111")));

        var arrayValue = new int[] { 111 };
        var arraySome = new Some<int[]>(arrayValue);
        Assert.IsTrue(arraySome.Equals(new Some<int[]>(arrayValue)));

        var classValue = new TestClass(111, "111");
        var classSome = new Some<TestClass>(classValue);
        Assert.IsTrue(classSome.Equals(new Some<TestClass>(classValue)));
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(Maybe{T})"/> method returns <see langword="true"/>
    ///  for primitive types with the same values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_false_for_primitive_types_with_the_different_value() {
        var intSome = new Some<int>(111);
        Assert.IsFalse(intSome.Equals(new Some<int>(222)));
        var enumSome = new Some<TestEnum>(TestEnum.E11);
        Assert.IsFalse(enumSome.Equals(new Some<TestEnum>(TestEnum.E22)));
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(Maybe{T})"/> method returns <see langword="true"/>
    ///  for value types with the same values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_false_for_value_types_with_the_different_value() {
        var tupleSome = new Some<(int, string)>((111, "111"));
        Assert.IsFalse(tupleSome.Equals(new Some<(int, string)>((222, "222"))));
        var decimalSome = new Some<Decimal>(111);
        Assert.IsFalse(decimalSome.Equals(new Some<Decimal>(222)));
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(Maybe{T})"/> method returns <see langword="true"/>
    ///  for reference types with the same values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_false_for_reference_types_with_the_different_value() {

        // Note: string overrides Equals to compare by value.
        // All other reference types compare by reference.

        var stringSome = new Some<string>("111");
        Assert.IsFalse(stringSome.Equals(new Some<string>("222")));

        var arrayValue = new int[] { 111 };
        var arraySome = new Some<int[]>(arrayValue);
        Assert.IsFalse(arraySome.Equals(new Some<int[]>(new int[] { 222 })));

        var classValue = new TestClass(111, "111");
        var classSome = new Some<TestClass>(classValue);
        Assert.IsFalse(classSome.Equals(new Some<TestClass>(new TestClass(222, "222"))));
    }
    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for primitive types with the same values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_false_for_primitive_types_with_no_value() {
        var intSome = new Some<int>(111);
        Assert.IsFalse(intSome.Equals(Maybe<int>.None));
        var enumSome = new Some<TestEnum>(TestEnum.E11);
        Assert.IsFalse(enumSome.Equals(Maybe<TestEnum>.None));
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for value types with the same values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_false_for_value_types_with_no_value() {
        var tupleSome = new Some<(int, string)>((111, "111"));
        Assert.IsFalse(tupleSome.Equals(Maybe<(int, string)>.None));
        var decimalSome = new Some<Decimal>(111);
        Assert.IsFalse(decimalSome.Equals(Maybe<Decimal>.None));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for reference types with the same values.
    /// </summary>
    [TestMethod]
    public void SomeT_EqualsMaybeT_returns_false_for_reference_types_with_no_value() {

        // Note: string overrides Equals to compare by value.
        // All other reference types compare by reference.

        var stringSome = new Some<string>("111");
        Assert.IsFalse(stringSome.Equals(Maybe<string>.None));

        var arrayValue = new int[] { 111 };
        var arraySome = new Some<int[]>(arrayValue);
        Assert.IsFalse(arraySome.Equals(Maybe<int[]>.None));

        var classValue = new TestClass(111, "111");
        var classSome = new Some<TestClass>(classValue);
        Assert.IsFalse(classSome.Equals(Maybe<TestClass>.None));
    }
}

/// <summary>
///   Unit tests for the <see cref="Some{T}.GetEnumerator"/> method.
/// </summary>
[TestClass]
public class GetEnumerator_Method_Tests {

    /// <summary>
    ///   The <see cref="Some{T}.GetEnumerator"/> methods return a correct
    ///   <see cref="IEnumerator{T}"/> for a <see cref="Some{T}"/>.
    /// </summary>
    [TestMethod]
    public void SomeT_GetEnumerator_returns_correct_enumerator() {
        var some = new Some<int>(111);
        var enumerator = some.GetEnumerator();
        Assert.IsInstanceOfType<IEnumerator<int>>(enumerator);
        Assert.IsTrue(enumerator.MoveNext());
        Assert.AreEqual(111, enumerator.Current);
        Assert.IsFalse(enumerator.MoveNext());
    }
}

/// <summary>
///   Unit tests for <see cref="Some{T}"/> methods and extention methods.
/// </summary>
[TestClass]
public class GetHashCode_Method_Tests {

    /// <summary>
    ///   The <see cref="None{T}.GetHashCode"/> method returns a statistically-unique
    ///   for each different <see cref="None{T}"/> type.
    /// </summary>
    [TestMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Globalization", "CA1307:Specify StringComparison for clarity",
        Justification = "StringComparison cannot be used within Maybe<T>")]
    public void SomeT_GetHashCode_returns_hash_value_derived_from_wrapped_value_for_reference_types() {
        var stringMaybe = Maybe.From("111");
        Assert.AreEqual("111".GetHashCode(), stringMaybe.GetHashCode());
        Assert.AreNotEqual("222".GetHashCode(), stringMaybe.GetHashCode());
        var arrayMaybe = Maybe.From(arrayValue);
        Assert.AreEqual(arrayValue.GetHashCode(), arrayMaybe.GetHashCode());
        Assert.AreNotEqual(arrayValue2.GetHashCode(), arrayMaybe.GetHashCode());
        var classMaybe = Maybe.From(classValue);
        Assert.AreEqual(classValue.GetHashCode(), classMaybe.GetHashCode());
        Assert.AreNotEqual(classValue2.GetHashCode(), classMaybe.GetHashCode());
    }
}

/// <summary>
///   Unit tests for the <see cref="Some{T}.HasValue"/> property.
/// </summary>
[TestClass]
public class HasValue_Property_Tests {

    /// <summary>
    ///   The <see cref="Some{T}.HasValue"/> property returns <see langword="true"/>
    ///   for primitive types.
    /// </summary>
    [TestMethod]
    public void SomeT_HasValue_returns_true_for_primitive_types() {
        var intSome = new Some<int>(111);
        Assert.IsTrue(intSome.HasValue);
        var enumSome = new Some<TestEnum>(TestEnum.E11);
        Assert.IsTrue(enumSome.HasValue);
    }

    /// <summary>
    ///   The <see cref="Some{T}.HasValue"/> property returns <see langword="true"/>
    ///   for value types.
    /// </summary>
    [TestMethod]
    public void SomeT_HasValue_returns_true_for_value_types() {
        Assert.IsFalse(Maybe<TestStruct>.None.HasValue);
        Assert.IsFalse(Maybe<(int, string)>.None.HasValue);
    }

    /// <summary>
    ///   The <see cref="Some{T}.HasValue"/> property returns <see langword="true"/>
    ///   for reference types.
    /// </summary>
    [TestMethod]
    public void SomeT_HasValue_returns_true_for_reference_types() {
        Assert.IsFalse(Maybe<string>.None.HasValue);
        Assert.IsFalse(Maybe<int[]>.None.HasValue);
        Assert.IsFalse(Maybe<TestClass>.None.HasValue);
    }
}

/// <summary>
///   Unit tests for <see cref="Some{T}"/> methods and extention methods.
/// </summary>
[TestClass]
public class Value_Property_Tests {

    /// <summary>
    ///   The <see cref="Some{T}.Value"/> property returns the value provided to
    ///   the constructor.
    /// </summary>
    [TestMethod]
    public void SomeT_Value_is_initialized_to_correct_value() {
        var some = new Some<int>(111);
        Assert.AreEqual(111, some.Value);
    }
}
