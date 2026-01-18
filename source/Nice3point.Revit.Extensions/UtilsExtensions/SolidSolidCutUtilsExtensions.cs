// ReSharper disable once CheckNamespace

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.SolidSolidCutUtils"/> class.
/// </summary>
[PublicAPI]
public static class SolidSolidCutUtilsExtensions
{
    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <summary>Gets all the solids which cut the input element.</summary>
        /// <returns>The ids of the solids which cut the input element.</returns>
        [Pure]
        public ICollection<ElementId> GetCuttingSolids()
        {
            return SolidSolidCutUtils.GetCuttingSolids(element);
        }

        /// <summary>Get all the solids which are cut by the input element.</summary>
        /// <returns>The ids of the solids which are cut by the input element.</returns>
        [Pure]
        public ICollection<ElementId> GetSolidsBeingCut()
        {
            return SolidSolidCutUtils.GetSolidsBeingCut(element);
        }

        /// <summary>Validates that the element is eligible for a solid-solid cut.</summary>
        /// <remarks>
        ///    The element must be solid and must be a GenericForm, GeomCombination, or a FamilyInstance.
        /// </remarks>
        /// <returns>
        ///    True if the input element can participate in a solid-solid cut.  False otherwise.
        /// </returns>
        public bool IsAllowedForSolidCut => SolidSolidCutUtils.IsAllowedForSolidCut(element);

        /// <summary>Validates that the element is from an appropriate document.</summary>
        /// <remarks>
        ///    Currently an element from either a project document, conceptual model, pattern based curtain panel, or adaptive component family
        ///    may participate in solid-solid cuts.
        /// </remarks>
        /// <returns>
        ///    True if the element is from an appropriate document for solid-solid cuts, false otherwise.
        /// </returns>
        public bool IsElementFromAppropriateContext => SolidSolidCutUtils.IsElementFromAppropriateContext(element);

        /// <summary>Verifies if the cutting element can add a solid cut to the target element.</summary>
        /// <param name="cutElement">The element to be cut.</param>
        /// <param name="reason">
        ///    The reason that the cutting element cannot add a solid cut to the cut element.
        /// </param>
        /// <returns>
        ///    True if the cutting element can add a solid cut to the target element, false otherwise.
        /// </returns>
        [Pure]
        public bool CanElementCutElement(Element cutElement, out CutFailureReason reason)
        {
            return SolidSolidCutUtils.CanElementCutElement(element, cutElement, out reason);
        }

        /// <summary>Checks that if there is a solid-solid cut between two elements.</summary>
        /// <param name="second">The solid being cut or the cutting solid.</param>
        /// <param name="firstCutsSecond">
        ///    If the return value of this function is true, this indicates which element is the cutting element from the pair.
        ///    True if the first solid cuts the second one, false if the second solid cuts the first one.
        /// </param>
        /// <returns>
        ///    True if there is a solid-solid cut between the input elements, false otherwise.
        /// </returns>
        [Pure]
        public bool CutExistsBetweenElements(Element second, out bool firstCutsSecond)
        {
            return SolidSolidCutUtils.CutExistsBetweenElements(element, second, out firstCutsSecond);
        }

        /// <summary>Adds a solid-solid cut for the two elements.</summary>
        /// <remarks>This utility will split faces of cutting solid by default.</remarks>
        /// <param name="cuttingSolid">The cutting solid.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The element must be in a project document or in a conceptual model, pattern based curtain panel, or adaptive component family.
        ///    -or-
        ///    The element does not meet the condition that it must be solid and must be a GenericForm, GeomCombination, or a FamilyInstance.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Failed to add solid-solid cut for the two elements.
        /// </exception>
        public Element AddCutBetweenSolids(Element cuttingSolid)
        {
            SolidSolidCutUtils.AddCutBetweenSolids(element.Document, element, cuttingSolid);
            return element;
        }

        /// <summary>
        ///    Adds a solid-solid cut for the two elements with the option to control splitting of faces of the cutting solid.
        /// </summary>
        /// <param name="cuttingSolid">The cutting solid.</param>
        /// <param name="splitFacesOfCuttingSolid">
        ///    True to split faces of cutting solid where it intersects the solid to be cut, false otherwise.
        /// </param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The element must be in a project document or in a conceptual model, pattern based curtain panel, or adaptive component family.
        ///    -or-
        ///    The element does not meet the condition that it must be solid and must be a GenericForm, GeomCombination, or a FamilyInstance.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Failed to add solid-solid cut for the two elements.
        /// </exception>
        public Element AddCutBetweenSolids(Element cuttingSolid, bool splitFacesOfCuttingSolid)
        {
            SolidSolidCutUtils.AddCutBetweenSolids(element.Document, element, cuttingSolid, splitFacesOfCuttingSolid);
            return element;
        }

        /// <summary>Removes the solid-solid cut between the two elements if it exists.</summary>
        /// <param name="second">The solid being cut or the cutting solid.</param>
        public Element RemoveCutBetweenSolids(Element second)
        {
            SolidSolidCutUtils.RemoveCutBetweenSolids(element.Document, element, second);
            return element;
        }

        /// <summary>
        ///    Causes the faces of the cutting element where it intersects the element it is cutting to be split or unsplit.
        /// </summary>
        /// <remarks>There must be a cut between the input elements.</remarks>
        /// <param name="second">The solid being cut or the cutting solid</param>
        /// <param name="split">True to split the faces of intersection, false to unsplit them.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    There is no solid-solid cut between the input elements.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Unable to split or unsplit faces of cutting solid
        /// </exception>
        public Element SplitFacesOfCuttingSolid(Element second, bool split)
        {
            SolidSolidCutUtils.SplitFacesOfCuttingSolid(element, second, split);
            return element;
        }
    }
}