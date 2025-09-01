using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class Fireball : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 54;
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.LightRed;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 13;
            Item.useAnimation = 26;
            Item.damage = 31;
            Item.knockBack = 5f;
            Item.shoot = ModContent.ProjectileType<FireBreath>();
            Item.shootSpeed = 3f;
            Item.UseSound = SoundID.Item20;
            Item.value = Item.sellPrice(gold: 3);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 6, 0f, 0f, 100, default, 2f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity.X += player.direction * 0f;
                dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 6, 0f, 0f, 100, default, 2f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity.X += player.direction * 0f;
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 360);
        }
    }
}