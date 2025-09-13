using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class RainbowProj : ModProjectile
    {
        public override string Texture => UniverseUtils.TexturesPath + "Empty";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 80;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(8);
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 17;
            Projectile.extraUpdates = 2;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.noEnchantmentVisuals = true;
        }

        int attackTarget = -1;
        public override void AI()
        {
            if (Main.rand.NextBool(4))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.RainbowMk2, newColor: Main.DiscoColor with { A = 0 }, Alpha: 0, Scale: 0.75f);
                dust.position = Projectile.Center + Main.rand.NextVector2Square(-Projectile.width + 4, Projectile.width - 4) - Projectile.velocity / 2f;
                dust.noGravity = true;
                dust.velocity *= 0.25f;
            }

            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.CanBeChasedBy(this) && attackTarget == -1 && npc.Distance(Projectile.Center) < 200f)
                {
                    attackTarget = npc.whoAmI;
                }
            }
            Projectile.ai[0]++;
            if (attackTarget != -1 && Projectile.ai[0] <= 10f)
            {
                Vector2 speed = Vector2.Normalize(Main.npc[attackTarget].Center - Projectile.Center);
                float rotation = MathHelper.WrapAngle(speed.ToRotation() - Projectile.velocity.ToRotation());
                Projectile.velocity = Projectile.velocity.RotatedBy(rotation * 0.05f);
            }
            else if (attackTarget != -1 && Projectile.ai[0] >= 10f)
            {
                Vector2 speed = Vector2.Normalize(Main.npc[attackTarget].Center - Projectile.Center);
                Projectile.velocity = (Projectile.velocity * 20f + speed * 10f) / 21f;
                Projectile.ai[0] = 0f;
            }
            float length = Projectile.velocity.Length();
            Projectile.velocity.Normalize();
            Projectile.velocity *= length + 0.0025f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(ProjectileID.StardustTowerMark);
            Texture2D texture = TextureAssets.Projectile[ProjectileID.StardustTowerMark].Value;
            Vector2 drawOrigin = texture.Size() / 2;
            Color drawColor = Main.DiscoColor with { A = 0 };
            Color trailColor = drawColor;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.9f;
                float scale = MathHelper.SmoothStep(Projectile.scale, 0f, 0.025f * i);
                Vector2 trailPos = Projectile.oldPos[i] + Projectile.Size / 2 - Projectile.velocity / Projectile.oldPos.Length - Main.screenPosition;
                Main.spriteBatch.Draw(texture, trailPos, null, trailColor, Projectile.rotation, drawOrigin, scale * 0.25f, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(texture, trailPos, null, trailColor * 0.2f, Projectile.rotation, drawOrigin, scale * 0.5f, SpriteEffects.None, 0f);
            }
            return false;
        }
    }
}
