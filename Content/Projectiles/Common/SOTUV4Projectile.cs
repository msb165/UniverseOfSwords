using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;
using UniverseOfSwords.Utilities.Projectiles;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class SOTUV4Projectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sotu Projectile 4");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Type] = 15;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Type] = 2;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;               //The width of projectile hitbox
            Projectile.height = 20;             //The height of projectile hitbox
            Projectile.scale = 1f;
            Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.alpha = 0;
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.VampireKnivesAI(ai: 0, maxTime: 30f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.myPlayer == Projectile.owner && Projectile.ai[1] != 1f)
            {
                for (int i = 0; i < 6; i++)
                {
                    Vector2 spawnPos = target.Center + new Vector2(Main.rand.Next(-400, 400), -Main.rand.Next(600, 900));
                    Vector2 spawnVel = Vector2.Normalize(target.Center - spawnPos) * 20f;
                    Projectile.NewProjectile(Projectile.GetSource_Death(), spawnPos, spawnVel, Type, Projectile.damage, Projectile.knockBack, Projectile.owner, ai0: -15f, ai1: 1f);
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = oldVelocity / 2f;
            return false;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float _ = 0f;
            Vector2 velocity = Projectile.velocity.SafeNormalize(Vector2.UnitY) * Projectile.scale;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center - velocity * 40f, Projectile.Center + velocity * 40f, 10f * Projectile.scale, ref _);
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.Damage();
        }

        public override bool PreDraw(ref Color lightColor)
        {
            UniverseUtils.Drawing.DrawWithAfterImages(Projectile, Color.White with { A = 0 } * Projectile.Opacity);
            return false;
        }
    }
}