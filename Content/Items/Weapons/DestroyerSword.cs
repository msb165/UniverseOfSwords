using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class DestroyerSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Boom, Boom!");
        }

        public override void SetDefaults()
        {
            Item.Size = new(48);
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.LightPurple;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 62;
            Item.knockBack = 6f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 1, silver: 60);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                UniverseUtils.SpawnRotatedDust(player, DustID.Clentaminator_Blue, start: (int)(14 * Item.scale), end: (int)(84 * Item.scale));
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target))
            {
                Vector2 spawnPos = player.Center - Vector2.UnitY * 96f;
                Vector2 newVel = (target.Center - spawnPos).SafeNormalize(Vector2.UnitY) * 12f;
                Projectile.NewProjectile(target.GetSource_OnHit(target), spawnPos, newVel.RotatedByRandom(MathHelper.ToRadians(5f)), ModContent.ProjectileType<MightBolt>(), Item.damage, Item.knockBack, player.whoAmI);
            }
        }
    }
}
