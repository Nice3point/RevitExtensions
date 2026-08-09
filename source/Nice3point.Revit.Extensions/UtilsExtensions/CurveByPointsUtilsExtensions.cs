

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.CurveByPointsUtils"/> class.
/// </summary>
[PublicAPI]
public static class CurveByPointsUtilsExtensions
{
    /// <param name="curveElement">The source curve element.</param>
    extension(CurveElement curveElement)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.CurveByPointsUtils.GetHostFace(Autodesk.Revit.DB.CurveElement)"/>
        [Pure]
        public Reference GetHostFace()
        {
            return CurveByPointsUtils.GetHostFace(curveElement);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.CurveByPointsUtils.GetProjectionType(Autodesk.Revit.DB.CurveElement)"/>
        [Pure]
        public CurveProjectionType GetProjectionType()
        {
            return CurveByPointsUtils.GetProjectionType(curveElement);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.CurveByPointsUtils.SetProjectionType(Autodesk.Revit.DB.CurveElement,Autodesk.Revit.DB.CurveProjectionType)"/>
        public void SetProjectionType(CurveProjectionType value)
        {
            CurveByPointsUtils.SetProjectionType(curveElement, value);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.CurveByPointsUtils.GetSketchOnSurface(Autodesk.Revit.DB.CurveElement)"/>
        [Pure]
        public bool GetSketchOnSurface()
        {
            return CurveByPointsUtils.GetSketchOnSurface(curveElement);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.CurveByPointsUtils.SetSketchOnSurface(Autodesk.Revit.DB.CurveElement,System.Boolean)"/>
        public void SetSketchOnSurface(bool sketchOnSurface)
        {
            CurveByPointsUtils.SetSketchOnSurface(curveElement, sketchOnSurface);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.CurveByPointsUtils.CreateArcThroughPoints(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ReferencePoint,Autodesk.Revit.DB.ReferencePoint,Autodesk.Revit.DB.ReferencePoint)"/>
        public static CurveElement CreateArcThroughPoints(Document document, ReferencePoint startPoint, ReferencePoint endPoint, ReferencePoint interiorPoint)
        {
            return CurveByPointsUtils.CreateArcThroughPoints(document, startPoint, endPoint, interiorPoint);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.CurveByPointsUtils.AddCurvesToFaceRegion(Autodesk.Revit.DB.Document,System.Collections.Generic.IList{Autodesk.Revit.DB.ElementId})"/>
        public static void AddCurvesToFaceRegion(Document document, IList<ElementId> curveElementIds)
        {
            CurveByPointsUtils.AddCurvesToFaceRegion(document, curveElementIds);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.CurveByPointsUtils.CreateRectangle(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ReferencePoint,Autodesk.Revit.DB.ReferencePoint,Autodesk.Revit.DB.CurveProjectionType,System.Boolean,System.Boolean,out System.Collections.Generic.IList{Autodesk.Revit.DB.ElementId},out System.Collections.Generic.IList{Autodesk.Revit.DB.ElementId})"/>
        public static void CreateRectangle(Document document, ReferencePoint startPoint, ReferencePoint endPoint, CurveProjectionType projectionType, bool boundaryReferenceLines, bool boundaryCurvesFollowSurface, out IList<ElementId> createdCurvesIds, out IList<ElementId> createdCornersIds)
        {
            CurveByPointsUtils.CreateRectangle(document, startPoint, endPoint, projectionType, boundaryReferenceLines, boundaryCurvesFollowSurface, out createdCurvesIds, out createdCornersIds);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.CurveByPointsUtils.ValidateCurveElementIdArrayForFaceRegions(Autodesk.Revit.DB.Document,System.Collections.Generic.IList{Autodesk.Revit.DB.ElementId})"/>
        [Pure]
        public static bool ValidateForFaceRegions(Document document, IList<ElementId> curveElemIds)
        {
            return CurveByPointsUtils.ValidateCurveElementIdArrayForFaceRegions(document, curveElemIds);
        }
    }

    /// <param name="reference">The source reference of face.</param>
    extension(Reference reference)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.CurveByPointsUtils.GetFaceRegions(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Reference)"/>
        [Pure]
        public IList<Reference> GetFaceRegions(Document document)
        {
            return CurveByPointsUtils.GetFaceRegions(document, reference);
        }
    }
}