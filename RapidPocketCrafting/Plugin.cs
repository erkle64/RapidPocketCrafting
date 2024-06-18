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
            VERSION = "0.2.0";

        public static LogSource log;

        public static TypedConfigEntry<float> configCraftingTimeDecreasePercentage;

        public Plugin()
        {
            log = new LogSource(MODNAME);

            new Config(GUID)
                .Group("General")
                    .Entry(
                        out configCraftingTimeDecreasePercentage,
                        "Crafting time decrease percentage",
                        90.0f,
                        "Percentage of crafting time to remove.",
                        "Maximum: 90",
                        "Negative numbers will increase crafting time")
                .EndGroup()
                .Load()
                .Save();
        }

        public override void Load(Mod mod)
        {
            log.Log($"Loading {MODNAME}");
        }

        public static long GetCraftingTimeDecrease()
        {
            var value = (long)(configCraftingTimeDecreasePercentage.Get() * 10000.0f);
            return value > 900000L ? 900000L : value;
        }

        [HarmonyPatch]
        public static class Patch
        {
            [HarmonyPatch(typeof(CharacterManager), nameof(CharacterManager.Init))]
            [HarmonyPostfix]
            public static void CharacterManagerInit()
            {
                CharacterManager.characterManager_setCharacterCraftingSpeedDecrementPercentage(GetCraftingTimeDecrease());
            }

            [HarmonyPatch(typeof(CharacterManager), nameof(CharacterManager.increasePlayerCharacterCraftingSpeedByResearch))]
            [HarmonyPostfix]
            public static void CharacterManagerIncreasePlayerCharacterCraftingSpeedByResearch()
            {
                CharacterManager.characterManager_setCharacterCraftingSpeedDecrementPercentage(GetCraftingTimeDecrease());
            }
        }
    }
}
