using System.Collections;
using Nice3point.TUnit.Revit;
using Nice3point.TUnit.Revit.Executors;
using TUnit.Core.Executors;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class CollectionExtensionsTests : RevitApiTest
{
    private Document _document = null!;
    private Wall _wall = null!;

    /// <summary>
    ///     Seeds a project holding one wall, the source of the arrays Revit itself builds.
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
    public async Task EnumerateValues_CurveArray_MatchesTheEnumerator()
    {
        // Arrange
        var array = Application.Create.NewCurveArray();
        for (var index = 0; index < 5; index++)
        {
            array.Append(Line.CreateBound(new XYZ(index, 0, 0), new XYZ(index, 1, 0)));
        }

        var expected = CollectThroughEnumerator<Curve>(array).Select(curve => curve.GetEndPoint(0).X).ToList();

        // Act
        var origins = array.EnumerateValues().Select(curve => curve.GetEndPoint(0).X).ToList();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(origins.Count).IsEqualTo(array.Size);
            await Assert.That(origins).IsEquivalentTo(expected);
        }
    }

    [Test]
    public async Task EnumerateValues_CurveArray_Empty_ReturnsEmptySequence()
    {
        // Arrange
        var array = Application.Create.NewCurveArray();

        // Act
        var curves = array.EnumerateValues().ToList();

        // Assert
        await Assert.That(curves).IsEmpty();
    }

    [Test]
    public async Task EnumerateValues_CurveArray_EnumeratedTwice_WalksTheArrayFromTheStart()
    {
        // Arrange
        var array = Application.Create.NewCurveArray();
        for (var index = 0; index < 5; index++)
        {
            array.Append(Line.CreateBound(new XYZ(index, 0, 0), new XYZ(index, 1, 0)));
        }

        var curves = array.EnumerateValues();

        // Act
        // ReSharper disable PossibleMultipleEnumeration
        var firstPass = curves.Select(curve => curve.GetEndPoint(0).X).ToList();
        var secondPass = curves.Select(curve => curve.GetEndPoint(0).X).ToList();
        // ReSharper restore PossibleMultipleEnumeration

        // Assert
        await Assert.That(secondPass).IsEquivalentTo(firstPass);
    }

    [Test]
    public async Task EnumerateValues_DoubleArray_MatchesTheEnumerator()
    {
        // Arrange
        var array = Application.Create.NewDoubleArray();
        foreach (var value in new[] { 1.5, -2.25, 0d })
        {
            var element = value;
            array.Append(ref element);
        }

        var expected = CollectThroughEnumerator<double>(array);

        // Act
        var values = array.EnumerateValues().ToList();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(values.Count).IsEqualTo(array.Size);
            await Assert.That(values).IsEquivalentTo(expected);
        }
    }

    [Test]
    public async Task EnumerateValues_FaceArray_MatchesTheEnumerator()
    {
        // Arrange
        var faces = GetWallSolid().Faces;
        var expected = CollectThroughEnumerator<Face>(faces).Select(face => face.Area).ToList();

        // Act
        var areas = faces.EnumerateValues().Select(face => face.Area).ToList();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(areas.Count).IsEqualTo(faces.Size);
            await Assert.That(areas).IsEquivalentTo(expected);
        }
    }

    [Test]
    public async Task EnumerateValues_EdgeArrayArray_MatchesTheEnumerator()
    {
        // Arrange
        var edgeLoops = GetWallSolid().Faces.EnumerateValues().First().EdgeLoops;
        var expected = CollectThroughEnumerator<EdgeArray>(edgeLoops).Select(loop => loop.Size).ToList();

        // Act
        var sizes = edgeLoops.EnumerateValues().Select(loop => loop.Size).ToList();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(sizes.Count).IsEqualTo(edgeLoops.Size);
            await Assert.That(sizes).IsEquivalentTo(expected);
        }
    }

    [Test]
    public async Task EnumerateValues_CategorySet_MatchesTheEnumerator()
    {
        // Arrange
        var set = Application.Create.NewCategorySet();
        foreach (var (_, category) in _document.Settings.Categories.EnumerateEntries())
        {
            set.Insert(category);
        }

        var expected = CollectThroughEnumerator<Category>(set).Select(category => category.Id).ToList();

        // Act
        var ids = set.EnumerateValues().Select(category => category.Id).ToList();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(ids).IsNotEmpty();
            await Assert.That(ids).IsEquivalentTo(expected);
        }
    }

    [Test]
    public async Task EnumerateValues_CategorySet_Empty_ReturnsEmptySequence()
    {
        // Arrange
        var set = Application.Create.NewCategorySet();

        // Act
        var categories = set.EnumerateValues().ToList();

        // Assert
        await Assert.That(categories).IsEmpty();
    }

    [Test]
    public async Task EnumerateValues_ParameterSet_MatchesTheEnumerator()
    {
        // Arrange
        var parameters = _wall.Parameters;
        var expected = CollectThroughEnumerator<Parameter>(parameters).Select(parameter => parameter.Id).ToList();

        // Act
        var ids = parameters.EnumerateValues().Select(parameter => parameter.Id).ToList();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(ids).IsNotEmpty();
            await Assert.That(ids).IsEquivalentTo(expected);
        }
    }

    private Solid GetWallSolid()
    {
        return _wall.get_Geometry(new Options())
            .OfType<Solid>()
            .First(solid => solid.Faces.Size > 0);
    }

    private static List<T> CollectThroughEnumerator<T>(IEnumerable source)
    {
        var items = new List<T>();

        foreach (T item in source)
        {
            items.Add(item);
        }

        return items;
    }
}
