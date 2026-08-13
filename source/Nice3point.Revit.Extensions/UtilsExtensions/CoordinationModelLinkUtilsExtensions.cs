#if REVIT2026_OR_GREATER
using Autodesk.Revit.DB.ExternalData;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.ExternalData;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils"/> class.
/// </summary>
[PublicAPI]
public static class CoordinationModelLinkUtilsExtensions
{
    /// <param name="document">The source document.</param>
    extension(Document document)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.GetAllCoordinationModelInstanceIds(Autodesk.Revit.DB.Document)"/>
        [Pure]
        public ISet<ElementId> GetAllCoordinationModelInstanceIds()
        {
            return CoordinationModelLinkUtils.GetAllCoordinationModelInstanceIds(document);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.GetAllCoordinationModelTypeIds(Autodesk.Revit.DB.Document)"/>
        [Pure]
        public ISet<ElementId> GetAllCoordinationModelTypeIds()
        {
            return CoordinationModelLinkUtils.GetAllCoordinationModelTypeIds(document);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.LinkCoordinationModelFromLocalPath(Autodesk.Revit.DB.Document,System.String,Autodesk.Revit.DB.ExternalData.CoordinationModelLinkOptions)"/>
        public Element LinkCoordinationModelFromLocalPath(string filePath, CoordinationModelLinkOptions linkOptions)
        {
            return CoordinationModelLinkUtils.LinkCoordinationModelFromLocalPath(document, filePath, linkOptions);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.Link3DViewFromAutodeskDocs(Autodesk.Revit.DB.Document,System.String,System.String,System.String,System.String,Autodesk.Revit.DB.ExternalData.CoordinationModelLinkOptions)"/>
        public Element Link3DViewFromAutodeskDocs(string accountId, string projectId, string fileId, string viewName, CoordinationModelLinkOptions linkOptions)
        {
            return CoordinationModelLinkUtils.Link3DViewFromAutodeskDocs(document, accountId, projectId, fileId, viewName, linkOptions);
        }
    }

    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.IsCoordinationModelInstance(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Element)"/>
        public bool IsCoordinationModelInstance => CoordinationModelLinkUtils.IsCoordinationModelInstance(element.Document, element);

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.GetAllPropertiesForReferenceInsideCoordinationModel(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Element,Autodesk.Revit.DB.Reference)"/>
        [Pure]
        public IList<CoordinationModelElementProperty> GetAllPropertiesForReferenceInsideCoordinationModel(Reference reference)
        {
            return CoordinationModelLinkUtils.GetAllPropertiesForReferenceInsideCoordinationModel(element.Document, element, reference);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.GetCategoryForReferenceInsideCoordinationModel(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Element,Autodesk.Revit.DB.Reference)"/>
        [Pure]
        public string GetCategoryForReferenceInsideCoordinationModel(Reference reference)
        {
            return CoordinationModelLinkUtils.GetCategoryForReferenceInsideCoordinationModel(element.Document, element, reference);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.GetVisibilityOverride(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,Autodesk.Revit.DB.Element)"/>
        [Pure]
        public bool GetCoordinationModelVisibilityOverride(View view)
        {
            return CoordinationModelLinkUtils.GetVisibilityOverride(element.Document, view, element);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.SetVisibilityOverride(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,Autodesk.Revit.DB.Element,System.Boolean)"/>
        public void SetCoordinationModelVisibilityOverride(View view, bool visible)
        {
            CoordinationModelLinkUtils.SetVisibilityOverride(element.Document, view, element, visible);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.GetVisibilityOverrideForReferenceInsideCoordinationModel(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,Autodesk.Revit.DB.Element,Autodesk.Revit.DB.Reference)"/>
        [Pure]
        public bool GetVisibilityOverrideForReferenceInsideCoordinationModel(View view, Reference reference)
        {
            return CoordinationModelLinkUtils.GetVisibilityOverrideForReferenceInsideCoordinationModel(element.Document, view, element, reference);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.SetVisibilityOverrideForReferenceInsideCoordinationModel(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,Autodesk.Revit.DB.Element,Autodesk.Revit.DB.Reference,System.Boolean)"/>
        public void SetVisibilityOverrideForReferenceInsideCoordinationModel(View view, Reference reference, bool visible)
        {
            CoordinationModelLinkUtils.SetVisibilityOverrideForReferenceInsideCoordinationModel(element.Document, view, element, reference, visible);
        }
    }

    /// <param name="elementType">The source coordination model type.</param>
    extension(ElementType elementType)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.IsCoordinationModelType(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Element)"/>
        public bool IsCoordinationModelType => CoordinationModelLinkUtils.IsCoordinationModelType(elementType.Document, elementType);

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.GetCoordinationModelTypeData(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementType)"/>
        [Pure]
        public CoordinationModelLinkData GetCoordinationModelTypeData()
        {
            return CoordinationModelLinkUtils.GetCoordinationModelTypeData(elementType.Document, elementType);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.GetColorOverride(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,Autodesk.Revit.DB.Element)"/>
        [Pure]
        public Color GetCoordinationModelColorOverride(View view)
        {
            return CoordinationModelLinkUtils.GetColorOverride(elementType.Document, view, elementType);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.SetColorOverride(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,Autodesk.Revit.DB.Element,Autodesk.Revit.DB.Color)"/>
        public void SetCoordinationModelColorOverride(View view, Color color)
        {
            CoordinationModelLinkUtils.SetColorOverride(elementType.Document, view, elementType, color);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.GetTransparencyOverride(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,Autodesk.Revit.DB.Element)"/>
        [Pure]
        public int GetCoordinationModelTransparencyOverride(View view)
        {
            return CoordinationModelLinkUtils.GetTransparencyOverride(elementType.Document, view, elementType);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.SetTransparencyOverride(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,Autodesk.Revit.DB.Element,System.Int32)"/>
        public void SetCoordinationModelTransparencyOverride(View view, int transparency)
        {
            CoordinationModelLinkUtils.SetTransparencyOverride(elementType.Document, view, elementType, transparency);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.ContainsCategory(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementType,System.String)"/>
        [Pure]
        public bool ContainsCoordinationModelCategory(string categoryName)
        {
            return CoordinationModelLinkUtils.ContainsCategory(elementType.Document, elementType, categoryName);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.GetColorOverrideForCategory(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,Autodesk.Revit.DB.ElementType,System.String)"/>
        [Pure]
        public Color GetCoordinationModelColorOverrideForCategory(View view, string categoryName)
        {
            return CoordinationModelLinkUtils.GetColorOverrideForCategory(elementType.Document, view, elementType, categoryName);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.SetColorOverrideForCategory(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,Autodesk.Revit.DB.ElementType,System.String,Autodesk.Revit.DB.Color)"/>
        public void SetCoordinationModelColorOverrideForCategory(View view, string categoryName, Color color)
        {
            CoordinationModelLinkUtils.SetColorOverrideForCategory(elementType.Document, view, elementType, categoryName, color);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.GetVisibilityOverrideForCategory(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,Autodesk.Revit.DB.ElementType,System.String)"/>
        [Pure]
        public bool GetCoordinationModelVisibilityOverrideForCategory(View view, string categoryName)
        {
            return CoordinationModelLinkUtils.GetVisibilityOverrideForCategory(elementType.Document, view, elementType, categoryName);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.SetVisibilityOverrideForCategory(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,Autodesk.Revit.DB.ElementType,System.String,System.Boolean)"/>
        public void SetCoordinationModelVisibilityOverrideForCategory(View view, string categoryName, bool visible)
        {
            CoordinationModelLinkUtils.SetVisibilityOverrideForCategory(elementType.Document, view, elementType, categoryName, visible);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.Reload(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementType)"/>
        public void ReloadCoordinationModel()
        {
            CoordinationModelLinkUtils.Reload(elementType.Document, elementType);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.ReloadAutodeskDocsCoordinationModelFrom(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementType,System.String,System.String,System.String,System.String)"/>
        public void ReloadAutodeskDocsCoordinationModelFrom(string accountId, string projectId, string fileId, string viewName)
        {
            CoordinationModelLinkUtils.ReloadAutodeskDocsCoordinationModelFrom(elementType.Document, elementType, accountId, projectId, fileId, viewName);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.ReloadLocalCoordinationModelFrom(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementType,System.String)"/>
        public void ReloadLocalCoordinationModelFrom(string filePath)
        {
            CoordinationModelLinkUtils.ReloadLocalCoordinationModelFrom(elementType.Document, elementType, filePath);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils.Unload(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementType)"/>
        public void UnloadCoordinationModel()
        {
            CoordinationModelLinkUtils.Unload(elementType.Document, elementType);
        }
    }
}
#endif
