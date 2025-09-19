using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;
using UniverseOfSwordsMod.Utilities.Projectiles;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class SkyProj : ModProjectile
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Projectile_{ProjectileID.SkyFracture}";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.alpha = 0;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            float projAmount = 20f;
            DelegateMethods.v3_1 = new Vector3(0.6f, 1f, 1f) * 0.2f;
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * 10f, 8f, DelegateMethods.CastLightOpen);
            Projectile.frame = (int)Projectile.ai[1];
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4;
            if (Projectile.ai[2] == 0f)
            {
                //SoundEngine.PlaySound(SoundID.Item9 with { Volume = 0.2f }, Projectile.Center);
                Projectile.ai[2] = 1f;
                Projectile.scale = 1.1f;
                for (int i = 0; i < projAmount; i++)
                {
                    Vector2 spinningpoint = -Vector2.UnitY.RotatedBy(i * (MathHelper.TwoPi / projAmount)) * new Vector2(3f, 6f);
                    spinningpoint = spinningpoint.RotatedBy(Projectile.velocity.ToRotation());
                    Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.Clentaminator_Cyan);
                    dust.scale = 1f;
                    dust.noGravity = true;
                    dust.position = Projectile.Center + spinningpoint;
                    dust.velocity = spinningpoint.SafeNormalize(Vector2.UnitY);
                }
            }

            if (Main.rand.NextBool(2))
            {
                Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.FireworksRGB, newColor: new Color(50, 155, 155));
                dust.scale = 1f;
                dust.alpha = Projectile.alpha;
                dust.noGravity = true;
                dust.position = Projectile.Center - Projectile.velocity / 10f;
                dust.velocity = -Projectile.oldVelocity.RotatedByRandom(MathHelper.ToRadians(45f)) * 0.1f;
            }

            Projectile.SimpleFadeOut(ai: 0, 1f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Weak, 200);
            target.AddBuff(BuffID.Slow, 200);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = -oldVelocity.SafeNormalize(Vector2.Zero) * 1.25f;
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Rectangle sourceRect = new(38 * Projectile.frame, 0, 38, 38);
            Vector2 origin = sourceRect.Size() / 2;
            Color drawColor = new Color(150, 255, 255, 0) * Projectile.Opacity;
            Color trailColor = drawColor;
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.75f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, sourceRect, trailColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            }
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, sourceRect, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
