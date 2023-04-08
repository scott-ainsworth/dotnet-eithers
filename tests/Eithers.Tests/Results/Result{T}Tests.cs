using System.Collections;
using System.Reflection;

using Ainsworth.Eithers.Results;

using TestSupport;
using static TestSupport.TestRunner;

// Disable SonarLint S2699 because most assertions are in called subroutines.
#pragma warning disable S2699 // Test should include assertions

namespace ResultT_Tests;

/// <summary>
///   Unit tests for <see cref="Result{T}"/> constructors.
/// </summary>
[TestClass]
public class Constructor_Tests {

    /// <summary>
    ///   The <see cref="Result{T}"/> constructors' visibility is not public.
    ///   This is a design assumptions check.
    /// </summary>
    [TestMethod]
    public void ResultT_constructors_are_protected() =>
        RunUnitTests(new Constructors_are_protected());

    private sealed class Constructors_are_protected : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            var type = typeof(Result<T>);
            var publicConstructors = type.GetConstructors(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            Assert.IsFalse(
                publicConstructors.Any(),
                $"{type.Name} has at least 1 public constructor");
        }
    }
}

/// <summary>
///   Unit tests for <see cref="Result{T}.GetEnumerator"/>.
/// </summary>
[TestClass]
public class GetEnumerator_Tests {

    /// <summary>
    ///   The <see cref="Result{T}.GetEnumerator"/> methods return a correct
    ///   <see cref="IEnumerator{T}"/> for a <see cref="Result{T}"/> wrapping a value.
    /// </summary>
    [TestMethod]
    public void ResultT_GetEnumerator_returns_correct_enumerator_for_wrapped_value() =>
        RunUnitTests(new GetEnumerator_returns_correct_enumerator_for_wrapped_value());

    private sealed class GetEnumerator_returns_correct_enumerator_for_wrapped_value : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From(value);
            var enumerator = result.GetEnumerator();
            Assert.IsInstanceOfType<IEnumerator<T>>(enumerator);
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(value, enumerator.Current);
            Assert.IsFalse(enumerator.MoveNext());
        }
    }

    /// <summary>
    ///   The <see cref="Result{T}.GetEnumerator"/> methods return a correct
    ///   <see cref="IEnumerator{T}"/> for a <see cref="Result{T}"/> wrapping an error.
    /// </summary>
    [TestMethod]
    public void ResultT_GetEnumerator_returns_correct_enumerator_for_ResultT_wrapping_error() =>
        RunUnitTests(new GetEnumerator_returns_correct_enumerator_for_wrapped_error());

    private sealed class GetEnumerator_returns_correct_enumerator_for_wrapped_error : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            var result = Result.From<T>(new ArgumentException("test"));
            var enumerator = result.GetEnumerator();
            Assert.IsInstanceOfType<IEnumerator<T>>(enumerator);
            Assert.IsFalse(enumerator.MoveNext());
        }
    }

    /// <summary>
    ///   The <see cref="Result{T}.GetEnumerator"/> methods return a correct
    ///   <see cref="IEnumerator{T}"/> for a <see cref="Result{T}"/> wrapping a value.
    /// </summary>
    [TestMethod]
    public void ResultT_IEnumerator_GetEnumerator_returns_correct_enumerator_for_wrapped_value() =>
        RunUnitTests(
            new IEnumerator_GetEnumerator_returns_correct_enumerator_for_wrapped_value());

    private sealed class IEnumerator_GetEnumerator_returns_correct_enumerator_for_wrapped_value
            : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From(value);
            var enumerator = ((IEnumerable)result).GetEnumerator();
            Assert.IsInstanceOfType<IEnumerator>(enumerator);
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(value, enumerator.Current);
            Assert.IsFalse(enumerator.MoveNext());
        }
    }

}

/// <summary>
///   Unit tests for <see cref="Result{T}.GetEnumerator"/>.
/// </summary>
[TestClass]
public class GetErrorEnumerator_Tests {

