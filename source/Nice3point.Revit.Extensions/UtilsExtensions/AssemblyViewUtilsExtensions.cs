

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.AssemblyViewUtils"/> class.
/// </summary>
[PublicAPI]
public static class AssemblyViewUtilsExtensions
{
    /// <param name="assemblyInstance">The source assembly instance.</param>
    extension(AssemblyInstance assemblyInstance)
    {
        /// <summary>
        ///    Transfers the assembly views owned by a source assembly instance to a target sibling assembly instance of the same assembly type.
        /// </summary>
        /// <param name="target">
        ///    Assembly instance which will become the new owner of the assembly views.
        /// </param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    sourceAssemblyInstanceId and targetAssemblyInstanceId are not AssemblyInstances from the same assembly type.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public void AcquireViews(AssemblyInstance target)
        {
            AssemblyViewUtils.AcquireAssemblyViews(assemblyInstance.Document, assemblyInstance.Id, target.Id);
        }

        /// <summary>Creates a new orthographic 3D assembly view for the assembly instance.</summary>
        /// <remarks>
        ///    The view will have the same orientation as the Default 3D view.
        ///    The document must be regenerated before using the 3D view.
        /// </remarks>
        /// <returns>A new orthographic 3D assembly view.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public View3D Create3DOrthographic()
        {
            return AssemblyViewUtils.Create3DOrthographic(assemblyInstance.Document, assemblyInstance.Id);
        }

        /// <summary>
        ///    Creates a new orthographic 3D assembly view for the assembly instance.
        ///    The view will have the same orientation as the Default 3D view.
        ///    The document must be regenerated before using the 3D view.
        /// </summary>
        /// <param name="viewTemplateId">
        ///    Id of the view template that is used to create the view;
        ///    if invalidElementId, the view will be created with the default settings.
        /// </param>
        /// <param name="isAssigned">
        ///    If true, the template will be assigned, if false, the template will be applied.
        /// </param>
        /// <returns>A new orthographic 3D assembly view.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    viewTemplateId is not a correct view template for the geom view.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public View3D Create3DOrthographic(ElementId viewTemplateId, bool isAssigned)
        {
            return AssemblyViewUtils.Create3DOrthographic(assemblyInstance.Document, assemblyInstance.Id, viewTemplateId, isAssigned);
        }

        /// <summary>Creates a new detail section assembly view for the assembly instance.</summary>
        /// <remarks>
        ///    The detail section will cut through the center of the assembly instance's outline.
        ///    The document must be regenerated before using the detail section.
        /// </remarks>
        /// <param name="direction">The direction for the new view.</param>
        /// <returns>A new detail section assembly view.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    A value passed for an enumeration argument is not a member of that enumeration
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public ViewSection CreateDetailSection(AssemblyDetailViewOrientation direction)
        {
            return AssemblyViewUtils.CreateDetailSection(assemblyInstance.Document, assemblyInstance.Id, direction);
        }

        /// <summary>Creates a new detail section assembly view for the assembly instance.</summary>
        /// <remarks>
        ///    The detail section will cut through the center of the assembly instance's outline.
        ///    The document must be regenerated before using the detail section.
        /// </remarks>
        /// <param name="direction">The direction for the new view.</param>
        /// <param name="viewTemplateId">
        ///    Id of the view template that is used to create the view; if invalidElementId, the view will be created with the default settings.
        /// </param>
        /// <param name="isAssigned">
        ///    If true, the template will be assigned; if false, the template will be applied.
        /// </param>
        /// <returns>A new detail section assembly view.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    viewTemplateId is not a correct view template for the geom view.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    A value passed for an enumeration argument is not a member of that enumeration
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public ViewSection CreateDetailSection(AssemblyDetailViewOrientation direction, ElementId viewTemplateId, bool isAssigned)
        {
            return AssemblyViewUtils.CreateDetailSection(assemblyInstance.Document, assemblyInstance.Id, direction, viewTemplateId, isAssigned);
        }

        /// <summary>
        ///    Creates a new material takeoff multicategory schedule assembly view for the assembly instance.
        /// </summary>
        /// <remarks>
        ///    The material takeoff schedule will be preloaded with fields "Material: Name" and "Material: Volume".
        ///    The document must be regenerated before using the schedule.
        /// </remarks>
        /// <returns>A new material takeoff multicategory schedule assembly view.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public ViewSchedule CreateMaterialTakeoff()
        {
            return AssemblyViewUtils.CreateMaterialTakeoff(assemblyInstance.Document, assemblyInstance.Id);
        }

