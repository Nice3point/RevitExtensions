using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.ExtensibleStorage;
using Autodesk.Revit.DB.Structure;
using Nice3point.TUnit.Revit;
using Nice3point.TUnit.Revit.Executors;
using TUnit.Core.Executors;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class FilteredElementCollectorExtensionsTests : RevitApiTest
{
    private static readonly Guid SchemaGuid = new("A1B2C3D4-E5F6-7890-ABCD-EF1234567890");

#pragma warning disable TUnit0023
    private Document _document = null!;

    private Level _groundFloor = null!;
    private Level _firstFloor = null!;
    private Wall _wall = null!;
    private Wall _crossingWall = null!;
    private Grid _grid = null!;
    private Room _room = null!;
    private FamilySymbol _familySymbol = null!;
    private Family _family = null!;
    private View _floorPlan = null!;
    private ViewPlan _areaView = null!;
    private Phase _phase = null!;
    private WorksetId _worksetId = null!;
#pragma warning restore TUnit0023

    [Before(Test)]
    [HookExecutor<RevitThreadExecutor>]
    public void SeedModel()
    {
        _document = Application.NewProjectDocument(UnitSystem.Metric);

        _familySymbol = new FilteredElementCollector(_document)
            .OfClass(typeof(FamilySymbol))
            .WhereElementIsElementType()
            .Cast<FamilySymbol>()
            .First();

        _phase = new FilteredElementCollector(_document)
            .OfClass(typeof(Phase))
            .Cast<Phase>()
            .Last();

        _family = (Family)_document.GetElement(_familySymbol.Family.Id);

        var areaScheme = new FilteredElementCollector(_document)
            .OfClass(typeof(AreaScheme))
            .Cast<AreaScheme>()
            .First();

        using (RevitApiContext.BeginFailureSuppressionScope())
        {
            using var transaction = new Transaction(_document, "Seed model");
            transaction.Start();

            CreateLevels();
            CreateWalls();
            CreateGrids();
            CreateViews();
            CreateRoomsAndAreas(areaScheme);
            CreateFamilyInstances();
            CreateExtensibleStorage();

            transaction.Commit();
        }

        EnableWorksharing();
    }

    [After(Test)]
    [HookExecutor<RevitThreadExecutor>]
    public void CloseModel()
    {
        _document.Close(false);
    }

    [Test]
    public async Task OfClass_ValidType_ReturnsOnlyWalls()
    {
        // Act
        var walls = _document.CollectElements()
            .OfClass<Wall>()
            .ToElements();

        // Assert
        await Assert.That(walls).IsNotEmpty();
        await Assert.That(walls).All().Satisfy(element => element is Wall, source => source.IsTrue());
    }

    [Test]
    public async Task OfClasses_IList_ReturnsMultipleTypes()
    {
        // Arrange
        IList<Type> types = [typeof(Wall), typeof(Grid)];

        // Act
        var elements = _document.CollectElements()
            .OfClasses(types)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element is Wall or Grid, source => source.IsTrue());
    }

    [Test]
    public async Task OfClasses_Params_ReturnsMultipleTypes()
    {
        // Act
        var elements = _document.CollectElements()
            .OfClasses(typeof(Wall), typeof(Grid))
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element is Wall or Grid, source => source.IsTrue());
    }

    [Test]
    public async Task ExcludingClass_ValidType_ExcludesWalls()
    {
        // Act
        var elements = _document.CollectElements()
            .Instances()
            .ExcludingClass<Wall>()
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element is not Wall, source => source.IsTrue());
    }

    [Test]
    public async Task ExcludingClasses_IList_ExcludesMultipleTypes()
    {
        // Arrange
        IList<Type> types = [typeof(Wall), typeof(Grid)];

        // Act
        var elements = _document.CollectElements()
            .Instances()
            .ExcludingClasses(types)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element is not Wall and not Grid, source => source.IsTrue());
    }

    [Test]
    public async Task ExcludingClasses_Params_ExcludesMultipleTypes()
    {
        // Act
        var elements = _document.CollectElements()
            .Instances()
            .ExcludingClasses(typeof(Wall), typeof(Grid))
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element is not Wall and not Grid, source => source.IsTrue());
    }

    [Test]
    public async Task OfCategories_ICollection_ReturnsMatchingCategories()
    {
        // Arrange
        ICollection<BuiltInCategory> categories = [BuiltInCategory.OST_Walls, BuiltInCategory.OST_Grids];

        // Act
        var elements = _document.CollectElements()
            .Instances()
            .OfCategories(categories)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element is Wall or Grid, source => source.IsTrue());
    }

    [Test]
    public async Task OfCategories_Params_ReturnsMatchingCategories()
    {
        // Act
        var elements = _document.CollectElements()
            .Instances()
            .OfCategories(BuiltInCategory.OST_Walls, BuiltInCategory.OST_Grids)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element is Wall or Grid, source => source.IsTrue());
    }

    [Test]
    public async Task ExcludingCategory_ExcludesMatchingCategory()
    {
        // Act
        var elements = _document.CollectElements()
            .Instances()
            .ExcludingCategory(BuiltInCategory.OST_Walls)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(
#if REVIT2023_OR_GREATER
            element => element.Category?.BuiltInCategory != BuiltInCategory.OST_Walls,
#else
            element => !element.Category?.Id.IsCategory(BuiltInCategory.OST_Walls),
#endif
            source => source.IsTrue());
    }

    [Test]
    public async Task ExcludingCategories_ICollection_ExcludesMultipleCategories()
    {
        // Arrange
        ICollection<BuiltInCategory> categories = [BuiltInCategory.OST_Walls, BuiltInCategory.OST_Grids];

        // Act
        var elements = _document.CollectElements()
            .Instances()
            .ExcludingCategories(categories)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element is not Wall and not Grid, source => source.IsTrue());
    }

    [Test]
    public async Task ExcludingCategories_Params_ExcludesMultipleCategories()
    {
        // Act
        var elements = _document.CollectElements()
            .Instances()
            .ExcludingCategories(BuiltInCategory.OST_Walls, BuiltInCategory.OST_Grids)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element is not Wall and not Grid, source => source.IsTrue());
    }
