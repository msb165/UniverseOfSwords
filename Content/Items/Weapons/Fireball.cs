using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class Fireball : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 54;
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.LightRed;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 13;
            Item.useAnimation = 26;
            Item.damage = 31;
            Item.knockBack = 5f;
            Item.shoot = ModContent.ProjectileType<FireBreath>();
            Item.shootSpeed = 2.5f;
            Item.UseSound = SoundID.Item20;
            Item.value = Item.sellPrice(gold: 3);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
        }

        public override void HoldItem(Player player)
        {
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
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
                Dust dust = Dust.NewDustPerfect(player.Center + rotation.ToRotationVector2() * 30f * Item.scale, DustID.InfernoFork, Vector2.Zero, Alpha: 127, newColor: default, Scale: 1.25f);
                dust.noGravity = true;
                dust.velocity = Main.rand.NextVector2Circular(4f, 10f) - Vector2.UnitY;
                dust.velocity = dust.velocity.RotatedBy(-player.itemRotation * player.gravDir);
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -64f), new Vector2(0f, 4f));
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Torch, 0f, 0f, 100, default, 2f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity.X += player.direction * 0f;
                dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Torch, 0f, 0f, 100, default, 2f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity.X += player.direction * 0f;
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 360);
        }
    }
}