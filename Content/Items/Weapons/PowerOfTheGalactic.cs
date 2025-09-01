using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class PowerOfTheGalactic : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Sword made from all galactic elements");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 32;
            Item.height = 32;			
			Item.scale = 1.5F;
            Item.rare = 10;            
            Item.useStyle = 1;             
            Item.useTime = 20;
            Item.useAnimation = 20;           
            Item.damage = 288;
            Item.knockBack = 15.0F;
			Item.shoot = ProjectileID.NebulaBlaze2 ;
            Item.shootSpeed = 10;
            Item.UseSound = SoundID.Item1;
            Item.value = 650500;			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
	   
       	public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
		}
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.FragmentSolar, 15);
			recipe.AddIngredient(ItemID.FragmentVortex, 15);
			recipe.AddIngredient(ItemID.FragmentNebula, 15);
			recipe.AddIngredient(ItemID.FragmentStardust, 15);
			recipe.AddIngredient(ItemID.LunarBar, 10);			
			recipe.AddIngredient(null, "LunarOrb", 1);
			recipe.AddIngredient(null, "SwordMatter", 150);
            recipe.AddTile(TileID.LunarCraftingStation);			
            recipe.Register();
	    }
		
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 10f * 0.0174f; //Replace 45 with whatever spread you want
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y)- spread/2;
            double deltaAngle = spread/2f;
            double offsetAngle;
            int i;
            for (i = 0; i < 3;i++ ) //Replace 2 with number of projectiles
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(source, position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), Item.shoot, damage, knockback, Item.playerIndexTheItemIsReservedFor);
            }
            return false;
        }
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
           target.AddBuff(BuffID.Ichor, 360);
           target.AddBuff(BuffID.OnFire, 360); 
           target.AddBuff(BuffID.Confused, 360);
           target.AddBuff(BuffID.Frostburn, 360);		   
        }
    }
}