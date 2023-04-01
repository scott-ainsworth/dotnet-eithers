using Ainsworth.Eithers;
using System.Diagnostics.CodeAnalysis;
using static Ainsworth.Eithers.Tests.TestData;

namespace Maybe_Tests;

/// <summary>
///   Unit tests for <see cref="Maybe.From{T}(T?)"/> methods.
/// </summary>
[TestClass]
public class FromT_Method_Tests {

    #region test implementions

    // Value type versions

    private static void Maybe_FromT_creates_Some_from_value<T>(T value) where T : struct {
        var maybe = Maybe.From<T>(value);
        Assert.IsInstanceOfType<Maybe<T>>(maybe);
        Assert.IsInstanceOfType<Some<T>>(maybe);
        var some = (maybe as Some<T>)!;
        Assert.AreEqual(value, some.Value);
    }

    private static void Maybe_FromT_creates_Some_from_nullable_value<T>(T? value) where T : struct {
        var maybe = Maybe.From(value);
        Assert.IsInstanceOfType<Maybe<T>>(maybe);
        Assert.IsInstanceOfType<Some<T>>(maybe);
        var some = (maybe as Some<T>)!;
        Assert.AreEqual(value, some.Value);
    }

    // Reference type versions

    [SuppressMessage(
        "Major Code Smell", "S4144:Methods should not have identical implementations",
        Justification = "S4144 is incorrect because T type constraints differ")]
    private static void Maybe_FromT_creates_Some_from_reference_value<T>(T? value) where T : class {
        var maybe = Maybe.From(value);
        Assert.IsInstanceOfType<Maybe<T>>(maybe);
        Assert.IsInstanceOfType<Some<T>>(maybe);
        var some = (maybe as Some<T>)!;
        Assert.AreEqual(value, some.Value);
    }

    private static void Maybe_FromT_creates_None_from_null<T>(T? value) where T : struct {
        var maybe = Maybe.From(value);
        Assert.IsInstanceOfType<Maybe<T>>(maybe);
        Assert.IsInstanceOfType<None<T>>(maybe);
        Assert.AreSame(Maybe<T>.None, maybe);
    }

    private static void Maybe_FromT_creates_None_from_null<T>(T? value) where T : class {
        var maybe = Maybe.From(value);
        Assert.IsInstanceOfType<Maybe<T>>(maybe);
        Assert.IsInstanceOfType<None<T>>(maybe);
        Assert.AreSame(Maybe<T>.None, maybe);
    }

    #endregion

    /// <summary>
    ///   The <see cref="Maybe.From{T}(T?)"/> method returns a <see cref="Some{T}"/> of the
    ///   correct type wrapping the provided primitive type value.
    /// </summary>
    [TestMethod]
    public void Maybe_FromT_creates_Some_from_primitive_type_value() {
        Maybe_FromT_creates_Some_from_value(TestEnum.E11);
        Maybe_FromT_creates_Some_from_value(111);
    }

    /// <summary>
    ///   The <see cref="Maybe.From{T}(T?)"/> method returns a <see cref="Some{T}"/> of the 
    ///   correct wrapping the provided nullable primitive type value.
    /// </summary>
    [TestMethod]
    public void Maybe_FromT_creates_Some_from_nullable_primitive_type_value() {
        Maybe_FromT_creates_Some_from_nullable_value((TestEnum?)TestEnum.E11);
        Maybe_FromT_creates_Some_from_nullable_value((int?)100);
    }

    /// <summary>
    ///   The <see cref="Maybe.From{T}(T?)"/> method returns a <see cref="Some{T}"/> of the 
    ///   correct wrapping the provided value type value.
    /// </summary>
    [TestMethod]
    public void Maybe_FromT_creates_Some_from_value_type_value() {
        Maybe_FromT_creates_Some_from_value((111, "111"));
        Maybe_FromT_creates_Some_from_value((decimal)111);
    }

    /// <summary>
    ///   The <see cref="Maybe.From{T}(T?)"/> method returns a <see cref="Some{T}"/> of the 
    ///   correct wrapping the provided nullable value type value.
    /// </summary>
    [TestMethod]
    public void Maybe_FromT_creates_Some_from_nullable_value_type_value() {
        Maybe_FromT_creates_Some_from_nullable_value(((int, string)?)(111, "111"));
        Maybe_FromT_creates_Some_from_nullable_value((decimal?)111);
    }

