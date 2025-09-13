using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class StarMaelstorm : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Star Maelstrom");
            // Tooltip.SetDefault("'Todays forecast: shooting stars and hurricanes'");
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 58;
            Item.height = 66;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Purple;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.damage = 200;
            Item.knockBack = 10f;
            Item.UseSound = SoundID.Item105;
            Item.shoot = ProjectileID.StarWrath;
            Item.shootSpeed = 20f;
            Item.value = Item.sellPrice(gold: 30);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.shootsEveryUse = true;
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useStyle = ItemUseStyleID.Swing;
                Item.useTime = 40;
                Item.useAnimation = 40;
                Item.damage = 100;
                Item.shoot = ProjectileID.DD2ApprenticeStorm;
                Item.shootSpeed = 5f;
                Item.shootsEveryUse = false;
            }
            else
            {
                Item.useStyle = ItemUseStyleID.Swing;
                Item.useTime = 15;
                Item.useAnimation = 15;
                Item.damage = 200;
                Item.shoot = ModContent.ProjectileType<MaelstormStar>();
                Item.shootSpeed = 20f;
                Item.shootsEveryUse = true;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float adjustedItemScale = player.GetAdjustedItemScale(Item);
            Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), ModContent.ProjectileType<MaelstormEnergy>(), damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
            NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);

            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(source, player.Center, Vector2.UnitX * player.direction * velocity.Length(), ProjectileID.DD2ApprenticeStorm, damage, knockback, player.whoAmI);
                Projectile.NewProjectile(source, player.Center, -Vector2.UnitX * player.direction * velocity.Length(), ProjectileID.DD2ApprenticeStorm, damage, knockback, player.whoAmI);
            }

            return false;
        }
    }
}