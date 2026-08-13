// ReSharper disable once CheckNamespace

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.ExportUtils" /> class.
/// </summary>
[PublicAPI]
public static class ExportUtilsExtensions
{
    /// <param name="document">The source document.</param>
    extension(Document document)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ExportUtils.GetGBXMLDocumentId(Autodesk.Revit.DB.Document)" />
        public Guid GbXmlId => ExportUtils.GetGBXMLDocumentId(document);
    }

    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ExportUtils.GetExportId(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        public Guid ExportId => ExportUtils.GetExportId(element.Document, element.Id);
    }

    /// <param name="subelement">The source subelement.</param>
    extension(Subelement subelement)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ExportUtils.GetExportId(Autodesk.Revit.DB.Subelement)" />
        public Guid ExportId => ExportUtils.GetExportId(subelement);
    }

    /// <param name="elementId">The element id.</param>
    extension(ElementId elementId)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ExportUtils.GetExportId(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public Guid GetExportId(Document document)
        {
            return ExportUtils.GetExportId(document, elementId);
        }
    }
#if REVIT2021_OR_GREATER
    /// <param name="surface">The source surface.</param>
    extension(Surface surface)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ExportUtils.GetNurbsSurfaceDataForSurface(Autodesk.Revit.DB.Surface)"/>
        [Pure]
        public NurbsSurfaceData GetNurbsSurfaceData()
        {
            return ExportUtils.GetNurbsSurfaceDataForSurface(surface);
        }
    }
#endif
}
