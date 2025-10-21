using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class WraithBlade : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.scale = 1.125f;
            Item.rare = ItemRarityID.LightPurple;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.damage = 56;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 25);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame) => player.itemLocation.Y -= 1f * player.gravDir;

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            Vector2 newVel = (target.Center - player.Center).SafeNormalize(Vector2.Zero) * 6f;
            Projectile.NewProjectile(target.GetSource_OnHit(target), player.Center + newVel, newVel, ModContent.ProjectileType<WraithProj>(), (int)(damageDone * 0.75), hit.Knockback, player.whoAmI);
        }

    }
}