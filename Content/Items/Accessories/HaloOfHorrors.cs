using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Items.Accessories
{
    public class HaloOfHorrors : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Halo of Horrors");
            /* Tooltip.SetDefault("'The fearsome power of the Dungeon Guardian is now yours!'"
				+ "\n100 defense"
				+ "\n100% increased armor penetration"
			    + "\nMaximum life increased by 2000"
				+ "\nProvides ultimate health regeneration"
				+ "\n15% increased damage"
				+ "\nCurses the wearer with infinite Potion Sickness debuff"
				+ "\n30% decreased melee speed"); */
        }

        public override void SetDefaults()
        {
            Item.width = 162;
            Item.height = 162;
            Item.value = Item.sellPrice(platinum: 1);
            Item.rare = ItemRarityID.Purple;
            Item.expert = true;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 100;
            player.GetArmorPenetration(DamageClass.Generic) += 100;
            player.lifeRegen += 40;
            player.GetAttackSpeed(DamageClass.Melee) -= 0.30f;
            player.statLifeMax2 += 2000;
            player.GetDamage(DamageClass.Melee) += 0.15f;
            player.GetDamage(DamageClass.Magic) += 0.15f;
            player.GetDamage(DamageClass.Throwing) += 0.15f;
            player.GetDamage(DamageClass.Summon) += 0.15f;
            player.GetDamage(DamageClass.Ranged) += 0.15f;
            player.AddBuff(BuffID.Rabies, 300);
            player.AddBuff(BuffID.PotionSickness, 10);
        }
    }
}