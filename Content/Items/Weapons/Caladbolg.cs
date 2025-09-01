using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class Caladbolg : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Caladbolg");
            // Tooltip.SetDefault("'Beati diripientes falsa pro veris sunt Messiahs'");
        }

        public override void SetDefaults()
        {
            Item.Size = new(60);
            Item.scale = 1.5f;
            Item.rare = ItemRarityID.Lime;
            Item.crit = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.damage = 150;
            Item.knockBack = 50f;
            Item.UseSound = SoundID.Item7 with { Volume = 0.5f };
            Item.value = Item.sellPrice(platinum: 1);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override bool MeleePrefix() => false;

        public override void UseItemFrame(Player player)
        {
            player.itemLocation = player.RotatedRelativePoint(player.MountedCenter);
        }

        public override void HoldItem(Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<FlyingSword>()] == 0)
            {
                Projectile.NewProjectile(Projectile.GetSource_None(), player.Center, Vector2.Zero, ModContent.ProjectileType<FlyingSword>(), Item.damage, 4f, player.whoAmI, ai0: MathHelper.Pi);
                Projectile.NewProjectile(Projectile.GetSource_None(), player.Center, Vector2.Zero, ModContent.ProjectileType<FlyingSword>(), Item.damage, 4f, player.whoAmI, ai0: MathHelper.TwoPi);
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            Vector2 newVel = (player.Center - target.Center).SafeNormalize(Vector2.UnitY) * 4f;
            for (int i = 0; i < 3; i++) //Replace 2 with number of projectiles
            {
                Projectile.NewProjectile(target.GetSource_OnHit(target), player.Center + newVel, newVel, ModContent.ProjectileType<GreenSaw>(), Item.damage, Item.knockBack, player.whoAmI);
            }

            target.AddBuff(BuffID.Poisoned, 1000);
            target.AddBuff(BuffID.CursedInferno, 1000);
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
        }
    }
}