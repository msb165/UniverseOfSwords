using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Placeable;

namespace UniverseOfSwords.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class RedDamascusChestplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Red Damascus Chestplate");
            /* Tooltip.SetDefault("'Armor for agressive warriors'"
			    + "\n10% increased melee damage"
				+ "\n10% increased melee critical chance"); */
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 20;
            Item.value = Item.buyPrice(gold: 7);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 20;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) => head.type == ModContent.ItemType<RedDamascusHelmet>() && legs.type == ModContent.ItemType<RedDamascusLeggings>();

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Grants Titan potion effect, 10% increased melee damage, 10% increased melee critical chance, maximum life increased by 20";
            player.GetDamage(DamageClass.Melee) += 0.10f;
            player.AddBuff(BuffID.Titan, 360);
            player.GetCritChance(DamageClass.Melee) += 10;
            player.statLifeMax2 += 20;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.10f;
            player.GetCritChance(DamageClass.Melee) += 10;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DamascusBar>(), 15);
            recipe.AddIngredient(ModContent.ItemType<DamascusBreastplate>());
            recipe.AddIngredient(ItemID.SoulofMight, 15);
            recipe.AddIngredient(ItemID.SoulofSight, 15);
            recipe.AddIngredient(ItemID.SoulofFright, 15);
            recipe.AddIngredient(ItemID.WrathPotion, 15);
            recipe.AddIngredient(ItemID.HallowedPlateMail, 1);
            recipe.AddIngredient(ItemID.HallowedBar, 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}