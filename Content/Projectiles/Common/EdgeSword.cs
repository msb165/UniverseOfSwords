using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;
using UniverseOfSwords.Utilities.Projectiles;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class EdgeSword : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.TrailingMode[Type] = 0;
        }

        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Projectile_{ProjectileID.SkyFracture}";

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.alpha = 0;
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
        }

        int attackTarget = -1;
        public override void AI()
        {
            Projectile.frame = (int)Projectile.ai[1];
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4;

            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.position, DustID.LifeDrain, Vector2.Zero, 100, Scale: 1.25f);
                dust.position = Projectile.Center - Projectile.velocity / 3f * i;
                dust.noGravity = true;
            }

            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.CanBeChasedBy(this) && attackTarget == -1 && npc.Distance(Projectile.Center) < 200f)
                {
                    attackTarget = npc.whoAmI;
                }
            }

            if (attackTarget != -1)
            {
                Projectile.timeLeft = 2;
                Vector2 speed = Vector2.Normalize(Main.npc[attackTarget].Center - Projectile.Center);
                Projectile.velocity = (Projectile.velocity * 20f + speed * 10f) / 21f;
            }
            Projectile.SimpleFadeOut(ai: 0, 30f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target))
            {
                UniverseUtils.Spawn.VampireHeal(damageDone, target.Center, target, Main.player[Projectile.owner]);
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.LifeDrain, Alpha: 100, Scale: 2.5f);
                dust.velocity *= 4f;
                dust.noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Rectangle sourceRect = new(38 * Projectile.frame, 0, 38, 38);
            Vector2 origin = sourceRect.Size() / 2;
            Color drawColor = Color.Red with { A = 0 } * Projectile.Opacity;
            Color trailColor = drawColor;
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.75f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, sourceRect, trailColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, sourceRect, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
