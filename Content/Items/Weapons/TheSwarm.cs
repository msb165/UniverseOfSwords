using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class TheSwarm : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.scale = 1f;
            Item.rare = ItemRarityID.LightRed;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 34;
            Item.useAnimation = 17;
            Item.damage = 15;
            Item.knockBack = 5f;
            Item.shoot = ProjectileID.Bee;
            Item.shootSpeed = 2f;
            Item.UseSound = SoundID.Item1;
            Item.value = 38500;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, player.beeType(), player.beeDamage(damage / 3), player.beeKB(0f), player.whoAmI);
            return Main.rand.NextBool(3);
        }
    }
}