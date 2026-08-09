using System.Reflection;
#if NET
using System.Runtime.CompilerServices;

#else
using System.Runtime.InteropServices;
#endif

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Parameter Extensions
/// </summary>
[PublicAPI]
public static class ParameterExtensions
{
    private static readonly Assembly ParameterAssembly = Assembly.GetAssembly(typeof(Parameter))!;
    private static readonly Type ADocumentType = ParameterAssembly.GetType("ADocument")!;
    private static readonly Type ElementIdType = ParameterAssembly.GetType("ElementId")!;
    private static readonly MethodInfo GetADocumentMethod = typeof(Document).GetMethod("getADocument", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;
    private static readonly ConstructorInfo ParameterConstructor = typeof(Parameter).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly, null, [ADocumentType.MakePointerType(), ElementIdType.MakePointerType()], null)!;

    /// <param name="parameter">The source parameter</param>
    extension(Parameter parameter)
    {
        /// <summary>Provides access to the boolean value within the parameter</summary>
        /// <returns>The bool value contained in the parameter</returns>
        /// <remarks>The AsBool method should only be used if the StorageType property returns that the internal contents of the parameter is an integer</remarks>
        [Pure]
        public bool AsBool()
        {
            return parameter.AsInteger() == 1;
        }

        /// <summary>Provides access to the Color within the parameter</summary>
        /// <returns>The Color value contained in the parameter</returns>
        /// <remarks>The AsColor method should only be used if the StorageType property returns that the internal contents of the parameter is an integer</remarks>
        [Pure]
        public Color AsColor()
        {
            var argb = parameter.AsInteger();
            return new Color((byte)(argb & byte.MaxValue), (byte)((argb >> 8) & byte.MaxValue), (byte)((argb >> 16) & byte.MaxValue));
        }

        /// <summary>Provides access to the Element within the parameter</summary>
        /// <returns>The element contained in the parameter as an ElementId</returns>
        /// <remarks>The AsElement method should only be used if the StorageType property returns that the internal contents of the parameter is an ElementId</remarks>
        [Pure]
        public Element? AsElement()
        {
            var elementId = parameter.AsElementId();
            return elementId == ElementId.InvalidElementId ? null : parameter.Element.Document.GetElement(elementId);
        }

        /// <summary>Provides access to the Element within the parameter</summary>
        /// <returns>The element contained in the parameter as an ElementId</returns>
        /// <remarks>The AsElement method should only be used if the StorageType property returns that the internal contents of the parameter is an ElementId</remarks>
        /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element" /></typeparam>
        [Pure]
        public T? AsElement<T>() where T : Element
        {
            var elementId = parameter.AsElementId();
            return elementId == ElementId.InvalidElementId ? null : (T)parameter.Element.Document.GetElement(elementId);
        }

        /// <summary>Sets the parameter to a new bool value</summary>
        /// <param name="value">The new bool value to which the parameter is to be set</param>
        /// <returns>The Set method will return True if the parameter was successfully set to the new value, otherwise false</returns>
        /// <remarks>You should only use this method if the StorageType property reports the type of the parameter as an integer</remarks>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">The parameter is read-only</exception>
        public bool Set(bool value)
        {
            return parameter.Set(value ? 1 : 0);
        }

        /// <summary>Sets the parameter to a new color</summary>
        /// <param name="value">The new color to which the parameter is to be set</param>
        /// <returns>The Set method will return True if the parameter was successfully set to the new value, otherwise false</returns>
        /// <remarks>You should only use this method if the StorageType property reports the type of the parameter as an integer</remarks>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">The parameter is read-only</exception>
        public bool Set(Color value)
        {
            return parameter.Set((value.Red << 0) | (value.Green << 8) | (value.Blue << 16));
        }
    }

    /// <param name="builtInParameter">The source parameter</param>
    extension(BuiltInParameter builtInParameter)
    {
        /// <summary>
        /// Converts a BuiltInParameter into a Revit Parameter object.
        /// </summary>
        /// <param name="document">The Revit Document associated with the parameter conversion.</param>
        /// <returns>A Parameter object corresponding to the specified BuiltInParameter.</returns>
        /// <remarks>This method performs low-level operation to instantiate a Parameter object.</remarks>
        public
#if NET
            unsafe
#endif
            Parameter ToParameter(Document document)
        {
#if REVIT2024_OR_GREATER
            var elementId = (long)builtInParameter;
#else
            var elementId = (int)builtInParameter;
#endif
#if NET
            var aDocument = GetADocumentMethod.Invoke(document, null);
            var parameter = (Parameter)ParameterConstructor.Invoke([aDocument, (nint)Unsafe.AsPointer(ref elementId)]);

            return parameter;
#else
            var aDocument = GetADocumentMethod.Invoke(document, null);

            var handle = GCHandle.Alloc(elementId, GCHandleType.Pinned);
            var parameter = (Parameter)ParameterConstructor.Invoke([aDocument, handle.AddrOfPinnedObject()]);
            handle.Free();

            return parameter;
#endif
        }

        /// <summary>
        ///     Create an ElementId handle with the given BuiltInParameter id.
        /// </summary>
        [Pure]
        public ElementId ToElementId()
        {
            return new ElementId(builtInParameter);
        }

        /// <summary>
        ///     Retrieves the numeric value of the BuiltInParameter.
        /// </summary>
        /// <returns>The value the parameter constant holds.</returns>
        [Pure]
        public long ToLong()
        {
            return (long)builtInParameter;
        }
    }

    /// <param name="elementId">The unique identification for an element.</param>
    extension(ElementId elementId)
    {
        /// <summary>
        ///     Checks if ElementID is a parameter identifier
        /// </summary>
        [Pure]
        [Obsolete("Use IsParameter() instead")]
        [CodeTemplate(
            searchTemplate: "$expr$.AreEquals($parameter$)",
            Message = "AreEquals is obsolete, use IsParameter instead",
            ReplaceTemplate = "$expr$.IsParameter($parameter$)",
            ReplaceMessage = "Replace with IsParameter")]
        public bool AreEquals(BuiltInParameter parameter)
        {
            return elementId.IsParameter(parameter);
        }

        /// <summary>
        ///     Checks if ElementID is a parameter identifier
        /// </summary>
        [Pure]
        public bool IsParameter(BuiltInParameter parameter)
        {
#if REVIT2024_OR_GREATER
            return elementId.Value == (long)parameter;
#else
            return elementId.IntegerValue == (int)parameter;
#endif
        }
    }
}