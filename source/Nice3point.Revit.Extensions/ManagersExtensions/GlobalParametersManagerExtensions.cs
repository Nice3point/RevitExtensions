// ReSharper disable once CheckNamespace

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.GlobalParametersManager"/> class.
/// </summary>
[PublicAPI]
public static class GlobalParametersManagerExtensions
{
    /// <param name="document">The Revit document.</param>
    extension(Document document)
    {
        /// <summary>Tests whether global parameters are allowed in the given document.</summary>
        /// <remarks>
        ///    First of all, global parameters can be had in main project documents only;
        ///    they are not supported in family documents. However, there may also be other
        ///    circumstances due to which global parameters may be disallowed in a particular
        ///    project, either temporarily or permanently.
        /// </remarks>
        public bool AreGlobalParametersAllowed => GlobalParametersManager.AreGlobalParametersAllowed(document);

        /// <summary>Returns all global parameters available in the given document.</summary>
        /// <returns>A collection of GlobalParameters</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    Global parameters are not supported in the given document.
        ///    A possible cause is that it is not a project document,
        ///    for global parameters are not supported in Revit families.
        /// </exception>
        [Pure]
        public ISet<ElementId> GetAllGlobalParameters()
        {
            return GlobalParametersManager.GetAllGlobalParameters(document);
        }

        /// <summary>
        ///    Finds whether a global parameter with the given name exists in the input document.
        /// </summary>
        /// <param name="name">Name of the global parameter</param>
        /// <returns>
        ///    GlobalParameter, or null if it was not found.
        /// </returns>
        [Pure]
        public GlobalParameter? FindGlobalParameter(string name)
        {
            var parameterId = GlobalParametersManager.FindByName(document, name);
            if (parameterId == ElementId.InvalidElementId) return null;

            return parameterId.ToElement<GlobalParameter>(document);
        }

        /// <summary>Tests whether an ElementId is of a global parameter in the given document.</summary>
        /// <param name="parameterId">Id of a global parameter</param>
        /// <returns>Returns True if the Id is of a valid global parameter; False otherwise.</returns>
        [Pure]
        public bool IsValidGlobalParameter(ElementId parameterId)
        {
            return GlobalParametersManager.IsValidGlobalParameter(document, parameterId);
        }

        /// <summary>
        ///    Tests whether a name is unique among existing global parameters of a given document.
        /// </summary>
        /// <remarks>
        ///    Typically, this method is used before a new global parameters is created, for
        ///    all global parameters must have their names unique in the scope of a document.
        /// </remarks>
        /// <param name="name">A name of a parameter being added.</param>
        /// <returns>
        ///    True if the given %name% does not exist yet among existing global parameters nof the document; False otherwise.
        /// </returns>
        [Pure]
        public bool IsUniqueGlobalParameterName(string name)
        {
            return GlobalParametersManager.IsUniqueName(document, name);
        }

        /// <summary>Returns all global parameters in an ordered array.</summary>
        /// <remarks>
        ///      <p>The order of the items corresponds to the order at which global parameters
        /// appear in Revit UI when shown in the standard Global Parameters dialog.
        /// However, the order of parameters is serialized in the document,
        /// thus available on the DB level as well.</p>
        ///    </remarks>
        /// <returns>A list of Global Parameters in the document.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    Global parameters are not supported in the given document.
        ///    A possible cause is that it is not a project document,
        ///    for global parameters are not supported in Revit families.
        /// </exception>
        [Pure]
        public IList<ElementId> GetGlobalParametersOrdered()
        {
            return GlobalParametersManager.GetGlobalParametersOrdered(document);
        }

        /// <summary>Sorts global parameters in the desired order.</summary>
        /// <remarks>
        ///      <p>All global parameters are sorted, but only within the range
        /// of their respective parameter group.</p>
        ///      <p>This operation has no effect on the global parameters themselves.
        /// The sorted order is only visible in the standard Global Parameters
        /// dialog. However, the order of parameters is serialized in the document,
        /// thus available on the DB level as well.</p>
        ///    </remarks>
        /// <param name="order">Desired sorting order</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    Global parameters are not supported in the given document.
        ///    A possible cause is that it is not a project document,
        ///    for global parameters are not supported in Revit families.
        /// </exception>
        public void SortGlobalParameters(ParametersOrder order)
        {
            GlobalParametersManager.SortParameters(document, order);
        }
    }

