using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Projectiles.Held    
{
    public class DoubleBladedLightsaberProjectile : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<Items.Weapons.DoubleBladedLightsaber>().Texture;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Double Bladed Lightsaber");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Type] = 60;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }
        Player Player => Main.player[Projectile.owner];

        public override void SetDefaults()
        {
            Projectile.width = 52;     //Set the hitbox width
            Projectile.height = 52;
            Projectile.scale = Player.HeldItem.scale;
            Projectile.friendly = true;    //Tells the game whether it is friendly to players/friendly npcs or not
            Projectile.penetrate = -1;    //Tells the game how many enemies it can hit before being destroyed. -1 = never
            Projectile.tileCollide = false; //Tells the game whether or not it can collide with a tile
            Projectile.ignoreWater = true; //Tells the game whether or not projectile will be affected by water        
            Projectile.DamageType = DamageClass.Melee;  //Tells the game whether it is a melee projectile or not
            Projectile.noEnchantmentVisuals = true;
        }

        public override void AI()
        {
            if (Projectile.soundDelay <= 0)
            {
                SoundEngine.PlaySound(SoundID.Item15, Projectile.Center);  
                Projectile.soundDelay = 45;    
            }

            if (!Player.channel || Player.noItems || Player.CCed)
            {
                Projectile.Kill();
            }
            Lighting.AddLight(Projectile.Center, Color.Red.ToVector3());     
            Projectile.ai[0]++;

            float rotation = Projectile.ai[0] * 0.15f;

            Projectile.Center = Player.MountedCenter;
            // Small offset from center
            Projectile.position.X += Player.width / 2 * Player.direction;  
            Projectile.spriteDirection = Player.direction;
            // Slowly increase rotation with a cap of 2f
            Projectile.rotation = MathHelper.WrapAngle(MathHelper.Lerp(0f, 2f, rotation * Player.direction)); 
            SetPlayerValues();
        }

        public void SetPlayerValues()
        {
            Player.SetDummyItemTime(2);
            Player.heldProj = Projectile.whoAmI;
            Player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - MathHelper.PiOver2 - MathHelper.PiOver4);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            NPCLoader.OnHitByItem(target, Player, Player.HeldItem, hit, damageDone);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float _ = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + Projectile.rotation.ToRotationVector2() * 60f * Projectile.scale, 2f, ref _);
        }

        public override bool PreDraw(ref Color lightColor)  //this make the projectile sprite rotate perfectaly around the player
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Color drawColor = Color.White;
            Color trailColor = drawColor with { A = 0 };
            Color outlineColor = trailColor;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.6f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, null, trailColor, Projectile.oldRot[i], texture.Size() / 2, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, drawColor, Projectile.rotation, texture.Size() / 2, Projectile.scale, spriteEffects, 0f);

            // "Glow" effect
            float timer = MathF.Cos((float)Main.timeForVisualEffects * MathHelper.TwoPi / 30f);
            for (int i = 0; i < 8; i++)
            {
                Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition + Vector2.UnitY.RotatedBy(MathHelper.PiOver4 * i) * (4f + 1f * timer), null, outlineColor * (0.75f + 0.25f * timer) * 0.125f, Projectile.rotation, texture.Size() / 2, Projectile.scale, spriteEffects, 0f);
            }
            return false;
        }
    }
}