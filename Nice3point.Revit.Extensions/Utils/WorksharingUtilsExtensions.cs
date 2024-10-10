// ReSharper disable once CheckNamespace

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.WorksharingUtils"/> class.
/// </summary>
public static class WorksharingUtilsExtensions
{
    /// <summary>Gets the ownership status of an element.</summary>
    /// <remarks>
    ///      <p> This method returns a locally cached value which may not be up to date with the current state
    /// of the element in the central.  Because of this, the return value is suitable for reporting to an
    /// interactive user (e.g. via a mechanism similar to Worksharing display mode), but cannot be considered
    /// a reliable indication of whether the element can be immediately edited by the application.  Also, the return value
    /// may not be dependable in the middle of a local transaction.  See the remarks
    /// on <see cref="T:Autodesk.Revit.DB.WorksharingUtils" /> for more details. </p>
    ///      <p> For performance reasons, the model is not validated to be workshared,
    /// and the element id is also not validated; the element will not be expanded. </p>
    ///    </remarks>
    /// <param name="element">The element itself.</param>
    /// <returns>
    ///    A summary of whether the element is unowned, owned by the current user, or owned by another user.
    /// </returns>
    [Pure]
    public static CheckoutStatus GetCheckoutStatus(this Element element)
    {
        return WorksharingUtils.GetCheckoutStatus(element.Document, element.Id);
    }

    /// <summary>Gets the ownership status and outputs the owner of an element.</summary>
    /// <remarks>
    ///      <p> This method returns a locally cached value which may not be up to date with the current state
    /// of the element in the central.  Because of this, the return value is suitable for reporting to an
    /// interactive user (e.g. via a mechanism similar to Worksharing display mode), but cannot be considered
    /// a reliable indication of whether the element can be immediately edited by the application.  Also, the return value
    /// may not be dependable in the middle of a local transaction.  See the remarks
    /// on <see cref="T:Autodesk.Revit.DB.WorksharingUtils" /> for more details. </p>
    ///      <p> For performance reasons, the model is not validated to be workshared,
    /// and the element id is also not validated; the element will not be expanded. </p>
    ///    </remarks>
    /// <param name="element">The element itself.</param>
    /// <param name="owner">The owner of the element, or an empty string if no one owns it.</param>
    /// <returns>
    ///    An indication of whether the element is unowned, owned by the current user, or owned by another user.
    /// </returns>
    [Pure]
    public static CheckoutStatus GetCheckoutStatus(this Element element, out string owner)
    {
        return WorksharingUtils.GetCheckoutStatus(element.Document, element.Id, out owner);
    }

    /// <summary>
    ///    Gets worksharing information about an element to display in an in-canvas tooltip.
    /// </summary>
    /// <remarks>
    ///      <p> If there is no element corresponding to the given id,
    /// then all the strings returned in WorksharingTooltipInfo are empty. </p>
    ///      <p> The return value may not be dependable in the middle of a transaction.
    /// See the remarks on <see cref="T:Autodesk.Revit.DB.WorksharingUtils" /> for more details. </p>
    ///    </remarks>
    /// <param name="element">The element itself.</param>
    /// <returns>Worksharing information about the specified element.</returns>
    [Pure]
    public static WorksharingTooltipInfo GetWorksharingTooltipInfo(this Element element)
    {
        return WorksharingUtils.GetWorksharingTooltipInfo(element.Document, element.Id);
    }

    /// <summary>Gets the status of a single element in the central model.</summary>
    /// <remarks>
    ///      <p> This method returns a locally cached value which may not be up to date with the current state
    /// of the element in the central.  Because of this, the return value is suitable for reporting to an
    /// interactive user (e.g. via a mechanism similar to Worksharing display mode), but cannot be considered
    /// a reliable indication of whether the element can be immediately edited by the application.  Also, the return value
    /// may not be dependable in the middle of a local transaction.  See the remarks
    /// on <see cref="T:Autodesk.Revit.DB.WorksharingUtils" /> for more details. </p>
    ///      <p> For performance reasons, the model is not validated to be workshared,
    /// and the element id is also not validated; the element will not be expanded.</p>
    ///    </remarks>
    /// <param name="element">The element itself.</param>
    /// <returns>The status of the element in the local session versus the central model.</returns>
    [Pure]
    public static ModelUpdatesStatus GetModelUpdatesStatus(this Element element)
    {
        return WorksharingUtils.GetModelUpdatesStatus(element.Document, element.Id);
    }

