using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class Golem : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Ancient sword that was inside Golems body");
        }

        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 64;
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Lime;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.damage = 90;
            Item.knockBack = 8f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 4);
            Item.shoot = ModContent.ProjectileType<GolemLaser>();
            Item.shootSpeed = 10f;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            UniverseUtils.SpawnRotatedDust(player, DustID.Lihzahrd, 1f);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            for (int j = 0; j < 3; j++)
            {
                Vector2 spawnPos = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.UnitY) * 40f;
                float offset = j * (3f - 1f) / 2f;
                Vector2 spawnOffset = spawnPos.RotatedBy(offset * MathHelper.TwoPi / 3f);
                Vector2 spawnVel = Vector2.Normalize(Main.MouseWorld - player.Center - spawnOffset);
                Projectile.NewProjectile(target.GetSource_OnHit(target), player.Center + spawnOffset - Vector2.UnitY * 12f, spawnVel * 10f, Item.shoot, Item.damage, Item.knockBack, player.whoAmI);
            }
        }
    }
}
