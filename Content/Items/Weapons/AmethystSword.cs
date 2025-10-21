using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;
using static UniverseOfSwords.Content.Projectiles.Common.GemBolt.GemType;


namespace UniverseOfSwords.Content.Items.Weapons
{
    public class AmethystSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.LightPurple;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.damage = 9;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 20);
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
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -64f), Vector2.UnitY * 6f);
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                UniverseUtils.SpawnRotatedDust(player, DustID.CrystalSerpent_Pink, 1.5f, 16, 80);
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            Vector2 newVel = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero) * 4f;
            Projectile.NewProjectile(target.GetSource_OnHit(target), player.Center + newVel, newVel, ModContent.ProjectileType<GemBolt>(), (int)(damageDone * 0.75), hit.Knockback, player.whoAmI, ai0: (float)Gem_Amethyst);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Amethyst, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
