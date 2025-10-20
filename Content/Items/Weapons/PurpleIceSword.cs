using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class PurpleIceSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.scale = 1f;
            Item.rare = ItemRarityID.White;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.damage = 14;
            Item.knockBack = 4.5f;
            Item.UseSound = SoundID.Item1;
            Item.value = 4500;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            base.MeleeEffects(player, hitbox);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(player, target, hit, damageDone);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PurpleIceBlock, 25);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}