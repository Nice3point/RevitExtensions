namespace Nice3point.Revit.Extensions.Tests;

public sealed class DoubleTests
{
    [Fact]
    public void IsAlmostEqualTest()
    {
        Assert.True(0.09999d.IsAlmostEqual(0.1, 1e-3));
        Assert.True(0.063636.IsAlmostEqual(0.06, 1e-2));
        Assert.True(1e-15.IsAlmostEqual(0));
    }

    [Fact]
    public void RoundTest()
    {
        Assert.Equal(0, 1e-15.Round(), 1);
        Assert.Equal(0.1, 0.099999.Round(2), 1);
    }
}