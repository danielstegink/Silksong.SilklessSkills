using HarmonyLib;
using HutongGames.PlayMaker.Actions;
using SilklessSkills.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace SilklessSkills.Helpers
{
    [HarmonyPatch(typeof(TakeSilk), "OnEnter")]
    public static class TakeSilk_OnEnter
    {
        /// <summary>
        /// Stores the silk cost of various actions so they can be reset afterwards
        /// </summary>
        private static Dictionary<SilkSkills, int?> silkCosts = new Dictionary<SilkSkills, int?>();

        [HarmonyPrefix]
        public static void Prefix(TakeSilk __instance)
        {
            if (NegateCost(__instance, out SilkSkills skill))
            {
                if (!silkCosts.ContainsKey(skill))
                {
                    silkCosts.Add(skill, null);
                }

                silkCosts[skill] = __instance.amount.Value;
                __instance.amount.Value = 0;
            }
        }

        [HarmonyPostfix]
        public static void Postfix(TakeSilk __instance)
        {
            SilkSkills skill = GetCurrentSkill(__instance);
            if (silkCosts.ContainsKey(skill) &&
                silkCosts[skill] != null)
            {
#pragma warning disable CS8629 // Nullable value type may be null.
                __instance.amount.Value = silkCosts[skill].Value;
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
        private static bool NegateCost(TakeSilk __instance, out SilkSkills skill)
        {
            skill = GetCurrentSkill(__instance);
            switch (skill)
            {
                case SilkSkills.Taunt:
                    return ConfigSettings.tauntFlag.Value;
                case SilkSkills.Clawline:
                    return ConfigSettings.harpoonFlag.Value;
                case SilkSkills.Needolin:
                    return ConfigSettings.needolinFlag.Value;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets the name of the skill currently being checked
        /// </summary>
        /// <param name="__instance"></param>
        /// <returns></returns>
        private static SilkSkills GetCurrentSkill(TakeSilk __instance)
        {
            if (__instance.Fsm.Name.Equals("Silk Specials") &&
                __instance.State.Name.Equals("Silk Taunt"))
            {
                return SilkSkills.Taunt;
            }

            if (__instance.Fsm.Name.Equals("Harpoon Dash") &&
                __instance.State.Name.Equals("Take Control"))
            {
                return SilkSkills.Clawline;
            }

            if (__instance.State.Name.Equals("Take Silk End"))
            {
                return SilkSkills.Needolin;
            }

            return SilkSkills.None;
        }
    }
}