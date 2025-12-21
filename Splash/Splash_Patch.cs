using HarmonyLib;
using UnityEngine;

namespace MalignantSplashes.Splash.Patches
{
    // Harmony Patch that changes properties of an item when it is created in the grid UI
    [HarmonyPatch(typeof(GridUI))]
    public static class Splash_Patch
    {
        [HarmonyPrefix]
        // Patches CreateObject method, this includes when the inventory is opened
        [HarmonyPatch("CreateObject")]
        public static void CreateObjectPrefix(GridUI __instance, SpatialItemInstance entry)
        {
            DockData currentDock = GameManager.Instance.Player.CurrentDock.dockData;
            SpatialItemData itemData = entry.GetItemData<SpatialItemData>();

            if (itemData.id == "dark-splash")
            {
                /// Changing item properties
                itemData.canBeDiscardedByPlayer = false;
                itemData.canBeDiscardedDuringQuestPickup = false;                    
                itemData.itemType = ItemType.EQUIPMENT;
                itemData.canBeSoldByPlayer = true;

                // Allow free movement when at the Iron Rig
                if (currentDock.id == "dock.the-iron-rig")
                {
                    itemData.moveMode = MoveMode.FREE;
                }
                else
                {
                    // Change Dark Splash movement mode based on config setting
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