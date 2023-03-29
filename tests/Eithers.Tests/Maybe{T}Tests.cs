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
public class MaybeT_Cast_Tests {

    /// <summary>
    ///   The <see cref="Maybe{T}"/> cast returns a <see cref="Some{T}"/> of the 
    ///   correct wrapping the provided primitive value.
    /// </summary>
    [TestMethod]
    public void MaybeT_cast_creates_correct_type_and_value_for_primitive_types() {

        var enumMaybe = (Maybe<TestEnum>)TestEnum.E11;
        Assert.IsInstanceOfType<Maybe<TestEnum>>(enumMaybe);
        Assert.IsInstanceOfType<Some<TestEnum>>(enumMaybe);
        Assert.AreEqual(TestEnum.E11, (enumMaybe as Some<TestEnum>)!.Value);

        var intMaybe = (Maybe<int>)111;
        Assert.IsInstanceOfType<Maybe<int>>(intMaybe);
        Assert.IsInstanceOfType<Some<int>>(intMaybe);
        Assert.AreEqual(111, (intMaybe as Some<int>)!.Value);
    }

    /// <summary>
    ///   The <see cref="Maybe{T}"/> cast returns a <see cref="Some{T}"/> of the 
    ///   correct wrapping the provided nullable value type value.
    /// </summary>
    [TestMethod]
    public void MaybeT_cast_creates_correct_type_and_value_for_nullable_primitive_types() {

        TestEnum? nullableEnum = TestEnum.E11;
        var enumMaybe = (Maybe<TestEnum>)nullableEnum;
        Assert.IsInstanceOfType<Maybe<TestEnum>>(enumMaybe);
        Assert.IsInstanceOfType<Some<TestEnum>>(enumMaybe);
        Assert.AreEqual(TestEnum.E11, (enumMaybe as Some<TestEnum>)!.Value);

        int? nullableInt = 111;
        var intMaybe = (Maybe<int>)nullableInt;
        Assert.IsInstanceOfType<Maybe<int>>(intMaybe);
        Assert.IsInstanceOfType<Some<int>>(intMaybe);
        Assert.AreEqual(111, (intMaybe as Some<int>)!.Value);
    }

    /// <summary>
    ///   The <see cref="Maybe{T}"/> cast returns a <see cref="Some{T}"/> of the 
    ///   correct wrapping the provided value type value.
    /// </summary>
    [TestMethod]
    public void MaybeT_Cast_creates_correct_type_and_value_for_value_types() {

        var tupleMaybe = (Maybe<(int, string)>)tupleValue;
        Assert.IsInstanceOfType<Maybe<(int, string)>>(tupleMaybe);
        Assert.IsInstanceOfType<Some<(int, string)>>(tupleMaybe);
        Assert.AreEqual(tupleValue, (tupleMaybe as Some<(int, string)>)!.Value);

        var structMaybe = (Maybe<Decimal>)111;
        Assert.IsInstanceOfType<Maybe<Decimal>>(structMaybe);
        Assert.IsInstanceOfType<Some<Decimal>>(structMaybe);
        Assert.AreEqual(111, (structMaybe as Some<Decimal>)!.Value);
    }

    /// <summary>
    ///   The <see cref="Maybe{T}"/> cast returns a <see cref="Some{T}"/> of the 
    ///   correct wrapping the provided nullable value type value.
    /// </summary>
    [TestMethod]
    public void MaybeT_cast_creates_correct_type_and_value_for_nullable_value_types() {

        (int, string)? nullableTuple = tupleValue;
        var tupleMaybe = (Maybe<(int, string)>)nullableTuple;
        Assert.IsInstanceOfType<Maybe<(int, string)>>(tupleMaybe);
        Assert.IsInstanceOfType<Some<(int, string)>>(tupleMaybe);
        Assert.AreEqual(tupleValue, (tupleMaybe as Some<(int, string)>)!.Value);

        Decimal? nullableStruct = 111;
        var structMaybe = (Maybe<Decimal>)nullableStruct;
        Assert.IsInstanceOfType<Maybe<Decimal>>(structMaybe);
        Assert.IsInstanceOfType<Some<Decimal>>(structMaybe);
        Assert.AreEqual(111, (structMaybe as Some<Decimal>)!.Value);
    }

