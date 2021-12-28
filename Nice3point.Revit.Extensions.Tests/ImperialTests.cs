using NUnit.Framework;

namespace Nice3point.Revit.Extensions.Tests
{
    [TestFixture]
    public class ImperialTests
    {
        [TestCase]
        public void ToFractionTest()
        {
            Assert.AreEqual("0\"", 0d.ToFraction());
            Assert.AreEqual("0\"", (-0d).ToFraction());
            Assert.AreEqual("0\"", (-0.00001d).ToFraction());
            Assert.AreEqual("1\"", 1d.ToFraction());
            Assert.AreEqual("-1\"", (-1d).ToFraction());
            Assert.AreEqual("0 1/8\"", 0.129.ToFraction());
            Assert.AreEqual("-0 1/8\"", (-0.129).ToFraction());
            Assert.AreEqual("-1 1/4\"", (-1.26378).ToFraction());
            Assert.AreEqual("5\"", 5.000001.ToFraction());
            Assert.AreEqual("3 1/4\"", 3.24997.ToFraction());
            Assert.AreEqual("3 1/4\"", 3.25001.ToFraction());
            Assert.AreEqual("1'-0\"", 12d.ToFraction());
            Assert.AreEqual("1'-0 3/32\"", 12.1d.ToFraction());
            Assert.AreEqual("1'-1\"", 13d.ToFraction());
            Assert.AreEqual("1'-3 1/8\"", 15.125d.ToFraction());
            Assert.AreEqual("1'-0\"", 12.00001d.ToFraction());
            Assert.AreEqual("-1'-0\"", (-12.00001d).ToFraction());
            Assert.AreEqual("-2'-1 7/32\"", (-25.231d).ToFraction());
        }

        [TestCase]
        public void FromFractionTest()
        {
            Assert.AreEqual("0\"".FromFraction(), 0d, 1e-4);
            Assert.AreEqual("-0\"".FromFraction(), 0d, 1e-4);
            Assert.AreEqual("1\"".FromFraction(), 1d, 1e-4);
            Assert.AreEqual("-1\"".FromFraction(), -1d, 1e-4);
            Assert.AreEqual("1/8\"".FromFraction(), 0.125, 1e-4);
            Assert.AreEqual("-1/8\"".FromFraction(), -0.125, 1e-4);
            Assert.AreEqual("-1 17/64\"".FromFraction(), -1.265625, 1e-4);
            Assert.AreEqual("5\"".FromFraction(), 5d, 1e-4);
            Assert.AreEqual("3 1/4\"".FromFraction(), 3.25, 1e-4);
            Assert.AreEqual("3 1/4\"".FromFraction(), 3.25, 1e-4);
            Assert.AreEqual("1'".FromFraction(), 12d, 1e-4);
            Assert.AreEqual("1'-3/32\"".FromFraction(), 12.09375d, 1e-4);
            Assert.AreEqual("1'".FromFraction(), 12d, 1e-4);
            Assert.AreEqual("69'-69".FromFraction(), 897d, 1e-4);
            Assert.AreEqual("-1'".FromFraction(), -12d, 1e-4);
            Assert.AreEqual("-2'-1 15/64\"".FromFraction(), -25.23437d, 1e-4);
            Assert.AreEqual("0.573".FromFraction(), 0.573d, 1e-4);
            Assert.AreEqual("1'1.75".FromFraction(), 13.75, 1e-4);
            Assert.AreEqual("".FromFraction(), 0d, 1e-4);
            Assert.AreEqual("-".FromFraction(), 0d, 1e-4);
            Assert.Throws<FormatException>(() => "qwerty".FromFraction());
            Assert.Throws<FormatException>(() => ".".FromFraction());
        }
    }
}