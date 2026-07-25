namespace Nice3point.Revit.Extensions.Tests.Coverage.Models;

/// <summary>
///     A single row of the map report.
/// </summary>
public sealed record ApiMapRow
{
    /// <summary>
    ///     The short name of the map type.
    /// </summary>
    public required string TypeName { get; init; }

    /// <summary>
    ///     The short name of the key type the iterator exposes.
    /// </summary>
    public required string KeyType { get; init; }

    /// <summary>
    ///     The short name of the value type the indexer returns.
    /// </summary>
    public required string ValueType { get; init; }

    /// <summary>
    ///     The short name of the iterator type the map returns from its iterator factory.
    /// </summary>
    public required string IteratorType { get; init; }

    /// <summary>
    ///     The short name of the type a <c>foreach</c> over the map yields.
    /// </summary>
    public required string EnumeratedType { get; init; }

    /// <summary>
    ///     The members the map leaves to an extension, named by <see cref="ApiCollectionIssues"/>.
    /// </summary>
    public required IReadOnlyList<string> Issues { get; init; }

    /// <summary>
    ///     Names of the library source files declaring an extension over <see cref="TypeName"/>.
    ///     An empty list marks a map the library does not wrap yet.
    /// </summary>
    public required IReadOnlyList<string> ImplementationFiles { get; init; }
}
