using HarmonyLib;
using SilklessSkills.Settings;
using System;

namespace SilklessSkills.Helpers
{
    [HarmonyPatch(typeof(HeroController), "TakeSilk", new Type[]{ typeof(int), typeof(SilkSpool.SilkTakeSource) })]
    public static class HeroController_TakeSilk
    {
        [HarmonyPrefix]
        public static void Prefix(HeroController __instance, ref int amount, SilkSpool.SilkTakeSource source)
        {
            // Wispfire Lantern
            if (ConfigSettings.lanternFlag.Value &&
                source == SilkSpool.SilkTakeSource.Wisp)
            {
                amount = 0;
            }
        }
    }
}
