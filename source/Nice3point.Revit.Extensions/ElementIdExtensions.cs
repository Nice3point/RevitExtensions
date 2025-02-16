namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Element Extensions
/// </summary>
[PublicAPI]
public static class ElementIdExtensions
{
    /// <summary>
    ///     Retrieves the Element associated with the specified ElementId.
    /// </summary>
    /// <param name="document">The document containing the element.</param>
    /// <param name="id">The ElementId of the element to retrieve.</param>
    /// <returns>The element associated with the specified ElementId, or <see langword="null"/> if the ElementId is invalid.</returns>
    [Pure]
    public static Element? ToElement(this ElementId id, Document document)
    {
        return document.GetElement(id);
    }

    /// <summary>
    ///     Retrieves the Element associated with the specified ElementId as the specified type T.
    /// </summary>
    /// <typeparam name="T">The expected type of the element.</typeparam>
    /// <param name="document">The document containing the element.</param>
    /// <param name="id">The ElementId of the element to retrieve.</param>
    /// <returns>The element of type T associated with the specified ElementId, or <see langword="null"/> if the ElementId is invalid.</returns>
    [Pure]
    public static T? ToElement<T>(this ElementId id, Document document) where T : Element
    {
        return (T?)document.GetElement(id);
    }

    /// <summary>
    ///     Retrieves a collection of Elements associated with the specified ElementIds.
    /// </summary>
    /// <param name="document">The document containing the elements.</param>
    /// <param name="elementIds">The collection of ElementIds to retrieve the elements for.</param>
    /// <returns>A list of elements associated with the specified ElementIds.</returns>
    [Pure]
    public static IList<Element> ToElements(this ICollection<ElementId> elementIds, Document document)
    {
        if (elementIds.Count == 0) return [];
        
        var elementTypes = new FilteredElementCollector(document, elementIds).WhereElementIsElementType();
        var elementInstances = new FilteredElementCollector(document, elementIds).WhereElementIsNotElementType();
        return elementTypes.UnionWith(elementInstances).ToElements();
    }

    /// <summary>
    ///     Retrieves a collection of Elements associated with the specified ElementIds as the specified type T.
    /// </summary>
    /// <typeparam name="T">The expected type of the elements.</typeparam>
    /// <param name="document">The document containing the elements.</param>
    /// <param name="elementIds">The collection of ElementIds to retrieve the elements for.</param>
    /// <returns>A list of elements of type T associated with the specified ElementIds.</returns>
    [Pure]
    public static IList<T> ToElements<T>(this ICollection<ElementId> elementIds, Document document) where T : Element
    {
        if (elementIds.Count == 0) return [];
        
        var elementTypes = new FilteredElementCollector(document, elementIds).WhereElementIsElementType();
        var elementInstances = new FilteredElementCollector(document, elementIds).WhereElementIsNotElementType();
        return Enumerable.Cast<T>(elementTypes.UnionWith(elementInstances)).ToList();
    }
    
    /// <summary>
    ///     Retrieves the Elements associated with the specified ElementIds in their original order.
    /// </summary>
    /// <param name="document">The document containing the elements.</param>
    /// <param name="elementIds">The collection of ElementIds to retrieve the elements for.</param>
    /// <returns>A list of elements in the same order as the input ElementIds.</returns>
    [Pure]
    public static IList<Element> ToOrderedElements(this ICollection<ElementId> elementIds, Document document)
    {
        if (elementIds.Count == 0) return [];
        
        var elements = elementIds.ToElements(document);
        var elementDictionary = elements.ToDictionary(element => element.Id);

        var orderedElements = new List<Element>(elementIds.Count);
        foreach (var id in elementIds)
        {
            orderedElements.Add(elementDictionary[id]);
        }

        return orderedElements;
    }
    
    /// <summary>
    ///     Retrieves the Elements associated with the specified ElementIds and casts them to the specified type T in their original order.
    /// </summary>
    /// <typeparam name="T">The target type derived from Element.</typeparam>
    /// <param name="document">The document containing the elements.</param>
    /// <param name="elementIds">The collection of ElementIds to retrieve the elements for.</param>
    /// <returns>A list of elements of type T in the same order as the input ElementIds.</returns>
    [Pure]
    public static IList<T> ToOrderedElements<T>(this ICollection<ElementId> elementIds, Document document) where T : Element
    {
        if (elementIds.Count == 0) return [];
        
        var elements = elementIds.ToElements<T>(document);
        var elementDictionary = elements.ToDictionary(element => element.Id);

        var orderedElements = new List<T>(elementIds.Count);
        foreach (var id in elementIds)
        {
            orderedElements.Add(elementDictionary[id]);
        }

        return orderedElements;
    }

    /// <summary>
    ///     Checks if ElementID is a category identifier
    /// </summary>
    [Pure]
    public static bool AreEquals(this ElementId elementId, BuiltInCategory category)
    {
#if REVIT2024_OR_GREATER
        return elementId.Value == (long)category;
#else
        return elementId.IntegerValue == (int) category;
#endif
    }

    /// <summary>
    ///     Checks if ElementID is a parameter identifier
    /// </summary>
    [Pure]
    public static bool AreEquals(this ElementId elementId, BuiltInParameter parameter)
    {
#if REVIT2024_OR_GREATER
        return elementId.Value == (long)parameter;
#else
        return elementId.IntegerValue == (int) parameter;
#endif
    }
}