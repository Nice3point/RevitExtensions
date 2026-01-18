using Nice3point.Revit.Extensions.SystemExtensions;
using Nice3point.TUnit.Revit;
using Nice3point.TUnit.Revit.Executors;
using TUnit.Core.Executors;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class SystemExtensionsTests : RevitApiTest
{
    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task Round_DefaultPrecision_RoundsTo9Decimals()
    {
        var value = 6.56170000000000000000000001;

        var rounded = value.Round();

        await Assert.That(rounded).IsEqualTo(6.5617);
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task Round_ZeroDecimals_RoundsToInteger()
    {
        var value = 6.56170000000000000000000001;

        var rounded = value.Round(0);

        await Assert.That(rounded).IsEqualTo(7);
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task Round_TwoDecimals_RoundsCorrectly()
    {
        var value = 6.56789;

        var rounded = value.Round(2);

        await Assert.That(rounded).IsEqualTo(6.57);
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task IsAlmostEqual_DefaultTolerance_SmallDifferenceReturnsTrue()
    {
        var value1 = 6.56170000000000000000000001;
        var value2 = 6.5617;

        var result = value1.IsAlmostEqual(value2);

        await Assert.That(result).IsTrue();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task IsAlmostEqual_DefaultTolerance_LargeDifferenceReturnsFalse()
    {
        var value1 = 6.5617;
        var value2 = 6.6;

        var result = value1.IsAlmostEqual(value2);

        await Assert.That(result).IsFalse();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task IsAlmostEqual_CustomTolerance_WithinToleranceReturnsTrue()
    {
        var value1 = 6.56170000000001;
        var value2 = 6.6;

        var result = value1.IsAlmostEqual(value2, 1e-1);

        await Assert.That(result).IsTrue();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task IsAlmostEqual_CustomTolerance_OutsideToleranceReturnsFalse()
    {
        var value1 = 6.5;
        var value2 = 6.7;

        var result = value1.IsAlmostEqual(value2, 1e-1);

        await Assert.That(result).IsFalse();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task IsNullOrEmpty_EmptyString_ReturnsTrue()
    {
        var value = string.Empty;

        var result = value.IsNullOrEmpty();

        await Assert.That(result).IsTrue();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task IsNullOrEmpty_NullString_ReturnsTrue()
    {
        string? value = null;

        var result = value.IsNullOrEmpty();

        await Assert.That(result).IsTrue();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task IsNullOrEmpty_NonEmptyString_ReturnsFalse()
    {
        var value = "Hello";

        var result = value.IsNullOrEmpty();

        await Assert.That(result).IsFalse();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task IsNullOrWhiteSpace_WhiteSpaceString_ReturnsTrue()
    {
        var value = "   ";

        var result = value.IsNullOrWhiteSpace();

        await Assert.That(result).IsTrue();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task IsNullOrWhiteSpace_NullString_ReturnsTrue()
    {
        string? value = null;

        var result = value.IsNullOrWhiteSpace();

        await Assert.That(result).IsTrue();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task IsNullOrWhiteSpace_NonWhiteSpaceString_ReturnsFalse()
    {
        var value = "Hello";

        var result = value.IsNullOrWhiteSpace();

        await Assert.That(result).IsFalse();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task AppendPath_TwoPaths_CombinesCorrectly()
    {
        var basePath = @"C:\Folder";
        var subPath = "AddIn";

        var result = basePath.AppendPath(subPath);

        await Assert.That(result).IsEqualTo(@"C:\Folder\AddIn");
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task Cast_ValidCast_ReturnsCorrectType()
    {
        object obj = "Hello World";

        var result = obj.Cast<string>();

        using (Assert.Multiple())
        {
            await Assert.That(result).IsTypeOf<string>();
            await Assert.That(result).IsEqualTo("Hello World");
        }
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task Cast_InvalidCast_ThrowsException()
    {
        object obj = "Hello World";

        await Assert.That(() => obj.Cast<int>()).Throws<InvalidCastException>();
    }
}