namespace Nice3point.Revit.Extensions.Tests.Coverage.Models;

/// <summary>
///     The members a Revit API collection leaves to an extension.
/// </summary>
/// <remarks>
///     Every issue names a member an extension can add. A missing interface is absent from the set: an extension method
///     adds no interface to a type.
/// </remarks>
public static class ApiCollectionIssues
{
    /// <summary>
    ///     The collection implements the non-generic <c>IEnumerable</c> alone. Every LINQ query starts with a <c>Cast</c>.
    /// </summary>
    public const string NoGenericEnumerable = "no IEnumerable<T>";

    /// <summary>
    ///     A <c>foreach</c> over the collection yields <see cref="object"/>.
    /// </summary>
    public const string UntypedEnumeration = "enumeration yields object";

    /// <summary>
    ///     The enumeration walks the values alone. The key of the current entry stays on the concrete iterator.
    /// </summary>
    public const string KeyOutsideEnumeration = "key outside the enumeration";

    /// <summary>
    ///     The collection counts through <c>Size</c>. LINQ <c>Count</c> walks the whole collection.
    /// </summary>
    public const string NoCount = "no Count";

    /// <summary>
    ///     The collection inserts through <c>Insert</c> or <c>Append</c>.
    /// </summary>
    public const string NoAdd = "no Add";

    /// <summary>
    ///     The collection removes through <c>Erase</c>.
    /// </summary>
    public const string NoRemove = "no Remove";

    /// <summary>
    ///     A safe lookup costs a <c>Contains</c> call plus an indexer call.
    /// </summary>
    public const string NoTryGetValue = "no TryGetValue";

    /// <summary>
    ///     The iterator factory hands out a disposable iterator holding a native handle.
    ///     A hand-written <c>while</c> loop over it holds the handle until collection. The <c>foreach</c> pattern disposes the iterator.
    /// </summary>
    public const string DisposableIterator = "iterator needs disposal";
}
