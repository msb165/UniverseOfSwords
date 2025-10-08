using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using UniverseOfSwords.Content.Items.Weapons;

namespace UniverseOfSwords.Utilities.Projectiles
{
    public static class GenericAI
    {
        /// <summary>
        /// Fades out the projectile after a certain period of time.
        /// </summary>

        public static void SimpleFadeOut(this Projectile proj, int ai, float maxTime)
        {
            proj.ai[ai]++;
            if (proj.ai[ai] > maxTime)
            {
                proj.alpha += 10;
                if (proj.alpha >= 255)
                {
                    proj.active = false;
                }
            }
        }

        public static void SwordBeamAI(this Projectile proj)
        {
            if (proj.localAI[1] < 15f)
            {
                proj.localAI[1]++;
            }
            else
            {
                if (proj.localAI[0] == 0f)
                {
                    proj.scale -= 0.02f;
                    proj.alpha += 30;
                    if (proj.alpha >= 250)
                    {
                        proj.alpha = 255;
                        proj.localAI[0] = 1f;
                    }
                }
                else if (proj.localAI[0] == 1f)
                {
                    proj.scale += 0.02f;
                    proj.alpha -= 30;
                    if (proj.alpha <= 0)
                    {
                        proj.alpha = 0;
                        proj.localAI[0] = 0f;
                    }
                }
            }
            proj.rotation = proj.velocity.ToRotation() + MathHelper.PiOver4;
            if (proj.velocity.Y > 16f)
            {
                proj.velocity.Y = 16f;
            }
        }

        public static void SolarEruptionAI(this Projectile proj, Player player, int ai = 0, float velocityOffset = 48f)
        {
            if (proj.localAI[0] == 0f)
            {
                proj.localAI[0] = proj.velocity.ToRotation();
            }
            float direction = (MathF.Cos(proj.localAI[0]) >= 0f).ToDirectionInt();
            if (proj.ai[1] <= 0f)
            {
                direction *= -1f;
            }
            proj.ai[ai]++;
            Vector2 spinningpoint = (direction * (proj.ai[ai] / 30f * MathHelper.TwoPi - MathHelper.PiOver2)).ToRotationVector2();
            spinningpoint.Y *= MathF.Sin(proj.ai[1]);
            if (proj.ai[1] <= 0f)
            {
                spinningpoint.Y *= -1f;
            }
            spinningpoint = spinningpoint.RotatedBy(proj.localAI[0]);
            if (proj.ai[ai] < 30f)
            {
                proj.velocity += velocityOffset * spinningpoint;
            }
            else
            {
                proj.Kill();
            }
            proj.rotation = proj.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4;
            proj.spriteDirection = proj.direction;
            proj.timeLeft = 2;
            proj.Center = player.RotatedRelativePoint(player.MountedCenter, addGfxOffY: false) + Vector2.Normalize(proj.velocity) + Vector2.Normalize(proj.rotation.ToRotationVector2());

        }
    }
}
