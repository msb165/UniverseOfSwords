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
using UniverseOfSwordsMod.Content.Items.Weapons;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;
using static UniverseOfSwordsMod.Content.Projectiles.Held.DeusExcalibur;

namespace UniverseOfSwordsMod.Content.Projectiles.Held
{
    internal class PaladinSword : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<Items.Weapons.PaladinSword>().Texture;

        public enum AIState : int
        {
            Swing_Left = 0,
            Swing_Right = 1,
            Throw = 2,
            Retract = 3
        }

        public AIState CurrentState
        {
            get => (AIState)Projectile.ai[1];
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
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.ownerHitCheck = true;
            Projectile.timeLeft = 32;
            Projectile.extraUpdates = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.noEnchantmentVisuals = true;
        }

        public ref float Timer => ref Projectile.ai[0];
        Player Player => Main.player[Projectile.owner];
        public override void AI()
        {
            Timer++;
            if (Main.myPlayer == Projectile.owner && Main.mapFullscreen)
            {
                Projectile.Kill();
                return;
            }

            switch (CurrentState)
            {
                case AIState.Swing_Left:
                case AIState.Swing_Right:
                    Swing();
                    break;
                case AIState.Throw:
                    Throw();
                    break;
                case AIState.Retract:
                    Retract();
                    break;
            }

            if (!Player.channel || Player.noItems || Player.CCed || Timer > 32f)
            {
                Player.reuseDelay = 2;
            }

            SetPlayerValues();
        }

        public void SetPlayerValues()
        {
            Projectile.spriteDirection = Projectile.direction;
            //Player.ChangeDir(Projectile.direction);
            Player.SetDummyItemTime(2);
            Player.heldProj = Projectile.whoAmI;
            if (CurrentState is AIState.Throw or AIState.Retract)
            {
                return;
            }
            float rotation = Projectile.rotation - MathHelper.Pi - MathHelper.PiOver4;
            if (Player.direction == -1)
            {
                rotation += MathHelper.PiOver2;
            }
            Player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, rotation);
        }


        public void Swing()
        {
            float SwingDirection = Projectile.ai[1] == 0f ? 1f : -1f;
            //Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.One);
            if (Timer < 32f)
            {
                Projectile.Center = Player.RotatedRelativePoint(Player.Center);
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 + MathHelper.PiOver4 + MathHelper.SmoothStep(-2f * SwingDirection * Player.direction, 2f * SwingDirection * Player.direction, Timer * 0.03f);
                if (Player.direction == -1)
                {
                    Projectile.rotation -= MathHelper.PiOver2;
                }
                Projectile.scale = 1.5f - MathHelper.SmoothStep(0f, 1f, Timer * 0.0125f);
            }
        }

        public void Throw()
        {
            Projectile.scale = 1.35f;
            Projectile.timeLeft = 2;
            Timer++;
            Projectile.Center = Player.RotatedRelativePoint(Player.Center) + Projectile.velocity * Timer * 8f;
            Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver4;
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation -= MathHelper.PiOver2;
            }
            //Player.ChangeDir((Projectile.velocity.X > 0f).ToDirectionInt());
            if (Timer >= 24f || Projectile.Distance(Player.MountedCenter) >= 300f)
            {
                CurrentState = AIState.Retract;
                Projectile.netUpdate = true;
                Timer = 0f;
                return;
            }
        }

        public void Retract()
        {
            Projectile.scale = 1.35f;
            Projectile.timeLeft = 2;
            Vector2 targetPos = Projectile.DirectionTo(Player.MountedCenter).SafeNormalize(Vector2.Zero);
            if (Projectile.Distance(Player.MountedCenter) <= 16f)
            {
                Projectile.Kill();
                return;
            }
            Projectile.direction = Player.direction;
            Projectile.rotation += 0.3f * Projectile.direction;
            Projectile.velocity *= 0.98f;
            Projectile.velocity = Projectile.velocity.MoveTowards(targetPos * 5f, 5f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (CurrentState is AIState.Throw or AIState.Retract && Main.myPlayer == Projectile.owner && Projectile.ai[2] == 0f)
            {
                for (int i = 0; i < 10; i++)
                {
                    Vector2 newVel = (Vector2.UnitY * 10f).RotatedBy(-i * MathHelper.TwoPi / 10f);
                    Projectile.NewProjectile(target.GetSource_OnHit(target), target.Center - newVel * 15f, newVel, ModContent.ProjectileType<FlyingHammer>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                }
                Projectile.ai[2] = 1f;
            }
            else if (CurrentState is AIState.Swing_Left or AIState.Swing_Right && Main.myPlayer == Projectile.owner)
            {
                NPCLoader.OnHitByItem(target, Player, Player.HeldItem, hit, damageDone);                
            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float _ = 0f;
            float rotation = (Projectile.rotation - MathHelper.PiOver2 - MathHelper.PiOver4);
            if (Player.direction == -1)
            {
                rotation += MathHelper.PiOver2;
            }
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + rotation.ToRotationVector2() * 100f, 2f, ref _);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Vector2 drawOrigin = new(Projectile.spriteDirection == 1 ? texture.Width : 0f, texture.Height);
            if (CurrentState is AIState.Throw or AIState.Retract)
            {
                drawOrigin = texture.Size() / 2;

            }
            Color drawColor = Projectile.GetAlpha(lightColor);
            Color trailColor = drawColor with { A = 0 };


            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.75f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, null, trailColor, Projectile.oldRot[i], drawOrigin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, drawColor, Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
