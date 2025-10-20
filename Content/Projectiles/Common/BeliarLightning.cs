using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    class BeliarLightning : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 5;
        }

        public override void SetDefaults()
        {
            Projectile.width = 144;
            Projectile.height = 144;
            Projectile.alpha = 255;
            Projectile.scale = 1.5F;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            if (Projectile.ai[0] == 0f)
            {
                SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);
                Projectile.ai[0] = 1f;
            }
            if ((Projectile.alpha -= 25) < 0)
            {
                Projectile.alpha = 0;
            }
            FindFrame();
        }

        public void FindFrame()
        {
            Projectile.frameCounter++; //increase the frameCounter by one
            if (Projectile.frameCounter >= Main.projFrames[Type]) //once the frameCounter has reached 10 - change the 10 to change how fast the projectile animates
            {
                Projectile.frameCounter = 0; //reset the counter
                if (Projectile.frame++ > Main.projFrames[Type]) //if past the last frame
                {
                    Projectile.Kill();
                    Projectile.frame = 0; //go back to the first frame
                }
            }
        }

        public override bool PreDraw(ref Color lightColor) //this is where the animation happens
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically;
            int frameHeight = texture.Height / Main.projFrames[Type];
            int startY = frameHeight * Projectile.frame;
            Rectangle sourceRectangle = new(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2;
            Color drawColor = Color.White with { A = 127 } * Projectile.Opacity;

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}