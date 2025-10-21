using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Items.Materials
{
    public class LunarOrb : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 5;
            // Tooltip.SetDefault("Essence of Lunar Towers");
        }

        public override void SetDefaults()
        {
            Item.Size = new(40);
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Cyan;
        }

        public override void AddRecipes()
        {
            Mod thorium = UniverseOfSwords.Instance.ThoriumMod;
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FragmentSolar, 10);
            recipe.AddIngredient(ItemID.FragmentVortex, 10);
            recipe.AddIngredient(ItemID.FragmentNebula, 10);
            recipe.AddIngredient(ItemID.FragmentStardust, 10);
            recipe.AddIngredient(ItemID.SoulofLight, 15);
            recipe.AddIngredient(ItemID.SoulofNight, 15);
            recipe.AddIngredient(ItemID.SoulofFlight, 15);
            recipe.AddIngredient(ItemID.SoulofMight, 20);
            recipe.AddIngredient(ItemID.SoulofFright, 20);
            recipe.AddIngredient(ItemID.SoulofSight, 20);
            if (thorium is not null)
            {
                recipe.AddIngredient(thorium.Find<ModItem>("CelestialFragment"), 20);
                recipe.AddIngredient(thorium.Find<ModItem>("ShootingStarFragment"), 20);
                recipe.AddIngredient(thorium.Find<ModItem>("InspirationFragment"), 20);
            }
            recipe.AddIngredient(ModContent.ItemType<MartianSaucerCore>());
            recipe.AddIngredient(ItemID.CelestialSigil);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}