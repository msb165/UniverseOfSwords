using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class OmegaExcalibur : ModItem
    {
        public override void SetDefaults()
        { 
            Item.width = 58;
            Item.height = 58; 
			Item.scale = 1.25f;
            Item.rare = ItemRarityID.Lime;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 17;
            Item.useAnimation = 17;           
            Item.damage = 77; 
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 10, silver: 10);			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ModContent.ProjectileType<OmegaExcaliburEnergy>();
            Item.noMelee = true;
            Item.shootsEveryUse = true;
            Item.holdStyle = 0;
        }

        public override void HoldItem(Player player) => Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -64f), Vector2.UnitY * 4f);
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float adjustedItemScale = player.GetAdjustedItemScale(Item);
            Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, Item.damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
            NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.TrueExcalibur)
                .AddIngredient(ItemID.SoulofFright, 10)
                .AddIngredient(ItemID.SoulofMight, 10)
                .AddIngredient(ItemID.SoulofSight, 10)
                .AddIngredient(ItemID.HallowedBar, 10)
                .AddIngredient(ItemID.LightShard, 3)
                .AddIngredient(ModContent.ItemType<Orichalcon>())
                .AddIngredient(ModContent.ItemType<UpgradeMatter>(), 25)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}