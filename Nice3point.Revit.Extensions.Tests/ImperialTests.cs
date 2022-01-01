using System;
using NUnit.Framework;

namespace Nice3point.Revit.Extensions.Tests;

[TestFixture]
public class ImperialTests
{
    [TestCase]
    public void ToFractionTest()
    {
        Assert.AreEqual("0\"", 0d.ToFraction());
        Assert.AreEqual("0\"", (-0d).ToFraction());
        Assert.AreEqual("0\"", (-0.00001d).ToFraction());
        Assert.AreEqual("0 5/32\"", 0.0123d.ToFraction());
        Assert.AreEqual("0 13/16\"", 0.0666d.ToFraction());
        Assert.AreEqual("1'-0\"", 1d.ToFraction());
        Assert.AreEqual("-1'-0\"", (-1d).ToFraction());
        Assert.AreEqual("1'-0\"", 1.000001.ToFraction());
        Assert.AreEqual("1 9/16\"", 0.129.ToFraction());
        Assert.AreEqual("-1 9/16\"", (-0.129).ToFraction());
        Assert.AreEqual("-1'-3 5/32\"", (-1.26378).ToFraction());
        Assert.AreEqual("12'-1 3/16\"", 12.1d.ToFraction());
        Assert.AreEqual("15'-1 1/2\"", 15.125d.ToFraction());
        Assert.AreEqual("25'-2 21/32\"", 25.222d.ToFraction());
        Assert.AreEqual("25'-2 11/16\"", 25.222d.ToFraction(16));
        Assert.AreEqual("25'-2 5/8\"", 25.222d.ToFraction(8));
        Assert.AreEqual("25'-2 3/4\"", 25.222d.ToFraction(4));
        Assert.AreEqual("25'-3\"", 25.222d.ToFraction(1));
        Assert.AreEqual("-25'-1\"", (-25.222d).ToFraction(1));
        Assert.AreEqual("0\"", 0.ToFraction());
        Assert.AreEqual("1'-0\"", 1.ToFraction());
        Assert.AreEqual("2'-0\"", 2.ToFraction());
    }

    [TestCase]
    public void FromFractionTest()
    {
        Assert.AreEqual(0.000, "0\"".FromFraction(), 1e-3);
        Assert.AreEqual(0.000, "-0\"".FromFraction(), 1e-3);
        Assert.AreEqual(0.010, "1/8\"".FromFraction(), 1e-3);
        Assert.AreEqual(0.047, "0.573".FromFraction(), 1e-3);
        Assert.AreEqual(0.083, "1\"".FromFraction(), 1e-3);
        Assert.AreEqual(0.105, "1 17/64\"".FromFraction(), 1e-3);
        Assert.AreEqual(0.270, "3 1/4\"".FromFraction(), 1e-3);
        Assert.AreEqual(0.416, "5\"".FromFraction(), 1e-3);
        Assert.AreEqual(1.000, "1'".FromFraction(), 1e-3);
        Assert.AreEqual(1.000, "12".FromFraction(), 1e-3);
        Assert.AreEqual(1.007, "1'-3/32\"".FromFraction(), 1e-3);
        Assert.AreEqual(1.145, "1'1.75".FromFraction(), 1e-3);
        Assert.AreEqual(2.102, "2'-1 15/64\"".FromFraction(), 1e-3);
        Assert.AreEqual(74.75, "69'-69".FromFraction(), 1e-3);
        Assert.AreEqual(-2.102, "-2'-1 15/64\"".FromFraction(), 1e-3);
        Assert.AreEqual(-74.75, "-69'-69".FromFraction(), 1e-3);
        Assert.AreEqual(0d, "".FromFraction(), 1e-3);
        Assert.AreEqual(0d, "-".FromFraction(), 1e-3);
        Assert.Throws<FormatException>(() => "qwerty".FromFraction());
        Assert.Throws<FormatException>(() => ".".FromFraction());   
        Assert.Throws<ArgumentNullException>(() => ((string)null!).FromFraction());
        Assert.AreEqual(true, "-2'-1 15/64\"".FromFraction(out _));
        Assert.AreEqual(true, "-69'-69".FromFraction(out _));
        Assert.AreEqual(true, "".FromFraction(out _));
        Assert.AreEqual(true, "-".FromFraction(out _));
        Assert.AreEqual(false, "qwerty".FromFraction(out _));
        Assert.AreEqual(false, ".".FromFraction(out _));
        Assert.AreEqual(false, ((string)null!).FromFraction(out _));
    }
}