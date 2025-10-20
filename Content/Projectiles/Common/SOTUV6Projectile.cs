using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities.Projectiles;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class SOTUV6Projectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sotu Projectile 6");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Type] = 120;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Type] = 2;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;               //The width of projectile hitbox
            Projectile.height = 8;          //The height of projectile hitbox
            Projectile.scale = 1f;
            Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
            Projectile.timeLeft = 300;
            Projectile.extraUpdates = 2;
        }

        public override void AI()
        {
            //for (int i = 0; i < 3; i++)
            //{
            //    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.SandSpray, Alpha: 100);
            //    dust.velocity *= 0.6f;
            //    dust.position = Projectile.Center;
            //    dust.noGravity = true;
            //}
            Projectile.VampireKnivesAI(ai: 0, maxTime: 60f);
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Zombie103);
            Projectile.Resize(144, 144);
            Projectile.Damage();
            for (int i = 0; i < 20; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.SandSpray, Alpha: 100, Scale: 2.5f);
                dust.noGravity = true;
                dust.velocity *= 2.5f;
                Dust dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.SandSpray, Alpha: 100);
                dust2.noGravity = true;
            }
        }

        //public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        //{
        //    float _ = 0f;
        //    Vector2 velocity = Projectile.velocity.SafeNormalize(Vector2.UnitY) * Projectile.scale;
        //    return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center - velocity * 40f, Projectile.Center + velocity * 120f, 60f * Projectile.scale, ref _);
        //}

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D glowTexture = TextureAssets.Extra[ExtrasID.FallingStar].Value;
            Rectangle sourceRect = glowTexture.Frame();
            Vector2 origin = new(sourceRect.Width / 2f, 4f);
            Vector2 vector34 = new(0f, Projectile.gfxOffY);
            Vector2 spinningpoint = new(0f, -5f);
            float timer = (float)Main.timeForVisualEffects / 60f;
            Vector2 vector35 = Projectile.Center + Projectile.velocity;
            Color outerColor = Color.Gold with { A = 0 } * 0.2f * Projectile.Opacity;
            Color innerColor = Color.White with { A = 0 } * 0.5f * Projectile.Opacity;
            Main.spriteBatch.Draw(glowTexture, vector35 - Main.screenPosition + vector34 + spinningpoint.RotatedBy(MathHelper.TwoPi * timer), sourceRect, outerColor, Projectile.rotation, origin, 1.5f, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(glowTexture, vector35 - Main.screenPosition + vector34 + spinningpoint.RotatedBy(MathHelper.TwoPi * timer + MathHelper.TwoPi / 3f), sourceRect, outerColor, Projectile.rotation, origin, 1.1f, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(glowTexture, vector35 - Main.screenPosition + vector34 + spinningpoint.RotatedBy(MathHelper.TwoPi * timer + MathHelper.Pi + MathHelper.PiOver4), sourceRect, outerColor, Projectile.rotation, origin, 1.3f, SpriteEffects.None, 0f);
            Vector2 vector36 = Projectile.Center - Projectile.velocity * 0.5f;
            for (float j = 0f; j < 1f; j += 0.5f)
            {
                float limitedTimer = timer % 0.5f / 0.5f;
                limitedTimer = (limitedTimer + j) % 1f;
                float doubleLimitedTimer = limitedTimer * 2f;
                if (doubleLimitedTimer > 1f)
                {
                    doubleLimitedTimer = 2f - doubleLimitedTimer;
                }
                Main.spriteBatch.Draw(glowTexture, vector36 - Main.screenPosition + vector34, sourceRect, innerColor * doubleLimitedTimer, Projectile.rotation, origin, 0.3f + limitedTimer * 0.5f, SpriteEffects.None, 0f);
            }
            return false;
        }
    }
}