    /// <summary>
    ///   The <see cref="Maybe.From{T}(T?)"/> method returns a <see cref="Some{T}"/> of the 
    ///   correct wrapping the provided reference type value.
    /// </summary>
    [TestMethod]
    public void Maybe_FromT_creates_Some_from_reference_type_value() {
        Maybe_FromT_creates_Some_from_reference_value("111");
        Maybe_FromT_creates_Some_from_reference_value(new int[] { 111 });
        Maybe_FromT_creates_Some_from_reference_value(new TestClass(111, "111"));
    }

    /// <summary>
    ///   The <see cref="Maybe.From{T}(T?)"/> method returns the correct instance of
    ///   <see cref="Maybe{T}.None"/> for nullable primitive type null value.
    /// </summary>
    [TestMethod]
    public void Maybe_FromT_creates_None_from_primitive_type_null() {
        Maybe_FromT_creates_None_from_null((TestEnum?)null);
        Maybe_FromT_creates_None_from_null((int?)null);
    }

    /// <summary>
    ///   The <see cref="Maybe.From{T}(T?)"/> method returns the correct instance of
    ///   <see cref="Maybe{T}.None"/> for for nullable primitive type null value.
    /// </summary>
    [TestMethod]
    public void Maybe_FromT_creates_None_from_value_type_null() {
        Maybe_FromT_creates_None_from_null(((int, string)?)null);
        Maybe_FromT_creates_None_from_null((decimal?)null);
    }

    /// <summary>
    ///   The <see cref="Maybe.From{T}(T?)"/> method returns the correct instance of
    ///   <see cref="Maybe{T}.None"/> for for nullable primitive type null value.
    /// </summary>
    [TestMethod]
    public void Maybe_FromT_creates_None_from_reference_type_null() {
        Maybe_FromT_creates_None_from_null((string)null!);
        Maybe_FromT_creates_None_from_null((int[])null!);
        Maybe_FromT_creates_None_from_null((TestClass?)null!);
    }
}

/// <summary>
///   Unit tests for <see cref="Maybe.Some{T}(T)"/> methods.
/// </summary>
[TestClass]
public class SomeT_Method_Tests {

    #region test implementions

    // Value type versions

    private static void Maybe_SomeT_creates_Some_from_value<T>(T value) where T : notnull {
        var maybe = Maybe.Some(value);
        Assert.IsInstanceOfType<Maybe<T>>(maybe);
        Assert.IsInstanceOfType<Some<T>>(maybe);
        var some = (maybe as Some<T>)!;
        Assert.AreEqual(value, some.Value);
    }

    private static void Maybe_SomeT_throws_for_null<T>() where T : class =>
        Assert.ThrowsException<ArgumentNullException>(() => Maybe.Some<T>(null!));

    #endregion


    /// <summary>
    ///   The <see cref="Maybe.Some{T}(T)"/> method returns a <see cref="Some{T}"/> of the
    ///   correct type wrapping the provided primitive type value.
    /// </summary>
    [TestMethod]
    public void Maybe_SomeT_creates_Some_for_primitive_types() {
        Maybe_SomeT_creates_Some_from_value(TestEnum.E11);
        Maybe_SomeT_creates_Some_from_value(111);
    }

    /// <summary>
    ///   The <see cref="Maybe.Some{T}(T)"/> method returns a <see cref="Some{T}"/> of the 
    ///   correct wrapping the provided value type value.
    /// </summary>
    [TestMethod]
    public void Maybe_SomeT_creates_Some_for_value_types() {
        Maybe_SomeT_creates_Some_from_value((111, "111"));
        Maybe_SomeT_creates_Some_from_value((decimal)111);
    }

    /// <summary>
    ///   The <see cref="Maybe.Some{T}(T)"/> method returns a <see cref="Some{T}"/> of the 
    ///   correct wrapping the provided reference type value.
    /// </summary>
    [TestMethod]
    public void Maybe_SomeT_creates_Some_for_reference_types() {
        Maybe_SomeT_creates_Some_from_value("111");
        Maybe_SomeT_creates_Some_from_value(new int[] { 111 });
        Maybe_SomeT_creates_Some_from_value(new TestClass(111, "111"));
    }

