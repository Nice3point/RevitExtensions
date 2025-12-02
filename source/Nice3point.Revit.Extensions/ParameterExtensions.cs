using System.Reflection;
using System.Runtime.InteropServices;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Parameter Extensions
/// </summary>
[PublicAPI]
public static class ParameterExtensions
{
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
        public Parameter ToParameter(Document document)
        {
            const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

            var documentType = typeof(Document);
            var parameterType = typeof(Parameter);
            var assembly = Assembly.GetAssembly(parameterType)!;
            var aDocumentType = assembly.GetType("ADocument")!;
            var elementIdType = assembly.GetType("ElementId")!;
            var elementIdIdType = elementIdType.GetField("<alignment member>", bindingFlags)!;
            var getADocumentType = documentType.GetMethod("getADocument", bindingFlags)!;
            var parameterCtorType = parameterType.GetConstructor(bindingFlags, null, [aDocumentType.MakePointerType(), elementIdType.MakePointerType()], null)!;

            var elementId = Activator.CreateInstance(elementIdType)!;
            elementIdIdType.SetValue(elementId, builtInParameter);

            var handle = GCHandle.Alloc(elementId);
            var elementIdPointer = GCHandle.ToIntPtr(handle);
            Marshal.StructureToPtr(elementId, elementIdPointer, true);

            var parameter = (Parameter)parameterCtorType.Invoke([getADocumentType.Invoke(document, null), elementIdPointer]);
            handle.Free();

            return parameter;
        }
    }
}