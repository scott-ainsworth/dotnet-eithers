#nullable enable

using System.Diagnostics;

namespace Ainsworth.Eithers.Tests;

internal static class TestData {

    // int values
    internal const int int111 = 111;
    internal const int int222 = 222;
    internal static readonly int? nullableInt111 = 111;
    internal static readonly int? nullInt = null!;

    // enum values
    internal enum TestEnum { E11, E22 }
    internal const TestEnum enumValue = TestEnum.E11;
    internal const TestEnum enumValue2 = TestEnum.E22;
    internal static readonly TestEnum? nullableEnumValue = enumValue;
    internal static readonly TestEnum? nullEnumValue = null!;

    // struct values
    internal record struct TestStruct(int I, string S);
    internal static TestStruct structValue = new(111, "111");
    internal static TestStruct structValue2 = new(222, "222");
    internal static readonly TestStruct? nullableStructValue = structValue;
    internal static readonly TestStruct? nullStructValue = null!;

    // tuple values
    internal static readonly (int, string) tupleValue = (111, "111");
    internal static readonly (int, string) tupleValue2 = (222, "222");
    internal static readonly (int, string)? nullableTupleValue = tupleValue;
    internal static readonly (int, string)? nullTupleValue = null!;

    // string values
    internal const string stringValue = "111";
    internal const string stringValue2 = "222";
    internal const string? nullableStringValue = stringValue;
    internal const string? nullStringValue = null!;

    // array values
    internal static readonly int[] arrayValue = new int[] { 111, 111 };
    internal static readonly int[] arrayValue2 = new int[] { 222, 222 };
    internal static readonly int[]? nullableArrayValue = arrayValue;
    internal static readonly int[]? nullArrayValue = null!;

    // class values
    internal record class TestClass(int I, string S);
    internal static readonly TestClass classValue = new(111, "111");
    internal static readonly TestClass classValue2 = new(222, "222");
    internal static readonly TestClass? nullableClassValue = classValue;
    internal static readonly TestClass? nullClassValue = null!;
}
