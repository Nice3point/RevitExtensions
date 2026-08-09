

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.ExternalResourceUtils"/> class.
/// </summary>
[PublicAPI]
public static class ExternalResourceUtilsExtensions
{
    /// <param name="document">The source document.</param>
    extension(Document document)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalResourceUtils.GetAllExternalResourceReferences(Autodesk.Revit.DB.Document)"/>
        [Pure]
        public ISet<ElementId> GetAllExternalResourceReferences()
        {
            return ExternalResourceUtils.GetAllExternalResourceReferences(document);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalResourceUtils.GetAllExternalResourceReferences(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ExternalResourceType)"/>
        [Pure]
        public ISet<ElementId> GetAllExternalResourceReferences(ExternalResourceType resourceType)
        {
            return ExternalResourceUtils.GetAllExternalResourceReferences(document, resourceType);
        }
    }
}