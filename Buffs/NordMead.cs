using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Buffs
{
    public class NordMead : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Nord Mead");
            // Description.SetDefault("'When we raise our flagon to another dead dragon, there is just one drink we need!'");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetCritChance(DamageClass.Melee) += 5;
            player.GetDamage(DamageClass.Melee) += 0.15f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.15f;
            player.endurance += 0.5f;
            player.statDefense -= 8;
            player.AddBuff(BuffID.Tipsy, 400);
        }
    }
}