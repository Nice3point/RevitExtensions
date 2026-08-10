using Nice3point.Revit.Extensions.Runtime;
using Nice3point.TUnit.Revit;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class SystemExtensionsTests : RevitApiTest
{
    [Test]
    public async Task Round_DefaultPrecision_RoundsTo9Decimals()
    {
        // Arrange
        var value = 6.56170000000000000000000001;

        // Act
        var rounded = value.Round();

        // Assert
        await Assert.That(rounded).IsEqualTo(6.5617);
    }

    [Test]
    public async Task Round_ZeroDecimals_RoundsToInteger()
    {
        // Arrange
        var value = 6.56170000000000000000000001;

        // Act
        var rounded = value.Round(0);

        // Assert
        await Assert.That(rounded).IsEqualTo(7);
    }

    [Test]
    public async Task Round_TwoDecimals_RoundsCorrectly()
    {
        // Arrange
        var value = 6.56789;

        // Act
        var rounded = value.Round(2);

        // Assert
        await Assert.That(rounded).IsEqualTo(6.57);
    }

    [Test]
    public async Task IsAlmostEqual_DefaultTolerance_SmallDifferenceReturnsTrue()
    {
        // Arrange
        var value1 = 6.56170000000000000000000001;
        var value2 = 6.5617;

        // Act
        var result = value1.IsAlmostEqual(value2);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsAlmostEqual_DefaultTolerance_LargeDifferenceReturnsFalse()
    {
        // Arrange
        var value1 = 6.5617;
        var value2 = 6.6;

        // Act
        var result = value1.IsAlmostEqual(value2);

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsAlmostEqual_CustomTolerance_WithinToleranceReturnsTrue()
    {
        // Arrange
        var value1 = 6.56170000000001;
        var value2 = 6.6;

        // Act
        var result = value1.IsAlmostEqual(value2, 1e-1);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsAlmostEqual_CustomTolerance_OutsideToleranceReturnsFalse()
    {
        // Arrange
        var value1 = 6.5;
        var value2 = 6.7;

        // Act
        var result = value1.IsAlmostEqual(value2, 1e-1);

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsNullOrEmpty_EmptyString_ReturnsTrue()
    {
        // Arrange
        var value = string.Empty;

        // Act
        var result = value.IsNullOrEmpty();

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsNullOrEmpty_NullString_ReturnsTrue()
    {
        // Arrange
        string? value = null;

        // Act
        var result = value.IsNullOrEmpty();

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsNullOrEmpty_NonEmptyString_ReturnsFalse()
    {
        // Arrange
        var value = "Hello";

        // Act
        var result = value.IsNullOrEmpty();

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsNullOrWhiteSpace_WhiteSpaceString_ReturnsTrue()
    {
        // Arrange
        var value = "   ";

        // Act
        var result = value.IsNullOrWhiteSpace();

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsNullOrWhiteSpace_NullString_ReturnsTrue()
    {
        // Arrange
        string? value = null;

        // Act
        var result = value.IsNullOrWhiteSpace();

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsNullOrWhiteSpace_NonWhiteSpaceString_ReturnsFalse()
    {
        // Arrange
        var value = "Hello";

        // Act
        var result = value.IsNullOrWhiteSpace();

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task AppendPath_TwoPaths_CombinesCorrectly()
    {
        // Arrange
        var basePath = @"C:\Folder";
        var subPath = "AddIn";

        // Act
        var result = basePath.AppendPath(subPath);

        // Assert
        await Assert.That(result).IsEqualTo(@"C:\Folder\AddIn");
    }

    [Test]
    public async Task Cast_ValidCast_ReturnsCorrectType()
    {
        // Arrange
        object obj = "Hello World";

        // Act
        var result = obj.Cast<string>();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(result).IsTypeOf<string>();
            await Assert.That(result).IsEqualTo("Hello World");
        }
    }

    [Test]
    public async Task Cast_InvalidCast_ThrowsException()
    {
        // Arrange
        object obj = "Hello World";

        // Act / Assert
        await Assert.That(() => obj.Cast<int>()).Throws<InvalidCastException>();
    }
}
