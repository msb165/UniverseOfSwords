using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Base
{
    public class BaseVilethorn : ModProjectile
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Projectile_{ProjectileID.VilethornBase}";

        public override void SetDefaults()
        {
            Projectile.Size = new(28);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
        }

        public virtual bool CountAsBase => true;

        public virtual float MaxAmount => 6f;

        public virtual int TipToSpawn => 0;

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (Projectile.ai[0] == 0f)
            {
                Projectile.alpha -= 50;
                if (Projectile.alpha > 0)
                {
                    return;
                }
                Projectile.alpha = 0;
                Projectile.ai[0] = 1f;
                if (Projectile.ai[1] == 0f)
                {
                    Projectile.ai[1]++;
                    Projectile.position += Projectile.velocity;
                }
                if (Main.myPlayer == Projectile.owner && CountAsBase)
                {
                    int projType = Type;
                    if (Projectile.ai[1] >= MaxAmount)
                    {
                        projType = TipToSpawn;                       
                    }
                    int proj = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position + Projectile.velocity + Projectile.Size / 2, Projectile.velocity, projType, Projectile.damage, Projectile.knockBack, Projectile.owner, ai1: Projectile.ai[1] + 1f);
                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, proj);
                }
            }
            Projectile.alpha += 5;
            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }
        }

        public override bool ShouldUpdatePosition() => false;
    }
}
