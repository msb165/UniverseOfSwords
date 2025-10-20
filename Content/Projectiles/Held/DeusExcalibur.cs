using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common.GlobalItems;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Held
{
    public class DeusExcalibur : ModProjectile
    {
        public enum SwordState : int
        {
            Swing_Left = 0,
            Swing_Right = 1,
            Thrust = 2
        }

        public SwordState CurrentState
        {
            get => (SwordState)Projectile.ai[1];
            set => Projectile.ai[1] = (float)value;
        }

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 120;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(16);
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 17;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 32;
            Projectile.noEnchantmentVisuals = true;
        }

        ref float Timer => ref Projectile.ai[0];
        Player Player => Main.player[Projectile.owner];
        public override void AI()
        {
            if (Timer <= 0f)
            {
                SoundEngine.PlaySound(SoundID.Item1, Projectile.position);
            }

            Timer++;
            switch (CurrentState)
            {
                case SwordState.Swing_Left:
                case SwordState.Swing_Right:
                    SwingSword();
                    break;
                case SwordState.Thrust:
                    Thrust();
                    break;
            }

            if (!Player.channel || Player.noItems || Player.CCed || Timer > 32f)
            {
                Player.reuseDelay = 2;
            }
            Lighting.AddLight(Projectile.Center, Color.White.ToVector3());
            if (Main.myPlayer == Projectile.owner)
            {
                foreach (Projectile proj in Main.ActiveProjectiles)
                {
                    if (proj.whoAmI != Projectile.whoAmI && Projectile.Colliding(Projectile.Hitbox, proj.Hitbox) && !proj.reflected && proj.hostile && Main.rand.Next(1, 100) <= Player.HeldItem.GetGlobalItem<ReflectionChance>().reflectChance)
                    {
                        SoundEngine.PlaySound(SoundID.Item150, Projectile.Center);
                        proj.velocity = -proj.oldVelocity;
                        proj.friendly = true;
                        proj.hostile = false;
                        proj.reflected = true;
                    }
                }
            }
            SetPlayerValues();
        }

        public void Thrust()
        {
            float fromValue = Timer / 10f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
            Projectile.Center = Player.Center + Vector2.Normalize(Projectile.velocity) * 2f * MathHelper.Lerp(-8f, 64f, MathF.Sin(fromValue));
            Projectile.scale = 2f - MathHelper.SmoothStep(1.25f, 0f, MathF.Sin(Projectile.ai[0] / 10f));
            Projectile.Opacity = 1f - MathHelper.SmoothStep(1f, 0f, MathF.Sin(Timer / 10f));
        }

        public void SwingSword()
        {
            float SwingDirection = Projectile.ai[1] == 0f ? 1f : -1f;
            if (Timer < 32f)
            {
                Projectile.Center = Player.RotatedRelativePoint(Player.Center);
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4 + MathHelper.SmoothStep(-2f * SwingDirection * Player.direction, 2f * SwingDirection * Player.direction, Timer * 0.03f);
                Projectile.scale = 1.5f - MathHelper.SmoothStep(0f, 1.25f, Timer * 0.0125f);
            }
        }

        public void SetPlayerValues()
        {
            Player.ChangeDir(Projectile.direction);
            Player.SetDummyItemTime(2);
            Player.heldProj = Projectile.whoAmI;
            Player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - MathHelper.PiOver2 - MathHelper.PiOver4);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target) && Projectile.CanHitWithMeleeWeapon(target) && Main.myPlayer == Projectile.owner)
            {
                int healAmount = (int)(damageDone * 0.075f);
                if (healAmount <= 0)
                {
                    healAmount = 1;
                }
                Player.Heal(healAmount);
                NPCLoader.OnHitByItem(target, Player, Player.HeldItem, hit, damageDone);

                if (CurrentState is SwordState.Thrust && Projectile.ai[2] == 0f)
                {
                    for (int i = 1; i <= 3; i++)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_OnHit(target), Projectile.Center + Projectile.velocity, Vector2.Normalize(target.Center - Player.Center).RotatedBy(-MathHelper.PiOver2) * 12f * (1f + i / 3), ModContent.ProjectileType<DeusExcaliburBeam>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                        Projectile.NewProjectile(Projectile.GetSource_OnHit(target), Projectile.Center + Projectile.velocity, Vector2.Normalize(target.Center - Player.Center).RotatedBy(MathHelper.PiOver2) * 12f * (1f + i / 3), ModContent.ProjectileType<DeusExcaliburBeam>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                    }
                    Projectile.ai[2] = 1f;
                }
            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float _ = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + (Projectile.rotation - MathHelper.PiOver4).ToRotationVector2() * 130f * Projectile.scale, 2f, ref _);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Texture2D backTexture = TextureAssets.Item[ModContent.ItemType<Items.Weapons.DeusExcalibur>()].Value;
            Vector2 origin = new(0f, texture.Height);
            SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.None;
            Color drawColor = Projectile.GetAlpha(lightColor);

            float offset = MathF.Cos((float)Main.timeForVisualEffects * MathHelper.TwoPi / 30f);
            Color outlineColor = Color.White with { A = 0 } * Projectile.Opacity * (0.75f + 0.25f * offset);
            Color trailColor = outlineColor;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.8f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, null, trailColor * 0.5f, Projectile.oldRot[i], origin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(backTexture, Projectile.Center - Main.screenPosition, null, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, Color.White with { A = 0 }, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);

            for (int j = 0; j < 8; j++)
            {
                Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY) + Vector2.UnitY.RotatedBy(MathHelper.PiOver4 * j) * (4f + 1f * offset), null, outlineColor * 0.15f, Projectile.rotation, origin, Projectile.scale, spriteEffects);
            }
            return false;
        }
    }
}
