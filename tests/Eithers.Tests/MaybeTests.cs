using Ainsworth.Eithers;

using TestSupport;
using static TestSupport.TestRunner;

// Disable SonarLint S2699 because most assertions are in called subroutines.
#pragma warning disable S2699 // Test should include assertions
// Disable SonarLint S4144 because it does not consider type constraints.
#pragma warning disable S4144 // Methods should not have identical implementations

namespace Maybe_Tests;

/// <summary>
///   Unit tests for <see cref="Maybe.From{T}(T?)"/> methods.
/// </summary>
[TestClass]
public class From_Tests {

    /// <summary>
    ///   The <see cref="Maybe.From{T}(T?)"/> method returns a <see cref="Some{T}"/>
    ///   wrapping the provided nullable value.
    /// </summary>
    [TestMethod]
    public void Maybe_From_creates_Some_from_nullable_value() =>
        RunUnitTests(new From_creates_Some_from_value());

    private sealed class From_creates_Some_from_value : IUnitTest1Split {
        public void RunTestOnReferenceType<T>(T value) where T : class {
            var maybe = Maybe.From((T?)value);
            Assert.IsInstanceOfType<Maybe<T>>(maybe);
            Assert.IsInstanceOfType<Some<T>>(maybe);
            var some = (maybe as Some<T>)!;
            Assert.AreEqual(value, some.Value);
        }
        public void RunTestOnValueType<T>(T value) where T : struct {
            var maybe = Maybe.From((T?)value);
            Assert.IsInstanceOfType<Maybe<T>>(maybe);
            Assert.IsInstanceOfType<Some<T>>(maybe);
            var some = (maybe as Some<T>)!;
            Assert.AreEqual(value, some.Value);
        }
    }

    /// <summary>
    ///   The <see cref="Maybe.From{T}(T?)"/> method returns the correct instance of
    ///   <see cref="Maybe{T}.None"/> for the provided null value.
    /// </summary>
    [TestMethod]
    public void Maybe_From_returns_None_from_null() =>
        RunUnitTests(new From_returns_None_from_null());

    private sealed class From_returns_None_from_null : IUnitTest0Split {
        public void RunTestOnReferenceType<T>() where T : class {
            var maybe = Maybe.From((T?)null);
            Assert.IsInstanceOfType<Maybe<T>>(maybe);
            Assert.IsInstanceOfType<None<T>>(maybe);
            Assert.AreSame(Maybe<T>.None, maybe);
        }
        public void RunTestOnValueType<T>() where T : struct {
            var maybe = Maybe.From((T?)null);
            Assert.IsInstanceOfType<Maybe<T>>(maybe);
            Assert.IsInstanceOfType<None<T>>(maybe);
            Assert.AreSame(Maybe<T>.None, maybe);
        }
    }
}

/// <summary>
///   Unit tests for <see cref="Maybe.FromValue{T}(T)"/> methods.
/// </summary>
[TestClass]
public class FromValue__Tests {

    /// <summary>
    ///   The <see cref="Maybe.FromValue{T}(T)"/> method returns a <see cref="Some{T}"/> of the
    ///   correct type wrapping the provided primitive type value.
    /// </summary>
    [TestMethod]
    public void Maybe_FromValue_creates_Some_from_value() =>
        RunUnitTests(new FromValue_creates_Some_from_value());

    private sealed class FromValue_creates_Some_from_value : IUnitTest1Split {
        public void RunTestOnReferenceType<T>(T value) where T : class {
            var some = Maybe.FromValue(value);
            Assert.IsInstanceOfType<Maybe<T>>(some);
            Assert.IsInstanceOfType<Some<T>>(some);
            Assert.AreEqual(value, some.Value);
        }
        public void RunTestOnValueType<T>(T value) where T : struct {
            var some = Maybe.FromValue(value);
            Assert.IsInstanceOfType<Maybe<T>>(some);
            Assert.IsInstanceOfType<Some<T>>(some);
            Assert.AreEqual(value, some.Value);
        }
    }

    /// <summary>
    ///   The <see cref="Maybe.FromValue{T}(T)"/> method throws an
    ///   <see cref="ArgumentNullException"/> when a non-nullable reference type value
    ///   is <see langword="null"/>.
    /// </summary>
    /// <remarks>
    ///     <para>This condition will normally be caught by the C# compiler's null analysis,
    ///   if nullable analysis is enabled (<c>#nullable enable</c>).  However, analysis
    ///   can not prevent <see cref="Maybe.FromValue{T}(T)"/> from being called with a
    ///   <see langword="null"/> value.</para>
    ///     <para>*Note*: This cannot happen for primitive and value types.</para>
    /// </remarks>
    [TestMethod]
    public void Maybe_FromValue_throws_for_null_reference_types() =>
        RunUnitTests(new FromValue_throws_for_null());

    private sealed class FromValue_throws_for_null : IUnitTest0Split {
        public void RunTestOnReferenceType<T>() where T : class {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => Maybe.FromValue<T>(null!));
            Assert.AreEqual("value", ex.ParamName);
        }
        public void RunTestOnValueType<T>() where T : struct {
            // This can only be executed for reference types
        }
    }
}

