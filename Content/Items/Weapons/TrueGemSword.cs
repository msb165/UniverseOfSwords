using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;
using static Terraria.ModLoader.ModContent;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class TrueGemSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 58;
            Item.height = 58;
            Item.scale = 1.125f;
            Item.rare = ItemRarityID.Lime;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            //Item.shoot = ProjectileID.MagicMissile;
            Item.damage = 80;
            Item.expert = true;
            Item.knockBack = 6f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 20);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.YellowTorch, 0f, 0f, 100, default, 2f);
                Main.dust[dust].noGravity = true;
                dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.TintableDustLighted, 0f, 0f, 100, default, 2f);
                Main.dust[dust].noGravity = true;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<GemSlayer>(), 1);
            recipe.AddIngredient(ItemID.BrokenHeroSword, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Midas, 360);
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            Vector2 newVel = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero) * 8f;
            int projType = Utils.SelectRandom(Main.rand, [ProjectileType<SapphireBolt>(), ProjectileType<AmberBolt>(), ProjectileType<DiamondBolt>(), ProjectileType<SapphireBolt>(), ProjectileType<TopazBolt>(), ProjectileType<AmethystBolt>(), ProjectileType<EmeraldBolt>()]);
            Projectile.NewProjectile(target.GetSource_OnHit(target), player.Center + newVel, newVel, projType, (int)(damageDone * 0.75), hit.Knockback, player.whoAmI);
        }
    }
}