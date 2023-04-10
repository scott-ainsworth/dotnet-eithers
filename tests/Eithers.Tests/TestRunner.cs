namespace TestSupport;

/// <summary>
///   Generic test actions to avoid writing near duplicate code over and over.
/// </summary>
/// <remarks>
///   There is some serious .NET interface magic and generics Ju-Ju going on here.  First,
///   interfaces are defined for classes that implement test actions.
///   I can't say
///   that I fully understand it.  But it works and avoids massive amounts of repetative code.
/// </remarks>
/// <see href="https://stackoverflow.com/questions/9412450/passing-a-generic-function-as-a-parameter"/>
public static class TestRunner {

	/// <summary>
	///   Run a unit test case, that requires no value arguments and separate functions for
	///   the class and struct type constraints, on mulitple types.
	/// </summary>
	/// <param name="unitTest">The unit test case to run.</param>
	/// <exception cref="ArgumentNullException">
	///   Thrown when <paramref name="unitTest"/> is null;
	/// </exception>
	public static void RunUnitTests(IUnitTest0Split unitTest) {

		_ = unitTest ?? throw new ArgumentNullException(nameof(unitTest));

		// Primitive Types
		unitTest.RunTestOnValueType<FileAccess>();
		unitTest.RunTestOnValueType<int>();

		// Value Types
		unitTest.RunTestOnValueType<(int, string)>();
		unitTest.RunTestOnValueType<decimal>();

		// Reference Types
		unitTest.RunTestOnReferenceType<string>();
		unitTest.RunTestOnReferenceType<int[]>();
		unitTest.RunTestOnReferenceType<TimeZoneInfo>();
	}

	/// <summary>
	///   Run a unit test case, that requires one value argument and separate functions for
	///   the class and struct type constraints, on mulitple types.
	/// </summary>
	/// <param name="unitTest">The unit test case to run.</param>
	/// <exception cref="ArgumentNullException">
	///   Thrown when <paramref name="unitTest"/> is null;
	/// </exception>
	public static void RunUnitTests(IUnitTest1Split unitTest) {

		_ = unitTest ?? throw new ArgumentNullException(nameof(unitTest));

		// Primitive Types
		unitTest.RunTestOnValueType(FileAccess.Read);
		unitTest.RunTestOnValueType(111);

		// Value Types
		unitTest.RunTestOnValueType((111, "111"));
		unitTest.RunTestOnValueType((decimal)111);

		// Reference Types
		unitTest.RunTestOnReferenceType("111");
		unitTest.RunTestOnReferenceType(new int[] { 111 });
		unitTest.RunTestOnReferenceType(TimeZoneInfo.Local);
	}

	/// <summary>
	///   Run a unit test case, that requires two value arguments and separate functions for
	///   the class and struct type constraints, on mulitple types.
	/// </summary>
	/// <param name="unitTest">The unit test case to run.</param>
	/// <exception cref="ArgumentNullException">
	///   Thrown when <paramref name="unitTest"/> is null;
	/// </exception>
	public static void RunUnitTests(IUnitTest2Split unitTest) {

		_ = unitTest ?? throw new ArgumentNullException(nameof(unitTest));

		// Primitive Types
		unitTest.RunTestOnValueType(FileAccess.Read, FileAccess.Write);
		unitTest.RunTestOnValueType(111, 222);

		// Value Types
		unitTest.RunTestOnValueType((111, "111"), (222, "222"));
		unitTest.RunTestOnValueType<decimal>(111, 222);

		// Reference Types
		unitTest.RunTestOnReferenceType("111", "222");
		unitTest.RunTestOnReferenceType(new int[] { 111 }, new int[] { 222 });
		unitTest.RunTestOnReferenceType(TimeZoneInfo.Local, TimeZoneInfo.Utc);
	}

	/// <summary>
	///   Run a unit test case, that requires no value arguments and single function for
	///   the notnull type constraint, on mulitple types.
	/// </summary>
	/// <param name="unitTest">The unit test case to run.</param>
	/// <exception cref="ArgumentNullException">
	///   Thrown when <paramref name="unitTest"/> is null;
	/// </exception>
	public static void RunUnitTests(IUnitTest0 unitTest) {

		_ = unitTest ?? throw new ArgumentNullException(nameof(unitTest));

		// Primitive Types
		unitTest.RunTest<FileAccess>();
		unitTest.RunTest<int>();

		// Value Types
		unitTest.RunTest<(int, string)>();
		unitTest.RunTest<decimal>();

		// Reference Types
		unitTest.RunTest<string>();
		unitTest.RunTest<int[]>();
		unitTest.RunTest<TimeZoneInfo>();
	}

	/// <summary>
	///   Run a unit test case, that requires one value argument and single function for
	///   the notnull type constraint, on mulitple types.
	/// </summary>
	/// <param name="unitTest">The unit test case to run.</param>
	/// <exception cref="ArgumentNullException">
	///   Thrown when <paramref name="unitTest"/> is null;
	/// </exception>
	public static void RunUnitTests(IUnitTest1 unitTest) {

		_ = unitTest ?? throw new ArgumentNullException(nameof(unitTest));

		// Primitive Types
		unitTest.RunTest(FileAccess.Read);
		unitTest.RunTest(111);

		// Value Types
		unitTest.RunTest((111, "111"));
		unitTest.RunTest((decimal)111);

		// Reference Types
		unitTest.RunTest("111");
		unitTest.RunTest(new int[] { 111 });
		unitTest.RunTest(TimeZoneInfo.Local);
	}

	/// <summary>
	///   Run a unit test case, that requires two value arguments and single function for
	///   the notnull type constraint, on mulitple types.
	/// </summary>
	/// <param name="unitTest">The unit test case to run.</param>
	/// <exception cref="ArgumentNullException">
	///   Thrown when <paramref name="unitTest"/> is null;
	/// </exception>
	public static void RunUnitTests(IUnitTest2 unitTest) {

		_ = unitTest ?? throw new ArgumentNullException(nameof(unitTest));

		// Primitive Types
		unitTest.RunTest(FileAccess.Read, FileAccess.Write);
		unitTest.RunTest(111, 222);

		// Value Types
		unitTest.RunTest((111, "111"), (222, "222"));
		unitTest.RunTest<decimal>(111, 222);

		// Reference Types
		unitTest.RunTest("111", "222");
		unitTest.RunTest(new int[] { 111 }, new int[] { 222 });
		unitTest.RunTest(TimeZoneInfo.Local, TimeZoneInfo.Utc);
	}

}
