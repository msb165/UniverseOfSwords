using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Materials;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class BladesOfBalance : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 51;
            Item.DamageType = DamageClass.Melee;
            Item.Size = new(34);
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5f;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.LightPurple;
			Item.scale = 1.25f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Held.BladesOfBalance>();
            Item.shootSpeed = 1f;
        }

        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[Item.shoot] < 1;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spin = (Main.rand.NextFloat() - 0.5f) * MathHelper.TwoPi;
            Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, spin);
            return false;
        }

		public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AncientBattleArmorMaterial)
                .AddIngredient(ItemID.FrostCore)
                .AddIngredient(ItemID.LightShard)
                .AddIngredient(ItemID.DarkShard)
                .AddIngredient(ItemID.SoulofNight, 10)
                .AddIngredient(ItemID.SoulofLight, 10)
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 100)
                .AddTile(TileID.MythrilAnvil)
                .Register();
	    }
    }
}