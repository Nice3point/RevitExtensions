using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Nice3point.BenchmarkDotNet.Revit;

namespace Nice3point.Revit.Extensions.Benchmarks.Benchmarks;

// ```
//
// BenchmarkDotNet v0.15.8, Windows 11 (10.0.28000.2269/26H1/2026Update)
// AMD Ryzen 9 9950X3D 4.30GHz, 1 CPU, 32 logical and 16 physical cores
// .NET SDK 10.0.302
//   [Host]     : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v4
//   Job-AAUCHH : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v4
//
// BuildConfiguration=Release.R27  
//
// ```
//
// | Method                                 |         Mean |     Error |    StdDev |   Gen0 |   Gen1 | Allocated |
// |----------------------------------------|-------------:|----------:|----------:|-------:|-------:|----------:|
// | OfClass()                              |           NA |        NA |        NA |     NA |     NA |        NA |
// | OfCategory(BuiltInCategory)            |   193.637 μs | 2.0085 μs | 1.8787 μs |      - |      - |    1016 B |
// | OfClass().OfCategory(BuiltInCategory)  |   212.730 μs | 1.4992 μs | 1.3290 μs |      - |      - |    1096 B |
// | OfCategory(BuiltInCategory).OfClass()  |   212.777 μs | 1.2869 μs | 1.2038 μs |      - |      - |    1096 B |
// |                                        |              |           |           |        |        |           |
// | AllInstances                           |   206.040 μs | 1.6507 μs | 1.5441 μs |      - |      - |     968 B |
// | ViewInstances                          |    32.641 μs | 0.2438 μs | 0.2161 μs |      - |      - |    1096 B |
// |                                        |              |           |           |        |        |           |
// | GetElementCount()                      |    84.116 μs | 1.4377 μs | 1.3449 μs |      - |      - |     160 B |
// | ToElementIds().Count                   |   101.958 μs | 1.5332 μs | 1.4341 μs | 0.2441 |      - |   16120 B |
// | Enumerable.Count()                     |    73.924 μs | 0.3590 μs | 0.3358 μs |      - |      - |     216 B |
// |                                        |              |           |           |        |        |           |
// | OfClass().Instances()                  |   208.276 μs | 3.2878 μs | 3.0754 μs |      - |      - |     968 B |
// | Instances().OfClass()                  |   207.560 μs | 1.7297 μs | 1.5334 μs |      - |      - |     968 B |
// |                                        |              |           |           |        |        |           |
// | FirstElement()                         |     1.597 μs | 0.0257 μs | 0.0240 μs | 0.0038 | 0.0019 |     240 B |
// | Enumerable.FirstOrDefault()            |     1.922 μs | 0.0283 μs | 0.0265 μs | 0.0038 |      - |     296 B |
// |                                        |              |           |           |        |        |           |
// | FirstElementId().Value&gt;0            |     1.199 μs | 0.0100 μs | 0.0084 μs | 0.0038 | 0.0019 |     208 B |
// | Enumerable.Any()                       |     1.454 μs | 0.0104 μs | 0.0087 μs | 0.0038 | 0.0019 |     216 B |
// |                                        |              |           |           |        |        |           |
// | ToElementIds()                         |   101.522 μs | 0.8912 μs | 0.7900 μs | 0.2441 |      - |   16120 B |
// | ToElements()                           |   160.315 μs | 1.1853 μs | 1.1087 μs | 0.2441 |      - |   24024 B |
// | Cast().ToList()                        |   189.820 μs | 2.6847 μs | 2.5113 μs | 0.2441 |      - |   24296 B |
// | OfType().ToList()                      |   188.597 μs | 1.8987 μs | 1.7760 μs | 0.2441 |      - |   22056 B |
// |                                        |              |           |           |        |        |           |
// | OfCategories()                         |   303.948 μs | 3.8863 μs | 3.6353 μs |      - |      - |    1704 B |
// | OfClasses()                            |   380.985 μs | 3.8366 μs | 3.5888 μs |      - |      - |    1904 B |
// | OfClass().UnionWith().OfClass()        | 1,002.570 μs | 7.8003 μs | 6.9148 μs |      - |      - |    1800 B |
// |                                        |              |           |           |        |        |           |
// | WhereParameter().NotEquals(ElementId)  |   236.002 μs | 1.7211 μs | 1.5257 μs |      - |      - |    1064 B |
// | Enumerable.Where(AsElementId)          |   219.541 μs | 2.3420 μs | 2.1907 μs |      - |      - |    2112 B |
// | WhereParameter().IsGreaterThan(double) |   216.566 μs | 1.3526 μs | 1.1991 μs |      - |      - |    1496 B |
// | Enumerable.Where(AsDouble)             |   220.858 μs | 1.4825 μs | 1.3142 μs |      - |      - |    1512 B |
// | WhereParameter().Contains(string)      |    75.580 μs | 1.3009 μs | 1.2169 μs |      - |      - |    1208 B |
// | Enumerable.Where(Name.Contains)        |    75.589 μs | 0.6855 μs | 0.6412 μs |      - |      - |    1112 B |
// |                                        |              |           |           |        |        |           |
// | OnLevel()                              |   325.402 μs | 3.7340 μs | 3.3101 μs |      - |      - |    1080 B |
// | OfCategory(BuiltInCategory).OnLevel()  |   408.403 μs | 4.4977 μs | 4.2072 μs |      - |      - |    1304 B |
// | OnLevel().OfCategory(BuiltInCategory)  |   409.903 μs | 6.0610 μs | 5.6695 μs |      - |      - |    1304 B |

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class FilteredElementCollectorBenchmark : RevitApiBenchmark
{
    private Document _document = null!;
    private Level _groundFloor = null!;
    private Level _firstFloor = null!;
    private View _floorPlan = null!;

    protected override void OnGlobalSetup()
    {
        _document = Application.NewProjectDocument(UnitSystem.Metric);

        using (RevitApiContext.BeginFailureSuppressionScope())
        {
            using var transaction = new Transaction(_document, "Seed model");
            transaction.Start();

            CreateLevels();
            CreateWalls();
            CreateGrids();
            CreateViews();

            transaction.Commit();
        }
    }

    protected override void OnGlobalCleanup()
    {
        _document?.Close(false);
    }

    [Benchmark(Description = "FirstElement()")]
    [BenchmarkCategory("FirstElement")]
    public Element FirstElement()
    {
        return new FilteredElementCollector(_document)
            .WhereElementIsElementType()
            .FirstElement();
    }

    [Benchmark(Description = "Enumerable.FirstOrDefault()")]
    [BenchmarkCategory("FirstElement")]
    public Element LinqFirstOrDefault()
    {
        return Enumerable.FirstOrDefault(
            new FilteredElementCollector(_document)
                .WhereElementIsElementType());
    }

    [Benchmark(Description = "GetElementCount()")]
    [BenchmarkCategory("ElementCount")]
    public int GetElementCount()
    {
        return new FilteredElementCollector(_document)
            .WhereElementIsElementType()
            .GetElementCount();
    }

    [Benchmark(Description = "ToElementIds().Count")]
    [BenchmarkCategory("ElementCount")]
    public int ToElementIdsCount()
    {
        return new FilteredElementCollector(_document)
            .WhereElementIsElementType()
            .ToElementIds()
            .Count;
    }

    [Benchmark(Description = "Enumerable.Count()")]
    [BenchmarkCategory("ElementCount")]
    public int LinqCount()
    {
        return Enumerable.Count(
            new FilteredElementCollector(_document)
                .WhereElementIsElementType());
    }

    [Benchmark(Description = "FirstElementId().Value>0")]
    [BenchmarkCategory("HasElements")]
    public bool Any()
    {
        return new FilteredElementCollector(_document)
            .WhereElementIsElementType()
#if REVIT2024_OR_GREATER
            .FirstElementId().Value > 0;
#else
            .FirstElementId().IntegerValue > 0;
#endif
    }

    [Benchmark(Description = "Enumerable.Any()")]
    [BenchmarkCategory("HasElements")]
    public bool LinqAny()
    {
        return Enumerable.Any(
            new FilteredElementCollector(_document)
                .WhereElementIsElementType());
    }

    [Benchmark(Description = "ToElementIds()")]
    [BenchmarkCategory("Materialization")]
    public ICollection<ElementId> ToElementIds()
    {
        return new FilteredElementCollector(_document)
            .WhereElementIsElementType()
            .ToElementIds();
    }

    [Benchmark(Description = "ToElements()")]
    [BenchmarkCategory("Materialization")]
    public IList<Element> ToElements()
    {
        return new FilteredElementCollector(_document)
            .WhereElementIsElementType()
            .ToElements();
    }

    [Benchmark(Description = "Cast<T>().ToList()")]
    [BenchmarkCategory("Materialization")]
    public List<ElementType> CastToList()
    {
        return new FilteredElementCollector(_document)
            .WhereElementIsElementType()
            .Cast<ElementType>()
            .ToList();
    }

    [Benchmark(Description = "OfType<T>().ToList()")]
    [BenchmarkCategory("Materialization")]
    public List<ElementType> OfTypeToList()
    {
        return new FilteredElementCollector(_document)
            .WhereElementIsElementType()
            .OfType<ElementType>()
            .ToList();
    }

    [Benchmark(Description = "OfClass<T>().Instances()")]
    [BenchmarkCategory("FilterOrder")]
    public IList<Element> ClassThenInstances()
    {
        return _document.CollectElements()
            .OfClass<Wall>()
            .Instances()
            .ToElements();
    }

    [Benchmark(Description = "Instances().OfClass<T>()")]
    [BenchmarkCategory("FilterOrder")]
    public IList<Element> InstancesThenClass()
    {
        return _document.CollectElements()
            .Instances()
            .OfClass<Wall>()
            .ToElements();
    }

    [Benchmark(Description = "OfClass<T>()")]
    [BenchmarkCategory("ClassVsCategory")]
    public IList<Element> ClassFilter()
    {
        return _document.CollectElements()
            .Instances()
            .OfClass<Wall>()
            .ToElements();
    }

    [Benchmark(Description = "OfCategory(BuiltInCategory)")]
    [BenchmarkCategory("ClassVsCategory")]
    public IList<Element> CategoryFilter()
    {
        return new FilteredElementCollector(_document)
            .Instances()
            .OfCategory(BuiltInCategory.OST_Walls)
            .ToElements();
    }

    [Benchmark(Description = "OfClass<T>().OfCategory(BuiltInCategory)")]
    [BenchmarkCategory("ClassVsCategory")]
    public IList<Element> ClassThenCategory()
    {
        return _document.CollectElements()
            .Instances()
            .OfClass<Wall>()
            .OfCategory(BuiltInCategory.OST_Walls)
            .ToElements();
    }

    [Benchmark(Description = "OfCategory(BuiltInCategory).OfClass<T>()")]
    [BenchmarkCategory("ClassVsCategory")]
    public IList<Element> CategoryThenClass()
    {
        return new FilteredElementCollector(_document)
            .Instances()
            .OfCategory(BuiltInCategory.OST_Walls)
            .OfClass<Wall>()
            .ToElements();
    }

    [Benchmark(Description = "WhereParameter().NotEquals(ElementId)")]
    [BenchmarkCategory("ParameterFilter")]
    public IList<Element> ParameterNotEqualsElementId()
    {
        return _document.CollectElements()
            .Instances()
            .OfClass<Wall>()
            .WhereParameter(BuiltInParameter.WALL_BASE_CONSTRAINT).NotEquals(_groundFloor.Id)
            .ToElements();
    }

    [Benchmark(Description = "Enumerable.Where(AsElementId)")]
    [BenchmarkCategory("ParameterFilter")]
    public List<Element> LinqWhereElementId()
    {
        return _document.CollectElements()
            .Instances()
            .OfClass<Wall>()
            .Where(element => element.get_Parameter(BuiltInParameter.WALL_BASE_CONSTRAINT)?.AsElementId() != _groundFloor.Id)
            .ToList();
    }

    [Benchmark(Description = "WhereParameter().IsGreaterThan(double)")]
    [BenchmarkCategory("ParameterFilter")]
    public IList<Element> ParameterGreaterThanDouble()
    {
        return _document.CollectElements()
            .Instances()
            .OfClass<Wall>()
            .WhereParameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM).IsGreaterThan(0d, 1e-9)
            .ToElements();
    }

    [Benchmark(Description = "Enumerable.Where(AsDouble)")]
    [BenchmarkCategory("ParameterFilter")]
    public List<Element> LinqWhereDouble()
    {
        return _document.CollectElements()
            .Instances()
            .OfClass<Wall>()
            .Where(element => element.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM)?.AsDouble() > 0d)
            .ToList();
    }

    [Benchmark(Description = "WhereParameter().Contains(string)")]
    [BenchmarkCategory("ParameterFilter")]
    public IList<Element> ParameterContainsString()
    {
        return _document.CollectElements()
            .Types()
            .OfClass<WallType>()
            .WhereParameter(BuiltInParameter.ALL_MODEL_TYPE_NAME).Contains("Wall")
            .ToElements();
    }

    [Benchmark(Description = "Enumerable.Where(Name.Contains)")]
    [BenchmarkCategory("ParameterFilter")]
    public List<Element> LinqWhereNameContains()
    {
        return _document.CollectElements()
            .Types()
            .OfClass<WallType>()
            .Where(element => element.get_Parameter(BuiltInParameter.ALL_MODEL_TYPE_NAME).AsString().Contains("Wall"))
            .ToList();
    }

    [Benchmark(Description = "OnLevel()")]
    [BenchmarkCategory("QuickVsSlowFilter")]
    public IList<Element> LevelFilterAlone()
    {
        return _document.CollectElements()
            .Instances()
            .OnLevel(_groundFloor)
            .ToElements();
    }

    [Benchmark(Description = "OfCategory(BuiltInCategory).OnLevel()")]
    [BenchmarkCategory("QuickVsSlowFilter")]
    public IList<Element> CategoryThenLevel()
    {
        return _document.CollectElements()
            .Instances()
            .OfCategories(BuiltInCategory.OST_Walls)
            .OnLevel(_groundFloor)
            .ToElements();
    }

    [Benchmark(Description = "OnLevel().OfCategory(BuiltInCategory)")]
    [BenchmarkCategory("QuickVsSlowFilter")]
    public IList<Element> LevelThenCategory()
    {
        return _document.CollectElements()
            .Instances()
            .OnLevel(_groundFloor)
            .OfCategories(BuiltInCategory.OST_Walls)
            .ToElements();
    }

    [Benchmark(Description = "AllInstances")]
    [BenchmarkCategory("CollectorScope")]
    public IList<Element> AllInstances()
    {
        return _document.CollectElements()
            .OfClass<Wall>()
            .Instances()
            .ToElements();
    }

    [Benchmark(Description = "ViewInstances")]
    [BenchmarkCategory("CollectorScope")]
    public IList<Element> ViewInstances()
    {
        return new FilteredElementCollector(_document, _floorPlan.Id)
            .OfClass<Wall>()
            .Instances()
            .ToElements();
    }

    [Benchmark(Description = "OfCategories()")]
    [BenchmarkCategory("MultiFilter")]
    public IList<Element> MultiCategoryFilter()
    {
        return _document.CollectElements()
            .Instances()
            .OfCategories(BuiltInCategory.OST_Walls, BuiltInCategory.OST_Grids, BuiltInCategory.OST_Levels)
            .ToElements();
    }

    [Benchmark(Description = "OfClasses()")]
    [BenchmarkCategory("MultiFilter")]
    public IList<Element> MultiClassFilter()
    {
        return _document.CollectElements()
            .OfClasses(typeof(Wall), typeof(Grid), typeof(Level))
            .ToElements();
    }

    [Benchmark(Description = "OfClass().UnionWith().OfClass()")]
    [BenchmarkCategory("MultiFilter")]
    public ICollection<ElementId> UnionWithFilter()
    {
        var walls = new FilteredElementCollector(_document).OfClass(typeof(Wall));
        var grids = new FilteredElementCollector(_document).OfClass(typeof(Grid));
        var levels = new FilteredElementCollector(_document).OfClass(typeof(Level));

        return walls.UnionWith(grids).UnionWith(levels).ToElementIds();
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
        Wall.Create(_document, Line.CreateBound(new XYZ(0, 0, 0), new XYZ(10, 0, 0)), _groundFloor.Id, false);
        Wall.Create(_document, Line.CreateBound(new XYZ(10, 0, 0), new XYZ(10, 6, 0)), _groundFloor.Id, false);
        Wall.Create(_document, Line.CreateBound(new XYZ(10, 6, 0), new XYZ(0, 6, 0)), _groundFloor.Id, false);
        Wall.Create(_document, Line.CreateBound(new XYZ(0, 6, 0), new XYZ(0, 0, 0)), _groundFloor.Id, false);
        Wall.Create(_document, Line.CreateBound(new XYZ(5, -1, 0), new XYZ(5, 7, 0)), _groundFloor.Id, false);

        Wall.Create(_document, Line.CreateBound(new XYZ(0, 0, 3), new XYZ(10, 0, 3)), _firstFloor.Id, false);
    }

    private void CreateGrids()
    {
        Grid.Create(_document, Line.CreateBound(new XYZ(0, -2, 0), new XYZ(0, 8, 0)));
        Grid.Create(_document, Line.CreateBound(new XYZ(5, -2, 0), new XYZ(5, 8, 0)));
        Grid.Create(_document, Line.CreateBound(new XYZ(10, -2, 0), new XYZ(10, 8, 0)));
    }

    private void CreateViews()
    {
        var viewFamilyType = new FilteredElementCollector(_document)
            .OfClass(typeof(ViewFamilyType))
            .Cast<ViewFamilyType>()
            .First(type => type.ViewFamily == ViewFamily.FloorPlan);

        _floorPlan = ViewPlan.Create(_document, viewFamilyType.Id, _groundFloor.Id);
    }
}