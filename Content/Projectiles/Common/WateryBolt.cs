using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class WateryBolt : ModProjectile
    {
        public override string Texture => UniverseUtils.TexturesPath + "Empty";
        public override void SetDefaults()
        {
            Projectile.Size = new(16);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.timeLeft = 400;
            Projectile.penetrate = 10;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.noEnchantmentVisuals = true;
        }

        public override void AI()
        {
            Projectile.velocity *= 0.99f;
            for (int i = 0; i < 5; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position + Vector2.One * 4f, Projectile.width - 8, Projectile.height - 8, DustID.Clentaminator_Blue, Alpha: 100, Scale: 1f);
                dust.noGravity = true;
                dust.velocity *= 0.1f;
                dust.velocity += Projectile.velocity * 0.1f;
                dust.position = Projectile.Center + Main.rand.NextVector2Square(-Projectile.width / 2 + 4, Projectile.width / 2 - 4) - Projectile.velocity / 5f * i;
                dust.alpha = Projectile.alpha;
            }
            if (Main.rand.NextBool(5))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position + Vector2.One * 4f, Projectile.width - 8, Projectile.height - 8, DustID.Clentaminator_Blue, Alpha: 100, Scale: 0.6f);
                dust.velocity *= 0.25f;
                dust.velocity.Y = Main.rand.Next(10, 80) * 0.1f;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            //Vector2 newVel = Vector2.Normalize(oldVelocity) * 5f;
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
    }
}
