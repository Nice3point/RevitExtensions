namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit FilteredElementCollector extensions
/// </summary>
/// <remarks>
///     <p>
///         Developers can assign a variety of conditions to filter the elements that are returned.
///         This class requires that at least one condition be set before making the attempt to access the elements.
///     </p>
///     <p>
///         Revit will attempt to organize the filters in order to minimize expansion of elements regardless of
///         the order in which conditions and filters are applied.
///     </p>
///     <p>
///         There are three groups of methods that you can use on a given collector once you have applied filter(s)
///         to it.  One group provides collections of all passing elements, a second finds the first match of the given
///         filter(s), and a third provides an iterator that is evaluated lazily (each element is tested by the filter
///         only when the iterator reaches it). You should only use
///         one of the methods from these group at a time; the collector will reset if you call another method to
///         extract elements.  Thus, if you have previously obtained an iterator, it will be stopped and traverse no more
///         elements if you call another method to extract elements.
///     </p>
///     <p>
///         In .NET, this class supports the IEnumerable interface for Elements.  You can use this class with
///         LINQ queries and operations to process lists of elements.  Note that because the ElementFilters
///         and the shortcut methods offered by this class process elements in native code before their
///         managed wrappers are generated, better performance will be obtained by using as many native filters
///         as possible on the collector before attempting to process the results using LINQ queries.
///     </p>
///     <p>
///         One special consideration when using this class in .NET: the debugger will attempt
///         to traverse the members of the collector because of its implementation of IEnumerable.  You may see strange
///         results if you also attempt to extract the first element or all elements from the collector while the debugger
///         is also looking at the contents of the collector.
///     </p>
/// </remarks>
public static class CollectorExtensions
{
    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>A new FilteredElementCollector that will search and filter the set of elements in a document</returns>
    /// <exception cref="Autodesk.Revit.Exceptions.InvalidOperationException">
    ///     The collector does not have a filter applied. Extraction or iteration of elements is not permitted without a filter
    /// </exception>
    [Pure]
    public static FilteredElementCollector GetElements(this Document document)
    {
        return new FilteredElementCollector(document);
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <returns>A new FilteredElementCollector that will search and filter the visible elements in a view</returns>
    /// <remarks>
    ///     <p>
    ///         Elements that will be passed by the collector have graphics that may be visible in
    ///         the input view. Some elements may still be hidden because they are obscured by other elements.
    ///     </p>
    ///     <p>
    ///         For elements which are outside of a crop region, they may still be passed by the collector because
    ///         Revit relies on later processing to eliminate the elements hidden by the crop.
    ///         This effect may more easily occur for non-rectangular crop regions, but may also happen even for rectangular crops.
    ///         You can compare the boundary of the region with the element's boundary if more precise results are required.
    ///     </p>
    ///     <p>
    ///         Accessing these visible elements may require Revit to rebuild the geometry of the view.
    ///         The first time your code constructs a collector for a given view, or the first time
    ///         your code constructs a collector for a view whose display settings have just been changed,
    ///         you may experience a significant performance degradation.
    ///     </p>
    /// </remarks>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///     viewId is not valid for element iteration, because it has no way of representing drawn elements. Many view templates will fail this check.
    /// </exception>
    [Pure]
    public static FilteredElementCollector GetElements(this Document document, ElementId viewId)
    {
        return new FilteredElementCollector(document, viewId);
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document that owns the elements matching the element ids</param>
    /// <returns>A new FilteredElementCollector that will search and filter a specified set of elements in a document</returns>
    /// <param name="elementIds">The input set of element ids</param>
    [Pure]
    public static FilteredElementCollector GetElements(this Document document, ICollection<ElementId> elementIds)
    {
        return new FilteredElementCollector(document, elementIds);
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetInstances(this Document document, BuiltInCategory category)
    {
        return CollectInstances(document, category).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetInstances(this Document document, BuiltInCategory category, ElementFilter filter)
    {
        return CollectInstances(document, category, filter).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetInstances(this Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        return CollectInstances(document, category, filters).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetInstances(this Document document)
    {
        return CollectInstances(document).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetInstances(this Document document, ElementFilter filter)
    {
        return CollectInstances(document, filter).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetInstances(this Document document, IEnumerable<ElementFilter> filters)
    {
        return CollectInstances(document, filters).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateInstances(this Document document, BuiltInCategory category)
    {
        return CollectInstances(document, category);
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateInstances(this Document document, BuiltInCategory category, ElementFilter filter)
    {
        return CollectInstances(document, category, filter);
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateInstances(this Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        return CollectInstances(document, category, filters);
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateInstances(this Document document)
    {
        return CollectInstances(document);
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateInstances(this Document document, ElementFilter filter)
    {
        return CollectInstances(document, filter);
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateInstances(this Document document, IEnumerable<ElementFilter> filters)
    {
        return CollectInstances(document, filters);
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateInstances<T>(this Document document, BuiltInCategory category) where T : Element
    {
        var elements = CollectInstances(document, category).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateInstances<T>(this Document document, BuiltInCategory category, ElementFilter filter) where T : Element
    {
        var elements = CollectInstances(document, category, filter).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateInstances<T>(this Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters) where T : Element
    {
        var elements = CollectInstances(document, category, filters).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateInstances<T>(this Document document) where T : Element
    {
        var elements = CollectInstances(document).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateInstances<T>(this Document document, ElementFilter filter) where T : Element
    {
        var elements = CollectInstances(document, filter).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateInstances<T>(this Document document, IEnumerable<ElementFilter> filters) where T : Element
    {
        var elements = CollectInstances(document, filters).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance;
        }
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetInstanceIds(this Document document, BuiltInCategory category)
    {
        return CollectInstances(document, category).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetInstanceIds(this Document document, BuiltInCategory category, ElementFilter filter)
    {
        return CollectInstances(document, category, filter).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetInstanceIds(this Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        return CollectInstances(document, category, filters).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetInstanceIds(this Document document)
    {
        return CollectInstances(document).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetInstanceIds(this Document document, ElementFilter filter)
    {
        return CollectInstances(document, filter).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetInstanceIds(this Document document, IEnumerable<ElementFilter> filters)
    {
        return CollectInstances(document, filters).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds(this Document document, BuiltInCategory category)
    {
        foreach (var element in CollectInstances(document, category)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds(this Document document, BuiltInCategory category, ElementFilter filter)
    {
        foreach (var element in CollectInstances(document, category, filter)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds(this Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        foreach (var element in CollectInstances(document, category, filters)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds(this Document document)
    {
        foreach (var element in CollectInstances(document)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds(this Document document, ElementFilter filter)
    {
        foreach (var element in CollectInstances(document, filter)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds(this Document document, IEnumerable<ElementFilter> filters)
    {
        foreach (var element in CollectInstances(document, filters)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds<T>(this Document document, BuiltInCategory category) where T : Element
    {
        var elements = CollectInstances(document, category).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds<T>(this Document document, BuiltInCategory category, ElementFilter filter) where T : Element
    {
        var elements = CollectInstances(document, category, filter).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds<T>(this Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters) where T : Element
    {
        var elements = CollectInstances(document, category, filters).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds<T>(this Document document) where T : Element
    {
        var elements = CollectInstances(document).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds<T>(this Document document, ElementFilter filter) where T : Element
    {
        var elements = CollectInstances(document, filter).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds<T>(this Document document, IEnumerable<ElementFilter> filters) where T : Element
    {
        var elements = CollectInstances(document, filters).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document visible in view
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetInstances(this Document document, ElementId viewId, BuiltInCategory category)
    {
        return CollectInstances(document, viewId, category).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document visible in view
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetInstances(this Document document, ElementId viewId, BuiltInCategory category, ElementFilter filter)
    {
        return CollectInstances(document, viewId, category, filter).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document visible in view
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetInstances(this Document document, ElementId viewId, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        return CollectInstances(document, viewId, category, filters).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document visible in view
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetInstances(this Document document, ElementId viewId)
    {
        return CollectInstances(document, viewId).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document visible in view
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetInstances(this Document document, ElementId viewId, ElementFilter filter)
    {
        return CollectInstances(document, viewId, filter).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document visible in view
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetInstances(this Document document, ElementId viewId, IEnumerable<ElementFilter> filters)
    {
        return CollectInstances(document, viewId, filters).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document visible in view
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateInstances(this Document document, ElementId viewId, BuiltInCategory category)
    {
        return CollectInstances(document, viewId, category);
    }

    /// <summary>
    ///     Searches for elements in a document visible in view
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateInstances(this Document document, ElementId viewId, BuiltInCategory category, ElementFilter filter)
    {
        return CollectInstances(document, viewId, category, filter);
    }

    /// <summary>
    ///     Searches for elements in a document visible in view
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateInstances(this Document document, ElementId viewId, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        return CollectInstances(document, viewId, category, filters);
    }

    /// <summary>
    ///     Searches for elements in a document visible in view
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateInstances(this Document document, ElementId viewId)
    {
        return CollectInstances(document, viewId);
    }

    /// <summary>
    ///     Searches for elements in a document visible in view
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateInstances(this Document document, ElementId viewId, ElementFilter filter)
    {
        return CollectInstances(document, viewId, filter);
    }

    /// <summary>
    ///     Searches for elements in a document visible in view
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateInstances(this Document document, ElementId viewId, IEnumerable<ElementFilter> filters)
    {
        return CollectInstances(document, viewId, filters);
    }

    /// <summary>
    ///     Searches for elements in a document visible in view by class of type T
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateInstances<T>(this Document document, ElementId viewId, BuiltInCategory category) where T : Element
    {
        var elements = CollectInstances(document, viewId, category).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance;
        }
    }

    /// <summary>
    ///     Searches for elements in a document visible in view by class of type T
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateInstances<T>(this Document document, ElementId viewId, BuiltInCategory category, ElementFilter filter) where T : Element
    {
        var elements = CollectInstances(document, viewId, category, filter).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance;
        }
    }

    /// <summary>
    ///     Searches for elements in a document visible in view by class of type T
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateInstances<T>(this Document document, ElementId viewId, BuiltInCategory category, IEnumerable<ElementFilter> filters) where T : Element
    {
        var elements = CollectInstances(document, viewId, category, filters).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance;
        }
    }

    /// <summary>
    ///     Searches for elements in a document visible in view by class of type T
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateInstances<T>(this Document document, ElementId viewId) where T : Element
    {
        var elements = CollectInstances(document, viewId).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance;
        }
    }

    /// <summary>
    ///     Searches for elements in a document visible in view by class of type T
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateInstances<T>(this Document document, ElementId viewId, ElementFilter filter) where T : Element
    {
        var elements = CollectInstances(document, viewId, filter).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance;
        }
    }

    /// <summary>
    ///     Searches for elements in a document visible in view by class of type T
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateInstances<T>(this Document document, ElementId viewId, IEnumerable<ElementFilter> filters) where T : Element
    {
        var elements = CollectInstances(document, viewId, filters).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance;
        }
    }

    /// <summary>
    ///     Searches for elements in a document visible in view
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetInstanceIds(this Document document, ElementId viewId, BuiltInCategory category)
    {
        return CollectInstances(document, viewId, category).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document visible in view
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetInstanceIds(this Document document, ElementId viewId, BuiltInCategory category, ElementFilter filter)
    {
        return CollectInstances(document, viewId, category, filter).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document visible in view
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetInstanceIds(this Document document, ElementId viewId, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        return CollectInstances(document, viewId, category, filters).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document visible in view
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetInstanceIds(this Document document, ElementId viewId)
    {
        return CollectInstances(document, viewId).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document visible in view
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetInstanceIds(this Document document, ElementId viewId, ElementFilter filter)
    {
        return CollectInstances(document, viewId, filter).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document visible in view
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetInstanceIds(this Document document, ElementId viewId, IEnumerable<ElementFilter> filters)
    {
        return CollectInstances(document, viewId, filters).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document visible in view
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds(this Document document, ElementId viewId, BuiltInCategory category)
    {
        foreach (var element in CollectInstances(document, viewId, category)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document visible in view
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds(this Document document, ElementId viewId, BuiltInCategory category, ElementFilter filter)
    {
        foreach (var element in CollectInstances(document, viewId, category, filter)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document visible in view
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds(this Document document, ElementId viewId, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        foreach (var element in CollectInstances(document, viewId, category, filters)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document visible in view
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds(this Document document, ElementId viewId)
    {
        foreach (var element in CollectInstances(document, viewId)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document visible in view
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds(this Document document, ElementId viewId, ElementFilter filter)
    {
        foreach (var element in CollectInstances(document, viewId, filter)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document visible in view
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds(this Document document, ElementId viewId, IEnumerable<ElementFilter> filters)
    {
        foreach (var element in CollectInstances(document, viewId, filters)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document visible in view by class of type T
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds<T>(this Document document, ElementId viewId, BuiltInCategory category) where T : Element
    {
        var elements = CollectInstances(document, viewId, category).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document visible in view by class of type T
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds<T>(this Document document, ElementId viewId, BuiltInCategory category, ElementFilter filter) where T : Element
    {
        var elements = CollectInstances(document, viewId, category, filter).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document visible in view by class of type T
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds<T>(this Document document, ElementId viewId, BuiltInCategory category, IEnumerable<ElementFilter> filters)
        where T : Element
    {
        var elements = CollectInstances(document, viewId, category, filters).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document visible in view by class of type T
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds<T>(this Document document, ElementId viewId) where T : Element
    {
        var elements = CollectInstances(document, viewId).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document visible in view by class of type T
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds<T>(this Document document, ElementId viewId, ElementFilter filter) where T : Element
    {
        var elements = CollectInstances(document, viewId, filter).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document visible in view by class of type T
    /// </summary>
    /// <param name="document">The document that owns the view</param>
    /// <param name="viewId">The view id</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds<T>(this Document document, ElementId viewId, IEnumerable<ElementFilter> filters) where T : Element
    {
        var elements = CollectInstances(document, viewId, filters).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetTypes(this Document document, BuiltInCategory category)
    {
        return CollectTypes(document, category).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetTypes(this Document document, BuiltInCategory category, ElementFilter filter)
    {
        return CollectTypes(document, category, filter).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetTypes(this Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        return CollectTypes(document, category, filters).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetTypes(this Document document)
    {
        return CollectTypes(document).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetTypes(this Document document, ElementFilter filter)
    {
        return CollectTypes(document, filter).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetTypes(this Document document, IEnumerable<ElementFilter> filters)
    {
        return CollectTypes(document, filters).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateTypes(this Document document, BuiltInCategory category)
    {
        return CollectTypes(document, category);
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateTypes(this Document document, BuiltInCategory category, ElementFilter filter)
    {
        return CollectTypes(document, category, filter);
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateTypes(this Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        return CollectTypes(document, category, filters);
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateTypes(this Document document)
    {
        return CollectTypes(document);
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateTypes(this Document document, ElementFilter filter)
    {
        return CollectTypes(document, filter);
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateTypes(this Document document, IEnumerable<ElementFilter> filters)
    {
        return CollectTypes(document, filters);
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateTypes<T>(this Document document, BuiltInCategory category) where T : Element
    {
        var elements = CollectTypes(document, category).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var type = (T) element;
            yield return type;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateTypes<T>(this Document document, BuiltInCategory category, ElementFilter filter) where T : Element
    {
        var elements = CollectTypes(document, category, filter).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var type = (T) element;
            yield return type;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateTypes<T>(this Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters) where T : Element
    {
        var elements = CollectTypes(document, category, filters).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var type = (T) element;
            yield return type;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateTypes<T>(this Document document) where T : Element
    {
        var elements = CollectTypes(document).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var type = (T) element;
            yield return type;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateTypes<T>(this Document document, ElementFilter filter) where T : Element
    {
        var elements = CollectTypes(document, filter).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var type = (T) element;
            yield return type;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateTypes<T>(this Document document, IEnumerable<ElementFilter> filters) where T : Element
    {
        var elements = CollectTypes(document, filters).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var type = (T) element;
            yield return type;
        }
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetTypeIds(this Document document, BuiltInCategory category)
    {
        return CollectTypes(document, category).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetTypeIds(this Document document, BuiltInCategory category, ElementFilter filter)
    {
        return CollectTypes(document, category, filter).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetTypeIds(this Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        return CollectTypes(document, category, filters).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetTypeIds(this Document document)
    {
        return CollectTypes(document).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetTypeIds(this Document document, ElementFilter filter)
    {
        return CollectTypes(document, filter).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetTypeIds(this Document document, IEnumerable<ElementFilter> filters)
    {
        return CollectTypes(document, filters).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateTypeIds(this Document document, BuiltInCategory category)
    {
        foreach (var element in CollectTypes(document, category)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateTypeIds(this Document document, BuiltInCategory category, ElementFilter filter)
    {
        foreach (var element in CollectTypes(document, category, filter)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateTypeIds(this Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        foreach (var element in CollectTypes(document, category, filters)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateTypeIds(this Document document)
    {
        foreach (var element in CollectTypes(document)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateTypeIds(this Document document, ElementFilter filter)
    {
        foreach (var element in CollectTypes(document, filter)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateTypeIds(this Document document, IEnumerable<ElementFilter> filters)
    {
        foreach (var element in CollectTypes(document, filters)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateTypeIds<T>(this Document document, BuiltInCategory category) where T : Element
    {
        var elements = CollectTypes(document, category).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var type = (T) element;
            yield return type.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateTypeIds<T>(this Document document, BuiltInCategory category, ElementFilter filter) where T : Element
    {
        var elements = CollectTypes(document, category, filter).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var type = (T) element;
            yield return type.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateTypeIds<T>(this Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters) where T : Element
    {
        var elements = CollectTypes(document, category, filters).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var type = (T) element;
            yield return type.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateTypeIds<T>(this Document document) where T : Element
    {
        var elements = CollectTypes(document).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var type = (T) element;
            yield return type.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateTypeIds<T>(this Document document, ElementFilter filter) where T : Element
    {
        var elements = CollectTypes(document, filter).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var type = (T) element;
            yield return type.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element"/></typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateTypeIds<T>(this Document document, IEnumerable<ElementFilter> filters) where T : Element
    {
        var elements = CollectTypes(document, filters).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var type = (T) element;
            yield return type.Id;
        }
    }

    private static FilteredElementCollector CollectInstances(Document document)
    {
        return new FilteredElementCollector(document).WhereElementIsNotElementType();
    }

    private static FilteredElementCollector CollectInstances(Document document, BuiltInCategory category)
    {
        return CollectInstances(document).OfCategory(category);
    }

    private static FilteredElementCollector CollectInstances(Document document, ElementFilter filter)
    {
        return CollectInstances(document).WherePasses(filter);
    }

    private static FilteredElementCollector CollectInstances(Document document, IEnumerable<ElementFilter> filters)
    {
        var elements = CollectInstances(document);
        ApplyFilters(elements, filters);
        return elements;
    }

    private static FilteredElementCollector CollectInstances(Document document, BuiltInCategory category, ElementFilter filter)
    {
        return CollectInstances(document, category).WherePasses(filter);
    }

    private static FilteredElementCollector CollectInstances(Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        var elements = CollectInstances(document, category);
        ApplyFilters(elements, filters);
        return elements;
    }

    private static FilteredElementCollector CollectInstances(Document document, ElementId viewId)
    {
        return new FilteredElementCollector(document, viewId).WhereElementIsNotElementType();
    }

    private static FilteredElementCollector CollectInstances(Document document, ElementId viewId, BuiltInCategory category)
    {
        return CollectInstances(document, viewId).OfCategory(category);
    }

    private static FilteredElementCollector CollectInstances(Document document, ElementId viewId, ElementFilter filter)
    {
        return CollectInstances(document, viewId).WherePasses(filter);
    }

    private static FilteredElementCollector CollectInstances(Document document, ElementId viewId, IEnumerable<ElementFilter> filters)
    {
        var elements = CollectInstances(document, viewId);
        ApplyFilters(elements, filters);
        return elements;
    }

    private static FilteredElementCollector CollectInstances(Document document, ElementId viewId, BuiltInCategory category, ElementFilter filter)
    {
        return CollectInstances(document, viewId, category).WherePasses(filter);
    }

    private static FilteredElementCollector CollectInstances(Document document, ElementId viewId, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        var elements = CollectInstances(document, viewId, category);
        ApplyFilters(elements, filters);
        return elements;
    }

    private static FilteredElementCollector CollectTypes(Document document)
    {
        return new FilteredElementCollector(document).WhereElementIsElementType();
    }

    private static FilteredElementCollector CollectTypes(Document document, BuiltInCategory category)
    {
        return CollectTypes(document).OfCategory(category);
    }

    private static FilteredElementCollector CollectTypes(Document document, ElementFilter filter)
    {
        return CollectTypes(document).WherePasses(filter);
    }

    private static FilteredElementCollector CollectTypes(Document document, IEnumerable<ElementFilter> filters)
    {
        var elements = CollectTypes(document);
        ApplyFilters(elements, filters);
        return elements;
    }

    private static FilteredElementCollector CollectTypes(Document document, BuiltInCategory category, ElementFilter filter)
    {
        return CollectTypes(document, category).WherePasses(filter);
    }

    private static FilteredElementCollector CollectTypes(Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        var elements = CollectTypes(document, category);
        ApplyFilters(elements, filters);
        return elements;
    }

    private static void ApplyFilters(FilteredElementCollector elements, IEnumerable<ElementFilter> filters)
    {
        foreach (var elementFilter in filters) elements.WherePasses(elementFilter);
    }
}