#nullable enable

using Ainsworth.Eithers;

using static Ainsworth.Eithers.Tests.TestData;

namespace Maybe_Tests;


/// <summary>
///   Unit tests for <see cref="Maybe.From{T}(T?)"/> methods.
/// </summary>
[TestClass]
public class FromT_Method_Tests {

    /// <summary>
    ///   The <see cref="Maybe.From{T}(T?)"/> method returns a <see cref="Some{T}"/> of the
    ///   correct type wrapping the provided primitive type value.
    /// </summary>
    [TestMethod]
    public void Maybe_From_creates_correct_type_and_value_for_primitive_types() {

        var enumMaybe = Maybe.From<TestEnum>(TestEnum.E11);
        Assert.IsInstanceOfType<Maybe<TestEnum>>(enumMaybe);
        Assert.IsInstanceOfType<Some<TestEnum>>(enumMaybe);
        Assert.AreEqual(TestEnum.E11, (enumMaybe as Some<TestEnum>)!.Value);

        var intMaybe = Maybe.From<int>(111);
        Assert.IsInstanceOfType<Maybe<int>>(intMaybe);
        Assert.IsInstanceOfType<Some<int>>(intMaybe);
        Assert.AreEqual(111, (intMaybe as Some<int>)!.Value);
    }

