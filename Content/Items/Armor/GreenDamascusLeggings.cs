using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Placeable;

namespace UniverseOfSwords.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class GreenDamascusLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Green Damascus Leggings");
            /* Tooltip.SetDefault("'Armor for fast warriors'"
                + "\n10% increased melee critical chance"
                + "\n35% increased movement speed"); */
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = Item.buyPrice(gold: 7);
            Item.rare = ItemRarityID.Green;
            Item.defense = 18;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.3f;
            player.GetCritChance(DamageClass.Melee) += 10;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DamascusBar>(), 15);
            recipe.AddIngredient(null, "DamascusLeggings", 1);
            recipe.AddIngredient(ItemID.SoulofMight, 15);
            recipe.AddIngredient(ItemID.SoulofSight, 15);
            recipe.AddIngredient(ItemID.SoulofFright, 15);
            recipe.AddIngredient(ItemID.SwiftnessPotion, 15);
            recipe.AddIngredient(ItemID.BeetleLeggings, 1);
            recipe.AddIngredient(ItemID.HallowedBar, 16);
            recipe.AddTile(TileID.MythrilAnvil);			
            recipe.Register();
        }
    }
}