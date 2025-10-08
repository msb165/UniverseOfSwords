using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class BlackBolt : ModProjectile
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Projectile_{ProjectileID.BlackBolt}";

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.timeLeft = 40;
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 8;
            Projectile.noEnchantmentVisuals = true;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (Projectile.alpha <= 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Granite);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 0.3f;
                    Main.dust[dust].noLight = true;
                }
            }
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 55;
                Projectile.scale = 1.3f;
                if (Projectile.alpha < 0)
                {
                    Projectile.alpha = 0;
                    float amount = 16f;
                    for (int i = 0; i < amount; i++)
                    {
                        Vector2 spinningpoint = -Vector2.UnitY.RotatedBy(i * (MathHelper.TwoPi / amount)) * new Vector2(1f, 4f);
                        spinningpoint = spinningpoint.RotatedBy(Projectile.velocity.ToRotation());
                        Dust dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.PurpleTorch);
                        dust.scale = 1.5f;
                        dust.noLight = true;
                        dust.noGravity = true;
                        dust.position = Projectile.Center + spinningpoint;
                        dust.velocity = dust.velocity * 4f + Projectile.velocity * 0.3f;
                    }
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.ShadowFlame, 300);
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.Resize(160, 160);
            Projectile.maxPenetrate = -1;
            Projectile.penetrate = -1;
            Projectile.Damage();
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            Vector2 spawnPos = Projectile.Center + Vector2.One * -20f;
            int width = 40, height = width;
            for (int i = 0; i < 4; i++)
            {
                int dust = Dust.NewDust(spawnPos, width, height, DustID.Granite, 0f, 0f, 100, Scale: 1.5f);
                Main.dust[dust].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(MathHelper.Pi) * (float)Main.rand.NextDouble() * width / 2f;
            }
            for (int j = 0; j < 20; j++)
            {
                Dust dust3 = Dust.NewDustDirect(spawnPos, width, height, DustID.PurpleTorch, 0f, 0f, 200, Scale: 3.7f);
                dust3.position = Projectile.Center + Vector2.UnitY.RotatedByRandom(MathHelper.Pi) * (float)Main.rand.NextDouble() * width / 2f;
                dust3.noGravity = true;
                dust3.noLight = true;
                Dust dust2 = dust3;
                dust2.velocity *= 3f;
                dust2 = dust3;
                dust2.velocity += Projectile.DirectionTo(dust3.position) * (2f + Main.rand.NextFloat() * 4f);
                dust3 = Dust.NewDustDirect(spawnPos, width, height, DustID.PurpleTorch, 0f, 0f, 100, Scale: 1.5f);
                dust3.position = Projectile.Center + Vector2.UnitY.RotatedByRandom(MathHelper.Pi) * (float)Main.rand.NextDouble() * width / 2f;
                dust2 = dust3;
                dust2.velocity *= 2f;
                dust3.noGravity = true;
                dust3.fadeIn = 1f;
                dust3.color = Color.Crimson * 0.5f;
                dust3.noLight = true;
                dust2 = dust3;
                dust2.velocity += Projectile.DirectionTo(dust3.position) * 8f;
            }
            for (int k = 0; k < 20; k++)
            {
                Dust dust4 = Dust.NewDustDirect(spawnPos, width, height, DustID.PurpleTorch, 0f, 0f, 0, Scale: 2.7f);
                dust4.position = Projectile.Center + Vector2.UnitX.RotatedByRandom(MathHelper.Pi).RotatedBy(Projectile.velocity.ToRotation()) * width / 2f;
                dust4.noGravity = true;
                dust4.noLight = true;
                Dust dust2 = dust4;
                dust2.velocity *= 3f;
                dust2 = dust4;
                dust2.velocity += Projectile.DirectionTo(dust4.position) * 2f;
            }
            for (int l = 0; l < 70; l++)
            {
                Dust dust5 = Dust.NewDustDirect(spawnPos, width, height, DustID.Granite, 0f, 0f, 0, Scale: 1.5f);
                dust5.position = Projectile.Center + Vector2.UnitX.RotatedByRandom(MathHelper.Pi).RotatedBy(Projectile.velocity.ToRotation()) * width / 2f;
                dust5.noGravity = true;
                Dust dust2 = dust5;
                dust2.velocity *= 3f;
                dust2 = dust5;
                dust2.velocity += Projectile.DirectionTo(dust5.position) * 3f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Color outColor = new Color(120, 40, 222, 120) * Projectile.Opacity;
            Texture2D outlineTexture = TextureAssets.Extra[ExtrasID.BlackBolt].Value;
            Vector2 origin = outlineTexture.Size() / 2;
            float timer = MathF.Cos((float)Main.timeForVisualEffects / 30f);
            for (int i = 0; i < 4; i++)
            {
                Main.spriteBatch.Draw(outlineTexture, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY) + Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * (4f + 1f * timer), null, outColor, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0);
            }

            UniverseUtils.Drawing.DrawWithAfterImages(Projectile, Color.White * Projectile.Opacity);
            return false;
        }
    }
}
