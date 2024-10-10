using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB.Macros;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit MacroManager Extensions
/// </summary>
public static class MacroManagerExtensions
{
    /// <summary>Sets the document macro security options.</summary>
    /// <param name="application">The application.</param>
    /// <param name="macroOptions">The document macro security options.</param>
    public static void SetDocumentMacroSecurityOptions(this Application application, DocumentMacroOptions macroOptions)
    {
        MacroManager.SetDocumentMacroSecurityOptions(application, macroOptions);
    }

    /// <summary>Gets the document macro security options.</summary>
    /// <param name="application">The application.</param>
    /// <returns>Returns the document macro security options.</returns>
    public static DocumentMacroOptions GetDocumentMacroSecurityOptions(this Application application)
    {
        return MacroManager.GetDocumentMacroSecurityOptions(application);
    }

    /// <summary>Sets the application macro security options.</summary>
    /// <param name="application">The application.</param>
    /// <param name="macroOptions">The application macro security options.</param>
    public static void SetApplicationMacroSecurityOptions(this Application application, ApplicationMacroOptions macroOptions)
    {
        MacroManager.SetApplicationMacroSecurityOptions(application, macroOptions);
    }

    /// <summary>Gets the application macro security options.</summary>
    /// <param name="application">The application.</param>
    /// <returns>Returns the application macro security options.</returns>
    public static ApplicationMacroOptions GetApplicationMacroSecurityOptions(this Application application)
    {
        return MacroManager.GetApplicationMacroSecurityOptions(application);
    }
    
    /// <summary>Gets the Macro manager from the document.</summary>
    /// <param name="document">The document.</param>
    /// <returns>The new Macro manager object.</returns>
    public static MacroManager GetMacroManager(this Document document)
    {
        return MacroManager.GetMacroManager(document);
    }
    
    /// <summary>Gets the Macro manager from the application.</summary>
    /// <param name="application">The application.</param>
    /// <returns>The new Macro manager object.</returns>
    public static MacroManager GetMacroManager(this Application application)
    {
        return MacroManager.GetMacroManager(application);
    }
}