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
// | Method           | Mean     | Error   | StdDev  | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
// |----------------- |---------:|--------:|--------:|------:|--------:|-------:|-------:|----------:|------------:|
// | ReflectionPinned | 590.2 ns | 9.30 ns | 8.70 ns |  2.20 |    0.03 | 0.0124 | 0.0114 |     632 B |        3.04 |
// | CachedPinned     | 294.4 ns | 2.69 ns | 2.52 ns |  1.10 |    0.01 | 0.0043 | 0.0038 |     232 B |        1.12 |
// | ReflectionUnsafe | 556.5 ns | 7.24 ns | 6.77 ns |  2.07 |    0.03 | 0.0114 | 0.0105 |     608 B |        2.92 |
// | CachedUnsafe     | 268.3 ns | 1.90 ns | 1.78 ns |  1.00 |    0.01 | 0.0038 | 0.0033 |     208 B |        1.00 |

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class ToCategoryBenchmark : RevitDocumentBenchmark
{
    private static readonly Assembly Assembly = Assembly.GetAssembly(typeof(Category))!;
    private static readonly Type ADocumentType = Assembly.GetType("ADocument")!;
    private static readonly Type ElementIdType = Assembly.GetType("ElementId")!;
    private static readonly MethodInfo GetADocumentMethod = typeof(Document).GetMethod("getADocument", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;

    private static readonly ConstructorInfo CategoryConstructor =
        typeof(Category).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly, null, [ADocumentType.MakePointerType(), ElementIdType.MakePointerType()], null)!;

    [Benchmark]
    public Category ReflectionPinned()
    {
        const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

        var documentType = typeof(Document);
        var categoryType = typeof(Category);
        var assembly = Assembly.GetAssembly(categoryType)!;
        var aDocumentType = assembly.GetType("ADocument")!;
        var elementIdType = assembly.GetType("ElementId")!;
        var getADocumentMethod = documentType.GetMethod("getADocument", bindingFlags)!;
        var categoryConstructor = categoryType.GetConstructor(bindingFlags, null, [aDocumentType.MakePointerType(), elementIdType.MakePointerType()], null)!;

        const long elementId = (long)Arguments.Category;
        var aDocument = getADocumentMethod.Invoke(Document, null);

        var handle = GCHandle.Alloc(elementId, GCHandleType.Pinned);
        var category = (Category)categoryConstructor.Invoke([aDocument, handle.AddrOfPinnedObject()]);
        handle.Free();

        return category;
    }

    [Benchmark]
    public Category CachedPinned()
    {
        const long elementId = (long)Arguments.Category;
        var aDocument = GetADocumentMethod.Invoke(Document, null);

        var handle = GCHandle.Alloc(elementId, GCHandleType.Pinned);
        var category = (Category)CategoryConstructor.Invoke([aDocument, handle.AddrOfPinnedObject()]);
        handle.Free();

        return category;
    }
#if NET
    [Benchmark]
    public unsafe Category ReflectionUnsafe()
    {
        const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

        var documentType = typeof(Document);
        var categoryType = typeof(Category);
        var assembly = Assembly.GetAssembly(categoryType)!;
        var aDocumentType = assembly.GetType("ADocument")!;
        var elementIdType = assembly.GetType("ElementId")!;
        var getADocumentMethod = documentType.GetMethod("getADocument", bindingFlags)!;
        var categoryConstructor = categoryType.GetConstructor(bindingFlags, null, [aDocumentType.MakePointerType(), elementIdType.MakePointerType()], null)!;

        var elementId = (long)Arguments.Category;
        var aDocument = getADocumentMethod.Invoke(Document, null);
        var category = (Category)categoryConstructor.Invoke([aDocument, (nint)Unsafe.AsPointer(ref elementId)]);

        return category;
    }

    [Benchmark(Baseline = true)]
    public unsafe Category CachedUnsafe()
    {
        var elementId = (long)Arguments.Category;
        var aDocument = GetADocumentMethod.Invoke(Document, null);
        var category = (Category)CategoryConstructor.Invoke([aDocument, (nint)Unsafe.AsPointer(ref elementId)]);

        return category;
    }
#endif
}

/// <summary>
///     Provides test values for benchmarks, avoiding early JIT resolution of Revit API structs.
/// </summary>
file static class Arguments
{
    public const BuiltInCategory Category = BuiltInCategory.OST_Walls;
}
