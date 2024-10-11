// ReSharper disable once CheckNamespace

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.FamilyUtils"/> class.
/// </summary>
public static class FamilyUtilsExtensions
{
    /// <summary>Indicates whether the family can be converted to face host based.</summary>
    /// <param name="family">The family.</param>
    /// <returns>
    /// True if the family can be converted to face-based.
    /// Otherwise false, which will be returned if there any family instances exist in the project, the family is already face-based, or the family does not have a host.
    /// Also, false is returned if the family does not belong to one of the following categories:
    /// <list type="bullet"><item>OST_CommunicationDevices</item><item>OST_DataDevices</item><item>OST_DuctTerminal</item><item>OST_ElectricalEquipment</item><item>OST_ElectricalFixtures</item><item>OST_FireAlarmDevices</item><item>OST_LightingDevices</item><item>OST_LightingFixtures</item><item>OST_MechanicalControlDevices</item><item>OST_MechanicalEquipment</item><item>OST_NurseCallDevices</item><item>OST_PlumbingEquipment</item><item>OST_PlumbingFixtures</item><item>OST_SecurityDevices</item><item>OST_Sprinklers</item><item>OST_TelephoneDevices</item></list></returns>
    [Pure]
    public static bool CanConvertToFaceHostBased(this Family family)
    {
        return FamilyUtils.FamilyCanConvertToFaceHostBased(family.Document, family.Id);
    }

    /// <summary>Converts a family to be face host based.</summary>
    /// <remarks>
    ///    Converts a family hosted by some element other than a face to be hosted by a face. This is done by replacing the existing host (wall, roof, ceiling, floor) with a face.
    ///    Conversion can succeed only if FamilyUtils.FamilyCanConvertToFaceHostBased() returns true.
    /// </remarks>
    /// <param name="family">The family.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The input familyId cannot be converted to face host based.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    Failed to convert the family to face host based.
    ///    -or-
    ///    The family is already unhosted.
    /// </exception>
    public static void ConvertToFaceHostBased(this Family family)
    {
        FamilyUtils.ConvertFamilyToFaceHostBased(family.Document, family.Id);
    }

    /// <summary>Gets the profile Family Symbols of the document.</summary>
    /// <param name="document">The document.</param>
    /// <param name="profileFamilyUsage">The profile family usage.</param>
    /// <param name="oneCurveLoopOnly">
    ///    Whether or not to return only profiles with one curve loop.
    /// </param>
    /// <returns>The set of profile Family Symbol element ids.</returns>
    public static ICollection<ElementId> GetProfileSymbols(this Document document, ProfileFamilyUsage profileFamilyUsage, bool oneCurveLoopOnly)
    {
        return FamilyUtils.GetProfileSymbols(document, profileFamilyUsage, oneCurveLoopOnly);
    }
}