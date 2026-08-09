

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
        /// <inheritdoc cref="Autodesk.Revit.DB.GlobalParametersManager.AreGlobalParametersAllowed(Autodesk.Revit.DB.Document)"/>
        public bool AreGlobalParametersAllowed => GlobalParametersManager.AreGlobalParametersAllowed(document);

        /// <inheritdoc cref="Autodesk.Revit.DB.GlobalParametersManager.GetAllGlobalParameters(Autodesk.Revit.DB.Document)"/>
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

        /// <inheritdoc cref="Autodesk.Revit.DB.GlobalParametersManager.IsUniqueName(Autodesk.Revit.DB.Document,System.String)"/>
        [Pure]
        public bool IsUniqueGlobalParameterName(string name)
        {
            return GlobalParametersManager.IsUniqueName(document, name);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.GlobalParametersManager.GetGlobalParametersOrdered(Autodesk.Revit.DB.Document)"/>
        [Pure]
        public IList<ElementId> GetGlobalParametersOrdered()
        {
            return GlobalParametersManager.GetGlobalParametersOrdered(document);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.GlobalParametersManager.SortParameters(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ParametersOrder)"/>
        public void SortGlobalParameters(ParametersOrder order)
        {
            GlobalParametersManager.SortParameters(document, order);
        }
    }

    /// <param name="parameter">The source parameter.</param>
    extension(GlobalParameter parameter)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.GlobalParametersManager.MoveParameterUpOrder(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        public bool MoveUpOrder()
        {
            return GlobalParametersManager.MoveParameterUpOrder(parameter.Document, parameter.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.GlobalParametersManager.MoveParameterDownOrder(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        public bool MoveDownOrder()
        {
            return GlobalParametersManager.MoveParameterDownOrder(parameter.Document, parameter.Id);
        }
    }

    /// <param name="parameterId">The global parameter element id.</param>
    extension(ElementId parameterId)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.GlobalParametersManager.IsValidGlobalParameter(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public bool IsValidGlobalParameter(Document document)
        {
            return GlobalParametersManager.IsValidGlobalParameter(document, parameterId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.GlobalParametersManager.MoveParameterUpOrder(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        public bool MoveGlobalParameterUpOrder(Document document)
        {
            return GlobalParametersManager.MoveParameterUpOrder(document, parameterId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.GlobalParametersManager.MoveParameterDownOrder(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        public bool MoveGlobalParameterDownOrder(Document document)
        {
            return GlobalParametersManager.MoveParameterDownOrder(document, parameterId);
        }
    }
}