using System.Text.Json;

using Ainsworth.Monads.Results;

using TestSupport;
using static TestSupport.TestRunner;

// Disable SonarLint S2699 because most assertions are in called subroutines.
#pragma warning disable S2699 // Test should include assertions

namespace Results_Result_Tests;

/// <summary>
///   Unit test for the <see cref="Result"/>.From(Func{T})"/> methods
/// </summary>
[TestClass]
public class From_Func_Tests {
	/// <summary>
	///   The <see cref="Result.From{T, E1}(Func{T})"/> methods creates a <see cref="IResult{T}"/>
	///   of the correct type wrapping the value argument on success.
	/// </summary>
	[TestMethod]
	public void Result_From_FuncT_E1_creates_ResultT_wrapping_value_on_success() =>
		RunUnitTests(new From_FuncT_E1_creates_ResultT_wrapping_value_argument());

	private sealed class From_FuncT_E1_creates_ResultT_wrapping_value_argument : IUnitTest1 {
		public void RunTest<T>(T value) where T : notnull {
			var fromFuncs = MakeFromFuncs(value);
			foreach (var fromF in fromFuncs) {
				var result = fromF();
				Assert.IsInstanceOfType<IResult<T>>(result);
				Assert.IsInstanceOfType<Ok<T>>(result);
				var errorResult = (Ok<T>)result;
				Assert.AreEqual(value, errorResult.ToValue());
			}
		}
	}

	/// <summary>
	///   The <see cref="Result.From{T, E1}(Func{T})"/> methods creates a <see cref="IResult{T}"/>
	///   of the correct type wrapping the value argument on failure.
	/// </summary>
	[TestMethod]
	public void Result_From_FuncT_E1_creates_ResultT_wrapping_error_on_failure() =>
		RunUnitTests(new From_FuncT_E1_creates_ResultT_wrapping_exception());

	private sealed class From_FuncT_E1_creates_ResultT_wrapping_exception : IUnitTest1 {
		public void RunTest<T>(T value) where T : notnull {
			var ex = new ArgumentNullException(nameof(value));
			var fromFuncs = MakeFromFuncs(value, ex);
			foreach (var fromF in fromFuncs) {
				var result = fromF();
				Assert.IsInstanceOfType<IResult<T>>(result);
				Assert.IsInstanceOfType<Err<T>>(result);
				var errorResult = (Err<T>)result;
				Assert.AreEqual(ex, errorResult.ToException());
			}
		}
	}

	/// <summary>
	///   The <see cref="Result.From{T, E1}(Func{T})"/> methods throws the function called throws
	///   an unlisted exception.
	/// </summary>
	[TestMethod]
	public void Result_From_FuncT_E1_throws_on_unlisted_exception() =>
		RunUnitTests(new From_FuncT_E1_on_unlisted_exception());

	private sealed class From_FuncT_E1_on_unlisted_exception : IUnitTest1 {
		public void RunTest<T>(T value) where T : notnull {
			var ex = new NotSupportedException(
				"This exception is not one of the type parameters below");
			var fromFuncs = MakeFromFuncs(value, ex);
			foreach (var fromF in fromFuncs) {
				_ = Assert.ThrowsException<NotSupportedException>(fromF);
			}
		}
	}

	/// <summary>
	///   Build the functions used to test all variations of the Result.From(func) functions.
	/// </summary>
	private static IEnumerable<Func<IResult<T>>> MakeFromFuncs<T>(
			T value, Exception? ex = null) where T : notnull {

		var tryF = () => ex is not null ? throw ex : value;

		return new Func<IResult<T>>[] {
			() => Result.From<T, ArgumentNullException>(tryF),
			() => Result.From<T, ArgumentNullException, ArgumentOutOfRangeException>(tryF),
			() => Result.From<T, ArgumentNullException, ArgumentOutOfRangeException,
					FormatException>(tryF),
			() => Result.From<T, ArgumentNullException, ArgumentOutOfRangeException,
					FormatException, JsonException>(tryF),
			() => Result.From<T, ArgumentNullException, ArgumentOutOfRangeException,
					FormatException, JsonException, IndexOutOfRangeException>(tryF)
		};
	}
}

/// <summary>
///   Unit tests for <see cref="Result"/>.From(...) methods.
/// </summary>
[TestClass]
public class From_Tests {

	/// <summary>
	///   The <see cref="Result.From{T}(Exception)"/> methods creates a <see cref="IResult{T}"/>
	///   of the correct type wrapping the error argument.
	/// </summary>
	[TestMethod]
	public void Result_From_Exception_creates_ResultT_wrapping_exception_argument() =>
		RunUnitTests(new From_T_creates_ResultT_wrapping_exception_argument());

