#if REVIT2022_OR_GREATER
// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.TemporaryGraphicsManager"/> class.
/// </summary>
[PublicAPI]
public static class TemporaryGraphicsManagerExtensions
{
    /// <summary>Gets a TemporaryGraphicsManager reference of the document.</summary>
    /// <param name="document">The document.</param>
    /// <returns>Instance of TemporaryGraphicsManager.</returns>
    [Pure]
    public static TemporaryGraphicsManager GetTemporaryGraphicsManager(this Document document)
    {
        return TemporaryGraphicsManager.GetTemporaryGraphicsManager(document);
    }
}
#endif