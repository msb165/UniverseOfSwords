using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
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
using UniverseOfSwords.Content.Dusts;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class StarProj : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.FallingStar}";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(32);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.alpha = 50;
            Projectile.light = 1f;
            Projectile.timeLeft = 96;
            Projectile.scale = 1.25f;
            Projectile.tileCollide = false;
        }

        public ref float Timer => ref Projectile.ai[0];
        public int Size = 32;
        public int soundDelay = 30;
        public override void AI()
        {
            if (Projectile.soundDelay <= 0)
            {
                Projectile.soundDelay = soundDelay > 8 ? soundDelay -= 8 : 8;
                SoundEngine.PlaySound(SoundID.Item9 with { Volume = 0.2f }, Projectile.position);
            }
            Timer++;
            if (Timer >= 24)
            {
                Projectile.velocity *= 0.95f;
            }
            if (Timer > 40)
            {
                Projectile.tileCollide = false;
                Projectile.scale += 0.1f;
                Projectile.Resize(Size++, Size++);
                Projectile.rotation += 0.4f * Projectile.direction;
                if (Projectile.scale >= 2.5f && Projectile.alpha < 255)
                {
                    Projectile.alpha += 5;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<GoldCoin>());
                dust.noGravity = true;
                dust.position = Projectile.Center + Main.rand.NextVector2Square(-Projectile.width / 2 + 4, Projectile.width / 2 - 4);
                dust.alpha = Projectile.alpha;
            }

            Projectile.rotation += 0.2f * Projectile.direction;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for (int i = 0; i < 30; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<GoldCoin>());
                dust.noGravity = true;
                dust.velocity *= 2f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 origin = texture.Size() / 2;
            Color drawColor = Color.Yellow * Projectile.Opacity;
            Color outColor = Color.Blue with { A = 0 } * Projectile.Opacity;
            Color outColor2 = Color.White with { A = 40 } * Projectile.Opacity;
            Color trailColor = drawColor;
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            float timer = MathF.Cos((float)Main.timeForVisualEffects * MathHelper.TwoPi / 30f) * 0.1f;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.75f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Projectile.velocity / 20f * i - Main.screenPosition, null, trailColor, Projectile.oldRot[i], origin, Projectile.scale, spriteEffects, 0f);
            }

            for (int i = 0; i < 8; i++)
            {
                Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition + Vector2.UnitY.RotatedBy(MathHelper.PiOver4 * i) * (4f + 1f * timer), null, outColor2 * 0.2f, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, outColor, Projectile.rotation, origin, Projectile.scale * 1.25f, spriteEffects, 0f);
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);

            return false;
        }
    }
}
