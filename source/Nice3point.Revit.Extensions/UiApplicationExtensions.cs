using Autodesk.Revit.UI;
#if NET8_0_OR_GREATER
using Nice3point.Revit.Extensions.Internal;

#else
using System.Reflection;
#endif

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.UI;

/// <summary>
///     Autodesk Revit UIApplication Extensions
/// </summary>
[PublicAPI]
public static class UiApplicationExtensions
{
    /// <param name="uiApplication">The source UIApplication</param>
    extension(UIApplication uiApplication)
    {
        /// <summary>
        ///     Creates a <see cref="UIControlledApplication" /> from the current <see cref="UIApplication" /> instance.
        ///     This allows using APIs that require a <see cref="UIControlledApplication" />, such as ribbon customization during application startup,
        ///     outside of the <see cref="Autodesk.Revit.UI.IExternalApplication.OnStartup" /> context.
        /// </summary>
        /// <returns>A <see cref="UIControlledApplication" /> wrapping the current <see cref="UIApplication" /></returns>
        [Pure]
        public UIControlledApplication AsControlledApplication()
        {
#if NET8_0_OR_GREATER
            return UnsafeUiAccessors.CreateUiControlledApplication(uiApplication);
#else
            return (UIControlledApplication) Activator.CreateInstance(
                typeof(UIControlledApplication),
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                [uiApplication],
                null)!;
#endif
        }
    }
}