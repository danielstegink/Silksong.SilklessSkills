using HarmonyLib;
using HutongGames.PlayMaker.Actions;
using SilklessSkills.Settings;
using System.Collections.Generic;

namespace SilklessSkills.Helpers
{
    [HarmonyPatch(typeof(CallMethodProper), "OnEnter")]
    public static class CallMethodProper_OnEnter
    {
        /// <summary>
        /// Stores the silk cost of various actions so they can be reset afterwards
        /// </summary>
        private static Dictionary<SilkSkills, int?> silkCosts = new Dictionary<SilkSkills, int?>();

        [HarmonyPrefix]
        public static void Prefix(CallMethodProper __instance)
        {
            if (NegateCost(__instance, out SilkSkills skill))
            {
                if (!silkCosts.ContainsKey(skill))
                {
                    silkCosts.Add(skill, null);
                }

                silkCosts[skill] = __instance.parameters[0].intValue;
                __instance.parameters[0].intValue = 0;
            }
        }

        [HarmonyPostfix]
        public static void Postfix(CallMethodProper __instance)
        {
            SilkSkills skill = GetCurrentSkill(__instance);
            if (silkCosts.ContainsKey(skill) &&
                silkCosts[skill] != null)
            {
#pragma warning disable CS8629 // Nullable value type may be null.
                __instance.parameters[0].intValue = silkCosts[skill].Value;
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
        private static bool NegateCost(CallMethodProper __instance, out SilkSkills skill)
        {
            skill = GetCurrentSkill(__instance);
            switch (skill)
            {
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
        private static SilkSkills GetCurrentSkill(CallMethodProper __instance)
        {
            if (__instance.State.Name.Equals("Silk? F") ||
                    __instance.State.Name.Equals("Silk? A"))
            {
                return SilkSkills.Silkshot;
            }

            if (__instance.State.Name.Equals("Snare Grnd Check"))
            {
                return SilkSkills.SnareSetter;
            }

            return SilkSkills.None;
        }
    }
}