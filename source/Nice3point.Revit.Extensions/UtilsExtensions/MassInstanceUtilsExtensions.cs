using Autodesk.Revit.DB.Analysis;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.MassInstanceUtils"/> class.
/// </summary>
[PublicAPI]
public static class MassInstanceUtilsExtensions
{
    /// <param name="massInstance">The source mass instance element.</param>
    extension(FamilyInstance massInstance)
    {
        /// <summary>Checks if the ElementId is a mass family instance.</summary>
        /// <returns>True if the ElementId is a mass family instance, false otherwise.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public bool IsMassFamilyInstance => MassLevelData.IsMassFamilyInstance(massInstance.Document, massInstance.Id);

        /// <summary>Get the total occupiable floor area represented by a mass instance.</summary>
        /// <remarks>
        ///    The area is computed from the cross sections that are created by intersecting the
        ///    associated Levels with the mass instance Geometry.
        /// </remarks>
        /// <returns>The gross floor area in square feet.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The ElementId massInstanceId is not a mass instance.
        /// </exception>
        [Pure]
        public double GetMassGrossFloorArea()
        {
            return MassInstanceUtils.GetGrossFloorArea(massInstance.Document, massInstance.Id);
        }

        /// <summary>
        ///    Get the total exterior building surface area represented by a mass instance.
        /// </summary>
        /// <returns>The gross surface area in square feet.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The ElementId massInstanceId is not a mass instance.
        /// </exception>
        [Pure]
        public double GetMassGrossSurfaceArea()
        {
            return MassInstanceUtils.GetGrossSurfaceArea(massInstance.Document, massInstance.Id);
        }

        /// <summary>Get the total building volume represented by a mass instance.</summary>
        /// <returns>The gross volume in cubic feet.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The ElementId massInstanceId is not a mass instance.
        /// </exception>
        [Pure]
        public double GetMassGrossVolume()
        {
            return MassInstanceUtils.GetGrossVolume(massInstance.Document, massInstance.Id);
        }

        /// <summary>Get the ElementIds of Elements that are joined to a mass instance.</summary>
        /// <returns>ElementIds of Elements joined to the mass instance.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The ElementId massInstanceId is not a mass instance.
        /// </exception>
        [Pure]
        public IList<ElementId> GetMassJoinedElementIds()
        {
            return MassInstanceUtils.GetJoinedElementIds(massInstance.Document, massInstance.Id);
        }

        /// <summary>
        ///    Get the ElementIds of the MassLevelDatas (Mass Floors) associated with a mass instance.
        /// </summary>
        /// <returns>The ElementIds of the MassLevelDatas.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The ElementId massInstanceId is not a mass instance.
        /// </exception>
        [Pure]
        public IList<ElementId> GetMassLevelDataIds()
        {
            return MassInstanceUtils.GetMassLevelDataIds(massInstance.Document, massInstance.Id);
        }

        /// <summary>Get the ElementIds of the Levels associated with a mass instance.</summary>
        /// <returns>The ElementIds of the Levels</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The ElementId massInstanceId is not a mass instance.
        /// </exception>
        [Pure]
        public IList<ElementId> GetMassLevelIds()
        {
            return MassInstanceUtils.GetMassLevelIds(massInstance.Document, massInstance.Id);
        }

        /// <summary>
        ///    Create a MassLevelData (Mass Floor) to associate a Level with a mass instance.
        /// </summary>
        /// <param name="levelId">The ElementId of the Level to associate with the mass instance.</param>
        /// <returns>
        ///    The ElementId of the MassLevelData that was created, or the existing ElementId if it was already in added.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The ElementId massInstanceId is not a mass instance.
        ///    -or-
        ///    The ElementId levelId is not a Level.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public ElementId AddMassLevelData(ElementId levelId)
        {
            return MassInstanceUtils.AddMassLevelDataToMassInstance(massInstance.Document, massInstance.Id, levelId);
        }

        /// <summary>
        ///    Delete the MassLevelData (Mass Floor) that associates a Level with a mass instance.
        /// </summary>
        /// <remarks>Alternatively, you could just delete the MassLevelData.</remarks>
        /// <param name="levelId">
        ///    The ElementId of the Level to disassociate from the mass instance.
        /// </param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The ElementId massInstanceId is not a mass instance.
        ///    -or-
        ///    The ElementId levelId is not a Level.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public void RemoveMassLevelData(ElementId levelId)
        {
            MassInstanceUtils.RemoveMassLevelDataFromMassInstance(massInstance.Document, massInstance.Id, levelId);
        }
    }

