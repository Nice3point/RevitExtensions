

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.ExportUtils"/> class.
/// </summary>
[PublicAPI]
public static class ExportUtilsExtensions
{
    /// <param name="document">The source document.</param>
    extension(Document document)
    {
        /// <summary>Retrieves the GUID representing this document in exported gbXML files.</summary>
        /// <remarks>
        ///    This id can be used to cross-reference different gbXML exports from the same document.
        /// </remarks>
        /// <returns>The value of the GUID representing this document in gbXML export.</returns>
        public Guid GbXmlId => ExportUtils.GetGBXMLDocumentId(document);
    }

    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <summary>Retrieves the GUID representing this element in DWF and IFC export.</summary>
        /// <remarks>
        ///    This id is used in the contents of DWF export and IFC export and it should be used
        ///    only when cross-referencing to the contents of these export formats.
        ///    When storing Ids that will need to be mapped back to elements in future sessions,
        ///    <see cref="P:Autodesk.Revit.DB.Element.UniqueId" /> must be used.
        /// </remarks>
        public Guid ExportId => ExportUtils.GetExportId(element.Document, element.Id);
    }

    /// <param name="subelement">The source subelement.</param>
    extension(Subelement subelement)
    {
        /// <summary>Retrieves the GUID representing the subelement in DWF and IFC export.</summary>
        /// <remarks>
        ///    This id is used in the contents of DWF export and IFC export and it should be used
        ///    only when cross-referencing to the contents of these export formats.
        ///    When storing Ids that will need to be mapped back to subelements in future sessions,
        ///    <see cref="P:Autodesk.Revit.DB.Subelement.UniqueId" /> must be used.
        /// </remarks>
        /// <returns>The value of the GUID representing the subelement in the export context.</returns>
        public Guid ExportId => ExportUtils.GetExportId(subelement);
    }

    /// <param name="elementId">The element id.</param>
    extension(ElementId elementId)
    {
        /// <summary>Retrieves the GUID representing this element in DWF and IFC export.</summary>
        /// <remarks>
        ///    This id is used in the contents of DWF export and IFC export and it should be used
        ///    only when cross-referencing to the contents of these export formats.
        ///    When storing Ids that will need to be mapped back to elements in future sessions,
        ///    <see cref="P:Autodesk.Revit.DB.Element.UniqueId" /> must be used.
        /// </remarks>
        /// <param name="document">The document.</param>
        /// <returns>The value of the GUID representing the element in the export context.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
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
        /// <summary>
        ///    Returns the necessary information to define a NURBS surface
        ///    for a given <see cref="!:Autodesk::Revit::DB::HermiteSuface" /> or <see cref="!:Autodesk::Revit::DB::RuledSuface" />.
        /// </summary>
        /// <remarks>This function is intended for export purposes.</remarks>
        /// <returns>A class containing the necessary data to define a NURBS surface.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    This surface type is not supported for this function.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Couldn't get NURBS data from surface.
        /// </exception>
        [Pure]
        public NurbsSurfaceData GetNurbsSurfaceData()
        {
            return ExportUtils.GetNurbsSurfaceDataForSurface(surface);
        }
    }
#endif
}