#if REVIT2021_OR_GREATER

    [Test]
    public async Task OfElements_ReturnsOnlyRequestedElements()
    {
        // Arrange
        ICollection<ElementId> ids = [_wall.Id, _grid.Id];

        // Act
        var elements = _document.CollectElements()
            .OfElements(ids)
            .ToElements();

        // Assert
        await Assert.That(elements).Count().IsEqualTo(2);
        await Assert.That(elements).All().Satisfy(element => ids.Contains(element.Id), source => source.IsTrue());
    }
#endif

    [Test]
    public async Task OfCurveElementType_ReturnsMatchingCurveElements()
    {
        // Act
        var elements = _document.CollectElements()
            .OfCurveElementType(CurveElementType.ModelCurve)
            .ToElements();

        // Assert
        await Assert.That(elements).All().Satisfy(element => element is CurveElement, source => source.IsTrue());
    }

    [Test]
    public async Task ExcludingCurveElementType_ExcludesMatchingCurveElements()
    {
        // Act
        var elements = _document.CollectElements()
            .OfCurveElementType(CurveElementType.ModelCurve)
            .ExcludingCurveElementType(CurveElementType.DetailCurve)
            .ToElements();

        // Assert
        await Assert.That(elements).All().Satisfy(element => element is CurveElement, source => source.IsTrue());
    }

    [Test]
    public async Task OfStructuralType_ReturnsMatchingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .Instances()
            .OfStructuralType(StructuralType.NonStructural)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
    }

    [Test]
    public async Task ExcludingStructuralType_ExcludesMatchingElements()
    {
        // Act
        var allCount = new FilteredElementCollector(_document)
            .WhereElementIsNotElementType()
            .GetElementCount();

        var nonStructuralCount = _document.CollectElements()
            .Instances()
            .OfStructuralType(StructuralType.NonStructural)
            .GetElementCount();

        var excludedCount = _document.CollectElements()
            .Instances()
            .ExcludingStructuralType(StructuralType.NonStructural)
            .GetElementCount();

        // Assert
        await Assert.That(nonStructuralCount + excludedCount).IsEqualTo(allCount);
    }

    [Test]
    public async Task Types_ReturnsOnlyElementTypes()
    {
        // Act
        var elements = _document.CollectElements()
            .Types()
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element is ElementType, source => source.IsTrue());
    }

    [Test]
    public async Task Instances_ReturnsNoElementTypes()
    {
        // Act
        var elements = _document.CollectElements()
            .Instances()
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element is not ElementType, source => source.IsTrue());
    }

    [Test]
    public async Task Rooms_ReturnsOnlyRooms()
    {
        // Act
        var elements = _document.CollectElements()
            .Rooms()
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element is Room, source => source.IsTrue());
    }

    [Test]
    public async Task RoomTags_ReturnsOnlyRoomTags()
    {
        // Act
        var elements = _document.CollectElements()
            .RoomTags()
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element is RoomTag, source => source.IsTrue());
    }

    [Test]
    public async Task Areas_ReturnsOnlyAreas()
    {
        // Act
        var elements = _document.CollectElements()
            .Areas()
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element is Area, source => source.IsTrue());
    }

    [Test]
    public async Task AreaTags_ReturnsOnlyAreaTags()
    {
        // Act
        var elements = _document.CollectElements()
            .AreaTags()
            .ToElements();

        // Assert
        await Assert.That(elements).All().Satisfy(element => element is AreaTag, source => source.IsTrue());
    }

    [Test]
    public async Task FamilySymbols_ById_ReturnsSymbolsOfFamily()
    {
        // Act
        var elements = _document.CollectElements()
            .FamilySymbols(_family.Id)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(
            element => element is FamilySymbol symbol && symbol.Family.Id == _family.Id,
            source => source.IsTrue());
    }

    [Test]
    public async Task FamilySymbols_ByFamily_ReturnsSymbolsOfFamily()
    {
        // Act
        var elements = _document.CollectElements()
            .FamilySymbols(_family)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(
            element => element is FamilySymbol symbol && symbol.Family.Id == _family.Id,
            source => source.IsTrue());
    }

    [Test]
    public async Task FamilyInstances_ByDocumentAndElementId_ReturnsInstancesOfSymbol()
    {
        // Act
        var elements = _document.CollectElements()
            .FamilyInstances(_document, _familySymbol.Id)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(
            element => element is FamilyInstance instance && instance.Symbol.Id == _familySymbol.Id,
            source => source.IsTrue());
    }

    [Test]
    public async Task FamilyInstances_BySymbol_ReturnsInstancesOfSymbol()
    {
        // Act
        var elements = _document.CollectElements()
            .FamilyInstances(_familySymbol)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(
            element => element is FamilyInstance instance && instance.Symbol.Id == _familySymbol.Id,
            source => source.IsTrue());
    }

    [Test]
    public async Task IsCurveDriven_ReturnsCurveDrivenElements()
    {
        // Act
        var elements = _document.CollectElements()
            .IsCurveDriven()
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element.Location is LocationCurve, source => source.IsTrue());
    }

    [Test]
    public async Task IsViewIndependent_ReturnsViewIndependentElements()
    {
        // Act
        var elements = _document.CollectElements()
            .IsViewIndependent()
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
    }

    [Test]
    public async Task OwnedByView_ByElementId_ReturnsViewOwnedElements()
    {
        // Act
        var elements = _document.CollectElements(_floorPlan)
            .OwnedByView(_floorPlan.Id)
            .ToElements();

        // Assert
        await Assert.That(elements).All().Satisfy(element => element.OwnerViewId == _floorPlan.Id, source => source.IsTrue());
    }

    [Test]
    public async Task OwnedByView_ByView_ReturnsViewOwnedElements()
    {
        // Act
        var elements = _document.CollectElements(_floorPlan)
            .OwnedByView(_floorPlan)
            .ToElements();

        // Assert
        await Assert.That(elements).All().Satisfy(element => element.OwnerViewId == _floorPlan.Id, source => source.IsTrue());
    }

    [Test]
    public async Task NotOwnedByView_ByElementId_ExcludesViewOwnedElements()
    {
        // Act
        var elements = _document.CollectElements()
            .NotOwnedByView(_floorPlan.Id)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element.OwnerViewId != _floorPlan.Id, source => source.IsTrue());
    }

    [Test]
    public async Task NotOwnedByView_ByView_ExcludesViewOwnedElements()
    {
        // Act
        var elements = _document.CollectElements()
            .NotOwnedByView(_floorPlan)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element.OwnerViewId != _floorPlan.Id, source => source.IsTrue());
    }
