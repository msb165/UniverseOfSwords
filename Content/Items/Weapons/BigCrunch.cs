using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Common;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class BigCrunch : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new(60);
            Item.scale = 1.5f;
            Item.rare = ItemRarityID.Red;
            Item.crit = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.damage = 190;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item109 with { Volume = 0.3f };
            Item.value = Item.sellPrice(platinum: 1);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 999;
        }

        public override bool MeleePrefix() => false;

        public override void UseItemFrame(Player player)
        {
            player.itemLocation = player.RotatedRelativePoint(player.MountedCenter);
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                Dust dust = Dust.NewDustDirect(player.Center + new Vector2(player.direction * -player.direction, player.gravDir * -110f), 96, 96, DustID.Clentaminator_Green, 0, 0, 127, default(Color), 1f);
                if (player.direction == -1)
                {
                    dust.position.X -= 64f;
                }
                dust.noGravity = true;
                dust.velocity = Main.rand.NextVector2Circular(2f, 4f) - Vector2.UnitY;
                dust.velocity = dust.velocity.RotatedBy(-player.itemRotation);
                UniverseUtils.CustomHoldStyle(player, new Vector2(32f * player.direction, -64f), new Vector2(0f, 4f));
            }
        }

        public override void HoldItem(Player player)
        {
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
                Projectile.NewProjectile(target.GetSource_OnHit(target), player.Center - Vector2.UnitY * 64f + newVel, newVel, ModContent.ProjectileType<GreenSaw>(), Item.damage, Item.knockBack, player.whoAmI);
            }
            target.AddBuff(BuffID.Poisoned, 1000);
            target.AddBuff(BuffID.CursedInferno, 1000);
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