    /// <summary>
    ///    Obtains ownership for the current user of as many specified worksets as possible.
    /// </summary>
    /// <remarks>
    ///      <p> For best performance, check out all worksets in one big call, rather than many small calls. </p>
    ///      <p> When there comes a contention error when locking the central model, this API would wait and retry
    /// endlessly until getting the lock of the central model. </p>
    ///    </remarks>
    /// <param name="document">The document containing the worksets.</param>
    /// <param name="worksetsToCheckout">The ids of the worksets to attempt to check out.</param>
    /// <returns>
    ///    The ids of all specified worksets that are now owned,
    ///    including all that were owned prior to the function call.
    /// </returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    document is not a workshared document.
    ///    -or-
    ///    document is not a primary document, it is a linked document.
    ///    -or-
    ///    document is read-only: It cannot be modified.
    ///    -or-
    ///    document has an open editing transaction and is accepting changes.
    ///    -or-
    ///    There are one or more ids with no corresponding workset.
    ///    -or-
    ///    Saving is not allowed in the current application mode.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralFileCommunicationException">
    ///    The file-based central model could not be reached,
    ///    e.g. the network is down or the file server is down.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelAccessDeniedException">
    ///    Access to the central model was denied due to lack of access privileges.
    ///    -or-
    ///    Access to the central model was denied. A possible reason is because the model was under maintenance.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelContentionException">
    ///    The central model are locked by another client.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelException">
    ///    The central model is overwritten by other user.
    ///    -or-
    ///    The central model is missing.
    ///    -or-
    ///    An internal error happened on the central model, please contact the server administrator.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelVersionArchivedException">
    ///    Last central version merged into the local model has been archived in the central model.
    ///    This exception could only be thrown from cloud models.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    Operation is not permitted when there is any open sub-transaction, transaction, or transaction group.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.RevitServerCommunicationException">
    ///    The server-based central model could not be accessed
    ///    because of a network communication error.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.RevitServerInternalException">
    ///    An internal error happened on the server, please contact the server administrator.
    /// </exception>
    public static ICollection<WorksetId> CheckoutWorksets(this ICollection<WorksetId> worksetsToCheckout, Document document)
    {
        return WorksharingUtils.CheckoutWorksets(document, worksetsToCheckout);
    }

    /// <summary>
    ///    Obtains ownership for the current user of as many specified worksets as possible.
    /// </summary>
    /// <remarks>
    ///    For best performance, check out all worksets in one big call, rather than many small calls.
    /// </remarks>
    /// <param name="document">The document containing the worksets.</param>
    /// <param name="worksetsToCheckout">The ids of the worksets to attempt to check out.</param>
    /// <param name="options">
    ///    Options to customize access to the central model.
    ///    <see langword="null" /> is allowed and means no customization.
    /// </param>
    /// <returns>
    ///    The ids of all specified worksets that are now owned,
    ///    including all that were owned prior to the function call.
    /// </returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    document is not a workshared document.
    ///    -or-
    ///    document is not a primary document, it is a linked document.
    ///    -or-
    ///    document is read-only: It cannot be modified.
    ///    -or-
    ///    document has an open editing transaction and is accepting changes.
    ///    -or-
    ///    There are one or more ids with no corresponding workset.
    ///    -or-
    ///    Saving is not allowed in the current application mode.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralFileCommunicationException">
    ///    The file-based central model could not be reached,
    ///    e.g. the network is down or the file server is down.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelAccessDeniedException">
    ///    Access to the central model was denied due to lack of access privileges.
    ///    -or-
    ///    Access to the central model was denied. A possible reason is because the model was under maintenance.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelContentionException">
    ///    The central model are locked by another client.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelException">
    ///    The central model is overwritten by other user.
    ///    -or-
    ///    The central model is missing.
    ///    -or-
    ///    An internal error happened on the central model, please contact the server administrator.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelVersionArchivedException">
    ///    Last central version merged into the local model has been archived in the central model.
    ///    This exception could only be thrown from cloud models.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    Operation is not permitted when there is any open sub-transaction, transaction, or transaction group.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.RevitServerCommunicationException">
    ///    The server-based central model could not be accessed
    ///    because of a network communication error.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.RevitServerInternalException">
    ///    An internal error happened on the server, please contact the server administrator.
    /// </exception>
    public static ICollection<WorksetId> CheckoutWorksets(this ISet<WorksetId> worksetsToCheckout, Document document, TransactWithCentralOptions? options)
    {
        return WorksharingUtils.CheckoutWorksets(document, worksetsToCheckout, options);
    }

