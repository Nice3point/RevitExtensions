#if REVIT2025_OR_GREATER
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.AnnotationMultipleAlignmentUtils"/> class.
/// </summary>
[PublicAPI]
public static class AnnotationMultipleAlignmentUtilsExtensions
{
    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.AnnotationMultipleAlignmentUtils.ElementSupportsMultiAlign(Autodesk.Revit.DB.Element)"/>
        public bool IsMultiAlignSupported => AnnotationMultipleAlignmentUtils.ElementSupportsMultiAlign(element);

        /// <inheritdoc cref="Autodesk.Revit.DB.AnnotationMultipleAlignmentUtils.GetAnnotationOutlineWithoutLeaders(Autodesk.Revit.DB.Element)"/>
        [Pure]
        public IList<XYZ> GetAnnotationOutlineWithoutLeaders()
        {
            return AnnotationMultipleAlignmentUtils.GetAnnotationOutlineWithoutLeaders(element);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AnnotationMultipleAlignmentUtils.MoveWithAnchoredLeaders(Autodesk.Revit.DB.Element,Autodesk.Revit.DB.XYZ)"/>
        public void MoveWithAnchoredLeaders(XYZ moveVector)
        {
            AnnotationMultipleAlignmentUtils.MoveWithAnchoredLeaders(element, moveVector);
        }
    }
}
#endif