using Autodesk.Revit.DB.Structure.StructuralSections;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.Structure.StructuralSections;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Structure.StructuralSections.StructuralSectionUtils" /> class.
/// </summary>
[PublicAPI]
public static class StructuralSectionUtilsExtensions
{
    /// <param name="familyInstance">The source family instance.</param>
    extension(FamilyInstance familyInstance)
    {
        /// <inheritdoc
        ///     cref="Autodesk.Revit.DB.Structure.StructuralSections.StructuralSectionUtils.GetStructuralElementDefinitionData(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,out Autodesk.Revit.DB.Structure.StructuralSections.StructuralElementDefinitionData)" />
        [Pure]
        public StructuralSectionErrorCode GetStructuralElementDefinitionData(out StructuralElementDefinitionData? data)
        {
            return StructuralSectionUtils.GetStructuralElementDefinitionData(familyInstance.Document, familyInstance.Id, out data);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.StructuralSections.StructuralSectionUtils.GetStructuralSection(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public StructuralSection? GetStructuralSection()
        {
            return StructuralSectionUtils.GetStructuralSection(familyInstance.Document, familyInstance.Id);
        }
    }

    /// <param name="familySymbol">The source family symbol.</param>
    extension(FamilySymbol familySymbol)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.StructuralSections.StructuralSectionUtils.SetStructuralSection(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.Structure.StructuralSections.StructuralSection)" />
        public bool SetStructuralSection(StructuralSection structuralSection)
        {
            return StructuralSectionUtils.SetStructuralSection(familySymbol.Document, familySymbol.Id, structuralSection);
        }
    }

    /// <param name="elementId">The element id.</param>
    extension(ElementId elementId)
    {
        /// <inheritdoc
        ///     cref="Autodesk.Revit.DB.Structure.StructuralSections.StructuralSectionUtils.GetStructuralElementDefinitionData(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,out Autodesk.Revit.DB.Structure.StructuralSections.StructuralElementDefinitionData)" />
        [Pure]
        public StructuralSectionErrorCode GetStructuralElementDefinitionData(Document document, out StructuralElementDefinitionData? data)
        {
            return StructuralSectionUtils.GetStructuralElementDefinitionData(document, elementId, out data);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.StructuralSections.StructuralSectionUtils.GetStructuralSection(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public StructuralSection? GetStructuralSection(Document document)
        {
            return StructuralSectionUtils.GetStructuralSection(document, elementId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.StructuralSections.StructuralSectionUtils.SetStructuralSection(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.Structure.StructuralSections.StructuralSection)" />
        public bool SetStructuralSection(Document document, StructuralSection structuralSection)
        {
            return StructuralSectionUtils.SetStructuralSection(document, elementId, structuralSection);
        }
    }
}
