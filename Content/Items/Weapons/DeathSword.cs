using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class DeathSword : ModItem
    {
        public override void SetStaticDefaults() => ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;

        public override void SetDefaults()
        { 
            Item.width = 64;
            Item.height = 72; 
			Item.scale = 1.25f;
            Item.rare = ItemRarityID.LightRed;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 60;
            Item.useAnimation = 30;           
            Item.damage = 20; 
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item8;
			Item.shoot = ModContent.ProjectileType<DeathProj>();
            Item.shootSpeed = 1f;
            Item.value = Item.sellPrice(silver: 50);			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(2))
			{
                UniverseUtils.SpawnRotatedDust(player, DustID.ShadowbeamStaff, 2f, (int)(20 * Item.scale), (int)(72 * Item.scale));
            }
		}

        public override bool AltFunctionUse(Player player) => true;

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (player.altFunctionUse == 2)
            {
                for (int j = 0; j < 3; j++)
                {
                    Vector2 spawnPos = player.Center - Vector2.UnitY * 16f + (0.3f * j * MathHelper.TwoPi + MathHelper.PiOver2).ToRotationVector2() * 60f;
                    Vector2 spawnVel = (target.Center - spawnPos).SafeNormalize(Vector2.UnitY) * Item.shootSpeed;
                    Projectile.NewProjectile(target.GetSource_OnHit(target), spawnPos + spawnVel, spawnVel * Main.rand.NextFloat(0.9f, 1.26f), Item.shoot, hit.Damage / 2, Item.knockBack, player.whoAmI);
                }
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                Projectile.NewProjectile(source, position - Vector2.UnitY * 48f + velocity * 2f, velocity, type, damage, knockback, player.whoAmI, ai1: 1f);                                
            }
            return false;
        }
    }
}