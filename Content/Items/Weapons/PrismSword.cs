using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Held;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class PrismSword : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("'There is no pot of gold at the end of this rainbow. Only death'");
		}
		
        public override void SetDefaults()
        {
            Item.width = 35;
            Item.height = 35; 
			Item.scale = 1f;
			Item.channel = true;
            Item.rare = ItemRarityID.Red;            
            Item.useStyle = ItemUseStyleID.Shoot;                        
            Item.damage = 120; 
            Item.UseSound = SoundID.Item67;
			Item.shoot = ModContent.ProjectileType<Projectiles.Held.PrismSword>();
            Item.shootSpeed = 120;
            Item.value = 600000;			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
			Item.noUseGraphic = true;
			Item.noMelee = true;
	    }

		public override bool CanUseItem(Player player) => player.ownedProjectileCounts[Item.shoot] < 1;	   

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			return false;
        }
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LargeSapphire, 1);
			recipe.AddIngredient(ItemID.LargeRuby, 1);
			recipe.AddIngredient(ItemID.LargeEmerald, 1);
			recipe.AddIngredient(ItemID.LargeTopaz, 1);
			recipe.AddIngredient(ItemID.LargeAmethyst, 1);
			recipe.AddIngredient(ItemID.LargeDiamond, 1);
			recipe.AddIngredient(ItemID.CrystalShard, 50);
			recipe.AddIngredient(ItemID.LifeCrystal, 3);
			recipe.AddIngredient(ItemID.ManaCrystal, 3);
			recipe.AddIngredient(ItemID.RainbowCrystalStaff, 1);
			recipe.AddIngredient(ItemID.LastPrism, 1);
			recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddTile(TileID.CrystalBall);			
            recipe.Register();
	    } 
    }
}