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
using UniverseOfSwords.Content.Dusts;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class PurpleLaserBeam : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.ShadowBeamFriendly}";
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            Projectile.Size = new(8);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 100;
            Projectile.timeLeft = 100;
            Projectile.alpha = 255;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 21;
            Projectile.noEnchantmentVisuals = true;
        }

        public override void AI()
        {
            for (int i = 0; i < 3; i++)
            {
                Vector2 position = Projectile.position - Projectile.velocity * (i * 0.25f);
                Dust dust = Dust.NewDustDirect(position, 1, 1, ModContent.DustType<PurpleBeam>());
                dust.position = position + Projectile.Size / 2;
                dust.scale = Main.rand.Next(70, 110) * 0.013f;
                Dust dust2 = dust;
                dust2.velocity *= 0.2f;
            }
        }
    }
}
