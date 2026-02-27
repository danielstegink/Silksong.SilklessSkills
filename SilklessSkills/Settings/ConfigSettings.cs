using BepInEx.Configuration;
using TeamCherry.Localization;

namespace SilklessSkills.Settings
{
    public static class ConfigSettings
    {
        /// <summary>
        /// Integrates with UI to set whether or not Taunt costs Silk
        /// </summary>
        public static ConfigEntry<bool> tauntFlag;

        /// <summary>
        /// Integrates with UI to set whether or not Clawline costs Silk
        /// </summary>
        public static ConfigEntry<bool> harpoonFlag;

        /// <summary>
        /// Integrates with UI to set whether or not Silk Soar costs Silk
        /// </summary>
        public static ConfigEntry<bool> superJumpFlag;

        /// <summary>
        /// Integrates with UI to set whether or not Needolin costs Silk
        /// </summary>
        public static ConfigEntry<bool> needolinFlag;

        /// <summary>
        /// Integrates with UI to set whether or not Silkspeed Anklets costs Silk
        /// </summary>
        public static ConfigEntry<bool> speedFlag;

        /// <summary>
        /// Integrates with UI to set whether or not Silkshot costs Silk
        /// </summary>
        public static ConfigEntry<bool> gunFlag;

        /// <summary>
        /// Integrates with UI to set whether or not Snare Setter costs Silk
        /// </summary>
        public static ConfigEntry<bool> trapFlag;

        /// <summary>
        /// Integrates with UI to set whether or not Wispfire Lantern costs Silk
        /// </summary>
        public static ConfigEntry<bool> lanternFlag;

        /// <summary>
        /// Initializes the settings
        /// </summary>
        /// <param name="config"></param>
        public static void Initialize(ConfigFile config)
        {
            // Bind set methods to Config
            bool defaultValue = true;

            // Taunt
            LocalisedString name = new LocalisedString($"Mods.{SilklessSkills.Id}", "TAUNT_NAME");
            LocalisedString description = new LocalisedString($"Mods.{SilklessSkills.Id}", "TAUNT_DESC");
            if (name.Exists &&
                description.Exists)
            {
                tauntFlag = config.Bind<bool>("Modifier", name, defaultValue, description);
            }
            else
            {
                tauntFlag = config.Bind<bool>("Modifier", "1", defaultValue, "2");
            }

            // Clawline
            name = new LocalisedString($"Mods.{SilklessSkills.Id}", "HARPOON_NAME");
            description = new LocalisedString($"Mods.{SilklessSkills.Id}", "HARPOON_DESC");
            if (name.Exists &&
                description.Exists)
            {
                harpoonFlag = config.Bind<bool>("Modifier", name, defaultValue, description);
            }
            else
            {
                harpoonFlag = config.Bind<bool>("Modifier", "1", defaultValue, "2");
            }

            // Silk Soar
            name = new LocalisedString($"Mods.{SilklessSkills.Id}", "SUPER_JUMP_NAME");
            description = new LocalisedString($"Mods.{SilklessSkills.Id}", "SUPER_JUMP_DESC");
            if (name.Exists &&
                description.Exists)
            {
                superJumpFlag = config.Bind<bool>("Modifier", name, defaultValue, description);
            }
            else
            {
                superJumpFlag = config.Bind<bool>("Modifier", "1", defaultValue, "2");
            }

            // Needolin
            name = new LocalisedString($"Mods.{SilklessSkills.Id}", "NEEDOLIN_NAME");
            description = new LocalisedString($"Mods.{SilklessSkills.Id}", "NEEDOLIN_DESC");
            if (name.Exists &&
                description.Exists)
            {
                needolinFlag = config.Bind<bool>("Modifier", name, defaultValue, description);
            }
            else
            {
                needolinFlag = config.Bind<bool>("Modifier", "1", defaultValue, "2");
            }

            // Silkspeed Anklets
            name = new LocalisedString($"Mods.{SilklessSkills.Id}", "SPEED_NAME");
            description = new LocalisedString($"Mods.{SilklessSkills.Id}", "SPEED_DESC");
            if (name.Exists &&
                description.Exists)
            {
                speedFlag = config.Bind<bool>("Modifier", name, defaultValue, description);
            }
            else
            {
                speedFlag = config.Bind<bool>("Modifier", "1", defaultValue, "2");
            }

            // Silkshot
            name = new LocalisedString($"Mods.{SilklessSkills.Id}", "GUN_NAME");
            description = new LocalisedString($"Mods.{SilklessSkills.Id}", "GUN_DESC");
            if (name.Exists &&
                description.Exists)
            {
                gunFlag = config.Bind<bool>("Modifier", name, defaultValue, description);
            }
            else
            {
                gunFlag = config.Bind<bool>("Modifier", "1", defaultValue, "2");
            }

            // Snare Setter
            name = new LocalisedString($"Mods.{SilklessSkills.Id}", "TRAP_NAME");
            description = new LocalisedString($"Mods.{SilklessSkills.Id}", "TRAP_DESC");
            if (name.Exists &&
                description.Exists)
            {
                trapFlag = config.Bind<bool>("Modifier", name, defaultValue, description);
            }
            else
            {
                trapFlag = config.Bind<bool>("Modifier", "1", defaultValue, "2");
            }

            // Wispfire Lantern
            name = new LocalisedString($"Mods.{SilklessSkills.Id}", "LANTERN_NAME");
            description = new LocalisedString($"Mods.{SilklessSkills.Id}", "LANTERN_DESC");
            if (name.Exists &&
                description.Exists)
            {
                lanternFlag = config.Bind<bool>("Modifier", name, defaultValue, description);
            }
            else
            {
                lanternFlag = config.Bind<bool>("Modifier", "1", defaultValue, "2");
            }
        }
    }
}