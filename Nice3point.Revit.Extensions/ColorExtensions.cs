using Nice3point.Revit.Extensions.Formats;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit and System Color Extensions
/// </summary>
[PublicAPI]
public static class ColorExtensions
{
    /// <summary>
    ///     Return a hexadecimal <see cref="string"/> representation of a RGB color
    /// </summary>
    /// <param name="color">The <see cref="Color"/> for the hexadecimal presentation</param>
    /// <returns>A hexadecimal <see cref="string"/> representation of a RGB color</returns>
    [Pure]
    public static string ToHex(this Color color)
    {
        return ColorRepresentationUtils.ColorToHex(color.GetDrawingColor());
    }

    /// <summary>
    /// Return a hexadecimal integer <see cref="string"/> representation of a RGB color
    /// </summary>
    /// <param name="color">The <see cref="Color"/> for the hexadecimal integer presentation</param>
    /// <returns>A hexadecimal integer <see cref="string"/> representation of a RGB color</returns>
    [Pure]
    public static string ToHexInteger(this Color color)
    {
        return ColorRepresentationUtils.ColorToHexInteger(color.GetDrawingColor());
    }

    /// <summary>
    /// Return a <see cref="string"/> representation of a RGB color
    /// </summary>
    /// <param name="color">The <see cref="Color"/> for the RGB color presentation</param>
    /// <returns>A <see cref="string"/> representation of a RGB color</returns>
    [Pure]
    public static string ToRgb(this Color color)
    {
        return ColorRepresentationUtils.ColorToRgb(color.GetDrawingColor());
    }

    /// <summary>
    /// Return a <see cref="string"/> representation of a HSL color
    /// </summary>
    /// <param name="color">The <see cref="Color"/> for the HSL color presentation</param>
    /// <returns>A <see cref="string"/> representation of a HSL color</returns>
    [Pure]
    public static string ToHsl(this Color color)
    {
        return ColorRepresentationUtils.ColorToHsl(color.GetDrawingColor());
    }

    /// <summary>
    /// Return a <see cref="string"/> representation of a HSV color
    /// </summary>
    /// <param name="color">The <see cref="Color"/> for the HSV color presentation</param>
    /// <returns>A <see cref="string"/> representation of a HSV color</returns>
    [Pure]
    public static string ToHsv(this Color color)
    {
        return ColorRepresentationUtils.ColorToHsv(color.GetDrawingColor());
    }

    /// <summary>
    /// Return a <see cref="string"/> representation of a CMYK color
    /// </summary>
    /// <param name="color">The <see cref="System.Windows.Media.Color"/> for the CMYK color presentation</param>
    /// <returns>A <see cref="string"/> representation of a CMYK color</returns>
    [Pure]
    public static string ToCmyk(this Color color)
    {
        return ColorRepresentationUtils.ColorToCmyk(color.GetDrawingColor());
    }

    /// <summary>
    /// Return a <see cref="string"/> representation of a HSB color
    /// </summary>
    /// <param name="color">The <see cref="Color"/> for the HSB color presentation</param>
    /// <returns>A <see cref="string"/> representation of a HSB color</returns>
    [Pure]
    public static string ToHsb(this Color color)
    {
        return ColorRepresentationUtils.ColorToHsb(color.GetDrawingColor());
    }

    /// <summary>
    /// Return a <see cref="string"/> representation of a HSI color
    /// </summary>
    /// <param name="color">The <see cref="Color"/> for the HSI color presentation</param>
    /// <returns>A <see cref="string"/> representation of a HSI color</returns>
    [Pure]
    public static string ToHsi(this Color color)
    {
        return ColorRepresentationUtils.ColorToHsi(color.GetDrawingColor());
    }

    /// <summary>
    /// Return a <see cref="string"/> representation of a HWB color
    /// </summary>
    /// <param name="color">The <see cref="Color"/> for the HWB color presentation</param>
    /// <returns>A <see cref="string"/> representation of a HWB color</returns>
    [Pure]
    public static string ToHwb(this Color color)
    {
        return ColorRepresentationUtils.ColorToHwb(color.GetDrawingColor());
    }

    /// <summary>
    /// Return a <see cref="string"/> representation of a natural color
    /// </summary>
    /// <param name="color">The <see cref="Color"/> for the natural color presentation</param>
    /// <returns>A <see cref="string"/> representation of a natural color</returns>
    [Pure]
    public static string ToNCol(this Color color)
    {
        return ColorRepresentationUtils.ColorToNCol(color.GetDrawingColor());
    }

    /// <summary>
    /// Returns a <see cref="string"/> representation of a CIE LAB color
    /// </summary>
    /// <param name="color">The <see cref="Color"/> for the CIE LAB color presentation</param>
    /// <returns>A <see cref="string"/> representation of a CIE LAB color</returns>
    [Pure]
    public static string ToCielab(this Color color)
    {
        return ColorRepresentationUtils.ColorToCielab(color.GetDrawingColor());
    }

    /// <summary>
    /// Returns a <see cref="string"/> representation of a CIE XYZ color
    /// </summary>
    /// <param name="color">The <see cref="Color"/> for the CIE XYZ color presentation</param>
    /// <returns>A <see cref="string"/> representation of a CIE XYZ color</returns>
    [Pure]
    public static string ToCieXyz(this Color color)
    {
        return ColorRepresentationUtils.ColorToCieXyz(color.GetDrawingColor());
    }

    /// <summary>
    /// Return a <see cref="string"/> representation float color styling(0.1f, 0.1f, 0.1f)
    /// </summary>
    /// <param name="color">The <see cref="Color"/> to convert</param>
    /// <returns>a string value (0.1f, 0.1f, 0.1f)</returns>
    [Pure]
    public static string ToFloat(this Color color)
    {
        return ColorRepresentationUtils.ColorToFloat(color.GetDrawingColor());
    }

    /// <summary>
    /// Return a <see cref="string"/> representation decimal color value
    /// </summary>
    /// <param name="color">The <see cref="Color"/> to convert</param>
    /// <returns>a string value number</returns>
    [Pure]
    public static int ToDecimal(this Color color)
    {
        return ColorRepresentationUtils.ColorToDecimal(color.GetDrawingColor());
    }
}