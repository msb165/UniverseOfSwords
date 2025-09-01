using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Dusts;
using UniverseOfSwordsMod.Content.Items.Weapons;
using UniverseOfSwordsMod.Content.Projectiles.Common;

namespace UniverseOfSwordsMod.Content.Projectiles.Held
{
    internal class SwordOfTheMultiverse : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<Items.Weapons.SwordOfTheMultiverse>().Texture;

        public float SwingDirection => 1f;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 120;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(8);
            Projectile.ownerHitCheck = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.extraUpdates = 2;
            Projectile.noEnchantmentVisuals = true;
        }

        Player Player => Main.player[Projectile.owner];

        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction;
            if (Projectile.ai[0] <= 0f)
            {
                //SoundEngine.PlaySound(SoundID.Item60, Projectile.position);
            }

            Projectile.ai[0]++;
            if (Projectile.ai[0] < 32f)
            {
                Projectile.Center = Player.Center;
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4 + MathHelper.SmoothStep(-2f * SwingDirection * Player.direction, 2f * SwingDirection * Player.direction, Projectile.ai[0] * 0.03f);
                Projectile.scale = 1.25f - MathHelper.SmoothStep(0f, 1.25f, Projectile.ai[0] * 0.0125f);
            }

            if (Projectile.owner == Main.myPlayer && Projectile.ai[0] == 0f)
            {
                Vector2 newVel = (Main.MouseWorld - Player.Center).SafeNormalize(Vector2.Zero) * 10f;
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center + newVel, newVel, ModContent.ProjectileType<SOTM>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            }

            if (!Player.channel && Player.noItems && Player.CCed || Projectile.ai[0] > 32f || Projectile.ai[0] >= 32f && !Player.channel)
            {
                Projectile.Kill();
                Player.reuseDelay = 2;
            }

            SetPlayerValues();
        }

        public void SetPlayerValues()
        {
            Player.SetDummyItemTime(2);
            Player.heldProj = Projectile.whoAmI;
            Player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - MathHelper.PiOver2 - MathHelper.PiOver4);
        }

        /*public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Buffs.EmperorBlaze>(), 600);
        }*/

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float _ = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Player.Center, Player.Center + (Projectile.rotation - MathHelper.PiOver4 + 0.1f).ToRotationVector2() * 555f * Projectile.scale, 16f, ref _);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 origin = new(Projectile.spriteDirection == -1 ? texture.Width : 0f, texture.Height);
            SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            float extraRotation = Projectile.spriteDirection == -1 ? MathHelper.PiOver2 : 0f;
            Color trailColor = Color.White with { A = 80 };

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.75f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, null, trailColor, Projectile.oldRot[i] + extraRotation, origin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation + extraRotation, origin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
