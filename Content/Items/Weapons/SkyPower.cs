using Microsoft.Xna.Framework;
using Newtonsoft.Json.Linq;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class SkyPower : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new(64);
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Pink;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 17;
            Item.damage = 60;
            Item.knockBack = 7f;
            Item.shoot = ProjectileID.SkyFracture;
            Item.shootSpeed = 15f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 40);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spin = MathHelper.ToRadians(6f);
            for (int i = 0; i < 3; i++)
            {
                float offset = i - (3f - 1f) / 2f;
                Vector2 newVel = new Vector2(14f * player.direction, 10f);
                Projectile.NewProjectileDirect(source, player.Center - Vector2.UnitY * player.height * 2f, newVel.RotatedBy(spin * offset) * Main.rand.NextFloat(0.75f, 1.25f), type, damage / 3, knockback, player.whoAmI, ai1: Main.rand.Next(1, 11));
            }
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(player, target, hit, damageDone);
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }
    }
}