    /// <param name="parameter">The source parameter.</param>
    extension(GlobalParameter parameter)
    {
        /// <summary>Moves given global parameter Up in the current order.</summary>
        /// <remarks>
        ///      <p>A parameter can only be moved within its parameter group, meaning that
        /// repeated moving a parameter will not push the parameter out of and into
        /// the next (in order) parameter group. When a parameter can no longer move
        /// because it is at the boundary of its group, this method returns False.</p>
        ///      <p>This operation has no effect on the global parameters themselves.
        /// The rearranged order is only visible in the standard Global Parameters
        /// dialog. However, the order of parameters is serialized in the document,
        /// thus available on the DB level as well.</p>
        ///    </remarks>
        /// <returns>Indicates whether the parameter could be moved Up in order or not.</returns>
        public bool MoveUpOrder()
        {
            return GlobalParametersManager.MoveParameterUpOrder(parameter.Document, parameter.Id);
        }

        /// <summary>Moves given global parameter Down in the current order.</summary>
        /// <remarks>
        ///      <p>A parameter can only be moved within its parameter group, meaning that
        /// repeated moving a parameter will not push the parameter out of and into
        /// the next (in order) parameter group. When a parameter can no longer move
        /// because it is at the boundary of its group, this method returns False.</p>
        ///      <p>This operation has no effect on the global parameters themselves.
        /// The rearranged order is only visible in the standard Global Parameters
        /// dialog. However, the order of parameters is serialized in the document,
        /// thus available on the DB level as well.</p>
        ///    </remarks>
        /// <returns>Indicates whether the parameter could be moved Down in order or not.</returns>
        public bool MoveDownOrder()
        {
            return GlobalParametersManager.MoveParameterDownOrder(parameter.Document, parameter.Id);
        }
    }

    /// <param name="elementId">The global parameter element id.</param>
    extension(ElementId elementId)
    {
        /// <summary>Moves given global parameter Up in the current order.</summary>
        /// <remarks>
        ///      <p>A parameter can only be moved within its parameter group, meaning that
        /// repeated moving a parameter will not push the parameter out of and into
        /// the next (in order) parameter group. When a parameter can no longer move
        /// because it is at the boundary of its group, this method returns False.</p>
        ///      <p>This operation has no effect on the global parameters themselves.
        /// The rearranged order is only visible in the standard Global Parameters
        /// dialog. However, the order of parameters is serialized in the document,
        /// thus available on the DB level as well.</p>
        ///    </remarks>
        /// <param name="document">The document containing the parameter.</param>
        /// <returns>Indicates whether the parameter could be moved Up in order or not.</returns>
        public bool MoveGlobalParameterUpOrder(Document document)
        {
            return GlobalParametersManager.MoveParameterUpOrder(document, elementId);
        }

        /// <summary>Moves given global parameter Down in the current order.</summary>
        /// <remarks>
        ///      <p>A parameter can only be moved within its parameter group, meaning that
        /// repeated moving a parameter will not push the parameter out of and into
        /// the next (in order) parameter group. When a parameter can no longer move
        /// because it is at the boundary of its group, this method returns False.</p>
        ///      <p>This operation has no effect on the global parameters themselves.
        /// The rearranged order is only visible in the standard Global Parameters
        /// dialog. However, the order of parameters is serialized in the document,
        /// thus available on the DB level as well.</p>
        ///    </remarks>
        /// <param name="document">The document containing the parameter.</param>
        /// <returns>Indicates whether the parameter could be moved Down in order or not.</returns>
        public bool MoveGlobalParameterDownOrder(Document document)
        {
            return GlobalParametersManager.MoveParameterDownOrder(document, elementId);
        }
    }
}