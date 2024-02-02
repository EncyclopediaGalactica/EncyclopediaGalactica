[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Naming",
    "CA1707:The name of an identifier contains the underscore (_) character",
    Justification = "In tests we use underscore for additional readability",
    Scope = "module")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Reliability",
    "warning CA2007: Consider calling ConfigureAwait on the awaited task (https://learn.microsoft.com/dotnet/fundamentals/code-analysis/quality-rules/ca2007)",
    Justification = "xunit suggest not calling ConfigureAwait(false) in tests",
    Scope = "module")]