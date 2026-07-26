

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.GeometryCreationUtilities"/> class.
/// </summary>
[PublicAPI]
public static class GeometryCreationUtilitiesExtensions
{
    extension(Solid)
    {
        /// <summary>
        ///    Creates a solid by blending two closed curve loops lying in non-coincident planes.
        /// </summary>
        /// <remarks>
        ///     The function chooses vertex connections that will result in a geometrically reasonable blend
        /// </remarks>
        /// <param name="firstLoop">
        ///    The first curve loop. The loop must be a closed planar loop without intersections or degeneracies. No orientation conditions are imposed. The loop may not contain just one closed curve - split such a loop into two or more curves beforehand.
        /// </param>
        /// <param name="secondLoop">
        ///    The second curve loop, satisfying the same conditions as the first loop.
        ///    The planes of the first and second loops must not be coincident, but they need not be parallel.
        /// </param>
        /// <returns>The requested solid.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The first profile CurveLoop do not satisfy the input requirements.
        ///    -or-
        ///    The second profile CurveLoop do not satisfy the input requirements.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public static Solid CreateBlendGeometry(CurveLoop firstLoop, CurveLoop secondLoop)
        {
            return GeometryCreationUtilities.CreateBlendGeometry(firstLoop, secondLoop, null);
        }

        /// <summary>
        ///    Creates a solid by blending two closed curve loops lying in non-coincident planes.
        /// </summary>
        /// <param name="firstLoop">
        ///    The first curve loop. The loop must be a closed planar loop without intersections or degeneracies. No orientation conditions are imposed. The loop may not contain just one closed curve - split such a loop into two or more curves beforehand.
        /// </param>
        /// <param name="secondLoop">
        ///    The second curve loop, satisfying the same conditions as the first loop.
        ///    The planes of the first and second loops must not be coincident, but they need not be parallel.
        /// </param>
        /// <param name="vertexPairs">
        ///    This input specifies how the two profile loops should be connected.
        /// </param>
        /// <returns>The requested solid.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The first profile CurveLoop do not satisfy the input requirements.
        ///    -or-
        ///    The second profile CurveLoop do not satisfy the input requirements.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public static Solid CreateBlendGeometry(CurveLoop firstLoop, CurveLoop secondLoop, ICollection<VertexPair> vertexPairs)
        {
            return GeometryCreationUtilities.CreateBlendGeometry(firstLoop, secondLoop, vertexPairs);
        }

        /// <summary>
        ///    Creates a solid by blending two closed curve loops lying in non-coincident planes.
        /// </summary>
        /// <param name="firstLoop">
        ///    The first curve loop. The loop must be a closed planar loop without intersections or degeneracies. No orientation conditions are imposed. The loop must be a closed planar loop without intersections or degeneracies. No orientation conditions are imposed. The loop may not contain just one closed curve - split such a loop into two or more curves beforehand.
        /// </param>
        /// <param name="secondLoop">
        ///    The second curve loop, satisfying the same conditions as the first loop.
        ///    The planes of the first and second loops must not be coincident, but they need not be parallel.
        /// </param>
        /// <param name="vertexPairs">
        ///    This input specifies how the two profile loops should be connected.
        ///    If null, the function chooses vertex connections that will result in a geometrically reasonable blend.
        /// </param>
        /// <param name="solidOptions">
        ///    The optional information to control the properties of the Solid.
        /// </param>
        /// <returns>The requested solid.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The first profile CurveLoop do not satisfy the input requirements.
        ///    -or-
        ///    The second profile CurveLoop do not satisfy the input requirements.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public static Solid CreateBlendGeometry(CurveLoop firstLoop, CurveLoop secondLoop, ICollection<VertexPair> vertexPairs, SolidOptions solidOptions)
        {
            return GeometryCreationUtilities.CreateBlendGeometry(firstLoop, secondLoop, vertexPairs, solidOptions);
        }