    /// <summary>
    ///   The <see cref="Maybe.Some{T}(T)"/> method throws an
    ///   <see cref="ArgumentNullException"/> when a non-nullable reference type value
    ///   is <see langword="null"/>. 
    /// </summary>
    /// <remarks>
    ///     <para>This condition will normally be caught by the C# compiler's null analysis,
    ///   if nullable analysis is enabled (<c>#nullable enable</c>).  However, analysis
    ///   can not prevent <see cref="Maybe.Some{T}(T)"/> from being called with a
    ///   <see langword="null"/> value.</para>
    ///     <para>*Note*: This cannot happen for primitive and value types.</para>
    /// </remarks>
    [TestMethod]
    public void Maybe_SomeT_throws_for_null_reference_types() {
        Maybe_SomeT_throws_for_null<string>();
        Maybe_SomeT_throws_for_null<int[]>();
        Maybe_SomeT_throws_for_null<TestClass>();
    }
}

/// <summary>
///   Unit tests for <see cref="Maybe.ToMaybe{T}(T?)"/> extension methods.
/// </summary>
[TestClass]
public class ToMaybeT_ExtensionMethod_Tests {

    #region test implementions

    private static void Maybe_ToMaybeT_creates_Some_from_reference_value<T>(T? value) where T : class {
        var maybe = value.ToMaybe();
        Assert.IsInstanceOfType<Maybe<T>>(maybe);
        Assert.IsInstanceOfType<Some<T>>(maybe);
        var some = (maybe as Some<T>)!;
        Assert.AreEqual(value, some.Value);
    }

    private static void Maybe_ToMaybeT_creates_None_from_null<T>(T? value) where T : struct {
        var maybe = value.ToMaybe();
        Assert.IsInstanceOfType<Maybe<T>>(maybe);
        Assert.IsInstanceOfType<None<T>>(maybe);
        Assert.AreSame(Maybe<T>.None, maybe);
    }

    private static void Maybe_ToMaybeT_creates_None_from_null<T>(T? value) where T : class {
        var maybe = value.ToMaybe();
        Assert.IsInstanceOfType<Maybe<T>>(maybe);
        Assert.IsInstanceOfType<None<T>>(maybe);
        Assert.AreSame(Maybe<T>.None, maybe);
    }

    #endregion

    /// <summary>
    ///   Ensure the ToMaybe() extension method returns a <see cref="Some{T}"/> of the correct
    ///   type wrapping the provided nullable primitive value.
    /// </summary>
    [TestMethod]
    [SuppressMessage(
        "Blocker Code Smell", "S2699:Tests should include assertions",
        Justification = "This method simply provides documentation for the next developer")]
    public void Maybe_ToMaybe_returns_Some_for_nullable_primitive_types() {
        // This is not possible. Use ToSome<T>(T) instead.
    }

    /// <summary>
    ///   Ensure the ToMaybe() extension method returns a <see cref="Some{T}"/> of the correct
    ///   type wrapping the provided nullable value type value.
    /// </summary>
    [TestMethod]
    [SuppressMessage(
        "Blocker Code Smell", "S2699:Tests should include assertions",
        Justification = "This method simply provides documentation for the next developer")]
    public void Maybe_ToMaybe_returns_Some_for_nullable_value_types() {
        // This is not possible. Use ToSome<T>(T) instead.
    }

    /// <summary>
    ///   Ensure the ToMaybe() extension method returns a <see cref="Some{T}"/> of the correct
    ///   type wrapping the provided nullable reference type value.
    /// </summary>
    [TestMethod]
    public void Maybe_ToMaybe_returns_Some_for_nullable_reference_types() {
        Maybe_ToMaybeT_creates_Some_from_reference_value("111");
        Maybe_ToMaybeT_creates_Some_from_reference_value(new int[] { 111 });
        Maybe_ToMaybeT_creates_Some_from_reference_value(new TestClass(111, "111"));
    }

    /// <summary>
    ///   The <see cref="Maybe.ToMaybe{T}(T?)"/> method returns the correct instance of
    ///   <see cref="Maybe{T}.None"/> for nullable primitive type null value.
    /// </summary>
    [TestMethod]
    public void Maybe_ToMaybe_creates_None_from_primitive_type_null() {
        Maybe_ToMaybeT_creates_None_from_null((TestEnum?)null);
        Maybe_ToMaybeT_creates_None_from_null((int?)null);
    }

