using Nice3point.Revit.Injector;
using Nice3point.TUnit.Revit;
using Nice3point.TUnit.Revit.Executors;
using TUnit.Core.Executors;

namespace Nice3point.Revit.Extensions.Tests.Abstractions;

/// <summary>
///     Supplies tests with an isolated copy of every family sample installed with Revit.
/// </summary>
public class RevitFamilySampleTest : RevitApiTest
{
    private static readonly string SamplesPath = $@"C:\Program Files\Autodesk\Revit {RevitEnvironment.MajorVersion}\Samples";

    /// <summary>
    ///     The opened documents, keyed by the path of the sample they copy.
    /// </summary>
    private protected Dictionary<string, Document> FamilyDocuments { get; } = [];

    /// <summary>
    ///     The paths of the installed family samples. The array is empty when Revit ships no samples directory.
    /// </summary>
    public static string[] RevitFamilies { get; } = Directory.Exists(SamplesPath) ? Directory.EnumerateFiles(SamplesPath, "*.rfa").ToArray() : [];

    /// <summary>
    ///     Copies every family sample to a temporary file and opens it with failure suppression.
    /// </summary>
    [Before(Test)]
    [HookExecutor<RevitThreadExecutor>]
    public void OpenDocuments()
    {
        foreach (var path in RevitFamilies)
        {
            var isolatedPath = Path.Combine(Path.GetTempPath(), $"{Path.GetRandomFileName()}.rfa");
            File.Copy(path, isolatedPath);

            using (RevitApiContext.BeginFailureSuppressionScope())
            {
                FamilyDocuments[path] = Application.OpenDocumentFile(isolatedPath);
            }
        }
    }

    /// <summary>
    ///     Closes every opened document and deletes its temporary copy.
    /// </summary>
    [After(Test)]
    [HookExecutor<RevitThreadExecutor>]
    public void CloseDocuments()
    {
        foreach (var document in FamilyDocuments.Values)
        {
            var filePath = document.PathName;
            document.Close(false);

            File.SetAttributes(filePath, FileAttributes.Normal);
            File.Delete(filePath);
        }
    }
}