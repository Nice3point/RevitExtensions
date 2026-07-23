

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.LoadedFamilyIntegrityCheck"/> class.
/// </summary>
[PublicAPI]
public static class LoadedFamilyIntegrityCheckExtensions
{
    /// <param name="document">The source document.</param>
    extension(Document document)
    {
        /// <summary>
        ///    Check that all families loaded in the host document have their content documents.
        /// </summary>
        /// <param name="corruptFamilyIds">
        ///    Return ids of families that need to be reloaded because their content documents are missing.
        /// </param>
        /// <returns>Returns true if all loaded families have their content documents.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public bool CheckAllFamilies(out ISet<ElementId> corruptFamilyIds)
        {
            corruptFamilyIds = new HashSet<ElementId>();
            return LoadedFamilyIntegrityCheck.CheckAllFamilies(document, corruptFamilyIds);
        }

        /// <summary>
        ///    Check integrity of content documents of all families loaded in the host document.
        /// </summary>
        /// <remarks>
        ///    This check is slow as it invloves traversal of all content documents.
        ///    It also dumps data about bad families into the journal, as well as the whole content tree into the dump file.
        /// </remarks>
        /// <param name="corruptFamilyIds">
        ///    Return ids of families that need to be reloaded because their content documents are missing or corrupt.
        /// </param>
        /// <returns>Returns true if all content documents are usable.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
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
        /// <summary>Check that the loaded family has its content document.</summary>
        /// <returns>Returns true if the family has its content document.</returns>
        [Pure]
        public bool CheckIntegrity()
        {
            return LoadedFamilyIntegrityCheck.CheckFamily(family.Document, family.Id);
        }
    }

    /// <param name="elementId">The family element id.</param>
    extension(ElementId elementId)
    {
        /// <summary>Check that the loaded family has its content document.</summary>
        /// <param name="document">The host document.</param>
        /// <returns>Returns true if the family has its content document.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public bool CheckFamilyIntegrity(Document document)
        {
            return LoadedFamilyIntegrityCheck.CheckFamily(document, elementId);
        }
    }
}