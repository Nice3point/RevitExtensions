// ReSharper disable once CheckNamespace

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.LoadedFamilyIntegrityCheck" /> class.
/// </summary>
[PublicAPI]
public static class LoadedFamilyIntegrityCheckExtensions
{
    /// <param name="document">The source document.</param>
    extension(Document document)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.LoadedFamilyIntegrityCheck.CheckAllFamilies(Autodesk.Revit.DB.Document,System.Collections.Generic.ISet{Autodesk.Revit.DB.ElementId})" />
        [Pure]
        public bool CheckAllFamilies(out ISet<ElementId> corruptFamilyIds)
        {
            corruptFamilyIds = new HashSet<ElementId>();
            return LoadedFamilyIntegrityCheck.CheckAllFamilies(document, corruptFamilyIds);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.LoadedFamilyIntegrityCheck.CheckAllFamiliesSlow(Autodesk.Revit.DB.Document,System.Collections.Generic.ISet{Autodesk.Revit.DB.ElementId})" />
        [Pure]
        public bool CheckAllFamiliesSlow(out ISet<ElementId> corruptFamilyIds)
        {
            corruptFamilyIds = new HashSet<ElementId>();
            return LoadedFamilyIntegrityCheck.CheckAllFamiliesSlow(document, corruptFamilyIds);
        }
    }

    /// <param name="family">The source family.</param>
    extension(Family family)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.LoadedFamilyIntegrityCheck.CheckFamily(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public bool CheckIntegrity()
        {
            return LoadedFamilyIntegrityCheck.CheckFamily(family.Document, family.Id);
        }
    }

    /// <param name="elementId">The family element id.</param>
    extension(ElementId elementId)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.LoadedFamilyIntegrityCheck.CheckFamily(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public bool CheckFamilyIntegrity(Document document)
        {
            return LoadedFamilyIntegrityCheck.CheckFamily(document, elementId);
        }
    }
}
