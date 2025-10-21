using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Buffs;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class BigCrunch : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new(30);
            Item.scale = 1.5f;
            Item.rare = ItemRarityID.Red;
            Item.crit = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 23;
            Item.useAnimation = 23;
            Item.damage = 200;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item109 with { Volume = 0.3f };
            Item.value = Item.sellPrice(platinum: 1);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
        }

        public override void UseItemFrame(Player player)
        {
            player.itemLocation = player.RotatedRelativePoint(player.MountedCenter);
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                float rotation = player.itemRotation - MathHelper.PiOver4 * player.gravDir;
                if (player.direction == -1)
                {
                    rotation -= MathHelper.PiOver2 * player.gravDir;
                }
                Dust dust = Dust.NewDustPerfect(player.Center + rotation.ToRotationVector2() * 80f * Item.scale, DustID.Clentaminator_Green, Vector2.Zero, Alpha: 127, newColor: default, Scale: 1f);
                dust.noGravity = true;
                dust.velocity = Main.rand.NextVector2Circular(2f, 4f) - Vector2.UnitY;
                dust.velocity = dust.velocity.RotatedBy(-player.itemRotation * player.gravDir);
                UniverseUtils.CustomHoldStyle(player, new Vector2(32f * player.direction, -64f), new Vector2(0f, 4f));
            }
        }

        public override void HoldItem(Player player)
        {
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
            if (player.ownedProjectileCounts[ModContent.ProjectileType<FlyingSword>()] == 0)
            {
                Projectile.NewProjectile(Projectile.GetSource_None(), player.Center, Vector2.Zero, ModContent.ProjectileType<FlyingSword>(), Item.damage * 2, 4f, player.whoAmI, ai0: MathHelper.Pi);
                Projectile.NewProjectile(Projectile.GetSource_None(), player.Center, Vector2.Zero, ModContent.ProjectileType<FlyingSword>(), Item.damage * 2, 4f, player.whoAmI, ai0: MathHelper.TwoPi);
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            Vector2 newVel = (player.Center - target.Center).SafeNormalize(Vector2.UnitY) * 4f;
            for (int i = 0; i < 3; i++)
            {
                Projectile.NewProjectile(target.GetSource_OnHit(target), player.Center - Vector2.UnitY * 64f + newVel, newVel, ModContent.ProjectileType<GreenSaw>(), Item.damage * 2, Item.knockBack, player.whoAmI);
            }
            target.AddBuff(ModContent.BuffType<TrueSlow>(), 1000);
            target.AddBuff(ModContent.BuffType<SuperVenom>(), 1000);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LunarBar, 15)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}