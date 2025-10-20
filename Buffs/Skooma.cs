using Terraria;
using Terraria.ModLoader;

namespace UniverseOfSwords.Buffs
{
    public class Skooma : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Skooma");
            // Description.SetDefault("'S stands for Skooma man!'");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed += 0.50f;
            player.maxRunSpeed += 13.37f;
            player.jumpBoost = true;
            player.jumpSpeedBoost += 13.37f;
            player.extraFall += 45;
            player.portalPhysicsFlag = true;
            player.noFallDmg = false;
        }
    }
}
