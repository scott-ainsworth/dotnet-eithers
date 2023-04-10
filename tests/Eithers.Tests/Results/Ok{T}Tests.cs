using System.Reflection;

using Ainsworth.Monads.Results;

using TestSupport;
using static TestSupport.TestRunner;

// Disable SonarLint S2699 because most assertions are in called subroutines.
#pragma warning disable S2699 // Test should include assertions

namespace Results_Ok_Tests;

/// <summary>
///   Unit tests for <see cref="Ok{T}"/> constructors.
/// </summary>
[TestClass]
public class Constructor_Checks {

	/// <summary>
	///   The <see cref="Ok{T}"/> constructors' visibility is not public.
	///   This is a design assumptions check.
	/// </summary>
	[TestMethod]
	public void ValueResultT_constructors_are_protected() =>
		RunUnitTests(new Constructors_are_protected());

	private sealed class Constructors_are_protected : IUnitTest0 {
		public void RunTest<T>() where T : notnull {
			var type = typeof(Err<T>);
			var publicConstructors = type.GetConstructors(
				BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
			Assert.IsFalse(
				publicConstructors.Any(),
				$"{type.Name} has at least 1 public constructor");
		}
	}
}

/// <summary>
///   Unit tests for <see cref="Ok{T}.Equals(Err{T})"/>.
/// </summary>
[TestClass]
public class EqualsErrorResultT_Tests {

	/// <summary>
	///  The <see cref="Ok{T}.Equals(Err{T})"/> method returns
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
	///  The <see cref="Err{T}.Equals(Err{T})"/> methods throws a
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
				() => result.Equals((Err<T>)null!));
			Assert.AreEqual("other", ex.ParamName);
		}
	}
}

/// <summary>
///   Unit tests for <see cref="Ok{T}.Equals(Exception)"/>.
/// </summary>
[TestClass]
public class EqualsException_Tests {

	/// <summary>
	///  The <see cref="Ok{T}.Equals(Exception)"/> method returns <see langword="false"/>
	///  for an <see cref="Err{T}"/> argument.
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
	///  The <see cref="Ok{T}.Equals(Exception)"/> method throws a
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
///   Unit tests for <see cref="Ok{T}.Equals(object)"/>.
/// </summary>
[TestClass]
public class EqualsObject_Tests {

	/// <summary>
	///  The <see cref="Ok{T}.Equals(object)"/> method returns <see langword="false"/>
	///  for an <see cref="Err{T}"/> argument.
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
	///  The <see cref="Ok{T}.Equals(object)"/> method returns <see langword="false"/>
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
	///  The <see cref="Ok{T}.Equals(object)"/> method returns <see langword="false"/>
	///  for a <see cref="Ok{T}"/> argument that wraps a different value.
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
	///  The <see cref="Ok{T}.Equals(object)"/> method returns <see langword="false"/>
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
	///  The <see cref="Ok{T}.Equals(object)"/> method returns <see langword="true"/>
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
	///  The <see cref="Ok{T}.Equals(object)"/> method returns <see langword="true"/>
	///  for a <see cref="Ok{T}"/> argument wrapping value equal to this instance's
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
///   Unit tests for <see cref="Ok{T}.Equals(IResult{T})"/>.
/// </summary>
[TestClass]
public class EqualsResultT_Tests {

	/// <summary>
	///  The <see cref="Ok{T}.Equals(IResult{T})"/> method returns <see langword="false"/>
	///  for an <see cref="Err{T}"/> argument.
	/// </summary>
	[TestMethod]
	public void ValueResultT_EqualsResultT_returns_false_for_error() =>
		RunUnitTests(new EqualsObject_returns_false_for_value_and_error());

	private sealed class EqualsObject_returns_false_for_value_and_error : IUnitTest1 {
		public void RunTest<T>(T value) where T : notnull {
			var result = Result.From(value);
			var result2 = Result.From<T>(new ArgumentException("test"));
			Assert.IsFalse(result.Equals((IResult<T>)result2));
		}
	}

	/// <summary>
	///  The <see cref="Ok{T}.Equals(IResult{T})"/> method returns <see langword="false"/>
	///  for a <see cref="Ok{T}"/> argument that wraps a different value.
	/// </summary>
	[TestMethod]
	public void ValueResultT_EqualsResultT_returns_false_for_not_equal_ValueResultT_value() =>
		RunUnitTests(new EqualsObject_returns_false_for_not_equal_ValueResultT_value());

