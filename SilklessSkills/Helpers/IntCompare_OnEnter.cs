using HarmonyLib;
using HutongGames.PlayMaker.Actions;
using SilklessSkills.Settings;
using System;
using System.Collections.Generic;

namespace SilklessSkills.Helpers
{
    [HarmonyPatch(typeof(IntCompare), "OnEnter")]
    public static class IntCompare_OnEnter
    {
        /// <summary>
        /// Stores the silk cost of various actions so they can be reset afterwards
        /// </summary>
        private static Dictionary<SilkSkills, int?> silkCosts = new Dictionary<SilkSkills, int?>();

        [HarmonyPrefix]
        public static void Prefix(IntCompare __instance)
        {
            if (NegateCost(__instance, out SilkSkills skill))
            {
                if (!silkCosts.ContainsKey(skill))
                {
                    silkCosts.Add(skill, null);
                }

                silkCosts[skill] = __instance.integer1.Value;
                __instance.integer1.Value = Math.Max(__instance.integer1.Value, __instance.integer2.Value + 1);
            }
        }

        [HarmonyPostfix]
        public static void Postfix(IntCompare __instance)
        {
            SilkSkills skill = GetCurrentSkill(__instance);
            if (silkCosts.ContainsKey(skill) &&
                silkCosts[skill] != null)
            {
#pragma warning disable CS8629 // Nullable value type may be null.
                __instance.integer1.Value = silkCosts[skill].Value;
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
        private static bool NegateCost(IntCompare __instance, out SilkSkills skill)
        {
            skill = GetCurrentSkill(__instance);
            switch (skill)
            {
                case SilkSkills.Clawline:
                    return ConfigSettings.harpoonFlag.Value;
                case SilkSkills.SilkSoar:
                    return ConfigSettings.superJumpFlag.Value;
                case SilkSkills.SilkspeedAnklets:
                    return ConfigSettings.speedFlag.Value;
                case SilkSkills.Silkshot:
                    return ConfigSettings.gunFlag.Value;
                case SilkSkills.SnareSetter:
                    return ConfigSettings.trapFlag.Value;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets the name of the skill currently being checked
        /// </summary>
        /// <param name="__instance"></param>
        /// <returns></returns>
        private static SilkSkills GetCurrentSkill(IntCompare __instance)
        {
            if (__instance.fsm.name.Equals("Harpoon Dash") &&
                __instance.State.name.Equals("Can Do?"))
            {
                return SilkSkills.Clawline;
            }

            if (__instance.fsm.name.Equals("Superjump") &&
                __instance.State.name.Equals("Enough Silk?") &&
                ConfigSettings.superJumpFlag.Value)
            {
                return SilkSkills.SilkSoar;
            }

            if (__instance.Fsm.Name.Equals("Sprint Silk Usage") &&
                (__instance.State.name.Equals("Start Usage") ||
                    __instance.State.name.Equals("Usage") ||
                    __instance.State.name.Equals("Pause Usage")))
            {
                return SilkSkills.SilkspeedAnklets;
            }

            if (__instance.State.name.Equals("Silk? F") ||
                __instance.State.name.Equals("Silk? A"))
            {
                return SilkSkills.Silkshot;
            }

            if (__instance.State.name.Equals("Silk? Snare"))
            {
                return SilkSkills.SnareSetter;
            }

            return SilkSkills.None;
        }
    }
}