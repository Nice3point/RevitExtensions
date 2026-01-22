// ReSharper disable once CheckNamespace

using JetBrains.Annotations;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.FamilyUtils"/> class.
/// </summary>
[PublicAPI]
public static class FamilyUtilsExtensions
{
    /// <param name="family">The source family.</param>
    extension(Family family)
    {
        /// <summary></summary>
        [Pure]
        [Obsolete("Use CanBeConvertedToFaceHostBased() instead")]
        [CodeTemplate(
            searchTemplate: "$expr$.CanConvertToFaceHostBased()",
            Message = "CanConvertToFaceHostBased is obsolete, use CanBeConvertedToFaceHostBased instead",
            ReplaceTemplate = "$expr$.CanBeConvertedToFaceHostBased()",
            ReplaceMessage = "Replace with CanBeConvertedToFaceHostBased()")]
        public bool CanConvertToFaceHostBased()
        {
            return FamilyUtils.FamilyCanConvertToFaceHostBased(family.Document, family.Id);
        }

        /// <summary>Indicates whether the family can be converted to face host based.</summary>
        /// <returns>
        /// True if the family can be converted to face-based.
        /// Otherwise false, which will be returned if there any family instances exist in the project, the family is already face-based, or the family does not have a host.
        /// Also, false is returned if the family does not belong to one of the following categories:
        /// <list type="bullet"><item>OST_CommunicationDevices</item><item>OST_DataDevices</item><item>OST_DuctTerminal</item><item>OST_ElectricalEquipment</item><item>OST_ElectricalFixtures</item><item>OST_FireAlarmDevices</item><item>OST_LightingDevices</item><item>OST_LightingFixtures</item><item>OST_MechanicalControlDevices</item><item>OST_MechanicalEquipment</item><item>OST_NurseCallDevices</item><item>OST_PlumbingEquipment</item><item>OST_PlumbingFixtures</item><item>OST_SecurityDevices</item><item>OST_Sprinklers</item><item>OST_TelephoneDevices</item></list></returns>
        [Pure]
        public bool CanBeConvertedToFaceHostBased()
        {
            return FamilyUtils.FamilyCanConvertToFaceHostBased(family.Document, family.Id);
        }

        /// <summary>Converts a family to be face host based.</summary>
        /// <remarks>
        ///    Converts a family hosted by some element other than a face to be hosted by a face. This is done by replacing the existing host (wall, roof, ceiling, floor) with a face.
        ///    Conversion can succeed only if FamilyUtils.FamilyCanConvertToFaceHostBased() returns true.
        /// </remarks>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The input familyId cannot be converted to face host based.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Failed to convert the family to face host based.
        ///    -or-
        ///    The family is already unhosted.
        /// </exception>
        public Family ConvertToFaceHostBased()
        {
            FamilyUtils.ConvertFamilyToFaceHostBased(family.Document, family.Id);
            return family;
        }
    }

    /// <param name="elementId">The family element id.</param>
    extension(ElementId elementId)
    {
        /// <summary>Indicates whether the family can be converted to face host based.</summary>
        /// <param name="document">The document containing the family.</param>
        /// <returns>
        /// True if the family can be converted to face-based.
        /// Otherwise false, which will be returned if there any family instances exist in the project, the family is already face-based, or the family does not have a host.
        /// Also, false is returned if the family does not belong to one of the following categories:
        /// <list type="bullet"><item>OST_CommunicationDevices</item><item>OST_DataDevices</item><item>OST_DuctTerminal</item><item>OST_ElectricalEquipment</item><item>OST_ElectricalFixtures</item><item>OST_FireAlarmDevices</item><item>OST_LightingDevices</item><item>OST_LightingFixtures</item><item>OST_MechanicalControlDevices</item><item>OST_MechanicalEquipment</item><item>OST_NurseCallDevices</item><item>OST_PlumbingEquipment</item><item>OST_PlumbingFixtures</item><item>OST_SecurityDevices</item><item>OST_Sprinklers</item><item>OST_TelephoneDevices</item></list></returns>
        [Pure]
        public bool CanBeConvertedToFaceHostBased(Document document)
        {
            return FamilyUtils.FamilyCanConvertToFaceHostBased(document, elementId);
        }

        /// <summary>Converts a family to be face host based.</summary>
        /// <param name="document">The document containing the family.</param>
        /// <remarks>
        ///    Converts a family hosted by some element other than a face to be hosted by a face. This is done by replacing the existing host (wall, roof, ceiling, floor) with a face.
        ///    Conversion can succeed only if FamilyUtils.FamilyCanConvertToFaceHostBased() returns true.
        /// </remarks>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The input familyId cannot be converted to face host based.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Failed to convert the family to face host based.
        ///    -or-
        ///    The family is already unhosted.
        /// </exception>
        public ElementId ConvertToFaceHostBased(Document document)
        {
            FamilyUtils.ConvertFamilyToFaceHostBased(document, elementId);
            return elementId;
        }
    }

    /// <param name="document">The source document.</param>
    extension(Document document)
    {
        /// <summary>Gets the profile Family Symbols of the document.</summary>
        /// <param name="profileFamilyUsage">The profile family usage.</param>
        /// <param name="oneCurveLoopOnly">
        ///    Whether or not to return only profiles with one curve loop.
        /// </param>
        /// <returns>The set of profile Family Symbol element ids.</returns>
        [Pure]
        public ICollection<ElementId> GetProfileSymbols(ProfileFamilyUsage profileFamilyUsage, bool oneCurveLoopOnly)
        {
            return FamilyUtils.GetProfileSymbols(document, profileFamilyUsage, oneCurveLoopOnly);
        }
    }
}