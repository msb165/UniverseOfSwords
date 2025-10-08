using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Buffs;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class TrueHorrormageddon : ModProjectile
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Projectile_{ProjectileID.NightsEdge}";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(30);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.ignoreWater = true;
            Projectile.scale = 2f;
            Projectile.localNPCHitCooldown = 10;
            Projectile.alpha = 255;
            Projectile.timeLeft = 90;
            Projectile.tileCollide = false;
        }

        Player Player => Main.player[Projectile.owner];

        public override void AI()
        {
            float num = 50f;
            float num2 = 15f;
            float num3 = Projectile.ai[1] + num;
            float maxTime = num3 + num2;
            float num5 = 77f;
            if (Projectile.localAI[0] == 0f)
            {
                SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
            }
            Projectile.localAI[0] += 1f;
            if (Projectile.damage == 0 && Projectile.localAI[0] < MathHelper.Lerp(num3, maxTime, 0.5f))
            {
                Projectile.localAI[0] += 6f;
            }
            Projectile.Opacity = Utils.Remap(Projectile.localAI[0], 0f, Projectile.ai[1], 0f, 1f) * Utils.Remap(Projectile.localAI[0], num3, maxTime, 1f, 0f);
            if (Projectile.localAI[0] >= maxTime)
            {
                Projectile.localAI[1] = 1f;
                Projectile.Kill();
                return;
            }
            float num6 = Utils.Remap(Projectile.localAI[0], Projectile.ai[1] * 0.4f, maxTime, 0f, 1f);
            Projectile.direction = (Projectile.spriteDirection = (int)Projectile.ai[0]);
            int num7 = 3;
            if (Projectile.damage != 0 && Projectile.localAI[0] >= num5 + num7)
            {
                Projectile.damage = 0;
            }
            Projectile.localAI[1]++;
            num6 = Utils.Remap(Projectile.localAI[1], Projectile.ai[1] * 0.4f, maxTime, 0f, 1f);
            Projectile.Center = Player.RotatedRelativePoint(Player.MountedCenter) - Projectile.velocity + Projectile.velocity * num6 * num6 * num5;
            Projectile.rotation += Projectile.ai[0] * MathHelper.TwoPi * (4f + Projectile.Opacity * 4f) / 90f;
            Projectile.scale = Utils.Remap(Projectile.localAI[0], Projectile.ai[1] + 2f, maxTime, 1.12f, 1f) * Projectile.ai[2];
            float f = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 0.7f;
            Vector2 spawnPos = Projectile.Center + f.ToRotationVector2() * 84f * Projectile.scale;
            if (Main.rand.NextBool(5))
            {
                Dust dust = Dust.NewDustPerfect(spawnPos, 14, null, 150, default, 1.4f);
                dust.noLight = dust.noLightEmittence = true;
            }
            for (int i = 0; i < 3f * Projectile.Opacity; i++)
            {
                Vector2 velocity = Projectile.velocity.SafeNormalize(Vector2.UnitX);
                Dust dust2 = Dust.NewDustPerfect(spawnPos, DustID.CursedTorch, Projectile.velocity * 0.2f + velocity * 3f, 100, default, 1.4f);
                dust2.noGravity = true;
                dust2.customData = Projectile.Opacity * 0.2f;
            }
            Lighting.AddLight(Projectile.Center, Color.Green.ToVector3());
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target))
            {
                Vector2 worldPos = Main.rand.NextVector2FromRectangle(target.Hitbox);
                ParticleOrchestraSettings orchestraSettings = default;
                orchestraSettings.PositionInWorld = worldPos;
                ParticleOrchestraSettings settings = orchestraSettings;
                ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.TrueNightsEdge, settings, Projectile.owner);
            }
            target.AddBuff(BuffID.CursedInferno, 300);
            target.AddBuff(ModContent.BuffType<TrueSlow>(), 300);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Vector2 v = targetHitbox.ClosestPointInRect(Projectile.Center) - Projectile.Center;
            v.SafeNormalize(Vector2.UnitX);
            float size = 100f * Projectile.scale;
            return v.Length() < size && Collision.CanHit(Projectile.Center, 0, 0, targetHitbox.Center.ToVector2(), 0, 0);
        }

        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);
        }


        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D val = TextureAssets.Projectile[Type].Value;
            Rectangle rectangle = val.Frame(1, 4);
            Vector2 origin = rectangle.Size() / 2f;
            float scale = Projectile.scale * 1.1f;
            SpriteEffects effects = (!(Projectile.ai[0] >= 0f)) ? SpriteEffects.FlipVertically : SpriteEffects.None;
            float num2 = 0.975f;
            float fromValue = Lighting.GetColor(Projectile.Center.ToTileCoordinates()).ToVector3().Length() / (float)Math.Sqrt(3.0);
            fromValue = Utils.Remap(fromValue, 0.2f, 1f, 0f, 1f);
            float num3 = MathHelper.Min(0.15f + fromValue * 0.85f, Utils.Remap(Projectile.localAI[0], 30f, 96f, 1f, 0f));
            for (float i = 2f; i >= 0f; i -= 1f)
            {
                if (!(Projectile.oldPos[(int)i] == Vector2.Zero))
                {
                    Vector2 vector = Projectile.Center - Projectile.velocity * 0.5f * i;
                    float num6 = Projectile.oldRot[(int)i] + Projectile.ai[0] * MathHelper.TwoPi * 0.1f * (0f - i);
                    Vector2 position = vector - Main.screenPosition;
                    float num7 = 1f - i / 2f;
                    float num8 = Projectile.Opacity * num7 * num7 * 0.85f;
                    float amount = Projectile.Opacity * Projectile.Opacity;
                    Color color = Color.Lerp(new Color(40, 60, 20, 120), new Color(80, 160, 50, 120), amount);
                    Main.spriteBatch.Draw(val, position, rectangle, color * num3 * num8, num6 + Projectile.ai[0] * MathHelper.PiOver4 * -1f, origin, scale * num2, effects, 0f);
                    Color color2 = Color.Lerp(new Color(80, 180, 40), new Color(155, 255, 100), amount);
                    Color color3 = Color.White * num8 * 0.5f;
                    color3.A = (byte)(color3.A * (1f - num3));
                    Color color4 = color3 * num3 * 0.5f;
                    color4.G = (byte)(color4.G * num3);
                    color4.R = (byte)(color4.R * (0.25f + num3 * 0.75f));
                    float num9 = 3f;
                    for (float j = -MathHelper.TwoPi + MathHelper.TwoPi / num9; j < 0f; j += MathHelper.TwoPi / num9)
                    {
                        float num11 = Utils.Remap(j, -MathHelper.TwoPi, 0f, 0f, 0.5f);
                        Main.spriteBatch.Draw(val, position, rectangle, color4 * 0.15f * num11, num6 + Projectile.ai[0] * 0.01f + j, origin, scale, effects, 0f);
                        Main.spriteBatch.Draw(val, position, rectangle, Color.Lerp(new Color(80, 160, 30), new Color(70, 255, 0), amount) * fromValue * num8 * num11, num6 + j, origin, scale * 0.8f, effects, 0f);
                        Main.spriteBatch.Draw(val, position, rectangle, color2 * fromValue * num8 * MathHelper.Lerp(0.05f, 0.4f, fromValue) * num11, num6 + j, origin, scale * num2, effects, 0f);
                        Main.spriteBatch.Draw(val, position, val.Frame(1, 4, 0, 3), Color.White * MathHelper.Lerp(0.05f, 0.5f, fromValue) * num8 * num11, num6 + j, origin, scale, effects, 0f);
                    }
                    Main.spriteBatch.Draw(val, position, rectangle, color4 * 0.15f, num6 + Projectile.ai[0] * 0.01f, origin, scale, effects, 0f);
                    Main.spriteBatch.Draw(val, position, rectangle, Color.Lerp(new Color(80, 160, 30), new Color(70, 255, 0), amount) * num3 * num8, num6, origin, scale * 0.8f, effects, 0f);
                    Main.spriteBatch.Draw(val, position, rectangle, color2 * fromValue * num8 * MathHelper.Lerp(0.05f, 0.4f, num3), num6, origin, scale * num2, effects, 0f);
                    Main.spriteBatch.Draw(val, position, val.Frame(1, 4, 0, 3), Color.White * MathHelper.Lerp(0.05f, 0.5f, num3) * num8, num6, origin, scale, effects, 0f);
                }
            }
            float num12 = 1f - Projectile.localAI[0] * 1f / 80f;
            if (num12 < 0.5f)
            {
                num12 = 0.5f;
            }
            Vector2 drawPos = Projectile.Center - Main.screenPosition + (Projectile.rotation + 0.48f * Projectile.ai[0]).ToRotationVector2() * (val.Width * 0.5f - 4f) * scale * num12;
            float num13 = MathHelper.Min(num3, MathHelper.Lerp(1f, fromValue, Utils.Remap(Projectile.localAI[0], 0f, 80f, 0f, 1f)));
            UniverseUtils.Drawing.DrawPrettyStarSparkle(Projectile.Opacity, SpriteEffects.None, drawPos, new Color(255, 255, 255, 0) * Projectile.Opacity * 0.5f * num13, new Color(150, 255, 100) * num13, Projectile.Opacity, 0f, 1f, 1f, 2f, (float)Math.PI / 4f, new Vector2(2f, 2f), Vector2.One);
            return false;
        }
    }
}
