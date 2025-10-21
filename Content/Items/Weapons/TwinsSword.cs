using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class TwinsSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Whoosh, whoosh!");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.LightPurple;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 40;
            Item.useAnimation = 20;
            Item.damage = 62;
            Item.knockBack = 6f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 5);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position += velocity * 6f - Vector2.UnitY * 48f;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                UniverseUtils.SpawnRotatedDust(player, DustID.Clentaminator_Green, start: (int)(14 * Item.scale), end: (int)(91 * Item.scale));
                UniverseUtils.SpawnRotatedDust(player, DustID.Clentaminator_Red, start: (int)(14 * Item.scale), end: (int)(91 * Item.scale));
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target))
            {
                Vector2 spawnPos = player.Center - Vector2.UnitY * 96f;
                Vector2 newVel = (target.Center - spawnPos).SafeNormalize(Vector2.UnitY) * 4f;
                Projectile.NewProjectile(target.GetSource_OnHit(target), spawnPos, newVel.RotatedByRandom(MathHelper.ToRadians(5f)), ModContent.ProjectileType<TwinsProj>(), Item.damage, Item.knockBack, player.whoAmI);
            }
        }
    }
}
