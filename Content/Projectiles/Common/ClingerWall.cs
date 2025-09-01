using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;
using UniverseOfSwordsMod.Utilities.Projectiles;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class ClingerWall : ModProjectile
    {
        public override string Texture => UniverseUtils.TexturesPath + "Empty";
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 16;
            Projectile.height = 200;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = 2;
        }

        public override void AI()
        {
            Projectile.velocity.Y = MathHelper.Clamp(Projectile.velocity.Y, 0f, 16f);
            Projectile.velocity.Y = Projectile.velocity.Y < 4f ? Projectile.velocity.Y += 2f : Projectile.velocity.Y += 4f;
            Projectile.SimpleFadeOut(ai: 0, maxTime: 13);
            Vector2 projPos = new(Projectile.Center.X - Projectile.width / 2, Projectile.position.Y + Projectile.height - 200);
            for (int i = 0; i < 1; i++)
            {
                if (Collision.SolidCollision(projPos, Projectile.width, 200))
                {
                    Projectile.velocity.Y = MathHelper.Clamp(Projectile.velocity.Y, -16f, 0f);
                    Projectile.velocity.Y = Projectile.velocity.Y > -4f ? Projectile.velocity.Y -= 2f : Projectile.velocity.Y -= 4f;
                    continue;
                }
            }
            for (int i = 0; i < 20; i++)
            {
                Dust dust = Dust.NewDustPerfect(Vector2.Lerp(Projectile.Top, Projectile.Bottom, i * 0.05f), ModContent.DustType<Dusts.ClingerWall>(), Vector2.Zero, Scale: 1f);
                dust.alpha = Projectile.alpha;
                dust.noGravity = true;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return base.OnTileCollide(oldVelocity);
        }
    }
}
