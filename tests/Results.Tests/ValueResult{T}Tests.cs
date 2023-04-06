using System.Reflection;

using Ainsworth.Eithers.Results;

using TestSupport;
using static TestSupport.TestRunner;

// Disable SonarLint S2699 because most assertions are in called subroutines.
#pragma warning disable S2699 // Test should include assertions

namespace ValueResultT_Tests;

/// <summary>
///   Unit tests for <see cref="ValueResult{T}"/> constructors.
/// </summary>
[TestClass]
public class Constructor_Checks {

    /// <summary>
    ///   The <see cref="ValueResult{T}"/> constructors' visibility is not public.
    ///   This is a design assumptions check.
    /// </summary>
    [TestMethod]
    public void ValueResultT_constructors_are_protected() =>
        RunUnitTests(new Constructors_are_protected());

    private sealed class Constructors_are_protected : IUnitTest0 {
        public void RunTest<T>() where T : notnull {
            var type = typeof(ErrorResult<T>);
            var publicConstructors = type.GetConstructors(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            Assert.IsFalse(
                publicConstructors.Any(),
                $"{type.Name} has at least 1 public constructor");
        }
    }
}

/// <summary>
///   Unit tests for <see cref="ValueResult{T}.Equals(ErrorResult{T})"/>.
/// </summary>
[TestClass]
public class EqualsErrorResultT_Tests {

    /// <summary>
    ///  The <see cref="ValueResult{T}.Equals(ErrorResult{T})"/> method returns
    ///  <see langword="false"/>.
    /// </summary>
    [TestMethod]
    public void ValueResultT_EqualsErrorResultT_returns_false() =>
        RunUnitTests(new EqualsErrorResultT_returns_false());

    private sealed class EqualsErrorResultT_returns_false : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From(value);
            var result2 = Result.From<T>(new ArgumentException("test"));
            Assert.IsFalse(result.Equals(result2));
        }
    }

    /// <summary>
    ///  The <see cref="ErrorResult{T}.Equals(ErrorResult{T})"/> methods throws a
    ///  <see cref="ArgumentNullException"/> for a <see langword="null"/> argument.
    /// </summary>
    /// <remarks>
    ///   This can only happen if called from a "#nullable disable" environment.
    /// </remarks>
    [TestMethod]
    public void ValueResultT_EqualsErrorResultT_throws_on_null_argument() =>
        RunUnitTests(new EqualsErrorResultT_throws_on_null_argument());

    private sealed class EqualsErrorResultT_throws_on_null_argument : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From(value);
            // Use 'null!' to simulate call from '#nullable disable' environment
            var ex = Assert.ThrowsException<ArgumentNullException>(
                () => result.Equals((ErrorResult<T>)null!));
            Assert.AreEqual("other", ex.ParamName);
        }
    }
}

/// <summary>
///   Unit tests for <see cref="ValueResult{T}.Equals(Exception)"/>.
/// </summary>
[TestClass]
public class EqualsException_Tests {

    /// <summary>
    ///  The <see cref="ValueResult{T}.Equals(Exception)"/> method returns <see langword="false"/>
    ///  for an <see cref="ErrorResult{T}"/> argument.
    /// </summary>
    [TestMethod]
    public void ValueResultT_EqualsException_returns_false_for_error() =>
        RunUnitTests(new EqualsException_returns_false_for_error());

    private sealed class EqualsException_returns_false_for_error : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From(value);
            Assert.IsFalse(result.Equals(new ArgumentException("test")));
        }
    }

    /// <summary>
    ///  The <see cref="ValueResult{T}.Equals(Exception)"/> method throws a
    ///  <see cref="ArgumentNullException"/> for a <see langword="null"/> argument.
    /// </summary>
    /// <remarks>
    ///   This can only happen if called from a "#nullable disable" environment.
    /// </remarks>
    [TestMethod]
    public void ValueResultT_EqualsException_throws_on_null_argument() =>
        RunUnitTests(new EqualsException_throws_on_null_argument());

    private sealed class EqualsException_throws_on_null_argument : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From(value);
            // Use 'null!' to simulate call from '#nullable disable' environment
            var ex = Assert.ThrowsException<ArgumentNullException>(
                () => result.Equals((Exception)null!));
            Assert.AreEqual("other", ex.ParamName);
        }
    }
}

/// <summary>z
///   Unit tests for <see cref="ValueResult{T}.Equals(object)"/>.
/// </summary>
[TestClass]
public class EqualsObject_Tests {

    /// <summary>
    ///  The <see cref="ValueResult{T}.Equals(object)"/> method returns <see langword="false"/>
    ///  for an <see cref="ErrorResult{T}"/> argument.
    /// </summary>
    [TestMethod]
    public void ValueResultT_EqualsObject_returns_false_for_error() =>
        RunUnitTests(new EqualsObject_returns_false_for_error());

