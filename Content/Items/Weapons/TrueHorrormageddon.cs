using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class TrueHorrormageddon : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("'There used to be a graveyard, now it is a crater'");
		}
		
        public override void SetDefaults()
        {
            Item.width = 35;
            Item.height = 35; 
			Item.scale = 2.7F;
            Item.rare = 10;            
            Item.useStyle = 1;             
            Item.useTime = 15;
            Item.useAnimation = 15;           
            Item.damage = 600; 
            Item.knockBack = 8.0F;
            Item.UseSound = SoundID.Item71;
			Item.shoot = ProjectileID.DeathSickle;
            Item.shootSpeed = 20;
            Item.value = 10000000;			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
	   
       	public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
			player.itemLocation.X -= 1f * player.direction;
            player.itemLocation.Y -= 1f * player.direction;
		}
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Horrormageddon", 1);
			recipe.AddIngredient(null, "PowerOfTheGalactic", 1);
			recipe.AddIngredient(null, "GnomBlade", 1);
			recipe.AddIngredient(ItemID.BrokenHeroSword, 10);
			recipe.AddIngredient(null, "UpgradeMatter", 25);
			recipe.AddIngredient(null, "LunarOrb", 3);
			recipe.AddIngredient(ItemID.LunarBar, 100);
            recipe.AddTile(TileID.DemonAltar);			
            recipe.Register();
	    } 
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position, velocity, ProjectileID.Meowmere, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position, velocity, ProjectileID.InfernoFriendlyBlast, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position, velocity, ProjectileID.VortexBeaterRocket, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position, velocity, ProjectileID.StarWrath, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position.X, position.Y+2, velocity.X, velocity.Y+2, ProjectileID.Meowmere, damage, knockback, player.whoAmI); 
			Projectile.NewProjectile(source, position.X, position.Y+2, velocity.X, velocity.Y+2, ProjectileID.DeathSickle, damage, knockback, player.whoAmI); 
            Projectile.NewProjectile(source, position.X, position.Y+2, velocity.X, velocity.Y+2, ProjectileID.InfernoFriendlyBlast, damage, knockback, player.whoAmI); 
            Projectile.NewProjectile(source, position.X, position.Y+2, velocity.X, velocity.Y+2, ProjectileID.VortexBeaterRocket, damage, knockback, player.whoAmI); 
            Projectile.NewProjectile(source, position.X, position.Y+2, velocity.X, velocity.Y+2, ProjectileID.StarWrath, damage, knockback, player.whoAmI); 
			Projectile.NewProjectile(source, position.X, position.Y-2, velocity.X, velocity.Y-2, ProjectileID.Meowmere, damage, knockback, player.whoAmI); 
			Projectile.NewProjectile(source, position.X, position.Y-2, velocity.X, velocity.Y-2, ProjectileID.DeathSickle, damage, knockback, player.whoAmI); 
            Projectile.NewProjectile(source, position.X, position.Y-2, velocity.X, velocity.Y-2, ProjectileID.InfernoFriendlyBlast, damage, knockback, player.whoAmI); 
            Projectile.NewProjectile(source, position.X, position.Y-2, velocity.X, velocity.Y-2, ProjectileID.VortexBeaterRocket, damage, knockback, player.whoAmI); 
            Projectile.NewProjectile(source, position.X, position.Y-2, velocity.X, velocity.Y-2, ProjectileID.StarWrath, damage, knockback, player.whoAmI); 
		    return true;
		}
    }
}