    /// <summary>
    ///    Obtains ownership for the current user of as many specified elements as possible.
    /// </summary>
    /// <remarks>
    ///      <p> For best performance, checkout all elements in one big call, rather than many small calls. </p>
    ///      <p> Revit may check out additional elements that are needed to check out the elements you requested.
    /// For example, if you request an element that is in a group, Revit will check out the entire group. </p>
    ///      <p> When there comes a contention error when locking the central model, this API would wait and retry
    /// endlessly until getting the lock of the central model. </p>
    ///    </remarks>
    /// <param name="document">The document containing the elements.</param>
    /// <param name="elementsToCheckout">The ids of the elements to attempt to check out.</param>
    /// <returns>
    ///    The ids of all specified elements that are now owned (but possibly out of date),
    ///    including all that were owned prior to the function call.
    /// </returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    document is not a workshared document.
    ///    -or-
    ///    document is not a primary document, it is a linked document.
    ///    -or-
    ///    One or more elements in elementsToCheckout do not exist in the document.
    ///    -or-
    ///    Saving is not allowed in the current application mode.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralFileCommunicationException">
    ///    Editing permissions for the file-based central model could not be accessed for write,
    ///    e.g. the network is down, central is missing, or central is read-only.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelAccessDeniedException">
    ///    Access to the central model was denied. A possible reason is because the model was under maintenance.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelContentionException">
    ///    Editing permissions for the central model are locked and the last attempt to lock was canceled.
    ///    -or-
    ///    The central model is being accessed by another client.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelException">
    ///    An error has occurred while checking out worksets or elements.
    ///    -or-
    ///    The central model is overwritten by other user.
    ///    -or-
    ///    The central model is missing.
    ///    -or-
    ///    An internal error happened on the central model, please contact the server administrator.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelVersionArchivedException">
    ///    Last central version merged into the local model has been archived in the central model.
    ///    This exception could only be thrown from cloud models.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.RevitServerCommunicationException">
    ///    The server-based central model could not be accessed
    ///    because of a network communication error.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.RevitServerInternalException">
    ///    An internal error happened on the server, please contact the server administrator.
    /// </exception>
    public static ICollection<ElementId> CheckoutElements(this ICollection<ElementId> elementsToCheckout, Document document)
    {
        return WorksharingUtils.CheckoutElements(document, elementsToCheckout);
    }

