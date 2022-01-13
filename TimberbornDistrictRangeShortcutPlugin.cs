using System.Reflection;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace TimberbornDistrictRangeShortcut
{
    [BepInPlugin("TimberbornDistrictRangeShortcut", "TimberbornDistrictRangeShortcut", "0.0.1")]
    [HarmonyPatch]
    public class TimberbornDistrictRangeShortcutPlugin : BaseUnityPlugin
    {

        internal static ManualLogSource Log;
        internal static Dictionary<string, KeyControl> shortcuts;
        internal static ConfigEntry<string> shortcutCE;

        public void Awake()
        {
            // Saves the logger instance
            Log = Logger;
            Logger.LogInfo("\n\n\n\nPlugin is loaded!\n\n\n\n");

            shortcutCE = Config.Bind("Key bindings", "showDistrictRange", "G");
            shortcuts = new Dictionary<string, KeyControl> {
                { "Test", ConfigEntryToKeyControl(shortcutCE) },
            };

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }

        private KeyControl ConfigEntryToKeyControl(ConfigEntry<string> configEntry)
        {
            string value = configEntry.Value.ToLower();
            Logger.LogInfo("Key is " + value);

            if (Keyboard.current == null) { 
                Logger.LogError("Keyboard is null :(");
                return null;
            }
            else if(value == "") { 
                Logger.LogError("Keyboard is not null, but value is null :(");
                return null;
            }
            else
                return (KeyControl)Keyboard.current[value];
        }

    }
}
