

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
        /// <inheritdoc cref="Autodesk.Revit.DB.InstanceVoidCutUtils.CanBeCutWithVoid(Autodesk.Revit.DB.Element)"/>
        public bool CanBeCutWithVoid => InstanceVoidCutUtils.CanBeCutWithVoid(element);

        /// <inheritdoc cref="Autodesk.Revit.DB.InstanceVoidCutUtils.GetCuttingVoidInstances(Autodesk.Revit.DB.Element)"/>
        [Pure]
        public ICollection<ElementId> GetCuttingVoidInstances()
        {
            return InstanceVoidCutUtils.GetCuttingVoidInstances(element);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.InstanceVoidCutUtils.AddInstanceVoidCut(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Element,Autodesk.Revit.DB.Element)"/>
        public void AddInstanceVoidCut(FamilyInstance cuttingInstance)
        {
            InstanceVoidCutUtils.AddInstanceVoidCut(element.Document, element, cuttingInstance);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.InstanceVoidCutUtils.RemoveInstanceVoidCut(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Element,Autodesk.Revit.DB.Element)"/>
        public void RemoveInstanceVoidCut(FamilyInstance cuttingInstance)
        {
            InstanceVoidCutUtils.RemoveInstanceVoidCut(element.Document, element, cuttingInstance);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.InstanceVoidCutUtils.InstanceVoidCutExists(Autodesk.Revit.DB.Element,Autodesk.Revit.DB.Element)"/>
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
        /// <inheritdoc cref="Autodesk.Revit.DB.InstanceVoidCutUtils.IsVoidInstanceCuttingElement(Autodesk.Revit.DB.Element)"/>
        public bool IsVoidInstanceCuttingElement => InstanceVoidCutUtils.IsVoidInstanceCuttingElement(familyInstance);

        /// <inheritdoc cref="Autodesk.Revit.DB.InstanceVoidCutUtils.GetElementsBeingCut(Autodesk.Revit.DB.Element)"/>
        [Pure]
        public ICollection<ElementId> GetElementsBeingCut()
        {
            return InstanceVoidCutUtils.GetElementsBeingCut(familyInstance);
        }
    }
}