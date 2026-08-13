using Autodesk.Revit.UI.Selection;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.UI;

/// <summary>
///     Revit FilteredElementCollector UI extensions
/// </summary>
[PublicAPI]
public static class FilteredElementCollectorUiExtensions
{
    extension(FilteredElementCollector collector)
    {
        /// <summary>
        ///     Applies a <see cref="SelectableInViewFilter" /> to the collector to match elements
        ///     that are selectable in the given view.
        /// </summary>
        /// <remarks>
        ///     This is a slow filter. It is designed to operate on a list of elements visible in the given view —
        ///     use a <see cref="FilteredElementCollector" /> constructed with the view id for correct results.
        ///     Elements not part of the visible elements of the view may not be correctly restricted.
        /// </remarks>
        /// <param name="document">The document that owns the view.</param>
        /// <param name="viewId">The view id.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector SelectableInView(Document document, ElementId viewId)
        {
            return collector.WherePasses(new SelectableInViewFilter(document, viewId));
        }

        /// <summary>
        ///     Applies a <see cref="SelectableInViewFilter" /> to the collector to match elements
        ///     that are selectable in the given view.
        /// </summary>
        /// <remarks>
        ///     This is a slow filter. It is designed to operate on a list of elements visible in the given view —
        ///     use a <see cref="FilteredElementCollector" /> constructed with the view id for correct results.
        ///     Elements not part of the visible elements of the view may not be correctly restricted.
        /// </remarks>
        /// <param name="view">The view.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector SelectableInView(View view)
        {
            return collector.WherePasses(new SelectableInViewFilter(view.Document, view.Id));
        }

        /// <summary>
        ///     Applies an inverted <see cref="SelectableInViewFilter" /> to the collector to match all elements
        ///     that are not selectable in the given view.
        /// </summary>
        /// <remarks>
        ///     This is a slow filter. It is designed to operate on a list of elements visible in the given view —
        ///     use a <see cref="FilteredElementCollector" /> constructed with the view id for correct results.
        ///     Elements not part of the visible elements of the view may not be correctly restricted.
        /// </remarks>
        /// <param name="document">The document that owns the view.</param>
        /// <param name="viewId">The view id.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector NotSelectableInView(Document document, ElementId viewId)
        {
            return collector.WherePasses(new SelectableInViewFilter(document, viewId, true));
        }

        /// <summary>
        ///     Applies an inverted <see cref="SelectableInViewFilter" /> to the collector to match all elements
        ///     that are not selectable in the given view.
        /// </summary>
        /// <remarks>
        ///     This is a slow filter. It is designed to operate on a list of elements visible in the given view —
        ///     use a <see cref="FilteredElementCollector" /> constructed with the view id for correct results.
        ///     Elements not part of the visible elements of the view may not be correctly restricted.
        /// </remarks>
        /// <param name="view">The view.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector NotSelectableInView(View view)
        {
            return collector.WherePasses(new SelectableInViewFilter(view.Document, view.Id, true));
        }
    }
}
