

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
        /// <summary>Gets the host face to which the CurveElement is added.</summary>
        /// <returns>
        ///    The host face to which the CurveElement is added, or an empty Reference if the host is not a face.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The input CurveElement is not a CurveByPoints.
        /// </exception>
        [Pure]
        public Reference GetHostFace()
        {
            return CurveByPointsUtils.GetHostFace(curveElement);
        }

        /// <summary>Gets the projection type of the CurveElement.</summary>
        /// <returns>The projection type.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The input CurveElement is not a CurveByPoints.
        /// </exception>
        [Pure]
        public CurveProjectionType GetProjectionType()
        {
            return CurveByPointsUtils.GetProjectionType(curveElement);
        }

        /// <summary>Sets the projection type of the CurveElement.</summary>
        /// <param name="value">The input projection type.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The input CurveElement is not a CurveByPoints.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    A value passed for an enumeration argument is not a member of that enumeration
        /// </exception>
        public void SetProjectionType(CurveProjectionType value)
        {
            CurveByPointsUtils.SetProjectionType(curveElement, value);
        }

        /// <summary>Gets the relationship between the CurveElement and face.</summary>
        /// <returns>
        ///    Whether or not the CurveElement should lie on the face and be able to be added to the face.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The input CurveElement is not a CurveByPoints.
        /// </exception>
        [Pure]
        public bool GetSketchOnSurface()
        {
            return CurveByPointsUtils.GetSketchOnSurface(curveElement);
        }

        /// <summary>Sets the relationship between the CurveElement and face.</summary>
        /// <param name="sketchOnSurface">
        ///    Whether or not the CurveElement should lie on the face and be able to be added to the face.
        /// </param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The input CurveElement is not a CurveByPoints.
        /// </exception>
        public void SetSketchOnSurface(bool sketchOnSurface)
        {
            CurveByPointsUtils.SetSketchOnSurface(curveElement, sketchOnSurface);
        }

        /// <summary>Creates an arc through the given reference points.</summary>
        /// <remarks>
        ///    The interiorPoint determines the orientation of the arc while startPoint and endPoint determine
        ///    the angle parameters at the ends.
        /// </remarks>
        /// <param name="document">The Document.</param>
        /// <param name="startPoint">The start point of the arc.</param>
        /// <param name="endPoint">The end end of the arc.</param>
        /// <param name="interiorPoint">The interior point on the arc.</param>
        /// <returns>The CurveElement to be created.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Can't create an arc from the given points
        /// </exception>
        public static CurveElement CreateArcThroughPoints(Document document, ReferencePoint startPoint, ReferencePoint endPoint, ReferencePoint interiorPoint)
        {
            return CurveByPointsUtils.CreateArcThroughPoints(document, startPoint, endPoint, interiorPoint);
        }

        /// <summary>Adds The CurveElements to one or more FaceRegions.</summary>
        /// <remarks>
        ///    The CurveElements that are input may produce an arbitrary number of regions.
        /// </remarks>
        /// <param name="document">The Document.</param>
        /// <param name="curveElementIds">
        ///    The ElementIds of CurveElements which are to define the FaceRegion.
        /// </param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    All the input CurveElements must be CurveByPoints, with the sketchOnSurface attribute set to True, and for each CurveElement, the defining
        ///    ReferencePoints must be hosted on References related to a common Face or Edge.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Failed to add curves to FaceRegion.
        /// </exception>
        public static void AddCurvesToFaceRegion(Document document, IList<ElementId> curveElementIds)
        {
            CurveByPointsUtils.AddCurvesToFaceRegion(document, curveElementIds);
        }

        /// <summary>Creates rectangle on face or sketchplane for two given diagonal points.</summary>
        /// <remarks>
        ///    This array contains the ElementIds of the two additional corner points that are created to complete the rectangle.
        /// </remarks>
        /// <param name="document">The Document.</param>
        /// <param name="startPoint">First diagonal point of rectangle.</param>
        /// <param name="endPoint">Second diagonal point of rectangle.</param>
        /// <param name="projectionType">
        ///    Projection type of rectangle's boundary curves.
        ///    If the rectangle input points are Face hosted, and CurveProjectionType::ParallelToLevel is requested,
        ///    and the Face normal at the location of the start point is at a less than 45 degree angle with the level
        ///    planes, then the projectionType will be set to FromTopDown, even if ParallelToLevel was requested.
        /// </param>
        /// <param name="boundaryReferenceLines">
        ///    True if rectangle's boundary curves should be reference lines, false otherwise.
        /// </param>
        /// <param name="boundaryCurvesFollowSurface">
        ///    True if rectangle's boundary curves should follow surface, false otherwise.
        /// </param>
        /// <param name="createdCurvesIds">Created rectangle's boundary curves ids.</param>
        /// <param name="createdCornersIds">Ids of two newly created corner points.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    Unexpected projection type.
        ///    -or-
        ///    A value passed for an enumeration argument is not a member of that enumeration
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Unable to create rectangle.
        /// </exception>
        public static void CreateRectangle(Document document, ReferencePoint startPoint, ReferencePoint endPoint, CurveProjectionType projectionType, bool boundaryReferenceLines, bool boundaryCurvesFollowSurface, out IList<ElementId> createdCurvesIds, out IList<ElementId> createdCornersIds)
        {
            CurveByPointsUtils.CreateRectangle(document, startPoint, endPoint, projectionType, boundaryReferenceLines, boundaryCurvesFollowSurface, out createdCurvesIds, out createdCornersIds);
        }

        /// <summary>
        ///    Validates that the input CurveElements can define FaceRegions.
        ///    The CurveElements must be CurveByPoints.  Each curve must be entirely hosted by a single Face or hosts related to a common
        ///    Face (for example, Edges of a common Face, other CurveElements hosted by a common Face). To be added to the FaceRegion definition,
        ///    a CurveElement must have the SketchOnSurface attribute set.
        /// </summary>
        /// <param name="document">The Document.</param>
        /// <param name="curveElemIds">The CurveElements.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public static bool ValidateForFaceRegions(Document document, IList<ElementId> curveElemIds)
        {
            return CurveByPointsUtils.ValidateCurveElementIdArrayForFaceRegions(document, curveElemIds);
        }
    }

    /// <param name="reference">The source reference of face.</param>
    extension(Reference reference)
    {
        /// <summary>Gets the FaceRegions in the existing face.</summary>
        /// <returns>
        ///    The FaceRegions in the existing face, or an empty collection if no FaceRegions are found.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The Reference is not a Face Reference.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public IList<Reference> GetFaceRegions(Document document)
        {
            return CurveByPointsUtils.GetFaceRegions(document, reference);
        }
    }
}