        /// <summary>
        ///    Creates a solid by linearly extruding one or more closed coplanar curve loops.
        /// </summary>
        /// <param name="profileLoops">
        ///    The profile loops to be extruded. The loops must be closed, coplanar, and without intersections, self-intersections, or degeneracies. No loop may contain just one closed curve - split such loops into two or more curves beforehand.
        ///    No conditions are imposed on the orientations of the loops: this function will use copies of the input loops that have been oriented as necessary to conform to Revit's orientation conventions.
        /// </param>
        /// <param name="extrusionDirection">
        ///    The direction in which to extrude the profile loops. This vector must be non-zero and transverse
        ///    (i.e., not parallel) to the plane of the profile loops. Its length is irrelevant; only its direction is used.
        /// </param>
        /// <param name="extrusionDistance">
        ///    The positive distance by which the loops are to be extruded in the direction of the input extrusionDir.
        /// </param>
        /// <returns>The requested solid.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The profile CurveLoops do not satisfy the input requirements.
        ///    -or-
        ///    The Input extrusionDir must be a non-zero vector.
        ///    The normal of the loop plane should not be perpendicular to the given extrusionDir.
        ///    -or-
        ///    The input argument extrusionDist must be positive.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public static Solid CreateExtrusionGeometry(IList<CurveLoop> profileLoops, XYZ extrusionDirection, double extrusionDistance)
        {
            return GeometryCreationUtilities.CreateExtrusionGeometry(profileLoops, extrusionDirection, extrusionDistance);
        }

        /// <summary>
        ///    Creates a solid by linearly extruding one or more closed coplanar curve loops.
        /// </summary>
        /// <param name="profileLoops">
        ///    The profile loops to be extruded. The loops must be closed, coplanar, and without intersections, self-intersections, or degeneracies. No loop may contain just one closed curve - split such loops into two or more curves beforehand.
        ///    No conditions are imposed on the orientations of the loops: this function will use copies of the input loops that have been oriented as necessary to conform to Revit's orientation conventions.
        /// </param>
        /// <param name="extrusionDirection">
        ///    The direction in which to extrude the profile loops. This vector must be non-zero and transverse
        ///    (i.e., not parallel) to the plane of the profile loops. Its length is irrelevant; only its direction is used.
        /// </param>
        /// <param name="extrusionDistance">
        ///    The positive distance by which the loops are to be extruded in the direction of the input extrusionDir.
        /// </param>
        /// <param name="solidOptions">
        ///    The optional information to control the properties of the Solid.
        /// </param>
        /// <returns>The requested solid.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The profile CurveLoops do not satisfy the input requirements.
        ///    -or-
        ///    The Input extrusionDir must be a non-zero vector.
        ///    The normal of the loop plane should not be perpendicular to the given extrusionDir.
        ///    -or-
        ///    The input argument extrusionDist must be positive.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public static Solid CreateExtrusionGeometry(IList<CurveLoop> profileLoops, XYZ extrusionDirection, double extrusionDistance, SolidOptions solidOptions)
        {
            return GeometryCreationUtilities.CreateExtrusionGeometry(profileLoops, extrusionDirection, extrusionDistance, solidOptions);
        }

