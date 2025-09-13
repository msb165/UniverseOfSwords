using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class TheBrain : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Sword of Crimson'");
        }

        public override void SetDefaults()
        {
            Item.width = 58;
            Item.height = 58;
            Item.scale = 1.125f;
            Item.rare = ItemRarityID.LightRed;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.damage = 24;
            Item.knockBack = 3.0F;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 50);
            Item.shoot = ModContent.ProjectileType<DraculaProj>();
            Item.shootSpeed = 3.5f;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Blood, 0f, 0f, 100, default, 2f);
                Main.dust[dust].noGravity = true;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position + velocity, velocity.RotatedByRandom(MathHelper.ToRadians(15f)), type, damage, knockback, player.whoAmI);
            return false;
        }
    }
}
