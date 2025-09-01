using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class SwordOfTheUniverseV6 : ModItem
    {
		public override void SetStaticDefaults()
		{
		    // DisplayName.SetDefault("Sword of the Universe");
			/* Tooltip.SetDefault("'This sword doesn't swing. It lifts the Universe towards the blade'"
			    + "\nHas changeable forms"); */
		}
		
        public override void SetDefaults()
        {
            Item.width = 100;
            Item.height = 100; 
			Item.scale = 1.1f;
            Item.rare = ItemRarityID.Purple;
            Item.crit = 16;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 15;
            Item.useAnimation = 15;           
            Item.damage = 1337; 
            Item.knockBack = 20.0F;
            Item.UseSound = new SoundStyle($"{nameof(UniverseOfSwordsMod)}/Assets/Sounds/Item/GiantExplosion");
			Item.shoot = Mod.Find<ModProjectile>("SOTU7").Type;
            Item.shootSpeed = 15f;
			Item.expert = true;
            Item.value = Item.sellPrice(platinum: 10);			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override void AddRecipes()
        {
			Recipe recipe = CreateRecipe(1);
			recipe.AddIngredient(null, "SwordOfTheUniverseV2");
			recipe.Register();
			
			recipe = CreateRecipe(1);
			recipe.AddIngredient(null, "SwordOfTheUniverse");
			recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SwordOfTheUniverseV9");
            recipe.Register();

            recipe = CreateRecipe(1);
			recipe.AddIngredient(null, "SwordOfTheUniverseV3");
			recipe.Register();
			
			recipe = CreateRecipe(1);
			recipe.AddIngredient(null, "SwordOfTheUniverseV4");
			recipe.Register();
			
			recipe = CreateRecipe(1);
			recipe.AddIngredient(null, "SwordOfTheUniverseV5");
			recipe.Register();
			
			recipe = CreateRecipe(1);
			recipe.AddIngredient(null, "SwordOfTheUniverseV7");
			recipe.Register();
			
			recipe = CreateRecipe(1);
			recipe.AddIngredient(null, "SwordOfTheUniverseV8");
			recipe.Register();
	    }
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(2) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 21, 0f, 0f, 100, default, 2f);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity.X -= player.direction * 0f;
			    Main.dust[dust].velocity.Y -= 0.0f;
            }
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position, velocity, ProjectileID.VortexBeaterRocket, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X+2, velocity.Y+2, ProjectileID.VortexBeaterRocket, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X-2, velocity.Y-2, ProjectileID.VortexBeaterRocket, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X+4, velocity.Y+4, ProjectileID.VortexBeaterRocket, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X-4, velocity.Y-4, ProjectileID.VortexBeaterRocket, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X+6, velocity.Y+6, ProjectileID.VortexBeaterRocket, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X-6, velocity.Y-6, ProjectileID.VortexBeaterRocket, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X+8, velocity.Y+8, ProjectileID.VortexBeaterRocket, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X-8, velocity.Y-8, ProjectileID.VortexBeaterRocket, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X+10, velocity.Y+10, ProjectileID.VortexBeaterRocket, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position, velocity, ProjectileID.InfluxWaver, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X+1, velocity.Y+1, ProjectileID.InfluxWaver, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X-1, velocity.Y-1, ProjectileID.InfluxWaver, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X+2, velocity.Y+2, ProjectileID.InfluxWaver, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X-2, velocity.Y-2, ProjectileID.InfluxWaver, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X+3, velocity.Y+3, ProjectileID.InfluxWaver, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X-3, velocity.Y-3, ProjectileID.InfluxWaver, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X+4, velocity.Y+4, ProjectileID.InfluxWaver, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X-4, velocity.Y-4, ProjectileID.InfluxWaver, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X+5, velocity.Y+5, ProjectileID.InfluxWaver, damage, knockback, player.whoAmI);
            return true;
		}
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
           target.AddBuff(BuffID.Midas, 360);
           target.AddBuff(BuffID.Ichor, 360); 
           target.AddBuff(BuffID.Frostburn, 360);
           target.AddBuff(BuffID.OnFire, 360);
           target.AddBuff(BuffID.Poisoned, 360);
           target.AddBuff(BuffID.CursedInferno, 360);		   
        }
    }
}