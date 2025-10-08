using Terraria;
using Terraria.ModLoader;

namespace UniverseOfSwords.Buffs
{
    public class LesserMeleePower : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lesser Melee Power");
            // Description.SetDefault("Increased melee stats: 4% increased melee crit, 10% increased melee damage and 10% increased melee speed");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetCritChance(DamageClass.Melee) += 4;
            player.GetCritChance(DamageClass.MeleeNoSpeed) += 4;
            player.GetDamage(DamageClass.Melee) += 0.1f;
            player.GetDamage(DamageClass.MeleeNoSpeed) += 0.1f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
            player.GetAttackSpeed(DamageClass.MeleeNoSpeed) += 0.1f;
        }
    }
}