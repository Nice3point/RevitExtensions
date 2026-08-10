using Nice3point.TUnit.Revit;
using Nice3point.TUnit.Revit.Executors;
using TUnit.Core.Executors;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class MapExtensionsTests : RevitApiTest
{
    private const string SharedParameterName = "MapExtensionsTestParameter";

#pragma warning disable TUnit0023
    private Document _document = null!;
    private Level _level = null!;
#pragma warning restore TUnit0023
    private string _sharedParametersPath = null!;
    private string _originalSharedParametersPath = null!;

    /// <summary>
    ///     Seeds a project holding one bound project parameter, the only source of a populated <see cref="BindingMap"/>.
    /// </summary>
    [Before(Test)]
    [HookExecutor<RevitThreadExecutor>]
    public void SeedModel()
    {
        _document = Application.NewProjectDocument(UnitSystem.Metric);

        _level = new FilteredElementCollector(_document)
            .OfClass(typeof(Level))
            .Cast<Level>()
            .First();

        _originalSharedParametersPath = Application.SharedParametersFilename;
        _sharedParametersPath = Path.Combine(Path.GetTempPath(), $"{Path.GetRandomFileName()}.txt");
        File.WriteAllText(_sharedParametersPath, string.Empty);
        Application.SharedParametersFilename = _sharedParametersPath;

        var definitionFile = Application.OpenSharedParameterFile();
        var definitionGroup = definitionFile.Groups.Create("MapExtensions");
#if REVIT2022_OR_GREATER
        var creationOptions = new ExternalDefinitionCreationOptions(SharedParameterName, SpecTypeId.String.Text);
#else
        var creationOptions = new ExternalDefinitionCreationOptions(SharedParameterName, ParameterType.Text);
#endif
        var definition = definitionGroup.Definitions.Create(creationOptions);

        var categories = Application.Create.NewCategorySet();
        categories.Insert(_document.Settings.Categories.get_Item(BuiltInCategory.OST_Walls));

        using var transaction = new Transaction(_document, "Bind project parameter");
        transaction.Start();
        _document.ParameterBindings.Insert(definition, Application.Create.NewInstanceBinding(categories));
        transaction.Commit();
    }

    [After(Test)]
    [HookExecutor<RevitThreadExecutor>]
    public void RestoreSharedParameterFile()
    {
        _document.Close(false);
        Application.SharedParametersFilename = _originalSharedParametersPath;
        File.Delete(_sharedParametersPath);
    }

    [Test]
    public async Task EnumerateEntries_ParameterMap_PairsEveryNameWithItsParameter()
    {
        // Arrange
        var map = _level.ParametersMap;

        // Act
        var entries = map.EnumerateEntries().ToList();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(entries).IsNotEmpty();
            await Assert.That(entries.Count).IsEqualTo(map.Size);
            await Assert.That(entries.All(entry => entry.Name == entry.Parameter.Definition.Name)).IsTrue();
        }
    }

    [Test]
    public async Task EnumerateEntries_ParameterMap_EnumeratedTwice_WalksTheMapFromTheStart()
    {
        // Arrange
        var entries = _level.ParametersMap.EnumerateEntries();

        // Act
        // ReSharper disable PossibleMultipleEnumeration
        var firstPass = entries.Select(entry => entry.Name).ToList();
        var secondPass = entries.Select(entry => entry.Name).ToList();
        // ReSharper restore PossibleMultipleEnumeration

        // Assert
        await Assert.That(secondPass).IsEquivalentTo(firstPass);
    }

    [Test]
    public async Task EnumerateKeys_ParameterMap_MatchesTheNamesOfTheEntries()
    {
        // Arrange
        var map = _level.ParametersMap;
        var expected = map.EnumerateEntries().Select(entry => entry.Name).ToList();

        // Act
        var names = map.EnumerateKeys().ToList();

        // Assert
        await Assert.That(names).IsEquivalentTo(expected);
    }

    [Test]
    public async Task EnumerateValues_ParameterMap_MatchesTheParametersOfTheEntries()
    {
        // Arrange
        var map = _level.ParametersMap;
        var expected = map.EnumerateEntries().Select(entry => entry.Parameter.Id).ToList();

        // Act
        var ids = map.EnumerateValues().Select(parameter => parameter.Id).ToList();

        // Assert
        await Assert.That(ids).IsEquivalentTo(expected);
    }

    [Test]
    public async Task TryGetValue_ParameterMap_StoredName_ReturnsTheParameter()
    {
        // Arrange
        var map = _level.ParametersMap;
        var storedName = map.EnumerateKeys().First();

        // Act
        var isFound = map.TryGetValue(storedName, out var parameter);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(isFound).IsTrue();
            await Assert.That(parameter!.Definition.Name).IsEqualTo(storedName);
        }
    }

    [Test]
    public async Task TryGetValue_ParameterMap_AbsentName_ReturnsFalse()
    {
        // Arrange
        var map = _level.ParametersMap;

        // Act
        var isFound = map.TryGetValue("MapExtensionsAbsentParameter", out var parameter);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(isFound).IsFalse();
            await Assert.That(parameter).IsNull();
        }
    }

    [Test]
    public async Task EnumerateEntries_Categories_PairsEveryNameWithItsCategory()
    {
        // Arrange
        var categories = _document.Settings.Categories;

        // Act
        var entries = categories.EnumerateEntries().ToList();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(entries).IsNotEmpty();
            await Assert.That(entries.Count).IsEqualTo(categories.Size);
            await Assert.That(entries.All(entry => entry.Name == entry.Category.Name)).IsTrue();
        }
    }

    [Test]
    public async Task TryGetValue_Categories_StoredName_ReturnsTheCategory()
    {
        // Arrange
        var categories = _document.Settings.Categories;
        var storedName = categories.EnumerateKeys().First();

        // Act
        var isFound = categories.TryGetValue(storedName, out var category);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(isFound).IsTrue();
            await Assert.That(category!.Name).IsEqualTo(storedName);
        }
    }

    [Test]
    public async Task TryGetValue_Categories_AbsentName_ReturnsFalse()
    {
        // Arrange
        var categories = _document.Settings.Categories;

        // Act
        var isFound = categories.TryGetValue("MapExtensionsAbsentCategory", out var category);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(isFound).IsFalse();
            await Assert.That(category).IsNull();
        }
    }

    [Test]
    public async Task EnumerateEntries_CategoryNameMap_PairsEveryNameWithItsSubcategory()
    {
        // Arrange
        var subCategories = _document.Settings.Categories
            .Cast<Category>()
            .Select(category => category.SubCategories)
            .First(map => map.Size > 0);

        // Act
        var entries = subCategories.EnumerateEntries().ToList();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(entries).IsNotEmpty();
            await Assert.That(entries.Count).IsEqualTo(subCategories.Size);
            await Assert.That(entries.All(entry => entry.Name == entry.Category.Name)).IsTrue();
        }
    }

    [Test]
    public async Task EnumerateEntries_BindingMap_PairsTheDefinitionWithItsBinding()
    {
        // Arrange
        var bindings = _document.ParameterBindings;

        // Act
        var entries = bindings.EnumerateEntries().ToList();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(entries.Count).IsEqualTo(bindings.Size);
            await Assert.That(entries.Select(entry => entry.Definition.Name)).Contains(SharedParameterName);
            await Assert.That(entries.All(entry => entry.Binding is ElementBinding)).IsTrue();
        }
    }

    [Test]
    public async Task TryGetValue_BindingMap_BoundDefinition_ReturnsTheBinding()
    {
        // Arrange
        var bindings = _document.ParameterBindings;
        var definition = bindings.EnumerateKeys().First(key => key.Name == SharedParameterName);

        // Act
        var isFound = bindings.TryGetValue(definition, out var binding);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(isFound).IsTrue();
            await Assert.That(binding).IsTypeOf<InstanceBinding>();
        }
    }

    [Test]
    public async Task EnumerateValues_BindingMap_MatchesTheBindingsOfTheEntries()
    {
        // Arrange
        var bindings = _document.ParameterBindings;

        // Act
        var values = bindings.EnumerateValues().ToList();

        // Assert
        await Assert.That(values.Count).IsEqualTo(bindings.Size);
    }
}
