using UnityEngine;
using Winch.Core;
using HarmonyLib;

namespace MalignantSplashes.Splash.Patches
{

    [HarmonyPatch(typeof(ItemManager), "GetItemValue")]
    public static class ItemManager_Patch
    {
        
        [HarmonyPostfix]
        public static void GetItemValue(ref decimal __result, SpatialItemInstance itemInstance, ItemManager.BuySellMode mode, float sellValueModifier = 1f)
        {
            BaseDestination currentDestination = GameManager.Instance.UI.CurrentDestination;
            SpatialItemData itemData = itemInstance.GetItemData<SpatialItemData>();

            if (itemData.id == "dark-splash") 
            { 
                if (currentDestination.id == "destination.tir-undermarket")
                {
                    itemData.value = Main.Config.GetProperty<decimal>("splashSellValue");
                    itemData.moveMode = MoveMode.FREE;
                    __result *= 1;
                }
                else
                {
                    itemData.value = Main.Config.GetProperty<decimal>("splashRemoveValue"); 
                    __result *= -1;

                    if (Main.Config.GetProperty<string>("malignance") == "NONE")
                    {
                        itemData.moveMode = MoveMode.NONE;
                    }
                    else if (Main.Config.GetProperty<string>("malignance") == "INSTALL")
                    {
                        itemData.moveMode = MoveMode.INSTALL;
                    } 
                    else if (Main.Config.GetProperty<string>("malignance") == "FREE")
                    {
                        itemData.moveMode = MoveMode.FREE;
                    }
                }
            }
        }
    }
}