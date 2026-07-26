#if NET8_0_OR_GREATER
using System.Reflection;
using System.Runtime.CompilerServices;
using Autodesk.Revit.ApplicationServices;
using BenchmarkDotNet.Attributes;
using Nice3point.BenchmarkDotNet.Revit;
using Application = Autodesk.Revit.ApplicationServices.Application;

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
// | Method                   | Mean      | Error    | StdDev   | Gen0   | Allocated |
// |------------------------- |----------:|---------:|---------:|-------:|----------:|
// | UnsafeAccessorsSingleton |  12.80 ns | 0.121 ns | 0.113 ns | 0.0014 |      72 B |
// | ReflectionSingleton      | 134.38 ns | 1.391 ns | 1.233 ns | 0.0086 |     432 B |

public class UnsafeAccessorsBenchmark : RevitApiBenchmark
{
    [Benchmark]
    public ControlledApplication UnsafeAccessorsSingleton()
    {
        return UnsafeAccessors.CreateControlledApplication(Application);
    }

    [Benchmark]
    public ControlledApplication ReflectionSingleton()
    {
        return (ControlledApplication)Activator.CreateInstance(
            typeof(ControlledApplication),
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            [Application],
            null)!;
    }
}

file static class UnsafeAccessors
{
    [UnsafeAccessor(UnsafeAccessorKind.Constructor)]
    public static extern ControlledApplication CreateControlledApplication(Application application);
}
#endif