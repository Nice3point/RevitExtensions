using System.Globalization;
using Color = System.Drawing.Color;

namespace Nice3point.Revit.Extensions.Internal.Formats;

/// <summary>
///     Helper class to easier work with color representation
/// </summary>
/// <remarks>
///     Implementation: https://github.com/microsoft/PowerToys/blob/main/src/modules/colorPicker/ColorPickerUI/Helpers/ColorRepresentationHelper.cs
/// </remarks>
internal static class ColorRepresentationUtils
{
    /// <summary>
    /// Return a <see cref="string"/> representation of a CMYK color
    /// </summary>
    /// <param name="color">The <see cref="System.Windows.Media.Color"/> for the CMYK color presentation</param>
    /// <returns>A <see cref="string"/> representation of a CMYK color</returns>
    internal static string ColorToCmyk(Color color)
    {
        var (cyan, magenta, yellow, blackKey) = ColorFormatUtils.ConvertToCmykColor(color);

        cyan = Math.Round(cyan * 100);
        magenta = Math.Round(magenta * 100);
        yellow = Math.Round(yellow * 100);
        blackKey = Math.Round(blackKey * 100);

        return $"{cyan.ToString(CultureInfo.InvariantCulture)}%"
               + $", {magenta.ToString(CultureInfo.InvariantCulture)}%"
               + $", {yellow.ToString(CultureInfo.InvariantCulture)}%"
               + $", {blackKey.ToString(CultureInfo.InvariantCulture)}%";
    }

    /// <summary>
    /// Return a hexadecimal <see cref="string"/> representation of a RGB color
    /// </summary>
    /// <param name="color">The <see cref="Color"/> for the hexadecimal presentation</param>
    /// <returns>A hexadecimal <see cref="string"/> representation of a RGB color</returns>
    internal static string ColorToHex(Color color)
    {
        const string hexFormat = "x2";

        return $"{color.R.ToString(hexFormat, CultureInfo.InvariantCulture)}"
               + $"{color.G.ToString(hexFormat, CultureInfo.InvariantCulture)}"
               + $"{color.B.ToString(hexFormat, CultureInfo.InvariantCulture)}";
    }

    /// <summary>
    /// Return a <see cref="string"/> representation of a HSB color
    /// </summary>
    /// <param name="color">The <see cref="Color"/> for the HSB color presentation</param>
    /// <returns>A <see cref="string"/> representation of a HSB color</returns>
    internal static string ColorToHsb(Color color)
    {
        var (hue, saturation, brightness) = ColorFormatUtils.ConvertToHsbColor(color);

        hue = Math.Round(hue);
        saturation = Math.Round(saturation * 100);
        brightness = Math.Round(brightness * 100);

        return $"{hue.ToString(CultureInfo.InvariantCulture)}"
               + $", {saturation.ToString(CultureInfo.InvariantCulture)}%"
               + $", {brightness.ToString(CultureInfo.InvariantCulture)}%";
    }

    /// <summary>
    /// Return a <see cref="string"/> representation float color styling(0.1f, 0.1f, 0.1f)
    /// </summary>
    /// <param name="color">The <see cref="Color"/> to convert</param>
    /// <returns>a string value (0.1f, 0.1f, 0.1f)</returns>
    internal static string ColorToFloat(Color color)
    {
        var (red, green, blue) = (color.R / 255d, color.G / 255d, color.B / 255d);
        const int precision = 2;
        const string floatFormat = "0.##";

        return $"{Math.Round(red, precision).ToString(floatFormat, CultureInfo.InvariantCulture)}f"
               + $", {Math.Round(green, precision).ToString(floatFormat, CultureInfo.InvariantCulture)}f"
               + $", {Math.Round(blue, precision).ToString(floatFormat, CultureInfo.InvariantCulture)}f, 1f";
    }

    /// <summary>
    /// Return a <see cref="string"/> representation decimal color value
    /// </summary>
    /// <param name="color">The <see cref="Color"/> to convert</param>
    /// <returns>a string value number</returns>
    internal static int ColorToDecimal(Color color)
    {
        return color.R * 65536 + color.G * 256 + color.B;
    }

    /// <summary>
    /// Return a <see cref="string"/> representation of a HSI color
    /// </summary>
    /// <param name="color">The <see cref="Color"/> for the HSI color presentation</param>
    /// <returns>A <see cref="string"/> representation of a HSI color</returns>
    internal static string ColorToHsi(Color color)
    {
        var (hue, saturation, intensity) = ColorFormatUtils.ConvertToHsiColor(color);

        hue = Math.Round(hue);
        saturation = Math.Round(saturation * 100);
        intensity = Math.Round(intensity * 100);

        return $"{hue.ToString(CultureInfo.InvariantCulture)}"
               + $", {saturation.ToString(CultureInfo.InvariantCulture)}%"
               + $", {intensity.ToString(CultureInfo.InvariantCulture)}%";
    }

    /// <summary>
    /// Return a <see cref="string"/> representation of a HSL color
    /// </summary>
    /// <param name="color">The <see cref="Color"/> for the HSL color presentation</param>
    /// <returns>A <see cref="string"/> representation of a HSL color</returns>
    internal static string ColorToHsl(Color color)
    {
        var (hue, saturation, lightness) = ColorFormatUtils.ConvertToHslColor(color);

        hue = Math.Round(hue);
        saturation = Math.Round(saturation * 100);
        lightness = Math.Round(lightness * 100);

        // Using InvariantCulture since this is used for color representation
        return $"{hue.ToString(CultureInfo.InvariantCulture)}"
               + $", {saturation.ToString(CultureInfo.InvariantCulture)}%"
               + $", {lightness.ToString(CultureInfo.InvariantCulture)}%";
    }

