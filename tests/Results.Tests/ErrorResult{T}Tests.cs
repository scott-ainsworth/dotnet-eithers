using System.Reflection;

using Ainsworth.Eithers.Results;

using TestSupport;
using static TestSupport.TestRunner;

// Disable SonarLint S2699 because most assertions are in called subroutines.
#pragma warning disable S2699 // Test should include assertions

namespace ErrorResultT_Tests;

/// <summary>
///   Unit tests for <see cref="ErrorResult{T}"/> constructors.
/// </summary>
[TestClass]
public class Constructor_Checks {

    /// <summary>
    ///   The <see cref="ErrorResult{T}"/> constructors' visibility is not public.
    ///   This is a design assumptions check.
    /// </summary>
    [TestMethod]
    public void ErrorResultT_constructors_are_protected() =>
        RunUnitTests(new Constructors_are_protected());

    private sealed class Constructors_are_protected : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            var type = typeof(ValueResult<T>);
            var publicConstructors = type.GetConstructors(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            Assert.IsFalse(
                publicConstructors.Any(),
                $"{type.Name} has at least 1 public constructor");
        }
    }

#if DEBUG

    /// <summary>
    ///   An <see cref="ErrorResult{T}"/> cannot be created from a <see langword="null"/>.
    /// </summary>
    [TestMethod]
    public void ErrorResult_construction_throws_if_T_is_Exception() =>
        RunUnitTests(new Construction_throws_if_T_is_Exception());

    private sealed class Construction_throws_if_T_is_Exception : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            var ex = Assert.ThrowsException<ArgumentException>(
                () => Result.From<Exception>(new ArgumentException("test")));
            Assert.AreEqual(
                $"An {nameof(ErrorResult<T>)}<T> should never be constructed with T as " +
                $"{nameof(Exception)}. Is a type parameter missing?", ex.Message);
        }
    }

#endif
}

/// <summary>
///   Unit tests for <see cref="Result{T}.Equals(Exception)"/>
/// </summary>
[TestClass]
public class EqualsException_Tests {

    /// <summary>
    ///  The <see cref="ErrorResult{T}.Equals(Exception)"/> method returns <see langword="false"/>
    ///  for exception arguments where the exception does not equal this instance's
    ///  wrapped exception.
    /// </summary>
    [TestMethod]
    public void ErrorResultT_EqualsException_returns_false_for_not_equal_exception() =>
        RunUnitTests(new EqualsObject_returns_false_for_not_equal_exception());

    private sealed class EqualsObject_returns_false_for_not_equal_exception : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            var result = Result.From<T>(new ArgumentException("test"));
            Assert.IsFalse(result.Equals(new ArgumentException("test")));
        }
    }

    /// <summary>
    ///  The <see cref="ErrorResult{T}.Equals(Exception)"/> method throws a
    ///  <see cref="ArgumentNullException"/> for a <see langword="null"/> argument.
    /// </summary>
    /// <remarks>
    ///   This can only happen if called from a "#nullable disable" environment.
    /// </remarks>
    [TestMethod]
    public void ErrorResultT_EqualsException_throws_for_null_argument() =>
        RunUnitTests(new EqualsException_throws_for_null_argument());

    private sealed class EqualsException_throws_for_null_argument : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From<T>(new ArgumentException("test"));
            // Use 'null!'  to simulate call from '#nullable disable' environment
            var ex = Assert.ThrowsException<ArgumentNullException>(
                () => result.Equals((Exception)null!));
            Assert.AreEqual("other", ex.ParamName);
        }
    }
}

/// <summary>
///   Unit tests for <see cref="ErrorResult{T}.Equals(ErrorResult{T})"/>.
/// </summary>
[TestClass]
public class EqualsErrorResultT_Tests {

    /// <summary>
    ///  The <see cref="ErrorResult{T}.Equals(ErrorResult{T})"/> method returns
    ///  <see langword="false"/> for <see cref="ErrorResult{T}"/> arguments where the wrapped
    ///  exception does not equal this instance's wrapped exception.
    /// </summary>
    [TestMethod]
    public void ErrorResultT_EqualsErrorResultT_returns_false_for_not_equal_ErrorResultT() =>
        RunUnitTests(new EqualsErrorResultT_returns_false_for_not_equal_ValueResultT_value());

