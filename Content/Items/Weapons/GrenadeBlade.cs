using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Placeable;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class GrenadeBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Some madman strapped a grenade to a sword to increase its damage. And now someone even crazier is wielding it as a weapon!'");
        }

        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 56;
            Item.scale = 1.3f;
            Item.rare = ItemRarityID.LightRed;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.damage = 20;
            Item.knockBack = 8f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 1);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Vector2 newpos = target.Center + new Vector2(Main.rand.Next(-100, 100), -200f);
            Vector2 newVel = Vector2.Normalize(target.Center - newpos) * 10f;
            Projectile.NewProjectile(target.GetSource_OnHit(target), newpos, newVel, ProjectileID.Grenade, Item.damage, Item.knockBack, player.whoAmI);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Grenade, 99);
            recipe.AddIngredient(ItemID.Wire, 20);
            recipe.AddIngredient(ModContent.ItemType<DamascusBar>(), 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}