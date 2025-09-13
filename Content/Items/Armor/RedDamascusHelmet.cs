using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Placeable;

namespace UniverseOfSwordsMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class RedDamascusHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Red Damascus Helmet");
            /* Tooltip.SetDefault("'Armor for agressive warriors'"
			    + "\n10% increased melee damage"
			    + "\n14% increased melee critical chance"); */
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 18;
            Item.value = Item.buyPrice(gold: 7);
            Item.rare = 3;
            Item.defense = 20;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.1f;
            player.GetDamage(DamageClass.MeleeNoSpeed) += 0.1f;
            player.GetCritChance(DamageClass.Melee) += 14;
            player.GetCritChance(DamageClass.MeleeNoSpeed) += 14;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DamascusBar>(), 15);
            recipe.AddIngredient(null, "DamascusHelmet", 1);
            recipe.AddIngredient(ItemID.SoulofMight, 15);
            recipe.AddIngredient(ItemID.SoulofSight, 15);
            recipe.AddIngredient(ItemID.SoulofFright, 15);
            recipe.AddIngredient(ItemID.WrathPotion, 15);
            recipe.AddIngredient(ItemID.HallowedMask, 1);
            recipe.AddIngredient(ModContent.ItemType<BlackBar>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}