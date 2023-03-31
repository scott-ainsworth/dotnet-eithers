#nullable enable

using System.Diagnostics;

namespace Ainsworth.Eithers.Tests;

internal static class TestData {

    internal enum TestEnum { E11, E22 }
    internal record struct TestStruct(int I, string S);
    internal sealed record class TestClass(int I, string S);
}
