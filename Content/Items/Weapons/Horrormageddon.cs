using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class Horrormageddon : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("'Where you see an army, I see a graveyard'");
		}
		
        public override void SetDefaults()
        {
            Item.width = 35;
            Item.height = 35; 
			Item.scale = 2.4F;
            Item.rare = 10;            
            Item.useStyle = 1;             
            Item.useTime = 15;
            Item.useAnimation = 15;           
            Item.damage = 360; 
            Item.knockBack = 4.0F;
            Item.UseSound = SoundID.Item71;
			Item.shoot = ProjectileID.DeathSickle;
            Item.shootSpeed = 10;
            Item.value = 666666;			
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
			recipe.AddIngredient(null, "Doomsday", 1);
			recipe.AddIngredient(null, "Apocalypse", 1);
			recipe.AddIngredient(ItemID.Meowmere, 1);
			recipe.AddIngredient(ItemID.TheHorsemansBlade, 1);
			recipe.AddIngredient(ItemID.StarWrath, 1);
			recipe.AddIngredient(null, "Machine", 1);
			recipe.AddIngredient(null, "InnosWrath", 1);
			recipe.AddIngredient(null, "BeliarClaw", 1);
			recipe.AddIngredient(ItemID.LargeEmerald, 1);
			recipe.AddIngredient(null, "UpgradeMatter", 10);
			recipe.AddIngredient(null, "LunarOrb", 1);
            recipe.AddTile(TileID.LunarCraftingStation);			
            recipe.Register();
	    } 
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position, velocity, ProjectileID.Meowmere, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position, velocity, ProjectileID.InfernoFriendlyBlast, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position, velocity, ProjectileID.StarWrath, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position, velocity, ProjectileID.VortexBeaterRocket, damage, knockback, player.whoAmI);
            return true;
		}
    }
}