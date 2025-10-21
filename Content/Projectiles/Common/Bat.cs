using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;
using UniverseOfSwords.Utilities.Projectiles;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    internal class Bat : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.Bat}";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.alpha = 255;
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Melee;
        }

        public int attackTarget = -1;
        float velocityLength = 0f;

        public override void AI()
        {
            if (Projectile.localAI[0] == 0f)
            {
                velocityLength = Projectile.velocity.Length();
                Projectile.localAI[0] = 1f;
            }
            Projectile.rotation = Projectile.velocity.X * 0.1f;
            Projectile.spriteDirection = Projectile.direction;
            if (Projectile.ai[0] == 0f && Projectile.alpha > 0)
            {
                Projectile.alpha -= 8;
                if (Projectile.alpha <= 0)
                {
                    Projectile.alpha = 0;
                    Projectile.ai[0] = 1f;
                }
            }

            if (Projectile.ai[0] >= 1f)
            {
                Projectile.VampireKnivesAI(ai: 0, 23f);
            }
            FindNPCAndApplySpeed(velocityLength);
            FindFrame();
        }

        public void FindNPCAndApplySpeed(float multiplier)
        {
            NPC npc = UniverseUtils.Misc.FindTargetWithinRange(Projectile, 300f);
            if (npc != null)
            {
                attackTarget = npc.whoAmI;
                Projectile.netUpdate = true;
            }

            if (attackTarget != -1 && Main.npc[attackTarget].active)
            {
                Vector2 speed = Vector2.Normalize(Main.npc[attackTarget].Center - Projectile.Center) * multiplier;
                Projectile.velocity = (Projectile.velocity * 20f + speed) / 21f;
            }
        }

        public void FindFrame()
        {
            if ((++Projectile.frameCounter) >= Main.projFrames[Type])
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Type])
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Poisoned, 300);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.BatScepter);
                dust.scale = 0.85f;
                dust.noGravity = true;
                Dust dust2 = dust;
                dust2.velocity += Projectile.velocity * 0.5f;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = oldVelocity / 2f;
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            int frameHeight = texture.Height / Main.projFrames[Type];
            int startY = frameHeight * Projectile.frame;
            Rectangle sourceRectangle = new(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2;
            Color drawColor = Projectile.GetAlpha(lightColor);
            Color trailColor = drawColor;
            float timer = MathF.Cos((float)Main.timeForVisualEffects * MathHelper.TwoPi / 30f);

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.75f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, sourceRectangle, trailColor, Projectile.oldRot[i], origin, Projectile.scale, spriteEffects, 0f);
            }

            for (int j = 0; j < 4; j++)
            {
                Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition + Vector2.UnitY.RotatedBy(MathHelper.PiOver4 * j) * (4f + 1f * timer), sourceRectangle, Color.SkyBlue with { A = 0 } * Projectile.Opacity, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
