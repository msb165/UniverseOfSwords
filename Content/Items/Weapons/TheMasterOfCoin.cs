using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class TheMasterOfCoin : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Master of Coin");
            // Tooltip.SetDefault("'End your financial problems with this sword'");
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Purple;
            Item.damage = 50;            
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.UseSound = SoundID.Item43;
            Item.value = Item.sellPrice(copper: 1);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Midas, 600);
            if (!NPCID.Sets.CountsAsCritter[target.type] && !target.immortal && target.chaseable && !target.dontTakeDamage && target.life < 0 && Main.rand.NextBool(3))
            {
                Projectile.NewProjectile(target.GetSource_OnHit(target), target.Center, Vector2.Zero, ProjectileID.CoinPortal, 0, 0f, player.whoAmI);
            }
        }



        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LuckyCoin, 1);
            recipe.AddIngredient(ItemID.GoldCrown, 10);
            recipe.AddIngredient(ItemID.GoldOre, 999);
            recipe.AddIngredient(ItemID.FlaskofGold, 60);
            recipe.AddIngredient(null, "Inflation", 1);
            recipe.AddIngredient(ItemID.PlatinumCoin, 9);
            recipe.AddIngredient(ItemID.GoldCoin, 99);
            recipe.AddIngredient(ItemID.SilverCoin, 999);
            recipe.AddIngredient(ItemID.CopperCoin, 999);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LuckyCoin, 1);
            recipe.AddIngredient(ItemID.PlatinumCrown, 10);
            recipe.AddIngredient(ItemID.PlatinumOre, 999);
            recipe.AddIngredient(ItemID.FlaskofGold, 60);
            recipe.AddIngredient(null, "Inflation", 1);
            recipe.AddIngredient(ItemID.PlatinumCoin, 9);
            recipe.AddIngredient(ItemID.GoldCoin, 99);
            recipe.AddIngredient(ItemID.SilverCoin, 999);
            recipe.AddIngredient(ItemID.CopperCoin, 999);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}