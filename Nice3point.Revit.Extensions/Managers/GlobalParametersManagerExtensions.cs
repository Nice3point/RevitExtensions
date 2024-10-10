// ReSharper disable once CheckNamespace

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.GlobalParametersManager"/> class.
/// </summary>
public static class GlobalParametersManagerExtensions
{
    /// <summary>Tests whether global parameters are allowed in the given document.</summary>
    /// <remarks>
    ///    First of all, global parameters can be had in main project documents only;
    ///    they are not supported in family documents. However, there may also be other
    ///    circumstances due to which global parameters may be disallowed in a particular
    ///    project, either temporarily or permanently.
    /// </remarks>
    /// <param name="document">A revit document of interest.</param>
    [Pure]
    public static bool AreGlobalParametersAllowed(this Document document)
    {
        return GlobalParametersManager.AreGlobalParametersAllowed(document);
    }

    /// <summary>Returns all global parameters available in the given document.</summary>
    /// <param name="document">The document containing the global parameters</param>
    /// <returns>A collection of Element Ids of global parameter elements.</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    Global parameters are not supported in the given document.
    ///    A possible cause is that it is not a project document,
    ///    for global parameters are not supported in Revit families.
    /// </exception>
    [Pure]
    public static ISet<ElementId> GetAllGlobalParameters(this Document document)
    {
        return GlobalParametersManager.GetAllGlobalParameters(document);
    }

    /// <summary>
    ///    Finds whether a global parameter with the given name exists in the input document.
    /// </summary>
    /// <remarks>
    ///    No exception is thrown when no parameter with such a name exists in the document;
    ///    instead, the method returns an ElementId.InvalidElementId.
    /// </remarks>
    /// <param name="document">The document expected to contain the global parameter.</param>
    /// <param name="name">Name of the global parameter</param>
    /// <returns>
    ///    ElementId of the parameter element, or InvalidElementId if it was not found.
    /// </returns>
    [Pure]
    public static GlobalParameter? FindGlobalParameter(this Document document, string name)
    {
        var parameterId = GlobalParametersManager.FindByName(document, name);
        if (parameterId == ElementId.InvalidElementId) return null;
        
        return parameterId.ToElement<GlobalParameter>(document);
    }

    /// <summary>Tests whether an ElementId is of a global parameter in the given document.</summary>
    /// <param name="document">The document containing the global parameter.</param>
    /// <param name="parameterId">Id of a global parameter</param>
    /// <returns>Returns True if the Id is of a valid global parameter; False otherwise.</returns>
    [Pure]
    public static bool IsValidGlobalParameter(this Document document, ElementId parameterId)
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
    /// <param name="document">Document in which a new parameter is to be added.</param>
    /// <param name="name">A name of a parameter being added.</param>
    /// <returns>
    ///    True if the given %name% does not exist yet among existing global parameters nof the document; False otherwise.
    /// </returns>
    [Pure]
    public static bool IsUniqueGlobalParameterName(this Document document, string name)
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
    /// <param name="document">Document containing the requested global parameters</param>
    /// <returns>An array of Element Ids of all Global Parameters in the document.</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    Global parameters are not supported in the given document.
    ///    A possible cause is that it is not a project document,
    ///    for global parameters are not supported in Revit families.
    /// </exception>
    [Pure]
    public static IList<ElementId> GetGlobalParametersOrdered(this Document document)
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
    /// <param name="document">Document containing the global parameters to be sorted</param>
    /// <param name="order">Desired sorting order</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    Global parameters are not supported in the given document.
    ///    A possible cause is that it is not a project document,
    ///    for global parameters are not supported in Revit families.
    /// </exception>
    public static void SortGlobalParameters(this Document document, ParametersOrder order)
    {
        GlobalParametersManager.SortParameters(document, order);
    }

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
    /// <param name="document">Document containing the give global parameter</param>
    /// <param name="parameterId">The parameter to move up</param>
    /// <returns>Indicates whether the parameter could be moved Up in order or not.</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    Global parameters are not supported in the given document.
    ///    A possible cause is that it is not a project document,
    ///    for global parameters are not supported in Revit families.
    ///    -or-
    ///    The input parameterId is not of a valid global parameter of the given document.
    /// </exception>
    public static bool MoveGlobalParameterUpOrder(this Document document, ElementId parameterId)
    {
        return GlobalParametersManager.MoveParameterUpOrder(document, parameterId);
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
    /// <param name="document">Document containing the give global parameter</param>
    /// <param name="parameterId">The parameter to move Down</param>
    /// <returns>Indicates whether the parameter could be moved Down in order or not.</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    Global parameters are not supported in the given document.
    ///    A possible cause is that it is not a project document,
    ///    for global parameters are not supported in Revit families.
    ///    -or-
    ///    The input parameterId is not of a valid global parameter of the given document.
    /// </exception>
    public static bool MoveGlobalParameterDownOrder(this Document document, ElementId parameterId)
    {
        return GlobalParametersManager.MoveParameterDownOrder(document, parameterId);
    }
}