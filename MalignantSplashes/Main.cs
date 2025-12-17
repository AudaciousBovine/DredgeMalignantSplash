﻿﻿using System.Linq;
using Winch.Util;
using HarmonyLib;
using System.Reflection;
using UnityEngine;
using Winch.Config;
using UnityEngine.Localization.SmartFormat.Utilities;
using Winch.Core;

namespace MalignantSplashes
{
    public class Loader
    {
        /// This method is run by Winch to initialize your mod
        public static void Initialize()
        {
            /// RUNS HARMONY PATCHES
            new Harmony("com.AudaciousBovine.MalignantSplashes").PatchAll(Assembly.GetExecutingAssembly());
            /// Runs Game Loaded Method
            ApplicationEvents.Instance.OnGameLoaded += OnGameLoaded;
        }
        
            // This method enables the shipwright to buy materials
        private static void OnGameLoaded()
        {
            var shipwrightDestination = DockUtil.GetAllShipyardDestinations().FirstOrDefault(shipyard => shipyard.Id == "destination.gm-shipwright");
            shipwrightDestination.itemSubtypesBought |= ItemSubtype.MATERIAL;
        }
    }
}