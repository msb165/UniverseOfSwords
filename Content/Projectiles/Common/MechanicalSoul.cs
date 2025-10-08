using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class MechanicalSoul : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 3;
            ProjectileID.Sets.TrailCacheLength[Type] = 60;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(20);
            Projectile.scale = 1.5f;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 100;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.ai[0]++;
            if (Projectile.ai[0] > 16f)
            {
                Projectile.velocity *= 0.97f;
            }
            if (Main.rand.NextBool(2))
            {
                Vector2 dustPos = Vector2.Normalize(Projectile.velocity) * 20f;
                Vector2 offset = Projectile.velocity.RotatedBy(MathHelper.PiOver2 * Projectile.direction) * 0.33f + Projectile.velocity / 4f;
                Vector2 offset2 = Projectile.velocity.RotatedBy(-MathHelper.PiOver2 * Projectile.direction) * 0.33f + Projectile.velocity / 4f;
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Clentaminator_Blue);
                dust.position = Projectile.Center + dustPos;
                dust.velocity = offset; 
                dust.noGravity = true;
                dust.scale = 1.5f;
                Dust dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Clentaminator_Red);
                dust2.position = Projectile.Center + dustPos;
                dust2.velocity = offset2;
                dust2.noGravity = true;
                dust2.scale = 1.5f;
                Dust dust3 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Clentaminator_Green);
                dust3.position = Projectile.Center - Projectile.velocity * 0.25f;
                dust3.noGravity = true;
                dust3.scale = 1.5f;
            }
            FindFrame();
        }

        public void FindFrame()
        {
            Projectile.frameCounter++; //increase the frameCounter by one
            if (Projectile.frameCounter >= Main.projFrames[Type]) //once the frameCounter has reached 10 - change the 10 to change how fast the projectile animates
            {
                Projectile.frameCounter = 0; //reset the counter
                if (++Projectile.frame >= Main.projFrames[Type]) //if past the last frame
                {
                    Projectile.frame = 0; //go back to the first frame
                }
            }
        }

        public override bool PreDraw(ref Color lightColor) //this is where the animation happens
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically;
            int frameHeight = texture.Height / Main.projFrames[Type];
            int startY = frameHeight * Projectile.frame;
            Rectangle sourceRectangle = new(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2;
            Color drawColor = Projectile.GetAlpha(lightColor);
            Color spinColor = drawColor with { A = 0 };
            Color trailColor = drawColor;
            float timer = (float)Main.timeForVisualEffects / 60f;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.75f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, sourceRectangle, trailColor, Projectile.oldRot[i], origin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);

            // Spinning thing
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition + (Vector2.UnitY * 8f).RotatedBy(MathHelper.TwoPi * timer), sourceRectangle with { Y = 0 }, spinColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition + (Vector2.UnitY * 8f).RotatedBy(MathHelper.TwoPi * timer + MathHelper.TwoPi / 3f), sourceRectangle with { Y = frameHeight * 1 }, spinColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition + (Vector2.UnitY * 8f).RotatedBy(MathHelper.TwoPi * timer + MathHelper.Pi + MathHelper.PiOver2), sourceRectangle with { Y = frameHeight * 2 }, spinColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);

            return false;
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.Damage();
            for (int k = 0; k < 10; k++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Clentaminator_Blue, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f, Scale: 2f);
                dust.noGravity = true;
                Dust dust2 = Dust.NewDustDirect(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Clentaminator_Red, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f, Scale: 2f);
                dust2.noGravity = true;
                Dust dust3 = Dust.NewDustDirect(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Clentaminator_Green, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f, Scale: 2f);
                dust3.noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
    }
}