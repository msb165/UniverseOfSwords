using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons      //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class HumanBuzzSaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Human Buzz Saw");
            // Tooltip.SetDefault("'Cuts through hordes of Terraria like butter'");
        }

        public override void SetDefaults()
        {
            Item.damage = 24;
            Item.DamageType = DamageClass.Melee;     //This defines if it does Melee damage and if its effected by Melee increasing Armor/Accessories.
            Item.width = 53;    //The size of the width of the hitbox in pixels.
            Item.height = 53;    //The size of the height of the hitbox in pixels.
            Item.crit = 8;
            Item.scale = 1f;
            Item.useTime = 4;   //How fast the Weapon is used.
            Item.useAnimation = 4;     //How long the Weapon is used for.
            Item.channel = true;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = 100;    //The way your Weapon will be used, 1 is the regular sword swing for example
            Item.knockBack = 5f;    //The knockback stat of your Weapon.
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.LightRed;   //The color the title of your Weapon when hovering over it ingame                    
            Item.shoot = ModContent.ProjectileType<Projectiles.Held.HumanBuzzSaw>();
            Item.noUseGraphic = true; // this defines if it does not use graphic
            Item.noMelee = true;
            Item.holdStyle = 0;
        }

        public override void HoldItem(Player player)
        {
            Item.noUseGraphic = player.ItemAnimationActive;
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -40f), Vector2.UnitY * 4f);
            }
        }

        public override void UseItemFrame(Player player)     //this defines what frame the player use when this weapon is used
        {
            player.bodyFrame.Y = 3 * player.bodyFrame.Height;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Sawmill, 1);
            recipe.AddIngredient(ItemID.TitaniumBar, 8);
            recipe.AddIngredient(ModContent.ItemType<SwordMatter>(), 60);
            recipe.AddIngredient(null, "DamascusBar", 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Sawmill, 1);
            recipe.AddIngredient(ItemID.AdamantiteBar, 8);
            recipe.AddIngredient(ModContent.ItemType<SwordMatter>(), 60);
            recipe.AddIngredient(null, "DamascusBar", 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}