        /// <summary>
        ///    Creates a solid by sweeping one or more closed coplanar curve loops along a path while keeping the
        ///    profile plane oriented so that a line in the plane that is initially perpendicular to a given fixed
        ///    direction remains perpendicular as the profile is swept along the path.
        /// </summary>
        /// <remarks>
        /// The profile loops must lie in a plane orthogonal to the sweep path at some attachment point along the path.
        /// An example where this method is useful is in constructing railings. If the fixed direction is the upward
        /// vertical, a line in the profile plane that is initially horizontal will remain horizontal as the profile
        /// is swept along the path. This property can be used to ensure that the top of the railing remains horizontal
        /// all along the railing.
        /// 
        /// The STEP ISO 10303-42 and IFC standards define a "Fixed Reference Sweep" similar to this sweep method, though
        /// there are some minor technical differences:
        /// <list type="bullet"><item>The STEP ISO reference describes a specific parameterization of the swept surface, whereas we do not guarantee any particular parameterization (partly because we simplify the surface when possible).</item><item>Neither reference mentions what should be done if the sweep pathâ€™s tangent is tangent to the reference direction at some point(s) or along the entire directrix.</item><item>Both references impose unnecessary conditions, and they're inconsistent: STEP says "the swept_curve is required to be a curve lying in the plane z = 0" while IFC says "The SweptArea shall lie in the plane z = 0" (SweptArea is the profile being swept).</item></list></remarks>
        /// <param name="sweepPath">
        ///    The sweep path, consisting of a set of contiguous curves. The path may be open or closed,
        ///    but should not otherwise have any self-intersections. The path may be planar or non-planar.
        ///    With the exception of path curves that lie in a plane parallel to %fixedReferenceDirection%,
        ///    the curve's tangent should be nowhere parallel to %fixedReferenceDirection%. If the sweep path
        ///    has corners, the solid segments that meet at a corner may not meet smoothly.
        /// </param>
        /// <param name="pathAttachmentCurveIndex">
        ///    The index of the curve in the sweep path where the profile loops are situated.
        ///    Indexing starts at 0. Together with pathAttachmentParam, this specifies the profile's attachment point.
        /// </param>
        /// <param name="pathAttachmentParameter">
        ///    Parameter of the path curve specified by pathAttachmentCrvIdx.
        ///    The profile curves must lie in the plane orthogonal to the path at this attachment point.
        /// </param>
        /// <param name="profileLoops">
        /// The curve loops defining the planar domain to be swept along the path.
        /// No conditions are imposed on the orientations of the loops; this function will use copies of the input loops
        /// that have been oriented as necessary to conform to Revit's orientation conventions.
        /// Restrictions:
        /// <list type="bullet"><item>The loops must lie in the plane orthogonal to the path at the attachment point as defined above. </item><item>The curve loop(s) must be closed and should define a single planar domain (one outer loop and, optionally, one or more inner loops). </item><item>The curve loops must be without intersections, self-intersections, or degeneracies. </item><item>No loop may contain just one closed curve - split such loops into two or more curves beforehand. </item></list></param>
        /// <param name="fixedReferenceDirection">
        ///    A unit vector specifying the fixed direction used to control how the profile plane is swept along the path; see the description and remarks above.
        /// </param>
        /// <returns>The requested solid.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The input argument sweepPath should at least contain one curve.
        ///    -or-
        ///    The input argument pathAttachmentCrvIdx is not valid.
        ///    -or-
        ///    The profile CurveLoops do not satisfy the input requirements.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    fixedReferenceDirection is not length 1.0.
        /// </exception>
        [Pure]
        public static Solid CreateFixedReferenceSweptGeometry(CurveLoop sweepPath, int pathAttachmentCurveIndex, double pathAttachmentParameter, IList<CurveLoop> profileLoops, XYZ fixedReferenceDirection)
        {
            return GeometryCreationUtilities.CreateFixedReferenceSweptGeometry(sweepPath, pathAttachmentCurveIndex, pathAttachmentParameter, profileLoops, fixedReferenceDirection);
        }

