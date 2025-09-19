using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    internal class HorrorBlast : ModProjectile
    {
        public override string Texture => UniverseUtils.TexturesPath + "Empty";

        public override void SetDefaults()
        {
            Projectile.Size = new(300);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 30;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }

        public override void AI()
        {
            if (Projectile.localAI[0] == 0f)
            {
                SoundEngine.PlaySound(SoundID.Item74, Projectile.position);
                Projectile.localAI[0]++;
            }
            for (int i = 0; i < 10; i++)
            {
                Vector2 spawnVel = Vector2.Normalize(Utils.RandomVector2(Main.rand, -10f, 11f)) * Main.rand.Next(6, 20);
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Clentaminator_Green);
                dust.noGravity = true;
                dust.scale = 1.5f;
                dust.position = Projectile.Center + Main.rand.NextVector2Square(-10f, 11f);
                dust.velocity = spawnVel;
            }
        }
    }
}
