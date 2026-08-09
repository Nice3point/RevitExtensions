#if REVIT2022_OR_GREATER
// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.TemporaryGraphicsManager"/> class.
/// </summary>
[PublicAPI]
public static class TemporaryGraphicsManagerExtensions
{
    /// <param name="document">The source document.</param>
    extension(Document document)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.TemporaryGraphicsManager.GetTemporaryGraphicsManager(Autodesk.Revit.DB.Document)"/>
        [Pure]
        public TemporaryGraphicsManager GetTemporaryGraphicsManager()
        {
            return TemporaryGraphicsManager.GetTemporaryGraphicsManager(document);
        }
    }
}
#endif