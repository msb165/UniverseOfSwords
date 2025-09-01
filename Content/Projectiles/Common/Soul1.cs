using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Dusts;
using UniverseOfSwordsMod.Utilities;
using UniverseOfSwordsMod.Utilities.Projectiles;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class Soul1 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.scale = 1f;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.SimpleFadeOut(ai: 0, 30f);

            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<TintableDust1>(), newColor: Color.Green, Scale: 1.25f);
                dust.scale = 1.25f;
                dust.velocity = Projectile.velocity;
                dust.alpha = Projectile.alpha;
                dust.position = Projectile.Center - Projectile.velocity / 3f * i;
            }

            if (Projectile.ai[1] == 1f)
            {
                Projectile.velocity *= 0.98f;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[1] = 1f;
            Projectile.velocity = oldVelocity / 2f;
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            UniverseUtils.Drawing.DrawWithAfterImages(Projectile, Color.White with { A = 0 } * Projectile.Opacity);
            return false;
        }
    }
}