using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class CosmoStorm : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("'Sword that shatters galaxies'");
		}
		
        public override void SetDefaults()
        {
            Item.width = 35;
            Item.height = 35; 
			Item.scale = 1.5F;
            Item.rare = 10;            
            Item.useStyle = 1; 
            Item.knockBack = 1.0F;            
            Item.useTime = 18;
            Item.useAnimation = 18;           
            Item.damage = 280; 
            Item.UseSound = SoundID.Item15;
			Item.shoot = ProjectileID.NebulaArcanum;
            Item.shootSpeed = 10;
            Item.value = 650000;			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
	   
       	public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
			player.itemLocation.X -= 3f * player.direction;
            player.itemLocation.Y -= -3f * player.direction;
		}
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.FragmentNebula, 80);
			recipe.AddIngredient(ItemID.FragmentSolar, 80);
			recipe.AddIngredient(null, "LunarOrb", 1);
			recipe.AddIngredient(null, "PowerOfTheGalactic", 1);
			recipe.AddIngredient(ItemID.LunarBar, 40);
			recipe.AddIngredient(ItemID.PortalGun, 1);
			recipe.AddIngredient(ItemID.NebulaArcanum, 1);
			recipe.AddIngredient(ItemID.TrueExcalibur, 1);
			recipe.AddIngredient(ItemID.LargeAmethyst, 4);
			recipe.AddIngredient(null, "UpgradeMatter", 10);
            recipe.AddTile(TileID.LunarCraftingStation);			
            recipe.Register();
	    } 
		
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 5f * 0.0174f; //Replace 45 with whatever spread you want
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y)- spread/2;
            double deltaAngle = spread/1f;
            double offsetAngle;
            int i;
            for (i = 0; i < 3;i++ ) //Replace 2 with number of projectiles
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(source, position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), Item.shoot, damage, knockback, Item.playerIndexTheItemIsReservedFor);
            }
            return false;
        }
    }
}