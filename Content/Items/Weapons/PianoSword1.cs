using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class PianoSword1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Beethoven Beater");
            // Tooltip.SetDefault("'Moonlight Sonata - Beethoven'");
        }

        public override void SetDefaults()
        {
            Item.width = 61;
            Item.height = 61;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Green;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.damage = 8;
            Item.knockBack = 3f;
            Item.UseSound = new SoundStyle($"{nameof(UniverseOfSwords)}/Assets/Sounds/Item/PianoGreen");
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 3f;
            Item.value = 40000;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.MeleeNoSpeed;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

/*        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LivingWoodPiano, 1);
            recipe.AddIngredient(ItemID.CactusPiano, 1);
            recipe.AddIngredient(ItemID.EbonwoodPiano, 1);
            recipe.AddIngredient(ItemID.RichMahoganyPiano, 1);
            recipe.AddIngredient(ItemID.PalmWoodPiano, 1);
            recipe.AddIngredient(ItemID.BorealWoodPiano, 1);
            recipe.AddIngredient(ItemID.Piano, 1);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
*/
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < Main.rand.Next(1, 6); i++)
            {
                Vector2 newVel = velocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.75f, 1.25f);
                Projectile.NewProjectile(source, position + newVel, newVel, Utils.SelectRandom(Main.rand, [ProjectileID.EighthNote, ProjectileID.QuarterNote, ProjectileID.TiedEighthNote]), 7, knockback, player.whoAmI);
            }
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Poisoned, 360);
        }
    }
}