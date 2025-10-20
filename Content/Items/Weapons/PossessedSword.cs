using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class PossessedSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.scale = 1.125f;
            Item.rare = ItemRarityID.Pink;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 57;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item1;
            Item.value = 57800;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
        }

        public override void HoldItem(Player player)
        {
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                float rotation = player.itemRotation - MathHelper.PiOver4 * player.gravDir;
                if (player.direction == -1)
                {
                    rotation -= MathHelper.PiOver2 * player.gravDir;
                }
                Dust dust = Dust.NewDustPerfect(player.Center + rotation.ToRotationVector2() * 84f * Item.scale, DustID.Corruption, Vector2.Zero, Alpha: 127, newColor: default, Scale: 2f);
                dust.noGravity = true;
                dust.velocity = Main.rand.NextVector2Circular(2f, 4f) - Vector2.UnitY;
                dust.velocity = dust.velocity.RotatedBy(-player.itemRotation * player.gravDir);
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -62f), new Vector2(0f, 4f));
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target))
            {
                UniverseUtils.Spawn.SummonGenericSlash(target.Center, Color.MediumPurple, player.whoAmI, Item.damage, lerpToWhite: 0.5f);
            }
        } 
    }
}