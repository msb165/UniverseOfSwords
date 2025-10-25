using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class BlizzardRage : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.damage = 50;
            Item.knockBack = 5f;
            Item.UseSound = SoundID.Item1;
            Item.value = 450500;
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

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (NPCID.Sets.CountsAsCritter[target.type] || target.immortal || !target.active)
            {
                return;
            }

            int numberProjectiles = 4 + Main.rand.Next(4);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 spawnPos = target.Center + new Vector2(Main.rand.Next(-300, 301), -300f * Main.rand.NextFloat(0.9f, 2f));
                Vector2 spawnVel = Vector2.Normalize(target.Center - spawnPos) * 20f;
                Projectile.NewProjectile(target.GetSource_OnHit(target), spawnPos, spawnVel, ProjectileID.NorthPoleSnowflake, (int)(Item.damage * 0.25f), Item.knockBack, player.whoAmI, ai1: Main.rand.Next(3));
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BlizzardStaff, 1);
            recipe.AddIngredient(null, "Orichalcon", 1);
            recipe.AddIngredient(ModContent.ItemType<SwordMatter>(), 100);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}