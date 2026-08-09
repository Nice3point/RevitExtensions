

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.AdaptiveComponentInstanceUtils"/> class.
/// </summary>
[PublicAPI]
public static class AdaptiveComponentInstanceUtilsExtensions
{
    /// <param name="familyInstance">The source family instance.</param>
    extension(FamilyInstance familyInstance)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.AdaptiveComponentInstanceUtils.HasAdaptiveFamilySymbol(Autodesk.Revit.DB.FamilyInstance)"/>
        public bool HasAdaptiveFamilySymbol => AdaptiveComponentInstanceUtils.HasAdaptiveFamilySymbol(familyInstance);

        /// <inheritdoc cref="Autodesk.Revit.DB.AdaptiveComponentInstanceUtils.IsAdaptiveComponentInstance(Autodesk.Revit.DB.FamilyInstance)"/>
        public bool IsAdaptiveComponentInstance => AdaptiveComponentInstanceUtils.IsAdaptiveComponentInstance(familyInstance);

        /// <inheritdoc cref="Autodesk.Revit.DB.AdaptiveComponentInstanceUtils.IsInstanceFlipped(Autodesk.Revit.DB.FamilyInstance)"/>
        public bool IsAdaptiveInstanceFlipped => AdaptiveComponentInstanceUtils.IsInstanceFlipped(familyInstance);

        /// <inheritdoc cref="Autodesk.Revit.DB.AdaptiveComponentInstanceUtils.GetInstancePlacementPointElementRefIds(Autodesk.Revit.DB.FamilyInstance)"/>
        [Pure]
        public IList<ElementId> GetAdaptivePlacementPointElementRefIds()
        {
            return AdaptiveComponentInstanceUtils.GetInstancePlacementPointElementRefIds(familyInstance);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AdaptiveComponentInstanceUtils.GetInstancePointElementRefIds(Autodesk.Revit.DB.FamilyInstance)"/>
        [Pure]
        public IList<ElementId> GetAdaptivePointElementRefIds()
        {
            return AdaptiveComponentInstanceUtils.GetInstancePointElementRefIds(familyInstance);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AdaptiveComponentInstanceUtils.GetInstanceShapeHandlePointElementRefIds(Autodesk.Revit.DB.FamilyInstance)"/>
        [Pure]
        public IList<ElementId> GetAdaptiveShapeHandlePointElementRefIds()
        {
            return AdaptiveComponentInstanceUtils.GetInstanceShapeHandlePointElementRefIds(familyInstance);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AdaptiveComponentInstanceUtils.SetInstanceFlipped(Autodesk.Revit.DB.FamilyInstance,System.Boolean)"/>
        public void SetAdaptiveInstanceFlipped(bool flip)
        {
            AdaptiveComponentInstanceUtils.SetInstanceFlipped(familyInstance, flip);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AdaptiveComponentInstanceUtils.MoveAdaptiveComponentInstance(Autodesk.Revit.DB.FamilyInstance,Autodesk.Revit.DB.Transform,System.Boolean)"/>
        public void MoveAdaptiveComponentInstance(Transform transform, bool unHost)
        {
            AdaptiveComponentInstanceUtils.MoveAdaptiveComponentInstance(familyInstance, transform, unHost);
        }
    }

    /// <param name="familySymbol">The source family symbol.</param>
    extension(FamilySymbol familySymbol)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.AdaptiveComponentInstanceUtils.IsAdaptiveFamilySymbol(Autodesk.Revit.DB.FamilySymbol)"/>
        public bool IsAdaptiveFamilySymbol => AdaptiveComponentInstanceUtils.IsAdaptiveFamilySymbol(familySymbol);

        /// <inheritdoc cref="Autodesk.Revit.DB.AdaptiveComponentInstanceUtils.CreateAdaptiveComponentInstance(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.FamilySymbol)"/>
        public FamilyInstance CreateAdaptiveComponentInstance()
        {
            return AdaptiveComponentInstanceUtils.CreateAdaptiveComponentInstance(familySymbol.Document, familySymbol);
        }
    }
}