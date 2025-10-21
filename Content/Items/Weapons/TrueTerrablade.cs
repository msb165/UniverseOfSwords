using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Common;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class TrueTerrablade : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Shoots big projectile that explodes into smaller beams after hitting an enemy");
            ItemID.Sets.BonusAttackSpeedMultiplier[Type] = 0.75f;
        }

        public override void SetDefaults()
        {
            Item.damage = 165;
            Item.DamageType = DamageClass.Melee;
            Item.width = 32;
            Item.height = 40;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6.5f;
            Item.value = Item.buyPrice(gold: 10);
			Item.shoot = ModContent.ProjectileType<Projectiles.Common.TrueTerrablade>();
			Item.shootSpeed = 12f;
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item60;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.shootsEveryUse = true;
            Item.scale = 1.25f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float adjustedItemScale = player.GetAdjustedItemScale(Item);
            Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), ModContent.ProjectileType<TrueTerrabladeEnergy>(), damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
            NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
            return false;
        }
    }
}