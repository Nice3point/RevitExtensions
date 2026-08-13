using Autodesk.Revit.DB.Analysis;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.MassInstanceUtils" /> class.
/// </summary>
[PublicAPI]
public static class MassInstanceUtilsExtensions
{
    /// <param name="massInstance">The source mass instance element.</param>
    extension(FamilyInstance massInstance)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Analysis.MassLevelData.IsMassFamilyInstance(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        public bool IsMassFamilyInstance => MassLevelData.IsMassFamilyInstance(massInstance.Document, massInstance.Id);

        /// <inheritdoc cref="Autodesk.Revit.DB.MassInstanceUtils.GetGrossFloorArea(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public double GetMassGrossFloorArea()
        {
            return MassInstanceUtils.GetGrossFloorArea(massInstance.Document, massInstance.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.MassInstanceUtils.GetGrossSurfaceArea(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public double GetMassGrossSurfaceArea()
        {
            return MassInstanceUtils.GetGrossSurfaceArea(massInstance.Document, massInstance.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.MassInstanceUtils.GetGrossVolume(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public double GetMassGrossVolume()
        {
            return MassInstanceUtils.GetGrossVolume(massInstance.Document, massInstance.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.MassInstanceUtils.GetJoinedElementIds(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public IList<ElementId> GetMassJoinedElementIds()
        {
            return MassInstanceUtils.GetJoinedElementIds(massInstance.Document, massInstance.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.MassInstanceUtils.GetMassLevelDataIds(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public IList<ElementId> GetMassLevelDataIds()
        {
            return MassInstanceUtils.GetMassLevelDataIds(massInstance.Document, massInstance.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.MassInstanceUtils.GetMassLevelIds(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public IList<ElementId> GetMassLevelIds()
        {
            return MassInstanceUtils.GetMassLevelIds(massInstance.Document, massInstance.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.MassInstanceUtils.AddMassLevelDataToMassInstance(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId)" />
        public ElementId AddMassLevelData(ElementId levelId)
        {
            return MassInstanceUtils.AddMassLevelDataToMassInstance(massInstance.Document, massInstance.Id, levelId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.MassInstanceUtils.RemoveMassLevelDataFromMassInstance(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId)" />
        public void RemoveMassLevelData(ElementId levelId)
        {
            MassInstanceUtils.RemoveMassLevelDataFromMassInstance(massInstance.Document, massInstance.Id, levelId);
        }
    }

    /// <param name="massInstanceId">The mass instance element id.</param>
    extension(ElementId massInstanceId)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Analysis.MassLevelData.IsMassFamilyInstance(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public bool IsMassFamilyInstance(Document document)
        {
            return MassLevelData.IsMassFamilyInstance(document, massInstanceId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.MassInstanceUtils.GetGrossFloorArea(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public double GetMassGrossFloorArea(Document document)
        {
            return MassInstanceUtils.GetGrossFloorArea(document, massInstanceId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.MassInstanceUtils.GetGrossSurfaceArea(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public double GetMassGrossSurfaceArea(Document document)
        {
            return MassInstanceUtils.GetGrossSurfaceArea(document, massInstanceId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.MassInstanceUtils.GetGrossVolume(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public double GetMassGrossVolume(Document document)
        {
            return MassInstanceUtils.GetGrossVolume(document, massInstanceId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.MassInstanceUtils.GetJoinedElementIds(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public IList<ElementId> GetMassJoinedElementIds(Document document)
        {
            return MassInstanceUtils.GetJoinedElementIds(document, massInstanceId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.MassInstanceUtils.GetMassLevelDataIds(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public IList<ElementId> GetMassLevelDataIds(Document document)
        {
            return MassInstanceUtils.GetMassLevelDataIds(document, massInstanceId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.MassInstanceUtils.GetMassLevelIds(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public IList<ElementId> GetMassLevelIds(Document document)
        {
            return MassInstanceUtils.GetMassLevelIds(document, massInstanceId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.MassInstanceUtils.AddMassLevelDataToMassInstance(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId)" />
        public ElementId AddMassLevelData(Document document, ElementId levelId)
        {
            return MassInstanceUtils.AddMassLevelDataToMassInstance(document, massInstanceId, levelId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.MassInstanceUtils.RemoveMassLevelDataFromMassInstance(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId)" />
        public void RemoveMassLevelData(Document document, ElementId levelId)
        {
            MassInstanceUtils.RemoveMassLevelDataFromMassInstance(document, massInstanceId, levelId);
        }
    }
}
