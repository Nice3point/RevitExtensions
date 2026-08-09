

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.ExternalFileUtils"/> class.
/// </summary>
[PublicAPI]
public static class ExternalFileUtilsExtensions
{
    /// <param name="document">The source document.</param>
    extension(Document document)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalFileUtils.GetAllExternalFileReferences(Autodesk.Revit.DB.Document)"/>
        [Pure]
        public ICollection<ElementId> GetAllExternalFileReferences()
        {
            return ExternalFileUtils.GetAllExternalFileReferences(document);
        }
    }

    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalFileUtils.IsExternalFileReference(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        public bool IsExternalFileReference => ExternalFileUtils.IsExternalFileReference(element.Document, element.Id);

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalFileUtils.GetExternalFileReference(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public ExternalFileReference GetExternalFileReference()
        {
            return ExternalFileUtils.GetExternalFileReference(element.Document, element.Id);
        }
    }

    /// <param name="elementId">The element id.</param>
    extension(ElementId elementId)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalFileUtils.IsExternalFileReference(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public bool IsExternalFileReference(Document document)
        {
            return ExternalFileUtils.IsExternalFileReference(document, elementId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalFileUtils.GetExternalFileReference(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public ExternalFileReference GetExternalFileReference(Document document)
        {
            return ExternalFileUtils.GetExternalFileReference(document, elementId);
        }
    }
}