using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Common;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class MoltenShard : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Inflicts On Fire! debuff");
        }

        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 56;
            Item.scale = 1.125f;
            Item.rare = ItemRarityID.LightRed;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.damage = 33;
            Item.knockBack = 7f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Common.MoltenShard>();
            Item.shootSpeed = 3.5f;
            Item.UseSound = SoundID.Item20;
            Item.value = Item.sellPrice(gold: 5);
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
                UniverseUtils.CustomHoldStyle(player, new Vector2(32f * player.direction, -22f), new Vector2(0f, 4f));
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                UniverseUtils.SpawnRotatedDust(player, DustID.Flare, 2f, end: 90);
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 400);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position += velocity * 4f - Vector2.UnitY * 32f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<PoisonCrystallus>())
                .AddIngredient(ItemID.HellstoneBar, 18)
                .AddIngredient(ItemID.Obsidian, 10)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}