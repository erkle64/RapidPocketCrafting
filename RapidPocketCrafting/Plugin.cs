using C3.ModKit;
using HarmonyLib;
using Unfoundry;

namespace RapidPocketCrafting
{
    [UnfoundryMod(GUID)]
    public class Plugin : UnfoundryPlugin
    {
        public const string
            MODNAME = "RapidPocketCrafting",
            AUTHOR = "erkle64",
            GUID = AUTHOR + "." + MODNAME,
            VERSION = "0.1.0";

        public static LogSource log;

        public Plugin()
        {
            log = new LogSource(MODNAME);
        }

        public override void Load(Mod mod)
        {
            log.Log($"Loading {MODNAME}");
        }

        [HarmonyPatch]
        public static class Patch
        {
            [HarmonyPatch(typeof(CharacterManager), nameof(CharacterManager.Init))]
            [HarmonyPostfix]
            public static void CharacterManagerInit()
            {
                CharacterManager.characterManager_setCharacterCraftingSpeedDecrementPercentage(900000L);
            }

            [HarmonyPatch(typeof(CharacterManager), nameof(CharacterManager.increasePlayerCharacterCraftingSpeedByResearch))]
            [HarmonyPostfix]
            public static void CharacterManagerIncreasePlayerCharacterCraftingSpeedByResearch()
            {
                CharacterManager.characterManager_setCharacterCraftingSpeedDecrementPercentage(900000L);
            }
        }
    }
}
