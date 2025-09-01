using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class TheForce : ModItem
    {
        public override void SetDefaults()
        { 
            Item.width = 64;
            Item.height = 64; 
			Item.scale = 1.0F;
            Item.rare = 4;            
            Item.useStyle = 1;             
            Item.useTime = 28;
            Item.useAnimation = 28;           
            Item.damage = 30; 
            Item.knockBack = 5.5F;
            Item.UseSound = SoundID.Item1;
			Item.shoot = ProjectileID.Starfury;
            Item.shootSpeed = 20;
            Item.value = 74800;			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(1) == 0)
			{
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 15, 0f, 0f, 100, default, 2f);
                Main.dust[dust].noGravity = true;
			}
		}
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Starfury, 1);
			recipe.AddIngredient(null, "MasterSword", 1);
			recipe.AddIngredient(ItemID.HellstoneBar, 15);
			recipe.AddIngredient(null, "UpgradeMatter", 1);
			recipe.AddIngredient(null, "SwordMatter", 150);
            recipe.AddTile(TileID.DemonAltar);			
            recipe.Register();
	    } 
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
           target.AddBuff(BuffID.Ichor, 360);
           target.AddBuff(BuffID.OnFire, 360);
		   target.AddBuff(BuffID.Midas, 360);
        }
    }
}