    /// <summary>
    ///    Obtains ownership for the current user of as many specified elements as possible.
    /// </summary>
    /// <remarks>
    ///      <p> For best performance, checkout all elements in one big call, rather than many small calls. </p>
    ///      <p> Revit may check out additional elements that are needed to check out the elements you requested.
    /// For example, if you request an element that is in a group, Revit will check out the entire group. </p>
    ///    </remarks>
    /// <param name="document">The document containing the elements.</param>
    /// <param name="elementsToCheckout">The ids of the elements to attempt to check out.</param>
    /// <param name="options">
    ///    Options to customize access to the central model.
    ///    <see langword="null" /> is allowed and means no customization.
    /// </param>
    /// <returns>
    ///    The ids of all specified elements that are now owned (but possibly out of date),
    ///    including all that were owned prior to the function call.
    /// </returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    document is not a workshared document.
    ///    -or-
    ///    document is not a primary document, it is a linked document.
    ///    -or-
    ///    One or more elements in elementsToCheckout do not exist in the document.
    ///    -or-
    ///    Saving is not allowed in the current application mode.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralFileCommunicationException">
    ///    Editing permissions for the file-based central model could not be accessed for write,
    ///    e.g. the network is down, central is missing, or central is read-only.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelAccessDeniedException">
    ///    Access to the central model was denied. A possible reason is because the model was under maintenance.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelContentionException">
    ///    Editing permissions for the central model are locked and the last attempt to lock was canceled.
    ///    -or-
    ///    The central model is being accessed by another client.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelException">
    ///    An error has occurred while checking out worksets or elements.
    ///    -or-
    ///    The central model is overwritten by other user.
    ///    -or-
    ///    The central model is missing.
    ///    -or-
    ///    An internal error happened on the central model, please contact the server administrator.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelVersionArchivedException">
    ///    Last central version merged into the local model has been archived in the central model.
    ///    This exception could only be thrown from cloud models.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.RevitServerCommunicationException">
    ///    The server-based central model could not be accessed
    ///    because of a network communication error.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.RevitServerInternalException">
    ///    An internal error happened on the server, please contact the server administrator.
    /// </exception>
    public static ICollection<ElementId> CheckoutElements(this ISet<ElementId> elementsToCheckout, Document document, TransactWithCentralOptions? options)
    {
        return WorksharingUtils.CheckoutElements(document, elementsToCheckout, options);
    }

    /// <summary>
    ///    Relinquishes ownership by the current user of as many specified elements and worksets as possible,
    ///    and grants element ownership requested by other users on a first-come, first-served basis.
    /// </summary>
    /// <remarks>
    ///      <p> Elements and worksets owned by other users are ignored. </p>
    ///      <p> Only unmodified elements already in central will be relinquished by this method.
    /// Newly added and modified elements cannot be relinquished
    /// until they have been synchronized with central. </p>
    ///      <p> For best performance, relinquish items in one big call, rather than many small calls. </p>
    ///    </remarks>
    /// <param name="document">The document containing the elements and worksets.</param>
    /// <param name="generalCategories">
    ///    General categories of items to relinquish.  See RelinquishOptions for details.
    /// </param>
    /// <param name="options">
    ///    Options to customize access to the central model.
    ///    <see langword="null" /> is allowed and means no customization.
    /// </param>
    /// <returns>The elements and worksets that were relinquished.</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    document is not a workshared document.
    ///    -or-
    ///    document is not a primary document, it is a linked document.
    ///    -or-
    ///    document is read-only: It cannot be modified.
    ///    -or-
    ///    document has an open editing transaction and is accepting changes.
    ///    -or-
    ///    Saving is not allowed in the current application mode.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralFileCommunicationException">
    ///    The file-based central model could not be reached,
    ///    e.g. the network is down or the file server is down.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelAccessDeniedException">
    ///    Access to the central model was denied due to lack of access privileges.
    ///    -or-
    ///    Access to the central model was denied. A possible reason is because the model was under maintenance.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelContentionException">
    ///    The central model is locked by another client.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelException">
    ///    The central model is overwritten by other user.
    ///    -or-
    ///    The central model is missing.
    ///    -or-
    ///    An internal error happened on the central model, please contact the server administrator.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    Operation is not permitted when there is any open sub-transaction, transaction, or transaction group.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.RevitServerCommunicationException">
    ///    The server-based central model could not be accessed
    ///    because of a network communication error.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.RevitServerInternalException">
    ///    An internal error happened on the server, please contact the server administrator.
    /// </exception>
    public static RelinquishedItems RelinquishOwnership(this Document document, RelinquishOptions generalCategories, TransactWithCentralOptions? options)
    {
        return WorksharingUtils.RelinquishOwnership(document, generalCategories, options);
    }

