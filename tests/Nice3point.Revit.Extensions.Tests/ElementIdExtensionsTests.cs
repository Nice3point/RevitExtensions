using Nice3point.TUnit.Revit;
using Nice3point.TUnit.Revit.Executors;
using TUnit.Core.Executors;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class ElementIdExtensionsTests : RevitApiTest
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
    public async Task AreEquals_BuiltInCategory_MatchingCategory_ReturnsTrue()
    {
        var wallCategoryId = new ElementId(BuiltInCategory.OST_Walls);

        var result = wallCategoryId.AreEquals(BuiltInCategory.OST_Walls);

        await Assert.That(result).IsTrue();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task AreEquals_BuiltInCategory_DifferentCategory_ReturnsFalse()
    {
        var wallCategoryId = new ElementId(BuiltInCategory.OST_Walls);

        var result = wallCategoryId.AreEquals(BuiltInCategory.OST_Doors);

        await Assert.That(result).IsFalse();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task AreEquals_BuiltInParameter_MatchingParameter_ReturnsTrue()
    {
        var parameterId = new ElementId(BuiltInParameter.WALL_BOTTOM_IS_ATTACHED);

        var result = parameterId.AreEquals(BuiltInParameter.WALL_BOTTOM_IS_ATTACHED);

        await Assert.That(result).IsTrue();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task AreEquals_BuiltInParameter_DifferentParameter_ReturnsFalse()
    {
        var parameterId = new ElementId(BuiltInParameter.WALL_BOTTOM_IS_ATTACHED);

        var result = parameterId.AreEquals(BuiltInParameter.WALL_TOP_OFFSET);

        await Assert.That(result).IsFalse();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    [MethodDataSource(nameof(GetSampleFiles))]
    public async Task ToElement_ValidElementId_ReturnsElement(string filePath)
    {
        Document? document = null;

        try
        {
            document = Application.OpenDocumentFile(filePath);

            var elementIds = new FilteredElementCollector(document)
                .WhereElementIsNotElementType()
                .ToElementIds();

            var firstId = elementIds.FirstOrDefault();
            if (firstId is null)
            {
                Skip.Test("No elements found in document");
                return;
            }

            var element = firstId.ToElement(document!);

            await Assert.That(element).IsNotNull();
        }
        finally
        {
            document?.Close(false);
        }
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    [MethodDataSource(nameof(GetSampleFiles))]
    public async Task ToElement_InvalidElementId_ReturnsNull(string filePath)
    {
        Document? document = null;

        try
        {
            document = Application.OpenDocumentFile(filePath);

#if REVIT2024_OR_GREATER
            var invalidId = new ElementId(999999999);
#else
            var invalidId = new ElementId(999999999);
#endif

            var element = invalidId.ToElement(document!);

            await Assert.That(element).IsNull();
        }
        finally
        {
            document?.Close(false);
        }
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    [MethodDataSource(nameof(GetSampleFiles))]
    public async Task ToElementGeneric_ValidElementId_ReturnsTypedElement(string filePath)
    {
        Document? document = null;

        try
        {
            document = Application.OpenDocumentFile(filePath);

            var elementIds = new FilteredElementCollector(document)
                .WhereElementIsElementType()
                .ToElementIds();

            var firstId = elementIds.FirstOrDefault();
            if (firstId is null)
            {
                Skip.Test("No element types found in document");
                return;
            }

            var elementType = firstId.ToElement<ElementType>(document!);

            using (Assert.Multiple())
            {
                await Assert.That(elementType).IsNotNull();
                await Assert.That(elementType).IsAssignableTo<ElementType>();
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
    public async Task ToElements_MultipleElementIds_ReturnsAllElements(string filePath)
    {
        Document? document = null;

        try
        {
            document = Application.OpenDocumentFile(filePath);

            var elementIds = new FilteredElementCollector(document)
                .WhereElementIsNotElementType()
                .ToElementIds()
                .Take(5)
                .ToList();

            var elements = elementIds.ToElements(document!);

            await Assert.That(elements.Count).IsEqualTo(elementIds.Count);
        }
        finally
        {
            document?.Close(false);
        }
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    [MethodDataSource(nameof(GetSampleFiles))]
    public async Task ToElements_EmptyCollection_ReturnsEmptyList(string filePath)
    {
        Document? document = null;

        try
        {
            document = Application.OpenDocumentFile(filePath);

            var elementIds = new List<ElementId>();

            var elements = elementIds.ToElements(document!);

            await Assert.That(elements).IsEmpty();
        }
        finally
        {
            document?.Close(false);
        }
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    [MethodDataSource(nameof(GetSampleFiles))]
    public async Task ToElementsGeneric_MultipleElementIds_ReturnsTypedElements(string filePath)
    {
        Document? document = null;

        try
        {
            document = Application.OpenDocumentFile(filePath);

            var elementIds = new FilteredElementCollector(document)
                .WhereElementIsElementType()
                .ToElementIds()
                .Take(5)
                .ToList();

            var elementTypes = elementIds.ToElements<ElementType>(document!);

            using (Assert.Multiple())
            {
                await Assert.That(elementTypes.Count).IsEqualTo(elementIds.Count);
                await Assert.That(elementTypes).All().Satisfy(source => source.IsAssignableTo<ElementType>());
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
    public async Task ToOrderedElements_MultipleElementIds_PreservesOrder(string filePath)
    {
        Document? document = null;

        try
        {
            document = Application.OpenDocumentFile(filePath);

            var elementIds = new FilteredElementCollector(document)
                .WhereElementIsNotElementType()
                .ToElementIds()
                .Take(5)
                .ToList();

            var orderedElements = elementIds.ToOrderedElements(document!);

            using (Assert.Multiple())
            {
                await Assert.That(orderedElements.Count).IsEqualTo(elementIds.Count);
                for (var i = 0; i < elementIds.Count; i++)
                {
                    await Assert.That(orderedElements[i].Id).IsEqualTo(elementIds[i]);
                }
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
    public async Task ToOrderedElementsGeneric_MultipleElementIds_PreservesOrderAndType(string filePath)
    {
        Document? document = null;

        try
        {
            document = Application.OpenDocumentFile(filePath);

            var elementIds = new FilteredElementCollector(document)
                .WhereElementIsElementType()
                .ToElementIds()
                .Take(5)
                .ToList();

            var orderedElementTypes = elementIds.ToOrderedElements<ElementType>(document!);

            using (Assert.Multiple())
            {
                await Assert.That(orderedElementTypes.Count).IsEqualTo(elementIds.Count);
                for (var i = 0; i < elementIds.Count; i++)
                {
                    await Assert.That(orderedElementTypes[i].Id).IsEqualTo(elementIds[i]);
                }

                await Assert.That(orderedElementTypes).All().Satisfy(source => source.IsAssignableTo<ElementType>());
            }
        }
        finally
        {
            document?.Close(false);
        }
    }
}