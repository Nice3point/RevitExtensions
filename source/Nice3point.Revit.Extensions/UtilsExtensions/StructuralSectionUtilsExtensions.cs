using Autodesk.Revit.DB.Structure.StructuralSections;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.Structure.StructuralSections;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Structure.StructuralSections.StructuralSectionUtils"/> class.
/// </summary>
[PublicAPI]
public static class StructuralSectionUtilsExtensions
{
    /// <param name="familyInstance">The source family instance.</param>
    extension(FamilyInstance familyInstance)
    {
        /// <summary>Return structural element definition data.</summary>
        /// <remarks>
        ///    This information is provided only for beams, braces and structural columns.
        /// </remarks>
        /// <param name="data">Structural element definition data.</param>
        /// <returns>
        ///    Success code is returned if StructuralElementDefinitionData was provided successfully, error code otherwise.
        /// </returns>
        [Pure]
        public StructuralSectionErrorCode GetStructuralElementDefinitionData(out StructuralElementDefinitionData? data)
        {
            return StructuralSectionUtils.GetStructuralElementDefinitionData(familyInstance.Document, familyInstance.Id, out data);
        }

        /// <summary>Return structural section from element.</summary>
        /// <remarks>
        ///    Only beams, braces and structural columns can have structural section associated with it.
        /// </remarks>
        /// <returns>
        ///    Structural section returned if element have one.
        ///    For elements that do not have structural section or can not have structural section <see langword="null" /> will be returned.
        /// </returns>
        [Pure]
        public StructuralSection? GetStructuralSection()
        {
            return StructuralSectionUtils.GetStructuralSection(familyInstance.Document, familyInstance.Id);
        }
    }

    /// <param name="familySymbol">The source family symbol.</param>
    extension(FamilySymbol familySymbol)
    {
        /// <summary>Set structural section in element.</summary>
        /// <remarks>
        ///    Only beams, braces and structural columns can have structural section associated with it.
        /// </remarks>
        /// <param name="structuralSection">Structural section with values that will be set.</param>
        /// <returns>
        ///    True is returned when requested shape with values was properly set. Return false otherwise.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public bool SetStructuralSection(StructuralSection structuralSection)
        {
            return StructuralSectionUtils.SetStructuralSection(familySymbol.Document, familySymbol.Id, structuralSection);
        }
    }

    /// <param name="elementId">The element id.</param>
    extension(ElementId elementId)
    {
        /// <summary>Return structural element definition data.</summary>
        /// <remarks>
        ///    This information is provided only for beams, braces and structural columns.
        /// </remarks>
        /// <param name="document">The document that owns the beam, brace or structural column.</param>
        /// <param name="data">Structural element definition data.</param>
        /// <returns>
        ///    Success code is returned if StructuralElementDefinitionData was provided successfully, error code otherwise.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public StructuralSectionErrorCode GetStructuralElementDefinitionData(Document document, out StructuralElementDefinitionData? data)
        {
            return StructuralSectionUtils.GetStructuralElementDefinitionData(document, elementId, out data);
        }

        /// <summary>Return structural section from element.</summary>
        /// <remarks>
        ///    Only beams, braces and structural columns can have structural section associated with it.
        /// </remarks>
        /// <param name="document">
        ///    The document that owns the family for beam, brace or structural column.
        /// </param>
        /// <returns>
        ///    Structural section returned if element have one.
        ///    For elements that do not have structural section or can not have structural section <see langword="null" /> will be returned.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public StructuralSection? GetStructuralSection(Document document)
        {
            return StructuralSectionUtils.GetStructuralSection(document, elementId);
        }

        /// <summary>Set structural section in element.</summary>
        /// <remarks>
        ///    Only beams, braces and structural columns can have structural section associated with it.
        /// </remarks>
        /// <param name="document">
        ///    The document that owns the family for beam, brace or structural column.
        /// </param>
        /// <param name="structuralSection">Structural section with values that will be set.</param>
        /// <returns>
        ///    True is returned when requested shape with values was properly set. Return false otherwise.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public bool SetStructuralSection(Document document, StructuralSection structuralSection)
        {
            return StructuralSectionUtils.SetStructuralSection(document, elementId, structuralSection);
        }
    }
}