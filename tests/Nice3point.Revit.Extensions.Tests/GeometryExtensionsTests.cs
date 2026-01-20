using Nice3point.TUnit.Revit;
using Nice3point.TUnit.Revit.Executors;
using TUnit.Core.Executors;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class GeometryExtensionsTests : RevitApiTest
{
    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task Distance_ParallelLines_ReturnsCorrectDistance()
    {
        var line1 = Line.CreateBound(new XYZ(0, 0, 0), new XYZ(10, 0, 0));
        var line2 = Line.CreateBound(new XYZ(0, 5, 0), new XYZ(10, 5, 0));

        var distance = line1.Distance(line2);

        await Assert.That(distance).IsEqualTo(5).Within(1e-9);
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task Distance_IntersectingLines_ReturnsZero()
    {
        var line1 = Line.CreateBound(new XYZ(0, 0, 0), new XYZ(10, 0, 0));
        var line2 = Line.CreateBound(new XYZ(5, -5, 0), new XYZ(5, 5, 0));

        var distance = line1.Distance(line2);

        await Assert.That(distance).IsEqualTo(0).Within(1e-9);
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task Distance_SkewLines_ReturnsCorrectDistance()
    {
        var line1 = Line.CreateBound(new XYZ(0, 0, 0), new XYZ(10, 0, 0));
        var line2 = Line.CreateBound(new XYZ(0, 5, 5), new XYZ(10, 5, 5));

        var distance = line1.Distance(line2);

        await Assert.That(distance).IsGreaterThan(0);
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task SetCoordinateX_ValidLine_ReturnsNewLineWithUpdatedX()
    {
        var line = Line.CreateBound(new XYZ(0, 5, 10), new XYZ(0, 15, 20));
        var newX = 100.0;

        var newLine = line.SetCoordinateX(newX);

        using (Assert.Multiple())
        {
            await Assert.That(newLine.GetEndPoint(0).X).IsEqualTo(newX).Within(1e-9);
            await Assert.That(newLine.GetEndPoint(1).X).IsEqualTo(newX).Within(1e-9);
            await Assert.That(newLine.GetEndPoint(0).Y).IsEqualTo(5).Within(1e-9);
            await Assert.That(newLine.GetEndPoint(1).Y).IsEqualTo(15).Within(1e-9);
        }
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task SetCoordinateY_ValidLine_ReturnsNewLineWithUpdatedY()
    {
        var line = Line.CreateBound(new XYZ(5, 0, 10), new XYZ(15, 0, 20));
        var newY = 100.0;

        var newLine = line.SetCoordinateY(newY);

        using (Assert.Multiple())
        {
            await Assert.That(newLine.GetEndPoint(0).Y).IsEqualTo(newY).Within(1e-9);
            await Assert.That(newLine.GetEndPoint(1).Y).IsEqualTo(newY).Within(1e-9);
            await Assert.That(newLine.GetEndPoint(0).X).IsEqualTo(5).Within(1e-9);
            await Assert.That(newLine.GetEndPoint(1).X).IsEqualTo(15).Within(1e-9);
        }
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task SetCoordinateZ_ValidLine_ReturnsNewLineWithUpdatedZ()
    {
        var line = Line.CreateBound(new XYZ(5, 10, 0), new XYZ(15, 20, 0));
        var newZ = 100.0;

        var newLine = line.SetCoordinateZ(newZ);

        using (Assert.Multiple())
        {
            await Assert.That(newLine.GetEndPoint(0).Z).IsEqualTo(newZ).Within(1e-9);
            await Assert.That(newLine.GetEndPoint(1).Z).IsEqualTo(newZ).Within(1e-9);
            await Assert.That(newLine.GetEndPoint(0).X).IsEqualTo(5).Within(1e-9);
            await Assert.That(newLine.GetEndPoint(1).X).IsEqualTo(15).Within(1e-9);
        }
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task SetCoordinateX_Arc_ReturnsNewArcWithUpdatedX()
    {
        var arc = Arc.Create(new XYZ(0, 0, 0), new XYZ(0, 10, 0), new XYZ(0, 5, 5));
        var newX = 50.0;

        var newArc = arc.SetCoordinateX(newX);

        using (Assert.Multiple())
        {
            await Assert.That(newArc.GetEndPoint(0).X).IsEqualTo(newX).Within(1e-9);
            await Assert.That(newArc.GetEndPoint(1).X).IsEqualTo(newX).Within(1e-9);
            await Assert.That(newArc.Evaluate(0.5, true).X).IsEqualTo(newX).Within(1e-9);
        }
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task SetCoordinateY_Arc_ReturnsNewArcWithUpdatedY()
    {
        var arc = Arc.Create(new XYZ(0, 0, 0), new XYZ(10, 0, 0), new XYZ(5, 0, 5));
        var newY = 50.0;

        var newArc = arc.SetCoordinateY(newY);

        using (Assert.Multiple())
        {
            await Assert.That(newArc.GetEndPoint(0).Y).IsEqualTo(newY).Within(1e-9);
            await Assert.That(newArc.GetEndPoint(1).Y).IsEqualTo(newY).Within(1e-9);
            await Assert.That(newArc.Evaluate(0.5, true).Y).IsEqualTo(newY).Within(1e-9);
        }
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task SetCoordinateZ_Arc_ReturnsNewArcWithUpdatedZ()
    {
        var arc = Arc.Create(new XYZ(0, 0, 0), new XYZ(10, 0, 0), new XYZ(5, 5, 0));
        var newZ = 50.0;

        var newArc = arc.SetCoordinateZ(newZ);

        using (Assert.Multiple())
        {
            await Assert.That(newArc.GetEndPoint(0).Z).IsEqualTo(newZ).Within(1e-9);
            await Assert.That(newArc.GetEndPoint(1).Z).IsEqualTo(newZ).Within(1e-9);
            await Assert.That(newArc.Evaluate(0.5, true).Z).IsEqualTo(newZ).Within(1e-9);
        }
    }
}