    /// <summary>
    ///   The <see cref="Maybe.ToMaybe{T}(T?)"/> method returns the correct instance of
    ///   <see cref="Maybe{T}.None"/> for for nullable primitive type null value.
    /// </summary>
    [TestMethod]
    public void Maybe_ToMaybe_creates_None_from_value_type_null() {
        Maybe_ToMaybeT_creates_None_from_null(((int, string)?)null);
        Maybe_ToMaybeT_creates_None_from_null((decimal?)null);
    }

    /// <summary>
    ///   The <see cref="Maybe.ToMaybe{T}(T?)"/> method returns the correct instance of
    ///   <see cref="Maybe{T}.None"/> for for nullable primitive type null value.
    /// </summary>
    [TestMethod]
    public void Maybe_ToMaybe_creates_None_from_reference_type_null() {
        Maybe_ToMaybeT_creates_None_from_null((string)null!);
        Maybe_ToMaybeT_creates_None_from_null((int[])null!);
        Maybe_ToMaybeT_creates_None_from_null((TestClass?)null!);
    }
}

/// <summary>
///   Unit tests for <see cref="Maybe.ToMaybe{T}(T?)"/> extension methods.
/// </summary>
[TestClass]
public class ToSomeT_ExtensionMethod_Tests {

    #region test implementions

    private static void Maybe_ToSomeT_creates_Some_from_value<T>(T value) where T : notnull {
        var some = value.ToSome();
        Assert.IsInstanceOfType<Maybe<T>>(some);
        Assert.IsInstanceOfType<Some<T>>(some);
        Assert.AreEqual(value, some.Value);
    }

    private static void Maybe_ToSomeT_throws_for_null<T>() where T : class =>
        Assert.ThrowsException<ArgumentNullException>(() => ((T)null!).ToSome());

    #endregion

    /// <summary>
    ///   Ensure the ToSome() extension method returns a <see cref="Some{T}"/> of the correct
    ///   type wrapping the provided nullable primitive value.
    /// </summary>
    [TestMethod]
    public void Maybe_ToSome_returns_Some_for_primitive_types() {
        Maybe_ToSomeT_creates_Some_from_value(TestEnum.E11);
        Maybe_ToSomeT_creates_Some_from_value(111);
    }

    /// <summary>
    ///   Ensure the ToSome() extension method returns a <see cref="Some{T}"/> of the correct
    ///   type wrapping the provided nullable value type value.
    /// </summary>
    [TestMethod]
    public void Maybe_ToSome_returns_Some_for_value_types() {
        Maybe_ToSomeT_creates_Some_from_value((111, "111"));
        Maybe_ToSomeT_creates_Some_from_value((decimal)111);
    }

    /// <summary>
    ///   Ensure the ToSome() extension method returns a <see cref="Some{T}"/> of the correct
    ///   type wrapping the provided nullable reference type value.
    /// </summary>
    [TestMethod]
    public void Maybe_ToSome_returns_Some_for_reference_types() {
        Maybe_ToSomeT_creates_Some_from_value("111");
        Maybe_ToSomeT_creates_Some_from_value(new int[] { 111 });
        Maybe_ToSomeT_creates_Some_from_value(new TestClass(111, "111"));
    }

    /// <summary>
    ///   The <see cref="Maybe.ToSome{T}(T)"/> method throws an
    ///   <see cref="ArgumentNullException"/> when a non-nullable reference type value
    ///   is <see langword="null"/>. 
    /// </summary>
    /// <remarks>
    ///     <para>This condition will normally be caught by the C# compiler's null analysis,
    ///   if nullable analysis is enabled (<c>#nullable enable</c>).  However, analysis
    ///   can not prevent <see cref="Maybe.Some{T}(T)"/> from being called with a
    ///   <see langword="null"/> value.</para>
    ///     <para>*Note*: This cannot happen for primitive and value types.</para>
    /// </remarks>
    [TestMethod]
    public void Maybe_ToSomeT_throws_for_null_reference_types() {
        Maybe_ToSomeT_throws_for_null<string>();
        Maybe_ToSomeT_throws_for_null<int[]>();
        Maybe_ToSomeT_throws_for_null<TestClass>();
    }
}
