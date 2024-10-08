namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.JoinGeometryUtils"/> class.
/// </summary>
[PublicAPI]
public static class JoinGeometryUtilsExtensions
{
    /// <summary>
    ///     Creates clean joins between two elements that share a common face
    /// </summary>
    /// <remarks>
    ///    The visible edge between joined elements is removed. The joined elements then share the same line weight and fill pattern.
    ///    This functionality is not available for family documents.
    /// </remarks>
    /// <param name="firstElement">The first element to be joined</param>
    /// <param name="secondElement">
    ///    The second element to be joined. This element must not be joined to the first element
    /// </param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The element secondElement was not found in the firstElement document<br />
    ///    The elements are already joined<br />
    ///    The elements cannot be joined<br />
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    Please remove or add segments on curtain grids instead of joining or unjoining geometry of the panels
    /// </exception>
    public static void JoinGeometry(this Element firstElement, Element secondElement)
    {
        JoinGeometryUtils.JoinGeometry(firstElement.Document, firstElement, secondElement);
    }

    /// <summary>
    ///     Removes a join between two elements
    /// </summary>
    /// <remarks>This functionality is not available for family documents</remarks>
    /// <param name="firstElement">The first element to be unjoined</param>
    /// <param name="secondElement">
    ///    The second element to be unjoined. This element must be joined to the fist element
    /// </param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The element secondElement was not found in the firstElement document<br />
    ///    The elements are not joined<br />
    ///    The elements cannot be unjoined
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    Please remove or add segments on curtain grids instead of joining or unjoining geometry of the panels
    /// </exception>
    public static void UnjoinGeometry(this Element firstElement, Element secondElement)
    {
        JoinGeometryUtils.UnjoinGeometry(firstElement.Document, firstElement, secondElement);
    }

    /// <summary>
    ///     Determines whether two elements are joined
    /// </summary>
    /// <remarks>This functionality is not available for family documents</remarks>
    /// <param name="firstElement">The first element</param>
    /// <param name="secondElement">The second element</param>
    /// <returns>True if the two elements are joined</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The element secondElement was not found in the firstElement document
    /// </exception>
    [Pure]
    public static bool AreElementsJoined(this Element firstElement, Element secondElement)
    {
        return JoinGeometryUtils.AreElementsJoined(firstElement.Document, firstElement, secondElement);
    }

    /// <summary>
    ///     Returns all elements joined to given element
    /// </summary>
    /// <remarks>This functionality is not available for family documents</remarks>
    /// <param name="element">The element</param>
    /// <returns>The set of elements that are joined to the given element</returns>
    [Pure]
    public static ICollection<ElementId> GetJoinedElements(this Element element)
    {
        return JoinGeometryUtils.GetJoinedElements(element.Document, element);
    }

    /// <summary>
    ///     Reverses the order in which two elements are joined
    /// </summary>
    /// <remarks>
    ///    The cutting element becomes the cut element and vice versa after the join order is switched
    ///    This functionality is not available for family documents
    /// </remarks>
    /// <param name="firstElement">The first element</param>
    /// <param name="secondElement">
    ///    The second element. This element must be joined to the first element
    /// </param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The element secondElement was not found in the firstElement document<br />
    ///    The elements are not joined<br />
    ///    The elements cannot be joined
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    Unable to switch the join order of these elements
    /// </exception>
    public static void SwitchJoinOrder(this Element firstElement, Element secondElement)
    {
        JoinGeometryUtils.SwitchJoinOrder(firstElement.Document, firstElement, secondElement);
    }

    /// <summary>
    ///    Determines whether the first of two joined elements is cutting the second element
    /// </summary>
    /// <remarks>This functionality is not available for family documents</remarks>
    /// <param name="firstElement">The first element</param>
    /// <param name="secondElement">The second element</param>
    /// <returns>
    ///    True if the secondElement is cut by the firstElement, false if the secondElement is cut by the firstElement
    /// </returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The element secondElement was not found in the firstElement document<br />
    ///    The elements are not joined
    /// </exception>
    [Pure]
    public static bool IsCuttingElementInJoin(this Element firstElement, Element secondElement)
    {
        return JoinGeometryUtils.IsCuttingElementInJoin(firstElement.Document, firstElement, secondElement);
    }
}