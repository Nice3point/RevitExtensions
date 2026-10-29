using Nice3point.Revit.Extensions.Tests.Abstractions;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class ParameterExtensionsTests : RevitFamilySampleTest
{
    [Test]
    [MethodDataSource(nameof(RevitFamilies))]
    public async Task AsElement_NoElement_ThrowsInvalidOperationException(string path)
    {
        // Arrange
        var document = FamilyDocuments[path];
        var parameter = FindElementIdParameter(document, static element => element is null);
        if (parameter is null)
        {
            Skip.Test("No parameter references no element in document");
            return;
        }

        // Act & Assert
        await Assert.That(parameter.AsElement).Throws<InvalidOperationException>();
    }

    [Test]
    [MethodDataSource(nameof(RevitFamilies))]
    public async Task AsElementGeneric_DifferentType_ThrowsInvalidCastException(string path)
    {
        // Arrange
        var document = FamilyDocuments[path];
        var parameter = FindElementIdParameter(document, static element => element is not null and not ElementType);
        if (parameter is null)
        {
            Skip.Test("No parameter references an element instance in document");
            return;
        }

        // Act & Assert
        await Assert.That(parameter.AsElement<ElementType>).Throws<InvalidCastException>();
    }

    [Test]
    [MethodDataSource(nameof(RevitFamilies))]
    public async Task AsElementOrDefault_NoElement_ReturnsNull(string path)
    {
        // Arrange
        var document = FamilyDocuments[path];
        var parameter = FindElementIdParameter(document, static element => element is null);
        if (parameter is null)
        {
            Skip.Test("No parameter references no element in document");
            return;
        }

        // Act
        var element = parameter.AsElementOrDefault();

        // Assert
        await Assert.That(element).IsNull();
    }

    [Test]
    [MethodDataSource(nameof(RevitFamilies))]
    public async Task AsElementOrDefaultGeneric_MatchingType_ReturnsTypedElement(string path)
    {
        // Arrange
        var document = FamilyDocuments[path];
        var parameter = FindElementIdParameter(document, static element => element is not null and not ElementType);
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
    public async Task AsElementOrDefaultGeneric_DifferentType_ReturnsNull(string path)
    {
        // Arrange
        var document = FamilyDocuments[path];
        var parameter = FindElementIdParameter(document, static element => element is not null and not ElementType);
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

    private static Parameter? FindElementIdParameter(Document document, Func<Element?, bool> referencedElementPredicate)
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

                if (referencedElementPredicate(document.GetElement(parameter.AsElementId())))
                {
                    return parameter;
                }
            }
        }

        return null;
    }
}