    /// <summary>
    ///   The <see cref="Maybe{T}"/> cast returns a <see cref="Some{T}"/> of the 
    ///   correct wrapping the provided nullable reference type value.
    /// </summary>
    [TestMethod]
    public void MaybeT_cast_creates_correct_type_and_value_for_reference_types() {

        var stringMaybe = (Maybe<string>)"111";
        Assert.IsInstanceOfType<Maybe<string>>(stringMaybe);
        Assert.IsInstanceOfType<Some<string>>(stringMaybe);
        Assert.AreEqual("111", (stringMaybe as Some<string>)!.Value);

        var arrayMaybe = (Maybe<int[]>)arrayValue;
        Assert.IsInstanceOfType<Maybe<int[]>>(arrayMaybe);
        Assert.IsInstanceOfType<Some<int[]>>(arrayMaybe);
        Assert.AreEqual(arrayValue, (arrayMaybe as Some<int[]>)!.Value);

        var classMaybe = (Maybe<TestClass>)classValue;
        Assert.IsInstanceOfType<Maybe<TestClass>>(classMaybe);
        Assert.IsInstanceOfType<Some<TestClass>>(classMaybe);
        Assert.AreEqual(classValue, (classMaybe as Some<TestClass>)!.Value);
    }
}

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

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for primitive types with the same values.
    /// </summary>
    [TestMethod]
    public void MaybeT_with_value_EqualsObject_returns_true_for_primitive_types_with_the_same_value() {
        Maybe<int> intSome = new Some<int>(111);
        Assert.IsTrue(intSome.Equals((object)111));
        Maybe<TestEnum> enumSome = new Some<TestEnum>(TestEnum.E11);
        Assert.IsTrue(enumSome.Equals((object)TestEnum.E11));
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for value types with the same values.
    /// </summary>
    [TestMethod]
    public void MaybeT_with_value_EqualsObject_returns_true_for_value_types_with_the_same_value() {
        Maybe<(int, string)> tupleSome = new Some<(int, string)>((111, "111"));
        Assert.IsTrue(tupleSome.Equals((object)(111, "111")));
        Maybe<Decimal> decimalSome = new Some<Decimal>(111);
        Assert.IsTrue(decimalSome.Equals((object)(Decimal)111));
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for reference types with the same values.
    /// </summary>
    [TestMethod]
    public void MaybeT_with_value_EqualsObject_returns_true_for_reference_types_with_the_same_value() {

        // Note: string overrides Equals to compare by value.
        // All other reference types compare by reference.

        Maybe<string> stringSome = new Some<string>("111");
        Assert.IsTrue(stringSome.Equals((object)"111"));

        var arrayValue = new int[] { 111 };
        Maybe<int[]> arraySome = new Some<int[]>(arrayValue);
        Assert.IsTrue(arraySome.Equals((object)arrayValue));

        var classValue = new TestClass(111, "111");
        Maybe<TestClass> classSome = new Some<TestClass>(classValue);
        Assert.IsTrue(classSome.Equals((object)classValue));
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for primitive types with the same values.
    /// </summary>
    [TestMethod]
    public void MaybeT_with_value_EqualsObject_returns_false_for_primitive_types_with_the_different_value() {
        Maybe<int> intSome = new Some<int>(111);
        Assert.IsFalse(intSome.Equals((object)222));
        Maybe<TestEnum> enumSome = new Some<TestEnum>(TestEnum.E11);
        Assert.IsFalse(enumSome.Equals((object)TestEnum.E22));
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for value types with the same values.
    /// </summary>
    [TestMethod]
    public void MaybeT_with_value_EqualsObject_returns_false_for_value_types_with_the_different_value() {
        Maybe<(int, string)> tupleSome = new Some<(int, string)>((111, "111"));
        Assert.IsFalse(tupleSome.Equals((object)(222, "222")));
        Maybe<Decimal> decimalSome = new Some<Decimal>(111);
        Assert.IsFalse(decimalSome.Equals((object)(Decimal)222));
    }

    /// <summary>
    ///  The <see cref="Some{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for reference types with the same values.
    /// </summary>
    [TestMethod]
    public void MaybeT_with_value_EqualsObject_returns_false_for_reference_types_with_the_different_value() {

        // Note: string overrides Equals to compare by value.
        // All other reference types compare by reference.

        Maybe<string> stringSome = new Some<string>("111");
        Assert.IsFalse(stringSome.Equals((object)"222"));

        var arrayValue = new int[] { 111 };
        Maybe<int[]> arraySome = new Some<int[]>(arrayValue);
        Assert.IsFalse(arraySome.Equals((object)new int[] { 222 }));

        var classValue = new TestClass(111, "111");
        Maybe<TestClass> classSome = new Some<TestClass>(classValue);
        Assert.IsFalse(classSome.Equals((object)new TestClass(222, "222")));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="false"/>
    ///  for primitive type values.
    /// </summary>
    [TestMethod]
    public void MaybeT_without_value_EqualsObject_returns_false_for_primitive_type_values() {
        Assert.IsFalse(Maybe<TestEnum>.None.Equals((object)TestEnum.E11));
        Assert.IsFalse(Maybe<int>.None.Equals((object)111));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="false"/>
    ///  for value type values.
    /// </summary>
    [TestMethod]
    public void MaybeT_without_value_EqualsObject_returns_false_for_value_types_values() {
        Assert.IsFalse(Maybe<(int, string)>.None.Equals((object)tupleValue));
        Assert.IsFalse(Maybe<Decimal>.None.Equals((object)(Decimal)111));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="false"/>
    ///  for reference type values.
    /// </summary>
    [TestMethod]
    public void MaybeT_without_value_EqualsObject_returns_false_for_reference_types_values() {
        Assert.IsFalse(Maybe<string>.None.Equals((object)"111"));
        Assert.IsFalse(Maybe<int[]>.None.Equals((object)arrayValue));
        Assert.IsFalse(Maybe<TestClass>.None.Equals((object)classValue));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="false"/>
    ///  for <see langword="null"/> primitive type values.
    /// </summary>
    [TestMethod]
    public void MaybeT_without_value_EqualsObject_returns_false_for_null_primitive_types() {
        TestEnum? nullEnum = null;
        Assert.IsFalse(Maybe<TestEnum>.None.Equals((object?)nullEnum));
        int? nullInt = null;
        Assert.IsFalse(Maybe<int>.None.Equals((object?)nullInt));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="false"/>
    ///  for <see langword="null"/> value type values.
    /// </summary>
    [TestMethod]
    public void MaybeT_without_value_EqualsObject_returns_false_for_null_value_types() {
        (int, string)? nullTuple = null;
        Assert.IsFalse(Maybe<(int, string)>.None.Equals((object?)nullTuple));
        Decimal? nullDecimal = null;
        Assert.IsFalse(Maybe<Decimal>.None.Equals((object?)nullDecimal));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="false"/>
    ///  for <see langword="null"/> reference type values.
    /// </summary>
    [TestMethod]
    public void MaybeT_without_value_EqualsObject_returns_false_for_null_reference_types() {
        string nullString = null!;
        Assert.IsFalse(Maybe<string>.None.Equals((object)nullString));
        int[] nullArray = null!;
        Assert.IsFalse(Maybe<int[]>.None.Equals((object)nullArray));
        TestClass nullClass = null!;
        Assert.IsFalse(Maybe<TestClass>.None.Equals((object)nullClass));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for primitive type <see cref="Maybe{T}.None"/> values.
    /// </summary>
    [TestMethod]
    public void MaybeT_without_value_EqualsObject_returns_false_for_primitive_type_NoneTs() {
        Assert.IsTrue(Maybe<TestEnum>.None.Equals((object?)Maybe<TestEnum>.None));
        Assert.IsTrue(Maybe<int>.None.Equals((object?)Maybe<int>.None));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for value type <see cref="Maybe{T}.None"/> values.
    /// </summary>
    [TestMethod]
    public void MaybeT_without_value_EqualsObject_returns_false_for_value_type_NoneTs() {
        Assert.IsTrue(Maybe<(int, string)>.None.Equals((object?)Maybe<(int, string)>.None));
        Assert.IsTrue(Maybe<Decimal>.None.Equals((object?)Maybe<Decimal>.None));
    }

    /// <summary>
    ///  The <see cref="None{T}.Equals(T)"/> method returns <see langword="true"/>
    ///  for reference type <see cref="Maybe{T}.None"/> values.
    /// </summary>
    [TestMethod]
    public void MaybeT_without_value_EqualsObject_returns_false_for_reference_type_NoneTs() {
        Assert.IsTrue(Maybe<string>.None.Equals((object)Maybe<string>.None));
        Assert.IsTrue(Maybe<int[]>.None.Equals((object)Maybe<int[]>.None));
        Assert.IsTrue(Maybe<TestClass>.None.Equals((object)Maybe<TestClass>.None));
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

    /// <summary>
    ///   The <see cref="Maybe{T}.GetHashCode"/> method returns the correct value
    ///   for primitive type values.
    /// </summary>
    [TestMethod]
    public void MaybeT_GetHashCode_returns_correct_value_for_primitive_types() {

        var enumMaybe = Maybe.From<TestEnum>(TestEnum.E11);
        Assert.AreEqual(TestEnum.E11.GetHashCode(), enumMaybe.GetHashCode());
        Assert.AreNotEqual(TestEnum.E22.GetHashCode(), enumMaybe.GetHashCode());

        var intMaybe = Maybe.From<int>(111);
        Assert.AreEqual(111.GetHashCode(), intMaybe.GetHashCode());
        Assert.AreNotEqual(222.GetHashCode(), intMaybe.GetHashCode());
    }

    /// <summary>
    ///   The <see cref="Maybe{T}.GetHashCode"/> method returns the correct value
    ///   for value type values.
    /// </summary>
    [TestMethod]
    public void MaybeT_GetHashCode_returns_correct_value_for_value_types() {

        var tupleMaybe = Maybe.From<(int, string)>(tupleValue);
        Assert.AreEqual(tupleValue.GetHashCode(), tupleMaybe.GetHashCode());
        Assert.AreNotEqual(tupleValue2.GetHashCode(), tupleMaybe.GetHashCode());

        var structMaybe = Maybe.From<Decimal>(111);
        Assert.AreEqual(((Decimal)111).GetHashCode(), structMaybe.GetHashCode());
        Assert.AreNotEqual(((Decimal)222).GetHashCode(), structMaybe.GetHashCode());
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
