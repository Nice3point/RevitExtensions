using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB.Structure.StructuralSections;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.LabelUtils" /> class.
/// </summary>
[PublicAPI]
public static class LabelUtilsExtensions
{
#if REVIT2021_OR_GREATER
    /// <param name="typeId">Unique identifier</param>
    extension(ForgeTypeId typeId)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.LabelUtils.GetLabelForSpec(Autodesk.Revit.DB.ForgeTypeId)"/>
        [Pure]
        public string ToSpecLabel()
        {
            return LabelUtils.GetLabelForSpec(typeId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.LabelUtils.GetLabelForSymbol(Autodesk.Revit.DB.ForgeTypeId)"/>
        [Pure]
        public string ToSymbolLabel()
        {
            return LabelUtils.GetLabelForSymbol(typeId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.LabelUtils.GetLabelForUnit(Autodesk.Revit.DB.ForgeTypeId)"/>
        [Pure]
        public string ToUnitLabel()
        {
            return LabelUtils.GetLabelForUnit(typeId);
        }
#if REVIT2022_OR_GREATER

        /// <inheritdoc cref="Autodesk.Revit.DB.LabelUtils.GetLabelForDiscipline(Autodesk.Revit.DB.ForgeTypeId)"/>
        [Pure]
        public string ToDisciplineLabel()
        {
            return LabelUtils.GetLabelForDiscipline(typeId);
        }

        /// <summary>
        ///     Gets the user-visible name for a ForgeTypeId
        /// </summary>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///     The ForgeTypeId is not valid in the context of the current API version
        /// </exception>
        /// <remarks>The name is obtained in the current Revit language</remarks>
        [Pure]
        public string ToLabel()
        {
            if (typeId.Empty()) return string.Empty;

            if (ParameterUtils.IsBuiltInParameter(typeId)) return LabelUtils.GetLabelForBuiltInParameter(typeId);
            if (ParameterUtils.IsBuiltInGroup(typeId)) return LabelUtils.GetLabelForGroup(typeId);
            if (UnitUtils.IsUnit(typeId)) return LabelUtils.GetLabelForUnit(typeId);
            if (UnitUtils.IsSymbol(typeId)) return LabelUtils.GetLabelForSymbol(typeId);
            if (SpecUtils.IsSpec(typeId)) return LabelUtils.GetLabelForSpec(typeId);
            return LabelUtils.GetLabelForDiscipline(typeId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.LabelUtils.GetLabelForGroup(Autodesk.Revit.DB.ForgeTypeId)"/>
        [Pure]
        public string ToGroupLabel()
        {
            return LabelUtils.GetLabelForGroup(typeId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.LabelUtils.GetLabelForBuiltInParameter(Autodesk.Revit.DB.ForgeTypeId)"/>
        [Pure]
        public string ToParameterLabel()
        {
            return LabelUtils.GetLabelForBuiltInParameter(typeId);
        }
#endif
    }
#endif

    /// <param name="parameter">The builtin parameter</param>
    extension(BuiltInParameter parameter)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.LabelUtils.GetLabelFor(Autodesk.Revit.DB.BuiltInParameter)" />
        [Pure]
        public string ToLabel()
        {
            return LabelUtils.GetLabelFor(parameter);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.LabelUtils.GetLabelFor(Autodesk.Revit.DB.BuiltInParameter,Autodesk.Revit.ApplicationServices.LanguageType)" />
        [Pure]
        public string ToLabel(LanguageType language)
        {
            return LabelUtils.GetLabelFor(parameter, language);
        }
    }

    /// <param name="category">The builtin category</param>
    extension(BuiltInCategory category)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.LabelUtils.GetLabelFor(Autodesk.Revit.DB.BuiltInCategory)" />
        [Pure]
        public string ToLabel()
        {
            return LabelUtils.GetLabelFor(category);
        }
    }
#if !REVIT2025_OR_GREATER
    /// <param name="parameterGroup">The builtin parameter group</param>
    extension(BuiltInParameterGroup parameterGroup)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.LabelUtils.GetLabelFor(Autodesk.Revit.DB.BuiltInParameterGroup)" />
        [Pure]
#if REVIT2024
        [Obsolete("This method is deprecated in Revit 2024 and may be removed in a future version of Revit. Please use the `GetLabelForGroup(typeId)` method instead.")]
#endif
        public string ToLabel()
        {
            return LabelUtils.GetLabelFor(parameterGroup);
        }
    }
#endif
#if !REVIT2022_OR_GREATER
    /// <param name="displayUnitType">The display unit type</param>
    extension(DisplayUnitType displayUnitType)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.LabelUtils.GetLabelFor(Autodesk.Revit.DB.DisplayUnitType)" />
        [Pure]
#if REVIT2021
        [Obsolete("This method is deprecated in Revit 2021")]
#endif
        public string ToLabel()
        {
            return LabelUtils.GetLabelFor(displayUnitType);
        }
    }
#endif
#if !REVIT2023_OR_GREATER
    /// <param name="parameterType">The parameter type</param>
    extension(ParameterType parameterType)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.LabelUtils.GetLabelFor(Autodesk.Revit.DB.ParameterType)" />
        [Pure]
#if REVIT2022
    [Obsolete("This method is deprecated in Revit 2022")]
#endif
        public string ToLabel()
        {
            return LabelUtils.GetLabelFor(parameterType);
        }
    }
#endif
#if REVIT2026_OR_GREATER
    /// <param name="severity">The Severity enum value</param>
    extension(FailureSeverity severity)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.LabelUtils.GetFailureSeverityName(Autodesk.Revit.DB.FailureSeverity)"/>
        [Pure]
        public string ToLabel()
        {
            return LabelUtils.GetFailureSeverityName(severity);
        }
    }
#endif

    /// <param name="sectionShape">The StructuralSectionShape enum value.</param>
    extension(StructuralSectionShape sectionShape)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.LabelUtils.GetStructuralSectionShapeName(Autodesk.Revit.DB.Structure.StructuralSections.StructuralSectionShape)" />
        [Pure]
        public string ToLabel()
        {
            return LabelUtils.GetStructuralSectionShapeName(sectionShape);
        }
    }
}
