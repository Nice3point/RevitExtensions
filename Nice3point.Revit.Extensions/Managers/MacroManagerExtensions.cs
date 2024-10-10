using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB.Macros;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit MacroManager Extensions
/// </summary>
public static class MacroManagerExtensions
{
    /// <summary>Sets the application macro security options.</summary>
    /// <param name="application">The application.</param>
    /// <param name="macroOptions">The application macro security options.</param>
    public static void SetMacroSecurityOptions(this Application application, ApplicationMacroOptions macroOptions)
    {
        MacroManager.SetApplicationMacroSecurityOptions(application, macroOptions);
    }

    /// <summary>Gets the application macro security options.</summary>
    /// <param name="application">The application.</param>
    /// <returns>Returns the application macro security options.</returns>
    [Pure]
    public static ApplicationMacroOptions GetMacroSecurityOptions(this Application application)
    {
        return MacroManager.GetApplicationMacroSecurityOptions(application);
    }
    
    /// <summary>Gets the Macro manager from the application.</summary>
    /// <param name="application">The application.</param>
    /// <returns>The new Macro manager object.</returns>
    [Pure]
    public static MacroManager GetMacroManager(this Application application)
    {
        return MacroManager.GetMacroManager(application);
    }
}