        /// <summary>
        ///    Creates a solid by sweeping one or more closed coplanar curve loops along a path while keeping the
        ///    profile plane oriented so that a line in the plane that is initially perpendicular to a given fixed
        ///    direction remains perpendicular as the profile is swept along the path.
        /// </summary>
        /// <remarks>
        /// The profile loops must lie in a plane orthogonal to the sweep path at some attachment point along the path.
        /// An example where this method is useful is in constructing railings. If the fixed direction is the upward
        /// vertical, a line in the profile plane that is initially horizontal will remain horizontal as the profile
        /// is swept along the path. This property can be used to ensure that the top of the railing remains horizontal
        /// all along the railing.
        /// 
        /// The STEP ISO 10303-42 and IFC standards define a "Fixed Reference Sweep" similar to this sweep method, though
        /// there are some minor technical differences:
        /// <list type="bullet"><item>The STEP ISO reference describes a specific parameterization of the swept surface, whereas we do not guarantee any particular parameterization (partly because we simplify the surface when possible).</item><item>Neither reference mentions what should be done if the sweep pathâ€™s tangent is tangent to the reference direction at some point(s) or along the entire directrix.</item><item>Both references impose unnecessary conditions, and they're inconsistent: STEP says "the swept_curve is required to be a curve lying in the plane z = 0" while IFC says "The SweptArea shall lie in the plane z = 0" (SweptArea is the profile being swept).</item></list></remarks>
        /// <param name="sweepPath">
        ///    The sweep path, consisting of a set of contiguous curves. The path may be open or closed,
        ///    but should not otherwise have any self-intersections. The path may be planar or non-planar.
        ///    With the exception of path curves that lie in a plane parallel to %fixedReferenceDirection%,
        ///    the curve's tangent should be nowhere parallel to %fixedReferenceDirection%. If the sweep path
        ///    has corners, the solid segments that meet at a corner may not meet smoothly.
        /// </param>
        /// <param name="pathAttachmentCurveIndex">
        ///    The index of the curve in the sweep path where the profile loops are situated.
        ///    Indexing starts at 0. Together with pathAttachmentParam, this specifies the profile's attachment point.
        /// </param>
        /// <param name="pathAttachmentParameter">
        ///    Parameter of the path curve specified by pathAttachmentCrvIdx.
        ///    The profile curves must lie in the plane orthogonal to the path at this attachment point.
        /// </param>
        /// <param name="profileLoops">
        /// The curve loops defining the planar domain to be swept along the path.
        /// No conditions are imposed on the orientations of the loops; this function will use copies of the input loops
        /// that have been oriented as necessary to conform to Revit's orientation conventions.
        /// Restrictions:
        /// <list type="bullet"><item>The loops must lie in the plane orthogonal to the path at the attachment point as defined above. </item><item>The curve loop(s) must be closed and should define a single planar domain (one outer loop and, optionally, one or more inner loops). </item><item>The curve loops must be without intersections, self-intersections, or degeneracies. </item><item>No loop may contain just one closed curve - split such loops into two or more curves beforehand. </item></list></param>
        /// <param name="fixedReferenceDirection">
        ///    A unit vector specifying the fixed direction used to control how the profile plane is swept along the path; see the description and remarks above.
        ///    The profile CurveLoops do not satisfy the input requirements.
        /// </param>
        /// <param name="solidOptions">
        ///    The optional information to control the properties of the Solid.
        /// </param>
        /// <returns>The requested solid.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The input argument sweepPath should at least contain one curve.
        ///    -or-
        ///    The input argument pathAttachmentCrvIdx is not valid.
        ///    -or-
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    fixedReferenceDirection is not length 1.0.
        /// </exception>
        [Pure]
        public static Solid CreateFixedReferenceSweptGeometry(CurveLoop sweepPath, int pathAttachmentCurveIndex, double pathAttachmentParameter, IList<CurveLoop> profileLoops, XYZ fixedReferenceDirection, SolidOptions solidOptions)
        {
            return GeometryCreationUtilities.CreateFixedReferenceSweptGeometry(sweepPath, pathAttachmentCurveIndex, pathAttachmentParameter, profileLoops, fixedReferenceDirection, solidOptions);
        }

        /// <summary>
        ///    Creates a solid or open shell geometry by lofting between a sequence of curve loops.
        /// </summary>
        /// <remarks>
        ///    If all the curve loops are closed it will create a solid. No loop may contain just one closed curve - split such loops into two or more curves beforehand.
        ///    If all the curve loops are open, then create an open shell.
        ///    If there are both open and closed loops, only the first and/or last loop are allowed to be open,
        ///    others (if they exist) must be closed. A solid will be created in this case.
        ///    The surface of the solid or open shell will pass through these profiles blending smoothly between the profiles.
        ///    Each profile loop must be free of intersections and degeneracies. No orientation conditions on the loops are imposed.
        /// </remarks>
        /// <param name="profileLoops">
        ///    The array of curve loops, where the order of the array determines the lofting sequence used.
        /// </param>
        /// <param name="solidOptions">
        ///    The optional information to control the properties of the solid or open shell.
        /// </param>
        /// <returns>The requested solid or open shell.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The number of profile CurveLoops is less than 2.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public static Solid CreateLoftGeometry(IList<CurveLoop> profileLoops, SolidOptions solidOptions)
        {
            return GeometryCreationUtilities.CreateLoftGeometry(profileLoops, solidOptions);
        }

