// ReSharper disable once CheckNamespace

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.SolidUtils" /> class.
/// </summary>
[PublicAPI]
public static class SolidUtilsExtensions
{
    /// <param name="solid">The source solid.</param>
    extension(Solid solid)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.SolidUtils.IsValidForTessellation(Autodesk.Revit.DB.Solid)" />
        public bool IsValidForTessellation => SolidUtils.IsValidForTessellation(solid);

        /// <inheritdoc cref="Autodesk.Revit.DB.SolidUtils.Clone(Autodesk.Revit.DB.Solid)" />
        [Pure]
        public Solid Clone()
        {
            return SolidUtils.Clone(solid);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.SolidUtils.CreateTransformed(Autodesk.Revit.DB.Solid,Autodesk.Revit.DB.Transform)" />
        [Pure]
        public Solid CreateTransformed(Transform transform)
        {
            return SolidUtils.CreateTransformed(solid, transform);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.SolidUtils.SplitVolumes(Autodesk.Revit.DB.Solid)" />
        [Pure]
        public IList<Solid> SplitVolumes()
        {
            return SolidUtils.SplitVolumes(solid);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.SolidUtils.TessellateSolidOrShell(Autodesk.Revit.DB.Solid,Autodesk.Revit.DB.SolidOrShellTessellationControls)" />
        [Pure]
        public TriangulatedSolidOrShell TessellateSolidOrShell(SolidOrShellTessellationControls tessellationControls)
        {
            return SolidUtils.TessellateSolidOrShell(solid, tessellationControls);
        }
#if REVIT2026_OR_GREATER
        /// <inheritdoc cref="Autodesk.Revit.DB.SolidUtils.ComputeIsGeometricallyClosed(Autodesk.Revit.DB.Solid)"/>
        [Pure]
        public bool ComputeIsGeometricallyClosed()
        {
            return SolidUtils.ComputeIsGeometricallyClosed(solid);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.SolidUtils.ComputeIsTopologicallyClosed(Autodesk.Revit.DB.Solid)"/>
        [Pure]
        public bool ComputeIsTopologicallyClosed()
        {
            return SolidUtils.ComputeIsTopologicallyClosed(solid);
        }
#endif
    }

#if REVIT2021_OR_GREATER
    /// <param name="endPoint">The source EdgeEndPoint.</param>
    extension(EdgeEndPoint endPoint)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.SolidUtils.FindAllEdgeEndPointsAtVertex(Autodesk.Revit.DB.EdgeEndPoint)"/>
        [Pure]
        public IList<EdgeEndPoint> FindAllEdgeEndPointsAtVertex()
        {
            return SolidUtils.FindAllEdgeEndPointsAtVertex(endPoint);
        }
    }
#endif
}
