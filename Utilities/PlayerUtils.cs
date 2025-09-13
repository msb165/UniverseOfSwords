using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using static Terraria.Player;

namespace UniverseOfSwordsMod.Utilities
{
    public partial class UniverseUtils
    {
        public static void CustomHoldStyle(Player player, Vector2 itemRotation, Vector2 holdOutOffset = default, float bounceSpeed = 1f)
        {
            if (Main.dedServ)
            {
                return;
            }
            float itemRot = itemRotation.ToRotation() + MathHelper.PiOver4;
            if (player.direction == -1)
            {
                itemRot += MathHelper.PiOver2;
            }

            player.itemRotation = MathHelper.Lerp(0f, itemRot, MathF.Sin((float)Main.timeForVisualEffects / 60f)) - MathHelper.PiOver4 / 2;
            if (player.direction == -1)
            {
                player.itemRotation += MathHelper.PiOver4;
            }

            CompositeArmStretchAmount stretch = CompositeArmStretchAmount.Quarter;
            float offset = MathHelper.Pi / 6f;
            if (player.direction == -1)
            {
                offset *= -1f;
            }
            float armRotation = player.itemRotation + MathHelper.PiOver2;
            if (player.direction == 1)
            {
                armRotation += MathHelper.Pi;
            }
            float rotation = armRotation + offset;
            float rotation2 = armRotation - offset;
            Vector2 finalRotation = (armRotation + MathHelper.PiOver2).ToRotationVector2() * 2f;

            player.itemLocation = player.MountedCenter.Floor() + holdOutOffset + finalRotation;
            player.SetCompositeArmFront(enabled: true, stretch, rotation);
            player.SetCompositeArmBack(enabled: true, stretch, rotation2);
            player.FlipItemLocationAndRotationForGravity();
            player.bodyFrame.Y = 0;
        }
    }
}
