using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class VugarMutater : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Vugar Mutater");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Type] = 10;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Type] = 3;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(8);
            Projectile.scale = 1f;
            Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.extraUpdates = 5;            //Set to above 0 if you want the projectile to update multiple time in a frame
            Projectile.timeLeft = 800;
        }

        public ref float Timer => ref Projectile.ai[0];

        public override void AI()
        {
            Timer++;
            if (Timer >= 16f)
            {
                Projectile.velocity *= 0.98f;
            }

            if (Timer >= 30f)
            {
                Projectile.alpha += 10;
                if (Projectile.alpha >= 255)
                {
                    Projectile.active = false;
                }
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.spriteDirection = Projectile.direction;

            for (int i = 0; i < 2; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.oldPosition, Projectile.width, Projectile.height, ModContent.DustType<Dusts.VugarMutater>(), newColor: Color.White with { A = 0 });
                dust.alpha = Projectile.alpha;
                dust.scale = 1f;
                dust.position = Projectile.oldPos[i] + Projectile.Size / 2 - Projectile.velocity / 2f * i + Main.rand.NextVector2Circular(-Projectile.width + 4, Projectile.width - 4) * 2f;
                dust.velocity = -Projectile.velocity / 4f;
                dust.noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 origin = new(texture.Width / 2, 0f);
            Color drawColor = Color.White with { A = 100 };
            Color trailColor = drawColor;
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.75f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, null, trailColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}