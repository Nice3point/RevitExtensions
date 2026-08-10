using Autodesk.Revit.DB.ExtensibleStorage;
using Nice3point.TUnit.Revit;
using Nice3point.TUnit.Revit.Executors;
using TUnit.Core.Executors;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class SchemaExtensionsTests : RevitApiTest
{
    private static readonly Guid SchemaGuid = new("D290F1EE-6C54-4B01-90E6-D701748F0851");

    private Document _document = null!;
    private Wall _wall = null!;
    private Schema _schema = null!;

    /// <summary>
    ///     Seeds a project holding one wall and a schema with a plain string field and a length-aware double field.
    /// </summary>
    [Before(Test)]
    [HookExecutor<RevitThreadExecutor>]
    public void SeedModel()
    {
        _document = Application.NewProjectDocument(UnitSystem.Metric);

        var level = new FilteredElementCollector(_document)
            .OfClass(typeof(Level))
            .Cast<Level>()
            .First();

        var builder = new SchemaBuilder(SchemaGuid)
            .SetSchemaName("SchemaExtensionsTestSchema")
            .SetReadAccessLevel(AccessLevel.Public)
            .SetWriteAccessLevel(AccessLevel.Public);

        builder.AddSimpleField("Manufacturer", typeof(string));
#if REVIT2021_OR_GREATER
        builder.AddSimpleField("Thickness", typeof(double)).SetSpec(SpecTypeId.Length);
#else
        builder.AddSimpleField("Thickness", typeof(double)).SetUnitType(UnitType.UT_Length);
#endif
        _schema = builder.Finish();

        using var transaction = new Transaction(_document, "Seed wall");
        transaction.Start();
        _wall = Wall.Create(_document, Line.CreateBound(new XYZ(0, 0, 0), new XYZ(10, 0, 0)), level.Id, false);
        transaction.Commit();
    }

    [After(Test)]
    [HookExecutor<RevitThreadExecutor>]
    public void CloseModel()
    {
        _document.Close(false);
    }

    [Test]
    public async Task SaveEntity_NewField_StoresValueAndReturnsTrue()
    {
        // Act
        bool saved;
        using (var transaction = new Transaction(_document, "Save entity"))
        {
            transaction.Start();
            saved = _wall.SaveEntity(_schema, "Acme", "Manufacturer");
            transaction.Commit();
        }

        var value = _wall.LoadEntity<string>(_schema, "Manufacturer");

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(saved).IsTrue();
            await Assert.That(value).IsEqualTo("Acme");
        }
    }

    [Test]
    public async Task SaveEntity_ExistingField_OverwritesPreviousValue()
    {
        // Act
        using (var transaction = new Transaction(_document, "Save entity"))
        {
            transaction.Start();
            _wall.SaveEntity(_schema, "Acme", "Manufacturer");
            _wall.SaveEntity(_schema, "Umbra", "Manufacturer");
            transaction.Commit();
        }

        var value = _wall.LoadEntity<string>(_schema, "Manufacturer");

        // Assert
        await Assert.That(value).IsEqualTo("Umbra");
    }

    [Test]
    public async Task SaveEntity_UnknownFieldName_ReturnsFalseAndStoresNothing()
    {
        // Act
        bool saved;
        using (var transaction = new Transaction(_document, "Save entity"))
        {
            transaction.Start();
            saved = _wall.SaveEntity(_schema, "Acme", "Unknown");
            transaction.Commit();
        }

        var value = _wall.LoadEntity<string>(_schema, "Manufacturer");

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(saved).IsFalse();
            await Assert.That(value).IsNull();
        }
    }

    [Test]
    public async Task LoadEntity_NoDataSaved_ReturnsDefault()
    {
        // Act
        var value = _wall.LoadEntity<string>(_schema, "Manufacturer");

        // Assert
        await Assert.That(value).IsNull();
    }

    [Test]
    public async Task LoadEntity_UnknownFieldName_ReturnsDefault()
    {
        // Act
        var value = _wall.LoadEntity<string>(_schema, "Unknown");

        // Assert
        await Assert.That(value).IsNull();
    }
#if REVIT2021_OR_GREATER

    [Test]
    public async Task SaveEntity_WithUnitTypeId_StoresValueConvertedToInternalUnits()
    {
        // Act
        using (var transaction = new Transaction(_document, "Save entity"))
        {
            transaction.Start();
            _wall.SaveEntity(_schema, 1d, "Thickness", UnitTypeId.Meters);
            transaction.Commit();
        }

        var internalValue = _wall.LoadEntity<double>(_schema, "Thickness", UnitTypeId.Meters);

        // Assert
        await Assert.That(internalValue).IsEqualTo(1d).Within(1e-9);
    }

    [Test]
    public async Task LoadEntity_WithUnitTypeId_ReturnsValueConvertedFromInternalUnits()
    {
        // Arrange
        using (var transaction = new Transaction(_document, "Save entity"))
        {
            transaction.Start();
            _wall.SaveEntity(_schema, 1d, "Thickness", UnitTypeId.Meters);
            transaction.Commit();
        }

        // Act
        var meters = _wall.LoadEntity<double>(_schema, "Thickness", UnitTypeId.Meters);

        // Assert
        await Assert.That(meters).IsEqualTo(1d).Within(1e-9);
    }
#else
    [Test]
    public async Task SaveEntity_WithDisplayUnitType_StoresValueConvertedToInternalUnits()
    {
        // Act
        using (var transaction = new Transaction(_document, "Save entity"))
        {
            transaction.Start();
            _wall.SaveEntity(_schema, 1d, "Thickness", DisplayUnitType.DUT_METERS);
            transaction.Commit();
        }

        var internalValue = _wall.LoadEntity<double>(_schema, "Thickness");

        // Assert
        await Assert.That(internalValue).IsEqualTo(UnitUtils.ConvertToInternalUnits(1, DisplayUnitType.DUT_METERS)).Within(1e-9);
    }

    [Test]
    public async Task LoadEntity_WithDisplayUnitType_ReturnsValueConvertedFromInternalUnits()
    {
        // Arrange
        using (var transaction = new Transaction(_document, "Save entity"))
        {
            transaction.Start();
            _wall.SaveEntity(_schema, 1d, "Thickness", DisplayUnitType.DUT_METERS);
            transaction.Commit();
        }

        // Act
        var meters = _wall.LoadEntity<double>(_schema, "Thickness", DisplayUnitType.DUT_METERS);

        // Assert
        await Assert.That(meters).IsEqualTo(1d).Within(1e-9);
    }
#endif
}