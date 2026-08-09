#if REVIT2025_OR_GREATER
using Autodesk.Revit.DB.Structure;
using JetBrains.Annotations;
using Nice3point.Revit.Extensions.Internal;
using Document = Autodesk.Revit.Creation.Document;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.Structure;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Structure.RebarSpliceTypeUtils"/> class.
/// </summary>
[PublicAPI]
public static class RebarSpliceTypeUtilsExtensions
{
    /// <param name="document">The source document.</param>
    extension(Document document)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceTypeUtils.CreateRebarSpliceType(Autodesk.Revit.DB.Document,System.String)"/>
        public ElementType NewRebarSpliceType(string typeName)
        {
            var dbDocument = UnsafeAccessors.GetDocument(document);
            return RebarSpliceTypeUtils.CreateRebarSpliceType(dbDocument, typeName);
        }
    }

    /// <param name="rebarSpliceType">The source rebar splice type element.</param>
    extension(ElementType rebarSpliceType)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceTypeUtils.GetLapLengthMultiplier(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public double GetRebarSpliceLapLengthMultiplier()
        {
            return RebarSpliceTypeUtils.GetLapLengthMultiplier(rebarSpliceType.Document, rebarSpliceType.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceTypeUtils.GetShiftOption(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public RebarSpliceShiftOption GetRebarSpliceShiftOption()
        {
            return RebarSpliceTypeUtils.GetShiftOption(rebarSpliceType.Document, rebarSpliceType.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceTypeUtils.GetStaggerLengthMultiplier(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public double GetRebarSpliceStaggerLengthMultiplier()
        {
            return RebarSpliceTypeUtils.GetStaggerLengthMultiplier(rebarSpliceType.Document, rebarSpliceType.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceTypeUtils.SetLapLengthMultiplier(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,System.Double)"/>
        public void SetRebarSpliceLapLengthMultiplier(double lapLengthMultiplier)
        {
            RebarSpliceTypeUtils.SetLapLengthMultiplier(rebarSpliceType.Document, rebarSpliceType.Id, lapLengthMultiplier);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceTypeUtils.SetShiftOption(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.Structure.RebarSpliceShiftOption)"/>
        public void SetRebarSpliceShiftOption(RebarSpliceShiftOption shiftOption)
        {
            RebarSpliceTypeUtils.SetShiftOption(rebarSpliceType.Document, rebarSpliceType.Id, shiftOption);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceTypeUtils.SetStaggerLengthMultiplier(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,System.Double)"/>
        public void SetRebarSpliceStaggerLengthMultiplier(double staggerLengthMultiplier)
        {
            RebarSpliceTypeUtils.SetStaggerLengthMultiplier(rebarSpliceType.Document, rebarSpliceType.Id, staggerLengthMultiplier);
        }
    }

    /// <param name="rebarSpliceTypeId">The rebar splice type element id.</param>
    extension(ElementId rebarSpliceTypeId)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceTypeUtils.GetLapLengthMultiplier(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public double GetRebarSpliceLapLengthMultiplier(Autodesk.Revit.DB.Document document)
        {
            return RebarSpliceTypeUtils.GetLapLengthMultiplier(document, rebarSpliceTypeId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceTypeUtils.GetShiftOption(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public RebarSpliceShiftOption GetRebarSpliceShiftOption(Autodesk.Revit.DB.Document document)
        {
            return RebarSpliceTypeUtils.GetShiftOption(document, rebarSpliceTypeId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceTypeUtils.GetStaggerLengthMultiplier(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public double GetRebarSpliceStaggerLengthMultiplier(Autodesk.Revit.DB.Document document)
        {
            return RebarSpliceTypeUtils.GetStaggerLengthMultiplier(document, rebarSpliceTypeId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceTypeUtils.SetLapLengthMultiplier(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,System.Double)"/>
        public void SetRebarSpliceLapLengthMultiplier(Autodesk.Revit.DB.Document document, double lapLengthMultiplier)
        {
            RebarSpliceTypeUtils.SetLapLengthMultiplier(document, rebarSpliceTypeId, lapLengthMultiplier);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceTypeUtils.SetShiftOption(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.Structure.RebarSpliceShiftOption)"/>
        public void SetRebarSpliceShiftOption(Autodesk.Revit.DB.Document document, RebarSpliceShiftOption shiftOption)
        {
            RebarSpliceTypeUtils.SetShiftOption(document, rebarSpliceTypeId, shiftOption);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarSpliceTypeUtils.SetStaggerLengthMultiplier(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,System.Double)"/>
        public void SetRebarSpliceStaggerLengthMultiplier(Autodesk.Revit.DB.Document document, double staggerLengthMultiplier)
        {
            RebarSpliceTypeUtils.SetStaggerLengthMultiplier(document, rebarSpliceTypeId, staggerLengthMultiplier);
        }
    }
}
#endif