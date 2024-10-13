namespace Nice3point.Revit.Extensions.Tests;

public sealed class ImperialTests
{
    [Fact]
    public void ToFractionTest()
    {
        Assert.Equal("0\"", 0d.ToFraction());
        Assert.Equal("0\"", (-0d).ToFraction());
        Assert.Equal("0\"", (-0.00001d).ToFraction());
        Assert.Equal("1/8\"", 0.0123d.ToFraction());
        Assert.Equal("3/4\"", 0.0666d.ToFraction());
        Assert.Equal("1'", 1d.ToFraction());
        Assert.Equal("-1'", (-1d).ToFraction());
        Assert.Equal("1'", 1.000001.ToFraction());
        Assert.Equal("1 1/2\"", 0.129.ToFraction());
        Assert.Equal("-1 1/2\"", (-0.129).ToFraction());
        Assert.Equal("-1'-3 1/8\"", (-1.26378).ToFraction());
        Assert.Equal("12'-1/8\"", 12.0123d.ToFraction());
        Assert.Equal("12'-1 1/4\"", 12.1d.ToFraction());
        Assert.Equal("15'-1 1/2\"", 15.125d.ToFraction());
        Assert.Equal("25'-2 5/8\"", 25.222d.ToFraction());
        Assert.Equal("25'-2 11/16\"", 25.222d.ToFraction(16));
        Assert.Equal("25'-2 5/8\"", 25.222d.ToFraction(8));
        Assert.Equal("25'-2 3/4\"", 25.222d.ToFraction(4));
        Assert.Equal("25'-3\"", 25.222d.ToFraction(1));
        Assert.Equal("-25'-1\"", (-25.222d).ToFraction(1));
    }

    [Fact]
    public void FromFractionTest()
    {
        Assert.Equal(0.000, "0\"".FromFraction(), 1e-3);
        Assert.Equal(0.000, "-0\"".FromFraction(), 1e-3);
        Assert.Equal(0.010, "1/8\"".FromFraction(), 1e-3);
        Assert.Equal(0.047, "0.573".FromFraction(), 1e-3);
        Assert.Equal(0.083, "1\"".FromFraction(), 1e-3);
        Assert.Equal(0.105, "1 17/64\"".FromFraction(), 1e-3);
        Assert.Equal(0.270, "3 1/4\"".FromFraction(), 1e-3);
        Assert.Equal(0.416, "5\"".FromFraction(), 1e-3);
        Assert.Equal(1.000, "1'".FromFraction(), 1e-3);
        Assert.Equal(1.000, "12".FromFraction(), 1e-3);
        Assert.Equal(1.007, "1'-3/32\"".FromFraction(), 1e-3);
        Assert.Equal(1.145, "1'1.75".FromFraction(), 1e-3);
        Assert.Equal(2.102, "2'-1 15/64\"".FromFraction(), 1e-3);
        Assert.Equal(74.75, "69'-69".FromFraction(), 1e-3);
        Assert.Equal(-2.102, "-2'-1 15/64\"".FromFraction(), 1e-3);
        Assert.Equal(-74.75, "-69'-69".FromFraction(), 1e-3);
        Assert.Equal(0d, "".FromFraction(), 1e-3);
        Assert.Equal(0d, "-".FromFraction(), 1e-3);
        Assert.True("-2'-1 15/64\"".TryFromFraction(out _));
        Assert.True("-69'-69".TryFromFraction(out _));
        Assert.True("-".TryFromFraction(out _));
        Assert.False("qwerty".TryFromFraction(out _));
        Assert.False(".".TryFromFraction(out _));
        Assert.False("".TryFromFraction(out _));
        Assert.Throws<FormatException>(() => "qwerty".FromFraction());
        Assert.Throws<FormatException>(() => ".".FromFraction());
        Assert.Throws<ArgumentNullException>(() => ((string)null!).FromFraction());
    }
}