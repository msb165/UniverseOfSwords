using FullSerializer.Internal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;
using UniverseOfSwords.Utilities.Projectiles;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class RedBeamV3 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 120;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 90 * Projectile.MaxUpdates;
            Projectile.tileCollide = false;
            Projectile.noEnchantmentVisuals = true;
        }

        public int attackTarget = -1;
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.SimpleFadeOut(ai: 0, maxTime: 30f);
            FindNPCAndApplySpeed(8f);
        }

        public void FindNPCAndApplySpeed(float multiplier)
        {
            NPC npc = UniverseUtils.Misc.FindTargetWithinRange(Projectile, 400f);
            if (npc != null)
            {
                attackTarget = npc.whoAmI;
            }

            if (attackTarget != -1 && Main.npc[attackTarget].active)
            {
                Projectile.timeLeft = 2;
                Vector2 speed = Vector2.Normalize(Main.npc[attackTarget].Center - Projectile.Center);
                Projectile.velocity = (Projectile.velocity * 20f + speed * multiplier) / 21f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            Projectile.Resize(80, 80);
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Clentaminator_Red, Scale: 2.5f);
                dust.noGravity = true;
                dust.velocity *= 2f;
                Dust dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Clentaminator_Red, Scale: 1f);
                dust2.scale = 1.5f;
                dust2.noGravity = true;
                dust2.velocity *= 1.2f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadNPC(NPCID.Shimmerfly);
            Texture2D value = TextureAssets.Npc[NPCID.Shimmerfly].Value;
            Rectangle sourceRect = value.Frame(4, 5, 2);
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 origin = texture.Size() / 2;
            Vector2 glowOrigin = sourceRect.Size() / 2;
            Color drawColor = Color.White with { A = 0 };
            Color trailColor = drawColor;
            Color glowColor = Color.Red with { A = 0 };
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.8f;
                glowColor *= 0.8f;
                Main.spriteBatch.Draw(value, Projectile.oldPos[i] + Projectile.Size / 2 - Projectile.oldVelocity / Projectile.oldPos.Length * i - Main.screenPosition, sourceRect, glowColor, Projectile.rotation, glowOrigin, Projectile.scale, spriteEffects, 0f);
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Projectile.oldVelocity / Projectile.oldPos.Length * i - Main.screenPosition, null, trailColor, Projectile.oldRot[i], origin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(value, Projectile.Center - Main.screenPosition, sourceRect, glowColor, Projectile.rotation, glowOrigin, Projectile.scale, spriteEffects, 0f);
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
