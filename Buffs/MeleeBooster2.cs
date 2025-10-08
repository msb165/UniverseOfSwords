using Terraria;
using Terraria.ModLoader;

namespace UniverseOfSwords.Buffs
{
    public class MeleeBooster2 : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.Melee) += 0.1f;            
        }
    }
}
