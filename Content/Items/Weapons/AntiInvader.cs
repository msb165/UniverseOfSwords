using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class AntiInvader : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Anti-Invader");
            // Tooltip.SetDefault("'I'll like to have chicken burger, fried chicken wings, two slices of your chicken pie, no make that three... What do you mean you can't supersize me?!'");
        }

        public override void SetDefaults()
        {
            Item.Size = new(64);
            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.damage = 100;
            Item.knockBack = 8f;
            Item.UseSound = SoundID.Item33;
            Item.value = Item.sellPrice(gold: 25);
            Item.scale = 1.4f;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }

            for (int i = 0; i < 6; i++) //Replace 2 with number of projectiles
            {
                UniverseUtils.Spawn.SummonGenericSlash(target.Center, new(100, 220, 255), player.whoAmI, damageDone, 180);
            }

            if (target.life <= 0)
            {
                Projectile.NewProjectileDirect(target.GetSource_Death(), target.Center, Vector2.Zero, ProjectileID.MonkStaffT3_AltShot, damageDone, Item.knockBack, player.whoAmI);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MartianConduitPlating, 1000);
            recipe.AddIngredient(null, "MartianSaucerCore", 1);
            recipe.AddIngredient(null, "SwordMatter", 200);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}