	private sealed class EqualsObject_returns_false_for_not_equal_ValueResultT_value : IUnitTest2 {
		public void RunTest<T>(T value, T value2) where T : notnull {
			var result = Result.From(value);
			var result2 = Result.From(value2);
			Assert.IsFalse(result.Equals((IResult<T>)result2));
		}
	}

	/// <summary>
	///  The <see cref="Ok{T}.Equals(IResult{T})"/> method throws a
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
				() => result.Equals((IResult<T>)null!));
			Assert.AreEqual("other", ex.ParamName);
		}
	}

	/// <summary>
	///  The <see cref="Ok{T}.Equals(IResult{T})"/> method returns <see langword="true"/>
	///  for a <see cref="Ok{T}"/> argument wrapping value equal to this instance's
	///  wrapped value.
	/// </summary>
	[TestMethod]
	public void ValueResultT_EqualsResultT_returns_true_for_equal_ValueResultT_value() =>
	RunUnitTests(new EqualsObject_returns_true_for_equal_ValueResultT_value());

	private sealed class EqualsObject_returns_true_for_equal_ValueResultT_value : IUnitTest1 {
		public void RunTest<T>(T value) where T : notnull {
			var result = Result.From(value);
			var result2 = Result.From(value);
			Assert.IsTrue(result.Equals((IResult<T>)result2));
		}
	}
}

/// <summary>
///   Unit tests for <see cref="Ok{T}.Equals(T)"/>.
/// </summary>
[TestClass]
public class EqualsT_Tests {

	/// <summary>
	///  The <see cref="Ok{T}.Equals(T)"/> method returns <see langword="true"/> for
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
	///  The <see cref="Ok{T}.Equals(T)"/> method returns <see langword="false"/> for
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
	///   The <see cref="Ok{T}.Equals(T)"/> method throws
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
///   Unit tests for <see cref="Ok{T}.Equals(Ok{T})"/>.
/// </summary>
[TestClass]
public class EqualsValueResultT_Tests {

	/// <summary>
	///  The <see cref="Ok{T}.Equals(Ok{T})"/> method returns
	///  <see langword="false"/> for a <see cref="Ok{T}"/> that wraps a
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
	///  The <see cref="Ok{T}.Equals(Ok{T})"/> method returns
	///  <see langword="true"/> for a <see cref="Ok{T}"/> argument wrapping
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

/// <summary>
///   Unit tests for <see cref="Ok{T}.TryGetValue(out T)"/>.
/// </summary>
[TestClass]
public class TryGetValue_Tests {

	/// <summary>
	///   The <see cref="Ok{T}.TryGetValue(out T)"/> method returns true
	///   and the wrapped value.
	/// </summary>
	[TestMethod]
	public void ValueResultT_TryGetValue_returns_true_and_correct_value() =>
		RunUnitTests(new TryGetValue_returns_true_and_correct_value());

	private sealed class TryGetValue_returns_true_and_correct_value : IUnitTest1 {
		public void RunTest<T>(T value) where T : notnull {
			var result = Result.From(value);
			Assert.IsTrue(result.TryGetValue(out var returnedValue));
			Assert.AreEqual(value, returnedValue);
		}
	}
}

/// <summary>
///   Unit tests for <see cref="Ok{T}.TryGetException(out Exception)"/>.
/// </summary>
[TestClass]
public class TryGetException_Tests {

	/// <summary>
	///   The <see cref="Ok{T}.TryGetException(out Exception)"/> method returns false
	///   and the default value for <see cref="Exception"/> when an exception is wrapped.
	/// </summary>
	[TestMethod]
	public void ValueResultT_TryGetException_returns_false_and_default() =>
		RunUnitTests(new TryGetException_returns_false_and_default());

	private sealed class TryGetException_returns_false_and_default
			: IUnitTest1 {
		public void RunTest<T>(T value) where T : notnull {
			var result = (IResult<T>)Result.From(value);
			Assert.IsFalse(result.TryGetException(out var returnedEx));
			Assert.AreEqual(default, returnedEx);
		}
	}
}
