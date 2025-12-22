using UnityEngine;
using Winch.Core;
using HarmonyLib;

namespace MalignantSplashes.Splash.Patches
{

    [HarmonyPatch(typeof(ItemManager), "GetItemValue")]
    public static class ItemManager_Patch
    {
        
        [HarmonyPostfix]
        public static void GetItemValuePostfix(ref decimal __result, SpatialItemInstance itemInstance, ItemManager.BuySellMode mode, float sellValueModifier = 1f)
        {
            if (itemInstance.GetItemData<SpatialItemData>() != null)
            {
                SpatialItemData itemData = itemInstance.GetItemData<SpatialItemData>();

                if (GameManager.Instance.UI.CurrentDestination != null && itemData != null)
                {
                    BaseDestination currentDestination = GameManager.Instance.UI.CurrentDestination;
                    
                    if (itemData.id == "dark-splash") 
                    { 
                        if (currentDestination.id == "destination.tir-undermarket")
                        {
                            itemData.value = Main.Config.GetProperty<decimal>("splashSellValue");
                            itemData.moveMode = MoveMode.FREE;
                            __result = itemData.value;
                        }
                        else
                        {
                            itemData.value = Main.Config.GetProperty<decimal>("splashRemoveCost"); 
                            __result = itemData.value * -1;

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
    }
}