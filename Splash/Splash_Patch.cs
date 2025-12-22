using HarmonyLib;
using UnityEngine;

namespace MalignantSplashes.Splash.Patches
{
    
    // Harmony Patch that runs before or after the creation of an item in the grid UI
    [HarmonyPatch(typeof(GridUI))]
    public static class Splash_Patch
    {
        [HarmonyPostfix]
        // Patches CreateObject method, this includes when the inventory is opened
        [HarmonyPatch("CreateObject")]
        public static void CreateObjectPostfix(GridUI __instance, SpatialItemInstance entry)
        {
            if (entry.GetItemData<SpatialItemData>() != null)
            {
                SpatialItemData itemData = entry.GetItemData<SpatialItemData>();

                if (itemData.id == "dark-splash")
                {
                    // Changing item properties
                    itemData.canBeDiscardedByPlayer = false;
                    itemData.canBeDiscardedDuringQuestPickup = false;                    
                    itemData.itemType = ItemType.EQUIPMENT;
                    //itemData.itemSubtype = ItemSubtype.GADGET;
                    itemData.canBeSoldByPlayer = true;

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

                    if (GameManager.Instance.Player.CurrentDock != null && entry.GetItemData<SpatialItemData>() != null)
                    {
                        DockData currentDock = GameManager.Instance.Player.CurrentDock.dockData;

                        // Allow free movement when at the Iron Rig
                        if (currentDock.id == "dock.the-iron-rig")
                        {
                            itemData.moveMode = MoveMode.FREE;
                        }
                    }
                }
            }
        }
    }
}
