using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Weapons;
using static UniverseOfSwords.Utilities.UniverseUtils;

namespace UniverseOfSwords.Content.Projectiles.Held
{
    public class SuperSolutionSpreader : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<GreenSolutionSpreader>().Texture;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(16);
            Projectile.scale = 1.5f;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Generic;  //Tells the game whether it is a melee projectile or not

        }

        Player Player => Main.player[Projectile.owner];
        ref float Timer => ref Projectile.ai[0];

        public override void AI()
        {
            Timer++;
            Projectile.Center = Player.Center;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4 + MathHelper.Lerp(-1.5f * Player.direction, 2f * Player.direction, MathF.Sqrt(Easings.EaseInSine(Timer * 0.125f)));

            if (Player.direction == -1)
            {
                Projectile.rotation += MathHelper.PiOver2;
            }

            if (Main.myPlayer == Projectile.owner)
            {
                bool canShoot = Player.channel && Player.HasAmmo(Player.HeldItem);
                if (canShoot)
                {
                    Projectile.velocity = Vector2.Normalize(Main.MouseWorld - Player.Center);
                    Player.PickAmmo(Player.HeldItem, out int projToShoot, out _, out _, out _, out int useAmmoItemID, true);
                    IEntitySource source = Player.GetSource_ItemUse_WithPotentialAmmo(Player.HeldItem, useAmmoItemID);

                    float rotation = Projectile.rotation - MathHelper.PiOver2 - MathHelper.PiOver4;
                    if (Player.direction == -1)
                    {
                        rotation += MathHelper.PiOver4 + MathHelper.PiOver2;
                    }
                    Vector2 newSpeed = rotation.ToRotationVector2() * 8f;
                    Projectile.NewProjectile(source, Projectile.Center, newSpeed, projToShoot, 0, 0, Projectile.owner);
                }
            }

            if (!Player.channel || Player.noItems || Player.CCed)
            {
                Projectile.Kill();
                Player.reuseDelay = 2;
            }
            SetPlayerValues();
        }

        public void SetPlayerValues()
        {
            Player.ChangeDir(Projectile.direction);
            Player.SetDummyItemTime(2);
            Player.heldProj = Projectile.whoAmI;
            float rotation = Projectile.rotation - MathHelper.PiOver2 - MathHelper.PiOver4;
            if (Player.direction == 1)
            {
                rotation -= MathHelper.PiOver2;
            }
            Player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, rotation);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Vector2 drawOrigin = new(Projectile.spriteDirection == 1 ? texture.Width : 0f, texture.Height);
            Color drawColor = Main.DiscoColor;
            Color trailColor = drawColor with { A = 0 };

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.76f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, null, trailColor, Projectile.oldRot[i], drawOrigin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, drawColor, Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
