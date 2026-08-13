// ReSharper disable once CheckNamespace

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.SolidSolidCutUtils" /> class.
/// </summary>
[PublicAPI]
public static class SolidSolidCutUtilsExtensions
{
    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.SolidSolidCutUtils.IsAllowedForSolidCut(Autodesk.Revit.DB.Element)" />
        public bool IsAllowedForSolidCut => SolidSolidCutUtils.IsAllowedForSolidCut(element);

        /// <inheritdoc cref="Autodesk.Revit.DB.SolidSolidCutUtils.IsElementFromAppropriateContext(Autodesk.Revit.DB.Element)" />
        public bool IsElementFromAppropriateContext => SolidSolidCutUtils.IsElementFromAppropriateContext(element);

        /// <inheritdoc cref="Autodesk.Revit.DB.SolidSolidCutUtils.GetCuttingSolids(Autodesk.Revit.DB.Element)" />
        [Pure]
        public ICollection<ElementId> GetCuttingSolids()
        {
            return SolidSolidCutUtils.GetCuttingSolids(element);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.SolidSolidCutUtils.GetSolidsBeingCut(Autodesk.Revit.DB.Element)" />
        [Pure]
        public ICollection<ElementId> GetSolidsBeingCut()
        {
            return SolidSolidCutUtils.GetSolidsBeingCut(element);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.SolidSolidCutUtils.CanElementCutElement(Autodesk.Revit.DB.Element,Autodesk.Revit.DB.Element,out Autodesk.Revit.DB.CutFailureReason)" />
        [Pure]
        public bool CanElementCutElement(Element cutElement, out CutFailureReason reason)
        {
            return SolidSolidCutUtils.CanElementCutElement(element, cutElement, out reason);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.SolidSolidCutUtils.CutExistsBetweenElements(Autodesk.Revit.DB.Element,Autodesk.Revit.DB.Element,out System.Boolean)" />
        [Pure]
        public bool CutExistsBetweenElements(Element second, out bool firstCutsSecond)
        {
            return SolidSolidCutUtils.CutExistsBetweenElements(element, second, out firstCutsSecond);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.SolidSolidCutUtils.AddCutBetweenSolids(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Element,Autodesk.Revit.DB.Element)" />
        public Element AddCutBetweenSolids(Element cuttingSolid)
        {
            SolidSolidCutUtils.AddCutBetweenSolids(element.Document, element, cuttingSolid);
            return element;
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.SolidSolidCutUtils.AddCutBetweenSolids(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Element,Autodesk.Revit.DB.Element,System.Boolean)" />
        public Element AddCutBetweenSolids(Element cuttingSolid, bool splitFacesOfCuttingSolid)
        {
            SolidSolidCutUtils.AddCutBetweenSolids(element.Document, element, cuttingSolid, splitFacesOfCuttingSolid);
            return element;
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.SolidSolidCutUtils.RemoveCutBetweenSolids(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Element,Autodesk.Revit.DB.Element)" />
        public Element RemoveCutBetweenSolids(Element second)
        {
            SolidSolidCutUtils.RemoveCutBetweenSolids(element.Document, element, second);
            return element;
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.SolidSolidCutUtils.SplitFacesOfCuttingSolid(Autodesk.Revit.DB.Element,Autodesk.Revit.DB.Element,System.Boolean)" />
        public Element SplitFacesOfCuttingSolid(Element second, bool split)
        {
            SolidSolidCutUtils.SplitFacesOfCuttingSolid(element, second, split);
            return element;
        }
    }
}
