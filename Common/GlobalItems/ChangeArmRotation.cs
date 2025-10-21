using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Common.GlobalItems
{
    public class ChangeArmRotation : GlobalItem
    {
        public override void UseStyle(Item item, Player player, Rectangle heldItemFrame)
        {
            int[] allowedTypes = [ItemID.NightsEdge, ItemID.TerraBlade, ItemID.TrueNightsEdge, ItemID.Excalibur, ItemID.TrueExcalibur];
            if ((item.DamageType == DamageClass.Melee || item.DamageType == DamageClass.MeleeNoSpeed) && !item.noMelee && !item.noUseGraphic && item.useStyle == ItemUseStyleID.Swing || allowedTypes.Contains(item.type))
            {
                float rotation = player.itemRotation - MathHelper.PiOver2 - MathHelper.PiOver4;
                if (player.direction == -1)
                {
                    rotation -= MathHelper.PiOver2;
                }
                if (player.gravDir == -1)
                {
                    rotation *= player.gravDir;
                    rotation += MathHelper.PiOver2 * player.direction;
                }
                player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, rotation );
                player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.ThreeQuarters, rotation);
            }
        }
    }
}