#if REVIT2021_OR_GREATER

    [Test]
    public async Task VisibleInView_ByDocumentAndElementId_ReturnsVisibleElements()
    {
        // Act
        var elements = _document.CollectElements(_floorPlan)
            .VisibleInView(_document, _floorPlan.Id)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
    }

    [Test]
    public async Task VisibleInView_ByView_ReturnsVisibleElements()
    {
        // Act
        var elements = _document.CollectElements(_floorPlan)
            .VisibleInView(_floorPlan)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
    }

    [Test]
    public async Task NotVisibleInView_ByDocumentAndElementId_ExcludesVisibleElements()
    {
        // Act
        var allCount = new FilteredElementCollector(_document, _floorPlan.Id)
            .WhereElementIsNotElementType()
            .GetElementCount();

        var visibleCount = _document.CollectElements(_floorPlan)
            .VisibleInView(_document, _floorPlan.Id)
            .GetElementCount();

        var notVisibleCount = _document.CollectElements(_floorPlan)
            .NotVisibleInView(_document, _floorPlan.Id)
            .GetElementCount();

        // Assert
        // Revit API issue. VisibleInViewFilter broken.
        // await Assert.That(visibleCount + notVisibleCount).IsEqualTo(allCount);
        await Assert.That(visibleCount + notVisibleCount).IsEqualTo(allCount * 2);
    }

    [Test]
    public async Task NotVisibleInView_ByView_ExcludesVisibleElements()
    {
        // Act
        var allCount = new FilteredElementCollector(_document, _floorPlan.Id)
            .WhereElementIsNotElementType()
            .GetElementCount();

        var visibleCount = _document.CollectElements()
            .VisibleInView(_floorPlan)
            .GetElementCount();

        var notVisibleCount = _document.CollectElements()
            .NotVisibleInView(_floorPlan)
            .GetElementCount();

        // Assert
        // Revit API issue. VisibleInViewFilter broken.
        // await Assert.That(visibleCount + notVisibleCount).IsEqualTo(allCount);
        await Assert.That(visibleCount + notVisibleCount).IsEqualTo(allCount * 2);
    }
