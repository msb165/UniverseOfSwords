using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;
using static UniverseOfSwords.Content.Projectiles.Common.GemBolt.GemType;


namespace UniverseOfSwords.Content.Items.Weapons
{
    public class DiamondSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 42;
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Pink;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.damage = 12;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 30);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(5))
            {
                UniverseUtils.SpawnRotatedDust(player, DustID.GemDiamond, 1.5f, (int)(16 * Item.scale), (int)(58 * Item.scale));
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            Vector2 newVel = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero) * 4f;
            Projectile.NewProjectile(target.GetSource_OnHit(target), player.Center + newVel, newVel, ModContent.ProjectileType<GemBolt>(), (int)(damageDone * 0.75), hit.Knockback, player.whoAmI, ai0: (float)Gem_Diamond);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Diamond, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
