using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class TheEater : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("'Sword of Corruption'");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 58;
            Item.height = 58;
            Item.scale = 1.125f;
            Item.rare = ItemRarityID.LightPurple;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 30;
            Item.useAnimation = 30;           
            Item.damage = 18; 
            Item.knockBack = 3f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 50);
            Item.shootSpeed = 5f;			
            Item.autoReuse = false; 
            Item.DamageType = DamageClass.Melee;
	    }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Vector2 spawnPos = Main.rand.NextVector2Circular(200f, 200f);
            Vector2 spawnVel = spawnPos.SafeNormalize(Vector2.UnitY) * Item.shootSpeed;
            spawnPos = target.Center - spawnPos;
            for (int i = 0; i < 5; i++)
            {
                if (Collision.SolidCollision(spawnPos, 8, 8))
                {
                    spawnPos = target.Center - spawnPos;
                }
            }
            int projType = Main.rand.NextBool(8) ? ProjectileID.EatersBite : ProjectileID.TinyEater;

            Projectile.NewProjectile(target.GetSource_OnHit(target), spawnPos, spawnVel, projType, Item.damage / 2, Item.knockBack, player.whoAmI);
        }
	   
	    public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(2))
			{
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.CorruptPlants, 0f, 0f, 100, default, 2f);
                Main.dust[dust].noGravity = true;
			}
		}
    }
}
