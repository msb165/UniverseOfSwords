using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Materials;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class TrueTerrablade : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Shoots big projectile that explodes into smaller beams after hitting an enemy");
        }

        public override void SetDefaults()
        {
            Item.damage = 122;
            Item.DamageType = DamageClass.Melee;
            Item.width = 52;
            Item.height = 60;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 10f;
            Item.value = Item.sellPrice(gold: 10);
			Item.shoot = ModContent.ProjectileType<Projectiles.Common.TrueTerrablade>();
			Item.shootSpeed = 15f;
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item60;
            Item.autoReuse = true;
            Item.useTurn = true;
        }
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
            int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.TerraBlade, 0f, 0f, 100, default, 2f);
            Main.dust[dust].noGravity = true;
        }

        public override void AddRecipes()
        {
            /*Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.TerraBlade, 1);
			recipe.AddIngredient(ModContent.ItemType<TheNightmareAmalgamation>());
            recipe.AddTile(TileID.LunarCraftingStation);			
            recipe.Register();*/
	    } 
    }
}