/// <summary>
///   Unit tests for <see cref="Maybe.ToMaybe{T}(T?)"/> extension methods.
/// </summary>
[TestClass]
public class ToMaybe_Tests {

    /// <summary>
    ///   Ensure the ToMaybe() extension method returns a <see cref="Some{T}"/> of the correct
    ///   type wrapping the provided nullable value.
    /// </summary>
    [TestMethod]
    public void Maybe_ToMaybe_creates_Some_for_nullable_value_() =>
        RunUnitTests(new ToMaybe_creates_Some_from_nullable_value());

    private sealed class ToMaybe_creates_Some_from_nullable_value : IUnitTest1Split {
        public void RunTestOnReferenceType<T>(T value) where T : class {
            var maybe = value.ToMaybe();
            Assert.IsInstanceOfType<Maybe<T>>(maybe);
            Assert.IsInstanceOfType<Some<T>>(maybe);
            var some = (Some<T>)maybe;
            Assert.AreEqual(value, some.Value);
        }
        public void RunTestOnValueType<T>(T value) where T : struct {
            var maybe = ((T?)value).ToMaybe();
            Assert.IsInstanceOfType<Maybe<T>>(maybe);
            Assert.IsInstanceOfType<Some<T>>(maybe);
            var some = (Some<T>)maybe;
            Assert.AreEqual(value, some.Value);
        }
    }

    /// <summary>
    ///   The <see cref="Maybe.ToMaybe{T}(T?)"/> method returns the correct instance of
    ///   <see cref="Maybe{T}.None"/> for null value.
    /// </summary>
    [TestMethod]
    public void Maybe_ToMaybe_returns_None_for_null() =>
        RunUnitTests(new ToMaybe_returns_None_for_null());

    private sealed class ToMaybe_returns_None_for_null : IUnitTest0Split {
        public void RunTestOnReferenceType<T>() where T : class {
            var maybe = ((T?)null).ToMaybe();
            Assert.IsInstanceOfType<Maybe<T>>(maybe);
            Assert.IsInstanceOfType<None<T>>(maybe);
            Assert.AreSame(Maybe<T>.None, maybe);
        }
        public void RunTestOnValueType<T>() where T : struct {
            var maybe = ((T?)null).ToMaybe();
            Assert.IsInstanceOfType<Maybe<T>>(maybe);
            Assert.IsInstanceOfType<None<T>>(maybe);
            Assert.AreSame(Maybe<T>.None, maybe);
        }
    }
}

/// <summary>
///   Unit tests for <see cref="Maybe.ToMaybe{T}(T?)"/> extension methods.
/// </summary>
[TestClass]
public class ToSome_Tests {

    /// <summary>
    ///   Ensure the ToSome() extension method returns a <see cref="Some{T}"/> of the correct
    ///   type wrapping the provided nullable primitive value.
    /// </summary>
    [TestMethod]
    public void Maybe_ToSome_returns_Some_for_primitive_types() =>
        RunUnitTests(new ToSome_creates_Some_from_value());

    private sealed class ToSome_creates_Some_from_value : IUnitTest1Split {
        public void RunTestOnReferenceType<T>(T value) where T : class {
            var some = value.ToSome();
            Assert.IsInstanceOfType<Maybe<T>>(some);
            Assert.IsInstanceOfType<Some<T>>(some);
            Assert.AreEqual(value, some.Value);
        }
        public void RunTestOnValueType<T>(T value) where T : struct {
            var some = value.ToSome();
            Assert.IsInstanceOfType<Maybe<T>>(some);
            Assert.IsInstanceOfType<Some<T>>(some);
            Assert.AreEqual(value, some.Value);
        }
    }

    /// <summary>
    ///   The <see cref="Maybe.ToSome{T}(T)"/> method throws an
    ///   <see cref="ArgumentNullException"/> when a non-nullable reference type value
    ///   is <see langword="null"/>.
    /// </summary>
    /// <remarks>
    ///     <para>This condition will normally be caught by the C# compiler's null analysis,
    ///   if nullable analysis is enabled (<c>#nullable enable</c>).  However, analysis
    ///   can not prevent <see cref="Maybe.FromValue{T}(T)"/> from being called with a
    ///   <see langword="null"/> value.</para>
    ///     <para>*Note*: This cannot happen for primitive and value types.</para>
    /// </remarks>
    [TestMethod]
    public void Maybe_ToSome_throws_for_null_reference_types() =>
        RunUnitTests(new ToSome_throws_for_null());

    private sealed class ToSome_throws_for_null : IUnitTest0Split {
        public void RunTestOnReferenceType<T>() where T : class {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => ((T)null!).ToSome());
            Assert.AreEqual("value", ex.ParamName);
        }
        public void RunTestOnValueType<T>() where T : struct {
            // This is not allowed by C# semantics
        }
    }
}