    private sealed class EqualsErrorResultT_returns_false_for_not_equal_ValueResultT_value
            : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            var result = Result.From<T>(new ArgumentException("test"));
            var result2 = Result.From<T>(new ArgumentException("test"));
            Assert.IsFalse(result.Equals(result2));
        }
    }

    /// <summary>
    ///  The <see cref="ErrorResult{T}.Equals(ErrorResult{T})"/> method throws a
    ///  <see cref="ArgumentNullException"/> for a <see langword="null"/> argument.
    /// </summary>
    /// <remarks>
    ///   This can only happen if called from a "#nullable disable" environment.
    /// </remarks>
    [TestMethod]
    public void ErrorResultT_EqualsErrorResultT_throws_for_null_argument() =>
        RunUnitTests(new EqualsErrorResultT_throws_for_null_argument());

    private sealed class EqualsErrorResultT_throws_for_null_argument : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From<T>(new ArgumentException("test"));
            // Use 'null!'  to simulate call from '#nullable disable' environment
            var ex = Assert.ThrowsException<ArgumentNullException>(
                () => result.Equals((ErrorResult<T>)null!));
            Assert.AreEqual("other", ex.ParamName);
        }
    }

    /// <summary>
    ///  The <see cref="ErrorResult{T}.Equals(ErrorResult{T})"/> method returns <see langword="true"/>
    ///  for <see cref="ErrorResult{T}"/> arguments where the argument's wrapped exception equals
    ///  this instance's wrapped exception.
    /// </summary>
    [TestMethod]
    public void ErrorResultT_EqualsErrorResultT_returns_true_for_equal_ErrorResultT() =>
        RunUnitTests(new EqualsErrorResultT_returns_true_for_equal_ErrorResultT());

    private sealed class EqualsErrorResultT_returns_true_for_equal_ErrorResultT : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            var ex = new ArgumentException("test");
            var result = Result.From<T>(ex);
            var result2 = Result.From<T>(ex);
            Assert.IsTrue(result.Equals(result2));
        }
    }
}

/// <summary>
///   Unit tests for <see cref="Result{T}.Equals(object)"/>.
/// </summary>
[TestClass]
public class EqualsObject_Tests {

    /// <summary>
    ///  The <see cref="ErrorResult{T}.Equals(object)"/> method returns <see langword="false"/>
    ///  for <see cref="ErrorResult{T}"/> arguments where the wrapped exception does not equal
    ///  this instance's wrapped exception.
    /// </summary>
    [TestMethod]
    public void ErrorResultT_EqualsObject_returns_false_for_not_equal_ErrorResultT() =>
        RunUnitTests(new EqualsObject_returns_false_for_not_equal_ValueResultT_value());

    private sealed class EqualsObject_returns_false_for_not_equal_ValueResultT_value : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            var result = Result.From<T>(new ArgumentException("test"));
            var result2 = Result.From<T>(new ArgumentException("test"));
            Assert.IsFalse(result.Equals((object)result2));
        }
    }

    /// <summary>
    ///  The <see cref="ErrorResult{T}.Equals(object)"/> method returns <see langword="false"/>
    ///  for exception arguments where the exception does not equal this instance's
    ///  wrapped exception.
    /// </summary>
    [TestMethod]
    public void ErrorResultT_EqualsObject_returns_false_for_not_equal_exception() =>
        RunUnitTests(new EqualsObject_returns_false_for_not_equal_exception());

    private sealed class EqualsObject_returns_false_for_not_equal_exception : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            var result = Result.From<T>(new ArgumentException("test"));
            Assert.IsFalse(result.Equals((object)new ArgumentException("test")));
        }
    }

    /// <summary>
    ///  The <see cref="ErrorResult{T}.Equals(object)"/> method returns <see langword="false"/>
    ///  for a <see langword="null"/> argument.
    /// </summary>
    [TestMethod]
    public void ErrorResultT_EqualsObject_returns_false_for_null_argument() =>
        RunUnitTests(new EqualsObject_returns_false_for_null_argument());

    private sealed class EqualsObject_returns_false_for_null_argument : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From<T>(new ArgumentException("test"));
            // Use 'null!'  to simulate call from '#nullable disable' environment
            Assert.IsFalse(result.Equals((object)null!));
        }
    }

    /// <summary>
    ///  The <see cref="ErrorResult{T}.Equals(object)"/> method returns <see langword="false"/>
    ///  for value arguments.
    /// </summary>
    [TestMethod]
    public void ErrorResultT_EqualsObject_returns_false_for_value() =>
        RunUnitTests(new EqualsObject_returns_false_for_value_and_error());

    private sealed class EqualsObject_returns_false_for_value_and_error : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From<T>(new ArgumentException("test"));
            Assert.IsFalse(result.Equals((object)value));
        }
    }

    /// <summary>
    ///  The <see cref="ErrorResult{T}.Equals(object)"/> method returns <see langword="false"/>
    ///  for <see cref="ValueResult{T}"/> arguments.
    /// </summary>
    [TestMethod]
    public void ErrorResultT_EqualsObject_returns_false_for_ValueResultT() =>
        RunUnitTests(new EqualsObject_returns_false_for_ValueResultT());

    private sealed class EqualsObject_returns_false_for_ValueResultT : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From<T>(new ArgumentException("test"));
            var result2 = Result.From(value);
            Assert.IsFalse(result.Equals((object)result2));
        }
    }

    /// <summary>
    ///  The <see cref="ErrorResult{T}.Equals(object)"/> method returns <see langword="true"/>
    ///  for <see cref="ErrorResult{T}"/> arguments where the argument's wrapped exception equals
    ///  this instance's wrapped exception.
    /// </summary>
    [TestMethod]
    public void ErrorResultT_EqualsObject_returns_true_for_equal_ErrorResultT() =>
        RunUnitTests(new EqualsObject_returns_true_for_equal_ErrorResultT());

    private sealed class EqualsObject_returns_true_for_equal_ErrorResultT : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            var ex = new ArgumentException("test");
            var result = Result.From<T>(ex);
            var result2 = Result.From<T>(ex);
            Assert.IsTrue(result.Equals((object)result2));
        }
    }

    /// <summary>
    ///  The <see cref="ErrorResult{T}.Equals(object)"/> method returns <see langword="true"/>
    ///  for exception arguments where the exception equals this instance's wrapped exception.
    /// </summary>
    [TestMethod]
    public void ErrorResultT_EqualsObject_returns_true_for_equal_exception() =>
        RunUnitTests(new EqualsObject_returns_true_for_equal_exception());

    private sealed class EqualsObject_returns_true_for_equal_exception : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            var ex = new ArgumentException("test");
            var result = Result.From<T>(ex);
            Assert.IsTrue(result.Equals((object)ex));
        }
    }
}

