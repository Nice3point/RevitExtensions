using System.Reflection;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using Nice3point.Revit.Extensions.Benchmarks.Abstractions;
#if NET
using System.Runtime.CompilerServices;
#endif

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
// | Method           | Mean     | Error    | StdDev   | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
// |----------------- |---------:|---------:|---------:|------:|--------:|-------:|-------:|----------:|------------:|
// | ReflectionPinned | 756.0 ns | 14.26 ns | 14.00 ns |  1.97 |    0.04 | 0.0134 | 0.0124 |     704 B |        3.26 |
// | CachedPinned     | 387.9 ns |  4.93 ns |  4.61 ns |  1.01 |    0.01 | 0.0048 | 0.0043 |     240 B |        1.11 |
// | ReflectionUnsafe | 717.1 ns |  9.39 ns |  8.78 ns |  1.87 |    0.02 | 0.0134 | 0.0124 |     680 B |        3.15 |
// | CachedUnsafe     | 383.8 ns |  1.88 ns |  1.67 ns |  1.00 |    0.01 | 0.0043 | 0.0038 |     216 B |        1.00 |

public class ToParameterBenchmark : RevitDocumentBenchmark
{
    private static readonly Assembly Assembly = Assembly.GetAssembly(typeof(Parameter))!;
    private static readonly Type ADocumentType = Assembly.GetType("ADocument")!;
    private static readonly Type ElementIdType = Assembly.GetType("ElementId")!;
    private static readonly MethodInfo GetADocumentMethod = typeof(Document).GetMethod("getADocument", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;
    private static readonly ConstructorInfo ParameterConstructor = typeof(Parameter).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly, null, [ADocumentType.MakePointerType(), ElementIdType.MakePointerType()], null)!;

    [Benchmark]
    public Parameter ReflectionPinned()
    {
        const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

        var documentType = typeof(Document);
        var parameterType = typeof(Parameter);
        var assembly = Assembly.GetAssembly(parameterType)!;
        var aDocumentType = assembly.GetType("ADocument")!;
        var elementIdType = assembly.GetType("ElementId")!;
        var getADocumentMethod = documentType.GetMethod("getADocument", bindingFlags)!;
        var parameterConstructor = parameterType.GetConstructor(bindingFlags, null, [aDocumentType.MakePointerType(), elementIdType.MakePointerType()], null)!;

        var elementId = (long)Arguments.Parameter;
        var aDocument = getADocumentMethod.Invoke(Document, null);

        var handle = GCHandle.Alloc(elementId, GCHandleType.Pinned);
        var parameter = (Parameter)parameterConstructor.Invoke([aDocument, handle.AddrOfPinnedObject()]);
        handle.Free();

        return parameter;
    }

    [Benchmark]
    public Parameter CachedPinned()
    {
        var elementId = (long)Arguments.Parameter;
        var aDocument = GetADocumentMethod.Invoke(Document, null);

        var handle = GCHandle.Alloc(elementId, GCHandleType.Pinned);
        var parameter = (Parameter)ParameterConstructor.Invoke([aDocument, handle.AddrOfPinnedObject()]);
        handle.Free();

        return parameter;
    }
#if NET

    [Benchmark]
    public unsafe Parameter ReflectionUnsafe()
    {
        const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

        var documentType = typeof(Document);
        var parameterType = typeof(Parameter);
        var assembly = Assembly.GetAssembly(parameterType)!;
        var aDocumentType = assembly.GetType("ADocument")!;
        var elementIdType = assembly.GetType("ElementId")!;
        var getADocumentMethod = documentType.GetMethod("getADocument", bindingFlags)!;
        var parameterConstructor = parameterType.GetConstructor(bindingFlags, null, [aDocumentType.MakePointerType(), elementIdType.MakePointerType()], null)!;

        var elementId = (long)Arguments.Parameter;
        var aDocument = getADocumentMethod.Invoke(Document, null);
        var parameter = (Parameter)parameterConstructor.Invoke([aDocument, (nint)Unsafe.AsPointer(ref elementId)]);

        return parameter;
    }

    [Benchmark(Baseline = true)]
    public unsafe Parameter CachedUnsafe()
    {
        var elementId = (long)Arguments.Parameter;
        var aDocument = GetADocumentMethod.Invoke(Document, null);
        var parameter = (Parameter)ParameterConstructor.Invoke([aDocument, (nint)Unsafe.AsPointer(ref elementId)]);

        return parameter;
    }
#endif
}

/// <summary>
///     Provides test values for benchmarks, avoiding early JIT resolution of Revit API structs.
/// </summary>
file static class Arguments
{
    public const BuiltInParameter Parameter = BuiltInParameter.ALL_MODEL_DESCRIPTION;
}