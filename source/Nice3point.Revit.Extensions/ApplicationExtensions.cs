#if NET8_0_OR_GREATER
using Nice3point.Revit.Extensions.Internal;
#else
using System.Reflection;
#endif
using Autodesk.Revit.ApplicationServices;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Autodesk Revit Application Extensions
/// </summary>
[PublicAPI]
public static class ApplicationExtensions
{
    /// <param name="application">The source Application</param>
    extension(Application application)
    {
        /// <summary>
        ///     Creates a <see cref="ControlledApplication" /> from the current <see cref="Application" /> instance.
        ///     This allows using APIs that require a <see cref="ControlledApplication" />,
        ///     outside of the <see cref="Autodesk.Revit.DB.IExternalDBApplication.OnStartup" /> context.
        /// </summary>
        /// <returns>A <see cref="ControlledApplication" /> wrapping the current <see cref="Application" /></returns>
        [Pure]
        public ControlledApplication AsControlledApplication()
        {
#if NET8_0_OR_GREATER
            return UnsafeAccessors.CreateControlledApplication(application);
#else
            return (ControlledApplication)Activator.CreateInstance(
                typeof(ControlledApplication),
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                [application],
                null)!;
#endif
        }
    }
}