/// <summary>
///   Unit tests for <see cref="ErrorResult{T}.Equals(Result{T})"/>.
/// </summary>
[TestClass]
public class EqualsResultT_Tests {

    /// <summary>
    ///  The <see cref="ErrorResult{T}.Equals(Result{T})"/> method returns <see langword="false"/>
    ///  for <see cref="ErrorResult{T}"/> arguments where the wrapped exception does not equal
    ///  this instance's wrapped exception.
    /// </summary>
    [TestMethod]
    public void ErrorResultT_EqualsResultT_returns_false_for_not_equal_ResultT() =>
        RunUnitTests(new EqualsResultT_returns_false_for_not_equal_ValueResultT_value());

    private sealed class EqualsResultT_returns_false_for_not_equal_ValueResultT_value : IUnitTest2 {
        public void RunTest<T>(T value, T value2) where T : notnull {
            var result = Result.From<T>(new ArgumentException("test"));
            var result2 = Result.From(value2);
            Assert.IsFalse(result.Equals((Result<T>)result2));
        }
    }

    /// <summary>
    ///  The <see cref="ErrorResult{T}.Equals(Result{T})"/> method throws a
    ///  <see cref="ArgumentNullException"/> for a <see langword="null"/> argument.
    /// </summary>
    /// <remarks>
    ///   This can only happen if called from a "#nullable disable" environment.
    /// </remarks>
    [TestMethod]
    public void ErrorResultT_EqualsResultT_throws_for_null_argument() =>
        RunUnitTests(new EqualsResultT_throws_for_null_argument());

    private sealed class EqualsResultT_throws_for_null_argument : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            var result = Result.From<T>(new ArgumentException("test"));
            // Use 'null!'  to simulate call from '#nullable disable' environment
            var ex = Assert.ThrowsException<ArgumentNullException>(
                () => result.Equals((Result<T>)null!));
            Assert.AreEqual("other", ex.ParamName);
        }
    }

    /// <summary>
    ///  The <see cref="ErrorResult{T}.Equals(Result{T})"/> method returns <see langword="false"/>
    ///  for <see cref="ValueResult{T}"/> arguments.
    /// </summary>
    [TestMethod]
    public void ErrorResultT_EqualsResultT_returns_false_for_ValueResultT() =>
        RunUnitTests(new EqualsResultT_returns_false_for_ValueResultT());

    private sealed class EqualsResultT_returns_false_for_ValueResultT : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From<T>(new ArgumentException("test"));
            var result2 = Result.From(value);
            Assert.IsFalse(result.Equals((Result<T>)result2));
        }
    }

    /// <summary>
    ///  The <see cref="ErrorResult{T}.Equals(Result{T})"/> method returns <see langword="true"/>
    ///  for <see cref="ErrorResult{T}"/> arguments where the argument's wrapped exception equals
    ///  this instance's wrapped exception.
    /// </summary>
    [TestMethod]
    public void ErrorResultT_EqualsResultT_returns_true_for_equal_ErrorResultT() =>
        RunUnitTests(new EqualsResultT_returns_true_for_equal_ErrorResultT());

    private sealed class EqualsResultT_returns_true_for_equal_ErrorResultT : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            var ex = new ArgumentException("test");
            var result = Result.From<T>(ex);
            var result2 = Result.From<T>(ex);
            Assert.IsTrue(result.Equals((Result<T>)result2));
        }
    }
}

