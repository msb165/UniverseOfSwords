using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace UniverseOfSwordsMod.Common
{
    public class UniverseConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("Misc")]
        [DefaultValue(true)]
        public bool enableHoldStyle;
    }
}
