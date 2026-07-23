

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.InstanceVoidCutUtils"/> class.
/// </summary>
[PublicAPI]
public static class InstanceVoidCutUtilsExtensions
{
    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <summary>Indicates if the element can be cut by an instance with unattached voids.</summary>
        /// <remarks>
        ///    Elements in a project can be cut if they are host elements or family instances
        ///    with a category Generic Model or one of the structural categories.
        /// </remarks>
        /// <returns>
        ///    Returns true if the element can be cut by an instance with unattached voids.
        /// </returns>
        public bool CanBeCutWithVoid => InstanceVoidCutUtils.CanBeCutWithVoid(element);

        /// <summary>Return ids of the instances with unattached voids cutting the element.</summary>
        /// <returns>Ids of instances with unattached voids that cut this element</returns>
        [Pure]
        public ICollection<ElementId> GetCuttingVoidInstances()
        {
            return InstanceVoidCutUtils.GetCuttingVoidInstances(element);
        }

        /// <summary>
        ///    Add a cut to an element using the unattached voids inside a cutting instance.
        /// </summary>
        /// <param name="cuttingInstance">The cutting family instance</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The element cannot be cut with a void instance.
        ///    -or-
        ///    The element is not a family instance with an unattached void that can cut.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ForbiddenForDynamicUpdateException">
        ///    This method may not be called during dynamic update.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Failed to cut element with the instances
        /// </exception>
        public void AddInstanceVoidCut(FamilyInstance cuttingInstance)
        {
            InstanceVoidCutUtils.AddInstanceVoidCut(element.Document, element, cuttingInstance);
        }

        /// <summary>
        ///    Remove a cut applied to the element by a cutting instance with unattached voids.
        /// </summary>
        /// <param name="cuttingInstance">The cutting family instance</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    No instance void cut exists between the two elements.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Failed to remove the instance cut from the element
        /// </exception>
        public void RemoveInstanceVoidCut(FamilyInstance cuttingInstance)
        {
            InstanceVoidCutUtils.RemoveInstanceVoidCut(element.Document, element, cuttingInstance);
        }

        /// <summary>Check whether the instance is cutting the element</summary>
        /// <param name="cuttingInstance">The cutting family instance</param>
        /// <returns>Returns true if the instance is cutting the element.</returns>
        [Pure]
        public bool IsInstanceVoidCutExists(FamilyInstance cuttingInstance)
        {
            return InstanceVoidCutUtils.InstanceVoidCutExists(element, cuttingInstance);
        }
    }

    /// <param name="familyInstance">The source family instance.</param>
    extension(FamilyInstance familyInstance)
    {
        /// <summary>
        ///    Indicates if the family instance with unattached voids that can cut other elements.
        /// </summary>
        /// <remarks>
        ///    A family instance can cut if the family has unattached voids and the family's parameter
        ///    "Cut with Voids When Loaded" is checked.
        /// </remarks>
        /// <returns>
        ///    Returns true if the element is a family instance with unattached voids that can cut other elements.
        /// </returns>
        public bool IsVoidInstanceCuttingElement => InstanceVoidCutUtils.IsVoidInstanceCuttingElement(familyInstance);

        /// <summary>Return ids of the elements being cut by the instance</summary>
        /// <returns>Ids of elements being cut by cuttingInstance</returns>
        [Pure]
        public ICollection<ElementId> GetElementsBeingCut()
        {
            return InstanceVoidCutUtils.GetElementsBeingCut(familyInstance);
        }
    }
}