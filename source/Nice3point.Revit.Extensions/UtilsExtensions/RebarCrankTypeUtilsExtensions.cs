#if REVIT2026_OR_GREATER
using Autodesk.Revit.DB.Structure;
using JetBrains.Annotations;
using Nice3point.Revit.Extensions.Internal;
using Document = Autodesk.Revit.Creation.Document;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.Structure;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Structure.RebarCrankTypeUtils"/> class.
/// </summary>
[PublicAPI]
public static class RebarCrankTypeUtilsExtensions
{
    /// <param name="document">The source document.</param>
    extension(Document document)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarCrankTypeUtils.CreateRebarCrankType(Autodesk.Revit.DB.Document,System.String)"/>
        public ElementType NewRebarCrankType(string typeName)
        {
            var dbDocument = UnsafeAccessors.GetDocument(document);
            return RebarCrankTypeUtils.CreateRebarCrankType(dbDocument, typeName);
        }
    }

    /// <param name="rebarCrankType">The source rebar crank type element.</param>
    extension(ElementType rebarCrankType)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarCrankTypeUtils.GetCrankLengthMultiplier(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public double GetRebarCrankLengthMultiplier()
        {
            return RebarCrankTypeUtils.GetCrankLengthMultiplier(rebarCrankType.Document, rebarCrankType.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarCrankTypeUtils.GetCrankOffsetMultiplier(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public double GetRebarCrankOffsetMultiplier()
        {
            return RebarCrankTypeUtils.GetCrankOffsetMultiplier(rebarCrankType.Document, rebarCrankType.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarCrankTypeUtils.GetCrankRatio(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public double GetRebarCrankRatio()
        {
            return RebarCrankTypeUtils.GetCrankRatio(rebarCrankType.Document, rebarCrankType.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarCrankTypeUtils.SetCrankLengthMultiplier(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,System.Double)"/>
        public void SetRebarCrankLengthMultiplier(double crankLengthMultiplier)
        {
            RebarCrankTypeUtils.SetCrankLengthMultiplier(rebarCrankType.Document, rebarCrankType.Id, crankLengthMultiplier);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarCrankTypeUtils.SetCrankOffsetMultiplier(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,System.Double)"/>
        public void SetRebarCrankOffsetMultiplier(double crankOffsetMultiplier)
        {
            RebarCrankTypeUtils.SetCrankOffsetMultiplier(rebarCrankType.Document, rebarCrankType.Id, crankOffsetMultiplier);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarCrankTypeUtils.SetCrankRatio(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,System.Double)"/>
        public void SetRebarCrankRatio(double crankRatio)
        {
            RebarCrankTypeUtils.SetCrankRatio(rebarCrankType.Document, rebarCrankType.Id, crankRatio);
        }
    }

    /// <param name="rebarCrankTypeId">The rebar crank type element id.</param>
    extension(ElementId rebarCrankTypeId)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarCrankTypeUtils.GetCrankLengthMultiplier(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public double GetRebarCrankLengthMultiplier(Autodesk.Revit.DB.Document document)
        {
            return RebarCrankTypeUtils.GetCrankLengthMultiplier(document, rebarCrankTypeId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarCrankTypeUtils.GetCrankOffsetMultiplier(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public double GetRebarCrankOffsetMultiplier(Autodesk.Revit.DB.Document document)
        {
            return RebarCrankTypeUtils.GetCrankOffsetMultiplier(document, rebarCrankTypeId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarCrankTypeUtils.GetCrankRatio(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public double GetRebarCrankRatio(Autodesk.Revit.DB.Document document)
        {
            return RebarCrankTypeUtils.GetCrankRatio(document, rebarCrankTypeId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarCrankTypeUtils.SetCrankLengthMultiplier(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,System.Double)"/>
        public void SetRebarCrankLengthMultiplier(Autodesk.Revit.DB.Document document, double crankLengthMultiplier)
        {
            RebarCrankTypeUtils.SetCrankLengthMultiplier(document, rebarCrankTypeId, crankLengthMultiplier);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarCrankTypeUtils.SetCrankOffsetMultiplier(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,System.Double)"/>
        public void SetRebarCrankOffsetMultiplier(Autodesk.Revit.DB.Document document, double crankOffsetMultiplier)
        {
            RebarCrankTypeUtils.SetCrankOffsetMultiplier(document, rebarCrankTypeId, crankOffsetMultiplier);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarCrankTypeUtils.SetCrankRatio(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,System.Double)"/>
        public void SetRebarCrankRatio(Autodesk.Revit.DB.Document document, double crankRatio)
        {
            RebarCrankTypeUtils.SetCrankRatio(document, rebarCrankTypeId, crankRatio);
        }
    }
}
#endif