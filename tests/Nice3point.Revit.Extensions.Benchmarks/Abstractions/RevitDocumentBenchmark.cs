using Nice3point.BenchmarkDotNet.Revit;

namespace Nice3point.Revit.Extensions.Benchmarks.Abstractions;

[AttributeUsage(AttributeTargets.Class)]
public sealed class DocumentSourceAttribute(string keyword, string extension) : Attribute
{
    public DocumentSourceAttribute() : this(null, "*.rvt")
    {
    }

    public DocumentSourceAttribute(string keyword) : this(keyword, "*.rvt")
    {
    }

    public string Keyword { get; } = keyword;
    public string Extension { get; } = extension;
}

public abstract class RevitDocumentBenchmark : RevitApiBenchmark
{
    protected Document Document { get; private set; }

    protected override void OnGlobalSetup()
    {
        var sourceAttribute = GetType().GetCustomAttributes(typeof(DocumentSourceAttribute), inherit: false)
            .Cast<DocumentSourceAttribute>()
            .FirstOrDefault();

        if (sourceAttribute is null)
        {
            Document = Application.NewProjectDocument(UnitSystem.Metric);
            return;
        }

        var samplesPath = $@"C:\Program Files\Autodesk\Revit {Application.VersionNumber}\Samples";
        var files = Directory.EnumerateFiles(samplesPath, sourceAttribute.Extension);

        if (sourceAttribute.Keyword is not null)
        {
            files = files.Where(path => path.Contains(sourceAttribute.Keyword, StringComparison.OrdinalIgnoreCase));
        }

        var targetPath = files
            .OrderBy(path => new FileInfo(path).Length)
            .FirstOrDefault();

        if (targetPath is null) throw new InvalidOperationException($"Cannot find a file matching '{sourceAttribute.Keyword ?? sourceAttribute.Extension}' in {samplesPath}");

        Document = Application.OpenDocumentFile(targetPath);
    }

    protected override void OnGlobalCleanup()
    {
        Document.Close(false);
    }
}