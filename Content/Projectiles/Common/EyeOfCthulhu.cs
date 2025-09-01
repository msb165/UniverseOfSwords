using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Weapons;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class EyeOfCthulhu : ModProjectile
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Projectile_{ProjectileID.Spazmamini}";
        public enum AIState : int
        {
            Idle = 0,
            Dash = 1,
            SlowDown = 2
        }

        public AIState CurrentState
        {
            get => (AIState)Projectile.ai[0];
            set => Projectile.ai[0] = (float)value;
        }

        public ref float TargetIndexTimer => ref Projectile.ai[1];

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 3;
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.TrailingMode[Type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 20;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 12;
            Projectile.extraUpdates = 1;
        }

        Player Player => Main.player[Projectile.owner];
        public override void AI()
        {
            if (Main.myPlayer == Projectile.owner && Player.HeldItem.type != ModContent.ItemType<CthulhuJudge>())
            {
                Projectile.Kill();
            }
            Projectile.timeLeft = 2;
            Projectile.spriteDirection = Projectile.direction;
            FindFrame();
            switch (CurrentState)
            {
                case AIState.Idle:
                    Idle();
                    break;
                case AIState.Dash:
                    Dash();
                    break;
                case AIState.SlowDown:
                    SlowDown();
                    break;
            }
        }

        public void FindFrame()
        {
            if ((Projectile.frameCounter++) / 2 >= Main.projFrames[Type])
            {
                Projectile.frameCounter = 0; 
                if (++Projectile.frame >= Main.projFrames[Type])
                {
                    Projectile.frame = 0; 
                }
            }
        }

        public void Idle()
        {
            Vector2 targetSpeed = Player.Center - Projectile.Center - Vector2.UnitY * 60f;
            float distance = targetSpeed.Length();
            float acceleration = 6f;
            float innertia = 40f;
            if (acceleration < Player.velocity.Length())
            {
                acceleration = Player.velocity.Length();
            }
            if (distance > 70f)
            {
                Vector2 newVel = Vector2.Normalize(targetSpeed) * acceleration;
                Projectile.velocity = (Projectile.velocity * innertia + newVel) / (innertia + 1f);
            }
            if (distance > 500f)
            {
                Projectile.position = Player.Center;
            }
            if (Projectile.velocity.Length() > acceleration)
            {
                Projectile.velocity *= 0.95f;
            }
            if (Projectile.velocity.Length() == 0f)
            {
                Projectile.velocity = Projectile.velocity.SafeNormalize(-Vector2.UnitY) * new Vector2(-0.98f, 1.1f);
            }
            Projectile.rotation = Utils.AngleLerp(Projectile.rotation, Projectile.velocity.ToRotation() + MathHelper.Pi, 0.15f);
            int startAttackRange = 200;
            int attackTarget = -1;
            Projectile.Minion_FindTargetInRange(startAttackRange, ref attackTarget, skipIfCannotHitWithOwnBody: false);
            if (attackTarget != -1)
            {
                CurrentState = AIState.Dash;
                TargetIndexTimer = attackTarget;
                Projectile.netUpdate = true;
            }
        }

        public void Dash()
        {
            NPC npc = null;
            int npcIndex = (int)TargetIndexTimer;
            if (Main.npc.IndexInRange(npcIndex) && Main.npc[npcIndex].CanBeChasedBy(this))
            {
                npc = Main.npc[npcIndex];
            }
            if (npc == null || (npc != null && Player.Distance(npc.Center) > 300f))
            {
                CurrentState = AIState.Idle;
                TargetIndexTimer = 0f;
                Projectile.netUpdate = true;
                return;
            }
            Vector2 targetVel = (npc.Center - Projectile.Center).RotatedByRandom(MathHelper.ToRadians(15f));
            Projectile.rotation = targetVel.ToRotation() + MathHelper.Pi;
            Projectile.velocity = targetVel.SafeNormalize(Vector2.UnitY) * 14f;
            CurrentState = AIState.SlowDown;
            Projectile.netUpdate = true;
        }

        public void SlowDown()
        {
            Projectile.velocity *= 0.98f;
            TargetIndexTimer++;
            if (TargetIndexTimer >= 30f)
            {
                CurrentState = AIState.Idle;
                TargetIndexTimer = 0f;
                Projectile.netUpdate = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipVertically;
            int frameHeight = texture.Height / Main.projFrames[Type];
            int startY = frameHeight * Projectile.frame;
            Rectangle sourceRectangle = new(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2;
            Color drawColor = Projectile.GetAlpha(lightColor);
            Color trailColor = Color.Red;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor = CurrentState is AIState.Dash or AIState.SlowDown ? trailColor * 0.75f : trailColor * 0.4f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, sourceRectangle, trailColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            }

            float timer = MathF.Cos((float)Main.timeForVisualEffects * MathHelper.TwoPi / 30f);

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, sourceRectangle, Color.Red with { A = 0 } * 0.5f * Projectile.Opacity, Projectile.rotation, origin, Projectile.scale * 1.25f, spriteEffects, 0f);
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
