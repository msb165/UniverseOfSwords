using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace UniverseOfSwordsMod.Utilities
{
    public partial class UniverseUtils
    {
        public static void SpawnDustLine(Vector2 start, Vector2 end, Vector2? velocity = null, int dustAmount = 10, int dustType = DustID.MagicMirror)
        {                                   
            for (int i = 0; i < dustAmount; i++)
            {
                Dust dust = Dust.NewDustPerfect(Vector2.Lerp(start, end, 0.1f * i), dustType, velocity);
                dust.noGravity = true;
            }
        }

        public static void SpawnRotatedDust(Player player, int dustType = DustID.Torch, float dustScale = 1f, int start = 14, int end = 84, int alpha = 100, Color color = default)
        {
            float rotated = MathHelper.PiOver4;
            if (player.gravDir == -1)
            {
                rotated += MathHelper.PiOver2 * -player.direction;
            }
            Vector2 spawnVel = player.itemRotation.ToRotationVector2().RotatedBy(rotated);

            if (player.direction == -1)
            {
                spawnVel = spawnVel.RotatedBy(MathHelper.PiOver2);
            }
            Dust dust = Dust.NewDustPerfect(Vector2.Lerp(player.Center + spawnVel * start, player.Center + spawnVel.RotatedBy(-MathHelper.PiOver2 * player.direction * player.gravDir) * end, Main.rand.NextFloat(0.05f, 1.125f)), dustType, spawnVel, Scale: dustScale, Alpha: alpha, newColor: color);
            dust.noGravity = true;
        }
    }
}
