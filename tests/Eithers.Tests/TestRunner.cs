namespace Ainsworth.Eithers.Tests;

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
internal static class TestRunner {

    public static void RunTestCases(ITestCase0 action) {

        // Primitive Types
        action.RunTestCase<FileAccess>();
        action.RunTestCase<int>();

        // Value Types
        action.RunTestCase<(int, string)>();
        action.RunTestCase<decimal>();

        // Reference Types
        action.RunTestCase<string>();
        action.RunTestCase<int[]>();
        action.RunTestCase<TimeZoneInfo>();
    }

    public static void RunTestCases(ITestCase1 action) {

        // Primitive Types
        action.RunTestCase(FileAccess.Read);
        action.RunTestCase(111);

        // Value Types
        action.RunTestCase((111, "111"));
        action.RunTestCase((decimal)111);

        // Reference Types
        action.RunTestCase("111");
        action.RunTestCase(new int[] { 111 });
        action.RunTestCase(TimeZoneInfo.Local);
    }

    public static void RunTestCases(ITestCase2 action) {

        // Primitive Types
        action.RunTestCase(FileAccess.Read, FileAccess.Write);
        action.RunTestCase(111, 222);

        // Value Types
        action.RunTestCase((111, "111"), (222, "222"));
        action.RunTestCase((decimal)111, (decimal)222);

        // Reference Types
        action.RunTestCase("111", "222");
        action.RunTestCase(new int[] { 111 }, new int[] { 222 });
        action.RunTestCase(TimeZoneInfo.Local, TimeZoneInfo.Utc);
    }
}

internal interface ITestCase0 {
    void RunTestCase<T>() where T : notnull;
}

internal interface ITestCase1 {
    void RunTestCase<T>(T value) where T : notnull;
}

internal interface ITestCase2 {
    void RunTestCase<T>(T value, T value2) where T : notnull;
}

