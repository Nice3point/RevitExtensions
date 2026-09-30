using Nice3point.Revit.Injector;
using Nice3point.TUnit.Revit;
using Nice3point.TUnit.Revit.Executors;
using TUnit.Core.Executors;

namespace Nice3point.Revit.Extensions.Tests.Abstractions;

/// <summary>
///     Provides read-only tests with every family sample installed with Revit.
/// </summary>
/// <remarks>
///     The samples open once per test class, and every test of the class reads the same documents.
/// </remarks>
public class RevitFamilySampleTest : RevitApiTest
{
    private static readonly string SamplesPath = $@"C:\Program Files\Autodesk\Revit {RevitEnvironment.MajorVersion}\Samples";

    /// <summary>
    ///     Gets the opened documents, keyed by the path of their sample.
    /// </summary>
    private protected static Dictionary<string, Document> FamilyDocuments => field ??= [];

    /// <summary>
    ///     Gets the paths of the installed family samples, or an empty array when Revit ships no samples directory.
    /// </summary>
    public static string[] RevitFamilies { get; } = Directory.Exists(SamplesPath) ? Directory.EnumerateFiles(SamplesPath, "*.rfa").ToArray() : [];

    /// <summary>
    ///     Opens every family sample with failure suppression.
    /// </summary>
    [Before(Class)]
    [HookExecutor<RevitThreadExecutor>]
    public static void OpenDocuments()
    {
        foreach (var path in RevitFamilies)
        {
            using (RevitApiContext.BeginFailureSuppressionScope())
            {
                FamilyDocuments[path] = Application.OpenDocumentFile(path);
            }
        }
    }

    /// <summary>
    ///     Closes every opened document without saving it.
    /// </summary>
    [After(Class)]
    [HookExecutor<RevitThreadExecutor>]
    public static void CloseDocuments()
    {
        foreach (var document in FamilyDocuments.Values)
        {
            document.Close(false);
        }

        FamilyDocuments.Clear();
    }
}
