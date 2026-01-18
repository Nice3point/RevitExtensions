using Nice3point.TUnit.Revit;
using Nice3point.TUnit.Revit.Executors;
using TUnit.Core.Executors;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class BoundingBoxXyzExtensionsTests : RevitApiTest
{
    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task Contains_PointInsideBox_ReturnsTrue()
    {
        var boundingBox = new BoundingBoxXYZ
        {
            Min = new XYZ(0, 0, 0),
            Max = new XYZ(10, 10, 10)
        };

        var pointInside = new XYZ(5, 5, 5);

        await Assert.That(boundingBox.Contains(pointInside)).IsTrue();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task Contains_PointOutsideBox_ReturnsFalse()
    {
        var boundingBox = new BoundingBoxXYZ
        {
            Min = new XYZ(0, 0, 0),
            Max = new XYZ(10, 10, 10)
        };

        var pointOutside = new XYZ(15, 15, 15);

        await Assert.That(boundingBox.Contains(pointOutside)).IsFalse();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task Contains_PointOnBorder_ReturnsTrue()
    {
        var boundingBox = new BoundingBoxXYZ
        {
            Min = new XYZ(0, 0, 0),
            Max = new XYZ(10, 10, 10)
        };

        var pointOnBorder = new XYZ(10, 10, 10);

        await Assert.That(boundingBox.Contains(pointOnBorder)).IsTrue();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task Contains_PointOnBorderStrictMode_ReturnsFalse()
    {
        var boundingBox = new BoundingBoxXYZ
        {
            Min = new XYZ(0, 0, 0),
            Max = new XYZ(10, 10, 10)
        };

        var pointOnBorder = new XYZ(10, 10, 10);

        await Assert.That(boundingBox.Contains(pointOnBorder, strict: true)).IsFalse();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task Contains_BoxInsideBox_ReturnsTrue()
    {
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

        await Assert.That(outerBox.Contains(innerBox)).IsTrue();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task Contains_BoxOutsideBox_ReturnsFalse()
    {
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

        await Assert.That(box1.Contains(box2)).IsFalse();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task Overlaps_OverlappingBoxes_ReturnsTrue()
    {
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

        await Assert.That(box1.Overlaps(box2)).IsTrue();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task Overlaps_NonOverlappingBoxes_ReturnsFalse()
    {
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

        await Assert.That(box1.Overlaps(box2)).IsFalse();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task ComputeCentroid_ValidBox_ReturnsCenter()
    {
        var boundingBox = new BoundingBoxXYZ
        {
            Min = new XYZ(0, 0, 0),
            Max = new XYZ(10, 10, 10)
        };

        var centroid = boundingBox.ComputeCentroid();

        using (Assert.Multiple())
        {
            await Assert.That(centroid.X).IsEqualTo(5).Within(1e-9);
            await Assert.That(centroid.Y).IsEqualTo(5).Within(1e-9);
            await Assert.That(centroid.Z).IsEqualTo(5).Within(1e-9);
        }
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task ComputeVolume_ValidBox_ReturnsCorrectVolume()
    {
        var boundingBox = new BoundingBoxXYZ
        {
            Min = new XYZ(0, 0, 0),
            Max = new XYZ(10, 10, 10)
        };

        var volume = boundingBox.ComputeVolume();

        await Assert.That(volume).IsEqualTo(1000).Within(1e-9);
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task ComputeSurfaceArea_ValidBox_ReturnsCorrectArea()
    {
        var boundingBox = new BoundingBoxXYZ
        {
            Min = new XYZ(0, 0, 0),
            Max = new XYZ(10, 10, 10)
        };

        var surfaceArea = boundingBox.ComputeSurfaceArea();

        // Surface area of cube: 6 * side^2 = 6 * 100 = 600
        await Assert.That(surfaceArea).IsEqualTo(600).Within(1e-9);
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task ComputeVertices_ValidBox_ReturnsEightVertices()
    {
        var boundingBox = new BoundingBoxXYZ
        {
            Min = new XYZ(0, 0, 0),
            Max = new XYZ(10, 10, 10)
        };

        var vertices = boundingBox.ComputeVertices();

        await Assert.That(vertices.Count).IsEqualTo(8);
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task ComputeVertices_ValidBox_ContainsMinAndMax()
    {
        var boundingBox = new BoundingBoxXYZ
        {
            Min = new XYZ(0, 0, 0),
            Max = new XYZ(10, 10, 10)
        };

        var vertices = boundingBox.ComputeVertices();

        using (Assert.Multiple())
        {
            await Assert.That(vertices.Any(v => v.IsAlmostEqualTo(boundingBox.Min))).IsTrue();
            await Assert.That(vertices.Any(v => v.IsAlmostEqualTo(boundingBox.Max))).IsTrue();
        }
    }
}