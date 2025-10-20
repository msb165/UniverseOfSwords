using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Dusts;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class NightmareBlast : ModProjectile
    {
        public override string Texture => UniverseUtils.TexturesPath + "Empty";

        public override void SetDefaults()
        {
            Projectile.Size = new(260);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100;
            //Projectile.usesIDStaticNPCImmunity = true;
            //Projectile.idStaticNPCHitCooldown = 25;
        }

        public override void AI()
        {
            NPC npc = Main.npc[(int)Projectile.ai[0]];
            if (Projectile.localAI[0] == 0f)
            {
                SoundEngine.PlaySound(SoundID.Item74, Projectile.position);
                Projectile.localAI[0]++;
            }
            for (int i = 0; i < 20; i++)
            {
                Vector2 spawnVel = Vector2.Normalize(Utils.RandomVector2(Main.rand, -10f, 11f)) * Main.rand.Next(6, 18);
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Shadowflames>());
                dust.noGravity = true;
                dust.scale = 1.5f;
                dust.alpha = Main.rand.Next(0, 200);
                dust.position = Projectile.Center + Main.rand.NextVector2Square(-10f, 11f);
                dust.velocity = spawnVel;
            }
            if (npc.CanBeChasedBy(this) && npc.whoAmI != -1)
            {
                Projectile.Center = npc.Center;
            }
        }
    }
}
