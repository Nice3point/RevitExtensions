namespace Nice3point.Revit.Extensions.Tests.Coverage.Models;

/// <summary>
///     A single row of the API surface report.
/// </summary>
public sealed record ApiMethodRow
{
    /// <summary>
    ///     The short name of the method return type.
    /// </summary>
    public required string ReturnType { get; init; }

    /// <summary>
    ///     The <c>Type.Method</c> name of the reported method.
    /// </summary>
    public required string QualifiedName { get; init; }

    /// <summary>
    ///     The method parameters rendered as a comma separated <c>Type name</c> list.
    /// </summary>
    public required string Parameters { get; init; }

    /// <summary>
    ///     Names of the library source files that mention <see cref="QualifiedName" />.
    ///     An empty list marks a method the library does not wrap yet.
    /// </summary>
    public required IReadOnlyList<string> ImplementationFiles { get; init; }
}
