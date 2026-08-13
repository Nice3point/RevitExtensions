// ReSharper disable once CheckNamespace

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.AdaptiveComponentFamilyUtils" /> class.
/// </summary>
[PublicAPI]
public static class AdaptiveComponentFamilyUtilsExtensions
{
    /// <param name="family">The source family.</param>
    extension(Family family)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.AdaptiveComponentFamilyUtils.IsAdaptiveComponentFamily(Autodesk.Revit.DB.Family)" />
        public bool IsAdaptiveComponentFamily => AdaptiveComponentFamilyUtils.IsAdaptiveComponentFamily(family);

        /// <inheritdoc cref="Autodesk.Revit.DB.AdaptiveComponentFamilyUtils.GetNumberOfAdaptivePoints(Autodesk.Revit.DB.Family)" />
        [Pure]
        public int GetNumberOfAdaptivePoints()
        {
            return AdaptiveComponentFamilyUtils.GetNumberOfAdaptivePoints(family);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AdaptiveComponentFamilyUtils.GetNumberOfPlacementPoints(Autodesk.Revit.DB.Family)" />
        [Pure]
        public int GetNumberOfAdaptivePlacementPoints()
        {
            return AdaptiveComponentFamilyUtils.GetNumberOfPlacementPoints(family);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AdaptiveComponentFamilyUtils.GetNumberOfShapeHandlePoints(Autodesk.Revit.DB.Family)" />
        [Pure]
        public int GetNumberOfAdaptiveShapeHandlePoints()
        {
            return AdaptiveComponentFamilyUtils.GetNumberOfShapeHandlePoints(family);
        }
    }

    /// <param name="referencePoint">The source reference point.</param>
    extension(ReferencePoint referencePoint)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.AdaptiveComponentFamilyUtils.IsAdaptivePlacementPoint(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        public bool IsAdaptivePlacementPoint => AdaptiveComponentFamilyUtils.IsAdaptivePlacementPoint(referencePoint.Document, referencePoint.Id);

        /// <inheritdoc cref="Autodesk.Revit.DB.AdaptiveComponentFamilyUtils.IsAdaptivePoint(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        public bool IsAdaptivePoint => AdaptiveComponentFamilyUtils.IsAdaptivePoint(referencePoint.Document, referencePoint.Id);

        /// <inheritdoc cref="Autodesk.Revit.DB.AdaptiveComponentFamilyUtils.IsAdaptiveShapeHandlePoint(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        public bool IsAdaptiveShapeHandlePoint => AdaptiveComponentFamilyUtils.IsAdaptiveShapeHandlePoint(referencePoint.Document, referencePoint.Id);

        /// <inheritdoc cref="Autodesk.Revit.DB.AdaptiveComponentFamilyUtils.GetPlacementNumber(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public int GetAdaptivePlacementNumber()
        {
            return AdaptiveComponentFamilyUtils.GetPlacementNumber(referencePoint.Document, referencePoint.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AdaptiveComponentFamilyUtils.GetPointConstraintType(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public AdaptivePointConstraintType GetAdaptivePointConstraintType()
        {
            return AdaptiveComponentFamilyUtils.GetPointConstraintType(referencePoint.Document, referencePoint.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AdaptiveComponentFamilyUtils.GetPointOrientationType(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public AdaptivePointOrientationType GetAdaptivePointOrientationType()
        {
            return AdaptiveComponentFamilyUtils.GetPointOrientationType(referencePoint.Document, referencePoint.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AdaptiveComponentFamilyUtils.MakeAdaptivePoint(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.AdaptivePointType)" />
        public void MakeAdaptivePoint(AdaptivePointType type)
        {
            AdaptiveComponentFamilyUtils.MakeAdaptivePoint(referencePoint.Document, referencePoint.Id, type);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AdaptiveComponentFamilyUtils.SetPlacementNumber(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,System.Int32)" />
        public void SetAdaptivePlacementNumber(int placementNumber)
        {
            AdaptiveComponentFamilyUtils.SetPlacementNumber(referencePoint.Document, referencePoint.Id, placementNumber);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AdaptiveComponentFamilyUtils.SetPointConstraintType(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.AdaptivePointConstraintType)" />
        public void SetAdaptivePointConstraintType(AdaptivePointConstraintType constraintType)
        {
            AdaptiveComponentFamilyUtils.SetPointConstraintType(referencePoint.Document, referencePoint.Id, constraintType);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AdaptiveComponentFamilyUtils.SetPointOrientationType(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.AdaptivePointOrientationType)" />
        public void SetAdaptivePointOrientationType(AdaptivePointOrientationType orientationType)
        {
            AdaptiveComponentFamilyUtils.SetPointOrientationType(referencePoint.Document, referencePoint.Id, orientationType);
        }
    }
}