    /// <summary>
    ///   The <see cref="Result{T}.GetErrorEnumerator"/> methods return an empty
    ///   <see cref="IEnumerator{T}"/> for a <see cref="Result{T}"/> wrapping a value.
    /// </summary>
    [TestMethod]
    public void ResultT_GetErrorEnumerator_returns_empty_enumerator_for_wrapped_value() =>
        RunUnitTests(
            new GetErrorEnumerator_returns_correct_enumerator_for_wrapped_value());

    private sealed class GetErrorEnumerator_returns_correct_enumerator_for_wrapped_value
            : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From(value);
            var enumerator = result.GetErrorEnumerator();
            Assert.IsInstanceOfType<IEnumerator<Exception>>(enumerator);
            Assert.IsFalse(enumerator.MoveNext());
        }
    }

    /// <summary>
    ///   The <see cref="Result{T}.GetErrorEnumerator"/> methods return a correct
    ///   <see cref="IEnumerator{T}"/> for a <see cref="Result{T}"/> wrapping an error.
    /// </summary>
    [TestMethod]
    public void ResultT_GetErrorEnumerator_returns_correct_enumerator_for_wrapped_error() =>
        RunUnitTests(
            new GetErrorEnumerator_returns_correct_enumerator_for_wrapped_error());

    private sealed class GetErrorEnumerator_returns_correct_enumerator_for_wrapped_error
            : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            var ex = new ArgumentException("test");
            var result = Result.From<T>(ex);
            var enumerator = result.GetErrorEnumerator();
            Assert.IsInstanceOfType<IEnumerator<Exception>>(enumerator);
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(ex, enumerator.Current);
            Assert.IsFalse(enumerator.MoveNext());
        }
    }

}

/// <summary>
///   Unit tests for <see cref="Result{T}"/>.GetHashCode().
/// </summary>
[TestClass]
public class GetHashCode_Tests {

    /// <summary>
    ///   The <see cref="object.GetHashCode"/> method returns the correct value
    ///   for <see cref="Result{T}"/>s wrapping values.
    /// </summary>
    [TestMethod]
    public void ResultT_GetHashCode_returns_correct_value_wrapped_values() =>
        RunUnitTests(new GetHashCode_returns_correct_value_for_wrapped_values());

    private sealed class GetHashCode_returns_correct_value_for_wrapped_values : IUnitTest2 {
        public void RunTest<T>(T value, T value2) where T : notnull {
            var result = Result.From(value);
            Assert.AreEqual(value.GetHashCode(), result.GetHashCode());
            Assert.AreNotEqual(value2.GetHashCode(), result.GetHashCode());
        }
    }

    /// <summary>
    ///   The <see cref="object.GetHashCode"/> method returns the correct value
    ///   for <see cref="Result{T}"/>s wrapping values.
    /// </summary>
    [TestMethod]
    public void ResultT_GetHashCode_returns_correct_value_for_wrapped_errors() =>
        RunUnitTests(new GetHashCode_returns_correct_value_for_wrapped_errors());

    private sealed class GetHashCode_returns_correct_value_for_wrapped_errors : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var ex = new ArgumentException("test");
            var result = Result.From<T>(ex);
            Assert.AreEqual(result.GetHashCode(), ex.GetHashCode());
            var result2 = Result.From<T>(new ArgumentException("test"));
            Assert.AreNotEqual(result.GetHashCode(), result2.GetHashCode());
            var result3 = Result.From(value);
            Assert.AreNotEqual(result.GetHashCode(), result3.GetHashCode());
        }
    }
}

/// <summary>
///   Unit tests for <see cref="Result{T}.IsValue"/>.
/// </summary>
[TestClass]
public class IsValue_and_IsError_Property_Tests {

    /// <summary>
    ///   The <see cref="Result{T}.IsError"/> property returns <see langword="false"/>
    ///   for a <see cref="Result{T}"/> wrapping a value.
    /// </summary>
    [TestMethod]
    public void ResultT_IsError_returns_false_for_wrapped_value() =>
        RunUnitTests(new IsError_returns_false_for_wrapped_value());

