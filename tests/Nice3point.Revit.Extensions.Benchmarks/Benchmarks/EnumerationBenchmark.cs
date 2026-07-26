using System.Collections;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using Nice3point.BenchmarkDotNet.Revit;

namespace Nice3point.Revit.Extensions.Benchmarks.Benchmarks;

// ```
//
// BenchmarkDotNet v0.15.8, Windows 11 (10.0.28000.2269/26H1/2026Update)
// AMD Ryzen 9 9950X3D 4.30GHz, 1 CPU, 32 logical and 16 physical cores
// .NET SDK 10.0.302
// [Host]     : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v4
// Job-AAUCHH : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v4
//
// BuildConfiguration=Release.R27  
//
// ```
//
// | Method                       |         Mean |       Error |      StdDev |   Gen0 |   Gen1 | Allocated |
// |------------------------------|-------------:|------------:|------------:|-------:|-------:|----------:|
// | Iterator over the LINQ       | 108,364.1 ns | 1,368.18 ns | 1,279.80 ns | 0.6104 | 0.4883 |   32120 B |
// | Iterator over the enumerator | 108,987.6 ns | 1,619.25 ns | 1,514.65 ns | 0.6104 | 0.4883 |   32120 B |
// | Iterator over the index      |  97,151.5 ns | 1,930.72 ns | 2,298.38 ns | 0.6104 | 0.4883 |   32000 B |
// |                              |              |             |             |        |        |           |
// | Select over the pairs        |     209.3 ns |     0.95 ns |     0.84 ns | 0.0036 |      - |     184 B |
// | Iterator over the key        |     147.3 ns |     1.41 ns |     1.32 ns | 0.0024 |      - |     120 B |
// |                              |              |             |             |        |        |           |
// | Select over the pairs        |     209.0 ns |     1.14 ns |     1.01 ns | 0.0036 |      - |     184 B |
// | Iterator over the value      |     160.9 ns |     1.07 ns |     0.95 ns | 0.0024 |      - |     120 B |
// |                              |              |             |             |        |        |           |
// | Iterator over the LINQ       |  40,547.6 ns |   684.91 ns |   640.66 ns | 0.1831 | 0.1221 |    9552 B |
// | Iterator over the enumerator |  38,521.1 ns |   744.90 ns |   696.78 ns | 0.1831 | 0.1221 |    9552 B |

/// <summary>
///     Compares the ways of carrying the element type of a Revit API collection or map into a typed sequence.
/// </summary>
/// <remarks>
///     An array exposes <c>Size</c> and an indexed <c>Item</c> property, a set exposes neither and yields its elements through an enumerator only.
///     A map keeps the key of the current entry on the iterator and the value on <c>Current</c>; reading a pair costs two interop calls, reading one side of it costs one.
/// </remarks>
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class EnumerationBenchmark : RevitApiBenchmark
{
    private readonly Consumer _consumer = new();
    
    private const int CurveCount = 1000;

    private Document _document = null!;
    private CurveArray _curveArray = null!;
    private CategorySet _categorySet = null!;
    private ParameterMap _parameterMap = null!;

    protected override void OnGlobalSetup()
    {
        _document = Application.NewProjectDocument(UnitSystem.Metric);

        _curveArray = Application.Create.NewCurveArray();
        for (var index = 0; index < CurveCount; index++)
        {
            _curveArray.Append(Line.CreateBound(new XYZ(index, 0, 0), new XYZ(index, 1, 0)));
        }

        _categorySet = Application.Create.NewCategorySet();
        foreach (Category category in _document.Settings.Categories)
        {
            _categorySet.Insert(category);
        }

        _parameterMap = new FilteredElementCollector(_document)
            .OfClass(typeof(Level))
            .Cast<Level>()
            .First()
            .ParametersMap;
    }

    protected override void OnGlobalCleanup()
    {
        _document?.Close(false);
    }

    [Benchmark(Description = "Iterator over the LINQ")]
    [BenchmarkCategory("Array")]
    public void ArrayCast()
    {
        _curveArray.Cast<Curve>().Consume(_consumer);
    }

    [Benchmark(Description = "Iterator over the enumerator")]
    [BenchmarkCategory("Array")]
    public void ArrayEnumerator()
    {
        EnumerateThroughEnumerator<Curve>(_curveArray).Consume(_consumer);
    }

    [Benchmark(Description = "Iterator over the index")]
    [BenchmarkCategory("Array")]
    public void ArrayIndex()
    {
        EnumerateThroughIndex(_curveArray).Consume(_consumer);
    }

    [Benchmark(Description = "Iterator over the LINQ")]
    [BenchmarkCategory("Set")]
    public void SetCast()
    {
        _categorySet.Cast<Category>().Consume(_consumer);
    }

    [Benchmark(Description = "Iterator over the enumerator")]
    [BenchmarkCategory("Set")]
    public void SetEnumerator()
    {
        EnumerateThroughEnumerator<Category>(_categorySet).Consume(_consumer);
    }

    [Benchmark(Description = "Select over the pairs")]
    [BenchmarkCategory("MapKeys")]
    public void MapKeysProjected()
    {
        EnumerateEntries(_parameterMap)
            .Select(entry => entry.Name)
            .Consume(_consumer);
    }

    [Benchmark(Description = "Iterator over the key")]
    [BenchmarkCategory("MapKeys")]
    public void MapKeysDirect()
    {
        _parameterMap.EnumerateKeys().Consume(_consumer);
    }

    [Benchmark(Description = "Select over the pairs")]
    [BenchmarkCategory("MapValues")]
    public void MapValuesProjected()
    {
        EnumerateEntries(_parameterMap)
            .Select(entry => entry.Parameter)
            .Consume(_consumer);
    }

    [Benchmark(Description = "Iterator over the value")]
    [BenchmarkCategory("MapValues")]
    public void MapValuesDirect()
    {
        _parameterMap.EnumerateValues().Consume(_consumer);
    }

    private static IEnumerable<T> EnumerateThroughEnumerator<T>(IEnumerable source)
    {
        foreach (T item in source)
        {
            yield return item;
        }
    }

    private static IEnumerable<Curve> EnumerateThroughIndex(CurveArray array)
    {
        var count = array.Size;

        for (var index = 0; index < count; index++)
        {
            yield return array.get_Item(index);
        }
    }

    private static IEnumerable<(string Name, Parameter Parameter)> EnumerateEntries(ParameterMap map)
    {
        using var iterator = map.ForwardIterator();

        while (iterator.MoveNext())
        {
            yield return (iterator.Key, (Parameter)iterator.Current!);
        }
    }
}
