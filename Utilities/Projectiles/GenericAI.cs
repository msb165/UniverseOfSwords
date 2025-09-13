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
using UniverseOfSwordsMod.Content.Items.Weapons;

namespace UniverseOfSwordsMod.Utilities.Projectiles
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


    }
}
