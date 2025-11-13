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
        /// <summary>Gets a TemporaryGraphicsManager reference of the document.</summary>
        /// <returns>Instance of TemporaryGraphicsManager.</returns>
        [Pure]
        public TemporaryGraphicsManager GetTemporaryGraphicsManager()
        {
            return TemporaryGraphicsManager.GetTemporaryGraphicsManager(document);
        }
    }
}
#endif