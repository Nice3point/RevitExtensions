

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
        /// <inheritdoc cref="Autodesk.Revit.DB.GeometryCreationUtilities.CreateBlendGeometry(Autodesk.Revit.DB.CurveLoop,Autodesk.Revit.DB.CurveLoop,System.Collections.Generic.ICollection{Autodesk.Revit.DB.VertexPair})"/>
        /// <remarks>
        ///     The function chooses vertex connections that will result in a geometrically reasonable blend
        /// </remarks>
        [Pure]
        public static Solid CreateBlendGeometry(CurveLoop firstLoop, CurveLoop secondLoop)
        {
            return GeometryCreationUtilities.CreateBlendGeometry(firstLoop, secondLoop, null);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.GeometryCreationUtilities.CreateBlendGeometry(Autodesk.Revit.DB.CurveLoop,Autodesk.Revit.DB.CurveLoop,System.Collections.Generic.ICollection{Autodesk.Revit.DB.VertexPair})"/>
        [Pure]
        public static Solid CreateBlendGeometry(CurveLoop firstLoop, CurveLoop secondLoop, ICollection<VertexPair> vertexPairs)
        {
            return GeometryCreationUtilities.CreateBlendGeometry(firstLoop, secondLoop, vertexPairs);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.GeometryCreationUtilities.CreateBlendGeometry(Autodesk.Revit.DB.CurveLoop,Autodesk.Revit.DB.CurveLoop,System.Collections.Generic.ICollection{Autodesk.Revit.DB.VertexPair},Autodesk.Revit.DB.SolidOptions)"/>
        [Pure]
        public static Solid CreateBlendGeometry(CurveLoop firstLoop, CurveLoop secondLoop, ICollection<VertexPair> vertexPairs, SolidOptions solidOptions)
        {
            return GeometryCreationUtilities.CreateBlendGeometry(firstLoop, secondLoop, vertexPairs, solidOptions);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.GeometryCreationUtilities.CreateExtrusionGeometry(System.Collections.Generic.IList{Autodesk.Revit.DB.CurveLoop},Autodesk.Revit.DB.XYZ,System.Double)"/>
        [Pure]
        public static Solid CreateExtrusionGeometry(IList<CurveLoop> profileLoops, XYZ extrusionDirection, double extrusionDistance)
        {
            return GeometryCreationUtilities.CreateExtrusionGeometry(profileLoops, extrusionDirection, extrusionDistance);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.GeometryCreationUtilities.CreateExtrusionGeometry(System.Collections.Generic.IList{Autodesk.Revit.DB.CurveLoop},Autodesk.Revit.DB.XYZ,System.Double,Autodesk.Revit.DB.SolidOptions)"/>
        [Pure]
        public static Solid CreateExtrusionGeometry(IList<CurveLoop> profileLoops, XYZ extrusionDirection, double extrusionDistance, SolidOptions solidOptions)
        {
            return GeometryCreationUtilities.CreateExtrusionGeometry(profileLoops, extrusionDirection, extrusionDistance, solidOptions);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.GeometryCreationUtilities.CreateFixedReferenceSweptGeometry(Autodesk.Revit.DB.CurveLoop,System.Int32,System.Double,System.Collections.Generic.IList{Autodesk.Revit.DB.CurveLoop},Autodesk.Revit.DB.XYZ)"/>
        [Pure]
        public static Solid CreateFixedReferenceSweptGeometry(CurveLoop sweepPath, int pathAttachmentCurveIndex, double pathAttachmentParameter, IList<CurveLoop> profileLoops, XYZ fixedReferenceDirection)
        {
            return GeometryCreationUtilities.CreateFixedReferenceSweptGeometry(sweepPath, pathAttachmentCurveIndex, pathAttachmentParameter, profileLoops, fixedReferenceDirection);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.GeometryCreationUtilities.CreateFixedReferenceSweptGeometry(Autodesk.Revit.DB.CurveLoop,System.Int32,System.Double,System.Collections.Generic.IList{Autodesk.Revit.DB.CurveLoop},Autodesk.Revit.DB.XYZ,Autodesk.Revit.DB.SolidOptions)"/>
        [Pure]
        public static Solid CreateFixedReferenceSweptGeometry(CurveLoop sweepPath, int pathAttachmentCurveIndex, double pathAttachmentParameter, IList<CurveLoop> profileLoops, XYZ fixedReferenceDirection, SolidOptions solidOptions)
        {
            return GeometryCreationUtilities.CreateFixedReferenceSweptGeometry(sweepPath, pathAttachmentCurveIndex, pathAttachmentParameter, profileLoops, fixedReferenceDirection, solidOptions);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.GeometryCreationUtilities.CreateLoftGeometry(System.Collections.Generic.IList{Autodesk.Revit.DB.CurveLoop},Autodesk.Revit.DB.SolidOptions)"/>
        [Pure]
        public static Solid CreateLoftGeometry(IList<CurveLoop> profileLoops, SolidOptions solidOptions)
        {
            return GeometryCreationUtilities.CreateLoftGeometry(profileLoops, solidOptions);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.GeometryCreationUtilities.CreateRevolvedGeometry(Autodesk.Revit.DB.Frame,System.Collections.Generic.IList{Autodesk.Revit.DB.CurveLoop},System.Double,System.Double)"/>
        [Pure]
        public static Solid CreateRevolvedGeometry(Frame coordinateFrame, IList<CurveLoop> profileLoops, double startAngle, double endAngle)
        {
            return GeometryCreationUtilities.CreateRevolvedGeometry(coordinateFrame, profileLoops, startAngle, endAngle);
        }


        /// <inheritdoc cref="Autodesk.Revit.DB.GeometryCreationUtilities.CreateRevolvedGeometry(Autodesk.Revit.DB.Frame,System.Collections.Generic.IList{Autodesk.Revit.DB.CurveLoop},System.Double,System.Double,Autodesk.Revit.DB.SolidOptions)"/>
        [Pure]
        public static Solid CreateRevolvedGeometry(Frame coordinateFrame, IList<CurveLoop> profileLoops, double startAngle, double endAngle, SolidOptions solidOptions)
        {
            return GeometryCreationUtilities.CreateRevolvedGeometry(coordinateFrame, profileLoops, startAngle, endAngle, solidOptions);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.GeometryCreationUtilities.CreateSweptBlendGeometry(Autodesk.Revit.DB.Curve,System.Collections.Generic.IList{System.Double},System.Collections.Generic.IList{Autodesk.Revit.DB.CurveLoop},System.Collections.Generic.IList{System.Collections.Generic.ICollection{Autodesk.Revit.DB.VertexPair}})"/>
        [Pure]
        public static Solid CreateSweptBlendGeometry(Curve pathCurve, IList<double> pathParameters, IList<CurveLoop> profileLoops, IList<ICollection<VertexPair>> vertexPairs)
        {
            return GeometryCreationUtilities.CreateSweptBlendGeometry(pathCurve, pathParameters, profileLoops, vertexPairs);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.GeometryCreationUtilities.CreateSweptBlendGeometry(Autodesk.Revit.DB.Curve,System.Collections.Generic.IList{System.Double},System.Collections.Generic.IList{Autodesk.Revit.DB.CurveLoop},System.Collections.Generic.IList{System.Collections.Generic.ICollection{Autodesk.Revit.DB.VertexPair}},Autodesk.Revit.DB.SolidOptions)"/>
        [Pure]
        public static Solid CreateSweptBlendGeometry(Curve pathCurve, IList<double> pathParameters, IList<CurveLoop> profileLoops, IList<ICollection<VertexPair>> vertexPairs, SolidOptions solidOptions)
        {
            return GeometryCreationUtilities.CreateSweptBlendGeometry(pathCurve, pathParameters, profileLoops, vertexPairs, solidOptions);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.GeometryCreationUtilities.CreateSweptGeometry(Autodesk.Revit.DB.CurveLoop,System.Int32,System.Double,System.Collections.Generic.IList{Autodesk.Revit.DB.CurveLoop})"/>
        [Pure]
        public static Solid CreateSweptGeometry(CurveLoop sweepPath, int pathAttachmentCurveIndex, double pathAttachmentParameter, IList<CurveLoop> profileLoops)
        {
            return GeometryCreationUtilities.CreateSweptGeometry(sweepPath, pathAttachmentCurveIndex, pathAttachmentParameter, profileLoops);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.GeometryCreationUtilities.CreateSweptGeometry(Autodesk.Revit.DB.CurveLoop,System.Int32,System.Double,System.Collections.Generic.IList{Autodesk.Revit.DB.CurveLoop},Autodesk.Revit.DB.SolidOptions)"/>
        [Pure]
        public static Solid CreateSweptGeometry(CurveLoop sweepPath, int pathAttachmentCurveIndex, double pathAttachmentParameter, IList<CurveLoop> profileLoops, SolidOptions solidOptions)
        {
            return GeometryCreationUtilities.CreateSweptGeometry(sweepPath, pathAttachmentCurveIndex, pathAttachmentParameter, profileLoops, solidOptions);
        }
    }
}