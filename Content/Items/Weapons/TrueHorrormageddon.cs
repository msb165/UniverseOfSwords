using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Items.Placeable;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class TrueHorrormageddon : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.BonusAttackSpeedMultiplier[Type] = 0.75f;
            // Tooltip.SetDefault("'There used to be a graveyard, now it is a crater'");
        }

        public override void SetDefaults()
        {
            Item.width = 128;
            Item.height = 128;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.damage = 182;
            Item.knockBack = 8f;
            Item.UseSound = SoundID.Item71;
            Item.shoot = ProjectileID.DeathSickle;
            Item.shootSpeed = 10f;
            Item.value = Item.sellPrice(platinum: 1);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
            Item.shootsEveryUse = true;
            Item.noMelee = true;
        }

        public override void HoldItem(Player player)
        {
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                Vector2 spawnVel = Main.rand.NextVector2CircularEdge(200f, 200f);
                Vector2 spawnPos = player.Center - spawnVel;
                Dust dust = Dust.NewDustPerfect(spawnPos, DustID.Clentaminator_Green, Vector2.Zero);
                dust.position = spawnPos;
                dust.scale = 1f;
                dust.velocity = -Vector2.Normalize(dust.position - player.Center) * 8f;
                dust.noGravity = true;
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -60f), Vector2.UnitY * 6f);
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override void AddRecipes()
        {
            Mod calamity = UniverseOfSwords.Instance.CalamityMod;
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Horrormageddon>());
            recipe.AddIngredient(ModContent.ItemType<GnomBlade>());
            recipe.AddIngredient(ItemID.BrokenHeroSword, 10);
            if (calamity is not null)
            {
                recipe.AddIngredient(calamity.Find<ModItem>("DarkPlasma"), 5);
            }
            recipe.AddIngredient(ModContent.ItemType<UpgradeMatter>(), 25);
            recipe.AddIngredient(ModContent.ItemType<BlackBar>(), 25);
            recipe.AddIngredient(ModContent.ItemType<LunarOrb>(), 3);
            recipe.AddIngredient(ItemID.LunarBar, 80);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float adjustedItemScale = player.GetAdjustedItemScale(Item);
            Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), ModContent.ProjectileType<HorrormageddonEnergy>(), Item.damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
            Projectile.NewProjectile(source, player.MountedCenter, velocity, ModContent.ProjectileType<Projectiles.Common.TrueHorrormageddon>(), Item.damage, knockback, player.whoAmI, player.direction * player.gravDir, 32f, 2f);
            NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
            return false;
        }
    }
}