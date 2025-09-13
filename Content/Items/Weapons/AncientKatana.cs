using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Common;
using UniverseOfSwordsMod.Content.Items.Materials;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class AncientKatana : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 68;
            Item.rare = ItemRarityID.LightPurple;
            Item.scale = 1.5f;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 9;
            Item.useAnimation = 9;
            Item.damage = 70;
            Item.knockBack = 5f;
            Item.UseSound = SoundID.Item1;
            Item.value = 600000;
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
                UniverseUtils.CustomHoldStyle(player, new Vector2(64f * player.direction, -96f), new Vector2(4f, 4f));
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                UniverseUtils.SpawnRotatedDust(player, DustID.PortalBoltTrail, 1.5f, (int)(16 * Item.scale), (int)(90 * Item.scale));
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target))
            {
                UniverseUtils.Spawn.SummonGenericSlash(target.Center, Color.White, player.whoAmI, damageDone, 200, lerpToWhite: 1f);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 250)
                .AddIngredient(ModContent.ItemType<Orichalcon>())
                .AddIngredient(ItemID.SoulofFright, 15)
                .AddIngredient(ItemID.SoulofMight, 10)
                .AddIngredient(ItemID.SoulofLight, 10)
                .AddIngredient(ItemID.ChlorophyteBar, 20)
                .AddIngredient(ModContent.ItemType<UpgradeMatter>(), 5)
                .AddIngredient(ItemID.Katana)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