        /// <summary>
        ///    Creates a solid of revolution by revolving a set of closed curve loops around an axis by a specified angle.
        /// </summary>
        /// <param name="coordinateFrame">
        ///    A right-handed orthonormal frame of vectors. The frame's z-vector is the axis of revolution. The start and end angle inputs refer to this frame.
        /// </param>
        /// <param name="profileLoops">
        /// The profile loops to be revolved. No conditions are imposed on the orientations of the loops.
        /// This function will use copies of the input loops that have been oriented as necessary to conform to Revit's orientation conventions.
        /// Restrictions:
        /// <list type="bullet"><item>The loops must lie in the xz coordinate plane of the input coordinate frame. </item><item>The curve loop(s) must be closed and must define a single planar domain (one outer loop and, optionally, one or more inner loops). </item><item>The curve loops must be without intersections, self-intersections, or degeneracies. </item><item>The loops must lie on the "right" side of the z axis (where x &gt;= 0). </item><item>No loop may contain just one closed curve - split such loops into two or more curves beforehand. </item></list></param>
        /// <param name="startAngle">
        ///    The start angle for the revolution, in radians,
        ///    measured counter-clockwise from the coordinate frame's x-axis as viewed looking down the frame's z-axis.
        /// </param>
        /// <param name="endAngle">
        ///    The end angle for the revolution, using the same conventions as the start angle.
        ///    The end angle may be less than (but not equal to) the start angle.
        ///    The total angle of revolution, equal to the absolute value of (endAngle â€“ startAngle), must be at most 2*PI.
        /// </param>
        /// <returns>
        ///    The requested solid. Note that if less than a full revolution is used, planar end faces will be added as part of the solid.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The input argument coordinateFrame should be a right-handed orthonormal frame of vectors.
        ///    -or-
        ///    The profile CurveLoops do not satisfy the input requirements.
        ///    -or-
        ///    The absolute value of %(endAngle â€“ startAngle)%, must be at most 2*PI.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public static Solid CreateRevolvedGeometry(Frame coordinateFrame, IList<CurveLoop> profileLoops, double startAngle, double endAngle)
        {
            return GeometryCreationUtilities.CreateRevolvedGeometry(coordinateFrame, profileLoops, startAngle, endAngle);
        }


        /// <summary>
        ///    Creates a solid of revolution by revolving a set of closed curve loops around an axis by a specified angle.
        /// </summary>
        /// <param name="coordinateFrame">
        ///    A right-handed orthonormal frame of vectors. The frame's z-vector is the axis of revolution. The start and end angle inputs refer to this frame.
        /// </param>
        /// <param name="profileLoops">
        /// The profile loops to be revolved. No conditions are imposed on the orientations of the loops.
        /// This function will use copies of the input loops that have been oriented as necessary to conform to Revit's orientation conventions.
        /// Restrictions:
        /// <list type="bullet"><item>The loops must lie in the xz coordinate plane of the input coordinate frame. </item><item>The curve loop(s) must be closed and must define a single planar domain (one outer loop and, optionally, one or more inner loops). </item><item>The curve loops must be without intersections, self-intersections, or degeneracies. </item><item>The loops must lie on the "right" side of the z axis (where x &gt;= 0). </item><item>No loop may contain just one closed curve - split such loops into two or more curves beforehand. </item></list></param>
        /// <param name="startAngle">
        ///    The start angle for the revolution, in radians,
        ///    measured counter-clockwise from the coordinate frame's x-axis as viewed looking down the frame's z-axis.
        /// </param>
        /// <param name="endAngle">
        ///    The end angle for the revolution, using the same conventions as the start angle.
        ///    The end angle may be less than (but not equal to) the start angle.
        ///    The total angle of revolution, equal to the absolute value of (endAngle â€“ startAngle), must be at most 2*PI.
        /// </param>
        /// <param name="solidOptions">
        ///    The optional information to control the properties of the Solid.
        /// </param>
        /// <returns>
        ///    The requested solid. Note that if less than a full revolution is used, planar end faces will be added as part of the solid.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The input argument coordinateFrame should be a right-handed orthonormal frame of vectors.
        ///    -or-
        ///    The profile CurveLoops do not satisfy the input requirements.
        ///    -or-
        ///    The absolute value of %(endAngle â€“ startAngle)%, must be at most 2*PI.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public static Solid CreateRevolvedGeometry(Frame coordinateFrame, IList<CurveLoop> profileLoops, double startAngle, double endAngle, SolidOptions solidOptions)
        {
            return GeometryCreationUtilities.CreateRevolvedGeometry(coordinateFrame, profileLoops, startAngle, endAngle, solidOptions);
        }

