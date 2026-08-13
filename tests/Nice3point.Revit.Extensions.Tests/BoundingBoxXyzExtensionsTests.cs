using Nice3point.TUnit.Revit;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class BoundingBoxXyzExtensionsTests : RevitApiTest
{
    [Test]
    public async Task Contains_PointInsideBox_ReturnsTrue()
    {
        // Arrange
        var boundingBox = new BoundingBoxXYZ
        {
            Min = new XYZ(0, 0, 0),
            Max = new XYZ(10, 10, 10)
        };

        var pointInside = new XYZ(5, 5, 5);

        // Act
        var contains = boundingBox.Contains(pointInside);

        // Assert
        await Assert.That(contains).IsTrue();
    }

    [Test]
    public async Task Contains_PointOutsideBox_ReturnsFalse()
    {
        // Arrange
        var boundingBox = new BoundingBoxXYZ
        {
            Min = new XYZ(0, 0, 0),
            Max = new XYZ(10, 10, 10)
        };

        var pointOutside = new XYZ(15, 15, 15);

        // Act
        var contains = boundingBox.Contains(pointOutside);

        // Assert
        await Assert.That(contains).IsFalse();
    }

    [Test]
    public async Task Contains_PointOnBorder_ReturnsTrue()
    {
        // Arrange
        var boundingBox = new BoundingBoxXYZ
        {
            Min = new XYZ(0, 0, 0),
            Max = new XYZ(10, 10, 10)
        };

        var pointOnBorder = new XYZ(10, 10, 10);

        // Act
        var contains = boundingBox.Contains(pointOnBorder);

        // Assert
        await Assert.That(contains).IsTrue();
    }

    [Test]
    public async Task Contains_PointOnBorderStrictMode_ReturnsFalse()
    {
        // Arrange
        var boundingBox = new BoundingBoxXYZ
        {
            Min = new XYZ(0, 0, 0),
            Max = new XYZ(10, 10, 10)
        };

        var pointOnBorder = new XYZ(10, 10, 10);

        // Act
        var contains = boundingBox.Contains(pointOnBorder, true);

        // Assert
        await Assert.That(contains).IsFalse();
    }

    [Test]
    public async Task Contains_BoxInsideBox_ReturnsTrue()
    {
        // Arrange
        var outerBox = new BoundingBoxXYZ
        {
            Min = new XYZ(0, 0, 0),
            Max = new XYZ(10, 10, 10)
        };

        var innerBox = new BoundingBoxXYZ
        {
            Min = new XYZ(2, 2, 2),
            Max = new XYZ(8, 8, 8)
        };

        // Act
        var contains = outerBox.Contains(innerBox);

        // Assert
        await Assert.That(contains).IsTrue();
    }

    [Test]
    public async Task Contains_BoxOutsideBox_ReturnsFalse()
    {
        // Arrange
        var box1 = new BoundingBoxXYZ
        {
            Min = new XYZ(0, 0, 0),
            Max = new XYZ(10, 10, 10)
        };

        var box2 = new BoundingBoxXYZ
        {
            Min = new XYZ(15, 15, 15),
            Max = new XYZ(20, 20, 20)
        };

        // Act
        var contains = box1.Contains(box2);

        // Assert
        await Assert.That(contains).IsFalse();
    }

    [Test]
    public async Task Overlaps_OverlappingBoxes_ReturnsTrue()
    {
        // Arrange
        var box1 = new BoundingBoxXYZ
        {
            Min = new XYZ(0, 0, 0),
            Max = new XYZ(10, 10, 10)
        };

        var box2 = new BoundingBoxXYZ
        {
            Min = new XYZ(5, 5, 5),
            Max = new XYZ(15, 15, 15)
        };

        // Act
        var overlaps = box1.Overlaps(box2);

        // Assert
        await Assert.That(overlaps).IsTrue();
    }

    [Test]
    public async Task Overlaps_NonOverlappingBoxes_ReturnsFalse()
    {
        // Arrange
        var box1 = new BoundingBoxXYZ
        {
            Min = new XYZ(0, 0, 0),
            Max = new XYZ(10, 10, 10)
        };

        var box2 = new BoundingBoxXYZ
        {
            Min = new XYZ(15, 15, 15),
            Max = new XYZ(20, 20, 20)
        };

        // Act
        var overlaps = box1.Overlaps(box2);

        // Assert
        await Assert.That(overlaps).IsFalse();
    }

    [Test]
    public async Task ComputeCentroid_ValidBox_ReturnsCenter()
    {
        // Arrange
        var boundingBox = new BoundingBoxXYZ
        {
            Min = new XYZ(0, 0, 0),
            Max = new XYZ(10, 10, 10)
        };

        // Act
        var centroid = boundingBox.ComputeCentroid();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(centroid.X).IsEqualTo(5).Within(1e-9);
            await Assert.That(centroid.Y).IsEqualTo(5).Within(1e-9);
            await Assert.That(centroid.Z).IsEqualTo(5).Within(1e-9);
        }
    }

    [Test]
    public async Task ComputeVolume_ValidBox_ReturnsCorrectVolume()
    {
        // Arrange
        var boundingBox = new BoundingBoxXYZ
        {
            Min = new XYZ(0, 0, 0),
            Max = new XYZ(10, 10, 10)
        };

        // Act
        var volume = boundingBox.ComputeVolume();

        // Assert
        await Assert.That(volume).IsEqualTo(1000).Within(1e-9);
    }

    [Test]
    public async Task ComputeSurfaceArea_ValidBox_ReturnsCorrectArea()
    {
        // Arrange
        var boundingBox = new BoundingBoxXYZ
        {
            Min = new XYZ(0, 0, 0),
            Max = new XYZ(10, 10, 10)
        };

        // Act
        var surfaceArea = boundingBox.ComputeSurfaceArea();

        // Assert
        await Assert.That(surfaceArea).IsEqualTo(600).Within(1e-9);
    }

    [Test]
    public async Task ComputeVertices_ValidBox_ReturnsEightVertices()
    {
        // Arrange
        var boundingBox = new BoundingBoxXYZ
        {
            Min = new XYZ(0, 0, 0),
            Max = new XYZ(10, 10, 10)
        };

        // Act
        var vertices = boundingBox.ComputeVertices();

        // Assert
        await Assert.That(vertices.Count).IsEqualTo(8);
    }

    [Test]
    public async Task ComputeVertices_ValidBox_ContainsMinAndMax()
    {
        // Arrange
        var boundingBox = new BoundingBoxXYZ
        {
            Min = new XYZ(0, 0, 0),
            Max = new XYZ(10, 10, 10)
        };

        // Act
        var vertices = boundingBox.ComputeVertices();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(vertices.Any(v => v.IsAlmostEqualTo(boundingBox.Min))).IsTrue();
            await Assert.That(vertices.Any(v => v.IsAlmostEqualTo(boundingBox.Max))).IsTrue();
        }
    }
}
