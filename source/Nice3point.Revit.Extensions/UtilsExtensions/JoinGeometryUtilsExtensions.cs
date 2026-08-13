// ReSharper disable once CheckNamespace

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.JoinGeometryUtils" /> class.
/// </summary>
[PublicAPI]
public static class JoinGeometryUtilsExtensions
{
    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.JoinGeometryUtils.JoinGeometry(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Element,Autodesk.Revit.DB.Element)" />
        public Element JoinGeometry(Element secondElement)
        {
            JoinGeometryUtils.JoinGeometry(element.Document, element, secondElement);
            return element;
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.JoinGeometryUtils.UnjoinGeometry(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Element,Autodesk.Revit.DB.Element)" />
        public void UnjoinGeometry(Element secondElement)
        {
            JoinGeometryUtils.UnjoinGeometry(element.Document, element, secondElement);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.JoinGeometryUtils.AreElementsJoined(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Element,Autodesk.Revit.DB.Element)" />
        [Pure]
        public bool AreElementsJoined(Element secondElement)
        {
            return JoinGeometryUtils.AreElementsJoined(element.Document, element, secondElement);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.JoinGeometryUtils.GetJoinedElements(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Element)" />
        [Pure]
        public ICollection<ElementId> GetJoinedElements()
        {
            return JoinGeometryUtils.GetJoinedElements(element.Document, element);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.JoinGeometryUtils.SwitchJoinOrder(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Element,Autodesk.Revit.DB.Element)" />
        public void SwitchJoinOrder(Element secondElement)
        {
            JoinGeometryUtils.SwitchJoinOrder(element.Document, element, secondElement);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.JoinGeometryUtils.IsCuttingElementInJoin(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Element,Autodesk.Revit.DB.Element)" />
        /// <returns>
        ///     True if this element is cutting the secondElement, false if the secondElement is cutting this element
        /// </returns>
        [Pure]
        public bool IsCuttingElementInJoin(Element secondElement)
        {
            return JoinGeometryUtils.IsCuttingElementInJoin(element.Document, element, secondElement);
        }
    }
}
