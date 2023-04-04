// Suppressions that apply to the entire C# project

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Naming", "CA1707: Identifiers should not contain underscores",
    Justification = "Test case names are easier to read with underscores.")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Minor Code Smell", "S101:Types should be named in PascalCase",
    Justification = "Test case names are easier to read with underscores")]
