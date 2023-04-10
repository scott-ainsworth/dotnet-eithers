// Suppressions that apply to the entire C# project

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
	"Naming", "CA1716: Type names should not match namespaces",
	Justification = "Matching names already used by Linq.")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
	"Naming", "CA1724: Type names should not match namespaces",
	Justification = "Decided to live with the minor confusion this creates.")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
	"Naming", "CS0659: Type names should not match namespaces",
	Justification = "Decided to live with the minor confusion this creates.")]