    /// <param name="massInstanceId">The mass instance element id.</param>
    extension(ElementId massInstanceId)
    {
        /// <summary>Checks if the ElementId is a mass family instance.</summary>
        /// <param name="document">The document.</param>
        /// <returns>True if the ElementId is a mass family instance, false otherwise.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public bool IsMassFamilyInstance(Document document)
        {
            return MassLevelData.IsMassFamilyInstance(document, massInstanceId);
        }

        /// <summary>Get the total occupiable floor area represented by a mass instance.</summary>
        /// <remarks>
        ///    The area is computed from the cross sections that are created by intersecting the
        ///    associated Levels with the mass instance Geometry.
        /// </remarks>
        /// <param name="document">The Document.</param>
        /// <returns>The gross floor area in square feet.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The ElementId massInstanceId is not a mass instance.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public double GetMassGrossFloorArea(Document document)
        {
            return MassInstanceUtils.GetGrossFloorArea(document, massInstanceId);
        }

        /// <summary>
        ///    Get the total exterior building surface area represented by a mass instance.
        /// </summary>
        /// <param name="document">The Document.</param>
        /// <returns>The gross surface area in square feet.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The ElementId massInstanceId is not a mass instance.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public double GetMassGrossSurfaceArea(Document document)
        {
            return MassInstanceUtils.GetGrossSurfaceArea(document, massInstanceId);
        }

        /// <summary>Get the total building volume represented by a mass instance.</summary>
        /// <param name="document">The Document.</param>
        /// <returns>The gross volume in cubic feet.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The ElementId massInstanceId is not a mass instance.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public double GetMassGrossVolume(Document document)
        {
            return MassInstanceUtils.GetGrossVolume(document, massInstanceId);
        }

        /// <summary>Get the ElementIds of Elements that are joined to a mass instance.</summary>
        /// <param name="document">The Document.</param>
        /// <returns>ElementIds of Elements joined to the mass instance.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The ElementId massInstanceId is not a mass instance.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public IList<ElementId> GetMassJoinedElementIds(Document document)
        {
            return MassInstanceUtils.GetJoinedElementIds(document, massInstanceId);
        }

        /// <summary>
        ///    Get the ElementIds of the MassLevelDatas (Mass Floors) associated with a mass instance.
        /// </summary>
        /// <param name="document">The Document.</param>
        /// <returns>The ElementIds of the MassLevelDatas.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The ElementId massInstanceId is not a mass instance.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public IList<ElementId> GetMassLevelDataIds(Document document)
        {
            return MassInstanceUtils.GetMassLevelDataIds(document, massInstanceId);
        }

        /// <summary>Get the ElementIds of the Levels associated with a mass instance.</summary>
        /// <param name="document">The Document.</param>
        /// <returns>The ElementIds of the Levels</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The ElementId massInstanceId is not a mass instance.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public IList<ElementId> GetMassLevelIds(Document document)
        {
            return MassInstanceUtils.GetMassLevelIds(document, massInstanceId);
        }

        /// <summary>
        ///    Create a MassLevelData (Mass Floor) to associate a Level with a mass instance.
        /// </summary>
        /// <param name="document">The Document.</param>
        /// <param name="levelId">The ElementId of the Level to associate with the mass instance.</param>
        /// <returns>
        ///    The ElementId of the MassLevelData that was created, or the existing ElementId if it was already in added.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The ElementId massInstanceId is not a mass instance.
        ///    -or-
        ///    The ElementId levelId is not a Level.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public ElementId AddMassLevelData(Document document, ElementId levelId)
        {
            return MassInstanceUtils.AddMassLevelDataToMassInstance(document, massInstanceId, levelId);
        }

        /// <summary>
        ///    Delete the MassLevelData (Mass Floor) that associates a Level with a mass instance.
        /// </summary>
        /// <remarks>Alternatively, you could just delete the MassLevelData.</remarks>
        /// <param name="document">The Document.</param>
        /// <param name="levelId">
        ///    The ElementId of the Level to disassociate from the mass instance.
        /// </param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The ElementId massInstanceId is not a mass instance.
        ///    -or-
        ///    The ElementId levelId is not a Level.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public void RemoveMassLevelData(Document document, ElementId levelId)
        {
            MassInstanceUtils.RemoveMassLevelDataFromMassInstance(document, massInstanceId, levelId);
        }
    }
}