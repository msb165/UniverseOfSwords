using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Projectiles.Held;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class GreenSolutionSpreader : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Infinite biome spreading? Awesome!");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.rare = ItemRarityID.Lime;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 20;
            Item.UseSound = SoundID.Item34;
            Item.shoot = ProjectileID.PureSpray;
            Item.shootSpeed = 1f;
            Item.value = Item.sellPrice(gold: 2, silver: 25);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Default;
            Item.useAmmo = AmmoID.Solution;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player) => false;

        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[ModContent.ProjectileType<SuperSolutionSpreader>()] < 1;

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = TextureAssets.Item[Type].Value;
            Color newColor = Main.DiscoColor * 1.25f;

            spriteBatch.Draw(texture, position, frame, newColor, 0f, origin, scale, SpriteEffects.None, 0f);
            return false;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = TextureAssets.Item[Type].Value;
            Vector2 origin = texture.Frame().Size() / 2;
            Color drawColor = Main.DiscoColor * 1.25f;
            Vector2 drawPos = Item.position - Main.screenPosition + texture.Frame().Size() / 2 + new Vector2(Item.width / 2 - origin.X, Item.height - texture.Frame().Height);

            spriteBatch.Draw(texture, drawPos, texture.Frame(), drawColor, rotation, origin, scale, SpriteEffects.None, 0f);
            return false;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SuperSolutionSpreader>(), damage, knockback, player.whoAmI);
            return false;
        }


        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 200)
                .AddIngredient(ItemID.GreenSolution, 200)
                .AddIngredient(ItemID.BlueSolution, 200)
                .AddIngredient(ItemID.SandSolution, 200)
                .AddIngredient(ItemID.DarkBlueSolution, 200)
                .AddIngredient(ItemID.DirtSolution, 200)
                .AddIngredient(ItemID.SnowSolution, 200)
                .AddIngredient(ItemID.RedSolution, 200)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}