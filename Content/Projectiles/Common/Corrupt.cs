using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;
using UniverseOfSwords.Utilities.Projectiles;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class Corrupt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 30;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Type] = 2;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;               //The width of projectile hitbox
            Projectile.height = 10;          //The height of projectile hitbox
            Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
        }

        public override void AI()
        {
            if (Projectile.velocity.Length() > 3f)
            {
                Projectile.velocity *= 0.94f;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.VampireKnivesAI(ai: 0, maxTime: 30f);

            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Corruption);
            dust.velocity *= 0.4f;
            dust.alpha = Projectile.alpha;
            dust.noGravity = true;
            dust.scale = 1.0f;
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 10; k++)
            {
                int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Corruption, Projectile.oldVelocity.X * 0.1f, Projectile.oldVelocity.Y * 0.1f, Alpha: 100);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            UniverseUtils.Drawing.DrawWithAfterImages(Projectile, Projectile.GetAlpha(lightColor));
            return false;
        }
    }
}