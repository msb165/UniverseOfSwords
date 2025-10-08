using Terraria;
using Terraria.ModLoader;

namespace UniverseOfSwords.Buffs
{
    public class MeleeBooster1 : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Melee) += 0.15f;
            player.GetDamage(DamageClass.MeleeNoSpeed) += 0.15f;
        }
    }
}
