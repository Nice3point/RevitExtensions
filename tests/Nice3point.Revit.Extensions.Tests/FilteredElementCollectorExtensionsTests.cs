using Nice3point.TUnit.Revit;
using Nice3point.TUnit.Revit.Executors;
using TUnit.Core.Executors;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class FilteredElementCollectorExtensionsTests : RevitApiTest
{
    private static readonly string SamplesPath = $@"C:\Program Files\Autodesk\Revit {Application.VersionNumber}\Samples";

    [Before(Class)]
    public static void ValidateSamples()
    {
        if (!Directory.Exists(SamplesPath))
        {
            Skip.Test($"Samples folder not found at {SamplesPath}");
            return;
        }

        if (!Directory.EnumerateFiles(SamplesPath, "*.rfa").Any())
        {
            Skip.Test($"No .rfa files found in {SamplesPath}");
        }
    }

    public static IEnumerable<string> GetSampleFiles()
    {
        if (!Directory.Exists(SamplesPath))
        {
            yield return string.Empty;
            yield break;
        }

        foreach (var file in Directory.EnumerateFiles(SamplesPath, "*.rfa")) yield return file;
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    [MethodDataSource(nameof(GetSampleFiles))]
    public async Task GetInstances_NoFilter_ReturnsElements(string filePath)
    {
        Document? document = null;

        try
        {
            document = Application.OpenDocumentFile(filePath);

            var instances = document!.GetInstances();

            await Assert.That(instances).IsNotEmpty();
        }
        finally
        {
            document?.Close(false);
        }
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    [MethodDataSource(nameof(GetSampleFiles))]
    public async Task GetInstances_WithCategory_ReturnsFilteredElements(string filePath)
    {
        Document? document = null;

        try
        {
            document = Application.OpenDocumentFile(filePath);

            var instances = document!.GetInstances(BuiltInCategory.OST_Dimensions);

            if (!instances.Any())
            {
                Skip.Test("No dimensions found in document");
                return;
            }

            using (Assert.Multiple())
            {
                await Assert.That(instances).IsNotEmpty();
                await Assert.That(instances)
                    .All()
                    .Satisfy(element => element.Category?.Id.AreEquals(BuiltInCategory.OST_Dimensions),
                        source => source.IsTrue());
            }
        }
        finally
        {
            document?.Close(false);
        }
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    [MethodDataSource(nameof(GetSampleFiles))]
    public async Task GetTypes_NoFilter_ReturnsElementTypes(string filePath)
    {
        Document? document = null;

        try
        {
            document = Application.OpenDocumentFile(filePath);

            var types = document!.GetTypes();

            using (Assert.Multiple())
            {
                await Assert.That(types).IsNotEmpty();
                await Assert.That(types).All().Satisfy(source => source.IsAssignableTo<ElementType>());
            }
        }
        finally
        {
            document?.Close(false);
        }
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    [MethodDataSource(nameof(GetSampleFiles))]
    public async Task EnumerateInstances_NoFilter_ReturnsEnumerable(string filePath)
    {
        Document? document = null;

        try
        {
            document = Application.OpenDocumentFile(filePath);

            var instances = document!.EnumerateInstances().ToArray;

            using (Assert.Multiple())
            {
                await Assert.That(instances).IsNotEmpty();
                await Assert.That(instances).All().Satisfy(element => element.IsValidObject, source => source.IsTrue());
            }
        }
        finally
        {
            document?.Close(false);
        }
    }
}