	private sealed class From_T_creates_ResultT_wrapping_exception_argument : IUnitTest0 {
		public void RunTest<T>() where T : notnull {
			var ex = new ArgumentException("test");
			var result = Result.From<T>(ex);
			Assert.IsInstanceOfType<IResult<T>>(result);
			Assert.IsInstanceOfType<Err<T>>(result);
			Assert.AreEqual(ex, result.ToException());
		}
	}

	/// <summary>
	///   The <see cref="Result.From{T}(T)"/> methods creates a <see cref="IResult{T}"/> of the
	///   correct type wrapping the value argument value.
	/// </summary>
	[TestMethod]
	public void Result_From_T_creates_ResultT_wrapping_value_argument() =>
		RunUnitTests(new From_T_creates_ResultT_wrapping_value_argument());

	private sealed class From_T_creates_ResultT_wrapping_value_argument : IUnitTest1 {
		public void RunTest<T>(T value) where T : notnull {
			var result = Result.From(value);
			Assert.IsInstanceOfType<IResult<T>>(result);
			Assert.IsInstanceOfType<Ok<T>>(result);
			Assert.AreEqual(value, result.ToValue());
		}
	}

	/// <summary>
	///   The <see cref="Result.From{T}(Exception)"/> method throws on null value.
	/// </summary>
	/// <remarks>
	///   For code build with null analysis enabled (e.g., <c>#nullable enable</c>), a
	///   <see langword="null"/> should be passed to any of the <c>From</c> methods.  However,
	///   good coding practice requires null checks. 
	/// </remarks>
	[TestMethod]
	public void Result_From_T_throws_on_null_exception_argument() =>
		RunUnitTests(new From_T_throws_on_null_exception_argument());

	private sealed class From_T_throws_on_null_exception_argument : IUnitTest1 {
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
			"Style", "IDE0004:Remove Unnecessary Cast",
			Justification = "The cast is necessary to force the correct argument type")]
		public void RunTest<T>(T value) where T : notnull {
			var ex = Assert.ThrowsException<ArgumentNullException>(
				() => Result.From<T>((Exception)null!));
			Assert.AreEqual("ex", ex.ParamName);
		}
	}

	/// <summary>
	///   The <see cref="Result.From{T}(T)"/> method throws on null value.
	/// </summary>
	/// <remarks>
	///   For code build with null analysis enabled (e.g., <c>#nullable enable</c>), a
	///   <see langword="null"/> should be passed to any of the <c>From</c> methods.  However,
	///   good coding practice requires null checks. 
	/// </remarks>
	[TestMethod]
	public void Result_From_T_throws_on_null_value_argument() =>
		RunUnitTests(new From_T_throws_on_null_value_argument());

	private sealed class From_T_throws_on_null_value_argument : IUnitTest1 {
		public void RunTest<T>(T value) where T : notnull {
			var ex = Assert.ThrowsException<ArgumentNullException>(() => Result.From<T>(null!));
			Assert.AreEqual("ex", ex.ParamName);
		}
	}
}

/// <summary>
///   Unit tests for the <see cref="Result"/>.ToResults methods
/// </summary>
[TestClass]
public class ToResult_Tests {

	/// <summary>
	///   The <see cref="Result.ToResult{T}(Exception)"/> method creates a <see cref="IResult{T}"/>
	///   of the correct type wrapping the error argument.
	/// </summary>
	[TestMethod]
	public void Result_ToResult_Exception_creates_ResultT_wrapping_error_argument() =>
		RunUnitTests(new ToResult_Exception_creates_ResultT_wrapping_error_argument());

	private sealed class ToResult_Exception_creates_ResultT_wrapping_error_argument : IUnitTest0 {
		public void RunTest<T>() where T : notnull {
			var ex = new ArgumentException("test");
			var result = ex.ToResult<T>();
			Assert.IsInstanceOfType<IResult<T>>(result);
			Assert.IsInstanceOfType<Err<T>>(result);
			var errorResult = (Err<T>)result;
			Assert.AreEqual(ex, errorResult.ToException());
		}
	}

	/// <summary>
	///   The <see cref="Result.ToResult{T}(T)"/> method creates a <see cref="IResult{T}"/> of the
	///   correct type wrapping the value argument.
	/// </summary>
	[TestMethod]
	public void Result_ToResult_T_creates_ResultT_wrapping_value_argument() =>
		RunUnitTests(new ToResult_T_creates_ResultT_wrapping_value_argument());

	private sealed class ToResult_T_creates_ResultT_wrapping_value_argument : IUnitTest1 {
		public void RunTest<T>(T value) where T : notnull {
			var result = value.ToResult();
			Assert.IsInstanceOfType<IResult<T>>(result);
			Assert.IsInstanceOfType<Ok<T>>(result);
			var valueResult = (Ok<T>)result;
			Assert.AreEqual(value, valueResult.ToValue());
		}
	}

}