    private sealed class IsError_returns_false_for_wrapped_value : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From(value);
            Assert.IsFalse(result.IsError);
        }
    }

    /// <summary>
    ///   The <see cref="Result{T}.IsError"/> property returns <see langword="true"/>
    ///   for a <see cref="Result{T}"/>s wrapping an error.
    /// </summary>
    [TestMethod]
    public void ResultT_IsError_returns_true_for_wrapped_error() =>
        RunUnitTests(new IsError_returns_true_for_wrapped_error());

    private sealed class IsError_returns_true_for_wrapped_error : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            var result = Result.From<T>(new ArgumentException("test"));
            Assert.IsTrue(result.IsError);
        }
    }

    /// <summary>
    ///   The <see cref="Result{T}.IsValue"/> property returns <see langword="false"/>
    ///   for a <see cref="Result{T}"/>s wrapping an error.
    /// </summary>
    [TestMethod]
    public void ResultT_IsValue_returns_false_for_wrapped_error() =>
        RunUnitTests(new IsValue_returns_false_for_wrapped_error());

    private sealed class IsValue_returns_false_for_wrapped_error : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            var result = Result.From<T>(new ArgumentException("test"));
            Assert.IsFalse(result.IsValue);
        }
    }

    /// <summary>
    ///   The <see cref="Result{T}.IsValue"/> property returns <see langword="true"/>
    ///   for a <see cref="Result{T}"/> wrapping a value.
    /// </summary>
    [TestMethod]
    public void ResultT_IsValue_returns_true_for_wrapped_value() =>
        RunUnitTests(new IsValue_returns_true_for_wrapped_value());

    private sealed class IsValue_returns_true_for_wrapped_value : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From(value);
            Assert.IsTrue(result.IsValue);
        }
    }
}

/// <summary>
///   Unit tests for <see cref="ValueResult{T}.ToValue"/>.
/// </summary>
[TestClass]
public class ToValue_Tests {

    /// <summary>
    ///   The <see cref="ValueResult{T}.ToValue"/> method returns the value provided
    ///   when the instance was created.
    /// </summary>
    [TestMethod]
    public void ResultT_ToValue_returns_correct_value() =>
        RunUnitTests(new ToValue_returns_correct_value());

    private sealed class ToValue_returns_correct_value : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From(value);
            Assert.AreEqual(value, result.ToValue());
        }
    }

    /// <summary>
    ///   The <see cref="ValueResult{T}.ToValue"/> method should only be implemented
    ///   on <see cref="ValueResult{T}"/> (not on <see cref="ErrorResult{T}"/>).
    ///   This is a design assumptions check.
    /// </summary>
    [TestMethod]
    public void ResultT_ToValue_is_only_available_on_ValueResult() =>
        RunUnitTests(new ToValue_is_only_available_on_ValueResult());

    private sealed class ToValue_is_only_available_on_ValueResult : IUnitTest1 {

        private const BindingFlags flags =
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        public void RunTest<T>(T value) where T : notnull {
            var toValueName = nameof(ValueResult<T>.ToValue);
            var resultMethods =
                typeof(Result<T>).GetMethods(flags).Where(m => m.Name == toValueName);
            Assert.IsFalse(resultMethods.Any());
            var valueMethods =
                typeof(ValueResult<T>).GetMethods(flags).Where(m => m.Name == toValueName);
            Assert.IsTrue(valueMethods.Any());
            var errorMethods =
                typeof(ErrorResult<T>).GetMethods(flags).Where(m => m.Name == toValueName);
            Assert.IsFalse(errorMethods.Any());
        }
    }
}

/// <summary>
///   Unit tests for <see cref="ValueResult{T}.ToValue"/>.
/// </summary>
[TestClass]
public class ToException_Tests {

    /// <summary>
    ///   The <see cref="ErrorResult{T}.ToException"/> method returns the error provided
    ///   when the instance was created.
    /// </summary>
    [TestMethod]
    public void ResultT_ToException_returns_correct_error() =>
        RunUnitTests(new ToException_returns_correct_error());