    /// <summary>
    /// Return a <see cref="string"/> representation of a HSV color
    /// </summary>
    /// <param name="color">The <see cref="Color"/> for the HSV color presentation</param>
    /// <returns>A <see cref="string"/> representation of a HSV color</returns>
    internal static string ColorToHsv(Color color)
    {
        var (hue, saturation, value) = ColorFormatUtils.ConvertToHsvColor(color);

        hue = Math.Round(hue);
        saturation = Math.Round(saturation * 100);
        value = Math.Round(value * 100);

        // Using InvariantCulture since this is used for color representation
        return $"{hue.ToString(CultureInfo.InvariantCulture)}"
               + $", {saturation.ToString(CultureInfo.InvariantCulture)}%"
               + $", {value.ToString(CultureInfo.InvariantCulture)}%";
    }

    /// <summary>
    /// Return a <see cref="string"/> representation of a HWB color
    /// </summary>
    /// <param name="color">The <see cref="Color"/> for the HWB color presentation</param>
    /// <returns>A <see cref="string"/> representation of a HWB color</returns>
    internal static string ColorToHwb(Color color)
    {
        var (hue, whiteness, blackness) = ColorFormatUtils.ConvertToHwbColor(color);

        hue = Math.Round(hue);
        whiteness = Math.Round(whiteness * 100);
        blackness = Math.Round(blackness * 100);

        return $"{hue.ToString(CultureInfo.InvariantCulture)}"
               + $", {whiteness.ToString(CultureInfo.InvariantCulture)}%"
               + $", {blackness.ToString(CultureInfo.InvariantCulture)}%";
    }

    /// <summary>
    /// Return a <see cref="string"/> representation of a natural color
    /// </summary>
    /// <param name="color">The <see cref="Color"/> for the natural color presentation</param>
    /// <returns>A <see cref="string"/> representation of a natural color</returns>
    internal static string ColorToNCol(Color color)
    {
        var (hue, whiteness, blackness) = ColorFormatUtils.ConvertToNaturalColor(color);

        whiteness = Math.Round(whiteness * 100);
        blackness = Math.Round(blackness * 100);

        return $"{hue}"
               + $", {whiteness.ToString(CultureInfo.InvariantCulture)}%"
               + $", {blackness.ToString(CultureInfo.InvariantCulture)}%";
    }

    /// <summary>
    /// Return a <see cref="string"/> representation of a RGB color
    /// </summary>
    /// <param name="color">The <see cref="Color"/> for the RGB color presentation</param>
    /// <returns>A <see cref="string"/> representation of a RGB color</returns>
    internal static string ColorToRgb(Color color)
        => $"{color.R.ToString(CultureInfo.InvariantCulture)}"
           + $", {color.G.ToString(CultureInfo.InvariantCulture)}"
           + $", {color.B.ToString(CultureInfo.InvariantCulture)}";

    /// <summary>
    /// Returns a <see cref="string"/> representation of a CIE LAB color
    /// </summary>
    /// <param name="color">The <see cref="Color"/> for the CIE LAB color presentation</param>
    /// <returns>A <see cref="string"/> representation of a CIE LAB color</returns>
    internal static string ColorToCielab(Color color)
    {
        var (lightness, chromaticityA, chromaticityB) = ColorFormatUtils.ConvertToCielabColor(color);
        lightness = Math.Round(lightness, 2);
        chromaticityA = Math.Round(chromaticityA, 2);
        chromaticityB = Math.Round(chromaticityB, 2);

        return $"{lightness.ToString(CultureInfo.InvariantCulture)}" +
               $", {chromaticityA.ToString(CultureInfo.InvariantCulture)}" +
               $", {chromaticityB.ToString(CultureInfo.InvariantCulture)}";
    }

    /// <summary>
    /// Returns a <see cref="string"/> representation of a CIE XYZ color
    /// </summary>
    /// <param name="color">The <see cref="Color"/> for the CIE XYZ color presentation</param>
    /// <returns>A <see cref="string"/> representation of a CIE XYZ color</returns>
    internal static string ColorToCieXyz(Color color)
    {
        var (x, y, z) = ColorFormatUtils.ConvertToCiexyzColor(color);

        x = Math.Round(x * 100, 4);
        y = Math.Round(y * 100, 4);
        z = Math.Round(z * 100, 4);

        return $"{x.ToString(CultureInfo.InvariantCulture)}" +
               $", {y.ToString(CultureInfo.InvariantCulture)}" +
               $", {z.ToString(CultureInfo.InvariantCulture)}";
    }

    /// <summary>
    /// Return a hexadecimal integer <see cref="string"/> representation of a RGB color
    /// </summary>
    /// <param name="color">The <see cref="Color"/> for the hexadecimal integer presentation</param>
    /// <returns>A hexadecimal integer <see cref="string"/> representation of a RGB color</returns>
    internal static string ColorToHexInteger(Color color)
    {
        const string hexFormat = "X2";

        return "0xFF"
               + $"{color.R.ToString(hexFormat, CultureInfo.InvariantCulture)}"
               + $"{color.G.ToString(hexFormat, CultureInfo.InvariantCulture)}"
               + $"{color.B.ToString(hexFormat, CultureInfo.InvariantCulture)}";
    }
    
    [Pure]
    private static Color ConvertHexStringToColor(string hex)
    {
        var red = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
        var green = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
        var blue = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);

        return Color.FromArgb(red, green, blue);
    }

    [Pure]
    private static double CalculateColorDistance(Color color1, Color color2)
    {
        var deltaR = color1.R - color2.R;
        var deltaG = color1.G - color2.G;
        var deltaB = color1.B - color2.B;

        return Math.Sqrt(deltaR * deltaR + deltaG * deltaG + deltaB * deltaB);
    }
}