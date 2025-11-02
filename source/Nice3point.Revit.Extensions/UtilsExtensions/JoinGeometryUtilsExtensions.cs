// ReSharper disable once CheckNamespace

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.JoinGeometryUtils"/> class.
/// </summary>
[PublicAPI]
public static class JoinGeometryUtilsExtensions
{
    /// <param name="source">The source element</param>
    extension(Element source)
    {
        /// <summary>
        ///     Creates clean joins between two elements that share a common face
        /// </summary>
        /// <remarks>
        ///    The visible edge between joined elements is removed. The joined elements then share the same line weight and fill pattern.
        ///    This functionality is not available for family documents.
        /// </remarks>
        /// <param name="secondElement">
        ///    The second element to be joined. This element must not be joined to the first element
        /// </param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The element secondElement was not found in the source document<br />
        ///    The elements are already joined<br />
        ///    The elements cannot be joined<br />
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Please remove or add segments on curtain grids instead of joining or unjoining geometry of the panels
        /// </exception>
        public void JoinGeometry(Element secondElement)
        {
            JoinGeometryUtils.JoinGeometry(source.Document, source, secondElement);
        }

        /// <summary>
        ///     Removes a join between two elements
        /// </summary>
        /// <remarks>This functionality is not available for family documents</remarks>
        /// <param name="secondElement">
        ///    The second element to be unjoined. This element must be joined to the fist element
        /// </param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The element secondElement was not found in the source document<br />
        ///    The elements are not joined<br />
        ///    The elements cannot be unjoined
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Please remove or add segments on curtain grids instead of joining or unjoining geometry of the panels
        /// </exception>
        public void UnjoinGeometry(Element secondElement)
        {
            JoinGeometryUtils.UnjoinGeometry(source.Document, source, secondElement);
        }

        /// <summary>
        ///     Determines whether two elements are joined
        /// </summary>
        /// <remarks>This functionality is not available for family documents</remarks>
        /// <param name="secondElement">The second element</param>
        /// <returns>True if the two elements are joined</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The element secondElement was not found in the source document
        /// </exception>
        [Pure]
        public bool AreElementsJoined(Element secondElement)
        {
            return JoinGeometryUtils.AreElementsJoined(source.Document, source, secondElement);
        }

        /// <summary>
        ///     Returns all elements joined to given element
        /// </summary>
        /// <remarks>This functionality is not available for family documents</remarks>
        /// <returns>The set of elements that are joined to the given element</returns>
        [Pure]
        public ICollection<ElementId> GetJoinedElements()
        {
            return JoinGeometryUtils.GetJoinedElements(source.Document, source);
        }

        /// <summary>
        ///     Reverses the order in which two elements are joined
        /// </summary>
        /// <remarks>
        ///    The cutting element becomes the cut element and vice versa after the join order is switched
        ///    This functionality is not available for family documents
        /// </remarks>
        /// <param name="secondElement">
        ///    The second element. This element must be joined to the first element
        /// </param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The element secondElement was not found in the source document<br />
        ///    The elements are not joined<br />
        ///    The elements cannot be joined
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Unable to switch the join order of these elements
        /// </exception>
        public void SwitchJoinOrder(Element secondElement)
        {
            JoinGeometryUtils.SwitchJoinOrder(source.Document, source, secondElement);
        }

        /// <summary>
        ///    Determines whether the first of two joined elements is cutting the second element
        /// </summary>
        /// <remarks>This functionality is not available for family documents</remarks>
        /// <param name="secondElement">The second element</param>
        /// <returns>
        ///    True if the secondElement is cut by the source, false if the secondElement is cut by the source
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The element secondElement was not found in the source document<br />
        ///    The elements are not joined
        /// </exception>
        [Pure]
        public bool IsCuttingElementInJoin(Element secondElement)
        {
            return JoinGeometryUtils.IsCuttingElementInJoin(source.Document, source, secondElement);
        }
    }
}