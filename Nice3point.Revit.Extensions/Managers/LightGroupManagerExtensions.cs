﻿using Autodesk.Revit.DB.Lighting;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Lighting.LightGroupManager"/> class.
/// </summary>
[PublicAPI]
public static class LightGroupManagerExtensions
{
    /// <summary>Creates a light group manager object from the given document</summary>
    /// <param name="document">The document the manager is from</param>
    /// <returns>The newly created Light group manager object</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The document is not valid because it is not a project (rvt) document
    /// </exception>
    [Pure]
    public static LightGroupManager GetLightGroupManager(this Document document)
    {
        return LightGroupManager.GetLightGroupManager(document);
    }
}