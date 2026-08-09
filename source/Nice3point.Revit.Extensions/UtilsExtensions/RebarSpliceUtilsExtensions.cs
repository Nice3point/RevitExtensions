#if REVIT2025_OR_GREATER
using Autodesk.Revit.DB.Structure;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.Structure;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Structure.RebarSpliceUtils"/> class.
/// </summary>
[PublicAPI]
public static class RebarSpliceUtilsExtensions
{
    /// <param name="rebar">The source rebar.</param>
    extension(Rebar rebar)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceUtils.CanRebarBeSpliced(Autodesk.Revit.DB.Structure.Rebar,Autodesk.Revit.DB.Structure.RebarSpliceOptions,Autodesk.Revit.DB.Line,Autodesk.Revit.DB.XYZ)"/>
        [Pure]
        public RebarSpliceError CanBeSpliced(RebarSpliceOptions spliceOptions, Line line, XYZ linePlaneNormal)
        {
            return RebarSpliceUtils.CanRebarBeSpliced(rebar, spliceOptions, line, linePlaneNormal);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceUtils.CanRebarBeSpliced(Autodesk.Revit.DB.Structure.Rebar,Autodesk.Revit.DB.Structure.RebarSpliceOptions,Autodesk.Revit.DB.Line,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public RebarSpliceError CanBeSpliced(RebarSpliceOptions spliceOptions, Line line, ElementId viewId)
        {
            return RebarSpliceUtils.CanRebarBeSpliced(rebar, spliceOptions, line, viewId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceUtils.CanRebarBeSpliced(Autodesk.Revit.DB.Structure.Rebar,Autodesk.Revit.DB.Structure.RebarSpliceOptions,Autodesk.Revit.DB.Structure.RebarSpliceGeometry)"/>
        [Pure]
        public RebarSpliceError CanBeSpliced(RebarSpliceOptions spliceOptions, RebarSpliceGeometry spliceGeometry)
        {
            return RebarSpliceUtils.CanRebarBeSpliced(rebar, spliceOptions, spliceGeometry);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceUtils.GetLapDirectionForSpliceGeometryAndPosition(Autodesk.Revit.DB.Structure.Rebar,Autodesk.Revit.DB.Structure.RebarSpliceGeometry,Autodesk.Revit.DB.Structure.RebarSplicePosition)"/>
        [Pure]
        public XYZ GetLapDirectionForSpliceGeometryAndPosition(RebarSpliceGeometry spliceGeometry, RebarSplicePosition splicePosition)
        {
            return RebarSpliceUtils.GetLapDirectionForSpliceGeometryAndPosition(rebar, spliceGeometry, splicePosition);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceUtils.GetSpliceChain(Autodesk.Revit.DB.Structure.Rebar)"/>
        [Pure]
        public IList<ElementId> GetSpliceChain()
        {
            return RebarSpliceUtils.GetSpliceChain(rebar);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceUtils.GetSpliceGeometries(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.Structure.RebarSpliceOptions,Autodesk.Revit.DB.Structure.RebarSpliceRules)"/>
        [Pure]
        public RebarSpliceByRulesResult GetSpliceGeometries(RebarSpliceOptions spliceOptions, RebarSpliceRules spliceRules)
        {
            return RebarSpliceUtils.GetSpliceGeometries(rebar.Document, rebar.Id, spliceOptions, spliceRules);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceUtils.SpliceRebar(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.Structure.RebarSpliceOptions,Autodesk.Revit.DB.Line,Autodesk.Revit.DB.XYZ)"/>
        public IList<ElementId> Splice(RebarSpliceOptions spliceOptions, Line line, XYZ linePlaneNormal)
        {
            return RebarSpliceUtils.SpliceRebar(rebar.Document, rebar.Id, spliceOptions, line, linePlaneNormal);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceUtils.SpliceRebar(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.Structure.RebarSpliceOptions,Autodesk.Revit.DB.Line,Autodesk.Revit.DB.ElementId)"/>
        public IList<ElementId> Splice(RebarSpliceOptions spliceOptions, Line line, ElementId viewId)
        {
            return RebarSpliceUtils.SpliceRebar(rebar.Document, rebar.Id, spliceOptions, line, viewId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceUtils.SpliceRebar(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.Structure.RebarSpliceOptions,System.Collections.Generic.IList{Autodesk.Revit.DB.Structure.RebarSpliceGeometry})"/>
        public IList<ElementId> Splice(RebarSpliceOptions spliceOptions, IList<RebarSpliceGeometry> spliceGeometries)
        {
            return RebarSpliceUtils.SpliceRebar(rebar.Document, rebar.Id, spliceOptions, spliceGeometries);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceUtils.UnifyRebarsIntoOne(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId)"/>
        public ElementId UnifyRebarsIntoOne(ElementId secondRebarId)
        {
            return RebarSpliceUtils.UnifyRebarsIntoOne(rebar.Document, rebar.Id, secondRebarId);
        }
    }

    /// <param name="elementId">The rebar element id.</param>
    extension(ElementId elementId)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceUtils.GetSpliceGeometries(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.Structure.RebarSpliceOptions,Autodesk.Revit.DB.Structure.RebarSpliceRules)"/>
        [Pure]
        public RebarSpliceByRulesResult GetRebarSpliceGeometries(Document document, RebarSpliceOptions spliceOptions, RebarSpliceRules spliceRules)
        {
            return RebarSpliceUtils.GetSpliceGeometries(document, elementId, spliceOptions, spliceRules);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceUtils.SpliceRebar(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.Structure.RebarSpliceOptions,Autodesk.Revit.DB.Line,Autodesk.Revit.DB.XYZ)"/>
        public IList<ElementId> SpliceRebar(Document document, RebarSpliceOptions spliceOptions, Line line, XYZ linePlaneNormal)
        {
            return RebarSpliceUtils.SpliceRebar(document, elementId, spliceOptions, line, linePlaneNormal);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceUtils.SpliceRebar(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.Structure.RebarSpliceOptions,Autodesk.Revit.DB.Line,Autodesk.Revit.DB.ElementId)"/>
        public IList<ElementId> SpliceRebar(Document document, RebarSpliceOptions spliceOptions, Line line, ElementId viewId)
        {
            return RebarSpliceUtils.SpliceRebar(document, elementId, spliceOptions, line, viewId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceUtils.SpliceRebar(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.Structure.RebarSpliceOptions,System.Collections.Generic.IList{Autodesk.Revit.DB.Structure.RebarSpliceGeometry})"/>
        public IList<ElementId> SpliceRebar(Document document, RebarSpliceOptions spliceOptions, IList<RebarSpliceGeometry> spliceGeometries)
        {
            return RebarSpliceUtils.SpliceRebar(document, elementId, spliceOptions, spliceGeometries);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceUtils.UnifyRebarsIntoOne(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId)"/>
        public ElementId UnifyRebarsIntoOne(Document document, ElementId secondRebarId)
        {
            return RebarSpliceUtils.UnifyRebarsIntoOne(document, elementId, secondRebarId);
        }
    }
}
#endif