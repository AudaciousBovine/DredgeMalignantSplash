using UnityEngine;
using Winch.Core;
using HarmonyLib;

namespace MalignantSplashes.Splash.Patches
{

    [HarmonyPatch(typeof(TooltipSectionEquipmentDetails))]
    public static class TooltipSectionEquipmentDetails_Patch
    {
        
        [HarmonyPostfix]
        [HarmonyPatch("RefreshUI")]
        public static void RefreshUIPostfix(ref TooltipSectionEquipmentDetails __instance)
        {
            if (__instance.spatialItemInstance != null)
            {
                SpatialItemData itemData = __instance.spatialItemInstance.GetItemData<SpatialItemData>();
                if (itemData.id == "dark-splash" && itemData != null)
                {
                    bool flag = false;

                    if (itemData.moveMode == MoveMode.FREE)
                    {
                        flag = true;
                    }
                    __instance.operationalStatusLocalizedString.StringReference.SetReference(LanguageManager.STRING_TABLE, flag ? "AudaciousBovine.MalignantSplashes.dark-splash.free" : "AudaciousBovine.MalignantSplashes.dark-splash.malignant");
                    __instance.operationalStatusTextField.color = (flag ? GameManager.Instance.LanguageManager.GetColor(DredgeColorTypeEnum.POSITIVE) : GameManager.Instance.LanguageManager.GetColor(DredgeColorTypeEnum.NEGATIVE));
                }
            }
        }
    }
}