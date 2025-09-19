using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class PrimeSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Pew, pew!");
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.Size = new(64);
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.LightPurple;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.damage = 65;
            Item.knockBack = 6f;
            Item.UseSound = SoundID.Item33;
            Item.value = 160000;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }

        public override bool AltFunctionUse(Player player)
        {
            return base.AltFunctionUse(player);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target))
            {
                Vector2 spawnPos = player.Center - Vector2.UnitY * 96f;
                Vector2 newVel = (target.Center - spawnPos).SafeNormalize(Vector2.UnitY) * 6f;
                Projectile.NewProjectile(target.GetSource_OnHit(target), spawnPos, newVel, ModContent.ProjectileType<PrimeBolt>(), (int)(Item.damage * 0.75), Item.knockBack, player.whoAmI);
            }
        }
    }
}
