using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using System.Collections;
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
    public class TwinsProj : ModProjectile
    {
        public override string Texture => UniverseUtils.TexturesPath + "Empty";

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 2;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 80;
        }

        public override void AI()
        {
            Vector2 spacing = new(8f * Projectile.direction, 8f);
            float normalRot = Vector2.Normalize(new(Projectile.velocity.X * Projectile.direction, Projectile.velocity.Y)).ToRotation();
            Projectile.localAI[0] += 0.5f;
            Vector2 offset = -Vector2.UnitY.RotatedBy(Projectile.localAI[0] * (MathHelper.PiOver2 / 6f) + MathHelper.Pi) * spacing - normalRot.ToRotationVector2() * 10f * -Projectile.direction;
            Vector2 offset2 = -Vector2.UnitY.RotatedBy(Projectile.localAI[0] * (MathHelper.PiOver2 / 6f) + MathHelper.TwoPi) * spacing - normalRot.ToRotationVector2() * 10f * -Projectile.direction;

            Dust dust = Dust.NewDustPerfect(Projectile.position, DustID.Clentaminator_Red, Vector2.Zero, Alpha: 100);
            dust.scale = 1.5f;
            dust.noGravity = true;
            dust.position = Projectile.Center + offset;

            Dust dust2 = Dust.NewDustPerfect(Projectile.position, DustID.Clentaminator_Green, Vector2.Zero, Alpha: 100);
            dust2.scale = 1.5f;
            dust2.noGravity = true;
            dust2.position = Projectile.Center + offset2;
        }

        public override void OnKill(int timeLeft)
        {
            Vector2 rotation = (Projectile.rotation - MathHelper.PiOver2).ToRotationVector2();
            Vector2 velocity = rotation * Projectile.velocity.Length() * Projectile.MaxUpdates;
            Projectile.Damage();
            for (int i = 0; i < 40; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Clentaminator_Red, Alpha: 200, Scale: 1.5f);
                dust.position = Projectile.Center + Vector2.UnitY.RotatedByRandom(MathHelper.Pi) * Main.rand.NextFloat() * Projectile.width / 2f;
                dust.noGravity = true;
                Dust dust2 = dust;
                dust2.velocity *= 3f;
                dust2.velocity += velocity * Main.rand.NextFloat();
                dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Clentaminator_Green, Alpha: 100, Scale: 1.5f);
                dust2.position = Projectile.Center + Vector2.UnitY.RotatedByRandom(MathHelper.Pi) * Main.rand.NextFloat() * Projectile.width / 2f;
                dust2.velocity *= 2f;
                dust2.noGravity = true;
                dust2.velocity += velocity * Main.rand.NextFloat();
            }
        }
    }
}