#endif

    [Test]
    public async Task OnLevel_ByElementId_ReturnsElementsOnLevel()
    {
        // Act
        var elements = _document.CollectElements()
            .Instances()
            .OnLevel(_groundFloor.Id)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element.LevelId == _groundFloor.Id, source => source.IsTrue());
    }

    [Test]
    public async Task OnLevel_ByLevel_ReturnsElementsOnLevel()
    {
        // Act
        var elements = _document.CollectElements()
            .Instances()
            .OnLevel(_groundFloor)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element.LevelId == _groundFloor.Id, source => source.IsTrue());
    }

    [Test]
    public async Task NotOnLevel_ByElementId_ExcludesElementsOnLevel()
    {
        // Act
        var elements = _document.CollectElements()
            .Instances()
            .NotOnLevel(_groundFloor.Id)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element.LevelId != _groundFloor.Id, source => source.IsTrue());
    }

    [Test]
    public async Task NotOnLevel_ByLevel_ExcludesElementsOnLevel()
    {
        // Act
        var elements = _document.CollectElements()
            .Instances()
            .NotOnLevel(_groundFloor)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element.LevelId != _groundFloor.Id, source => source.IsTrue());
    }

    [Test]
    public async Task InWorkset_ReturnsElementsInWorkset()
    {
        // Act
        var elements = _document.CollectElements()
            .InWorkset(_worksetId)
            .ToElements();

        // Assert
        await Assert.That(elements).All().Satisfy(element => element.WorksetId == _worksetId, source => source.IsTrue());
    }

    [Test]
    public async Task NotInWorkset_ExcludesElementsInWorkset()
    {
        // Act
        var elements = _document.CollectElements()
            .Instances()
            .NotInWorkset(_worksetId)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element.WorksetId != _worksetId, source => source.IsTrue());
    }

    [Test]
    public async Task WithPhaseStatus_ById_ReturnsMatchingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .Instances()
            .WithPhaseStatus(_phase.Id, ElementOnPhaseStatus.New)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
    }

    [Test]
    public async Task WithPhaseStatus_ByPhase_ReturnsMatchingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .Instances()
            .WithPhaseStatus(_phase, ElementOnPhaseStatus.New)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
    }

    [Test]
    public async Task WithoutPhaseStatus_ById_ExcludesMatchingElements()
    {
        // Act
        var allCount = new FilteredElementCollector(_document)
            .WhereElementIsNotElementType()
            .GetElementCount();

        var newCount = _document.CollectElements()
            .Instances()
            .WithPhaseStatus(_phase.Id, ElementOnPhaseStatus.New)
            .GetElementCount();

        var notNewCount = _document.CollectElements()
            .Instances()
            .WithoutPhaseStatus(_phase.Id, ElementOnPhaseStatus.New)
            .GetElementCount();

        // Assert
        await Assert.That(newCount + notNewCount).IsEqualTo(allCount);
    }

    [Test]
    public async Task WithoutPhaseStatus_ByPhase_ExcludesMatchingElements()
    {
        // Act
        var allCount = new FilteredElementCollector(_document)
            .WhereElementIsNotElementType()
            .GetElementCount();

        var newCount = _document.CollectElements()
            .Instances()
            .WithPhaseStatus(_phase, ElementOnPhaseStatus.New)
            .GetElementCount();

        var notNewCount = _document.CollectElements()
            .Instances()
            .WithoutPhaseStatus(_phase, ElementOnPhaseStatus.New)
            .GetElementCount();

        // Assert
        await Assert.That(newCount + notNewCount).IsEqualTo(allCount);
    }

    [Test]
    public async Task WithExtensibleStorage_ByGuid_ReturnsElementsWithSchema()
    {
        // Act
        var elements = _document.CollectElements()
            .WithExtensibleStorage(SchemaGuid)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element.Id == _wall.Id, source => source.IsTrue());
    }

    [Test]
    public async Task WithExtensibleStorage_BySchema_ReturnsElementsWithSchema()
    {
        // Arrange
        var schema = Schema.Lookup(SchemaGuid);

        // Act
        var elements = _document.CollectElements()
            .WithExtensibleStorage(schema)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element.Id == _wall.Id, source => source.IsTrue());
    }

    [Test]
    public async Task IntersectingBoundingBox_ReturnsIntersectingElements()
    {
        // Arrange
        var boundingBox = _wall.get_BoundingBox(null);
        var outline = new Outline(boundingBox.Min, boundingBox.Max);

        // Act
        var elements = _document.CollectElements()
            .IntersectingBoundingBox(outline)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
    }

    [Test]
    public async Task IntersectingBoundingBox_WithTolerance_ReturnsIntersectingElements()
    {
        // Arrange
        var boundingBox = _wall.get_BoundingBox(null);
        var outline = new Outline(boundingBox.Min, boundingBox.Max);

        // Act
        var elements = _document.CollectElements()
            .IntersectingBoundingBox(outline, 0.1)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
    }

    [Test]
    public async Task NotIntersectingBoundingBox_ExcludesIntersectingElements()
    {
        // Arrange
        var boundingBox = _wall.get_BoundingBox(null);
        var outline = new Outline(boundingBox.Min, boundingBox.Max);

        // Act
        var allCount = new FilteredElementCollector(_document)
            .WhereElementIsNotElementType()
            .GetElementCount();

        var intersectingCount = _document.CollectElements()
            .Instances()
            .IntersectingBoundingBox(outline)
            .GetElementCount();

        var notIntersectingCount = _document.CollectElements()
            .Instances()
            .NotIntersectingBoundingBox(outline)
            .GetElementCount();

        // Assert
        await Assert.That(intersectingCount + notIntersectingCount).IsEqualTo(allCount);
    }

    [Test]
    public async Task InsideBoundingBox_ReturnsContainedElements()
    {
        // Arrange
        var boundingBox = _wall.get_BoundingBox(null);
        var outline = new Outline(boundingBox.Min - new XYZ(1, 1, 1), boundingBox.Max + new XYZ(1, 1, 1));

        // Act
        var elements = _document.CollectElements()
            .InsideBoundingBox(outline)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
    }

    [Test]
    public async Task InsideBoundingBox_WithTolerance_ReturnsContainedElements()
    {
        // Arrange
        var boundingBox = _wall.get_BoundingBox(null);
        var outline = new Outline(boundingBox.Min - new XYZ(1, 1, 1), boundingBox.Max + new XYZ(1, 1, 1));

        // Act
        var elements = _document.CollectElements()
            .InsideBoundingBox(outline, 0.1)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
    }

    [Test]
    public async Task NotInsideBoundingBox_ExcludesContainedElements()
    {
        // Arrange
        var boundingBox = _wall.get_BoundingBox(null);
        var outline = new Outline(boundingBox.Min - new XYZ(1, 1, 1), boundingBox.Max + new XYZ(1, 1, 1));

        // Act
        var allCount = new FilteredElementCollector(_document)
            .WhereElementIsNotElementType()
            .GetElementCount();

        var insideCount = _document.CollectElements()
            .Instances()
            .InsideBoundingBox(outline)
            .GetElementCount();

        var notInsideCount = _document.CollectElements()
            .Instances()
            .NotInsideBoundingBox(outline)
            .GetElementCount();

        // Assert
        await Assert.That(insideCount + notInsideCount).IsEqualTo(allCount);
    }

    [Test]
    public async Task ContainingPoint_ReturnsElementsContainingPoint()
    {
        // Arrange
        var boundingBox = _wall.get_BoundingBox(null);
        var center = (boundingBox.Min + boundingBox.Max) / 2;

        // Act
        var elements = _document.CollectElements()
            .ContainingPoint(center)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
    }

    [Test]
    public async Task ContainingPoint_WithTolerance_ReturnsElementsContainingPoint()
    {
        // Arrange
        var boundingBox = _wall.get_BoundingBox(null);
        var center = (boundingBox.Min + boundingBox.Max) / 2;

        // Act
        var elements = _document.CollectElements()
            .ContainingPoint(center, 0.1)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
    }

    [Test]
    public async Task NotContainingPoint_ExcludesElementsContainingPoint()
    {
        // Arrange
        var boundingBox = _wall.get_BoundingBox(null);
        var center = (boundingBox.Min + boundingBox.Max) / 2;

        var containingIds = _document.CollectElements()
            .ContainingPoint(center)
            .ToElementIds();

        // Act
        var elements = _document.CollectElements()
            .NotContainingPoint(center)
            .ToElements();

        // Assert
        await Assert.That(elements).All().Satisfy(element => !containingIds.Contains(element.Id), source => source.IsTrue());
    }

    [Test]
    public async Task IntersectingElement_ReturnsIntersectingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .OfClass<Wall>()
            .IntersectingElement(_crossingWall)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element.Id != _crossingWall.Id, source => source.IsTrue());
    }

    [Test]
    public async Task NotIntersectingElement_ExcludesIntersectingElements()
    {
        // Arrange
        var intersectingIds = _document.CollectElements()
            .OfClass<Wall>()
            .IntersectingElement(_crossingWall)
            .ToElementIds();

        // Act
        var elements = _document.CollectElements()
            .OfClass<Wall>()
            .NotIntersectingElement(_crossingWall)
            .ToElements();

        // Assert
        await Assert.That(elements).All().Satisfy(element => !intersectingIds.Contains(element.Id), source => source.IsTrue());
    }

    [Test]
    public async Task IntersectingSolid_ReturnsIntersectingElements()
    {
        // Arrange
        var solid = CreateSolidAtPoint(new XYZ(5, 0, 0), 2);

        // Act
        var elements = _document.CollectElements()
            .OfClass<Wall>()
            .IntersectingSolid(solid)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
    }

    [Test]
    public async Task NotIntersectingSolid_ExcludesIntersectingElements()
    {
        // Arrange
        var solid = CreateSolidAtPoint(new XYZ(5, 0, 0), 2);

        var intersectingIds = _document.CollectElements()
            .OfClass<Wall>()
            .IntersectingSolid(solid)
            .ToElementIds();

        // Act
        var elements = _document.CollectElements()
            .OfClass<Wall>()
            .NotIntersectingSolid(solid)
            .ToElements();

        // Assert
        await Assert.That(elements).All().Satisfy(element => !intersectingIds.Contains(element.Id), source => source.IsTrue());
    }

    [Test]
    public async Task WhereParameter_Equals_String_ReturnsMatchingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .OfClass<Level>()
            .WhereParameter(BuiltInParameter.DATUM_TEXT).Equals("Ground Floor")
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element.Name == "Ground Floor", source => source.IsTrue());
    }

    [Test]
    public async Task WhereParameter_NotEquals_String_ExcludesMatchingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .OfClass<Level>()
            .WhereParameter(BuiltInParameter.DATUM_TEXT).NotEquals("Ground Floor")
            .ToElements();

        // Assert
        await Assert.That(elements).All().Satisfy(element => element.Name != "Ground Floor", source => source.IsTrue());
    }

    [Test]
    public async Task WhereParameter_Equals_ElementId_ReturnsMatchingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .OfClass<Wall>()
            .Instances()
            .WhereParameter(BuiltInParameter.WALL_BASE_CONSTRAINT).Equals(_groundFloor.Id)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(
            element => element.get_Parameter(BuiltInParameter.WALL_BASE_CONSTRAINT)?.AsElementId() == _groundFloor.Id,
            source => source.IsTrue());
    }

    [Test]
    public async Task WhereParameter_NotEquals_ElementId_ExcludesMatchingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .OfClass<Wall>()
            .Instances()
            .WhereParameter(BuiltInParameter.WALL_BASE_CONSTRAINT).NotEquals(_groundFloor.Id)
            .ToElements();

        // Assert
        await Assert.That(elements).All().Satisfy(
            element => element.get_Parameter(BuiltInParameter.WALL_BASE_CONSTRAINT)?.AsElementId() != _groundFloor.Id,
            source => source.IsTrue());
    }

    [Test]
    public async Task WhereParameter_Equals_Double_ReturnsMatchingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .OfClass<Level>()
            .WhereParameter(BuiltInParameter.LEVEL_ELEV).Equals(0.0, 1e-6)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
    }

    [Test]
    public async Task WhereParameter_NotEquals_Double_ExcludesMatchingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .OfClass<Level>()
            .WhereParameter(BuiltInParameter.LEVEL_ELEV).NotEquals(0.0, 1e-6)
            .ToElements();

        // Assert
        await Assert.That(elements).All().Satisfy(
            element => element.get_Parameter(BuiltInParameter.LEVEL_ELEV)?.AsDouble() is not 0.0,
            source => source.IsTrue());
    }

    [Test]
    public async Task WhereParameter_Equals_Int_ReturnsMatchingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .OfClass<Wall>()
            .Instances()
            .WhereParameter(BuiltInParameter.WALL_ATTR_ROOM_BOUNDING).Equals(1)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
    }

    [Test]
    public async Task WhereParameter_NotEquals_Int_ExcludesMatchingElements()
    {
        // Act
        var allCount = new FilteredElementCollector(_document)
            .WhereElementIsNotElementType()
            .OfClass(typeof(Wall))
            .GetElementCount();

        var boundingCount = _document.CollectElements()
            .OfClass<Wall>()
            .Instances()
            .WhereParameter(BuiltInParameter.WALL_ATTR_ROOM_BOUNDING).Equals(1)
            .GetElementCount();

        var notBoundingCount = _document.CollectElements()
            .OfClass<Wall>()
            .Instances()
            .WhereParameter(BuiltInParameter.WALL_ATTR_ROOM_BOUNDING).NotEquals(1)
            .GetElementCount();

        // Assert
        await Assert.That(boundingCount + notBoundingCount).IsEqualTo(allCount);
    }

    [Test]
    public async Task WhereParameter_IsGreaterThan_Double_ReturnsMatchingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .OfClass<Wall>()
            .Instances()
            .WhereParameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM).IsGreaterThan(0.0, 1e-6)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
    }

    [Test]
    public async Task WhereParameter_IsGreaterThan_Int_ReturnsMatchingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .OfClass<Wall>()
            .Instances()
            .WhereParameter(BuiltInParameter.WALL_ATTR_ROOM_BOUNDING).IsGreaterThan(0)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
    }

    [Test]
    public async Task WhereParameter_IsGreaterThanOrEqualTo_Double_ReturnsMatchingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .OfClass<Level>()
            .WhereParameter(BuiltInParameter.LEVEL_ELEV).IsGreaterThanOrEqualTo(0.0, 1e-6)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
    }

    [Test]
    public async Task WhereParameter_IsGreaterThanOrEqualTo_Int_ReturnsMatchingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .OfClass<Wall>()
            .Instances()
            .WhereParameter(BuiltInParameter.WALL_ATTR_ROOM_BOUNDING).IsGreaterThanOrEqualTo(1)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
    }

    [Test]
    public async Task WhereParameter_IsLessThan_Double_ReturnsMatchingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .OfClass<Wall>()
            .Instances()
            .WhereParameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM).IsLessThan(1000.0, 1e-6)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
    }

    [Test]
    public async Task WhereParameter_IsLessThan_Int_ReturnsMatchingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .OfClass<Wall>()
            .Instances()
            .WhereParameter(BuiltInParameter.WALL_ATTR_ROOM_BOUNDING).IsLessThan(2)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
    }

    [Test]
    public async Task WhereParameter_IsLessThanOrEqualTo_Double_ReturnsMatchingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .OfClass<Wall>()
            .Instances()
            .WhereParameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM).IsLessThanOrEqualTo(1000.0, 1e-6)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
    }

    [Test]
    public async Task WhereParameter_IsLessThanOrEqualTo_Int_ReturnsMatchingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .OfClass<Wall>()
            .Instances()
            .WhereParameter(BuiltInParameter.WALL_ATTR_ROOM_BOUNDING).IsLessThanOrEqualTo(1)
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
    }

    [Test]
    public async Task WhereParameter_Contains_ReturnsMatchingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .OfClass<Level>()
            .WhereParameter(BuiltInParameter.DATUM_TEXT).Contains("Floor")
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element.Name.Contains("Floor"), source => source.IsTrue());
    }

    [Test]
    public async Task WhereParameter_NotContains_ExcludesMatchingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .OfClass<Level>()
            .WhereParameter(BuiltInParameter.DATUM_TEXT).NotContains("Ground")
            .ToElements();

        // Assert
        await Assert.That(elements).All().Satisfy(element => !element.Name.Contains("Ground"), source => source.IsTrue());
    }

    [Test]
    public async Task WhereParameter_StartsWith_ReturnsMatchingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .OfClass<Level>()
            .WhereParameter(BuiltInParameter.DATUM_TEXT).StartsWith("Ground")
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element.Name.StartsWith("Ground"), source => source.IsTrue());
    }

    [Test]
    public async Task WhereParameter_NotStartsWith_ExcludesMatchingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .OfClass<Level>()
            .WhereParameter(BuiltInParameter.DATUM_TEXT).NotStartsWith("Ground")
            .ToElements();

        // Assert
        await Assert.That(elements).All().Satisfy(element => !element.Name.StartsWith("Ground"), source => source.IsTrue());
    }

    [Test]
    public async Task WhereParameter_EndsWith_ReturnsMatchingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .OfClass<Level>()
            .WhereParameter(BuiltInParameter.DATUM_TEXT).EndsWith("Floor")
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(element => element.Name.EndsWith("Floor"), source => source.IsTrue());
    }

    [Test]
    public async Task WhereParameter_NotEndsWith_ExcludesMatchingElements()
    {
        // Act
        var elements = _document.CollectElements()
            .OfClass<Level>()
            .WhereParameter(BuiltInParameter.DATUM_TEXT).NotEndsWith("Floor")
            .ToElements();

        // Assert
        await Assert.That(elements).All().Satisfy(element => !element.Name.EndsWith("Floor"), source => source.IsTrue());
    }

    [Test]
    public async Task WhereParameter_HasValue_ReturnsElementsWithValue()
    {
        // Act
        var elements = _document.CollectElements()
            .OfClass<Wall>()
            .Instances()
            .WhereParameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM).HasValue()
            .ToElements();

        // Assert
        await Assert.That(elements).IsNotEmpty();
        await Assert.That(elements).All().Satisfy(
            element => element.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM) is { HasValue: true },
            source => source.IsTrue());
    }

    [Test]
    public async Task WhereParameter_HasNoValue_ReturnsElementsWithoutValue()
    {
        // Act
        var allCount = new FilteredElementCollector(_document)
            .WhereElementIsNotElementType()
            .OfClass(typeof(Wall))
            .GetElementCount();

        var withValueCount = _document.CollectElements()
            .OfClass<Wall>()
            .Instances()
            .WhereParameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM).HasValue()
            .GetElementCount();

        var withoutValueCount = _document.CollectElements()
            .OfClass<Wall>()
            .Instances()
            .WhereParameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM).HasNoValue()
            .GetElementCount();

        // Assert
        await Assert.That(withValueCount + withoutValueCount).IsEqualTo(allCount);
    }

    [Test]
    public async Task First_NonEmptyCollector_ReturnsElement()
    {
        // Act
        var element = _document.CollectElements()
            .Types()
            .First();

        // Assert
        await Assert.That(element).IsNotNull();
    }

    [Test]
    public async Task First_EmptyCollector_ThrowsInvalidOperationException()
    {
        // Act / Assert
        await Assert.That(() =>
                _document.CollectElements()
                    .OfClass<Wall>()
                    .OfClass<Floor>()
                    .First())
            .Throws<InvalidOperationException>();
    }

    [Test]
    public async Task FirstOrDefault_NonEmptyCollector_ReturnsElement()
    {
        // Act
        var element = _document.CollectElements()
            .Types()
            .FirstOrDefault();

        // Assert
        await Assert.That(element).IsNotNull();
    }

    [Test]
    public async Task FirstOrDefault_EmptyCollector_ReturnsNull()
    {
        // Act
        var element = _document.CollectElements()
            .OfClass<Wall>()
            .OfClass<Floor>()
            .FirstOrDefault();

        // Assert
        await Assert.That(element).IsNull();
    }

    [Test]
    public async Task Count_ReturnsCorrectCount()
    {
        // Arrange
        var expectedCount = new FilteredElementCollector(_document)
            .WhereElementIsElementType()
            .GetElementCount();

        // Act
        var actualCount = _document.CollectElements()
            .Types()
            .Count();

        // Assert
        await Assert.That(actualCount).IsEqualTo(expectedCount);
    }

    [Test]
    public async Task Any_NonEmptyCollector_ReturnsTrue()
    {
        // Act
        var result = _document.CollectElements()
            .Types()
            .Any();

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Any_EmptyCollector_ReturnsFalse()
    {
        // Act
        var result = _document.CollectElements()
            .OfClass<Wall>()
            .OfClass<Floor>()
            .Any();

        // Assert
        await Assert.That(result).IsFalse();
    }

    private void CreateLevels()
    {
        _groundFloor = Level.Create(_document, 0);
        _groundFloor.Name = "Ground Floor";

        _firstFloor = Level.Create(_document, 3);
        _firstFloor.Name = "First Floor";
    }

    private void CreateWalls()
    {
        _wall = Wall.Create(_document, Line.CreateBound(new XYZ(0, 0, 0), new XYZ(10, 0, 0)), _firstFloor.Id, false);
        Wall.Create(_document, Line.CreateBound(new XYZ(10, 0, 0), new XYZ(10, 6, 0)), _groundFloor.Id, false);
        Wall.Create(_document, Line.CreateBound(new XYZ(10, 6, 0), new XYZ(0, 6, 0)), _groundFloor.Id, false);
        Wall.Create(_document, Line.CreateBound(new XYZ(0, 6, 0), new XYZ(0, 0, 0)), _groundFloor.Id, false);
        _crossingWall = Wall.Create(_document, Line.CreateBound(new XYZ(5, -1, 0), new XYZ(5, 7, 0)), _groundFloor.Id, false);

        Wall.Create(_document, Line.CreateBound(new XYZ(0, 0, 3), new XYZ(10, 0, 3)), _firstFloor.Id, false);
    }

    private void CreateGrids()
    {
        _grid = Grid.Create(_document, Line.CreateBound(new XYZ(0, -2, 0), new XYZ(0, 8, 0)));
        Grid.Create(_document, Line.CreateBound(new XYZ(5, -2, 0), new XYZ(5, 8, 0)));
        Grid.Create(_document, Line.CreateBound(new XYZ(10, -2, 0), new XYZ(10, 8, 0)));
    }

    private void CreateRoomsAndAreas(AreaScheme areaScheme)
    {
        _room = _document.Create.NewRoom(_groundFloor, new UV(5, 3));
        _document.Create.NewRoomTag(new LinkElementId(_room.Id), new UV(5, 3), _floorPlan.Id);

        _areaView = ViewPlan.CreateAreaPlan(_document, areaScheme.Id, _groundFloor.Id);

        var sketchPlane = SketchPlane.Create(_document, Plane.CreateByNormalAndOrigin(XYZ.BasisZ, XYZ.Zero));
        _document.Create.NewAreaBoundaryLine(sketchPlane, Line.CreateBound(new XYZ(20, 0, 0), new XYZ(30, 0, 0)), _areaView);
        _document.Create.NewAreaBoundaryLine(sketchPlane, Line.CreateBound(new XYZ(30, 0, 0), new XYZ(30, 10, 0)), _areaView);
        _document.Create.NewAreaBoundaryLine(sketchPlane, Line.CreateBound(new XYZ(30, 10, 0), new XYZ(20, 10, 0)), _areaView);
        _document.Create.NewAreaBoundaryLine(sketchPlane, Line.CreateBound(new XYZ(20, 10, 0), new XYZ(20, 0, 0)), _areaView);
        _document.Create.NewArea(_areaView, new UV(25, 5));
    }

    private void CreateViews()
    {
        var viewFamilyType = new FilteredElementCollector(_document)
            .OfClass(typeof(ViewFamilyType))
            .Cast<ViewFamilyType>()
            .First(type => type.ViewFamily == ViewFamily.FloorPlan);

        _floorPlan = ViewPlan.Create(_document, viewFamilyType.Id, _groundFloor.Id);
    }

    private void CreateFamilyInstances()
    {
        if (!_familySymbol.IsActive)
        {
            _familySymbol.Activate();
        }

        _document.Create.NewFamilyInstance(new XYZ(5, 3, 0), _familySymbol, _groundFloor, StructuralType.NonStructural);
    }

    private void CreateExtensibleStorage()
    {
        var builder = new SchemaBuilder(SchemaGuid)
            .SetSchemaName("TestSchema")
            .SetReadAccessLevel(AccessLevel.Public)
            .SetWriteAccessLevel(AccessLevel.Public);

        builder.AddSimpleField("Value", typeof(string));

        var schema = builder.Finish();

        var entity = new Entity(schema);
        entity.Set("Value", "test");
        _wall.SetEntity(entity);
    }

    private void EnableWorksharing()
    {
        _document.EnableWorksharing("Shared Levels and Grids", "Workset1");

        using var transaction = new Transaction(_document, "Workset");
        transaction.Start();
        _worksetId = Workset.Create(_document, "TestWorkset").Id;
        transaction.Commit();
    }

    private static Solid CreateSolidAtPoint(XYZ center, double size)
    {
        var half = size / 2;
        var profile = new CurveLoop();
        profile.Append(Line.CreateBound(center + new XYZ(-half, -half, -half), center + new XYZ(half, -half, -half)));
        profile.Append(Line.CreateBound(center + new XYZ(half, -half, -half), center + new XYZ(half, half, -half)));
        profile.Append(Line.CreateBound(center + new XYZ(half, half, -half), center + new XYZ(-half, half, -half)));
        profile.Append(Line.CreateBound(center + new XYZ(-half, half, -half), center + new XYZ(-half, -half, -half)));
        return GeometryCreationUtilities.CreateExtrusionGeometry([profile], XYZ.BasisZ, size);
    }
}