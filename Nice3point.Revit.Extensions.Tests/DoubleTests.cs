using NUnit.Framework;

namespace Nice3point.Revit.Extensions.Tests;

[TestFixture]
public class DoubleTests
{
    [TestCase]
    public void IsAlmostEqualTest()
    {
        Assert.AreEqual(true, 0.09999.IsAlmostEqual(0.1, 1e-3));
        Assert.AreEqual(true, 0.063636.IsAlmostEqual(0.06, 1e-2));
        Assert.AreEqual(true, 1e-15.IsAlmostEqual(0));
    }

    [TestCase]
    public void RoundTest()
    {
        Assert.AreEqual(0, 1e-15.Round());
        Assert.AreEqual(0.1, 0.099999.Round(2));
    }
}