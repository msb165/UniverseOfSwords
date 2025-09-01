using Terraria;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Buffs
{
    public class MeleeBooster1 : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Melee) += 0.1f;
            player.GetDamage(DamageClass.MeleeNoSpeed) += 0.1f;
        }
    }
}
