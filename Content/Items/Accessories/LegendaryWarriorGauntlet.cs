using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Materials;

namespace UniverseOfSwords.Content.Items.Accessories
{
    public class LegendaryWarriorGauntlet : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Legendary Warrior's Gauntlet");
            /* Tooltip.SetDefault("'Legendary gauntlet that grants wearer ultimate melee skills'"
				+ "\n30 defense"
				+ "\nHighly increased melee damage"
			    + "\n30% increased melee critical chance"
				+ "\nGreatly increased life regeneration"
				+ "\nIncreases maximum life by 200"
				+ "\n20% increased endurance"
				+ "\nGrants immunity to most debuffs"
				+ "\nGrants Spelunker, Thorns and Titan potion effects"); */
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.sellPrice(platinum: 1);
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 30;
            player.GetDamage(DamageClass.Melee) += 1.5f;
            player.lifeRegen += 15;
            player.GetCritChance(DamageClass.Melee) += 30;
            player.statLifeMax2 += 200;
            player.endurance += 0.20f;
            player.AddBuff(BuffID.Spelunker, 5);
            player.AddBuff(BuffID.Titan, 5);
            player.AddBuff(BuffID.Thorns, 5);
            player.meleeScaleGlove = true;
            player.buffImmune[BuffID.Bleeding] = true;
            player.buffImmune[BuffID.BrokenArmor] = true;
            player.buffImmune[BuffID.Burning] = true;
            player.buffImmune[BuffID.Darkness] = true;
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.Cursed] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Silenced] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.Chilled] = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.WarriorEmblem, 1)
                .AddIngredient(ItemID.DestroyerEmblem, 1)
                .AddIngredient(ItemID.AvengerEmblem, 1)
                .AddIngredient(ItemID.FireGauntlet, 1)
                .AddIngredient(ItemID.PowerGlove, 1)
                .AddIngredient(ItemID.TitanGlove, 1)
                .AddIngredient(ItemID.MechanicalGlove, 1)
                .AddIngredient(ItemID.EyeoftheGolem, 1)
                .AddIngredient(ItemID.CelestialShell, 1)
                .AddIngredient(ItemID.AnkhShield, 1)
                .AddIngredient(ItemID.LifeFruit, 10)
                .AddIngredient(ItemID.LunarBar, 99)
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 5000)
                .AddIngredient(ModContent.ItemType<LunarOrb>(), 4)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}