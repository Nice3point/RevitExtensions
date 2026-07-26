using Nice3point.Revit.Extensions.Tests.Coverage.Models;

namespace Nice3point.Revit.Extensions.Tests.Coverage.Discovery;

/// <summary>
///     The reflected shape of a Revit API collection.
/// </summary>
internal sealed record ApiCollectionShape
{
    /// <summary>
    ///     The collection type.
    /// </summary>
    public required Type CollectionType { get; init; }

    /// <summary>
    ///     The shape the collection enumerates.
    /// </summary>
    public required ApiCollectionKind Kind { get; init; }

    /// <summary>
    ///     The iterator type the collection returns from its iterator factory.
    /// </summary>
    public required Type IteratorType { get; init; }

    /// <summary>
    ///     Whether the collection hands out the iterator through a factory method, the shape a hand-written loop consumes.
    /// </summary>
    public required bool HasIteratorFactory { get; init; }

    /// <summary>
    ///     The key type the iterator exposes, or <c>null</c> outside a map.
    /// </summary>
    public required Type? KeyType { get; init; }

    /// <summary>
    ///     The element type the collection holds.
    /// </summary>
    public required Type ElementType { get; init; }

    /// <summary>
    ///     The type a <c>foreach</c> over the collection yields.
    /// </summary>
    public required Type EnumeratedType { get; init; }
}