/// <summary>
///   Unit tests for <see cref="ErrorResult{T}.Equals(T)"/>.
/// </summary>
[TestClass]
public class EqualsT_Tests {

    /// <summary>
    ///  The <see cref="ErrorResult{T}.Equals(T)"/> methods throws a
    ///  <see cref="ArgumentNullException"/> for a <see langword="null"/> argument.
    /// </summary>
    /// <remarks>
    ///   This can only happen if called from a "#nullable disable" environment.
    /// </remarks>
    [TestMethod]
    public void ErrorResultT_EqualsT_throws_for_null_reference_argument() =>
        RunUnitTests(new EqualsT_throws_for_null_reference_argument());

    private sealed class EqualsT_throws_for_null_reference_argument : IUnitTest0Split {
        public void RunTestOnReferenceType<T>() where T : class {
            var result = Result.From<T>(new ArgumentException("test"));
            // Use 'null!' to simulate call from '#nullable disable' environment
            var ex = Assert.ThrowsException<ArgumentNullException>(
                () => result.Equals((T)null!));
            Assert.AreEqual("other", ex.ParamName);
        }
        public void RunTestOnValueType<T>() where T : struct {
            // Cannot pass null to Equals(T) where T is a value type
        }
    }

    /// <summary>
    ///  The <see cref="ErrorResult{T}.Equals(T)"/> method returns <see langword="false"/> for
    ///  value arguments.
    /// </summary>
    [TestMethod]
    public void ErrorResultT_EqualsT_returns_false_for_value() =>
        RunUnitTests(new EqualsT_returns_false_for_values());

    private sealed class EqualsT_returns_false_for_values : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From<T>(new ArgumentException("test"));
            Assert.IsFalse(result.Equals(value));
        }
    }
}

/// <summary>
///   Unit tests for <see cref="ErrorResult{T}.Equals(ValueResult{T})"/>.
/// </summary>
[TestClass]
public class EqualsValueResultT_Tests {

    /// <summary>
    ///  The <see cref="ErrorResult{T}.Equals(ValueResult{T})"/> method returns
    ///  <see langword="false"/>.
    /// </summary>
    [TestMethod]
    public void ErrorResultT_EqualsValueResultT_returns_false() =>
        RunUnitTests(new EqualsValueResultT_returns_false());
    private sealed class EqualsValueResultT_returns_false : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From<T>(new ArgumentException("test"));
            var result2 = Result.From<T>(value);
            Assert.IsFalse(result.Equals(result2));
        }
    }
}

/// <summary>
///   Unit tests for <see cref="ErrorResult{T}.TryGetValue(out T)"/>.
/// </summary>
[TestClass]
public class TryGetValue_Tests {

    /// <summary>
    ///   The <see cref="ErrorResult{T}.TryGetValue(out T)"/> method returns false.
    /// </summary>
    [TestMethod]
    public void ErrorResultT_TryGetValue_returns_false_and_default_value() =>
        RunUnitTests(new TryGetValue_returns_false_and_default_value());

    private sealed class TryGetValue_returns_false_and_default_value : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            var result = Result.From<T>(new ArgumentException("test"));
            Assert.IsFalse(result.TryGetValue(out var returnedValue));
            Assert.AreEqual(default, returnedValue);
        }
    }
}

/// <summary>
///   Unit tests for <see cref="ErrorResult{T}.TryGetException(out Exception)"/>.
/// </summary>
[TestClass]
public class TryGetException_Tests {

    /// <summary>
    ///   The <see cref="ErrorResult{T}.TryGetException(out Exception)"/> method returns true
    ///   and the wrapped exception when an exception is wrapped.
    /// </summary>
    [TestMethod]
    public void ErrorResultT_TryGetException_returns_true_and_correct_exception() =>
        RunUnitTests(new TryGetException_returns_true_and_correct_exception());

    private sealed class TryGetException_returns_true_and_correct_exception
            : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var ex = new ArgumentException("test");
            var result = Result.From<T>(ex);
            Assert.IsTrue(result.TryGetException(out var returnedEx));
            Assert.AreEqual(ex, returnedEx);
        }
    }
}
