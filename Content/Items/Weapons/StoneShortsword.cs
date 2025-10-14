using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class StoneShortsword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 19;
            Item.height = 19;
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Blue;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 5;
            Item.knockBack = 3f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(copper: 20);
            Item.autoReuse = false;
            Item.DamageType = DamageClass.Melee;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Common.StoneShortsword>();
            Item.shootSpeed = 2.5f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.StoneBlock, 15)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}