using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Materials;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class VugarMutater : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Gleams with an otherwordly light'");
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 39;
            Item.useAnimation = 13;
            Item.damage = 90;
            Item.knockBack = 6f;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<Projectiles.Common.VugarMutater>();
            Item.shootSpeed = 6f;
            Item.value = Item.buyPrice(gold: 8);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position + velocity, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 360);
        }
    }
}