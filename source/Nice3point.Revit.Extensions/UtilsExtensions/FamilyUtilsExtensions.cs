

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.FamilyUtils"/> class.
/// </summary>
[PublicAPI]
public static class FamilyUtilsExtensions
{
    /// <param name="family">The source family.</param>
    extension(Family family)
    {
        /// <summary></summary>
        [Pure]
        [Obsolete("Use CanBeConvertedToFaceHostBased() instead")]
        [CodeTemplate(
            searchTemplate: "$expr$.CanConvertToFaceHostBased()",
            Message = "CanConvertToFaceHostBased is obsolete, use CanBeConvertedToFaceHostBased instead",
            ReplaceTemplate = "$expr$.CanBeConvertedToFaceHostBased",
            ReplaceMessage = "Replace with CanBeConvertedToFaceHostBased")]
        public bool CanConvertToFaceHostBased()
        {
            return FamilyUtils.FamilyCanConvertToFaceHostBased(family.Document, family.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.FamilyUtils.FamilyCanConvertToFaceHostBased(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        public bool CanBeConvertedToFaceHostBased => FamilyUtils.FamilyCanConvertToFaceHostBased(family.Document, family.Id);

        /// <inheritdoc cref="Autodesk.Revit.DB.FamilyUtils.ConvertFamilyToFaceHostBased(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        public Family ConvertToFaceHostBased()
        {
            FamilyUtils.ConvertFamilyToFaceHostBased(family.Document, family.Id);
            return family;
        }
    }

    /// <param name="familySymbol">The source family symbol.</param>
    extension(FamilySymbol familySymbol)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.FamilyUtils.GetProfileSymbols(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ProfileFamilyUsage,System.Boolean)"/>
        [Pure]
        public static ICollection<ElementId> GetProfileSymbols(Document document, ProfileFamilyUsage profileFamilyUsage, bool oneCurveLoopOnly)
        {
            return FamilyUtils.GetProfileSymbols(document, profileFamilyUsage, oneCurveLoopOnly);
        }
    }

    /// <param name="elementId">The family element id.</param>
    extension(ElementId elementId)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.FamilyUtils.FamilyCanConvertToFaceHostBased(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public bool CanBeConvertedToFaceHostBased(Document document)
        {
            return FamilyUtils.FamilyCanConvertToFaceHostBased(document, elementId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.FamilyUtils.ConvertFamilyToFaceHostBased(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        public ElementId ConvertToFaceHostBased(Document document)
        {
            FamilyUtils.ConvertFamilyToFaceHostBased(document, elementId);
            return elementId;
        }
    }
}