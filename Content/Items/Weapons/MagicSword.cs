using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class MagicSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("You're wizard!");
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.scale = 1.125f;
            Item.rare = ItemRarityID.Orange;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 20;
            Item.damage = 27;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item109 with { Volume = 0.3f };
            Item.value = Item.buyPrice(gold: 2);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            Vector2 newVel = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero) * 12f;
            Projectile.NewProjectile(target.GetSource_OnHit(target), player.Center + newVel, newVel, ModContent.ProjectileType<SuperBeam>(), (int)(damageDone * 1.75), hit.Knockback, player.whoAmI);
        }
    }
}