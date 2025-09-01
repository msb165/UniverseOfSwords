using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class TrueRuneWave : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.DD2SquireSonicBoom}";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(16);
            Projectile.alpha = 0;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.scale = 1.5f;
            Projectile.stopsDealingDamageAfterPenetrateHits = true;
            Projectile.penetrate = -1;
        }

        public float AttackType => Projectile.ai[1];
        Color outlineColor = Color.White;
        int buffType = BuffID.CursedInferno;

        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] >= 5f)
            {
                Projectile.scale += 0.1f;
                Projectile.alpha += 20;
                if (Projectile.alpha >= 255)
                {
                    Projectile.active = false;
                }
            }

            switch (AttackType)
            {
                case 0:
                    buffType = BuffID.CursedInferno;
                    outlineColor = Color.Green with { A = 40 };
                    break;
                case 1:
                    buffType = BuffID.OnFire3;
                    outlineColor = Color.Orange with { A = 40 };
                    break;
                case 2:
                    buffType = BuffID.Frostburn2;
                    outlineColor = Color.Cyan with { A = 40 };
                    break;
                case 3:
                    buffType = BuffID.OnFire;
                    outlineColor = new Color(220, 40, 30, 40);
                    break;
                case 4:
                    buffType = BuffID.ShadowFlame;
                    outlineColor = Color.Purple with { A = 40 };
                    break;
                default:
                    buffType = BuffID.CursedInferno;
                    outlineColor = Color.Green with { A = 40 };
                    break;
            }

            Projectile.localAI[0]++;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Lighting.AddLight(Projectile.position, Color.Red.ToVector3());
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(buffType, Main.rand.Next(200, 600));
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float _ = 0f;
            Vector2 velocity = Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedBy(-MathHelper.PiOver2) * Projectile.scale;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center - velocity * 40f, Projectile.Center + velocity * 40f, 16f * Projectile.scale, ref _))
            {
                return true;
            }
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            int frameHeight = texture.Height / Main.projFrames[Type];
            int startY = frameHeight * Projectile.frame;
            Rectangle sourceRectangle = new(0, startY, texture.Width, frameHeight);
            Vector2 drawOrigin = sourceRectangle.Size() / 2f;
            float num150 = 10f;
            SpriteEffects dir = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Color trailColor = outlineColor * Projectile.Opacity;

            for (int i = 1; i < Projectile.oldPos.Length; i++)
            {
                if (i >= Projectile.oldPos.Length)
                {
                    continue;
                }

                trailColor *= 0.75f;
                Vector2 oldPos = Projectile.oldPos[i];
                float trailRotation = Projectile.oldRot[i];
                SpriteEffects spriteEffects = Projectile.oldSpriteDirection[i] == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                if (oldPos == Vector2.Zero)
                {
                    continue;
                }
                Vector2 trailPos = oldPos + Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
                Main.EntitySpriteDraw(texture, trailPos, sourceRectangle, trailColor, trailRotation, drawOrigin, Projectile.scale, spriteEffects);
            }

            float offset = MathF.Cos(Projectile.localAI[0] * MathHelper.TwoPi / 30f);
            outlineColor *= Projectile.Opacity;
            outlineColor *= 0.75f + 0.25f * offset;
            for (int j = 0; j < 8; j++)
            {
                Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY) + Vector2.UnitY.RotatedBy(MathHelper.PiOver4 * j) * (4f + 1f * offset), sourceRectangle, outlineColor, Projectile.rotation, drawOrigin, Projectile.scale, dir);
            }

            Color drawColor = Projectile.GetAlpha(lightColor) with { A = 127 } * Projectile.Opacity;
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), sourceRectangle, drawColor, Projectile.rotation, drawOrigin, Projectile.scale, dir);
            return false;
        }
    }
}