        /// <summary>
        ///    Creates a solid by simultaneously sweeping and blending two or more closed planar curve loops along a single curve.
        /// </summary>
        /// <param name="pathCurve">The sweep path, consisting of a single bounded, open curve.</param>
        /// <param name="pathParameters">
        ///    An increasing sequence of parameters along the path curve (lying within the curve's bounds).
        ///    These parameters specify the locations of the planes orthogonal to the path that contain the profile loops.
        ///    This array must have the same size as the input array "profileLoops".
        /// </param>
        /// <param name="profileLoops">
        ///    Closed, planar curve loops arrayed along the path. No loop may contain just one closed curve - split such loops into two or more curves beforehand.
        ///    The solid will have these profiles as cross-sections at the points specified by the input pathParams. The solid will blend smoothly between the profiles.
        ///    This array must have the same size as the input array "pathParams", and each profile loop must lie in the plane orthogonal to the path at the point specified by the corresponding entry in the input array "pathParams".
        ///    Each profile loop must define a single planar domain and must be free of intersections and degeneracies. No orientation conditions on the loops are imposed.
        /// </param>
        /// <param name="vertexPairs">
        ///    This input specifies how adjacent profile loops should be connected.
        ///    It must contain one less element than the "profileLoops" input, and entry vertexPairs[idx] specifies how profileLoops[idx] and profileLoops[idx+1] should be connected (indexing starts at 0).
        /// </param>
        /// <returns>The requested solid.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The input pathCurve is a helical curve and is not supported for this operation.
        ///    -or-
        ///    The input argument pathCurve should be bounded.
        ///    The input argument pathCurve should be non-degenerate.
        ///    -or-
        ///    The input argument pathParams should be an increasing array.
        ///    -or-
        ///    The profile CurveLoops do not satisfy the input requirements.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public static Solid CreateSweptBlendGeometry(Curve pathCurve, IList<double> pathParameters, IList<CurveLoop> profileLoops, IList<ICollection<VertexPair>> vertexPairs)
        {
            return GeometryCreationUtilities.CreateSweptBlendGeometry(pathCurve, pathParameters, profileLoops, vertexPairs);
        }

        /// <summary>
        ///    Creates a solid by simultaneously sweeping and blending two or more closed planar curve loops along a single curve.
        /// </summary>
        /// <param name="pathCurve">The sweep path, consisting of a single bounded, open curve.</param>
        /// <param name="pathParameters">
        ///    An increasing sequence of parameters along the path curve (lying within the curve's bounds).
        ///    These parameters specify the locations of the planes orthogonal to the path that contain the profile loops.
        ///    This array must have the same size as the input array "profileLoops".
        /// </param>
        /// <param name="profileLoops">
        ///    Closed, planar curve loops arrayed along the path. No loop may contain just one closed curve - split such loops into two or more curves beforehand.
        ///    The solid will have these profiles as cross-sections at the points specified by the input pathParams. The solid will blend smoothly between the profiles.
        ///    This array must have the same size as the input array "pathParams", and each profile loop must lie in the plane orthogonal to the path at the point specified by the corresponding entry in the input array "pathParams".
        ///    Each profile loop must define a single planar domain and must be free of intersections and degeneracies. No orientation conditions on the loops are imposed.
        /// </param>
        /// <param name="vertexPairs">
        ///    This input specifies how adjacent profile loops should be connected.
        ///    It must contain one less element than the "profileLoops" input, and entry vertexPairs[idx] specifies how profileLoops[idx] and profileLoops[idx+1] should be connected (indexing starts at 0).
        /// </param>
        /// <param name="solidOptions">
        ///    The optional information to control the properties of the Solid.
        /// </param>
        /// <returns>The requested solid.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The input pathCurve is a helical curve and is not supported for this operation.
        ///    -or-
        ///    The input argument pathCurve should be bounded.
        ///    The input argument pathCurve should be non-degenerate.
        ///    -or-
        ///    The input argument pathParams should be an increasing array.
        ///    -or-
        ///    The profile CurveLoops do not satisfy the input requirements.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public static Solid CreateSweptBlendGeometry(Curve pathCurve, IList<double> pathParameters, IList<CurveLoop> profileLoops, IList<ICollection<VertexPair>> vertexPairs, SolidOptions solidOptions)
        {
            return GeometryCreationUtilities.CreateSweptBlendGeometry(pathCurve, pathParameters, profileLoops, vertexPairs, solidOptions);
        }

