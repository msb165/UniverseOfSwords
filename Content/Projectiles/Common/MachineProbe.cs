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
using UniverseOfSwordsMod.Utilities;
using UniverseOfSwordsMod.Utilities.Projectiles;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class MachineProbe : ModProjectile
    {
        public enum AIState : int
        {
            Idle = 0,
            Dash = 1,
        }

        public AIState CurrentState
        {
            get => (AIState)Projectile.ai[0];
            set => Projectile.ai[0] = (float)value;
        }

        public ref float TargetIndex => ref Projectile.ai[1];
        public ref float Timer => ref Projectile.ai[2];

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(20);
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 150;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
            Projectile.extraUpdates = 1;
        }

        Player Player => Main.player[Projectile.owner];
        public override bool? CanDamage() => false;

        public override void AI()
        {
            if (Main.myPlayer == Projectile.owner && Player.HeldItem.type != ModContent.ItemType<UltraMachine>())
            {
                Projectile.Kill();
            }
            Projectile.spriteDirection = Projectile.direction;
            switch (CurrentState)
            {
                case AIState.Idle:
                    Idle();
                    break;
                case AIState.Dash:
                    ShootTarget();
                    break;
            }
            Projectile.Opacity = Projectile.timeLeft / 32f;
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
            if (Projectile.velocity.Length() > acceleration)
            {
                Projectile.velocity *= 0.95f;
            }
            if (Projectile.velocity.Length() == 0f)
            {
                Projectile.velocity = Projectile.velocity.SafeNormalize(-Vector2.UnitY) * new Vector2(0.98f, -1.1f);
            }
            Projectile.rotation = Utils.AngleLerp(Projectile.rotation, Projectile.velocity.ToRotation() + MathHelper.PiOver2, 0.15f);
            int startAttackRange = 500;
            int attackTarget = -1;
            Projectile.Minion_FindTargetInRange(startAttackRange, ref attackTarget, skipIfCannotHitWithOwnBody: false);
            if (attackTarget != -1)
            {
                CurrentState = AIState.Dash;
                TargetIndex = attackTarget;
                Projectile.netUpdate = true;
            }
        }

        public void ShootTarget()
        {
            NPC npc = null;
            int npcIndex = (int)TargetIndex;
            if (Main.npc.IndexInRange(npcIndex) && Main.npc[npcIndex].CanBeChasedBy(this))
            {
                npc = Main.npc[npcIndex];
            }
            if (npc == null || (npc != null && Player.Distance(npc.Center) > 300f))
            {
                CurrentState = AIState.Idle;
                TargetIndex = 0f;
                Timer = 0f;
                Projectile.netUpdate = true;
                return;
            }
            Timer++;
            Vector2 targetVel = Vector2.Normalize(npc.Center - Vector2.UnitY * npc.height * 2f - Projectile.Center) * 8f;
            Vector2 targetPos = npc.Center - Projectile.Center;
            Vector2 spawnVel = targetPos.SafeNormalize(Vector2.Zero) * 10f;
            Projectile.rotation = targetPos.ToRotation() + MathHelper.PiOver2;
            Projectile.velocity = Projectile.velocity.MoveTowards(targetVel, 0.1f);
            if (Timer % 48f == 0f && Main.myPlayer == Projectile.owner)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center + spawnVel, spawnVel, ModContent.ProjectileType<ProbeLaser>(), Projectile.damage, 4f, Projectile.owner);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 origin = texture.Size() / 2;
            Color drawColor = Projectile.GetAlpha(lightColor);
            Color trailColor = drawColor;
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.75f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, null, trailColor, Projectile.oldRot[i], origin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
