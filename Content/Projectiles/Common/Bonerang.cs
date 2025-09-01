using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class Bonerang : ModProjectile
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Projectile_{ProjectileID.BoneGloveProj}";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.scale = 3f;
            Projectile.friendly = true;
            Projectile.penetrate = 3;
            Projectile.extraUpdates = 1;
            //Projectile.usesLocalNPCImmunity = true;
            //Projectile.localNPCHitCooldown = 10;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.alpha = 255;
        }

        Player Player => Main.player[Projectile.owner];
        public ref float Timer => ref Projectile.ai[0];
        public override void AI()
        {
            float ai1 = Projectile.ai[1] + 65f;
            Timer++;
            Projectile.localAI[0]++;
            float remapValue = Utils.Remap(Projectile.localAI[0], Projectile.ai[1] * 0.4f, ai1, 0f, 1f);
            Projectile.Center = Player.RotatedRelativePoint(Player.Center) - Projectile.velocity + Projectile.velocity * remapValue * remapValue * 77f;
            Projectile.rotation += 0.4f * Projectile.direction;
            if (Projectile.alpha >= 0)
            {
                Projectile.alpha -= 8;
            }
            if (Timer >= 30f)
            {
                Vector2 targetVel = Vector2.Normalize(Player.Center - Projectile.Center);
                Projectile.velocity = Projectile.velocity.MoveTowards(targetVel, 0.4f);
                if (Projectile.Hitbox.Intersects(Player.Hitbox))
                {
                    Projectile.Kill();
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            UniverseUtils.Drawing.DrawWithAfterImages(Projectile, Projectile.GetAlpha(lightColor));
            return false;
        }
    }
}
