using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class GemBolt : ModProjectile
    {
        public enum GemType : int
        {
            Gem_Diamond = 0,
            Gem_Topaz = 1, 
            Gem_Emerald = 2,
            Gem_Amethyst = 3,
            Gem_Amber = 4,
            Gem_Sapphire = 5,
            Gem_Ruby = 6
        }

        public override string Texture => UniverseUtils.TexturesPath + "Empty";
        public override void SetDefaults()
        {
            Projectile.Size = new(10);
            Projectile.aiStyle = -1;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 180;
        }

        public ref float DustSelected => ref Projectile.ai[0];
        
        int dustType = DustID.AmberBolt;
        Player Player => Main.player[Projectile.owner];
        public override void AI()
        {
            switch (DustSelected)
            {
                case (int)GemType.Gem_Diamond:
                    dustType = DustID.GemDiamond;
                    break;
                case (int)GemType.Gem_Topaz:
                    dustType = DustID.GemTopaz;
                    break;
                case (int)GemType.Gem_Emerald:
                    dustType = DustID.GemEmerald;
                    break;
                case (int)GemType.Gem_Amethyst:
                    dustType = DustID.GemAmethyst;
                    break;
                case (int)GemType.Gem_Amber:
                    dustType = DustID.AmberBolt;
                    break;
                case (int)GemType.Gem_Sapphire:
                    dustType = DustID.GemSapphire;
                    break;
                case (int)GemType.Gem_Ruby:
                    dustType = DustID.GemRuby;
                    break;
            }

            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, dustType, Projectile.velocity.X, Projectile.velocity.Y, 50, default, 1.25f);
                dust.noGravity = true;
                Dust dust2 = dust;
                dust2.velocity *= 0.3f;
            }
            if (Projectile.ai[1] == 0f)
            {
                Projectile.ai[1] = 1f;
                SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int j = 0; j < 15; j++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, dustType, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 50, default, 1.25f);
                dust.noGravity = true;
                Dust dust2 = dust;
                dust2.scale *= 1.25f;
                dust2 = dust;
                dust2.velocity *= 0.5f;
            }
        }
    }
}
