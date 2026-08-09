using Nice3point.TUnit.Revit;
using Nice3point.TUnit.Revit.Executors;
using TUnit.Core.Executors;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class BuiltInExtensionsTests : RevitApiTest
{
    private static Document _document = null!;

    [Before(Class)]
    [HookExecutor<RevitThreadExecutor>]
    public static void Setup()
    {
        _document = Application.NewProjectDocument(UnitSystem.Metric);
    }

    [After(Class)]
    [HookExecutor<RevitThreadExecutor>]
    public static void Cleanup()
    {
        _document.Close(false);
    }

    [Test]
    public async Task ToParameter_ValidBuiltInParameter_ReturnsParameter()
    {
        // Act
        var parameter = BuiltInParameter.ELEM_CATEGORY_PARAM.ToParameter(_document);

        // Assert
        await Assert.That(parameter).IsNotNull();
    }

    [Test]
    public async Task ToParameter_ValidBuiltInParameter_ReturnsCorrectDefinitionName()
    {
        // Act
        var parameter = BuiltInParameter.ELEM_CATEGORY_PARAM.ToParameter(_document);

        // Assert
        await Assert.That(parameter.Definition.Name).IsNotNull().And.IsNotEmpty();
    }

    [Test]
    public async Task ToParameter_DifferentParameters_ReturnDifferentDefinitions()
    {
        // Act
        var parameter1 = BuiltInParameter.ELEM_CATEGORY_PARAM.ToParameter(_document);
        var parameter2 = BuiltInParameter.ELEM_TYPE_PARAM.ToParameter(_document);

        // Assert
        await Assert.That(parameter1.Definition.Name).IsNotEqualTo(parameter2.Definition.Name);
    }

    [Test]
    public async Task ToCategory_ValidBuiltInCategory_ReturnsCategory()
    {
        // Act
        var category = BuiltInCategory.OST_Walls.ToCategory(_document);

        // Assert
        await Assert.That(category).IsNotNull();
    }

    [Test]
    public async Task ToCategory_ValidBuiltInCategory_ReturnsCorrectName()
    {
        // Act
        var category = BuiltInCategory.OST_Walls.ToCategory(_document);

        // Assert
        await Assert.That(category.Name).IsNotNull().And.IsNotEmpty();
    }

    [Test]
    public async Task ToCategory_ValidBuiltInCategory_ReturnsMatchingId()
    {
        // Act
        var category = BuiltInCategory.OST_Walls.ToCategory(_document);

        // Assert
        await Assert.That(category.Id.IsCategory(BuiltInCategory.OST_Walls)).IsTrue();
    }

    [Test]
    public async Task ToCategory_DifferentCategories_ReturnDifferentNames()
    {
        // Act
        var walls = BuiltInCategory.OST_Walls.ToCategory(_document);
        var doors = BuiltInCategory.OST_Doors.ToCategory(_document);

        // Assert
        await Assert.That(walls.Name).IsNotEqualTo(doors.Name);
    }

    [Test]
    public async Task ToCategory_DifferentCategories_ReturnDifferentIds()
    {
        // Act
        var walls = BuiltInCategory.OST_Walls.ToCategory(_document);
        var doors = BuiltInCategory.OST_Doors.ToCategory(_document);

        // Assert
        await Assert.That(walls.Id).IsNotEqualTo(doors.Id);
    }
}