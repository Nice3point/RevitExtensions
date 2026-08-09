using Nice3point.Revit.Extensions.Tests.Abstractions;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class ElementIdExtensionsTests : RevitFamilySampleTest
{
    [Test]
    public async Task IsCategory_BuiltInCategory_MatchingCategory_ReturnsTrue()
    {
        // Arrange
        var wallCategoryId = new ElementId(BuiltInCategory.OST_Walls);

        // Act
        var result = wallCategoryId.IsCategory(BuiltInCategory.OST_Walls);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsCategory_BuiltInCategory_DifferentCategory_ReturnsFalse()
    {
        // Arrange
        var wallCategoryId = new ElementId(BuiltInCategory.OST_Walls);

        // Act
        var result = wallCategoryId.IsCategory(BuiltInCategory.OST_Doors);

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task ToLong_BuiltInCategory_ReturnsUnderlyingValue()
    {
        // Arrange
        var category = BuiltInCategory.OST_Walls;

        // Act
        var value = category.ToLong();

        // Assert
        await Assert.That(value).IsEqualTo((long)category);
    }

    [Test]
    public async Task ToLong_ElementId_ReturnsSameValueAsSourceBuiltInCategory()
    {
        // Arrange
        var elementId = BuiltInCategory.OST_Walls.ToElementId();

        // Act
        var value = elementId.ToLong();

        // Assert
        await Assert.That(value).IsEqualTo(BuiltInCategory.OST_Walls.ToLong());
    }

    [Test]
    public async Task ToElementId_Long_RoundTripsThroughToLong()
    {
        // Arrange
        const long value = 123456L;

        // Act
        var elementId = value.ToElementId();

        // Assert
        await Assert.That(elementId.ToLong()).IsEqualTo(value);
    }

    [Test]
    public async Task IsCategory_BuiltInParameter_MatchingParameter_ReturnsTrue()
    {
        // Arrange
        var parameterId = new ElementId(BuiltInParameter.WALL_BOTTOM_IS_ATTACHED);

        // Act
        var result = parameterId.IsParameter(BuiltInParameter.WALL_BOTTOM_IS_ATTACHED);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsCategory_BuiltInParameter_DifferentParameter_ReturnsFalse()
    {
        // Arrange
        var parameterId = new ElementId(BuiltInParameter.WALL_BOTTOM_IS_ATTACHED);

        // Act
        var result = parameterId.IsParameter(BuiltInParameter.WALL_TOP_OFFSET);

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task ToLong_BuiltInParameter_ReturnsUnderlyingValue()
    {
        // Arrange
        var parameter = BuiltInParameter.WALL_BOTTOM_IS_ATTACHED;

        // Act
        var value = parameter.ToLong();

        // Assert
        await Assert.That(value).IsEqualTo((long)parameter);
    }

    [Test]
    [MethodDataSource(nameof(RevitFamilies))]
    public async Task ToElement_ValidElementId_ReturnsElement(string path)
    {
        // Arrange
        var document = FamilyDocuments[path];
        var elementIds = new FilteredElementCollector(document)
            .WhereElementIsNotElementType()
            .ToElementIds();

        var firstId = elementIds.FirstOrDefault();
        if (firstId is null)
        {
            Skip.Test("No elements found in document");
            return;
        }

        // Act
        var element = firstId.ToElement(document);

        // Assert
        await Assert.That(element).IsNotNull();
    }

    [Test]
    [MethodDataSource(nameof(RevitFamilies))]
    public async Task ToElement_InvalidElementId_ReturnsNull(string path)
    {
        // Arrange
        var document = FamilyDocuments[path];

#if REVIT2024_OR_GREATER
        var invalidId = new ElementId(999999999L);
#else
        var invalidId = new ElementId(999999999);
#endif

        // Act
        var element = invalidId.ToElement(document);

        // Assert
        await Assert.That(element).IsNull();
    }

    [Test]
    [MethodDataSource(nameof(RevitFamilies))]
    public async Task ToElementGeneric_ValidElementId_ReturnsTypedElement(string path)
    {
        // Arrange
        var document = FamilyDocuments[path];
        var elementIds = new FilteredElementCollector(document)
            .WhereElementIsElementType()
            .ToElementIds();

        var firstId = elementIds.FirstOrDefault();
        if (firstId is null)
        {
            Skip.Test("No element types found in document");
            return;
        }

        // Act
        var elementType = firstId.ToElement<ElementType>(document);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(elementType).IsNotNull();
            await Assert.That(elementType).IsAssignableTo<ElementType>();
        }
    }

    [Test]
    [MethodDataSource(nameof(RevitFamilies))]
    public async Task ToElements_MultipleElementIds_ReturnsAllElements(string path)
    {
        // Arrange
        var document = FamilyDocuments[path];
        var elementIds = new FilteredElementCollector(document)
            .WhereElementIsNotElementType()
            .ToElementIds()
            .Take(5)
            .ToList();

        // Act
        var elements = elementIds.ToElements(document);

        // Assert
        await Assert.That(elements.Count).IsEqualTo(elementIds.Count);
    }

    [Test]
    [MethodDataSource(nameof(RevitFamilies))]
    public async Task ToElements_EmptyCollection_ReturnsEmptyList(string path)
    {
        // Arrange
        var document = FamilyDocuments[path];
        var elementIds = new List<ElementId>();

        // Act
        var elements = elementIds.ToElements(document);

        // Assert
        await Assert.That(elements).IsEmpty();
    }

    [Test]
    [MethodDataSource(nameof(RevitFamilies))]
    public async Task ToElementsGeneric_MultipleElementIds_ReturnsTypedElements(string path)
    {
        // Arrange
        var document = FamilyDocuments[path];
        var elementIds = new FilteredElementCollector(document)
            .WhereElementIsElementType()
            .ToElementIds()
            .Take(5)
            .ToList();

        // Act
        var elementTypes = elementIds.ToElements<ElementType>(document);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(elementTypes.Count).IsEqualTo(elementIds.Count);
            await Assert.That(elementTypes).All().Satisfy(source => source.IsAssignableTo<ElementType>());
        }
    }

    [Test]
    [MethodDataSource(nameof(RevitFamilies))]
    public async Task ToOrderedElements_MultipleElementIds_PreservesOrder(string path)
    {
        // Arrange
        var document = FamilyDocuments[path];
        var elementIds = new FilteredElementCollector(document)
            .WhereElementIsNotElementType()
            .ToElementIds()
            .Take(5)
            .ToList();

        // Act
        var orderedElements = elementIds.ToOrderedElements(document);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(orderedElements.Count).IsEqualTo(elementIds.Count);
            for (var i = 0; i < elementIds.Count; i++)
            {
                await Assert.That(orderedElements[i].Id).IsEqualTo(elementIds[i]);
            }
        }
    }

    [Test]
    [MethodDataSource(nameof(RevitFamilies))]
    public async Task ToOrderedElementsGeneric_MultipleElementIds_PreservesOrderAndType(string path)
    {
        // Arrange
        var document = FamilyDocuments[path];
        var elementIds = new FilteredElementCollector(document)
            .WhereElementIsElementType()
            .ToElementIds()
            .Take(5)
            .ToList();

        // Act
        var orderedElementTypes = elementIds.ToOrderedElements<ElementType>(document);

        // Assert
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
}
