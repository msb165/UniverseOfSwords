using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;
using UniverseOfSwords.Utilities.Projectiles;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class Armageddon : ModProjectile
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Projectile_{ProjectileID.Meteor1}";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;               //The width of projectile hitbox
            Projectile.height = 32;          //The height of projectile hitbox
            Projectile.scale = 1f;
            Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true;          //Can the projectile collide with tiles?
        }

        public override void AI()
        {
            if (Projectile.velocity.Length() > 6f)
            {
                Projectile.velocity *= 0.94f;
            }
            if (Main.rand.NextBool(2))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Flare);
                dust.noGravity = true;
                dust.scale = 2f;
            }
            Projectile.rotation += 0.3f * Projectile.direction;
            KeepProjectileCloseToPlayer();
            Projectile.VampireKnivesAI(ai: 0, 20f);
        }

        public void KeepProjectileCloseToPlayer()
        {
            float maxDistance = 250f;
            Vector2 distance = Main.player[Projectile.owner].Center - Projectile.Center;
            if (distance.Length() > maxDistance)
            {
                float num5 = distance.Length() - maxDistance;
                Vector2 vector2 = distance;
                distance.Normalize();
                distance *= maxDistance;
                Projectile.position = Main.player[Projectile.owner].Center - distance;
                Projectile.position -= Projectile.Size / 2;
                float velLength = Projectile.velocity.Length();
                Projectile.velocity.Normalize();
                if (num5 > velLength - 1f)
                {
                    num5 = velLength - 1f;
                }
                Projectile.velocity *= velLength - num5;
                velLength = Projectile.velocity.Length();
                Vector2 projCenter = Projectile.Center;
                Vector2 ownerCenter = Main.player[Projectile.owner].Center;
                if (projCenter.Y < ownerCenter.Y)
                {
                    vector2.Y = Math.Abs(vector2.Y);
                }
                else if (projCenter.Y > ownerCenter.Y)
                {
                    vector2.Y = -Math.Abs(vector2.Y);
                }
                if (projCenter.X < ownerCenter.X)
                {
                    vector2.X = Math.Abs(vector2.X);
                }
                else if (projCenter.X > ownerCenter.X)
                {
                    vector2.X = -Math.Abs(vector2.X);
                }
                vector2.Normalize();
                vector2 *= Projectile.velocity.Length();
                if (Math.Abs(Projectile.velocity.X) > Math.Abs(Projectile.velocity.Y))
                {
                    Vector2 vector5 = Projectile.velocity;
                    vector5.Y += vector2.Y;
                    vector5.Normalize();
                    vector5 *= Projectile.velocity.Length();
                    if ((double)Math.Abs(vector2.X) < 0.1 || (double)Math.Abs(vector2.Y) < 0.1)
                    {
                        Projectile.velocity = vector5;
                    }
                    else
                    {
                        Projectile.velocity = (vector5 + Projectile.velocity * 20f) / 30f;
                    }
                }
                else
                {
                    Vector2 velocity = Projectile.velocity;
                    velocity.X += vector2.X;
                    velocity.Normalize();
                    velocity *= Projectile.velocity.Length();
                    if ((double)Math.Abs(vector2.X) < 0.2 || (double)Math.Abs(vector2.Y) < 0.2)
                    {
                        Projectile.velocity = velocity;
                    }
                    else
                    {
                        Projectile.velocity = (velocity + Projectile.velocity * 20f) / 30f;
                    }
                }
            }
        }


        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 20; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Flare, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Flare, Projectile.oldVelocity.X * 0.10f, Projectile.oldVelocity.Y * 0.10f);
            }

            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Daybreak, 500);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            UniverseUtils.Drawing.DrawWithAfterImages(Projectile, Projectile.GetAlpha(lightColor));
            return false;
        }
    }
}