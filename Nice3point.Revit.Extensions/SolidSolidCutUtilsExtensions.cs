namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.SolidSolidCutUtils"/> class.
/// </summary>
public static class SolidSolidCutUtilsExtensions
{
    /// <summary>Gets all the solids which cut the input element.</summary>
    /// <param name="element">The input element.</param>
    /// <returns>The ids of the solids which cut the input element.</returns>
    [Pure]
    public static ICollection<ElementId> GetCuttingSolids(this Element element)
    {
        return SolidSolidCutUtils.GetCuttingSolids(element);
    }

    /// <summary>Get all the solids which are cut by the input element.</summary>
    /// <param name="element">The input element.</param>
    /// <returns>The ids of the solids which are cut by the input element.</returns>
    [Pure]
    public static ICollection<ElementId> GetSolidsBeingCut(this Element element)
    {
        return SolidSolidCutUtils.GetSolidsBeingCut(element);
    }

    /// <summary>Validates that the element is eligible for a solid-solid cut.</summary>
    /// <remarks>
    ///    The element must be solid and must be a GenericForm, GeomCombination, or a FamilyInstance.
    /// </remarks>
    /// <param name="element">The solid to be cut or the cutting solid.</param>
    /// <returns>
    ///    True if the input element can participate in a solid-solid cut.  False otherwise.
    /// </returns>
    [Pure]
    public static bool IsAllowedForSolidCut(this Element element)
    {
        return SolidSolidCutUtils.IsAllowedForSolidCut(element);
    }

    /// <summary>Validates that the element is from an appropriate document.</summary>
    /// <remarks>
    ///    Currently an element from either a project document, conceptual model, pattern based curtain panel, or adaptive component family
    ///    may participate in solid-solid cuts.
    /// </remarks>
    /// <param name="element">The solid to be cut or the cutting solid.</param>
    /// <returns>
    ///    True if the element is from an appropriate document for solid-solid cuts, false otherwise.
    /// </returns>
    [Pure]
    public static bool IsElementFromAppropriateContext(this Element element)
    {
        return SolidSolidCutUtils.IsElementFromAppropriateContext(element);
    }

    /// <summary>Verifies if the cutting element can add a solid cut to the target element.</summary>
    /// <param name="cuttingElement">The cutting element.</param>
    /// <param name="cutElement">The element to be cut.</param>
    /// <param name="reason">
    ///    The reason that the cutting element cannot add a solid cut to the cut element.
    /// </param>
    /// <returns>
    ///    True if the cutting element can add a solid cut to the target element, false otherwise.
    /// </returns>
    [Pure]
    public static bool CanElementCutElement(this Element cuttingElement, Element cutElement, out CutFailureReason reason)
    {
        return SolidSolidCutUtils.CanElementCutElement(cuttingElement, cutElement, out reason);
    }

    /// <summary>Checks that if there is a solid-solid cut between two elements.</summary>
    /// <param name="first">The solid being cut or the cutting solid.</param>
    /// <param name="second">The solid being cut or the cutting solid.</param>
    /// <param name="firstCutsSecond">
    ///    If the return value of this function is true, this indicates which element is the cutting element from the pair.
    ///    True if the first solid cuts the second one, false if the second solid cuts the first one.
    /// </param>
    /// <returns>
    ///    True if there is a solid-solid cut between the input elements, false otherwise.
    /// </returns>
    [Pure]
    public static bool CutExistsBetweenElements(this Element first, Element second, out bool firstCutsSecond)
    {
        return SolidSolidCutUtils.CutExistsBetweenElements(first, second, out firstCutsSecond);
    }

    /// <summary>Adds a solid-solid cut for the two elements.</summary>
    /// <remarks>This utility will split faces of cutting solid by default.</remarks>
    /// <param name="solidToBeCut">The solid to be cut.</param>
    /// <param name="cuttingSolid">The cutting solid.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The element must be in a project document or in a conceptual model, pattern based curtain panel, or adaptive component family.
    ///    -or-
    ///    The element does not meet the condition that it must be solid and must be a GenericForm, GeomCombination, or a FamilyInstance.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    Failed to add solid-solid cut for the two elements.
    /// </exception>
    public static void AddCutBetweenSolids(this Element solidToBeCut, Element cuttingSolid)
    {
        SolidSolidCutUtils.AddCutBetweenSolids(solidToBeCut.Document, solidToBeCut, cuttingSolid);
    }

    /// <summary>
    ///    Adds a solid-solid cut for the two elements with the option to control splitting of faces of the cutting solid.
    /// </summary>
    /// <param name="solidToBeCut">The solid to be cut.</param>
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
    public static void AddCutBetweenSolids(this Element solidToBeCut, Element cuttingSolid, bool splitFacesOfCuttingSolid)
    {
        SolidSolidCutUtils.AddCutBetweenSolids(solidToBeCut.Document, solidToBeCut, cuttingSolid, splitFacesOfCuttingSolid);
    }

    /// <summary>Removes the solid-solid cut between the two elements if it exists.</summary>
    /// <param name="first">The solid being cut or the cutting solid.</param>
    /// <param name="second">The solid being cut or the cutting solid.</param>
    public static void RemoveCutBetweenSolids(this Element first, Element second)
    {
        SolidSolidCutUtils.RemoveCutBetweenSolids(first.Document, first, second);
    }

    /// <summary>
    ///    Causes the faces of the cutting element where it intersects the element it is cutting to be split or unsplit.
    /// </summary>
    /// <remarks>There must be a cut between the input elements.</remarks>
    /// <param name="first">The solid being cut or the cutting solid</param>
    /// <param name="second">The solid being cut or the cutting solid</param>
    /// <param name="split">True to split the faces of intersection, false to unsplit them.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    There is no solid-solid cut between the input elements.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    Unable to split or unsplit faces of cutting solid
    /// </exception>
    public static void SplitFacesOfCuttingSolid(this Element first, Element second, bool split)
    {
        SolidSolidCutUtils.SplitFacesOfCuttingSolid(first, second, split);
    }
}