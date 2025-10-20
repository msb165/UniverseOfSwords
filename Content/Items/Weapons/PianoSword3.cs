using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class PianoSword3 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mozart Mauler");
            // Tooltip.SetDefault("'Piano Concerto No.20 - Mozart'");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.scale = 2.5F;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.damage = 66;
            Item.knockBack = 3.0F;
            Item.UseSound = new SoundStyle($"{nameof(UniverseOfSwords)}/Assets/Sounds/Item/PianoRed");
            Item.shoot = ProjectileID.FallingStar;
            Item.shootSpeed = 15;
            Item.value = Item.sellPrice(gold: 10);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.X -= 1f * player.direction;
            player.itemLocation.Y -= 1f * player.direction;
        }

/*        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SkywarePiano, 1);
            recipe.AddIngredient(ItemID.SlimePiano, 1);
            recipe.AddIngredient(ItemID.BlueDungeonPiano, 1);
            recipe.AddIngredient(ItemID.GreenDungeonPiano, 1);
            recipe.AddIngredient(ItemID.PinkDungeonPiano, 1);
            recipe.AddIngredient(ItemID.ObsidianPiano, 1);
            recipe.AddIngredient(ItemID.MeteoritePiano, 1);
            recipe.AddIngredient(ItemID.BonePiano, 1);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }*/

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ProjectileID.LostSoulFriendly, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, velocity, ProjectileID.ShadowBeamFriendly, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, velocity, ProjectileID.InfernoFriendlyBolt, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, velocity, ProjectileID.HellfireArrow, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, velocity, ProjectileID.MeteorShot, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, velocity, ProjectileID.Bone, damage, knockback, player.whoAmI);
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Slimed, 360);
        }
    }
}