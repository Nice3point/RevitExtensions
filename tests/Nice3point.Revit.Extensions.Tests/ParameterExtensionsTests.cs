using Nice3point.Revit.Extensions.Tests.Abstractions;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class ParameterExtensionsTests : RevitFamilySampleTest
{
    [Test]
    [MethodDataSource(nameof(RevitFamilies))]
    public async Task AsElementOrDefault_MatchingType_ReturnsTypedElement(string path)
    {
        // Arrange
        var document = FamilyDocuments[path];
        var parameter = FindInstanceParameter(document);
        if (parameter is null)
        {
            Skip.Test("No parameter references an element instance in document");
            return;
        }

        // Act
        var element = parameter.AsElementOrDefault<Element>();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(element).IsNotNull();
            await Assert.That(element!.Id).IsEqualTo(parameter.AsElementId());
        }
    }

    [Test]
    [MethodDataSource(nameof(RevitFamilies))]
    public async Task AsElementOrDefault_DifferentType_ReturnsNull(string path)
    {
        // Arrange
        var document = FamilyDocuments[path];
        var parameter = FindInstanceParameter(document);
        if (parameter is null)
        {
            Skip.Test("No parameter references an element instance in document");
            return;
        }

        // Act
        var elementType = parameter.AsElementOrDefault<ElementType>();

        // Assert
        await Assert.That(elementType).IsNull();
    }

    private static Parameter? FindInstanceParameter(Document document)
    {
        var elements = new FilteredElementCollector(document).WhereElementIsNotElementType();
        foreach (var element in elements)
        {
            foreach (Parameter parameter in element.Parameters)
            {
                if (parameter.StorageType != StorageType.ElementId)
                {
                    continue;
                }

                var referencedElement = document.GetElement(parameter.AsElementId());
                if (referencedElement is not null and not ElementType)
                {
                    return parameter;
                }
            }
        }

        return null;
    }
}
