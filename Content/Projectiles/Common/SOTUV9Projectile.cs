using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;
using UniverseOfSwords.Utilities.Projectiles;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class SOTUV9Projectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sotu Projectile 9");     
            ProjectileID.Sets.TrailCacheLength[Type] = 15;   
            ProjectileID.Sets.TrailingMode[Type] = 2;       
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;               //The width of projectile hitbox
            Projectile.height = 10;             //The height of projectile hitbox
            Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.light = 0.5f;
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
            Projectile.timeLeft = 200;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4;
            if (Projectile.ai[0] == 0f && Projectile.alpha > 0)
            {
                Projectile.alpha -= 8;
                if (Projectile.alpha <= 0)
                {
                    Projectile.alpha = 0;
                    Projectile.ai[0] = 1f;
                }
            }

            if (Projectile.ai[0] >= 1f)
            {
                Projectile.SimpleFadeOut(ai: 0, 60f);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = oldVelocity / 2f;
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.Damage();
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.OrangeTorch, Alpha: 100, Scale: 3f);
                dust.velocity *= 4f;
                dust.noGravity = true;
            }
        }


        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(ProjectileID.SkyFracture);
            Texture2D texture = TextureAssets.Projectile[ProjectileID.SkyFracture].Value;
            Rectangle sourceRect = new(76, 0, 38, 38);
            Vector2 origin = sourceRect.Size() / 2;
            Color drawColor = Color.OrangeRed with { A = 0 } * Projectile.Opacity;
            Color trailColor = drawColor;
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.75f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, sourceRect, trailColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, sourceRect, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
