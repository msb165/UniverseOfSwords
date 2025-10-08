using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Dusts;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class ChlorophyteBolt : ModProjectile
    {
        public override string Texture => UniverseUtils.TexturesPath + "Empty";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(14);
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 200;
            Projectile.noEnchantmentVisuals = true;
            Projectile.penetrate = 1;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
        }

        public int attackTarget = -1;
        float velocityLength = 0f;

        public override void AI()
        {
            if (Projectile.localAI[0] == 0f)
            {
                velocityLength = Projectile.velocity.Length();
                Projectile.localAI[0] = 1f;
            }
            for (int i = 0; i < 3; i++)
            {
                Vector2 pos = Projectile.Center - Projectile.velocity / 3f * i;
                Dust dust = Dust.NewDustPerfect(pos, ModContent.DustType<Chloroflames>());
                dust.scale = 1.5f;
                dust.position = pos;
                dust.velocity = Vector2.Zero;
                dust.noGravity = true;
            }
            FindNPCAndApplySpeed(velocityLength);
        }

        public void FindNPCAndApplySpeed(float multiplier)
        {
            NPC npc = UniverseUtils.Misc.FindTargetWithinRange(Projectile, 200f);
            if (npc != null)
            {
                attackTarget = npc.whoAmI;
                Projectile.netUpdate = true;
            }

            if (attackTarget != -1 && Main.npc[attackTarget].active)
            {
                Projectile.timeLeft = 2;
                Vector2 speed = Vector2.Normalize(Main.npc[attackTarget].Center - Projectile.Center);
                Projectile.velocity = (Projectile.velocity * 10f + speed * multiplier) / 11f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for (int i = 0; i < 30; i++)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<Chloroflames>());
                dust.scale *= 2f;
                dust.velocity = -(Projectile.oldVelocity / 2f).RotatedByRandom(MathHelper.ToRadians(5f)) * Main.rand.NextFloat(0.5f, 1.2f);
                dust.noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadNPC(NPCID.Shimmerfly);
            Texture2D value = TextureAssets.Npc[NPCID.Shimmerfly].Value;
            Rectangle sourceRect = value.Frame(4, 5, 2);
            Vector2 glowOrigin = sourceRect.Size() / 2;
            Color drawColor = new(179, 252, 0, 0);
            Color glowColor = drawColor;
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                glowColor *= 0.9f;
                Main.spriteBatch.Draw(value, Projectile.oldPos[i] + Projectile.Size / 2 - Projectile.velocity - Main.screenPosition, sourceRect, glowColor, Projectile.rotation, glowOrigin, Projectile.scale * 1.25f, spriteEffects, 0f);
            }

            return false;
        }
    }
}
