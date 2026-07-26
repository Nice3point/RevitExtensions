#if REVIT2026_OR_GREATER
using Autodesk.Revit.DB.Structure;
using BenchmarkDotNet.Attributes;
using Nice3point.Revit.Extensions.Benchmarks.Abstractions;

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
// | Method                   | Mean     | Error   | StdDev  | Allocated |
// |------------------------- |---------:|--------:|--------:|----------:|
// | GetAllRebarCrankTypes    | 182.6 μs | 2.00 μs | 1.87 μs |     144 B |
// | FilteredElementCollector | 183.4 μs | 1.60 μs | 1.49 μs |     352 B |


[DocumentSource("Structural")]
public class RebarCrankTypeUtilsBenchmark : RevitDocumentBenchmark
{
    [Benchmark]
    public IList<ElementId> GetAllRebarCrankTypes()
    {
        return RebarCrankTypeUtils.GetAllRebarCrankTypes(Document);
    }

    [Benchmark]
    public ICollection<ElementId> FilteredElementCollector()
    {
        return new FilteredElementCollector(Document)
            .OfCategory(BuiltInCategory.OST_RebarCrankType)
            .ToElementIds();
    }
}
#endif