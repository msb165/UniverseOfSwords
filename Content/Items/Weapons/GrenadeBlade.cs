using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Items.Placeable;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class GrenadeBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Some madman strapped a grenade to a sword to increase its damage. And now someone even crazier is wielding it as a weapon!'");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.scale = 1.3f;
            Item.rare = ItemRarityID.LightRed;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.damage = 30;
            Item.knockBack = 6f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 40);
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
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target))
            {
                Vector2 newPos = target.Center + new Vector2(Main.rand.Next(-100, 100), -200f);
                Vector2 newVel = Vector2.Normalize(target.Center - newPos) * 10f;
                Projectile.NewProjectile(target.GetSource_OnHit(target), newPos, newVel, ModContent.ProjectileType<Grenade>(), Item.damage, Item.knockBack, player.whoAmI, ai1: target.Center.Y);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Grenade, 99);
            recipe.AddIngredient(ItemID.Wire, 20);
            recipe.AddIngredient(ModContent.ItemType<DamascusBar>(), 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}