namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Element Extensions
/// </summary>
[PublicAPI]
public static class ElementIdExtensions
{
    /// <param name="elementId">The unique identification for an element.</param>
    extension(ElementId elementId)
    {
        /// <summary>
        ///     Gets the element referenced by the input ElementId.
        /// </summary>
        /// <param name="document">The document that contains the element.</param>
        /// <returns>The element.</returns>
        /// <exception cref="System.InvalidOperationException">The ElementId references no element in the document.</exception>
        [Pure]
        public Element ToElement(Document document)
        {
            return document.GetElement(elementId) ?? throw new InvalidOperationException($"The document contains no element with the id {elementId.ToLong()}.");
        }

        /// <summary>
        ///     Gets the element of type <typeparamref name="T" /> referenced by the input ElementId.
        /// </summary>
        /// <typeparam name="T">The expected type of the element.</typeparam>
        /// <param name="document">The document that contains the element.</param>
        /// <returns>The element.</returns>
        /// <exception cref="System.InvalidOperationException">The ElementId references no element in the document.</exception>
        /// <exception cref="System.InvalidCastException">The element is not of type <typeparamref name="T" />.</exception>
        [Pure]
        public T ToElement<T>(Document document) where T : Element
        {
            return (T)elementId.ToElement(document);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Document.GetElement(Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public Element? ToElementOrDefault(Document document)
        {
            return document.GetElement(elementId);
        }

        /// <summary>
        ///     Gets the element of type <typeparamref name="T" /> referenced by the input ElementId.
        /// </summary>
        /// <typeparam name="T">The expected type of the element.</typeparam>
        /// <param name="document">The document that contains the element.</param>
        /// <returns>The element, or <see langword="null" /> when the ElementId references no element or an element of another type.</returns>
        [Pure]
        public T? ToElementOrDefault<T>(Document document) where T : Element
        {
            return document.GetElement(elementId) as T;
        }

        /// <summary>
        ///     Retrieves the numeric value of the ElementId.
        /// </summary>
        /// <returns>The value the identifier holds.</returns>
        [Pure]
        public long ToLong()
        {
#if REVIT2024_OR_GREATER
            return elementId.Value;
#else
            return elementId.IntegerValue;
#endif
        }
    }

    /// <param name="value">The numeric value of an element identifier.</param>
    extension(long value)
    {
#if REVIT2024_OR_GREATER
        /// <inheritdoc cref="Autodesk.Revit.DB.ElementId(System.Int64)" />
#else
        /// <inheritdoc cref="Autodesk.Revit.DB.ElementId(System.Int32)" />
#endif
        [Pure]
        public ElementId ToElementId()
        {
#if REVIT2024_OR_GREATER
            return new ElementId(value);
#else
            return new ElementId((int)value);
#endif
        }
    }

    /// <param name="elementIds">The collection of unique identifications for elements.</param>
    extension(ICollection<ElementId> elementIds)
    {
        /// <summary>
        ///     Retrieves a collection of Elements associated with the specified ElementIds.
        /// </summary>
        /// <param name="document">The document containing the elements.</param>
        /// <returns>A list of elements associated with the specified ElementIds.</returns>
        [Pure]
        public IList<Element> ToElements(Document document)
        {
            if (elementIds.Count == 0)
            {
                return [];
            }

            var elementTypes = new FilteredElementCollector(document, elementIds).WhereElementIsElementType();
            var elementInstances = new FilteredElementCollector(document, elementIds).WhereElementIsNotElementType();
            return elementTypes.UnionWith(elementInstances).ToElements();
        }

        /// <summary>
        ///     Retrieves a collection of Elements associated with the specified ElementIds as the specified type T.
        /// </summary>
        /// <typeparam name="T">The expected type of the elements.</typeparam>
        /// <param name="document">The document containing the elements.</param>
        /// <returns>A list of elements of type T associated with the specified ElementIds.</returns>
        [Pure]
        public IList<T> ToElements<T>(Document document) where T : Element
        {
            if (elementIds.Count == 0)
            {
                return [];
            }

            var elementTypes = new FilteredElementCollector(document, elementIds).WhereElementIsElementType();
            var elementInstances = new FilteredElementCollector(document, elementIds).WhereElementIsNotElementType();
            return elementTypes.UnionWith(elementInstances).Cast<T>().ToList();
        }

        /// <summary>
        ///     Retrieves the Elements associated with the specified ElementIds in their original order.
        /// </summary>
        /// <param name="document">The document containing the elements.</param>
        /// <returns>A list of elements in the same order as the input ElementIds.</returns>
        [Pure]
        public IList<Element> ToOrderedElements(Document document)
        {
            if (elementIds.Count == 0)
            {
                return [];
            }

            var elements = elementIds.ToElements(document);
            var elementDictionary = elements.ToDictionary(static element => element.Id);

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
        /// <returns>A list of elements of type T in the same order as the input ElementIds.</returns>
        [Pure]
        public IList<T> ToOrderedElements<T>(Document document) where T : Element
        {
            if (elementIds.Count == 0)
            {
                return [];
            }

            var elements = elementIds.ToElements<T>(document);
            var elementDictionary = elements.ToDictionary(static element => element.Id);

            var orderedElements = new List<T>(elementIds.Count);
            foreach (var id in elementIds)
            {
                orderedElements.Add(elementDictionary[id]);
            }

            return orderedElements;
        }
    }
}
