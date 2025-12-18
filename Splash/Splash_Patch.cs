using HarmonyLib;
using MalignantSplashes;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UI;

namespace MalignantSplashes.Splash.Patches
{
    [HarmonyPatch(typeof(GridUI))]
    public static class Splash_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch("CreateObject")]
        public static void CreateObjectPrefix(GridUI __instance, SpatialItemInstance entry)
        {
            /// Getting Dark Splash Item
            if (entry.id == "dark-splash")
            {
                /// Setting the item data to a variable for easier use
                SpatialItemData dark_splash = entry.GetItemData<SpatialItemData>();
                if (dark_splash != null)
                {
                    /// Changing item properties
                    dark_splash.canBeDiscardedByPlayer = false; 
                    dark_splash.itemSubtype = ItemSubtype.MATERIAL;
                    dark_splash.canBeSoldByPlayer = true;
                    dark_splash.value = 1;

                    /// Making Dark Splash Unmovable if the config option is enabled
                    if (Main.Config.GetProperty<string>("malignance") == "NONE")
                    {
                        dark_splash.moveMode = MoveMode.NONE;
                    }
                    else if (Main.Config.GetProperty<string>("malignance") == "INSTALL")
                    {
                        dark_splash.moveMode = MoveMode.INSTALL;
                    } 
                    else if (Main.Config.GetProperty<string>("malignance") == "FREE")
                    {
                        dark_splash.moveMode = MoveMode.FREE;
                    }
                    
                }
            }
        }
    }
}