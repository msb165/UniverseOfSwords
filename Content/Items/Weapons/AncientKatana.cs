using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class AncientKatana : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.BonusAttackSpeedMultiplier[Type] = 0.75f;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.LightPurple;
            Item.scale = 1.5f;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.damage = 70;
            Item.knockBack = 5f;
            Item.UseSound = SoundID.Item1;
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
                UniverseUtils.CustomHoldStyle(player, new Vector2(64f * player.direction, -96f), new Vector2(3f * player.direction, 4f));
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
                .AddIngredient(ModContent.ItemType<Orichalcon>(), 5)
                .AddIngredient(ItemID.SoulofFright, 15)
                .AddIngredient(ItemID.SoulofMight, 10)
                .AddIngredient(ItemID.SoulofLight, 10)
                .AddIngredient(ItemID.ChlorophyteBar, 20)
                .AddIngredient(ModContent.ItemType<UpgradeMatter>(), 50)
                .AddIngredient(ItemID.Katana)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
