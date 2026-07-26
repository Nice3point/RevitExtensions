namespace Nice3point.Revit.Extensions.Tests.Coverage.Models;

/// <summary>
///     The shape a Revit API collection enumerates.
/// </summary>
public enum ApiCollectionKind
{
    /// <summary>
    ///     A sequence reachable through iteration alone.
    /// </summary>
    Sequence,

    /// <summary>
    ///     A sequence carrying an indexer over a position.
    /// </summary>
    IndexedSequence,

    /// <summary>
    ///     A collection of entries whose iterator carries the key of the current entry.
    /// </summary>
    Map
}
