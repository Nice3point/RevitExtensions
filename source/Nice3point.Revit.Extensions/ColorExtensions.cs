using Nice3point.Revit.Extensions.Internal.Formats;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit and System Color Extensions
/// </summary>
[PublicAPI]
public static class ColorExtensions
{
    /// <param name="color">The <see cref="Color"/> for the hexadecimal presentation</param>
    extension(Color color)
    {
        /// <summary>
        ///     Return a hexadecimal <see cref="string"/> representation of a RGB color
        /// </summary>
        /// <returns>A hexadecimal <see cref="string"/> representation of a RGB color</returns>
        [Pure]
        public string ToHex()
        {
            return ColorRepresentationUtils.ColorToHex(color.GetDrawingColor());
        }

        /// <summary>
        /// Return a hexadecimal integer <see cref="string"/> representation of a RGB color
        /// </summary>
        /// <returns>A hexadecimal integer <see cref="string"/> representation of a RGB color</returns>
        [Pure]
        public string ToHexInteger()
        {
            return ColorRepresentationUtils.ColorToHexInteger(color.GetDrawingColor());
        }

        /// <summary>
        /// Return a <see cref="string"/> representation of a RGB color
        /// </summary>
        /// <returns>A <see cref="string"/> representation of a RGB color</returns>
        [Pure]
        public string ToRgb()
        {
            return ColorRepresentationUtils.ColorToRgb(color.GetDrawingColor());
        }

        /// <summary>
        /// Return a <see cref="string"/> representation of a HSL color
        /// </summary>
        /// <returns>A <see cref="string"/> representation of a HSL color</returns>
        [Pure]
        public string ToHsl()
        {
            return ColorRepresentationUtils.ColorToHsl(color.GetDrawingColor());
        }

        /// <summary>
        /// Return a <see cref="string"/> representation of a HSV color
        /// </summary>
        /// <returns>A <see cref="string"/> representation of a HSV color</returns>
        [Pure]
        public string ToHsv()
        {
            return ColorRepresentationUtils.ColorToHsv(color.GetDrawingColor());
        }

        /// <summary>
        /// Return a <see cref="string"/> representation of a CMYK color
        /// </summary>
        /// <returns>A <see cref="string"/> representation of a CMYK color</returns>
        [Pure]
        public string ToCmyk()
        {
            return ColorRepresentationUtils.ColorToCmyk(color.GetDrawingColor());
        }

        /// <summary>
        /// Return a <see cref="string"/> representation of a HSB color
        /// </summary>
        /// <returns>A <see cref="string"/> representation of a HSB color</returns>
        [Pure]
        public string ToHsb()
        {
            return ColorRepresentationUtils.ColorToHsb(color.GetDrawingColor());
        }

        /// <summary>
        /// Return a <see cref="string"/> representation of a HSI color
        /// </summary>
        /// <returns>A <see cref="string"/> representation of a HSI color</returns>
        [Pure]
        public string ToHsi()
        {
            return ColorRepresentationUtils.ColorToHsi(color.GetDrawingColor());
        }

        /// <summary>
        /// Return a <see cref="string"/> representation of a HWB color
        /// </summary>
        /// <returns>A <see cref="string"/> representation of a HWB color</returns>
        [Pure]
        public string ToHwb()
        {
            return ColorRepresentationUtils.ColorToHwb(color.GetDrawingColor());
        }

        /// <summary>
        /// Return a <see cref="string"/> representation of a natural color
        /// </summary>
        /// <returns>A <see cref="string"/> representation of a natural color</returns>
        [Pure]
        public string ToNCol()
        {
            return ColorRepresentationUtils.ColorToNCol(color.GetDrawingColor());
        }

        /// <summary>
        /// Returns a <see cref="string"/> representation of a CIE LAB color
        /// </summary>
        /// <returns>A <see cref="string"/> representation of a CIE LAB color</returns>
        [Pure]
        public string ToCielab()
        {
            return ColorRepresentationUtils.ColorToCielab(color.GetDrawingColor());
        }

        /// <summary>
        /// Returns a <see cref="string"/> representation of a CIE XYZ color
        /// </summary>
        /// <returns>A <see cref="string"/> representation of a CIE XYZ color</returns>
        [Pure]
        public string ToCieXyz()
        {
            return ColorRepresentationUtils.ColorToCieXyz(color.GetDrawingColor());
        }

        /// <summary>
        /// Return a <see cref="string"/> representation float color styling(0.1f, 0.1f, 0.1f)
        /// </summary>
        /// <returns>a string value (0.1f, 0.1f, 0.1f)</returns>
        [Pure]
        public string ToFloat()
        {
            return ColorRepresentationUtils.ColorToFloat(color.GetDrawingColor());
        }

        /// <summary>
        /// Return a <see cref="string"/> representation decimal color value
        /// </summary>
        /// <returns>a string value number</returns>
        [Pure]
        public int ToDecimal()
        {
            return ColorRepresentationUtils.ColorToDecimal(color.GetDrawingColor());
        }
    }
}