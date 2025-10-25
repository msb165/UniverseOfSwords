using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class PumpkinBoom : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new(48);
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 65;
            Item.knockBack = 7f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 2, silver: 60);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
        }

        public override void HoldItem(Player player) => Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -64f), Vector2.UnitY * 4f);
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame) => player.itemLocation = player.Center;

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            Projectile.NewProjectile(target.GetSource_OnHit(target), target.Center, Vector2.Zero, ProjectileID.SolarWhipSwordExplosion, damageDone, hit.Knockback, player.whoAmI, ai1: 0.85f + Main.rand.NextFloat() * 1.15f);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<PumpkinSword>())
                .AddIngredient(ItemID.JackOLanternLauncher)
                .AddIngredient(ModContent.ItemType<Orichalcon>())
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 100)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}