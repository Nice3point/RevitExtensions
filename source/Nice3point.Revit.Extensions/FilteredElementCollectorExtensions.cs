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
        /// <inheritdoc cref="Autodesk.Revit.DB.FilteredElementCollector.OfClass(System.Type)"/>
        [Pure]
        public FilteredElementCollector OfClass<T>() where T : Element
        {
            return collector.OfClass(typeof(T));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementMulticlassFilter(System.Collections.Generic.IList{System.Type})"/>
        [Pure]
        public FilteredElementCollector OfClasses(IList<Type> types)
        {
            return collector.WherePasses(new ElementMulticlassFilter(types));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementMulticlassFilter(System.Collections.Generic.IList{System.Type})"/>
        [Pure]
        public FilteredElementCollector OfClasses(params Type[] types)
        {
            return collector.WherePasses(new ElementMulticlassFilter(types));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementClassFilter(System.Type,System.Boolean)"/>
        [Pure]
        public FilteredElementCollector ExcludingClass<T>() where T : Element
        {
            return collector.WherePasses(new ElementClassFilter(typeof(T), inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementMulticlassFilter(System.Collections.Generic.IList{System.Type},System.Boolean)"/>
        [Pure]
        public FilteredElementCollector ExcludingClasses(IList<Type> types)
        {
            return collector.WherePasses(new ElementMulticlassFilter(types, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementMulticlassFilter(System.Collections.Generic.IList{System.Type},System.Boolean)"/>
        [Pure]
        public FilteredElementCollector ExcludingClasses(params Type[] types)
        {
            return collector.WherePasses(new ElementMulticlassFilter(types, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementMulticategoryFilter(System.Collections.Generic.ICollection{Autodesk.Revit.DB.BuiltInCategory})"/>
        [Pure]
        public FilteredElementCollector OfCategories(ICollection<BuiltInCategory> categories)
        {
            return collector.WherePasses(new ElementMulticategoryFilter(categories));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementMulticategoryFilter(System.Collections.Generic.ICollection{Autodesk.Revit.DB.BuiltInCategory})"/>
        [Pure]
        public FilteredElementCollector OfCategories(params BuiltInCategory[] categories)
        {
            return collector.WherePasses(new ElementMulticategoryFilter(categories));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementCategoryFilter(Autodesk.Revit.DB.BuiltInCategory,System.Boolean)"/>
        [Pure]
        public FilteredElementCollector ExcludingCategory(BuiltInCategory category)
        {
            return collector.WherePasses(new ElementCategoryFilter(category, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementMulticategoryFilter(System.Collections.Generic.ICollection{Autodesk.Revit.DB.BuiltInCategory},System.Boolean)"/>
        [Pure]
        public FilteredElementCollector ExcludingCategories(ICollection<BuiltInCategory> categories)
        {
            return collector.WherePasses(new ElementMulticategoryFilter(categories, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementMulticategoryFilter(System.Collections.Generic.ICollection{Autodesk.Revit.DB.BuiltInCategory},System.Boolean)"/>
        [Pure]
        public FilteredElementCollector ExcludingCategories(params BuiltInCategory[] categories)
        {
            return collector.WherePasses(new ElementMulticategoryFilter(categories, inverted: true));
        }
#if REVIT2021_OR_GREATER
        /// <inheritdoc cref="Autodesk.Revit.DB.ElementIdSetFilter(System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId})"/>
        [Pure]
        public FilteredElementCollector OfElements(ICollection<ElementId> ids)
        {
            return collector.WherePasses(new ElementIdSetFilter(ids));
        }
#endif

        /// <inheritdoc cref="Autodesk.Revit.DB.CurveElementFilter(Autodesk.Revit.DB.CurveElementType)"/>
        [Pure]
        public FilteredElementCollector OfCurveElementType(CurveElementType curveElementType)
        {
            return collector.WherePasses(new CurveElementFilter(curveElementType));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.CurveElementFilter(Autodesk.Revit.DB.CurveElementType,System.Boolean)"/>
        [Pure]
        public FilteredElementCollector ExcludingCurveElementType(CurveElementType curveElementType)
        {
            return collector.WherePasses(new CurveElementFilter(curveElementType, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementStructuralTypeFilter(Autodesk.Revit.DB.Structure.StructuralType)"/>
        [Pure]
        public FilteredElementCollector OfStructuralType(StructuralType structuralType)
        {
            return collector.WherePasses(new ElementStructuralTypeFilter(structuralType));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementStructuralTypeFilter(Autodesk.Revit.DB.Structure.StructuralType,System.Boolean)"/>
        [Pure]
        public FilteredElementCollector ExcludingStructuralType(StructuralType structuralType)
        {
            return collector.WherePasses(new ElementStructuralTypeFilter(structuralType, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.FilteredElementCollector.WhereElementIsElementType"/>
        [Pure]
        public FilteredElementCollector Types()
        {
            return collector.WhereElementIsElementType();
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.FilteredElementCollector.WhereElementIsNotElementType"/>
        [Pure]
        public FilteredElementCollector Instances()
        {
            return collector.WhereElementIsNotElementType();
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Architecture.RoomFilter"/>
        [Pure]
        public FilteredElementCollector Rooms()
        {
            return collector.WherePasses(new RoomFilter());
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Architecture.RoomTagFilter"/>
        [Pure]
        public FilteredElementCollector RoomTags()
        {
            return collector.WherePasses(new RoomTagFilter());
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AreaFilter"/>
        [Pure]
        public FilteredElementCollector Areas()
        {
            return collector.WherePasses(new AreaFilter());
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AreaTagFilter"/>
        [Pure]
        public FilteredElementCollector AreaTags()
        {
            return collector.WherePasses(new AreaTagFilter());
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Mechanical.SpaceFilter"/>
        [Pure]
        public FilteredElementCollector Spaces()
        {
            return collector.WherePasses(new SpaceFilter());
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Mechanical.SpaceTagFilter"/>
        [Pure]
        public FilteredElementCollector SpaceTags()
        {
            return collector.WherePasses(new SpaceTagFilter());
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.FamilySymbolFilter(Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public FilteredElementCollector FamilySymbols(ElementId familyId)
        {
            return collector.WherePasses(new FamilySymbolFilter(familyId));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.FamilySymbolFilter(Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public FilteredElementCollector FamilySymbols(Family family)
        {
            return collector.WherePasses(new FamilySymbolFilter(family.Id));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.FamilyInstanceFilter(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public FilteredElementCollector FamilyInstances(Document document, ElementId symbolId)
        {
            return collector.WherePasses(new FamilyInstanceFilter(document, symbolId));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.FamilyInstanceFilter(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public FilteredElementCollector FamilyInstances(FamilySymbol symbol)
        {
            return collector.WherePasses(new FamilyInstanceFilter(symbol.Document, symbol.Id));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.FilteredElementCollector.WhereElementIsCurveDriven"/>
        [Pure]
        public FilteredElementCollector IsCurveDriven()
        {
            return collector.WhereElementIsCurveDriven();
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.FilteredElementCollector.WhereElementIsViewIndependent"/>
        [Pure]
        public FilteredElementCollector IsViewIndependent()
        {
            return collector.WhereElementIsViewIndependent();
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.PrimaryDesignOptionMemberFilter()"/>
        [Pure]
        public FilteredElementCollector IsPrimaryDesignOptionMember()
        {
            return collector.WherePasses(new PrimaryDesignOptionMemberFilter());
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.PrimaryDesignOptionMemberFilter(System.Boolean)"/>
        [Pure]
        public FilteredElementCollector IsNotPrimaryDesignOptionMember()
        {
            return collector.WherePasses(new PrimaryDesignOptionMemberFilter(inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementOwnerViewFilter(Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public FilteredElementCollector OwnedByView(ElementId viewId)
        {
            return collector.WherePasses(new ElementOwnerViewFilter(viewId));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementOwnerViewFilter(Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public FilteredElementCollector OwnedByView(View view)
        {
            return collector.WherePasses(new ElementOwnerViewFilter(view.Id));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementOwnerViewFilter(Autodesk.Revit.DB.ElementId,System.Boolean)"/>
        [Pure]
        public FilteredElementCollector NotOwnedByView(ElementId viewId)
        {
            return collector.WherePasses(new ElementOwnerViewFilter(viewId, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementOwnerViewFilter(Autodesk.Revit.DB.ElementId,System.Boolean)"/>
        [Pure]
        public FilteredElementCollector NotOwnedByView(View view)
        {
            return collector.WherePasses(new ElementOwnerViewFilter(view.Id, inverted: true));
        }
#if REVIT2021_OR_GREATER
        /// <inheritdoc cref="Autodesk.Revit.DB.VisibleInViewFilter(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public FilteredElementCollector VisibleInView(Document document, ElementId viewId)
        {
            return collector.WherePasses(new VisibleInViewFilter(document, viewId));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.VisibleInViewFilter(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public FilteredElementCollector VisibleInView(View view)
        {
            return collector.WherePasses(new VisibleInViewFilter(view.Document, view.Id));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.VisibleInViewFilter(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,System.Boolean)"/>
        [Pure]
        public FilteredElementCollector NotVisibleInView(Document document, ElementId viewId)
        {
            return collector.WherePasses(new VisibleInViewFilter(document, viewId, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.VisibleInViewFilter(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,System.Boolean)"/>
        [Pure]
        public FilteredElementCollector NotVisibleInView(View view)
        {
            return collector.WherePasses(new VisibleInViewFilter(view.Document, view.Id, inverted: true));
        }
#endif

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementLevelFilter(Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public FilteredElementCollector OnLevel(ElementId levelId)
        {
            return collector.WherePasses(new ElementLevelFilter(levelId));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementLevelFilter(Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public FilteredElementCollector OnLevel(Level level)
        {
            return collector.WherePasses(new ElementLevelFilter(level.Id));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementLevelFilter(Autodesk.Revit.DB.ElementId,System.Boolean)"/>
        [Pure]
        public FilteredElementCollector NotOnLevel(ElementId levelId)
        {
            return collector.WherePasses(new ElementLevelFilter(levelId, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementLevelFilter(Autodesk.Revit.DB.ElementId,System.Boolean)"/>
        [Pure]
        public FilteredElementCollector NotOnLevel(Level level)
        {
            return collector.WherePasses(new ElementLevelFilter(level.Id, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementDesignOptionFilter(Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public FilteredElementCollector InDesignOption(ElementId designOptionId)
        {
            return collector.WherePasses(new ElementDesignOptionFilter(designOptionId));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementDesignOptionFilter(Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public FilteredElementCollector InDesignOption(DesignOption designOption)
        {
            return collector.WherePasses(new ElementDesignOptionFilter(designOption.Id));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementDesignOptionFilter(Autodesk.Revit.DB.ElementId,System.Boolean)"/>
        [Pure]
        public FilteredElementCollector NotInDesignOption(ElementId designOptionId)
        {
            return collector.WherePasses(new ElementDesignOptionFilter(designOptionId, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementDesignOptionFilter(Autodesk.Revit.DB.ElementId,System.Boolean)"/>
        [Pure]
        public FilteredElementCollector NotInDesignOption(DesignOption designOption)
        {
            return collector.WherePasses(new ElementDesignOptionFilter(designOption.Id, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementWorksetFilter(Autodesk.Revit.DB.WorksetId)"/>
        [Pure]
        public FilteredElementCollector InWorkset(WorksetId worksetId)
        {
            return collector.WherePasses(new ElementWorksetFilter(worksetId));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementWorksetFilter(Autodesk.Revit.DB.WorksetId,System.Boolean)"/>
        [Pure]
        public FilteredElementCollector NotInWorkset(WorksetId worksetId)
        {
            return collector.WherePasses(new ElementWorksetFilter(worksetId, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.StructuralInstanceUsageFilter(Autodesk.Revit.DB.Structure.StructuralInstanceUsage)"/>
        [Pure]
        public FilteredElementCollector WithStructuralUsage(StructuralInstanceUsage usage)
        {
            return collector.WherePasses(new StructuralInstanceUsageFilter(usage));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.StructuralInstanceUsageFilter(Autodesk.Revit.DB.Structure.StructuralInstanceUsage,System.Boolean)"/>
        [Pure]
        public FilteredElementCollector WithoutStructuralUsage(StructuralInstanceUsage usage)
        {
            return collector.WherePasses(new StructuralInstanceUsageFilter(usage, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.StructuralWallUsageFilter(Autodesk.Revit.DB.Structure.StructuralWallUsage)"/>
        [Pure]
        public FilteredElementCollector WithStructuralWallUsage(StructuralWallUsage usage)
        {
            return collector.WherePasses(new StructuralWallUsageFilter(usage));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.StructuralWallUsageFilter(Autodesk.Revit.DB.Structure.StructuralWallUsage,System.Boolean)"/>
        [Pure]
        public FilteredElementCollector WithoutStructuralWallUsage(StructuralWallUsage usage)
        {
            return collector.WherePasses(new StructuralWallUsageFilter(usage, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.StructuralMaterialTypeFilter(Autodesk.Revit.DB.Structure.StructuralMaterialType)"/>
        [Pure]
        public FilteredElementCollector WithStructuralMaterial(StructuralMaterialType type)
        {
            return collector.WherePasses(new StructuralMaterialTypeFilter(type));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.StructuralMaterialTypeFilter(Autodesk.Revit.DB.Structure.StructuralMaterialType,System.Boolean)"/>
        [Pure]
        public FilteredElementCollector WithoutStructuralMaterial(StructuralMaterialType type)
        {
            return collector.WherePasses(new StructuralMaterialTypeFilter(type, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.FamilyStructuralMaterialTypeFilter(Autodesk.Revit.DB.Structure.StructuralMaterialType)"/>
        [Pure]
        public FilteredElementCollector WithFamilyStructuralMaterial(StructuralMaterialType type)
        {
            return collector.WherePasses(new FamilyStructuralMaterialTypeFilter(type));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.FamilyStructuralMaterialTypeFilter(Autodesk.Revit.DB.Structure.StructuralMaterialType,System.Boolean)"/>
        [Pure]
        public FilteredElementCollector WithoutFamilyStructuralMaterial(StructuralMaterialType type)
        {
            return collector.WherePasses(new FamilyStructuralMaterialTypeFilter(type, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementPhaseStatusFilter(Autodesk.Revit.DB.ElementId,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementOnPhaseStatus})"/>
        [Pure]
        public FilteredElementCollector WithPhaseStatus(ElementId phaseId, ICollection<ElementOnPhaseStatus> statuses)
        {
            return collector.WherePasses(new ElementPhaseStatusFilter(phaseId, statuses));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementPhaseStatusFilter(Autodesk.Revit.DB.ElementId,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementOnPhaseStatus})"/>
        [Pure]
        public FilteredElementCollector WithPhaseStatus(ElementId phaseId, params ElementOnPhaseStatus[] statuses)
        {
            return collector.WherePasses(new ElementPhaseStatusFilter(phaseId, statuses));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementPhaseStatusFilter(Autodesk.Revit.DB.ElementId,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementOnPhaseStatus})"/>
        [Pure]
        public FilteredElementCollector WithPhaseStatus(Phase phase, ICollection<ElementOnPhaseStatus> statuses)
        {
            return collector.WherePasses(new ElementPhaseStatusFilter(phase.Id, statuses));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementPhaseStatusFilter(Autodesk.Revit.DB.ElementId,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementOnPhaseStatus})"/>
        [Pure]
        public FilteredElementCollector WithPhaseStatus(Phase phase, params ElementOnPhaseStatus[] statuses)
        {
            return collector.WherePasses(new ElementPhaseStatusFilter(phase.Id, statuses));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementPhaseStatusFilter(Autodesk.Revit.DB.ElementId,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementOnPhaseStatus},System.Boolean)"/>
        [Pure]
        public FilteredElementCollector WithoutPhaseStatus(ElementId phaseId, ICollection<ElementOnPhaseStatus> statuses)
        {
            return collector.WherePasses(new ElementPhaseStatusFilter(phaseId, statuses, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementPhaseStatusFilter(Autodesk.Revit.DB.ElementId,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementOnPhaseStatus},System.Boolean)"/>
        [Pure]
        public FilteredElementCollector WithoutPhaseStatus(ElementId phaseId, params ElementOnPhaseStatus[] statuses)
        {
            return collector.WherePasses(new ElementPhaseStatusFilter(phaseId, statuses, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementPhaseStatusFilter(Autodesk.Revit.DB.ElementId,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementOnPhaseStatus},System.Boolean)"/>
        [Pure]
        public FilteredElementCollector WithoutPhaseStatus(Phase phase, ICollection<ElementOnPhaseStatus> statuses)
        {
            return collector.WherePasses(new ElementPhaseStatusFilter(phase.Id, statuses, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementPhaseStatusFilter(Autodesk.Revit.DB.ElementId,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementOnPhaseStatus},System.Boolean)"/>
        [Pure]
        public FilteredElementCollector WithoutPhaseStatus(Phase phase, params ElementOnPhaseStatus[] statuses)
        {
            return collector.WherePasses(new ElementPhaseStatusFilter(phase.Id, statuses, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExtensibleStorage.ExtensibleStorageFilter(System.Guid)"/>
        [Pure]
        public FilteredElementCollector WithExtensibleStorage(Guid schemaGuid)
        {
            return collector.WherePasses(new ExtensibleStorageFilter(schemaGuid));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExtensibleStorage.ExtensibleStorageFilter(System.Guid)"/>
        [Pure]
        public FilteredElementCollector WithExtensibleStorage(Schema schema)
        {
            return collector.WherePasses(new ExtensibleStorageFilter(schema.GUID));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.SharedParameterApplicableRule(System.String)"/>
        [Pure]
        public FilteredElementCollector HasSharedParameter(string parameterName)
        {
            return collector.WherePasses(new ElementParameterFilter(new SharedParameterApplicableRule(parameterName)));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.BoundingBoxIntersectsFilter(Autodesk.Revit.DB.Outline)"/>
        [Pure]
        public FilteredElementCollector IntersectingBoundingBox(Outline outline)
        {
            return collector.WherePasses(new BoundingBoxIntersectsFilter(outline));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.BoundingBoxIntersectsFilter(Autodesk.Revit.DB.Outline,System.Double)"/>
        [Pure]
        public FilteredElementCollector IntersectingBoundingBox(Outline outline, double tolerance)
        {
            return collector.WherePasses(new BoundingBoxIntersectsFilter(outline, tolerance));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.BoundingBoxIntersectsFilter(Autodesk.Revit.DB.Outline,System.Boolean)"/>
        [Pure]
        public FilteredElementCollector NotIntersectingBoundingBox(Outline outline)
        {
            return collector.WherePasses(new BoundingBoxIntersectsFilter(outline, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.BoundingBoxIntersectsFilter(Autodesk.Revit.DB.Outline,System.Double,System.Boolean)"/>
        [Pure]
        public FilteredElementCollector NotIntersectingBoundingBox(Outline outline, double tolerance)
        {
            return collector.WherePasses(new BoundingBoxIntersectsFilter(outline, tolerance, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.BoundingBoxIsInsideFilter(Autodesk.Revit.DB.Outline)"/>
        [Pure]
        public FilteredElementCollector InsideBoundingBox(Outline outline)
        {
            return collector.WherePasses(new BoundingBoxIsInsideFilter(outline));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.BoundingBoxIsInsideFilter(Autodesk.Revit.DB.Outline,System.Double)"/>
        [Pure]
        public FilteredElementCollector InsideBoundingBox(Outline outline, double tolerance)
        {
            return collector.WherePasses(new BoundingBoxIsInsideFilter(outline, tolerance));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.BoundingBoxIsInsideFilter(Autodesk.Revit.DB.Outline,System.Boolean)"/>
        [Pure]
        public FilteredElementCollector NotInsideBoundingBox(Outline outline)
        {
            return collector.WherePasses(new BoundingBoxIsInsideFilter(outline, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.BoundingBoxIsInsideFilter(Autodesk.Revit.DB.Outline,System.Double,System.Boolean)"/>
        [Pure]
        public FilteredElementCollector NotInsideBoundingBox(Outline outline, double tolerance)
        {
            return collector.WherePasses(new BoundingBoxIsInsideFilter(outline, tolerance, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.BoundingBoxContainsPointFilter(Autodesk.Revit.DB.XYZ)"/>
        [Pure]
        public FilteredElementCollector ContainingPoint(XYZ point)
        {
            return collector.WherePasses(new BoundingBoxContainsPointFilter(point));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.BoundingBoxContainsPointFilter(Autodesk.Revit.DB.XYZ,System.Double)"/>
        [Pure]
        public FilteredElementCollector ContainingPoint(XYZ point, double tolerance)
        {
            return collector.WherePasses(new BoundingBoxContainsPointFilter(point, tolerance));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.BoundingBoxContainsPointFilter(Autodesk.Revit.DB.XYZ,System.Boolean)"/>
        [Pure]
        public FilteredElementCollector NotContainingPoint(XYZ point)
        {
            return collector.WherePasses(new BoundingBoxContainsPointFilter(point, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.BoundingBoxContainsPointFilter(Autodesk.Revit.DB.XYZ,System.Double,System.Boolean)"/>
        [Pure]
        public FilteredElementCollector NotContainingPoint(XYZ point, double tolerance)
        {
            return collector.WherePasses(new BoundingBoxContainsPointFilter(point, tolerance, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementIntersectsElementFilter(Autodesk.Revit.DB.Element)"/>
        [Pure]
        public FilteredElementCollector IntersectingElement(Element element)
        {
            return collector.WherePasses(new ElementIntersectsElementFilter(element));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementIntersectsElementFilter(Autodesk.Revit.DB.Element,System.Boolean)"/>
        [Pure]
        public FilteredElementCollector NotIntersectingElement(Element element)
        {
            return collector.WherePasses(new ElementIntersectsElementFilter(element, inverted: true));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementIntersectsSolidFilter(Autodesk.Revit.DB.Solid)"/>
        [Pure]
        public FilteredElementCollector IntersectingSolid(Solid solid)
        {
            return collector.WherePasses(new ElementIntersectsSolidFilter(solid));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementIntersectsSolidFilter(Autodesk.Revit.DB.Solid,System.Boolean)"/>
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

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateEqualsRule(Autodesk.Revit.DB.ElementId,System.String)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector Equals(string value)
    {
#if REVIT2023_OR_GREATER
        return ApplyFilter(ParameterFilterRuleFactory.CreateEqualsRule(_parameterId, value));
#else
        return ApplyFilter(ParameterFilterRuleFactory.CreateEqualsRule(_parameterId, value, true));
#endif
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateEqualsRule(Autodesk.Revit.DB.ElementId,System.Int32)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector Equals(int value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateEqualsRule(_parameterId, value));
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateEqualsRule(Autodesk.Revit.DB.ElementId,System.Double,System.Double)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector Equals(double value, double epsilon)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateEqualsRule(_parameterId, value, epsilon));
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateEqualsRule(Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector Equals(ElementId value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateEqualsRule(_parameterId, value));
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateNotEqualsRule(Autodesk.Revit.DB.ElementId,System.String)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector NotEquals(string value)
    {
#if REVIT2023_OR_GREATER
        return ApplyFilter(ParameterFilterRuleFactory.CreateNotEqualsRule(_parameterId, value));
#else
        return ApplyFilter(ParameterFilterRuleFactory.CreateNotEqualsRule(_parameterId, value, true));
#endif
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateNotEqualsRule(Autodesk.Revit.DB.ElementId,System.Int32)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector NotEquals(int value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateNotEqualsRule(_parameterId, value));
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateNotEqualsRule(Autodesk.Revit.DB.ElementId,System.Double,System.Double)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector NotEquals(double value, double epsilon)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateNotEqualsRule(_parameterId, value, epsilon));
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateNotEqualsRule(Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector NotEquals(ElementId value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateNotEqualsRule(_parameterId, value));
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateGreaterRule(Autodesk.Revit.DB.ElementId,System.String)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector IsGreaterThan(string value)
    {
#if REVIT2023_OR_GREATER
        return ApplyFilter(ParameterFilterRuleFactory.CreateGreaterRule(_parameterId, value));
#else
        return ApplyFilter(ParameterFilterRuleFactory.CreateGreaterRule(_parameterId, value, true));
#endif
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateGreaterRule(Autodesk.Revit.DB.ElementId,System.Int32)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector IsGreaterThan(int value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateGreaterRule(_parameterId, value));
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateGreaterRule(Autodesk.Revit.DB.ElementId,System.Double,System.Double)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    /// <remarks>Values greater than <paramref name="value" /> but within 1e-9 are considered equal, not greater.</remarks>
    public FilteredElementCollector IsGreaterThan(double value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateGreaterRule(_parameterId, value, 1e-9));
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateGreaterRule(Autodesk.Revit.DB.ElementId,System.Double,System.Double)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector IsGreaterThan(double value, double epsilon)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateGreaterRule(_parameterId, value, epsilon));
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateGreaterRule(Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector IsGreaterThan(ElementId value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateGreaterRule(_parameterId, value));
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateGreaterOrEqualRule(Autodesk.Revit.DB.ElementId,System.String)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector IsGreaterThanOrEqualTo(string value)
    {
#if REVIT2023_OR_GREATER
        return ApplyFilter(ParameterFilterRuleFactory.CreateGreaterOrEqualRule(_parameterId, value));
#else
        return ApplyFilter(ParameterFilterRuleFactory.CreateGreaterOrEqualRule(_parameterId, value, true));
#endif
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateGreaterOrEqualRule(Autodesk.Revit.DB.ElementId,System.Int32)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector IsGreaterThanOrEqualTo(int value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateGreaterOrEqualRule(_parameterId, value));
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateGreaterOrEqualRule(Autodesk.Revit.DB.ElementId,System.Double,System.Double)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector IsGreaterThanOrEqualTo(double value, double epsilon)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateGreaterOrEqualRule(_parameterId, value, epsilon));
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateGreaterOrEqualRule(Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector IsGreaterThanOrEqualTo(ElementId value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateGreaterOrEqualRule(_parameterId, value));
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateLessRule(Autodesk.Revit.DB.ElementId,System.String)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector IsLessThan(string value)
    {
#if REVIT2023_OR_GREATER
        return ApplyFilter(ParameterFilterRuleFactory.CreateLessRule(_parameterId, value));
#else
        return ApplyFilter(ParameterFilterRuleFactory.CreateLessRule(_parameterId, value, true));
#endif
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateLessRule(Autodesk.Revit.DB.ElementId,System.Int32)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector IsLessThan(int value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateLessRule(_parameterId, value));
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateLessRule(Autodesk.Revit.DB.ElementId,System.Double,System.Double)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector IsLessThan(double value, double epsilon)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateLessRule(_parameterId, value, epsilon));
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateLessRule(Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector IsLessThan(ElementId value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateLessRule(_parameterId, value));
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateLessOrEqualRule(Autodesk.Revit.DB.ElementId,System.String)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector IsLessThanOrEqualTo(string value)
    {
#if REVIT2023_OR_GREATER
        return ApplyFilter(ParameterFilterRuleFactory.CreateLessOrEqualRule(_parameterId, value));
#else
        return ApplyFilter(ParameterFilterRuleFactory.CreateLessOrEqualRule(_parameterId, value, true));
#endif
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateLessOrEqualRule(Autodesk.Revit.DB.ElementId,System.Int32)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector IsLessThanOrEqualTo(int value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateLessOrEqualRule(_parameterId, value));
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateLessOrEqualRule(Autodesk.Revit.DB.ElementId,System.Double,System.Double)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector IsLessThanOrEqualTo(double value, double epsilon)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateLessOrEqualRule(_parameterId, value, epsilon));
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateLessOrEqualRule(Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector IsLessThanOrEqualTo(ElementId value)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateLessOrEqualRule(_parameterId, value));
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateContainsRule(Autodesk.Revit.DB.ElementId,System.String)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector Contains(string value)
    {
#if REVIT2023_OR_GREATER
        return ApplyFilter(ParameterFilterRuleFactory.CreateContainsRule(_parameterId, value));
#else
        return ApplyFilter(ParameterFilterRuleFactory.CreateContainsRule(_parameterId, value, true));
#endif
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateNotContainsRule(Autodesk.Revit.DB.ElementId,System.String)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector NotContains(string value)
    {
#if REVIT2023_OR_GREATER
        return ApplyFilter(ParameterFilterRuleFactory.CreateNotContainsRule(_parameterId, value));
#else
        return ApplyFilter(ParameterFilterRuleFactory.CreateNotContainsRule(_parameterId, value, true));
#endif
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateBeginsWithRule(Autodesk.Revit.DB.ElementId,System.String)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector StartsWith(string value)
    {
#if REVIT2023_OR_GREATER
        return ApplyFilter(ParameterFilterRuleFactory.CreateBeginsWithRule(_parameterId, value));
#else
        return ApplyFilter(ParameterFilterRuleFactory.CreateBeginsWithRule(_parameterId, value, true));
#endif
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateNotBeginsWithRule(Autodesk.Revit.DB.ElementId,System.String)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector NotStartsWith(string value)
    {
#if REVIT2023_OR_GREATER
        return ApplyFilter(ParameterFilterRuleFactory.CreateNotBeginsWithRule(_parameterId, value));
#else
        return ApplyFilter(ParameterFilterRuleFactory.CreateNotBeginsWithRule(_parameterId, value, true));
#endif
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateEndsWithRule(Autodesk.Revit.DB.ElementId,System.String)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector EndsWith(string value)
    {
#if REVIT2023_OR_GREATER
        return ApplyFilter(ParameterFilterRuleFactory.CreateEndsWithRule(_parameterId, value));
#else
        return ApplyFilter(ParameterFilterRuleFactory.CreateEndsWithRule(_parameterId, value, true));
#endif
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateNotEndsWithRule(Autodesk.Revit.DB.ElementId,System.String)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector NotEndsWith(string value)
    {
#if REVIT2023_OR_GREATER
        return ApplyFilter(ParameterFilterRuleFactory.CreateNotEndsWithRule(_parameterId, value));
#else
        return ApplyFilter(ParameterFilterRuleFactory.CreateNotEndsWithRule(_parameterId, value, true));
#endif
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateHasValueParameterRule(Autodesk.Revit.DB.ElementId)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector HasValue()
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateHasValueParameterRule(_parameterId));
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateHasNoValueParameterRule(Autodesk.Revit.DB.ElementId)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector HasNoValue()
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateHasNoValueParameterRule(_parameterId));
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateIsAssociatedWithGlobalParameterRule(Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
    public FilteredElementCollector IsAssociatedWithGlobalParameter(ElementId globalParameterId)
    {
        return ApplyFilter(ParameterFilterRuleFactory.CreateIsAssociatedWithGlobalParameterRule(_parameterId, globalParameterId));
    }

    /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterRuleFactory.CreateIsNotAssociatedWithGlobalParameterRule(Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId)"/>
    /// <returns>The <see cref="Autodesk.Revit.DB.FilteredElementCollector" /> for chaining additional filters.</returns>
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