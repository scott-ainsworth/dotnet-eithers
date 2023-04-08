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

    public static void RunUnitTests(IUnitTest0Split action) {

        _ = action ?? throw new ArgumentNullException(nameof(action));

        // Primitive Types
        action.RunTestOnValueType<FileAccess>();
        action.RunTestOnValueType<int>();

        // Value Types
        action.RunTestOnValueType<(int, string)>();
        action.RunTestOnValueType<decimal>();

        // Reference Types
        action.RunTestOnReferenceType<string>();
        action.RunTestOnReferenceType<int[]>();
        action.RunTestOnReferenceType<TimeZoneInfo>();
    }

    public static void RunUnitTests(IUnitTest1Split action) {

        _ = action ?? throw new ArgumentNullException(nameof(action));

        // Primitive Types
        action.RunTestOnValueType(FileAccess.Read);
        action.RunTestOnValueType(111);

        // Value Types
        action.RunTestOnValueType((111, "111"));
        action.RunTestOnValueType((decimal)111);

        // Reference Types
        action.RunTestOnReferenceType("111");
        action.RunTestOnReferenceType(new int[] { 111 });
        action.RunTestOnReferenceType(TimeZoneInfo.Local);
    }

    public static void RunUnitTests(IUnitTest2Split action) {

        _ = action ?? throw new ArgumentNullException(nameof(action));

        // Primitive Types
        action.RunTestOnValueType(FileAccess.Read, FileAccess.Write);
        action.RunTestOnValueType(111, 222);

        // Value Types
        action.RunTestOnValueType((111, "111"), (222, "222"));
        action.RunTestOnValueType<decimal>(111, 222);

        // Reference Types
        action.RunTestOnReferenceType("111", "222");
        action.RunTestOnReferenceType(new int[] { 111 }, new int[] { 222 });
        action.RunTestOnReferenceType(TimeZoneInfo.Local, TimeZoneInfo.Utc);
    }

    public static void RunUnitTests(IUnitTest0 action) {

        _ = action ?? throw new ArgumentNullException(nameof(action));

        // Primitive Types
        action.RunTest<FileAccess>();
        action.RunTest<int>();

        // Value Types
        action.RunTest<(int, string)>();
        action.RunTest<decimal>();

        // Reference Types
        action.RunTest<string>();
        action.RunTest<int[]>();
        action.RunTest<TimeZoneInfo>();
    }

    public static void RunUnitTests(IUnitTest1 action) {

        _ = action ?? throw new ArgumentNullException(nameof(action));

        // Primitive Types
        action.RunTest(FileAccess.Read);
        action.RunTest(111);

        // Value Types
        action.RunTest((111, "111"));
        action.RunTest((decimal)111);

        // Reference Types
        action.RunTest("111");
        action.RunTest(new int[] { 111 });
        action.RunTest(TimeZoneInfo.Local);
    }

    public static void RunUnitTests(IUnitTest2 action) {

        _ = action ?? throw new ArgumentNullException(nameof(action));

        // Primitive Types
        action.RunTest(FileAccess.Read, FileAccess.Write);
        action.RunTest(111, 222);

        // Value Types
        action.RunTest((111, "111"), (222, "222"));
        action.RunTest<decimal>(111, 222);

        // Reference Types
        action.RunTest("111", "222");
        action.RunTest(new int[] { 111 }, new int[] { 222 });
        action.RunTest(TimeZoneInfo.Local, TimeZoneInfo.Utc);
    }

}