    /// <summary>
    ///   The <see cref="Maybe.From{T}(T?)"/> method returns a <see cref="Some{T}"/> of the 
    ///   correct wrapping the provided nullable primitive type value.
    /// </summary>
    [TestMethod]
    public void Maybe_From_creates_correct_type_and_value_for_nullable_primitive_types() {

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
    ///   The <see cref="Maybe.From{T}(T?)"/> method returns a <see cref="Some{T}"/> of the 
    ///   correct wrapping the provided value type value.
    /// </summary>
    [TestMethod]
    public void Maybe_From_creates_correct_type_and_value_for_value_types() {

        var tupleMaybe = Maybe.From<(int, string)>(tupleValue);
        Assert.IsInstanceOfType<Maybe<(int, string)>>(tupleMaybe);
        Assert.IsInstanceOfType<Some<(int, string)>>(tupleMaybe);
        Assert.AreEqual(tupleValue, (tupleMaybe as Some<(int, string)>)!.Value);

        var structMaybe = Maybe.From<Decimal>(111);
        Assert.IsInstanceOfType<Maybe<Decimal>>(structMaybe);
        Assert.IsInstanceOfType<Some<Decimal>>(structMaybe);
        Assert.AreEqual(111, (structMaybe as Some<Decimal>)!.Value);
    }

    /// <summary>
    ///   The <see cref="Maybe.From{T}(T?)"/> method returns a <see cref="Some{T}"/> of the 
    ///   correct wrapping the provided nullable value type value.
    /// </summary>
    [TestMethod]
    public void Maybe_From_creates_correct_type_and_value_for_nullable_value_types() {

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
    ///   The <see cref="Maybe.From{T}(T?)"/> method returns a <see cref="Some{T}"/> of the 
    ///   correct wrapping the provided reference type value.
    /// </summary>
    [TestMethod]
    public void Maybe_From_creates_correct_type_and_value_for_reference_types() {

        var stringMaybe = Maybe.From("111");
        Assert.IsInstanceOfType<Maybe<string>>(stringMaybe);
        Assert.IsInstanceOfType<Some<string>>(stringMaybe);
        Assert.AreEqual("111", (stringMaybe as Some<string>)!.Value);

        var arrayMaybe = Maybe.From(arrayValue);
        Assert.IsInstanceOfType<Maybe<int[]>>(arrayMaybe);
        Assert.IsInstanceOfType<Some<int[]>>(arrayMaybe);
        Assert.AreEqual(arrayValue, (arrayMaybe as Some<int[]>)!.Value);

        var classMaybe = Maybe.From(classValue);
        Assert.IsInstanceOfType<Maybe<TestClass>>(classMaybe);
        Assert.IsInstanceOfType<Some<TestClass>>(classMaybe);
        Assert.AreEqual(classValue, (classMaybe as Some<TestClass>)!.Value);
    }

    /// <summary>
    ///   The <see cref="Maybe.From{T}(T?)"/> method returns the correct instance of
    ///   <see cref="Maybe{T}.None"/> for nullable primitive type null value.
    /// </summary>
    [TestMethod]
    public void Maybe_From_creates_correct_type_and_value_for_null_primitive_types() {
        TestEnum? nullableEnum = null;
        var enumMaybe = Maybe.From(nullableEnum);
        Assert.AreSame(Maybe<TestEnum>.None, enumMaybe);

        int? nullableInt = null;
        var intMaybe = Maybe.From(nullableInt);
        Assert.AreSame(Maybe<int>.None, intMaybe);
    }

    /// <summary>
    ///   The <see cref="Maybe.From{T}(T?)"/> method returns the correct instance of
    ///   <see cref="Maybe{T}.None"/> for for nullable primitive type null value.
    /// </summary>
    [TestMethod]
    public void Maybe_From_creates_correct_type_and_value_for_null_value_yypes() {
        (int, string)? nullableTuple = null;
        var tupleMaybe = Maybe.From(nullableTuple);
        Assert.AreSame(Maybe<(int, string)>.None, tupleMaybe);

        Decimal? nullableStruct = null;
        var structMaybe = Maybe.From(nullableStruct);
        Assert.AreSame(Maybe<Decimal>.None, structMaybe);
    }

    /// <summary>
    ///   The <see cref="Maybe.From{T}(T?)"/> method returns the correct instance of
    ///   <see cref="Maybe{T}.None"/> for for nullable primitive type null value.
    /// </summary>
    [TestMethod]
    public void Maybe_From_ReturnsNoneInstance_ForNullReferenceTypes() {
        string? stringValue = null;
        var stringMaybe = Maybe.From(stringValue);
        Assert.AreSame(Maybe<string>.None, stringMaybe);

        int[]? arrayValue = null;
        var arrayMaybe = Maybe.From(arrayValue);
        Assert.AreSame(Maybe<int[]>.None, arrayMaybe);

        TestClass? classValue = null;
        var classMaybe = Maybe.From(classValue);
        Assert.AreSame(Maybe<TestClass>.None, classMaybe);
    }

}

/// <summary>
///   Unit tests for <see cref="Maybe.Some{T}(T)"/> methods.
/// </summary>
[TestClass]
public class SomeT_Method_Tests {

    /// <summary>
    ///   The <see cref="Maybe.Some{T}(T)"/> method returns a <see cref="Some{T}"/> of the
    ///   correct type wrapping the provided primitive type value.
    /// </summary>
    [TestMethod]
    public void Maybe_Some_creates_correct_type_and_value_for_primitive_types() {

        var enumSome = Maybe.Some<TestEnum>(TestEnum.E11);
        Assert.IsInstanceOfType<Maybe<TestEnum>>(enumSome);
        Assert.IsInstanceOfType<Some<TestEnum>>(enumSome);
        Assert.AreEqual(TestEnum.E11, (enumSome as Some<TestEnum>)!.Value);

        var intSome = Maybe.Some<int>(111);
        Assert.IsInstanceOfType<Maybe<int>>(intSome);
        Assert.IsInstanceOfType<Some<int>>(intSome);
        Assert.AreEqual(111, (intSome as Some<int>)!.Value);
    }

    /// <summary>
    ///   The <see cref="Maybe.Some{T}(T)"/> method returns a <see cref="Some{T}"/> of the 
    ///   correct wrapping the provided value type value.
    /// </summary>
    [TestMethod]
    public void Maybe_Some_creates_correct_type_and_value_for_value_types() {

        var tupleSome = Maybe.Some<(int, string)>(tupleValue);
        Assert.IsInstanceOfType<Maybe<(int, string)>>(tupleSome);
        Assert.IsInstanceOfType<Some<(int, string)>>(tupleSome);
        Assert.AreEqual(tupleValue, (tupleSome as Some<(int, string)>)!.Value);

        var structSome = Maybe.Some<Decimal>(111);
        Assert.IsInstanceOfType<Maybe<Decimal>>(structSome);
        Assert.IsInstanceOfType<Some<Decimal>>(structSome);
        Assert.AreEqual(111, (structSome as Some<Decimal>)!.Value);
    }

    /// <summary>
    ///   The <see cref="Maybe.Some{T}(T)"/> method returns a <see cref="Some{T}"/> of the 
    ///   correct wrapping the provided reference type value.
    /// </summary>
    [TestMethod]
    public void Maybe_Some_creates_correct_type_and_value_for_referencetypes() {

        var stringSome = Maybe.Some("111");
        Assert.IsInstanceOfType<Maybe<string>>(stringSome);
        Assert.IsInstanceOfType<Some<string>>(stringSome);
        Assert.AreEqual("111", (stringSome as Some<string>)!.Value);

        var arraySome = Maybe.Some(arrayValue);
        Assert.IsInstanceOfType<Maybe<int[]>>(arraySome);
        Assert.IsInstanceOfType<Some<int[]>>(arraySome);
        Assert.AreEqual(arrayValue, (arraySome as Some<int[]>)!.Value);

        var classSome = Maybe.Some(classValue);
        Assert.IsInstanceOfType<Maybe<TestClass>>(classSome);
        Assert.IsInstanceOfType<Some<TestClass>>(classSome);
        Assert.AreEqual(classValue, (classSome as Some<TestClass>)!.Value);
    }

    /// <summary>
    ///   The <see cref="Maybe.Some{T}(T)"/> method throws an
    ///   <see cref="ArgumentNullException"/> when a non-nullable reference type value
    ///   is <see langword="null"/>. 
    /// </summary>
    /// <remarks>
    ///     <para>This condition will normally be caught by the cC# ompler's null analysis,
    ///   if nullable analysis is enabled (<c>#nullable endable</c>).  However, analysis
    ///   can not prevent <see cref="Maybe.Some{T}(T)"/> from being called with a
    ///   <see langword="null"/> value.</para>
    ///     <para>*Note*: This cannot happen for primitive and value types.</para>
    /// </remarks>
    [TestMethod]
    public void Maybe_Some_throws_for_null_reference_types() {
        string stringValue = null!;
        _ = Assert.ThrowsException<ArgumentNullException>(() => Maybe.Some(stringValue));

        int[] arrayValue = null!;
        _ = Assert.ThrowsException<ArgumentNullException>(() => Maybe.Some(arrayValue));

        TestClass classValue = null!;
        _ = Assert.ThrowsException<ArgumentNullException>(() => Maybe.Some(classValue));
    }
}

/// <summary>
///   Unit tests for <see cref="Maybe.ToMaybe{T}(T?)"/> extention methods.
/// </summary>
[TestClass]
public class ToMaybeT_ExtensionMethod_Tests {

    /// <summary>
    ///   Ensure the ToMaybe() extension method returns a <see cref="Some{T}"/> of the correct
    ///   type wrapping the provided nullable primitive value.
    /// </summary>
    [TestMethod]
    public void Maybe_ToMaybe_returns_correct_type_and_value_for_nullable_primitive_yypes() {

        //TestEnum? nullableEnumValue = TestEnum.E11;
        var enumMaybe = nullableEnumValue.ToMaybe();
        Assert.IsInstanceOfType<Maybe<TestEnum>>(enumMaybe);
        Assert.IsInstanceOfType<Some<TestEnum>>(enumMaybe);
        Assert.AreEqual(TestEnum.E11, (enumMaybe as Some<TestEnum>)!.Value);

        //int? nullableIntValue = 111;
        var intMaybe = nullableInt111.ToMaybe();
        Assert.IsInstanceOfType<Maybe<int>>(intMaybe);
        Assert.IsInstanceOfType<Some<int>>(intMaybe);
        Assert.AreEqual(111, (intMaybe as Some<int>)!.Value);
    }

    /// <summary>
    ///   Ensure the ToMaybe() extension method returns a <see cref="Some{T}"/> of the correct
    ///   type wrapping the provided nullable value type value.
    /// </summary>
    [TestMethod]
    public void Maybe_ToMaybe_returns_correct_type_and_value_for_nullable_value_types() {

        (int, string)? nullableTupleValue = tupleValue;
        var tupleMaybe = nullableTupleValue.ToMaybe();
        Assert.IsInstanceOfType<Maybe<(int, string)>>(tupleMaybe);
        Assert.IsInstanceOfType<Some<(int, string)>>(tupleMaybe);
        Assert.AreEqual(tupleValue, (tupleMaybe as Some<(int, string)>)!.Value);

        Decimal? nullableStructValue = 111;
        var structMaybe = nullableStructValue.ToMaybe();
        Assert.IsInstanceOfType<Maybe<Decimal>>(structMaybe);
        Assert.IsInstanceOfType<Some<Decimal>>(structMaybe);
        Assert.AreEqual(111, (structMaybe as Some<Decimal>)!.Value);
    }

    /// <summary>
    ///   Ensure the ToMaybe() extension method returns a <see cref="Some{T}"/> of the correct
    ///   type wrapping the provided nullable reference type value.
    /// </summary>
    [TestMethod]
    public void Maybe_ToMaybe_returns_correct_type_and_value_for_nullable_reference_types() {

        var stringMaybe = "111".ToMaybe();
        Assert.IsInstanceOfType<Maybe<string>>(stringMaybe);
        Assert.IsInstanceOfType<Some<string>>(stringMaybe);
        Assert.AreEqual("111", (stringMaybe as Some<string>)!.Value);

        var arrayMaybe = arrayValue.ToMaybe();
        Assert.IsInstanceOfType<Maybe<int[]>>(arrayMaybe);
        Assert.IsInstanceOfType<Some<int[]>>(arrayMaybe);
        Assert.AreEqual(arrayValue, (arrayMaybe as Some<int[]>)!.Value);

        var classMaybe = classValue.ToMaybe();
        Assert.IsInstanceOfType<Maybe<TestClass>>(classMaybe);
        Assert.IsInstanceOfType<Some<TestClass>>(classMaybe);
        Assert.AreEqual(classValue, (classMaybe as Some<TestClass>)!.Value);
    }

    /// <summary>
    ///   Ensure the ToSome() extension method returns a <see cref="Some{T}"/> of the correct
    ///   type wrapping the provided nullable primitive value.
    /// </summary>
    [TestMethod]
    public void Maybe_ToSome_returns_correct_type_and_value_for_primitive_types() {

        var enumSome = TestEnum.E11.ToSome();
        Assert.IsInstanceOfType<Maybe<TestEnum>>(enumSome);
        Assert.IsInstanceOfType<Some<TestEnum>>(enumSome);
        Assert.AreEqual(TestEnum.E11, (enumSome as Some<TestEnum>)!.Value);

        var intSome = 111.ToSome();
        Assert.IsInstanceOfType<Maybe<int>>(intSome);
        Assert.IsInstanceOfType<Some<int>>(intSome);
        Assert.AreEqual(111, (intSome as Some<int>)!.Value);
    }

    /// <summary>
    ///   Ensure the ToSome() extension method returns a <see cref="Some{T}"/> of the correct
    ///   type wrapping the provided nullable value type value.
    /// </summary>
    [TestMethod]
    public void Maybe_ToSome_returns_correct_type_and_value_for_value_types() {

        var tupleSome = tupleValue.ToSome();
        Assert.IsInstanceOfType<Maybe<(int, string)>>(tupleSome);
        Assert.IsInstanceOfType<Some<(int, string)>>(tupleSome);
        Assert.AreEqual(tupleValue, (tupleSome as Some<(int, string)>)!.Value);

        var structSome = ((Decimal)111).ToSome();
        Assert.IsInstanceOfType<Maybe<Decimal>>(structSome);
        Assert.IsInstanceOfType<Some<Decimal>>(structSome);
        Assert.AreEqual(111, (structSome as Some<Decimal>)!.Value);
    }

    /// <summary>
    ///   Ensure the ToSome() extension method returns a <see cref="Some{T}"/> of the correct
    ///   type wrapping the provided nullable reference type value.
    /// </summary>
    [TestMethod]
    public void Maybe_ToSome_returns_correct_type_and_value_for_reference_types() {

        var stringSome = "111".ToSome();
        Assert.IsInstanceOfType<Maybe<string>>(stringSome);
        Assert.IsInstanceOfType<Some<string>>(stringSome);
        Assert.AreEqual("111", (stringSome as Some<string>)!.Value);

        var arraySome = arrayValue.ToSome();
        Assert.IsInstanceOfType<Maybe<int[]>>(arraySome);
        Assert.IsInstanceOfType<Some<int[]>>(arraySome);
        Assert.AreEqual(arrayValue, (arraySome as Some<int[]>)!.Value);

        var classSome = classValue.ToSome();
        Assert.IsInstanceOfType<Maybe<TestClass>>(classSome);
        Assert.IsInstanceOfType<Some<TestClass>>(classSome);
        Assert.AreEqual(classValue, (classSome as Some<TestClass>)!.Value);
    }
}