    /// <summary>
    ///    Takes a path to a central model and copies the model into a new local file for the current user.
    /// </summary>
    /// <param name="source">The path to the central model.</param>
    /// <param name="target">The path to put the new local file.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The given path sourcePath is a cloud path which is not supported in this method.
    ///    -or-
    ///    The model is not workshared.
    ///    -or-
    ///    The central model has not fully enabled worksharing.
    ///    It must be opened and resaved to finish enabling worksharing.
    ///    -or-
    ///    The model is a local file.
    ///    -or-
    ///    The central model is not saved in the current Revit version.
    ///    -or-
    ///    The model is transmitted.
    ///    -or-
    /// 
    ///    -or-
    ///    The specified filepath is invalid.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelAccessDeniedException">
    ///    Access to the central model was denied due to lack of access privileges.
    ///    -or-
    ///    Access to the central model was denied. A possible reason is because the model was under maintenance.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelContentionException">
    ///    The central model is locked by another user.
    ///    -or-
    ///    The central model is being accessed by another client.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelException">
    ///    The central model is missing.
    ///    -or-
    ///    An internal error happened on the central model, please contact the server administrator.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.FileArgumentAlreadyExistsException">
    ///    The file or folder already exists and cannot be overwritten.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    This functionality is not available in Revit LT.
    ///    -or-
    ///    File already exists!
    ///    -or-
    ///    Revit Server does not support local models.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.RevitServerCommunicationException">
    ///    The server-based central model could not be accessed
    ///    because of a network communication error.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.RevitServerInternalException">
    ///    An internal error happened on the server, please contact the server administrator.
    /// </exception>
    public static ModelPath CreateNewLocal(this ModelPath source, ModelPath target)
    {
        WorksharingUtils.CreateNewLocal(source, target);
        return target;
    }

        /// <summary>
    ///    Gets information about user worksets in a workshared model file, without fully opening the file.
    /// </summary>
    /// <remarks>
    ///    This method provides a preview of the user worksets available in a file, allowing an
    ///    application to look up the necessary workset ids and information to properly fill out a WorksetConfiguration
    ///    structure before opening or linking to this model.
    /// </remarks>
    /// <param name="path">The path to the workshared model.</param>
    /// <returns>
    ///    Information about all the user worksets in the model.
    ///    The list is sorted by workset id.
    /// </returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelAccessDeniedException">
    ///    Access to the central model was denied due to lack of access privileges.
    ///    -or-
    ///    Access to the central model was denied. A possible reason is because the model was under maintenance.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelContentionException">
    ///    The central model are locked by another client.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelException">
    ///    The central model is missing.
    ///    -or-
    ///    The central model is corrupt or not an RVT file.
    ///    -or-
    ///    The model is not workshared.
    ///    -or-
    ///    The central model is overwritten by other user.
    ///    -or-
    ///    An internal error happened on the central model, please contact the server administrator.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.FileAccessException">
    ///    The model could not be accessed due to lack of access privileges.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.FileArgumentNotFoundException">
    ///    The Revit model specified by path doesn't exist.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.FileNotFoundException">
    ///    The model could not be found at the specified path.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    This functionality is not available in Revit LT.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.RevitServerCommunicationException">
    ///    The server-based central model could not be accessed
    ///    because of a network communication error.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.RevitServerInternalException">
    ///    An internal error happened on the server, please contact the server administrator.
    /// </exception>
    [Pure]
    public static IList<WorksetPreview> GetUserWorksetInfo(this ModelPath path)
    {
        return WorksharingUtils.GetUserWorksetInfo(path);
    }
}