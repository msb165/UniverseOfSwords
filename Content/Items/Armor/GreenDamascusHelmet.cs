using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Placeable;

namespace UniverseOfSwords.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class GreenDamascusHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Green Damascus Helmet");
            /* Tooltip.SetDefault("'Armor for fast warriors'"
			    + "\n20% increased melee speed"
			    + "\n14% increased melee critical chance"); */
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 18;
            Item.value = Item.buyPrice(gold: 7);
            Item.rare = ItemRarityID.Green;
            Item.defense = 20;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.Melee) += 0.20f;
            player.GetCritChance(DamageClass.Melee) += 14;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DamascusBar>(), 15);
            recipe.AddIngredient(null, "DamascusHelmet", 1);
            recipe.AddIngredient(ItemID.SoulofMight, 15);
            recipe.AddIngredient(ItemID.SoulofSight, 15);
            recipe.AddIngredient(ItemID.SoulofFright, 15);
            recipe.AddIngredient(ItemID.SwiftnessPotion, 15);
            recipe.AddIngredient(ItemID.BeetleHusk, 1);
            recipe.AddIngredient(ItemID.HallowedBar, 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}