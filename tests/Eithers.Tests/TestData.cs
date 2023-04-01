namespace Ainsworth.Eithers.Tests;

internal static class TestData {

    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Minor Code Smell", "S2344:Enumeration type names should not have \"Flags\" or \"Enum\" suffixes",
        Justification = "The 'Enum' suffix makes sense in this testing context.")]
    internal enum TestEnum { E11, E22 }
    internal sealed record class TestClass(int I, string S);
}
