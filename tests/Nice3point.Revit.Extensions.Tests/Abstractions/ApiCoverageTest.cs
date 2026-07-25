using System.Collections.Concurrent;
using System.Reflection;
using Nice3point.Revit.Extensions.Tests.Coverage.Discovery;
using Nice3point.Revit.Extensions.Tests.Coverage.Models;
using Nice3point.TUnit.Revit;

namespace Nice3point.Revit.Extensions.Tests.Abstractions;

/// <summary>
///     Supplies report tests with the public static utility surface of an assembly, annotated with the library source files wrapping each method.
/// </summary>
public abstract class ApiCoverageTest : RevitApiTest
{
    private static readonly string LibraryProjectName = typeof(ElementIdExtensions).Assembly.GetName().Name!;
    private static readonly ConcurrentDictionary<Assembly, IReadOnlyList<ApiMethodRow>> RowsByAssembly = new();

    private static SourceFileIndex _librarySourceIndex = null!;

    [Before(HookType.Assembly)]
    public static void BuildLibrarySourceIndex()
    {
        _librarySourceIndex = SourceFileIndex.Build(FindLibrarySourceDirectory());
    }

    /// <summary>
    ///     Scans the assembly once per test session and returns the report rows in discovery order.
    /// </summary>
    /// <param name="assembly">The assembly to report on.</param>
    protected static IReadOnlyList<ApiMethodRow> GetUtilityMethodRows(Assembly assembly)
    {
        return RowsByAssembly.GetOrAdd(assembly, static target => ApiCoverageScanner.ScanUtilityMethods(target, _librarySourceIndex));
    }

    private static string FindLibrarySourceDirectory()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);

        while (directory is not null)
        {
            var sourceDirectory = Path.Combine(directory.FullName, "source", LibraryProjectName);
            if (Directory.Exists(sourceDirectory))
            {
                return sourceDirectory;
            }

            directory = directory.Parent;
        }

        throw new DirectoryNotFoundException($"No 'source/{LibraryProjectName}' directory was found above '{AppContext.BaseDirectory}'.");
    }
}
