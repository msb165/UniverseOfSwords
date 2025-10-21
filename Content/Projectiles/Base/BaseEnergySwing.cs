using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common.GlobalItems;
using UniverseOfSwords.Utilities;
using static UniverseOfSwords.Utilities.UniverseUtils.Drawing;

namespace UniverseOfSwords.Content.Projectiles.Base
{
    public class BaseEnergySwing : ModProjectile
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Projectile_{ProjectileID.Excalibur}";

        public override void SetDefaults()
        {
            Projectile.Size = new(16);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 3;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.ownerHitCheck = true;
            Projectile.ownerHitCheckDistance = 300f;
            Projectile.usesOwnerMeleeHitCD = true;
            Projectile.stopsDealingDamageAfterPenetrateHits = true;
            Projectile.noEnchantmentVisuals = true;
        }

        public Player Player => Main.player[Projectile.owner];
        public virtual float ScaleAdd => 0.6f;
        public virtual float BaseScale => 1f;
        public virtual Color DustColorFrom => Color.Gold;
        public virtual Color DustColorTo => Color.White;

        public override void AI()
        {
            Projectile.localAI[0]++;
            float num = Projectile.localAI[0] / Projectile.ai[1];
            float num2 = Projectile.ai[0];
            float num3 = Projectile.velocity.ToRotation();
            float rotation = MathHelper.Pi * num2 * num + num3 + num2 * MathHelper.Pi + Player.fullRotation;

            Lighting.AddLight(Projectile.Center, DustColorFrom.ToVector3());
            Projectile.rotation = rotation;
            Projectile.Center = Player.RotatedRelativePoint(Player.MountedCenter) - Projectile.velocity;
            Projectile.scale = BaseScale + num * ScaleAdd;
            float randomRotation = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 0.7f;
            Vector2 vector2 = Projectile.Center + randomRotation.ToRotationVector2() * 84f * Projectile.scale * Player.HeldItem.scale;
            Vector2 dustVel = (randomRotation + Projectile.ai[0] * MathHelper.PiOver2).ToRotationVector2();
            if (Main.rand.NextFloat() * 2f < Projectile.Opacity)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.Center + randomRotation.ToRotationVector2() * (Main.rand.NextFloat() * 80f * Projectile.scale + 20f * Projectile.scale), 278, dustVel * 1f, 100, Color.Lerp(DustColorFrom, DustColorTo, Main.rand.NextFloat() * 0.3f), 0.4f);
                dust.fadeIn = 0.4f + Main.rand.NextFloat() * 0.15f;
                dust.noGravity = true;
            }
            if (Main.rand.NextFloat() * 1.5f < Projectile.Opacity)
            {
                Dust.NewDustPerfect(vector2, 43, dustVel * 1f, 100, DustColorTo * Projectile.Opacity, 1.25f * Projectile.Opacity);
            }
            Projectile.scale *= Projectile.ai[2];
            if (Projectile.localAI[0] >= Projectile.ai[1])
            {
                Projectile.Kill();
            }

            for (float i = -MathHelper.PiOver4; i <= MathHelper.PiOver4; i += MathHelper.PiOver2)
            {
                Rectangle r = Utils.CenteredRectangle(Projectile.Center + (Projectile.rotation + i).ToRotationVector2() * 70f * Projectile.scale, new Vector2(60f * Projectile.scale, 60f * Projectile.scale));
                Projectile.EmitEnchantmentVisualsAt(r.TopLeft(), r.Width, r.Height);
            }
            Player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - MathHelper.PiOver2 + 0.2f);
            if (Main.myPlayer == Projectile.owner)
            {
                foreach (Projectile proj in Main.ActiveProjectiles)
                {
                    if (proj.whoAmI != Projectile.whoAmI && Projectile.Colliding(Projectile.Hitbox, proj.Hitbox) && !proj.reflected && proj.hostile && Main.rand.Next(1, 100) <= Player.HeldItem.GetGlobalItem<ReflectionChance>().reflectChance && Player.ItemAnimationJustStarted)
                    {
                        SoundEngine.PlaySound(SoundID.Item150, Projectile.Center);
                        proj.velocity = -proj.oldVelocity;
                        proj.friendly = true;
                        proj.hostile = false;
                        proj.reflected = true;
                    }
                }
            }
        }

        public override void CutTiles()
        {
            Vector2 start = (Projectile.rotation - MathHelper.PiOver4).ToRotationVector2() * 60f * Projectile.scale;
            Vector2 end = (Projectile.rotation + MathHelper.PiOver4).ToRotationVector2() * 60f * Projectile.scale;
            float width = 60f * Projectile.scale;
            Utils.PlotTileLine(Projectile.Center + start, Projectile.Center + end, width, DelegateMethods.CutTiles);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float coneLength = 94f * Projectile.scale;
            float offset = MathHelper.TwoPi / 25f * Projectile.ai[0];
            float maximumAngle = MathHelper.PiOver4;
            float rotation = Projectile.rotation + offset;
            if (targetHitbox.IntersectsConeSlowMoreAccurate(Projectile.Center, coneLength, rotation, maximumAngle))
            {
                return true;
            }
            float swingBack = Utils.Remap(Projectile.localAI[0], Projectile.ai[1] * 0.3f, Projectile.ai[1] * 0.5f, 1f, 0f);
            if (swingBack > 0f)
            {
                float coneRotation = rotation - MathHelper.PiOver4 * Projectile.ai[0] * swingBack;
                if (targetHitbox.IntersectsConeSlowMoreAccurate(Projectile.Center, coneLength, coneRotation, maximumAngle))
                {
                    return true;
                }
            }
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target) && Projectile.CanHitWithMeleeWeapon(target) && Main.myPlayer == Projectile.owner)
            {
                NPCLoader.OnHitByItem(target, Player, Player.HeldItem, hit, damageDone);
            }
        }       

        public virtual Color BackColor => new(180, 160, 60);
        public virtual Color MiddleColor => new(255, 240, 150);
        public virtual Color FrontColor => new(255, 255, 80);


        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawPosition = Projectile.Center - Main.screenPosition;
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Rectangle rectangle = texture.Frame(1, 4);
            Vector2 drawOrigin = rectangle.Size() / 2f;
            float scale = Projectile.scale * 1.1f;
            SpriteEffects effects = !(Projectile.ai[0] >= 0f) ? SpriteEffects.FlipVertically : SpriteEffects.None;
            float projTime = Projectile.localAI[0] / Projectile.ai[1];
            float num3 = Utils.Remap(projTime, 0f, 0.6f, 0f, 1f) * Utils.Remap(projTime, 0.6f, 1f, 1f, 0f);
            float fromValue = Lighting.GetColor(Projectile.Center.ToTileCoordinates()).ToVector3().Length() / MathF.Sqrt(3f);
            fromValue = Utils.Remap(fromValue, 0.2f, 1f, 0f, 1f);

            Main.spriteBatch.Draw(texture, drawPosition, rectangle, BackColor * fromValue * num3, Projectile.rotation + Projectile.ai[0] * MathHelper.PiOver4 * -1f * (1f - projTime), drawOrigin, scale, effects, 0f);
            Color color4 = Color.White * num3 * 0.5f;
            color4.A = (byte)(color4.A * (1f - fromValue));
            Color color5 = color4 * fromValue * 0.5f;
            color5.G = (byte)(color5.G * fromValue);
            color5.B = (byte)(color5.R * (0.25f + fromValue * 0.75f));

            Main.spriteBatch.Draw(texture, drawPosition, rectangle, color5 * 0.15f, Projectile.rotation + Projectile.ai[0] * 0.01f, drawOrigin, scale, effects, 0f);
            Main.spriteBatch.Draw(texture, drawPosition, rectangle, FrontColor * fromValue * num3 * 0.3f, Projectile.rotation, drawOrigin, scale, effects, 0f);
            Main.spriteBatch.Draw(texture, drawPosition, rectangle, MiddleColor * fromValue * num3 * 0.5f, Projectile.rotation, drawOrigin, scale * 0.975f, effects, 0f);
            Main.spriteBatch.Draw(texture, drawPosition, texture.Frame(1, 4, 0, 3), Color.White * 0.6f * num3, Projectile.rotation + Projectile.ai[0] * 0.01f, drawOrigin, scale, effects, 0f);
            Main.spriteBatch.Draw(texture, drawPosition, texture.Frame(1, 4, 0, 3), Color.White * 0.5f * num3, Projectile.rotation + Projectile.ai[0] * -0.05f, drawOrigin, scale * 0.8f, effects, 0f);
            Main.spriteBatch.Draw(texture, drawPosition, texture.Frame(1, 4, 0, 3), Color.White * 0.4f * num3, Projectile.rotation + Projectile.ai[0] * -0.1f, drawOrigin, scale * 0.6f, effects, 0f);

            for (float i = 0f; i < 8f; i += 1f)
            {
                float rotation = Projectile.rotation + Projectile.ai[0] * i * -MathHelper.TwoPi * 0.025f + Utils.Remap(projTime, 0f, 1f, 0f, MathHelper.PiOver4) * Projectile.ai[0];
                Vector2 drawPos = drawPosition + rotation.ToRotationVector2() * (texture.Width / 2 - 6f) * scale;
                DrawPrettyStarSparkle(Projectile.Opacity, SpriteEffects.None, drawPos, Color.White with { A = 0 } * num3 * (i / 9f), FrontColor, projTime, 0f, 0.5f, 0.5f, 1f, rotation, new Vector2(0f, Utils.Remap(projTime, 0f, 1f, 3f, 0f)) * scale, Vector2.One * scale);
            }
            Vector2 drawPos2 = drawPosition + (Projectile.rotation + Utils.Remap(projTime, 0f, 1f, 0f, MathHelper.PiOver4) * Projectile.ai[0]).ToRotationVector2() * (texture.Width * 0.5f - 4f) * scale;
            DrawPrettyStarSparkle(Projectile.Opacity, SpriteEffects.None, drawPos2, Color.White with { A = 0 } * num3 * 0.5f, FrontColor, projTime, 0f, 0.5f, 0.5f, 1f, 0f, new Vector2(2f, Utils.Remap(projTime, 0f, 1f, 4f, 1f)) * scale, Vector2.One * scale);

            return false;
        }
    }
}