    private sealed class EqualsObject_returns_false_for_error : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From(value);
            var result2 = Result.From<T>(new ArgumentException("test"));
            Assert.IsFalse(result.Equals((object)result2));
        }
    }

    /// <summary>
    ///  The <see cref="ValueResult{T}.Equals(object)"/> method returns <see langword="false"/>
    ///  for a value argument that does not equal the wrapped value.
    /// </summary>
    [TestMethod]
    public void ValueResultT_EqualsObject_returns_false_for_not_equal_value() =>
        RunUnitTests(new EqualsObject_returns_false_for_not_equal_values());

    private sealed class EqualsObject_returns_false_for_not_equal_values : IUnitTest2 {
        public void RunTest<T>(T value, T value2) where T : notnull {
            var result = Result.From(value);
            Assert.IsFalse(result.Equals((object)value2));
        }
    }

    /// <summary>
    ///  The <see cref="ValueResult{T}.Equals(object)"/> method returns <see langword="false"/>
    ///  for a <see cref="ValueResult{T}"/> argument that wraps a different value.
    /// </summary>
    [TestMethod]
    public void ValueResultT_EqualsObject_returns_false_for_not_equal_ValueResultT_value() =>
        RunUnitTests(new EqualsObject_returns_false_for_not_equal_ValueResultT_value());

    private sealed class EqualsObject_returns_false_for_not_equal_ValueResultT_value : IUnitTest2 {
        public void RunTest<T>(T value, T value2) where T : notnull {
            var result = Result.From(value);
            var result2 = Result.From(value2);
            Assert.IsFalse(result.Equals((object)result2));
        }
    }

    /// <summary>
    ///  The <see cref="ValueResult{T}.Equals(object)"/> method returns <see langword="false"/>
    ///  for a <see langword="null"/> argument.
    /// </summary>
    [TestMethod]
    public void ValueResultT_EqualsObject_returns_false_for_null_argument() =>
        RunUnitTests(new EqualsObject_returns_false_for_error_and_null_argument());

    private sealed class EqualsObject_returns_false_for_error_and_null_argument : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From(value);
            // Use 'null!' to simulate call from '#nullable disable' environment
            Assert.IsFalse(result.Equals((object)null!));
        }
    }

    /// <summary>
    ///  The <see cref="ValueResult{T}.Equals(object)"/> method returns <see langword="true"/>
    ///  for a value argument that equals this instance's wrapped value.
    /// </summary>
    [TestMethod]
    public void ValueResultT_EqualsObject_returns_true_for_equal_value() =>
        RunUnitTests(new EqualsObject_returns_true_for_equal_value());

    private sealed class EqualsObject_returns_true_for_equal_value : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From(value);
            Assert.IsTrue(result.Equals((object)value));
        }
    }

    /// <summary>
    ///  The <see cref="ValueResult{T}.Equals(object)"/> method returns <see langword="true"/>
    ///  for a <see cref="ValueResult{T}"/> argument wrapping value equal to this instance's
    ///  wrapped value.
    /// </summary>
    [TestMethod]
    public void ValueResultT_EqualsObject_returns_true_for_equal_ValueResultT_value() =>
    RunUnitTests(new EqualsObject_returns_true_for_equal_ValueResultT_value());

    private sealed class EqualsObject_returns_true_for_equal_ValueResultT_value : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From(value);
            var result2 = Result.From(value);
            Assert.IsTrue(result.Equals((object)result2));
        }
    }
}

/// <summary>z
///   Unit tests for <see cref="ValueResult{T}.Equals(Result{T})"/>.
/// </summary>
[TestClass]
public class EqualsResultT_Tests {

    /// <summary>
    ///  The <see cref="ValueResult{T}.Equals(Result{T})"/> method returns <see langword="false"/>
    ///  for an <see cref="ErrorResult{T}"/> argument.
    /// </summary>
    [TestMethod]
    public void ValueResultT_EqualsResultT_returns_false_for_error() =>
        RunUnitTests(new EqualsObject_returns_false_for_value_and_error());

    private sealed class EqualsObject_returns_false_for_value_and_error : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From(value);
            var result2 = Result.From<T>(new ArgumentException("test"));
            Assert.IsFalse(result.Equals((Result<T>)result2));
        }
    }

    /// <summary>
    ///  The <see cref="ValueResult{T}.Equals(Result{T})"/> method returns <see langword="false"/>
    ///  for a <see cref="ValueResult{T}"/> argument that wraps a different value.
    /// </summary>
    [TestMethod]
    public void ValueResultT_EqualsResultT_returns_false_for_not_equal_ValueResultT_value() =>
        RunUnitTests(new EqualsObject_returns_false_for_not_equal_ValueResultT_value());

    private sealed class EqualsObject_returns_false_for_not_equal_ValueResultT_value : IUnitTest2 {
        public void RunTest<T>(T value, T value2) where T : notnull {
            var result = Result.From(value);
            var result2 = Result.From(value2);
            Assert.IsFalse(result.Equals((Result<T>)result2));
        }
    }

    /// <summary>
    ///  The <see cref="ValueResult{T}.Equals(Result{T})"/> method throws a
    ///  <see cref="ArgumentNullException"/> for a <see langword="null"/> argument.
    /// </summary>
    /// <remarks>
    ///   This can only happen if called from a "#nullable disable" environment.
    /// </remarks>
    [TestMethod]
    public void ValueResultT_EqualsResultT_throws_on_null_argument() =>
        RunUnitTests(new EqualsResultT_throws_on_null_argument());

    private sealed class EqualsResultT_throws_on_null_argument : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From(value);
            // Use 'null!' to simulate call from '#nullable disable' environment
            var ex = Assert.ThrowsException<ArgumentNullException>(
                () => result.Equals((Result<T>)null!));
            Assert.AreEqual("other", ex.ParamName);
        }
    }

    /// <summary>
    ///  The <see cref="ValueResult{T}.Equals(Result{T})"/> method returns <see langword="true"/>
    ///  for a <see cref="ValueResult{T}"/> argument wrapping value equal to this instance's
    ///  wrapped value.
    /// </summary>
    [TestMethod]
    public void ValueResultT_EqualsResultT_returns_true_for_equal_ValueResultT_value() =>
    RunUnitTests(new EqualsObject_returns_true_for_equal_ValueResultT_value());

    private sealed class EqualsObject_returns_true_for_equal_ValueResultT_value : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From(value);
            var result2 = Result.From(value);
            Assert.IsTrue(result.Equals((Result<T>)result2));
        }
    }
}