        /// <summary>
        ///    Creates a solid by sweeping one or more closed coplanar curve loops along a path.
        /// </summary>
        /// <remarks>
        ///    The profile loops must lie in a plane orthogonal to the sweep path at some attachment point along the path.
        /// </remarks>
        /// <param name="sweepPath">
        ///    The sweep path, consisting of a set of contiguous curves. The path may be open or closed,
        ///    but should not otherwise have any self-intersections. The path may be planar or non-planar.
        /// </param>
        /// <param name="pathAttachmentCurveIndex">
        ///    The index of the curve in the sweep path where the profile loops are situated.
        ///    Indexing starts at 0. Together with pathAttachmentParam, this specifies the profile's attachment point.
        /// </param>
        /// <param name="pathAttachmentParameter">
        ///    Parameter of the path curve specified by pathAttachmentCrvIdx.
        ///    The profile curves must lie in the plane orthogonal to the path at this attachment point.
        /// </param>
        /// <param name="profileLoops">
        /// The curve loops defining the planar domain to be swept along the path.
        /// No conditions are imposed on the orientations of the loops:
        /// this function will use copies of the input loops that have been oriented as necessary to conform to Revit's orientation conventions.
        /// Restrictions:
        /// <list type="bullet"><item>The loops must lie in the plane orthogonal to the path at the attachment point as defined above. </item><item>The curve loop(s) must be closed and should define a single planar domain (one outer loop and, optionally, one or more inner loops) </item><item>The curve loops must be without intersections, self-intersections, or degeneracies. </item><item>No loop may contain just one closed curve - split such loops into two or more curves beforehand. </item></list></param>
        /// <returns>The requested solid.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The input argument sweepPath should at least contain one curve.
        ///    -or-
        ///    The input argument pathAttachmentCrvIdx is not valid.
        ///    The given attachment point doesn't lie in the plane of the Curve Loop.
        ///    -or-
        ///    The profile CurveLoops do not satisfy the input requirements.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Failed to create the swept solid.
        /// </exception>
        [Pure]
        public static Solid CreateSweptGeometry(CurveLoop sweepPath, int pathAttachmentCurveIndex, double pathAttachmentParameter, IList<CurveLoop> profileLoops)
        {
            return GeometryCreationUtilities.CreateSweptGeometry(sweepPath, pathAttachmentCurveIndex, pathAttachmentParameter, profileLoops);
        }

        /// <summary>
        ///    Creates a solid by sweeping one or more closed coplanar curve loops along a path.
        /// </summary>
        /// <remarks>
        ///    The profile loops must lie in a plane orthogonal to the sweep path at some attachment point along the path.
        /// </remarks>
        /// <param name="sweepPath">
        ///    The sweep path, consisting of a set of contiguous curves. The path may be open or closed,
        ///    but should not otherwise have any self-intersections. The path may be planar or non-planar.
        /// </param>
        /// <param name="pathAttachmentCurveIndex">
        ///    The index of the curve in the sweep path where the profile loops are situated.
        ///    Indexing starts at 0. Together with pathAttachmentParam, this specifies the profile's attachment point.
        /// </param>
        /// <param name="pathAttachmentParameter">
        ///    Parameter of the path curve specified by pathAttachmentCrvIdx.
        ///    The profile curves must lie in the plane orthogonal to the path at this attachment point.
        /// </param>
        /// <param name="profileLoops">
        /// The curve loops defining the planar domain to be swept along the path.
        /// No conditions are imposed on the orientations of the loops:
        /// this function will use copies of the input loops that have been oriented as necessary to conform to Revit's orientation conventions.
        /// Restrictions:
        /// <list type="bullet"><item>The loops must lie in the plane orthogonal to the path at the attachment point as defined above. </item><item>The curve loop(s) must be closed and should define a single planar domain (one outer loop and, optionally, one or more inner loops) </item><item>The curve loops must be without intersections, self-intersections, or degeneracies. </item><item>No loop may contain just one closed curve - split such loops into two or more curves beforehand. </item></list></param>
        /// <param name="solidOptions">
        ///    The optional information to control the properties of the Solid.
        /// </param>
        /// <returns>The requested solid.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The input argument sweepPath should at least contain one curve.
        ///    -or-
        ///    The input argument pathAttachmentCrvIdx is not valid.
        ///    The given attachment point doesn't lie in the plane of the Curve Loop.
        ///    -or-
        ///    The profile CurveLoops do not satisfy the input requirements.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public static Solid CreateSweptGeometry(CurveLoop sweepPath, int pathAttachmentCurveIndex, double pathAttachmentParameter, IList<CurveLoop> profileLoops, SolidOptions solidOptions)
        {
            return GeometryCreationUtilities.CreateSweptGeometry(sweepPath, pathAttachmentCurveIndex, pathAttachmentParameter, profileLoops, solidOptions);
        }
    }
}