    private sealed class ToException_returns_correct_error : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            var ex = new ArgumentException("test");
            var result = Result.From<T>(ex);
            Assert.AreEqual(ex, result.ToException());
        }
    }

    /// <summary>
    ///   The <see cref="ErrorResult{T}.ToException"/> method should only be implemented
    ///   on <see cref="ErrorResult{T}"/> (not on <see cref="ValueResult{T}"/>).
    ///   This is a design assumptions check.
    /// </summary>
    [TestMethod]
    public void ResultT_ToError_is_only_available_on_ErrorResult() =>
        RunUnitTests(new ToError_is_only_available_on_ErrorResult());

    private sealed class ToError_is_only_available_on_ErrorResult : IUnitTest1 {

        public void RunTest<T>(T value) where T : notnull {
            var resultMethods = GetToErrorMethods<T>(typeof(Result<T>));
            Assert.IsFalse(resultMethods.Any());
            var valueMethods = GetToErrorMethods<T>(typeof(ValueResult<T>));
            Assert.IsFalse(valueMethods.Any());
            var errorMethods = GetToErrorMethods<T>(typeof(ErrorResult<T>));
            Assert.IsTrue(errorMethods.Any());
        }

        private const BindingFlags flags =
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        private static IEnumerable<MethodInfo> GetToErrorMethods<T>(Type type) where T : notnull =>
            type.GetMethods(flags).Where(m => m.Name == nameof(ErrorResult<T>.ToException));
    }
}

/// <summary>
///   Unit tests for <see cref="Result{T}.TryGetValue(out T)"/>.
/// </summary>
[TestClass]
public class TryGetValue_Tests {

    /// <summary>
    ///   The <see cref="Result{T}.TryGetValue(out T)"/> method returns false when no value
    ///   is wrapped.
    /// </summary>
    [TestMethod]
    public void ResultT_TryGetValue_returns_false_and_default_value_for_ErrorResultT() =>
        RunUnitTests(new TryGetValue_returns_false_and_default_value_for_NoneT());

    private sealed class TryGetValue_returns_false_and_default_value_for_NoneT : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            var result = (Result<T>)Result.From<T>(new ArgumentException("test"));
            Assert.IsFalse(result.TryGetValue(out var returnedValue));
            Assert.AreEqual(default, returnedValue);
        }
    }

    /// <summary>
    ///   The <see cref="Result{T}.TryGetValue(out T)"/> method returns true and the wrapped
    ///   value when a value is wrapped.
    /// </summary>
    [TestMethod]
    public void ResultT_TryGetValue_returns_true_and_correct_value_for_ValueResultT() =>
        RunUnitTests(new TryGetValue_returns_true_and_correct_value_for_SomeT());

    private sealed class TryGetValue_returns_true_and_correct_value_for_SomeT : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = (Result<T>)Result.From(value);
            Assert.IsTrue(result.TryGetValue(out var returnedValue));
            Assert.AreEqual(value, returnedValue);
        }
    }
}

/// <summary>
///   Unit tests for <see cref="Result{T}.TryGetException(out Exception)"/>.
/// </summary>
[TestClass]
public class TryGetException_Tests {

    /// <summary>
    ///   The <see cref="Result{T}.TryGetException(out Exception)"/> method returns true and
    ///   the wrapped exception when an exception is wrapped.
    /// </summary>
    [TestMethod]
    public void ResultT_TryGetException_returns_true_and_correct_exception_for_ErrorResultT() =>
        RunUnitTests(new TryGetException_returns_true_and_correct_exception_for_ErrorResultT());

    private sealed class TryGetException_returns_true_and_correct_exception_for_ErrorResultT
            : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var ex = new ArgumentException("test");
            var result = (Result<T>)Result.From<T>(ex);
            Assert.IsTrue(result.TryGetException(out var returnedEx));
            Assert.AreEqual(ex, returnedEx);
        }
    }

    /// <summary>
    ///   The <see cref="Result{T}.TryGetException(out Exception)"/> method returns false and
    ///   the default value for <see cref="Exception"/> when an exception is wrapped.
    /// </summary>
    [TestMethod]
    public void ResultT_TryGetException_returns_false_and_default_for_ValueResultT() =>
        RunUnitTests(new TryGetException_returns_false_and_default_for_ValueResultT());

    private sealed class TryGetException_returns_false_and_default_for_ValueResultT
            : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = (Result<T>)Result.From(value);
            Assert.IsFalse(result.TryGetException(out var returnedEx));
            Assert.AreEqual(default, returnedEx);
        }
    }
}
