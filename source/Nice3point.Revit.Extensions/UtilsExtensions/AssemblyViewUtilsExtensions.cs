// ReSharper disable once CheckNamespace

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.AssemblyViewUtils" /> class.
/// </summary>
[PublicAPI]
public static class AssemblyViewUtilsExtensions
{
    /// <param name="assemblyInstance">The source assembly instance.</param>
    extension(AssemblyInstance assemblyInstance)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.AssemblyViewUtils.AcquireAssemblyViews(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId)" />
        public void AcquireViews(AssemblyInstance target)
        {
            AssemblyViewUtils.AcquireAssemblyViews(assemblyInstance.Document, assemblyInstance.Id, target.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AssemblyViewUtils.Create3DOrthographic(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        public View3D Create3DOrthographic()
        {
            return AssemblyViewUtils.Create3DOrthographic(assemblyInstance.Document, assemblyInstance.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AssemblyViewUtils.Create3DOrthographic(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId,System.Boolean)" />
        public View3D Create3DOrthographic(ElementId viewTemplateId, bool isAssigned)
        {
            return AssemblyViewUtils.Create3DOrthographic(assemblyInstance.Document, assemblyInstance.Id, viewTemplateId, isAssigned);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AssemblyViewUtils.CreateDetailSection(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.AssemblyDetailViewOrientation)" />
        public ViewSection CreateDetailSection(AssemblyDetailViewOrientation direction)
        {
            return AssemblyViewUtils.CreateDetailSection(assemblyInstance.Document, assemblyInstance.Id, direction);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AssemblyViewUtils.CreateDetailSection(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.AssemblyDetailViewOrientation,Autodesk.Revit.DB.ElementId,System.Boolean)" />
        public ViewSection CreateDetailSection(AssemblyDetailViewOrientation direction, ElementId viewTemplateId, bool isAssigned)
        {
            return AssemblyViewUtils.CreateDetailSection(assemblyInstance.Document, assemblyInstance.Id, direction, viewTemplateId, isAssigned);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AssemblyViewUtils.CreateMaterialTakeoff(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        public ViewSchedule CreateMaterialTakeoff()
        {
            return AssemblyViewUtils.CreateMaterialTakeoff(assemblyInstance.Document, assemblyInstance.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AssemblyViewUtils.CreateMaterialTakeoff(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId,System.Boolean)" />
        public ViewSchedule CreateMaterialTakeoff(ElementId viewTemplateId, bool isAssigned)
        {
            return AssemblyViewUtils.CreateMaterialTakeoff(assemblyInstance.Document, assemblyInstance.Id, viewTemplateId, isAssigned);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AssemblyViewUtils.CreatePartList(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        public ViewSchedule CreatePartList()
        {
            return AssemblyViewUtils.CreatePartList(assemblyInstance.Document, assemblyInstance.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AssemblyViewUtils.CreatePartList(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId,System.Boolean)" />
        public ViewSchedule CreatePartList(ElementId viewTemplateId, bool isAssigned)
        {
            return AssemblyViewUtils.CreatePartList(assemblyInstance.Document, assemblyInstance.Id, viewTemplateId, isAssigned);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AssemblyViewUtils.CreateSheet(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId)" />
        public ViewSheet CreateSheet(ElementId titleBlockId)
        {
            return AssemblyViewUtils.CreateSheet(assemblyInstance.Document, assemblyInstance.Id, titleBlockId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AssemblyViewUtils.CreateSingleCategorySchedule(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId)" />
        public ViewSchedule CreateSingleCategorySchedule(ElementId scheduleCategoryId)
        {
            return AssemblyViewUtils.CreateSingleCategorySchedule(assemblyInstance.Document, assemblyInstance.Id, scheduleCategoryId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.AssemblyViewUtils.CreateSingleCategorySchedule(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId,System.Boolean)" />
        public ViewSchedule CreateSingleCategorySchedule(ElementId scheduleCategoryId, ElementId viewTemplateId, bool isAssigned)
        {
            return AssemblyViewUtils.CreateSingleCategorySchedule(assemblyInstance.Document, assemblyInstance.Id, scheduleCategoryId, viewTemplateId, isAssigned);
        }
    }
}