/// <summary>
///   Unit tests for <see cref="ValueResult{T}.Equals(T)"/>.
/// </summary>
[TestClass]
public class EqualsT_Tests {

    /// <summary>
    ///  The <see cref="ValueResult{T}.Equals(T)"/> method returns <see langword="true"/> for
    ///  a value argument equal to this instance's wrapped value.
    /// </summary>
    [TestMethod]
    public void ValueResultT_EqualsT_returns_true_for_equal_values() =>
        RunUnitTests(new EqualsT_returns_true_for_equal_values());

    private sealed class EqualsT_returns_true_for_equal_values : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From(value);
            Assert.IsTrue(result.Equals(value));
        }
    }

    /// <summary>
    ///  The <see cref="ValueResult{T}.Equals(T)"/> method returns <see langword="false"/> for
    ///  a value does not equal this instance's wrapped value.
    /// </summary>
    [TestMethod]
    public void ValueResultT_EqualsT_returns_false_for_not_equal_values() =>
        RunUnitTests(new EqualsT_returns_false_for_not_equal_values());

    private sealed class EqualsT_returns_false_for_not_equal_values : IUnitTest2 {
        public void RunTest<T>(T value, T value2) where T : notnull {
            var result = Result.From(value);
            Assert.IsFalse(result.Equals(value2));
        }
    }

    /// <summary>
    ///   The <see cref="ValueResult{T}.Equals(T)"/> method throws
    ///   <see cref="ArgumentNullException"/> for a <see langword="null"/> argument.
    /// </summary>
    [TestMethod]
    public void ValueResultT_EqualsT_on_reference_null_argument() =>
        RunUnitTests(new EqualsT_returns_throws_reference_null_argument());

    private sealed class EqualsT_returns_throws_reference_null_argument : IUnitTest1Split {
        public void RunTestOnReferenceType<T>(T value) where T : class {
            var result = Result.From(value);
            // Use 'null!' to simulate call from '#nullable disable' environment
            var ex = Assert.ThrowsException<ArgumentNullException>(() => result.Equals((T)null!));
            Assert.AreEqual("other", ex.ParamName);
        }
        public void RunTestOnValueType<T>(T value) where T : struct {
            // Cannot pass null to Equals(T) where T is a value type
        }
    }
}

/// <summary>
///   Unit tests for <see cref="ValueResult{T}.Equals(ValueResult{T})"/>.
/// </summary>
[TestClass]
public class EqualsValueResultT_Tests {

    /// <summary>
    ///  The <see cref="ValueResult{T}.Equals(ValueResult{T})"/> method returns
    ///  <see langword="false"/> for a <see cref="ValueResult{T}"/> that wraps a
    ///  different value.
    /// </summary>
    [TestMethod]
    public void ValueResultT_EqualsValueResultT_returns_false_for_not_equal_value() =>
        RunUnitTests(new EqualsErrorResultT_returns_false());

    private sealed class EqualsErrorResultT_returns_false : IUnitTest2 {
        public void RunTest<T>(T value, T value2) where T : notnull {
            var result = Result.From(value);
            var result2 = Result.From(value2);
            Assert.IsFalse(result.Equals(result2));
        }
    }

    /// <summary>
    ///  The <see cref="ValueResult{T}.Equals(ValueResult{T})"/> method returns
    ///  <see langword="true"/> for a <see cref="ValueResult{T}"/> argument wrapping
    ///  an equal value.
    /// </summary>
    [TestMethod]
    public void ValueResultT_EqualsValueResultT_returns_true_for_equal_value() =>
        RunUnitTests(new EqualsValueResultT_returns_true_for_equal_values());

    private sealed class EqualsValueResultT_returns_true_for_equal_values : IUnitTest1 {
        public void RunTest<T>(T value) where T : notnull {
            var result = Result.From(value);
            var result2 = Result.From(value);
            Assert.IsTrue(result.Equals(result2));
        }
    }
}
