using Nice3point.Revit.Injector;
using Nice3point.TUnit.Revit;
using Nice3point.TUnit.Revit.Executors;
using TUnit.Core.Executors;

namespace Nice3point.Revit.Extensions.Tests.Abstractions;

/// <summary>
///     Supplies tests with an isolated copy of the smallest project sample installed with Revit.
/// </summary>
public class RevitModelSampleTest : RevitApiTest
{
    private static readonly string SamplesPath = $@"C:\Program Files\Autodesk\Revit {RevitEnvironment.MajorVersion}\Samples";

    /// <summary>
    ///     The opened documents, keyed by the path of the sample they copy.
    /// </summary>
    private protected Dictionary<string, Document> ModelDocuments { get; } = [];

    /// <summary>
    ///     The path of the smallest installed project sample. The array is empty when Revit ships no samples directory.
    /// </summary>
    public static string[] RevitModels { get; } = Directory.Exists(SamplesPath)
        ? Directory.EnumerateFiles(SamplesPath, "*.rvt")
            .Select(path => new FileInfo(path))
            .OrderBy(file => file.Length)
            .Take(1)
            .Select(file => file.FullName)
            .ToArray()
        : [];

    /// <summary>
    ///     Copies every project sample to a temporary file and opens it with failure suppression.
    /// </summary>
    [Before(Test)]
    [HookExecutor<RevitThreadExecutor>]
    public void OpenDocuments()
    {
        foreach (var path in RevitModels)
        {
            var isolatedPath = Path.Combine(Path.GetTempPath(), $"{Path.GetRandomFileName()}.rvt");
            File.Copy(path, isolatedPath);

            using (RevitApiContext.BeginFailureSuppressionScope())
            {
                ModelDocuments[path] = Application.OpenDocumentFile(isolatedPath);
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
        foreach (var document in ModelDocuments.Values)
        {
            var filePath = document.PathName;
            document.Close(false);

            File.SetAttributes(filePath, FileAttributes.Normal);
            File.Delete(filePath);
        }
    }
}
