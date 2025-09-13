using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class Sharkron : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Shaaark!");
		}
		
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64; 
			Item.scale = 1.25f;
            Item.rare = ItemRarityID.Yellow;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 20;   
            Item.useAnimation = 20; 			
            Item.damage = 98; 
            Item.knockBack = 6f;
            Item.UseSound = SoundID.Item1;
            Item.value = 440000;		
            Item.shoot = ModContent.ProjectileType<AquaticusProj>();
            Item.shootSpeed = 5f;			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < Main.rand.Next(1, 6); i++)
            {
                Projectile.NewProjectile(source, position + velocity * 4f, velocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(1f, 1.5f), type, (int)(damage * 0.5), knockback / 2, player.whoAmI, ai1: (float)Main.rand.Next(60, 151) / 100f);
            }
            return false;
        }


        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(player, target, hit, damageDone);
        }
	}
}