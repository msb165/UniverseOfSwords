using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class DeathProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(48);
            Projectile.friendly = true;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 300;
            Projectile.tileCollide = false;
        }

        public ref float Timer => ref Projectile.ai[0];
        public bool ShouldExpand => Projectile.ai[1] == 1f;
        public override void AI()
        {
            Timer++;
            Projectile.rotation = MathHelper.WrapAngle(Timer * 0.5f) * Projectile.direction;
            if (Timer > 16f)
            {
                Projectile.velocity *= 1.06f;
                if (ShouldExpand)
                {
                    Projectile.Resize(Projectile.width += 1, Projectile.height += 1);
                    Projectile.scale = Projectile.Size.X / 48f;
                }
                Projectile.alpha += 5;
                if (Projectile.alpha >= 255)
                {
                    Projectile.active = false;
                }
            }
            if (Projectile.velocity.Length() > 16f)
            {
                Projectile.velocity.Normalize();
                Projectile.velocity *= 16f;
            }
            for (int i = 0; i < 10; i++)
            {
                Vector2 spawnPos = Vector2.UnitY.RotatedBy(i * MathHelper.TwoPi / 10f) * new Vector2(Projectile.Size.X / 2);
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame);
                dust.position = Projectile.Center + spawnPos;
                dust.velocity = -Vector2.Normalize(spawnPos);
                dust.alpha = 255;
                dust.noGravity = true;
                dust.scale = 1.25f;
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Clentaminator_Purple, Projectile.oldVelocity.X * 0.2f, Projectile.oldVelocity.Y * 0.2f, 100);
                dust.noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 origin = texture.Size() / 2;
            Color drawColor = Color.White with { A = 40 } * Projectile.Opacity;
            Color trailColor = drawColor;
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.75f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, null, trailColor, Projectile.oldRot[i], origin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
