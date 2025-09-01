using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class IceBreaker : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("'Freezing to the touch'");
		}
		
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64; 
			Item.scale = 1.25f;
            Item.rare = ItemRarityID.Lime;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 40;
            Item.useAnimation = 20;           
            Item.damage = 61; 
            Item.knockBack = 7f;
            Item.UseSound = SoundID.Item28;
			Item.shoot = ProjectileID.IceBolt;
            Item.shootSpeed = 40;
            Item.value = 300200;			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
	   
       	public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= -1f * player.gravDir;
		}
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.IceBlade, 1);
			recipe.AddIngredient(ItemID.SnowBlock, 1000);
			recipe.AddIngredient(null, "Orichalcon", 1);
			recipe.AddIngredient(null, "SwordShard", 1);
			recipe.AddIngredient(null, "SwordMatter", 150);
            recipe.AddTile(TileID.MythrilAnvil);			
            recipe.Register();
	    } 
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float numberProjectiles = 2 + Main.rand.Next(3); // 3, 4, or 5 shots
			float rotation = MathHelper.ToRadians(10f);
			position += Vector2.Normalize(velocity) * 5f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
				Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
			}
			return false;
		}
    }
}