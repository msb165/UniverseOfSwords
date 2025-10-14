using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;
using UniverseOfSwords.Utilities.Projectiles;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class MagicProj : ModProjectile
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Projectile_{ProjectileID.SwordBeam}";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.light = 0.5f;
            Projectile.alpha = 0;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            if (Projectile.velocity.Length() > 7f)
            {
                Projectile.velocity *= 0.94f;
            }
            Projectile.SwordBeamAI();
            if (Projectile.ai[0] >= 16f)
            {
                Projectile.velocity *= 0.95f;
            }
            Projectile.VampireKnivesAI(ai: 0, 30f);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for (int i = 4; i < 31; i++)
            {
                Vector2 oldVel = Projectile.oldVelocity * (30f / (float)i);
                int dust = Dust.NewDust(Projectile.position - oldVel, 8, 8, DustID.SandSpray, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100, default, 1.25f);
                Main.dust[dust].noGravity = true;
                dust = Dust.NewDust(Projectile.position - oldVel, 8, 8, DustID.SandSpray, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100, default, 1f);
                Main.dust[dust].noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 origin = new(texture.Width, 0f);
            Color drawColor = Projectile.GetAlpha(lightColor);
            Color trailColor = drawColor;
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            float timer = MathF.Cos((float)Main.timeForVisualEffects * MathHelper.TwoPi / 30f);

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.75f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, null, trailColor, Projectile.oldRot[i], origin, Projectile.scale, spriteEffects, 0f);
                for (int j = 0; j < 8; j++)
                {
                    Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition + (Vector2.UnitY).RotatedBy(MathHelper.PiOver2 * j) * (4f + 1f * timer), null, trailColor with { A = 0 } * 0.1f, Projectile.rotation, origin, Projectile.scale * 1.25f, spriteEffects, 0f);
                }
            }
            return false;
        }
    }
}
