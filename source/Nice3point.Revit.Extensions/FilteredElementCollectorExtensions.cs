using System.Diagnostics.CodeAnalysis;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.ExtensibleStorage;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Structure;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit FilteredElementCollector extensions
/// </summary>
[PublicAPI]
[SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
public static partial class FilteredElementCollectorExtensions
{
    /// <param name="document">The document</param>
    extension(Document document)
    {
        /// <summary>
        ///     Creates a collector to search and filter all elements in the document.
        /// </summary>
        /// <returns>A new <see cref="FilteredElementCollector" /> for the document</returns>
        /// <exception cref="Autodesk.Revit.Exceptions.InvalidOperationException">
        ///     The collector does not have a filter applied. Extraction or iteration of elements is not permitted without a filter.
        /// </exception>
        [Pure]
        public FilteredElementCollector CollectElements()
        {
            return new FilteredElementCollector(document);
        }

        /// <summary>
        ///     Creates a collector to search and filter elements visible in the specified view.
        /// </summary>
        /// <param name="viewId">The id of the view to restrict element collection to</param>
        /// <returns>A new <see cref="FilteredElementCollector" /> restricted to the given view</returns>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     viewId is not valid for element iteration, because it has no way of representing drawn elements.
        ///     Many view templates will fail this check.
        /// </exception>
        [Pure]
        public FilteredElementCollector CollectElements(ElementId viewId)
        {
            return new FilteredElementCollector(document, viewId);
        }

        /// <summary>
        ///     Creates a collector to search and filter elements visible in the specified view.
        /// </summary>
        /// <param name="view">The view to restrict element collection to</param>
        /// <returns>A new <see cref="FilteredElementCollector" /> restricted to the given view</returns>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     view is not valid for element iteration, because it has no way of representing drawn elements.
        ///     Many view templates will fail this check.
        /// </exception>
        [Pure]
        public FilteredElementCollector CollectElements(View view)
        {
            return new FilteredElementCollector(document, view.Id);
        }

        /// <summary>
        ///     Creates a collector to search and filter a specific set of elements in the document.
        /// </summary>
        /// <param name="elementIds">The input set of element ids to restrict collection to</param>
        /// <returns>A new <see cref="FilteredElementCollector" /> restricted to the given element ids</returns>
        [Pure]
        public FilteredElementCollector CollectElements(ICollection<ElementId> elementIds)
        {
            return new FilteredElementCollector(document, elementIds);
        }
    }

    extension(FilteredElementCollector collector)
    {
        /// <summary>
        ///     Applies an <see cref="ElementClassFilter" /> to the collector.
        /// </summary>
        /// <remarks>
        ///     Only elements whose class is an exact match to <typeparamref name="T" />,
        ///     or elements whose type is derived from <typeparamref name="T" /> will pass the collector.
        ///     <p>
        ///         There is a small subset of <see cref="Autodesk.Revit.DB.Element" /> subclasses in the API that are not supported
        ///         by this filter. These classes exist in the API, but not in Revit's native object model,
        ///         which means that this filter doesn't support them. In order to use a class filter to
        ///         find elements of these types, it is necessary to use a higher level class and then
        ///         process the results further to find elements matching only the subclass.
        ///         For a list of subclasses affected by this restriction, consult the documentation for
        ///         <see cref="ElementClassFilter" />.
        ///     </p>
        ///     <p>If you have an active iterator to this collector it will be stopped by this call.</p>
        /// </remarks>
        /// <typeparam name="T">The element type.</typeparam>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     The input type is of an element type that exists in the API, but not in Revit's native object model.
        /// </exception>
        [Pure]
        public FilteredElementCollector OfClass<T>() where T : Element
        {
            return collector.OfClass(typeof(T));
        }

        /// <summary>
        ///     Applies an <see cref="ElementMulticlassFilter" /> to the collector to match elements whose class
        ///     matches any of the given types.
        /// </summary>
        /// <param name="types">The list of <see cref="Autodesk.Revit.DB.Element" /> subclass types to match.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     One or more input types are not valid subclasses of <see cref="Autodesk.Revit.DB.Element" /> for this filter,
        ///     or do not exist in Revit's native object model.
        /// </exception>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector OfClasses(IList<Type> types)
        {
            return collector.WherePasses(new ElementMulticlassFilter(types));
        }

        /// <summary>
        ///     Applies an <see cref="ElementMulticlassFilter" /> to the collector to match elements whose class
        ///     matches any of the given types.
        /// </summary>
        /// <param name="types">The list of <see cref="Autodesk.Revit.DB.Element" /> subclass types to match.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     One or more input types are not valid subclasses of <see cref="Autodesk.Revit.DB.Element" /> for this filter,
        ///     or do not exist in Revit's native object model.
        /// </exception>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector OfClasses(params Type[] types)
        {
            return collector.WherePasses(new ElementMulticlassFilter(types));
        }

        /// <summary>
        ///     Applies an inverted <see cref="ElementClassFilter" /> to the collector to match all elements
        ///     that are not of type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">The type to exclude.</typeparam>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     The input type is of an element class that exists in the API, but not in Revit's native object model.
        /// </exception>
        [Pure]
        public FilteredElementCollector ExcludingClass<T>() where T : Element
        {
            return collector.WherePasses(new ElementClassFilter(typeof(T), inverted: true));
        }

        /// <summary>
        ///     Applies an inverted <see cref="ElementMulticlassFilter" /> to the collector to match all elements
        ///     whose class does not match any of the given types.
        /// </summary>
        /// <param name="types">The list of <see cref="Autodesk.Revit.DB.Element" /> subclass types to exclude.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     One or more input types are not valid subclasses of <see cref="Autodesk.Revit.DB.Element" /> for this filter,
        ///     or do not exist in Revit's native object model.
        /// </exception>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector ExcludingClasses(IList<Type> types)
        {
            return collector.WherePasses(new ElementMulticlassFilter(types, inverted: true));
        }

        /// <summary>
        ///     Applies an inverted <see cref="ElementMulticlassFilter" /> to the collector to match all elements
        ///     whose class does not match any of the given types.
        /// </summary>
        /// <param name="types">The list of <see cref="Autodesk.Revit.DB.Element" /> subclass types to exclude.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     One or more input types are not valid subclasses of <see cref="Autodesk.Revit.DB.Element" /> for this filter,
        ///     or do not exist in Revit's native object model.
        /// </exception>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector ExcludingClasses(params Type[] types)
        {
            return collector.WherePasses(new ElementMulticlassFilter(types, inverted: true));
        }

        /// <summary>
        ///     Applies an <see cref="ElementMulticategoryFilter" /> to the collector to match elements
        ///     belonging to any of the given categories.
        /// </summary>
        /// <param name="categories">The built-in categories to match.</param>
        [Pure]
        public FilteredElementCollector OfCategories(ICollection<BuiltInCategory> categories)
        {
            return collector.WherePasses(new ElementMulticategoryFilter(categories));
        }

        /// <summary>
        ///     Applies an <see cref="ElementMulticategoryFilter" /> to the collector to match elements
        ///     belonging to any of the given categories.
        /// </summary>
        /// <param name="categories">The built-in categories to match.</param>
        [Pure]
        public FilteredElementCollector OfCategories(params BuiltInCategory[] categories)
        {
            return collector.WherePasses(new ElementMulticategoryFilter(categories));
        }

        /// <summary>
        ///     Applies an inverted <see cref="ElementCategoryFilter" /> to the collector to match all elements
        ///     not belonging to the given category.
        /// </summary>
        /// <param name="category">The category to exclude.</param>
        [Pure]
        public FilteredElementCollector ExcludingCategory(BuiltInCategory category)
        {
            return collector.WherePasses(new ElementCategoryFilter(category, inverted: true));
        }

        /// <summary>
        ///     Applies an inverted <see cref="ElementMulticategoryFilter" /> to the collector to match all elements
        ///     not belonging to any of the given categories.
        /// </summary>
        /// <param name="categories">The built-in categories to exclude.</param>
        [Pure]
        public FilteredElementCollector ExcludingCategories(ICollection<BuiltInCategory> categories)
        {
            return collector.WherePasses(new ElementMulticategoryFilter(categories, inverted: true));
        }

        /// <summary>
        ///     Applies an inverted <see cref="ElementMulticategoryFilter" /> to the collector to match all elements
        ///     not belonging to any of the given categories.
        /// </summary>
        /// <param name="categories">The built-in categories to exclude.</param>
        [Pure]
        public FilteredElementCollector ExcludingCategories(params BuiltInCategory[] categories)
        {
            return collector.WherePasses(new ElementMulticategoryFilter(categories, inverted: true));
        }
#if REVIT2021_OR_GREATER
        /// <summary>
        ///     Applies an <see cref="ElementIdSetFilter" /> to the collector to match only elements with the given ids.
        /// </summary>
        /// <param name="ids">The ids to match.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     The input collection of ids was empty, or its contents were not valid for iteration.
        /// </exception>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector OfElements(ICollection<ElementId> ids)
        {
            return collector.WherePasses(new ElementIdSetFilter(ids));
        }
#endif

        /// <summary>
        ///     Applies a <see cref="CurveElementFilter" /> to the collector to match curve elements of the given type.
        /// </summary>
        /// <param name="curveElementType">The curve element type to match.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///     A value passed for an enumeration argument is not a member of that enumeration.
        /// </exception>
        [Pure]
        public FilteredElementCollector OfCurveElementType(CurveElementType curveElementType)
        {
            return collector.WherePasses(new CurveElementFilter(curveElementType));
        }

        /// <summary>
        ///     Applies an inverted <see cref="CurveElementFilter" /> to the collector to match all curve elements
        ///     not of the given type.
        /// </summary>
        /// <param name="curveElementType">The curve element type to exclude.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///     A value passed for an enumeration argument is not a member of that enumeration.
        /// </exception>
        [Pure]
        public FilteredElementCollector ExcludingCurveElementType(CurveElementType curveElementType)
        {
            return collector.WherePasses(new CurveElementFilter(curveElementType, inverted: true));
        }

        /// <summary>
        ///     Applies an <see cref="ElementStructuralTypeFilter" /> to the collector to match elements
        ///     of the given structural type.
        /// </summary>
        /// <param name="structuralType">The structural type to match.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///     A value passed for an enumeration argument is not a member of that enumeration.
        /// </exception>
        [Pure]
        public FilteredElementCollector OfStructuralType(StructuralType structuralType)
        {
            return collector.WherePasses(new ElementStructuralTypeFilter(structuralType));
        }

        /// <summary>
        ///     Applies an inverted <see cref="ElementStructuralTypeFilter" /> to the collector to match all elements
        ///     not of the given structural type.
        /// </summary>
        /// <param name="structuralType">The structural type to exclude.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///     A value passed for an enumeration argument is not a member of that enumeration.
        /// </exception>
        [Pure]
        public FilteredElementCollector ExcludingStructuralType(StructuralType structuralType)
        {
            return collector.WherePasses(new ElementStructuralTypeFilter(structuralType, inverted: true));
        }

        /// <summary>
        ///     Applies an <see cref="ElementIsElementTypeFilter" /> to the collector.
        ///     Only element types will pass the collector.
        /// </summary>
        /// <remarks>If you have an active iterator to this collector it will be stopped by this call.</remarks>
        [Pure]
        public FilteredElementCollector Types()
        {
            return collector.WhereElementIsElementType();
        }

        /// <summary>
        ///     Applies an inverted <see cref="ElementIsElementTypeFilter" /> to the collector.
        ///     Only element instances will pass the collector.
        /// </summary>
        /// <remarks>If you have an active iterator to this collector it will be stopped by this call.</remarks>
        [Pure]
        public FilteredElementCollector Instances()
        {
            return collector.WhereElementIsNotElementType();
        }

        /// <summary>
        ///     Applies a <see cref="RoomFilter" /> to the collector to match room elements.
        /// </summary>
        /// <remarks>
        ///     Although this is a slow filter, it uses an internal quick filter to eliminate non-candidate elements
        ///     before expansion. It does not need to be paired with an additional quick filter.
        /// </remarks>
        [Pure]
        public FilteredElementCollector Rooms()
        {
            return collector.WherePasses(new RoomFilter());
        }

        /// <summary>
        ///     Applies a <see cref="RoomTagFilter" /> to the collector to match room tag elements.
        /// </summary>
        /// <remarks>
        ///     Although this is a slow filter, it uses an internal quick filter to eliminate non-candidate elements
        ///     before expansion. It does not need to be paired with an additional quick filter.
        /// </remarks>
        [Pure]
        public FilteredElementCollector RoomTags()
        {
            return collector.WherePasses(new RoomTagFilter());
        }

        /// <summary>
        ///     Applies an <see cref="AreaFilter" /> to the collector to match area elements.
        /// </summary>
        /// <remarks>
        ///     Although this is a slow filter, it uses an internal quick filter to eliminate non-candidate elements
        ///     before expansion. It does not need to be paired with an additional quick filter.
        /// </remarks>
        [Pure]
        public FilteredElementCollector Areas()
        {
            return collector.WherePasses(new AreaFilter());
        }

        /// <summary>
        ///     Applies an <see cref="AreaTagFilter" /> to the collector to match area tag elements.
        /// </summary>
        /// <remarks>
        ///     Although this is a slow filter, it uses an internal quick filter to eliminate non-candidate elements
        ///     before expansion. It does not need to be paired with an additional quick filter.
        /// </remarks>
        [Pure]
        public FilteredElementCollector AreaTags()
        {
            return collector.WherePasses(new AreaTagFilter());
        }

        /// <summary>
        ///     Applies a <see cref="SpaceFilter" /> to the collector to match space elements.
        /// </summary>
        /// <remarks>
        ///     Although this is a slow filter, it uses an internal quick filter to eliminate non-candidate elements
        ///     before expansion. It does not need to be paired with an additional quick filter.
        /// </remarks>
        [Pure]
        public FilteredElementCollector Spaces()
        {
            return collector.WherePasses(new SpaceFilter());
        }

        /// <summary>
        ///     Applies a <see cref="SpaceTagFilter" /> to the collector to match space tag elements.
        /// </summary>
        /// <remarks>
        ///     Although this is a slow filter, it uses an internal quick filter to eliminate non-candidate elements
        ///     before expansion. It does not need to be paired with an additional quick filter.
        /// </remarks>
        [Pure]
        public FilteredElementCollector SpaceTags()
        {
            return collector.WherePasses(new SpaceTagFilter());
        }

        /// <summary>
        ///     Applies a <see cref="FamilySymbolFilter" /> to the collector to match all family symbols
        ///     belonging to the given family.
        /// </summary>
        /// <param name="familyId">The id of the family.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     The familyId is invalid.
        /// </exception>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector FamilySymbols(ElementId familyId)
        {
            return collector.WherePasses(new FamilySymbolFilter(familyId));
        }

        /// <summary>
        ///     Applies a <see cref="FamilySymbolFilter" /> to the collector to match all family symbols
        ///     belonging to the given family.
        /// </summary>
        /// <param name="family">The family.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector FamilySymbols(Family family)
        {
            return collector.WherePasses(new FamilySymbolFilter(family.Id));
        }

        /// <summary>
        ///     Applies a <see cref="FamilyInstanceFilter" /> to the collector to match family instances
        ///     of the given family symbol.
        /// </summary>
        /// <param name="document">
        ///     The document. Required to validate that the symbol id is valid and to ensure maximum filter performance.
        /// </param>
        /// <param name="symbolId">The family symbol id.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     The symbolId does not represent a valid <see cref="Autodesk.Revit.DB.FamilySymbol" /> record in the document.
        /// </exception>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector FamilyInstances(Document document, ElementId symbolId)
        {
            return collector.WherePasses(new FamilyInstanceFilter(document, symbolId));
        }

        /// <summary>
        ///     Applies a <see cref="FamilyInstanceFilter" /> to the collector to match family instances
        ///     of the given family symbol.
        /// </summary>
        /// <param name="symbol">The family symbol.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     The symbol does not represent a valid <see cref="Autodesk.Revit.DB.FamilySymbol" /> record in the document.
        /// </exception>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector FamilyInstances(FamilySymbol symbol)
        {
            return collector.WherePasses(new FamilyInstanceFilter(symbol.Document, symbol.Id));
        }

        /// <summary>
        ///     Applies an <see cref="ElementIsCurveDrivenFilter" /> to the collector to match elements that are curve-driven.
        /// </summary>
        [Pure]
        public FilteredElementCollector IsCurveDriven()
        {
            return collector.WhereElementIsCurveDriven();
        }

        /// <summary>
        ///     Applies an <see cref="ElementIsCurveDrivenFilter" /> to the collector to match elements that are view-independent.
        /// </summary>
        [Pure]
        public FilteredElementCollector IsViewIndependent()
        {
            return collector.WhereElementIsViewIndependent();
        }

        /// <summary>
        ///     Applies a <see cref="PrimaryDesignOptionMemberFilter" /> to the collector to match elements
        ///     contained in any primary design option of any design option set.
        /// </summary>
        [Pure]
        public FilteredElementCollector IsPrimaryDesignOptionMember()
        {
            return collector.WherePasses(new PrimaryDesignOptionMemberFilter());
        }

        /// <summary>
        ///     Applies an inverted <see cref="PrimaryDesignOptionMemberFilter" /> to the collector to match all elements
        ///     not contained in any primary design option of any design option set.
        /// </summary>
        [Pure]
        public FilteredElementCollector IsNotPrimaryDesignOptionMember()
        {
            return collector.WherePasses(new PrimaryDesignOptionMemberFilter(inverted: true));
        }

        /// <summary>
        ///     Applies an <see cref="ElementOwnerViewFilter" /> to the collector to match elements owned by the given view.
        /// </summary>
        /// <param name="viewId">
        ///     The view id to match. Pass an invalid element id to match non-view-specific elements.
        /// </param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector OwnedByView(ElementId viewId)
        {
            return collector.WherePasses(new ElementOwnerViewFilter(viewId));
        }

        /// <summary>
        ///     Applies an <see cref="ElementOwnerViewFilter" /> to the collector to match elements owned by the given view.
        /// </summary>
        /// <param name="view">The view to match.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector OwnedByView(View view)
        {
            return collector.WherePasses(new ElementOwnerViewFilter(view.Id));
        }

        /// <summary>
        ///     Applies an inverted <see cref="ElementOwnerViewFilter" /> to the collector to match all elements
        ///     not owned by the given view.
        /// </summary>
        /// <param name="viewId">
        ///     The view id to exclude. Pass an invalid element id to exclude non-view-specific elements.
        /// </param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector NotOwnedByView(ElementId viewId)
        {
            return collector.WherePasses(new ElementOwnerViewFilter(viewId, inverted: true));
        }

        /// <summary>
        ///     Applies an inverted <see cref="ElementOwnerViewFilter" /> to the collector to match all elements
        ///     not owned by the given view.
        /// </summary>
        /// <param name="view">The view to exclude.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector NotOwnedByView(View view)
        {
            return collector.WherePasses(new ElementOwnerViewFilter(view.Id, inverted: true));
        }
#if REVIT2021_OR_GREATER
        /// <summary>
        ///     Applies a <see cref="VisibleInViewFilter" /> to the collector to match elements visible in the given view.
        /// </summary>
        /// <param name="document">The document that owns the view.</param>
        /// <param name="viewId">The view id.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     viewId is not a view, or is not valid for element iteration because it has no way of representing drawn elements.
        ///     Many view templates will fail this check.
        /// </exception>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector VisibleInView(Document document, ElementId viewId)
        {
            return collector.WherePasses(new VisibleInViewFilter(document, viewId));
        }

        /// <summary>
        ///     Applies a <see cref="VisibleInViewFilter" /> to the collector to match elements visible in the given view.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     The view is not valid for element iteration because it has no way of representing drawn elements.
        ///     Many view templates will fail this check.
        /// </exception>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector VisibleInView(View view)
        {
            return collector.WherePasses(new VisibleInViewFilter(view.Document, view.Id));
        }

        /// <summary>
        ///     Applies an inverted <see cref="VisibleInViewFilter" /> to the collector to match all elements
        ///     not visible in the given view.
        /// </summary>
        /// <param name="document">The document that owns the view.</param>
        /// <param name="viewId">The view id.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     viewId is not a view, or is not valid for element iteration because it has no way of representing drawn elements.
        ///     Many view templates will fail this check.
        /// </exception>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector NotVisibleInView(Document document, ElementId viewId)
        {
            return collector.WherePasses(new VisibleInViewFilter(document, viewId, inverted: true));
        }

        /// <summary>
        ///     Applies an inverted <see cref="VisibleInViewFilter" /> to the collector to match all elements
        ///     not visible in the given view.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     The view is not valid for element iteration because it has no way of representing drawn elements.
        ///     Many view templates will fail this check.
        /// </exception>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector NotVisibleInView(View view)
        {
            return collector.WherePasses(new VisibleInViewFilter(view.Document, view.Id, inverted: true));
        }
#endif

        /// <summary>
        ///     Applies an <see cref="ElementLevelFilter" /> to the collector to match elements associated with the given level.
        /// </summary>
        /// <param name="levelId">The id of the level to match.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector OnLevel(ElementId levelId)
        {
            return collector.WherePasses(new ElementLevelFilter(levelId));
        }

        /// <summary>
        ///     Applies an <see cref="ElementLevelFilter" /> to the collector to match elements associated with the given level.
        /// </summary>
        /// <param name="level">The level to match.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector OnLevel(Level level)
        {
            return collector.WherePasses(new ElementLevelFilter(level.Id));
        }

        /// <summary>
        ///     Applies an inverted <see cref="ElementLevelFilter" /> to the collector to match all elements
        ///     not associated with the given level.
        /// </summary>
        /// <param name="levelId">The id of the level to exclude.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector NotOnLevel(ElementId levelId)
        {
            return collector.WherePasses(new ElementLevelFilter(levelId, inverted: true));
        }

        /// <summary>
        ///     Applies an inverted <see cref="ElementLevelFilter" /> to the collector to match all elements
        ///     not associated with the given level.
        /// </summary>
        /// <param name="level">The level to exclude.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector NotOnLevel(Level level)
        {
            return collector.WherePasses(new ElementLevelFilter(level.Id, inverted: true));
        }

        /// <summary>
        ///     Applies an <see cref="ElementDesignOptionFilter" /> to the collector to match elements
        ///     contained within the given design option.
        /// </summary>
        /// <param name="designOptionId">The design option id to match.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector InDesignOption(ElementId designOptionId)
        {
            return collector.WherePasses(new ElementDesignOptionFilter(designOptionId));
        }

        /// <summary>
        ///     Applies an <see cref="ElementDesignOptionFilter" /> to the collector to match elements
        ///     contained within the given design option.
        /// </summary>
        /// <param name="designOption">The design option to match.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector InDesignOption(DesignOption designOption)
        {
            return collector.WherePasses(new ElementDesignOptionFilter(designOption.Id));
        }

        /// <summary>
        ///     Applies an inverted <see cref="ElementDesignOptionFilter" /> to the collector to match all elements
        ///     not contained within the given design option.
        /// </summary>
        /// <param name="designOptionId">The design option id to exclude.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector NotInDesignOption(ElementId designOptionId)
        {
            return collector.WherePasses(new ElementDesignOptionFilter(designOptionId, inverted: true));
        }

        /// <summary>
        ///     Applies an inverted <see cref="ElementDesignOptionFilter" /> to the collector to match all elements
        ///     not contained within the given design option.
        /// </summary>
        /// <param name="designOption">The design option to exclude.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector NotInDesignOption(DesignOption designOption)
        {
            return collector.WherePasses(new ElementDesignOptionFilter(designOption.Id, inverted: true));
        }

        /// <summary>
        ///     Applies an <see cref="ElementWorksetFilter" /> to the collector to match elements in the given workset.
        /// </summary>
        /// <param name="worksetId">The workset id to match.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector InWorkset(WorksetId worksetId)
        {
            return collector.WherePasses(new ElementWorksetFilter(worksetId));
        }

        /// <summary>
        ///     Applies an inverted <see cref="ElementWorksetFilter" /> to the collector to match all elements
        ///     not in the given workset.
        /// </summary>
        /// <param name="worksetId">The workset id to exclude.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector NotInWorkset(WorksetId worksetId)
        {
            return collector.WherePasses(new ElementWorksetFilter(worksetId, inverted: true));
        }

        /// <summary>
        ///     Applies a <see cref="StructuralInstanceUsageFilter" /> to the collector to match structural family instances
        ///     (typically columns, beams, or braces) with the given structural usage.
        /// </summary>
        /// <param name="usage">The structural usage to match.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///     A value passed for an enumeration argument is not a member of that enumeration.
        /// </exception>
        [Pure]
        public FilteredElementCollector WithStructuralUsage(StructuralInstanceUsage usage)
        {
            return collector.WherePasses(new StructuralInstanceUsageFilter(usage));
        }

        /// <summary>
        ///     Applies an inverted <see cref="StructuralInstanceUsageFilter" /> to the collector to match all family instances
        ///     not of the given structural usage.
        /// </summary>
        /// <param name="usage">The structural usage to exclude.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///     A value passed for an enumeration argument is not a member of that enumeration.
        /// </exception>
        [Pure]
        public FilteredElementCollector WithoutStructuralUsage(StructuralInstanceUsage usage)
        {
            return collector.WherePasses(new StructuralInstanceUsageFilter(usage, inverted: true));
        }

        /// <summary>
        ///     Applies a <see cref="StructuralWallUsageFilter" /> to the collector to match walls
        ///     with the given structural wall usage.
        /// </summary>
        /// <param name="usage">The structural wall usage to match.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///     A value passed for an enumeration argument is not a member of that enumeration.
        /// </exception>
        [Pure]
        public FilteredElementCollector WithStructuralWallUsage(StructuralWallUsage usage)
        {
            return collector.WherePasses(new StructuralWallUsageFilter(usage));
        }

        /// <summary>
        ///     Applies an inverted <see cref="StructuralWallUsageFilter" /> to the collector to match all walls
        ///     not of the given structural wall usage.
        /// </summary>
        /// <param name="usage">The structural wall usage to exclude.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///     A value passed for an enumeration argument is not a member of that enumeration.
        /// </exception>
        [Pure]
        public FilteredElementCollector WithoutStructuralWallUsage(StructuralWallUsage usage)
        {
            return collector.WherePasses(new StructuralWallUsageFilter(usage, inverted: true));
        }

        /// <summary>
        ///     Applies a <see cref="StructuralMaterialTypeFilter" /> to the collector to match family instances
        ///     that have the given structural material type.
        /// </summary>
        /// <param name="type">The structural material type to match.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///     A value passed for an enumeration argument is not a member of that enumeration.
        /// </exception>
        [Pure]
        public FilteredElementCollector WithStructuralMaterial(StructuralMaterialType type)
        {
            return collector.WherePasses(new StructuralMaterialTypeFilter(type));
        }

        /// <summary>
        ///     Applies an inverted <see cref="StructuralMaterialTypeFilter" /> to the collector to match all family instances
        ///     not of the given structural material type.
        /// </summary>
        /// <param name="type">The structural material type to exclude.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///     A value passed for an enumeration argument is not a member of that enumeration.
        /// </exception>
        [Pure]
        public FilteredElementCollector WithoutStructuralMaterial(StructuralMaterialType type)
        {
            return collector.WherePasses(new StructuralMaterialTypeFilter(type, inverted: true));
        }

        /// <summary>
        ///     Applies a <see cref="FamilyStructuralMaterialTypeFilter" /> to the collector to match families
        ///     that have the given structural material type.
        /// </summary>
        /// <param name="type">The structural material type to match.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///     A value passed for an enumeration argument is not a member of that enumeration.
        /// </exception>
        [Pure]
        public FilteredElementCollector WithFamilyStructuralMaterial(StructuralMaterialType type)
        {
            return collector.WherePasses(new FamilyStructuralMaterialTypeFilter(type));
        }

        /// <summary>
        ///     Applies an inverted <see cref="FamilyStructuralMaterialTypeFilter" /> to the collector to match all families
        ///     not of the given structural material type.
        /// </summary>
        /// <param name="type">The structural material type to exclude.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///     A value passed for an enumeration argument is not a member of that enumeration.
        /// </exception>
        [Pure]
        public FilteredElementCollector WithoutFamilyStructuralMaterial(StructuralMaterialType type)
        {
            return collector.WherePasses(new FamilyStructuralMaterialTypeFilter(type, inverted: true));
        }

        /// <summary>
        ///     Applies an <see cref="ElementPhaseStatusFilter" /> to the collector to match elements
        ///     that have any of the given phase statuses on the given phase.
        /// </summary>
        /// <param name="phaseId">Id of the phase.</param>
        /// <param name="statuses">Target statuses.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector WithPhaseStatus(ElementId phaseId, ICollection<ElementOnPhaseStatus> statuses)
        {
            return collector.WherePasses(new ElementPhaseStatusFilter(phaseId, statuses));
        }

        /// <summary>
        ///     Applies an <see cref="ElementPhaseStatusFilter" /> to the collector to match elements
        ///     that have any of the given phase statuses on the given phase.
        /// </summary>
        /// <param name="phaseId">Id of the phase.</param>
        /// <param name="statuses">Target statuses.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector WithPhaseStatus(ElementId phaseId, params ElementOnPhaseStatus[] statuses)
        {
            return collector.WherePasses(new ElementPhaseStatusFilter(phaseId, statuses));
        }

        /// <summary>
        ///     Applies an <see cref="ElementPhaseStatusFilter" /> to the collector to match elements
        ///     that have any of the given phase statuses on the given phase.
        /// </summary>
        /// <param name="phase">The phase.</param>
        /// <param name="statuses">Target statuses.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector WithPhaseStatus(Phase phase, ICollection<ElementOnPhaseStatus> statuses)
        {
            return collector.WherePasses(new ElementPhaseStatusFilter(phase.Id, statuses));
        }

        /// <summary>
        ///     Applies an <see cref="ElementPhaseStatusFilter" /> to the collector to match elements
        ///     that have any of the given phase statuses on the given phase.
        /// </summary>
        /// <param name="phase">The phase.</param>
        /// <param name="statuses">Target statuses.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector WithPhaseStatus(Phase phase, params ElementOnPhaseStatus[] statuses)
        {
            return collector.WherePasses(new ElementPhaseStatusFilter(phase.Id, statuses));
        }

        /// <summary>
        ///     Applies an inverted <see cref="ElementPhaseStatusFilter" /> to the collector to match all elements
        ///     that do not have any of the given phase statuses on the given phase.
        /// </summary>
        /// <param name="phaseId">Id of the phase.</param>
        /// <param name="statuses">Statuses to exclude.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector WithoutPhaseStatus(ElementId phaseId, ICollection<ElementOnPhaseStatus> statuses)
        {
            return collector.WherePasses(new ElementPhaseStatusFilter(phaseId, statuses, inverted: true));
        }

        /// <summary>
        ///     Applies an inverted <see cref="ElementPhaseStatusFilter" /> to the collector to match all elements
        ///     that do not have any of the given phase statuses on the given phase.
        /// </summary>
        /// <param name="phaseId">Id of the phase.</param>
        /// <param name="statuses">Statuses to exclude.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector WithoutPhaseStatus(ElementId phaseId, params ElementOnPhaseStatus[] statuses)
        {
            return collector.WherePasses(new ElementPhaseStatusFilter(phaseId, statuses, inverted: true));
        }

        /// <summary>
        ///     Applies an inverted <see cref="ElementPhaseStatusFilter" /> to the collector to match all elements
        ///     that do not have any of the given phase statuses on the given phase.
        /// </summary>
        /// <param name="phase">The phase.</param>
        /// <param name="statuses">Statuses to exclude.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector WithoutPhaseStatus(Phase phase, ICollection<ElementOnPhaseStatus> statuses)
        {
            return collector.WherePasses(new ElementPhaseStatusFilter(phase.Id, statuses, inverted: true));
        }

        /// <summary>
        ///     Applies an inverted <see cref="ElementPhaseStatusFilter" /> to the collector to match all elements
        ///     that do not have any of the given phase statuses on the given phase.
        /// </summary>
        /// <param name="phase">The phase.</param>
        /// <param name="statuses">Statuses to exclude.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector WithoutPhaseStatus(Phase phase, params ElementOnPhaseStatus[] statuses)
        {
            return collector.WherePasses(new ElementPhaseStatusFilter(phase.Id, statuses, inverted: true));
        }

        /// <summary>
        ///     Applies an <see cref="ExtensibleStorageFilter" /> to the collector to match elements
        ///     that have extensible storage data for the given schema.
        /// </summary>
        /// <param name="schemaGuid">The schema GUID to match.</param>
        [Pure]
        public FilteredElementCollector WithExtensibleStorage(Guid schemaGuid)
        {
            return collector.WherePasses(new ExtensibleStorageFilter(schemaGuid));
        }

        /// <summary>
        ///     Applies an <see cref="ExtensibleStorageFilter" /> to the collector to match elements
        ///     that have extensible storage data for the given schema.
        /// </summary>
        /// <param name="schema">The schema to match.</param>
        [Pure]
        public FilteredElementCollector WithExtensibleStorage(Schema schema)
        {
            return collector.WherePasses(new ExtensibleStorageFilter(schema.GUID));
        }

        /// <summary>
        ///     Applies a <see cref="SharedParameterApplicableRule" /> filter to the collector to match elements
        ///     that support a shared parameter with the given name.
        /// </summary>
        /// <param name="parameterName">
        ///     The name of the shared parameter that an element must support to pass this filter.
        /// </param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector HasSharedParameter(string parameterName)
        {
            return collector.WherePasses(new ElementParameterFilter(new SharedParameterApplicableRule(parameterName)));
        }

        /// <summary>
        ///     Applies a <see cref="BoundingBoxIntersectsFilter" /> to the collector to match elements
        ///     whose bounding box intersects the given outline.
        /// </summary>
        /// <param name="outline">The outline to check intersection against.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     outline is an empty Outline.
        /// </exception>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector IntersectingBoundingBox(Outline outline)
        {
            return collector.WherePasses(new BoundingBoxIntersectsFilter(outline));
        }

        /// <summary>
        ///     Applies a <see cref="BoundingBoxIntersectsFilter" /> to the collector to match elements
        ///     whose bounding box intersects the given outline within the given tolerance.
        /// </summary>
        /// <param name="outline">The outline to check intersection against.</param>
        /// <param name="tolerance">The tolerance value to use instead of zero.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     outline is an empty Outline, or the tolerance value is not finite or not a number.
        /// </exception>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector IntersectingBoundingBox(Outline outline, double tolerance)
        {
            return collector.WherePasses(new BoundingBoxIntersectsFilter(outline, tolerance));
        }

        /// <summary>
        ///     Applies an inverted <see cref="BoundingBoxIntersectsFilter" /> to the collector to match all elements
        ///     whose bounding box does not intersect the given outline.
        /// </summary>
        /// <param name="outline">The outline to check intersection against.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     outline is an empty Outline.
        /// </exception>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector NotIntersectingBoundingBox(Outline outline)
        {
            return collector.WherePasses(new BoundingBoxIntersectsFilter(outline, inverted: true));
        }

        /// <summary>
        ///     Applies an inverted <see cref="BoundingBoxIntersectsFilter" /> to the collector to match all elements
        ///     whose bounding box does not intersect the given outline within the given tolerance.
        /// </summary>
        /// <param name="outline">The outline to check intersection against.</param>
        /// <param name="tolerance">The tolerance value to use instead of zero.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     outline is an empty Outline, or the tolerance value is not finite or not a number.
        /// </exception>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector NotIntersectingBoundingBox(Outline outline, double tolerance)
        {
            return collector.WherePasses(new BoundingBoxIntersectsFilter(outline, tolerance, inverted: true));
        }

        /// <summary>
        ///     Applies a <see cref="BoundingBoxIsInsideFilter" /> to the collector to match elements
        ///     whose bounding box is fully contained by the given outline.
        /// </summary>
        /// <param name="outline">The outline to check containment against.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     outline is an empty Outline.
        /// </exception>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector InsideBoundingBox(Outline outline)
        {
            return collector.WherePasses(new BoundingBoxIsInsideFilter(outline));
        }

        /// <summary>
        ///     Applies a <see cref="BoundingBoxIsInsideFilter" /> to the collector to match elements
        ///     whose bounding box is fully contained by the given outline within the given tolerance.
        /// </summary>
        /// <param name="outline">The outline to check containment against.</param>
        /// <param name="tolerance">The tolerance value to use instead of zero.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     outline is an empty Outline, or the tolerance value is not finite or not a number.
        /// </exception>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector InsideBoundingBox(Outline outline, double tolerance)
        {
            return collector.WherePasses(new BoundingBoxIsInsideFilter(outline, tolerance));
        }

        /// <summary>
        ///     Applies an inverted <see cref="BoundingBoxIsInsideFilter" /> to the collector to match all elements
        ///     whose bounding box is not fully contained by the given outline.
        /// </summary>
        /// <param name="outline">The outline to check containment against.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     outline is an empty Outline.
        /// </exception>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector NotInsideBoundingBox(Outline outline)
        {
            return collector.WherePasses(new BoundingBoxIsInsideFilter(outline, inverted: true));
        }

        /// <summary>
        ///     Applies an inverted <see cref="BoundingBoxIsInsideFilter" /> to the collector to match all elements
        ///     whose bounding box is not fully contained by the given outline within the given tolerance.
        /// </summary>
        /// <param name="outline">The outline to check containment against.</param>
        /// <param name="tolerance">The tolerance value to use instead of zero.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     outline is an empty Outline, or the tolerance value is not finite or not a number.
        /// </exception>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector NotInsideBoundingBox(Outline outline, double tolerance)
        {
            return collector.WherePasses(new BoundingBoxIsInsideFilter(outline, tolerance, inverted: true));
        }

        /// <summary>
        ///     Applies a <see cref="BoundingBoxContainsPointFilter" /> to the collector to match elements
        ///     whose bounding box contains the given point.
        /// </summary>
        /// <param name="point">The point to check containment for.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector ContainingPoint(XYZ point)
        {
            return collector.WherePasses(new BoundingBoxContainsPointFilter(point));
        }

        /// <summary>
        ///     Applies a <see cref="BoundingBoxContainsPointFilter" /> to the collector to match elements
        ///     whose bounding box contains the given point within the given tolerance.
        /// </summary>
        /// <param name="point">The point to check containment for.</param>
        /// <param name="tolerance">The tolerance value to use instead of zero.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     The tolerance value is not finite or not a number.
        /// </exception>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector ContainingPoint(XYZ point, double tolerance)
        {
            return collector.WherePasses(new BoundingBoxContainsPointFilter(point, tolerance));
        }

        /// <summary>
        ///     Applies an inverted <see cref="BoundingBoxContainsPointFilter" /> to the collector to match all elements
        ///     whose bounding box does not contain the given point.
        /// </summary>
        /// <param name="point">The point to check containment for.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector NotContainingPoint(XYZ point)
        {
            return collector.WherePasses(new BoundingBoxContainsPointFilter(point, inverted: true));
        }

        /// <summary>
        ///     Applies an inverted <see cref="BoundingBoxContainsPointFilter" /> to the collector to match all elements
        ///     whose bounding box does not contain the given point within the given tolerance.
        /// </summary>
        /// <param name="point">The point to check containment for.</param>
        /// <param name="tolerance">The tolerance value to use instead of zero.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     The tolerance value is not finite or not a number.
        /// </exception>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector NotContainingPoint(XYZ point, double tolerance)
        {
            return collector.WherePasses(new BoundingBoxContainsPointFilter(point, tolerance, inverted: true));
        }

        /// <summary>
        ///     Applies an <see cref="ElementIntersectsElementFilter" /> to the collector to match elements
        ///     whose geometry intersects the given element.
        /// </summary>
        /// <param name="element">The element to check intersection against.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     The category or type of the element is not supported for element intersection filters.
        /// </exception>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector IntersectingElement(Element element)
        {
            return collector.WherePasses(new ElementIntersectsElementFilter(element));
        }

        /// <summary>
        ///     Applies an inverted <see cref="ElementIntersectsElementFilter" /> to the collector to match all elements
        ///     whose geometry does not intersect the given element.
        /// </summary>
        /// <param name="element">The element to check intersection against.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     The category or type of the element is not supported for element intersection filters.
        /// </exception>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector NotIntersectingElement(Element element)
        {
            return collector.WherePasses(new ElementIntersectsElementFilter(element, inverted: true));
        }

        /// <summary>
        ///     Applies an <see cref="ElementIntersectsSolidFilter" /> to the collector to match elements
        ///     whose geometry intersects the given solid.
        /// </summary>
        /// <param name="solid">The solid geometry to check intersection against.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector IntersectingSolid(Solid solid)
        {
            return collector.WherePasses(new ElementIntersectsSolidFilter(solid));
        }

        /// <summary>
        ///     Applies an inverted <see cref="ElementIntersectsSolidFilter" /> to the collector to match all elements
        ///     whose geometry does not intersect the given solid.
        /// </summary>
        /// <param name="solid">The solid geometry to check intersection against.</param>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentNullException">
        ///     A non-optional argument was null.
        /// </exception>
        [Pure]
        public FilteredElementCollector NotIntersectingSolid(Solid solid)
        {
            return collector.WherePasses(new ElementIntersectsSolidFilter(solid, inverted: true));
        }

        /// <summary>
        ///     Begins a parameter filter expression for the given built-in parameter.
        /// </summary>
        /// <returns>A <see cref="ParameterFilterBuilder" /> to complete the filter expression.</returns>
        [Pure]
        public ParameterFilterBuilder WhereParameter(BuiltInParameter parameter)
        {
            return new ParameterFilterBuilder(collector, new ElementId(parameter));
        }

        /// <summary>
        ///     Begins a parameter filter expression for the given parameter id (shared or project parameter).
        /// </summary>
        /// <returns>A <see cref="ParameterFilterBuilder" /> to complete the filter expression.</returns>
        [Pure]
        public ParameterFilterBuilder WhereParameter(ElementId parameterId)
        {
            return new ParameterFilterBuilder(collector, parameterId);
        }

        /// <summary>
        ///     Returns the first element in the collector.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">The collector contains no elements.</exception>
        [Pure]
        public Element First()
        {
            return collector.FirstElement() ?? throw new InvalidOperationException("Sequence contains no elements.");
        }

        /// <summary>
        ///     Returns the first element in the collector, or <see langword="null" /> if the collector contains no elements.
        /// </summary>
        [Pure]
        public Element? FirstOrDefault()
        {
            return collector.FirstElement();
        }

        /// <summary>
        ///     Returns the number of elements in the collector.
        /// </summary>
        /// <remarks>
        ///     This method calls Revit's native <c>GetElementCount()</c> which iterates all elements internally.
        ///     Avoid calling this method in performance-sensitive paths.
        /// </remarks>
        [Pure]
        public int Count()
        {
            return collector.GetElementCount();
        }

        /// <summary>
        ///     Returns <see langword="true" /> if the collector contains at least one element.
        /// </summary>
        [Pure]
        public bool Any()
        {
#if REVIT2024_OR_GREATER
            return collector.FirstElementId().Value > 0;
#else
            return collector.FirstElementId().IntegerValue > 0;
#endif
        }
    }
}

/// <summary>
///     Revit ParameterFilterRuleFactory extensions
/// </summary>
[PublicAPI]
public sealed class ParameterFilterBuilder
{
    private readonly FilteredElementCollector _collector;
    private readonly ElementId _parameterId;

    internal ParameterFilterBuilder(FilteredElementCollector collector, ElementId parameterId)
    {
        _collector = collector;
        _parameterId = parameterId;
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter value
    ///     equals the given string.
    /// </summary>
    /// <param name="value">The string value to compare against.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector Equals(string value)
    {
#if REVIT2023_OR_GREATER
        return ApplyFilter(ParameterFilterRuleFactory.CreateEqualsRule(_parameterId, value));
#else
        return ApplyFilter(ParameterFilterRuleFactory.CreateEqualsRule(_parameterId, value, true));
#endif
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter value
    ///     equals the given integer.
    /// </summary>
    /// <param name="value">The integer value to compare against.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector Equals(int value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateEqualsRule(_parameterId, value));
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter value
    ///     equals the given double within the specified tolerance.
    /// </summary>
    /// <param name="value">The double value to compare against.</param>
    /// <param name="epsilon">The tolerance within which two values are considered equal.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///     <paramref name="value" /> or <paramref name="epsilon" /> is not finite or not a number.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector Equals(double value, double epsilon)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateEqualsRule(_parameterId, value, epsilon));
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter value
    ///     equals the given <see cref="Autodesk.Revit.DB.ElementId" />.
    /// </summary>
    /// <param name="value">The <see cref="Autodesk.Revit.DB.ElementId" /> value to compare against.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector Equals(ElementId value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateEqualsRule(_parameterId, value));
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter value
    ///     does not equal the given string.
    /// </summary>
    /// <param name="value">The string value to compare against.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector NotEquals(string value)
    {
#if REVIT2023_OR_GREATER
        return ApplyFilter(ParameterFilterRuleFactory.CreateNotEqualsRule(_parameterId, value));
#else
        return ApplyFilter(ParameterFilterRuleFactory.CreateNotEqualsRule(_parameterId, value, true));
#endif
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter value
    ///     does not equal the given integer.
    /// </summary>
    /// <param name="value">The integer value to compare against.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector NotEquals(int value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateNotEqualsRule(_parameterId, value));
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter value
    ///     does not equal the given double within the specified tolerance.
    /// </summary>
    /// <param name="value">The double value to compare against.</param>
    /// <param name="epsilon">The tolerance within which two values are considered equal.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///     <paramref name="value" /> or <paramref name="epsilon" /> is not finite or not a number.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector NotEquals(double value, double epsilon)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateNotEqualsRule(_parameterId, value, epsilon));
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter value
    ///     does not equal the given <see cref="Autodesk.Revit.DB.ElementId" />.
    /// </summary>
    /// <param name="value">The <see cref="Autodesk.Revit.DB.ElementId" /> value to compare against.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector NotEquals(ElementId value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateNotEqualsRule(_parameterId, value));
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the string parameter value
    ///     is greater than the given string (lexicographic comparison).
    /// </summary>
    /// <param name="value">The string value to compare against.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector IsGreaterThan(string value)
    {
#if REVIT2023_OR_GREATER
        return ApplyFilter(ParameterFilterRuleFactory.CreateGreaterRule(_parameterId, value));
#else
        return ApplyFilter(ParameterFilterRuleFactory.CreateGreaterRule(_parameterId, value, true));
#endif
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter value
    ///     is greater than the given integer.
    /// </summary>
    /// <param name="value">The integer value to compare against.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector IsGreaterThan(int value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateGreaterRule(_parameterId, value));
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter value
    ///     is greater than the given double.
    /// </summary>
    /// <remarks>
    ///     Values greater than <paramref name="value" /> but within 1e-9
    ///     are considered equal, not greater.
    /// </remarks>
    /// <param name="value">The double value to compare against.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector IsGreaterThan(double value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateGreaterRule(_parameterId, value, 1e-9));
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter value
    ///     is greater than the given double.
    /// </summary>
    /// <remarks>
    ///     Values greater than <paramref name="value" /> but within <paramref name="epsilon" />
    ///     are considered equal, not greater.
    /// </remarks>
    /// <param name="value">The double value to compare against.</param>
    /// <param name="epsilon">The tolerance within which two values are considered equal.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///     <paramref name="value" /> or <paramref name="epsilon" /> is not finite or not a number.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector IsGreaterThan(double value, double epsilon)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateGreaterRule(_parameterId, value, epsilon));
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter value
    ///     is greater than the given <see cref="Autodesk.Revit.DB.ElementId" />.
    /// </summary>
    /// <param name="value">The <see cref="Autodesk.Revit.DB.ElementId" /> value to compare against.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector IsGreaterThan(ElementId value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateGreaterRule(_parameterId, value));
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the string parameter value
    ///     is greater than or equal to the given string (lexicographic comparison).
    /// </summary>
    /// <param name="value">The string value to compare against.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector IsGreaterThanOrEqualTo(string value)
    {
#if REVIT2023_OR_GREATER
        return ApplyFilter(ParameterFilterRuleFactory.CreateGreaterOrEqualRule(_parameterId, value));
#else
        return ApplyFilter(ParameterFilterRuleFactory.CreateGreaterOrEqualRule(_parameterId, value, true));
#endif
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter value
    ///     is greater than or equal to the given integer.
    /// </summary>
    /// <param name="value">The integer value to compare against.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector IsGreaterThanOrEqualTo(int value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateGreaterOrEqualRule(_parameterId, value));
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter value
    ///     is greater than or equal to the given double.
    /// </summary>
    /// <remarks>
    ///     Values less than <paramref name="value" /> but within <paramref name="epsilon" />
    ///     are considered equal; therefore, such values satisfy the condition.
    /// </remarks>
    /// <param name="value">The double value to compare against.</param>
    /// <param name="epsilon">The tolerance within which two values are considered equal.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///     <paramref name="value" /> or <paramref name="epsilon" /> is not finite or not a number.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector IsGreaterThanOrEqualTo(double value, double epsilon)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateGreaterOrEqualRule(_parameterId, value, epsilon));
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter value
    ///     is greater than or equal to the given <see cref="Autodesk.Revit.DB.ElementId" />.
    /// </summary>
    /// <param name="value">The <see cref="Autodesk.Revit.DB.ElementId" /> value to compare against.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector IsGreaterThanOrEqualTo(ElementId value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateGreaterOrEqualRule(_parameterId, value));
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the string parameter value
    ///     is less than the given string (lexicographic comparison).
    /// </summary>
    /// <param name="value">The string value to compare against.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector IsLessThan(string value)
    {
#if REVIT2023_OR_GREATER
        return ApplyFilter(ParameterFilterRuleFactory.CreateLessRule(_parameterId, value));
#else
        return ApplyFilter(ParameterFilterRuleFactory.CreateLessRule(_parameterId, value, true));
#endif
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter value
    ///     is less than the given integer.
    /// </summary>
    /// <param name="value">The integer value to compare against.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector IsLessThan(int value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateLessRule(_parameterId, value));
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter value
    ///     is less than the given double.
    /// </summary>
    /// <remarks>
    ///     Values less than <paramref name="value" /> but within <paramref name="epsilon" />
    ///     are considered equal, not less.
    /// </remarks>
    /// <param name="value">The double value to compare against.</param>
    /// <param name="epsilon">The tolerance within which two values are considered equal.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///     <paramref name="value" /> or <paramref name="epsilon" /> is not finite or not a number.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector IsLessThan(double value, double epsilon)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateLessRule(_parameterId, value, epsilon));
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter value
    ///     is less than the given <see cref="Autodesk.Revit.DB.ElementId" />.
    /// </summary>
    /// <param name="value">The <see cref="Autodesk.Revit.DB.ElementId" /> value to compare against.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector IsLessThan(ElementId value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateLessRule(_parameterId, value));
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the string parameter value
    ///     is less than or equal to the given string (lexicographic comparison).
    /// </summary>
    /// <param name="value">The string value to compare against.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector IsLessThanOrEqualTo(string value)
    {
#if REVIT2023_OR_GREATER
        return ApplyFilter(ParameterFilterRuleFactory.CreateLessOrEqualRule(_parameterId, value));
#else
        return ApplyFilter(ParameterFilterRuleFactory.CreateLessOrEqualRule(_parameterId, value, true));
#endif
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter value
    ///     is less than or equal to the given integer.
    /// </summary>
    /// <param name="value">The integer value to compare against.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector IsLessThanOrEqualTo(int value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateLessOrEqualRule(_parameterId, value));
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter value
    ///     is less than or equal to the given double.
    /// </summary>
    /// <remarks>
    ///     Values greater than <paramref name="value" /> but within <paramref name="epsilon" />
    ///     are considered equal; therefore, such values satisfy the condition.
    /// </remarks>
    /// <param name="value">The double value to compare against.</param>
    /// <param name="epsilon">The tolerance within which two values are considered equal.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///     <paramref name="value" /> or <paramref name="epsilon" /> is not finite or not a number.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector IsLessThanOrEqualTo(double value, double epsilon)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateLessOrEqualRule(_parameterId, value, epsilon));
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter value
    ///     is less than or equal to the given <see cref="Autodesk.Revit.DB.ElementId" />.
    /// </summary>
    /// <param name="value">The <see cref="Autodesk.Revit.DB.ElementId" /> value to compare against.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector IsLessThanOrEqualTo(ElementId value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateLessOrEqualRule(_parameterId, value));
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the string parameter value
    ///     contains the given string.
    /// </summary>
    /// <param name="value">The string value to search for.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector Contains(string value)
    {
#if REVIT2023_OR_GREATER
        return ApplyFilter(ParameterFilterRuleFactory.CreateContainsRule(_parameterId, value));
#else
        return ApplyFilter(ParameterFilterRuleFactory.CreateContainsRule(_parameterId, value, true));
#endif
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the string parameter value
    ///     does not contain the given string.
    /// </summary>
    /// <param name="value">The string value to search for.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector NotContains(string value)
    {
#if REVIT2023_OR_GREATER
        return ApplyFilter(ParameterFilterRuleFactory.CreateNotContainsRule(_parameterId, value));
#else
        return ApplyFilter(ParameterFilterRuleFactory.CreateNotContainsRule(_parameterId, value, true));
#endif
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the string parameter value
    ///     begins with the given string.
    /// </summary>
    /// <param name="value">The string value to search for.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector StartsWith(string value)
    {
#if REVIT2023_OR_GREATER
        return ApplyFilter(ParameterFilterRuleFactory.CreateBeginsWithRule(_parameterId, value));
#else
        return ApplyFilter(ParameterFilterRuleFactory.CreateBeginsWithRule(_parameterId, value, true));
#endif
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the string parameter value
    ///     does not begin with the given string.
    /// </summary>
    /// <param name="value">The string value to search for.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector NotStartsWith(string value)
    {
#if REVIT2023_OR_GREATER
        return ApplyFilter(ParameterFilterRuleFactory.CreateNotBeginsWithRule(_parameterId, value));
#else
        return ApplyFilter(ParameterFilterRuleFactory.CreateNotBeginsWithRule(_parameterId, value, true));
#endif
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the string parameter value
    ///     ends with the given string.
    /// </summary>
    /// <param name="value">The string value to search for.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector EndsWith(string value)
    {
#if REVIT2023_OR_GREATER
        return ApplyFilter(ParameterFilterRuleFactory.CreateEndsWithRule(_parameterId, value));
#else
        return ApplyFilter(ParameterFilterRuleFactory.CreateEndsWithRule(_parameterId, value, true));
#endif
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the string parameter value
    ///     does not end with the given string.
    /// </summary>
    /// <param name="value">The string value to search for.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector NotEndsWith(string value)
    {
#if REVIT2023_OR_GREATER
        return ApplyFilter(ParameterFilterRuleFactory.CreateNotEndsWithRule(_parameterId, value));
#else
        return ApplyFilter(ParameterFilterRuleFactory.CreateNotEndsWithRule(_parameterId, value, true));
#endif
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter has a value.
    /// </summary>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector HasValue()
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateHasValueParameterRule(_parameterId));
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter does not have a value.
    /// </summary>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector HasNoValue()
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateHasNoValueParameterRule(_parameterId));
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter is associated
    ///     with the given global parameter.
    /// </summary>
    /// <param name="globalParameterId">
    ///     The <see cref="Autodesk.Revit.DB.ElementId" /> of the global parameter to test the association against.
    /// </param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector IsAssociatedWithGlobalParameter(ElementId globalParameterId)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateIsAssociatedWithGlobalParameterRule(_parameterId, globalParameterId));
    }

    /// <summary>
    ///     Applies a filter rule that matches elements where the parameter is not associated
    ///     with the given global parameter.
    /// </summary>
    /// <param name="globalParameterId">
    ///     The <see cref="Autodesk.Revit.DB.ElementId" /> of the global parameter to test the association against.
    /// </param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null.
    /// </exception>
    public FilteredElementCollector IsNotAssociatedWithGlobalParameter(ElementId globalParameterId)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateIsNotAssociatedWithGlobalParameterRule(_parameterId, globalParameterId));
    }

    private FilteredElementCollector ApplyFilter(FilterRule rule)
    {
        return _collector.WherePasses(new ElementParameterFilter(rule));
    }
}

[SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
public static partial class FilteredElementCollectorExtensions
{
    /// <param name="document">The document</param>
    extension(Document document)
    {
        /// <summary></summary>
        [Obsolete("Use document.CollectElements() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetElements()",
            Message = "Use CollectElements() instead",
            ReplaceTemplate = "$document$.CollectElements()",
            ReplaceMessage = "Replace with CollectElements()")]
        [Pure]
        public FilteredElementCollector GetElements()
        {
            return new FilteredElementCollector(document);
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetElements($viewId$)",
            Message = "Use CollectElements(viewId) instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$)",
            ReplaceMessage = "Replace with CollectElements(viewId)")]
        [Pure]
        public FilteredElementCollector GetElements(ElementId viewId)
        {
            return new FilteredElementCollector(document, viewId);
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(elementIds) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetElements($elementIds$)",
            Message = "Use CollectElements(elementIds) instead",
            ReplaceTemplate = "$document$.CollectElements($elementIds$)",
            ReplaceMessage = "Replace with CollectElements(elementIds)")]
        [Pure]
        public FilteredElementCollector GetElements(ICollection<ElementId> elementIds)
        {
            return new FilteredElementCollector(document, elementIds);
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Instances().OfCategory(category).ToElements() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetInstances($category$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Instances().OfCategory($category$).ToElements()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IList<Element> GetInstances(BuiltInCategory category)
        {
            return CollectInstances(document, category).ToElements();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Instances().OfCategory(category).WherePasses(filter).ToElements() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetInstances($category$, $filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Instances().OfCategory($category$).WherePasses($filter$).ToElements()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IList<Element> GetInstances(BuiltInCategory category, ElementFilter filter)
        {
            return CollectInstances(document, category, filter).ToElements();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Instances().OfCategory(category).ToElements() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetInstances($category$, $filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Instances().OfCategory($category$).ToElements()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IList<Element> GetInstances(BuiltInCategory category, IEnumerable<ElementFilter> filters)
        {
            return CollectInstances(document, category, filters).ToElements();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Instances().ToElements() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetInstances()",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Instances().ToElements()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IList<Element> GetInstances()
        {
            return CollectInstances(document).ToElements();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Instances().WherePasses(filter).ToElements() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetInstances($filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Instances().WherePasses($filter$).ToElements()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IList<Element> GetInstances(ElementFilter filter)
        {
            return CollectInstances(document, filter).ToElements();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Instances().ToElements() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetInstances($filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Instances().ToElements()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IList<Element> GetInstances(IEnumerable<ElementFilter> filters)
        {
            return CollectInstances(document, filters).ToElements();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Instances().OfCategory(category) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstances($category$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Instances().OfCategory($category$)",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<Element> EnumerateInstances(BuiltInCategory category)
        {
            return CollectInstances(document, category);
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Instances().OfCategory(category).WherePasses(filter) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstances($category$, $filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Instances().OfCategory($category$).WherePasses($filter$)",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<Element> EnumerateInstances(BuiltInCategory category, ElementFilter filter)
        {
            return CollectInstances(document, category, filter);
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Instances().OfCategory(category) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstances($category$, $filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Instances().OfCategory($category$)",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<Element> EnumerateInstances(BuiltInCategory category, IEnumerable<ElementFilter> filters)
        {
            return CollectInstances(document, category, filters);
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Instances() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstances()",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Instances()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<Element> EnumerateInstances()
        {
            return CollectInstances(document);
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Instances().WherePasses(filter) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstances($filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Instances().WherePasses($filter$)",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<Element> EnumerateInstances(ElementFilter filter)
        {
            return CollectInstances(document, filter);
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Instances() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstances($filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Instances()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<Element> EnumerateInstances(IEnumerable<ElementFilter> filters)
        {
            return CollectInstances(document, filters);
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Instances().OfCategory(category) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstances<$T$>($category$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().OfClass<$T$>().Instances().OfCategory($category$)",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<T> EnumerateInstances<T>(BuiltInCategory category) where T : Element
        {
            var elements = CollectInstances(document, category).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var instance = (T)element;
                yield return instance;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Instances().OfCategory(category).WherePasses(filter) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstances<$T$>($category$, $filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().OfClass<$T$>().Instances().OfCategory($category$).WherePasses($filter$)",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<T> EnumerateInstances<T>(BuiltInCategory category, ElementFilter filter) where T : Element
        {
            var elements = CollectInstances(document, category, filter).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var instance = (T)element;
                yield return instance;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Instances().OfCategory(category) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstances<$T$>($category$, $filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().OfClass<$T$>().Instances().OfCategory($category$)",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<T> EnumerateInstances<T>(BuiltInCategory category, IEnumerable<ElementFilter> filters) where T : Element
        {
            var elements = CollectInstances(document, category, filters).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var instance = (T)element;
                yield return instance;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Instances() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstances<$T$>()",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().OfClass<$T$>().Instances()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<T> EnumerateInstances<T>() where T : Element
        {
            var elements = CollectInstances(document).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var instance = (T)element;
                yield return instance;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Instances().WherePasses(filter) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstances<$T$>($filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().OfClass<$T$>().Instances().WherePasses($filter$)",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<T> EnumerateInstances<T>(ElementFilter filter) where T : Element
        {
            var elements = CollectInstances(document, filter).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var instance = (T)element;
                yield return instance;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Instances() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstances<$T$>($filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().OfClass<$T$>().Instances()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<T> EnumerateInstances<T>(IEnumerable<ElementFilter> filters) where T : Element
        {
            var elements = CollectInstances(document, filters).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var instance = (T)element;
                yield return instance;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Instances().OfCategory(category).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetInstanceIds($category$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Instances().OfCategory($category$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public ICollection<ElementId> GetInstanceIds(BuiltInCategory category)
        {
            return CollectInstances(document, category).ToElementIds();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Instances().OfCategory(category).WherePasses(filter).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetInstanceIds($category$, $filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Instances().OfCategory($category$).WherePasses($filter$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public ICollection<ElementId> GetInstanceIds(BuiltInCategory category, ElementFilter filter)
        {
            return CollectInstances(document, category, filter).ToElementIds();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Instances().OfCategory(category).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetInstanceIds($category$, $filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Instances().OfCategory($category$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public ICollection<ElementId> GetInstanceIds(BuiltInCategory category, IEnumerable<ElementFilter> filters)
        {
            return CollectInstances(document, category, filters).ToElementIds();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Instances().ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetInstanceIds()",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Instances().ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public ICollection<ElementId> GetInstanceIds()
        {
            return CollectInstances(document).ToElementIds();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Instances().WherePasses(filter).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetInstanceIds($filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Instances().WherePasses($filter$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public ICollection<ElementId> GetInstanceIds(ElementFilter filter)
        {
            return CollectInstances(document, filter).ToElementIds();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Instances().ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetInstanceIds($filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Instances().ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public ICollection<ElementId> GetInstanceIds(IEnumerable<ElementFilter> filters)
        {
            return CollectInstances(document, filters).ToElementIds();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Instances().OfCategory(category).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstanceIds($category$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Instances().OfCategory($category$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateInstanceIds(BuiltInCategory category)
        {
            foreach (var element in CollectInstances(document, category)) yield return element.Id;
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Instances().OfCategory(category).WherePasses(filter).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstanceIds($category$, $filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Instances().OfCategory($category$).WherePasses($filter$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateInstanceIds(BuiltInCategory category, ElementFilter filter)
        {
            foreach (var element in CollectInstances(document, category, filter)) yield return element.Id;
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Instances().OfCategory(category).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstanceIds($category$, $filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Instances().OfCategory($category$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateInstanceIds(BuiltInCategory category, IEnumerable<ElementFilter> filters)
        {
            foreach (var element in CollectInstances(document, category, filters)) yield return element.Id;
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Instances().ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstanceIds()",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Instances().ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateInstanceIds()
        {
            foreach (var element in CollectInstances(document)) yield return element.Id;
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Instances().WherePasses(filter).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstanceIds($filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Instances().WherePasses($filter$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateInstanceIds(ElementFilter filter)
        {
            foreach (var element in CollectInstances(document, filter)) yield return element.Id;
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Instances().ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstanceIds($filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Instances().ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateInstanceIds(IEnumerable<ElementFilter> filters)
        {
            foreach (var element in CollectInstances(document, filters)) yield return element.Id;
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Instances().OfCategory(category).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstanceIds<$T$>($category$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().OfClass<$T$>().Instances().OfCategory($category$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateInstanceIds<T>(BuiltInCategory category) where T : Element
        {
            var elements = CollectInstances(document, category).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var instance = (T)element;
                yield return instance.Id;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Instances().OfCategory(category).WherePasses(filter).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstanceIds<$T$>($category$, $filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().OfClass<$T$>().Instances().OfCategory($category$).WherePasses($filter$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateInstanceIds<T>(BuiltInCategory category, ElementFilter filter) where T : Element
        {
            var elements = CollectInstances(document, category, filter).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var instance = (T)element;
                yield return instance.Id;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Instances().OfCategory(category).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstanceIds<$T$>($category$, $filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().OfClass<$T$>().Instances().OfCategory($category$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateInstanceIds<T>(BuiltInCategory category, IEnumerable<ElementFilter> filters) where T : Element
        {
            var elements = CollectInstances(document, category, filters).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var instance = (T)element;
                yield return instance.Id;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Instances().ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstanceIds<$T$>()",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().OfClass<$T$>().Instances().ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateInstanceIds<T>() where T : Element
        {
            var elements = CollectInstances(document).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var instance = (T)element;
                yield return instance.Id;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Instances().WherePasses(filter).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstanceIds<$T$>($filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().OfClass<$T$>().Instances().WherePasses($filter$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateInstanceIds<T>(ElementFilter filter) where T : Element
        {
            var elements = CollectInstances(document, filter).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var instance = (T)element;
                yield return instance.Id;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Instances().ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstanceIds<$T$>($filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().OfClass<$T$>().Instances().ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateInstanceIds<T>(IEnumerable<ElementFilter> filters) where T : Element
        {
            var elements = CollectInstances(document, filters).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var instance = (T)element;
                yield return instance.Id;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).Instances().OfCategory(category).ToElements() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetInstances($viewId$, $category$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).Instances().OfCategory($category$).ToElements()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IList<Element> GetInstances(ElementId viewId, BuiltInCategory category)
        {
            return CollectInstances(document, viewId, category).ToElements();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).Instances().OfCategory(category).WherePasses(filter).ToElements() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetInstances($viewId$, $category$, $filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).Instances().OfCategory($category$).WherePasses($filter$).ToElements()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IList<Element> GetInstances(ElementId viewId, BuiltInCategory category, ElementFilter filter)
        {
            return CollectInstances(document, viewId, category, filter).ToElements();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).Instances().OfCategory(category).ToElements() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetInstances($viewId$, $category$, $filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).Instances().OfCategory($category$).ToElements()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IList<Element> GetInstances(ElementId viewId, BuiltInCategory category, IEnumerable<ElementFilter> filters)
        {
            return CollectInstances(document, viewId, category, filters).ToElements();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).Instances().ToElements() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetInstances($viewId$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).Instances().ToElements()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IList<Element> GetInstances(ElementId viewId)
        {
            return CollectInstances(document, viewId).ToElements();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).Instances().WherePasses(filter).ToElements() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetInstances($viewId$, $filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).Instances().WherePasses($filter$).ToElements()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IList<Element> GetInstances(ElementId viewId, ElementFilter filter)
        {
            return CollectInstances(document, viewId, filter).ToElements();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).Instances().ToElements() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetInstances($viewId$, $filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).Instances().ToElements()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IList<Element> GetInstances(ElementId viewId, IEnumerable<ElementFilter> filters)
        {
            return CollectInstances(document, viewId, filters).ToElements();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).Instances().OfCategory(category) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstances($viewId$, $category$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).Instances().OfCategory($category$)",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<Element> EnumerateInstances(ElementId viewId, BuiltInCategory category)
        {
            return CollectInstances(document, viewId, category);
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).Instances().OfCategory(category).WherePasses(filter) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstances($viewId$, $category$, $filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).Instances().OfCategory($category$).WherePasses($filter$)",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<Element> EnumerateInstances(ElementId viewId, BuiltInCategory category, ElementFilter filter)
        {
            return CollectInstances(document, viewId, category, filter);
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).Instances().OfCategory(category) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstances($viewId$, $category$, $filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).Instances().OfCategory($category$)",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<Element> EnumerateInstances(ElementId viewId, BuiltInCategory category, IEnumerable<ElementFilter> filters)
        {
            return CollectInstances(document, viewId, category, filters);
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).Instances() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstances($viewId$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).Instances()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<Element> EnumerateInstances(ElementId viewId)
        {
            return CollectInstances(document, viewId);
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).Instances().WherePasses(filter) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstances($viewId$, $filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).Instances().WherePasses($filter$)",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<Element> EnumerateInstances(ElementId viewId, ElementFilter filter)
        {
            return CollectInstances(document, viewId, filter);
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).Instances() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstances($viewId$, $filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).Instances()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<Element> EnumerateInstances(ElementId viewId, IEnumerable<ElementFilter> filters)
        {
            return CollectInstances(document, viewId, filters);
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Instances().OfCategory(category) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstances<$T$>($viewId$, $category$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).OfClass<$T$>().Instances().OfCategory($category$)",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<T> EnumerateInstances<T>(ElementId viewId, BuiltInCategory category) where T : Element
        {
            var elements = CollectInstances(document, viewId, category).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var instance = (T)element;
                yield return instance;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).OfClass<T>().Instances().OfCategory(category).WherePasses(filter) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstances<$T$>($viewId$, $category$, $filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).OfClass<$T$>().Instances().OfCategory($category$).WherePasses($filter$)",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<T> EnumerateInstances<T>(ElementId viewId, BuiltInCategory category, ElementFilter filter) where T : Element
        {
            var elements = CollectInstances(document, viewId, category, filter).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var instance = (T)element;
                yield return instance;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).OfClass<T>().Instances().OfCategory(category) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstances<$T$>($viewId$, $category$, $filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).OfClass<$T$>().Instances().OfCategory($category$)",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<T> EnumerateInstances<T>(ElementId viewId, BuiltInCategory category, IEnumerable<ElementFilter> filters) where T : Element
        {
            var elements = CollectInstances(document, viewId, category, filters).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var instance = (T)element;
                yield return instance;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).OfClass<T>().Instances() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstances<$T$>($viewId$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).OfClass<$T$>().Instances()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<T> EnumerateInstances<T>(ElementId viewId) where T : Element
        {
            var elements = CollectInstances(document, viewId).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var instance = (T)element;
                yield return instance;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).OfClass<T>().Instances().WherePasses(filter) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstances<$T$>($viewId$, $filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).OfClass<$T$>().Instances().WherePasses($filter$)",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<T> EnumerateInstances<T>(ElementId viewId, ElementFilter filter) where T : Element
        {
            var elements = CollectInstances(document, viewId, filter).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var instance = (T)element;
                yield return instance;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).OfClass<T>().Instances() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstances<$T$>($viewId$, $filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).OfClass<$T$>().Instances()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<T> EnumerateInstances<T>(ElementId viewId, IEnumerable<ElementFilter> filters) where T : Element
        {
            var elements = CollectInstances(document, viewId, filters).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var instance = (T)element;
                yield return instance;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).Instances().OfCategory(category).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetInstanceIds($viewId$, $category$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).Instances().OfCategory($category$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public ICollection<ElementId> GetInstanceIds(ElementId viewId, BuiltInCategory category)
        {
            return CollectInstances(document, viewId, category).ToElementIds();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).Instances().OfCategory(category).WherePasses(filter).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetInstanceIds($viewId$, $category$, $filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).Instances().OfCategory($category$).WherePasses($filter$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public ICollection<ElementId> GetInstanceIds(ElementId viewId, BuiltInCategory category, ElementFilter filter)
        {
            return CollectInstances(document, viewId, category, filter).ToElementIds();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).Instances().OfCategory(category).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetInstanceIds($viewId$, $category$, $filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).Instances().OfCategory($category$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public ICollection<ElementId> GetInstanceIds(ElementId viewId, BuiltInCategory category, IEnumerable<ElementFilter> filters)
        {
            return CollectInstances(document, viewId, category, filters).ToElementIds();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).Instances().ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetInstanceIds($viewId$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).Instances().ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public ICollection<ElementId> GetInstanceIds(ElementId viewId)
        {
            return CollectInstances(document, viewId).ToElementIds();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).Instances().WherePasses(filter).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetInstanceIds($viewId$, $filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).Instances().WherePasses($filter$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public ICollection<ElementId> GetInstanceIds(ElementId viewId, ElementFilter filter)
        {
            return CollectInstances(document, viewId, filter).ToElementIds();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).Instances().ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetInstanceIds($viewId$, $filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).Instances().ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public ICollection<ElementId> GetInstanceIds(ElementId viewId, IEnumerable<ElementFilter> filters)
        {
            return CollectInstances(document, viewId, filters).ToElementIds();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).Instances().OfCategory(category).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstanceIds($viewId$, $category$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).Instances().OfCategory($category$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateInstanceIds(ElementId viewId, BuiltInCategory category)
        {
            foreach (var element in CollectInstances(document, viewId, category)) yield return element.Id;
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).Instances().OfCategory(category).WherePasses(filter).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstanceIds($viewId$, $category$, $filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).Instances().OfCategory($category$).WherePasses($filter$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateInstanceIds(ElementId viewId, BuiltInCategory category, ElementFilter filter)
        {
            foreach (var element in CollectInstances(document, viewId, category, filter)) yield return element.Id;
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).Instances().OfCategory(category).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstanceIds($viewId$, $category$, $filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).Instances().OfCategory($category$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateInstanceIds(ElementId viewId, BuiltInCategory category, IEnumerable<ElementFilter> filters)
        {
            foreach (var element in CollectInstances(document, viewId, category, filters)) yield return element.Id;
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).Instances().ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstanceIds($viewId$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).Instances().ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateInstanceIds(ElementId viewId)
        {
            foreach (var element in CollectInstances(document, viewId)) yield return element.Id;
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).Instances().WherePasses(filter).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstanceIds($viewId$, $filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).Instances().WherePasses($filter$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateInstanceIds(ElementId viewId, ElementFilter filter)
        {
            foreach (var element in CollectInstances(document, viewId, filter)) yield return element.Id;
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).Instances().ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstanceIds($viewId$, $filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).Instances().ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateInstanceIds(ElementId viewId, IEnumerable<ElementFilter> filters)
        {
            foreach (var element in CollectInstances(document, viewId, filters)) yield return element.Id;
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Instances().OfCategory(category).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstanceIds<$T$>($viewId$, $category$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).OfClass<$T$>().Instances().OfCategory($category$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateInstanceIds<T>(ElementId viewId, BuiltInCategory category) where T : Element
        {
            var elements = CollectInstances(document, viewId, category).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var instance = (T)element;
                yield return instance.Id;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).OfClass<T>().Instances().OfCategory(category).WherePasses(filter).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstanceIds<$T$>($viewId$, $category$, $filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).OfClass<$T$>().Instances().OfCategory($category$).WherePasses($filter$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateInstanceIds<T>(ElementId viewId, BuiltInCategory category, ElementFilter filter) where T : Element
        {
            var elements = CollectInstances(document, viewId, category, filter).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var instance = (T)element;
                yield return instance.Id;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).OfClass<T>().Instances().OfCategory(category).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstanceIds<$T$>($viewId$, $category$, $filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).OfClass<$T$>().Instances().OfCategory($category$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateInstanceIds<T>(ElementId viewId, BuiltInCategory category, IEnumerable<ElementFilter> filters)
            where T : Element
        {
            var elements = CollectInstances(document, viewId, category, filters).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var instance = (T)element;
                yield return instance.Id;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).OfClass<T>().Instances().ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstanceIds<$T$>($viewId$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).OfClass<$T$>().Instances().ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateInstanceIds<T>(ElementId viewId) where T : Element
        {
            var elements = CollectInstances(document, viewId).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var instance = (T)element;
                yield return instance.Id;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).OfClass<T>().Instances().WherePasses(filter).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstanceIds<$T$>($viewId$, $filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).OfClass<$T$>().Instances().WherePasses($filter$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateInstanceIds<T>(ElementId viewId, ElementFilter filter) where T : Element
        {
            var elements = CollectInstances(document, viewId, filter).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var instance = (T)element;
                yield return instance.Id;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements(viewId).OfClass<T>().Instances().ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateInstanceIds<$T$>($viewId$, $filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements($viewId$).OfClass<$T$>().Instances().ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateInstanceIds<T>(ElementId viewId, IEnumerable<ElementFilter> filters) where T : Element
        {
            var elements = CollectInstances(document, viewId, filters).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var instance = (T)element;
                yield return instance.Id;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Types().OfCategory(category).ToElements() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetTypes($category$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Types().OfCategory($category$).ToElements()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IList<Element> GetTypes(BuiltInCategory category)
        {
            return CollectTypes(document, category).ToElements();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Types().OfCategory(category).WherePasses(filter).ToElements() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetTypes($category$, $filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Types().OfCategory($category$).WherePasses($filter$).ToElements()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IList<Element> GetTypes(BuiltInCategory category, ElementFilter filter)
        {
            return CollectTypes(document, category, filter).ToElements();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Types().OfCategory(category).ToElements() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetTypes($category$, $filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Types().OfCategory($category$).ToElements()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IList<Element> GetTypes(BuiltInCategory category, IEnumerable<ElementFilter> filters)
        {
            return CollectTypes(document, category, filters).ToElements();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Types().ToElements() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetTypes()",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Types().ToElements()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IList<Element> GetTypes()
        {
            return CollectTypes(document).ToElements();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Types().WherePasses(filter).ToElements() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetTypes($filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Types().WherePasses($filter$).ToElements()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IList<Element> GetTypes(ElementFilter filter)
        {
            return CollectTypes(document, filter).ToElements();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Types().ToElements() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetTypes($filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Types().ToElements()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IList<Element> GetTypes(IEnumerable<ElementFilter> filters)
        {
            return CollectTypes(document, filters).ToElements();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Types().OfCategory(category) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateTypes($category$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Types().OfCategory($category$)",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<Element> EnumerateTypes(BuiltInCategory category)
        {
            return CollectTypes(document, category);
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Types().OfCategory(category).WherePasses(filter) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateTypes($category$, $filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Types().OfCategory($category$).WherePasses($filter$)",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<Element> EnumerateTypes(BuiltInCategory category, ElementFilter filter)
        {
            return CollectTypes(document, category, filter);
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Types().OfCategory(category) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateTypes($category$, $filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Types().OfCategory($category$)",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<Element> EnumerateTypes(BuiltInCategory category, IEnumerable<ElementFilter> filters)
        {
            return CollectTypes(document, category, filters);
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Types() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateTypes()",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Types()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<Element> EnumerateTypes()
        {
            return CollectTypes(document);
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Types().WherePasses(filter) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateTypes($filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Types().WherePasses($filter$)",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<Element> EnumerateTypes(ElementFilter filter)
        {
            return CollectTypes(document, filter);
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Types() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateTypes($filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Types()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<Element> EnumerateTypes(IEnumerable<ElementFilter> filters)
        {
            return CollectTypes(document, filters);
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Types().OfCategory(category) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateTypes<$T$>($category$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().OfClass<$T$>().Types().OfCategory($category$)",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<T> EnumerateTypes<T>(BuiltInCategory category) where T : Element
        {
            var elements = CollectTypes(document, category).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var type = (T)element;
                yield return type;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Types().OfCategory(category).WherePasses(filter) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateTypes<$T$>($category$, $filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().OfClass<$T$>().Types().OfCategory($category$).WherePasses($filter$)",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<T> EnumerateTypes<T>(BuiltInCategory category, ElementFilter filter) where T : Element
        {
            var elements = CollectTypes(document, category, filter).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var type = (T)element;
                yield return type;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Types().OfCategory(category) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateTypes<$T$>($category$, $filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().OfClass<$T$>().Types().OfCategory($category$)",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<T> EnumerateTypes<T>(BuiltInCategory category, IEnumerable<ElementFilter> filters) where T : Element
        {
            var elements = CollectTypes(document, category, filters).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var type = (T)element;
                yield return type;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Types() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateTypes<$T$>()",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().OfClass<$T$>().Types()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<T> EnumerateTypes<T>() where T : Element
        {
            var elements = CollectTypes(document).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var type = (T)element;
                yield return type;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Types().WherePasses(filter) instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateTypes<$T$>($filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().OfClass<$T$>().Types().WherePasses($filter$)",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<T> EnumerateTypes<T>(ElementFilter filter) where T : Element
        {
            var elements = CollectTypes(document, filter).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var type = (T)element;
                yield return type;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Types() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateTypes<$T$>($filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().OfClass<$T$>().Types()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<T> EnumerateTypes<T>(IEnumerable<ElementFilter> filters) where T : Element
        {
            var elements = CollectTypes(document, filters).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var type = (T)element;
                yield return type;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Types().OfCategory(category).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetTypeIds($category$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Types().OfCategory($category$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public ICollection<ElementId> GetTypeIds(BuiltInCategory category)
        {
            return CollectTypes(document, category).ToElementIds();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Types().OfCategory(category).WherePasses(filter).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetTypeIds($category$, $filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Types().OfCategory($category$).WherePasses($filter$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public ICollection<ElementId> GetTypeIds(BuiltInCategory category, ElementFilter filter)
        {
            return CollectTypes(document, category, filter).ToElementIds();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Types().OfCategory(category).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetTypeIds($category$, $filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Types().OfCategory($category$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public ICollection<ElementId> GetTypeIds(BuiltInCategory category, IEnumerable<ElementFilter> filters)
        {
            return CollectTypes(document, category, filters).ToElementIds();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Types().ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetTypeIds()",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Types().ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public ICollection<ElementId> GetTypeIds()
        {
            return CollectTypes(document).ToElementIds();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Types().WherePasses(filter).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetTypeIds($filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Types().WherePasses($filter$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public ICollection<ElementId> GetTypeIds(ElementFilter filter)
        {
            return CollectTypes(document, filter).ToElementIds();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Types().ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.GetTypeIds($filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Types().ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public ICollection<ElementId> GetTypeIds(IEnumerable<ElementFilter> filters)
        {
            return CollectTypes(document, filters).ToElementIds();
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Types().OfCategory(category).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateTypeIds($category$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Types().OfCategory($category$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateTypeIds(BuiltInCategory category)
        {
            foreach (var element in CollectTypes(document, category)) yield return element.Id;
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Types().OfCategory(category).WherePasses(filter).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateTypeIds($category$, $filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Types().OfCategory($category$).WherePasses($filter$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateTypeIds(BuiltInCategory category, ElementFilter filter)
        {
            foreach (var element in CollectTypes(document, category, filter)) yield return element.Id;
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Types().OfCategory(category).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateTypeIds($category$, $filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Types().OfCategory($category$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateTypeIds(BuiltInCategory category, IEnumerable<ElementFilter> filters)
        {
            foreach (var element in CollectTypes(document, category, filters)) yield return element.Id;
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Types().ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateTypeIds()",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Types().ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateTypeIds()
        {
            foreach (var element in CollectTypes(document)) yield return element.Id;
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Types().WherePasses(filter).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateTypeIds($filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Types().WherePasses($filter$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateTypeIds(ElementFilter filter)
        {
            foreach (var element in CollectTypes(document, filter)) yield return element.Id;
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().Types().ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateTypeIds($filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().Types().ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateTypeIds(IEnumerable<ElementFilter> filters)
        {
            foreach (var element in CollectTypes(document, filters)) yield return element.Id;
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Types().OfCategory(category).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateTypeIds<$T$>($category$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().OfClass<$T$>().Types().OfCategory($category$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateTypeIds<T>(BuiltInCategory category) where T : Element
        {
            var elements = CollectTypes(document, category).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var type = (T)element;
                yield return type.Id;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Types().OfCategory(category).WherePasses(filter).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateTypeIds<$T$>($category$, $filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().OfClass<$T$>().Types().OfCategory($category$).WherePasses($filter$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateTypeIds<T>(BuiltInCategory category, ElementFilter filter) where T : Element
        {
            var elements = CollectTypes(document, category, filter).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var type = (T)element;
                yield return type.Id;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Types().OfCategory(category).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateTypeIds<$T$>($category$, $filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().OfClass<$T$>().Types().OfCategory($category$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateTypeIds<T>(BuiltInCategory category, IEnumerable<ElementFilter> filters) where T : Element
        {
            var elements = CollectTypes(document, category, filters).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var type = (T)element;
                yield return type.Id;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Types().ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateTypeIds<$T$>()",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().OfClass<$T$>().Types().ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateTypeIds<T>() where T : Element
        {
            var elements = CollectTypes(document).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var type = (T)element;
                yield return type.Id;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Types().WherePasses(filter).ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateTypeIds<$T$>($filter$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().OfClass<$T$>().Types().WherePasses($filter$).ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateTypeIds<T>(ElementFilter filter) where T : Element
        {
            var elements = CollectTypes(document, filter).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var type = (T)element;
                yield return type.Id;
            }
        }

        /// <summary></summary>
        [Obsolete("Use document.CollectElements().OfClass<T>().Types().ToElementIds() instead")]
        [CodeTemplate(
            searchTemplate: "$document$.EnumerateTypeIds<$T$>($filters$)",
            Message = "Use CollectElements fluent API instead",
            ReplaceTemplate = "$document$.CollectElements().OfClass<$T$>().Types().ToElementIds()",
            ReplaceMessage = "Replace with CollectElements fluent API")]
        [Pure]
        public IEnumerable<ElementId> EnumerateTypeIds<T>(IEnumerable<ElementFilter> filters) where T : Element
        {
            var elements = CollectTypes(document, filters).OfClass(typeof(T));
            foreach (var element in elements)
            {
                var type = (T)element;
                yield return type.Id;
            }
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