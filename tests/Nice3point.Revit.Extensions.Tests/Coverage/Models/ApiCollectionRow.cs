namespace Nice3point.Revit.Extensions.Tests.Coverage.Models;

/// <summary>
///     A single row of the collection report.
/// </summary>
public sealed record ApiCollectionRow
{
    /// <summary>
    ///     The shape the collection enumerates.
    /// </summary>
    public required ApiCollectionKind Kind { get; init; }

    /// <summary>
    ///     The short name of the collection type.
    /// </summary>
    public required string TypeName { get; init; }

    /// <summary>
    ///     The short name of the element type the collection holds.
    /// </summary>
    public required string ElementType { get; init; }

    /// <summary>
    ///     The short name of the iterator type the collection returns from its iterator factory.
    /// </summary>
    public required string IteratorType { get; init; }

    /// <summary>
    ///     The short name of the type a <c>foreach</c> over the collection yields.
    /// </summary>
    public required string EnumeratedType { get; init; }

    /// <summary>
    ///     The members the collection leaves to an extension, named by <see cref="ApiCollectionIssues" />.
    ///     An empty list marks a collection carrying the whole BCL surface already.
    /// </summary>
    public required IReadOnlyList<string> Issues { get; init; }

    /// <summary>
    ///     Names of the library source files declaring an extension over <see cref="TypeName" />.
    ///     An empty list marks a collection the library does not wrap yet.
    /// </summary>
    public required IReadOnlyList<string> ImplementationFiles { get; init; }
}
