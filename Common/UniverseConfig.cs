using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace UniverseOfSwords.Common
{
    public class UniverseConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("General")]
        [DefaultValue(false)]
        public bool enableVanillaChanges;

        [DefaultValue(true)]
        public bool starterSwords;

        [Header("Misc")]
        [DefaultValue(true)]
        public bool enableHoldStyle;
    }
}
