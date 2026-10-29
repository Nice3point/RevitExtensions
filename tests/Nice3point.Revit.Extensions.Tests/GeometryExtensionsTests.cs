using Nice3point.TUnit.Revit;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class GeometryExtensionsTests : RevitApiTest
{
    [Test]
    public async Task Distance_ParallelLines_ReturnsCorrectDistance()
    {
        // Arrange
        var line1 = Line.CreateBound(new XYZ(x: 0, y: 0, z: 0), new XYZ(x: 10, y: 0, z: 0));
        var line2 = Line.CreateBound(new XYZ(x: 0, y: 5, z: 0), new XYZ(x: 10, y: 5, z: 0));

        // Act
        var distance = line1.Distance(line2);

        // Assert
        await Assert.That(distance).IsEqualTo(5).Within(1e-9);
    }

    [Test]
    public async Task Distance_IntersectingLines_ReturnsZero()
    {
        // Arrange
        var line1 = Line.CreateBound(new XYZ(x: 0, y: 0, z: 0), new XYZ(x: 10, y: 0, z: 0));
        var line2 = Line.CreateBound(new XYZ(x: 5, y: -5, z: 0), new XYZ(x: 5, y: 5, z: 0));

        // Act
        var distance = line1.Distance(line2);

        // Assert
        await Assert.That(distance).IsEqualTo(0).Within(1e-9);
    }

    [Test]
    public async Task Distance_SkewLines_ReturnsCorrectDistance()
    {
        // Arrange
        var line1 = Line.CreateBound(new XYZ(x: 0, y: 0, z: 0), new XYZ(x: 10, y: 0, z: 0));
        var line2 = Line.CreateBound(new XYZ(x: 0, y: 5, z: 5), new XYZ(x: 10, y: 5, z: 5));

        // Act
        var distance = line1.Distance(line2);

        // Assert
        await Assert.That(distance).IsGreaterThan(0);
    }

    [Test]
    public async Task SetCoordinateX_ValidLine_ReturnsNewLineWithUpdatedX()
    {
        // Arrange
        var line = Line.CreateBound(new XYZ(x: 0, y: 5, z: 10), new XYZ(x: 0, y: 15, z: 20));
        const double newX = 100.0;

        // Act
        var newLine = line.SetCoordinateX(newX);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(newLine.GetEndPoint(0).X).IsEqualTo(newX).Within(1e-9);
            await Assert.That(newLine.GetEndPoint(1).X).IsEqualTo(newX).Within(1e-9);
            await Assert.That(newLine.GetEndPoint(0).Y).IsEqualTo(5).Within(1e-9);
            await Assert.That(newLine.GetEndPoint(1).Y).IsEqualTo(15).Within(1e-9);
        }
    }

    [Test]
    public async Task SetCoordinateY_ValidLine_ReturnsNewLineWithUpdatedY()
    {
        // Arrange
        var line = Line.CreateBound(new XYZ(x: 5, y: 0, z: 10), new XYZ(x: 15, y: 0, z: 20));
        const double newY = 100.0;

        // Act
        var newLine = line.SetCoordinateY(newY);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(newLine.GetEndPoint(0).Y).IsEqualTo(newY).Within(1e-9);
            await Assert.That(newLine.GetEndPoint(1).Y).IsEqualTo(newY).Within(1e-9);
            await Assert.That(newLine.GetEndPoint(0).X).IsEqualTo(5).Within(1e-9);
            await Assert.That(newLine.GetEndPoint(1).X).IsEqualTo(15).Within(1e-9);
        }
    }

    [Test]
    public async Task SetCoordinateZ_ValidLine_ReturnsNewLineWithUpdatedZ()
    {
        // Arrange
        var line = Line.CreateBound(new XYZ(x: 5, y: 10, z: 0), new XYZ(x: 15, y: 20, z: 0));
        const double newZ = 100.0;

        // Act
        var newLine = line.SetCoordinateZ(newZ);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(newLine.GetEndPoint(0).Z).IsEqualTo(newZ).Within(1e-9);
            await Assert.That(newLine.GetEndPoint(1).Z).IsEqualTo(newZ).Within(1e-9);
            await Assert.That(newLine.GetEndPoint(0).X).IsEqualTo(5).Within(1e-9);
            await Assert.That(newLine.GetEndPoint(1).X).IsEqualTo(15).Within(1e-9);
        }
    }

    [Test]
    public async Task SetCoordinateX_Arc_ReturnsNewArcWithUpdatedX()
    {
        // Arrange
        var arc = Arc.Create(new XYZ(x: 0, y: 0, z: 0), new XYZ(x: 0, y: 10, z: 0), new XYZ(x: 0, y: 5, z: 5));
        const double newX = 50.0;

        // Act
        var newArc = arc.SetCoordinateX(newX);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(newArc.GetEndPoint(0).X).IsEqualTo(newX).Within(1e-9);
            await Assert.That(newArc.GetEndPoint(1).X).IsEqualTo(newX).Within(1e-9);
            await Assert.That(newArc.Evaluate(parameter: 0.5, normalized: true).X).IsEqualTo(newX).Within(1e-9);
        }
    }

    [Test]
    public async Task SetCoordinateY_Arc_ReturnsNewArcWithUpdatedY()
    {
        // Arrange
        var arc = Arc.Create(new XYZ(x: 0, y: 0, z: 0), new XYZ(x: 10, y: 0, z: 0), new XYZ(x: 5, y: 0, z: 5));
        const double newY = 50.0;

        // Act
        var newArc = arc.SetCoordinateY(newY);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(newArc.GetEndPoint(0).Y).IsEqualTo(newY).Within(1e-9);
            await Assert.That(newArc.GetEndPoint(1).Y).IsEqualTo(newY).Within(1e-9);
            await Assert.That(newArc.Evaluate(parameter: 0.5, normalized: true).Y).IsEqualTo(newY).Within(1e-9);
        }
    }

    [Test]
    public async Task SetCoordinateZ_Arc_ReturnsNewArcWithUpdatedZ()
    {
        // Arrange
        var arc = Arc.Create(new XYZ(x: 0, y: 0, z: 0), new XYZ(x: 10, y: 0, z: 0), new XYZ(x: 5, y: 5, z: 0));
        const double newZ = 50.0;

        // Act
        var newArc = arc.SetCoordinateZ(newZ);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(newArc.GetEndPoint(0).Z).IsEqualTo(newZ).Within(1e-9);
            await Assert.That(newArc.GetEndPoint(1).Z).IsEqualTo(newZ).Within(1e-9);
            await Assert.That(newArc.Evaluate(parameter: 0.5, normalized: true).Z).IsEqualTo(newZ).Within(1e-9);
        }
    }
}
