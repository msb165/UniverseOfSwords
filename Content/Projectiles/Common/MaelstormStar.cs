using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class MaelstormStar : ModProjectile
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Extra_{ExtrasID.StardustJellyfishSmall}";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 2;
            //Projectile.alpha = 255;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.extraUpdates = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
        }

        Player Player => Main.player[Projectile.owner];
        public override void AI()
        {
            Lighting.AddLight(Projectile.position, Color.LightCyan.ToVector3());
            Projectile.tileCollide = Projectile.Center.Y > Player.Center.Y;
            if (Projectile.soundDelay == 0)
            {
                Projectile.soundDelay = 20 + Main.rand.Next(40);
                SoundEngine.PlaySound(SoundID.Item9 with { Volume = 0.125f }, Projectile.position);
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (Main.rand.NextBool(16))
            {
                Vector2 dustVel = Vector2.UnitX.RotatedByRandom(MathHelper.PiOver2).RotatedBy(Projectile.velocity.ToRotation());
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.FireworkFountain_Blue, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 150, default, 1.2f);
                dust.noGravity = true;
                dust.velocity = dustVel * 0.66f;
                dust.position = Projectile.Center + dustVel * 12f;
            }
            if (Main.rand.NextBool(48))
            {
                Gore gore1 = Gore.NewGoreDirect(null, Projectile.Center, Projectile.velocity * 0.2f, 16);
                Gore gore2 = gore1;
                gore2.velocity *= 0.66f;
                gore2 = gore1;
                gore2.velocity += Projectile.velocity * 0.3f;
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10 with { Volume = 0.5f }, Projectile.position);
            Projectile.Resize(48, 48);
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.FireworkFountain_Blue, Alpha: 150, Scale: 0.9f);
                dust.noGravity = true;
                dust.velocity *= 2f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Main.instance.LoadProjectile(ProjectileID.StardustTowerMark);
            Texture2D textureGlow = TextureAssets.Projectile[ProjectileID.StardustTowerMark].Value;
            Texture2D textureStar = TextureAssets.Projectile[ProjectileID.SuperStar].Value;

            Vector2 origin = new(texture.Width / 2, 0f);
            Vector2 originStar = new(textureStar.Width / 2, textureStar.Height / 2 - texture.Height / 7);
            Vector2 originGlow = new(textureGlow.Width / 2, textureGlow.Height / 2 - texture.Height / 7);
            Color drawColor = Projectile.GetAlpha(lightColor) with { A = 0 } * 0.2f * Projectile.Opacity;
            Color trailColor = Color.Cyan with { A = 0 } * Projectile.Opacity;

            float timer = (float)Main.timeForVisualEffects / 60f;

            for (int j = 0; j < Projectile.oldPos.Length; j++)
            {
                for (float i = 0f; i < 1f; i += 0.5f)
                {
                    float scaleTimer = timer % 0.5f / 0.5f;
                    scaleTimer = (scaleTimer + i) % 1f;
                    float doubleScale = scaleTimer * 2f;
                    if (doubleScale > 1f)
                    {
                        doubleScale = 2f - doubleScale;
                    }
                    Main.spriteBatch.Draw(texture, Projectile.oldPos[j] + Projectile.Size / 2 - Main.screenPosition, null, drawColor * doubleScale * (1f - j * 0.075f), Projectile.rotation, origin, 0.3f + scaleTimer * 0.5f, SpriteEffects.None, 0f);
                }
                trailColor *= 0.75f;
                Main.spriteBatch.Draw(textureGlow, Projectile.oldPos[j] + Projectile.Size / 2 - Main.screenPosition, null, trailColor * 0.1f, Projectile.rotation, originGlow, MathHelper.SmoothStep(Projectile.scale * 0.75f, 0f, j * 0.025f), SpriteEffects.None, 0f);
            }

            // Spinning part
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition - (Vector2.UnitY * 5f).RotatedBy(MathHelper.TwoPi * timer), null, drawColor, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition - (Vector2.UnitY * 5f).RotatedBy(MathHelper.TwoPi * timer + MathHelper.TwoPi / 3f), null, drawColor, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition - (Vector2.UnitY * 5f).RotatedBy(MathHelper.TwoPi * timer + MathHelper.Pi + MathHelper.PiOver2), null, drawColor, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0f);

            Main.spriteBatch.Draw(textureStar, Projectile.Center - Main.screenPosition, null, drawColor, Projectile.rotation, originStar, MathHelper.Clamp(Projectile.scale - 0.1f, 0f, Projectile.scale), SpriteEffects.None, 0f);

            return false;
        }
    }
}
