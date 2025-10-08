using Microsoft.Xna.Framework;
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
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    internal class AquaticusProj : ModProjectile
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Projectile_{ProjectileID.Bubble}";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 30;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.noEnchantmentVisuals = true;
        }

        int baseWidth = 14;
        int baseHeight = 14;

        public override void AI()
        {
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] > 3f)
            {
                Projectile.alpha -= 25;
                if (Projectile.alpha < 127)
                {
                    Projectile.alpha = 127;
                }
            }
            Projectile.velocity *= 0.96f;
            Projectile.scale = Projectile.ai[1];
            Projectile.Resize((int)(baseWidth * Projectile.ai[1]), (int)(baseHeight * Projectile.ai[1]));
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item54, Projectile.position);
            Projectile.Damage();
            for (int i = 0; i < 10; i++)
            {
                int size = (int)(10f * Projectile.ai[1]);
                int dust = Dust.NewDust(Projectile.Center - Vector2.One * size, size * 2, size * 2, DustID.BubbleBurst_White);
                Dust dust2 = Main.dust[dust];
                Vector2 velocity = Vector2.Normalize(dust2.position - Projectile.Center);
                dust2.position = Projectile.Center + velocity * size * Projectile.scale;
                if (i < 30)
                {
                    dust2.velocity = velocity * dust2.velocity.Length();
                }
                else
                {
                    dust2.velocity = velocity * Main.rand.Next(45, 91) / 10f;
                }
                dust2.color = Main.hslToRgb((float)(0.4f + Main.rand.NextDouble() * 0.2f), 0.9f, 0.5f);
                dust2.color = Color.Lerp(dust2.color, Color.White, 0.3f);
                dust2.noGravity = true;
                dust2.scale = 0.7f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 origin = texture.Size() / 2;
            Color drawColor = Projectile.GetAlpha(lightColor);
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
