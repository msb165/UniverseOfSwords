using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Items.Placeable;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class RedFlareLongsword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Scarlet Flare Longsword");
            /* Tooltip.SetDefault("Fires scarlet flare waves and ignites enemies with Scarlet flames"
                + "\n'Ignite your foes in scarlet flames'"); */
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.scale = 1.125f;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.damage = 92;
            Item.knockBack = 5f;
            Item.shoot = ModContent.ProjectileType<ScarletFlareBolt>();
            Item.shootSpeed = 6f;
            Item.UseSound = SoundID.Item45;
            Item.value = Item.sellPrice(gold: 10);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
        }

        public override void HoldItem(Player player) => Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -60f), Vector2.UnitY * 6f);
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                UniverseUtils.SpawnRotatedDust(player, DustID.LifeDrain, 1.25f, (int)(30 * Item.scale), (int)(90 * Item.scale));
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HellstoneBar, 25)
                .AddIngredient(ItemID.RedTorch, 25)
                .AddIngredient(ItemID.Ruby, 50)
                .AddIngredient(ItemID.SoulofFright, 20)
                .AddIngredient(ModContent.ItemType<BlackBar>(), 5)
                .AddIngredient(ItemID.BrokenHeroSword)
                .AddIngredient(ModContent.ItemType<DeathSword>())
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 200)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 500);
            if (UniverseUtils.IsAValidTarget(target))
            {
                player.Heal(3);
            }
        }
    }
}