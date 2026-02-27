using HarmonyLib;
using HutongGames.PlayMaker.Actions;
using SilklessSkills.Settings;
using System.Collections.Generic;

namespace SilklessSkills.Helpers
{
    [HarmonyPatch(typeof(AddUsingSilk), "OnEnter")]
    public static class AddUsingSilk_OnEnter
    {
        /// <summary>
        /// Stores the silk cost of various actions so they can be reset afterwards
        /// </summary>
        private static Dictionary<SilkSkills, int?> silkCosts = new Dictionary<SilkSkills, int?>();

        [HarmonyPrefix]
        public static void Prefix(AddUsingSilk __instance)
        {
            if (NegateCost(__instance, out SilkSkills skill))
            {
                if (!silkCosts.ContainsKey(skill))
                {
                    silkCosts.Add(skill, null);
                }

                silkCosts[skill] = __instance.Amount.value;
                __instance.Amount.Value = 0;
            }
        }

        [HarmonyPostfix]
        public static void Postfix(AddUsingSilk __instance)
        {
            SilkSkills skill = GetCurrentSkill(__instance);
            if (silkCosts.ContainsKey(skill) &&
                silkCosts[skill] != null)
            {
#pragma warning disable CS8629 // Nullable value type may be null.
                __instance.Amount.Value = silkCosts[skill].Value;
#pragma warning restore CS8629 // Nullable value type may be null.
                silkCosts[skill] = null;
            }
        }

        /// <summary>
        /// Determines if a skill's cost needs to be negated
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="skill">Name of the skill to negate</param>
        /// <returns></returns>
        private static bool NegateCost(AddUsingSilk __instance, out SilkSkills skill)
        {
            skill = GetCurrentSkill(__instance);
            switch (skill)
            {
                case SilkSkills.SilkSoar:
                    return ConfigSettings.superJumpFlag.Value;
                case SilkSkills.Needolin:
                    return ConfigSettings.needolinFlag.Value;
                case SilkSkills.SilkspeedAnklets:
                    return ConfigSettings.speedFlag.Value;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets the name of the skill currently being checked
        /// </summary>
        /// <param name="__instance"></param>
        /// <returns></returns>
        private static SilkSkills GetCurrentSkill(AddUsingSilk __instance)
        {
            if (__instance.fsm.name.Equals("Superjump") &&
                __instance.State.name.Equals("Ground Charge"))
            {
                return SilkSkills.SilkSoar;
            }

            if (__instance.State.Name.Equals("Start Needolin Proper") ||
                __instance.State.Name.Equals("Resume Fast Travel Fail React"))
            {
                return SilkSkills.Needolin;
            }

            if (__instance.Fsm.Name.Equals("Sprint Silk Usage") &&
                __instance.State.Name.Equals("Usage"))
            {
                return SilkSkills.SilkspeedAnklets;
            }

            return SilkSkills.None;
        }
    }
}