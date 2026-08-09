#if REVIT2022_OR_GREATER
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.BoundaryValidation"/> class.
/// </summary>
[PublicAPI]
public static class BoundaryValidationExtensions
{
    extension(CurveLoop)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.BoundaryValidation.IsValidHorizontalBoundary(System.Collections.Generic.IList{Autodesk.Revit.DB.CurveLoop})"/>
        [Pure]
        public static bool IsValidHorizontalBoundary(IList<CurveLoop> curveLoops)
        {
            return BoundaryValidation.IsValidHorizontalBoundary(curveLoops);
        }
#if REVIT2023_OR_GREATER

        /// <inheritdoc cref="Autodesk.Revit.DB.BoundaryValidation.IsValidBoundaryOnSketchPlane(Autodesk.Revit.DB.SketchPlane,System.Collections.Generic.IList{Autodesk.Revit.DB.CurveLoop})"/>
        [Pure]
        public static bool IsValidBoundaryOnSketchPlane(SketchPlane sketchPlane, IList<CurveLoop> curveLoops)
        {
            return BoundaryValidation.IsValidBoundaryOnSketchPlane(sketchPlane, curveLoops);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.BoundaryValidation.IsValidBoundaryOnView(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,System.Collections.Generic.IList{Autodesk.Revit.DB.CurveLoop})"/>
        [Pure]
        public static bool IsValidBoundaryOnView(Document document, ElementId viewId, IList<CurveLoop> curveLoops)
        {
            return BoundaryValidation.IsValidBoundaryOnView(document, viewId, curveLoops);
        }
#endif
    }
}
#endif