        /// <summary>
        ///    Creates a new material takeoff multicategory schedule assembly view for the assembly instance.
        /// </summary>
        /// <remarks>
        ///    The material takeoff schedule will be preloaded with fields "Material: Name" and "Material: Volume".
        ///    The document must be regenerated before using the schedule.
        /// </remarks>
        /// <param name="viewTemplateId">
        ///    Id of the view template that is used to create the view;
        ///    if invalidElementId, the view will be created with the default settings.
        /// </param>
        /// <param name="isAssigned">
        ///    If true, the template will be assigned, if false, the template will be applied.
        /// </param>
        /// <returns>A new material takeoff multicategory schedule assembly view.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    viewTemplateId is not a correct view template for the schedule view.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public ViewSchedule CreateMaterialTakeoff(ElementId viewTemplateId, bool isAssigned)
        {
            return AssemblyViewUtils.CreateMaterialTakeoff(assemblyInstance.Document, assemblyInstance.Id, viewTemplateId, isAssigned);
        }

        /// <summary>
        ///    Creates a new part list multicategory schedule assembly view for the assembly instance.
        /// </summary>
        /// <remarks>
        ///    The new part list schedule will be preloaded with fields "Category", "Family and Type" and "Count".
        ///    The document must be regenerated before using the schedule.
        /// </remarks>
        /// <returns>A new part list multicategory schedule assembly view.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public ViewSchedule CreatePartList()
        {
            return AssemblyViewUtils.CreatePartList(assemblyInstance.Document, assemblyInstance.Id);
        }

        /// <summary>
        ///    Creates a new part list multicategory schedule assembly view for the assembly instance.
        /// </summary>
        /// <remarks>
        ///    The new part list schedule will be preloaded with fields "Category", "Family and Type" and "Count".
        ///    The document must be regenerated before using the schedule.
        /// </remarks>
        /// <param name="viewTemplateId">
        ///    Id of the view template that is used to create the view;
        ///    if invalidElementId, the view will be created with the default settings.
        /// </param>
        /// <param name="isAssigned">
        ///    If true, the template will be assigned, if false, the template will be applied.
        /// </param>
        /// <returns>A new part list multicategory schedule assembly view.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    viewTemplateId is not a correct view template for the schedule view.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public ViewSchedule CreatePartList(ElementId viewTemplateId, bool isAssigned)
        {
            return AssemblyViewUtils.CreatePartList(assemblyInstance.Document, assemblyInstance.Id, viewTemplateId, isAssigned);
        }

        /// <summary>Creates a new sheet assembly view for the assembly instance.</summary>
        /// <remarks>The document must be regenerated before using the sheet.</remarks>
        /// <param name="titleBlockId">
        ///    Id of the titleblock family to use.  For no titleblock, pass invalidElementId.
        /// </param>
        /// <returns>A new sheet assembly view.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    titleBlockId is not a TitleBlock.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public ViewSheet CreateSheet(ElementId titleBlockId)
        {
            return AssemblyViewUtils.CreateSheet(assemblyInstance.Document, assemblyInstance.Id, titleBlockId);
        }

        /// <summary>
        ///    Creates a new single-category schedule assembly view for the assembly instance.
        /// </summary>
        /// <remarks>
        ///    The new single-category schedule will be preloaded with fields "Family and Type" and "Count".
        ///    The schedule will be empty if there are no elements of the specified category in the assembly instance.
        ///    The document must be regenerated before using the schedule.
        /// </remarks>
        /// <param name="scheduleCategoryId">
        ///    Id of the category for which the schedule will be created.
        ///    Use ViewSchedule.IsValidCategoryForSchedule() to check if a category can be scheduled.
        /// </param>
        /// <returns>A new single-category schedule assembly view.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    scheduleCategoryId is not a valid category for a regular schedule.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public ViewSchedule CreateSingleCategorySchedule(ElementId scheduleCategoryId)
        {
            return AssemblyViewUtils.CreateSingleCategorySchedule(assemblyInstance.Document, assemblyInstance.Id, scheduleCategoryId);
        }

        /// <summary>
        ///    Creates a new single-category schedule assembly view for the assembly instance.
        /// </summary>
        /// <remarks>
        ///    The new single-category schedule will be preloaded with fields "Family and Type" and "Count".
        ///    The schedule will be empty if there are no elements of the specified category in the assembly instance.
        ///    The document must be regenerated before using the schedule.
        /// </remarks>
        /// <param name="scheduleCategoryId">
        ///    Id of the category for which the schedule will be created.
        ///    Use ViewSchedule.IsValidCategoryForSchedule() to check if a category can be scheduled.
        /// </param>
        /// <param name="viewTemplateId">
        ///    Id of the view template that is used to create the view;
        ///    if invalidElementId, the view will be created with the default settings.
        /// </param>
        /// <param name="isAssigned">
        ///    If true, the template will be assigned, if false, the template will be applied.
        /// </param>
        /// <returns>A new single-category schedule assembly view.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    scheduleCategoryId is not a valid category for a regular schedule.
        ///    -or-
        ///    viewTemplateId is not a correct view template for the schedule view.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public ViewSchedule CreateSingleCategorySchedule(ElementId scheduleCategoryId, ElementId viewTemplateId, bool isAssigned)
        {
            return AssemblyViewUtils.CreateSingleCategorySchedule(assemblyInstance.Document, assemblyInstance.Id, scheduleCategoryId, viewTemplateId, isAssigned);
        }
    }
}