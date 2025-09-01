using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class RevenantProj : ModProjectile
    {
        public override string Texture => UniverseUtils.TexturesPath + "Empty";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 120;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(8);
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 2;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 300;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }

        int attackTarget = -1;
        public override void AI()
        {
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.SpectreStaff, Scale: 2f, newColor: Color.Cyan);
                dust.noGravity = true;
                dust.position = Projectile.position + Main.rand.NextVector2Square(-Projectile.width / 2 + 4, Projectile.width / 2 + 4) - Projectile.velocity / 3f * i;
                dust.velocity *= 0f;

                Dust dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.SpectreStaff, Scale: 1.25f, newColor: Color.Cyan);
                dust2.noGravity = true;
                dust2.position = Projectile.Center;
                dust2.velocity = Projectile.velocity.RotatedBy(MathHelper.PiOver4 * Projectile.direction) * 0.33f;

                Dust dust3 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.SpectreStaff, Scale: 1.25f, newColor: Color.Cyan);
                dust3.noGravity = true;
                dust3.position = Projectile.Center;
                dust3.velocity = Projectile.velocity.RotatedBy(-MathHelper.PiOver4 * Projectile.direction) * 0.33f;

            }

            Lighting.AddLight(Projectile.Center, Color.Cyan.ToVector3());

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
                float velMulti = Main.npc[attackTarget].velocity.Length() * 1.25f;
                if (MathF.Abs(velMulti) <= 1f)
                {
                    velMulti = 6f;
                }
                Projectile.velocity = (Projectile.velocity * 20f + speed * velMulti) / 21f;
            }
            //Projectile.velocity = Vector2.Normalize(Projectile.velocity) * (Projectile.velocity.Length() + 0.0025f);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.SpectreStaff, Scale: 2f, newColor: Color.Cyan);
                dust.noGravity = true;
                dust.velocity += -Projectile.oldVelocity / 4f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(ProjectileID.StardustTowerMark);
            Texture2D texture = TextureAssets.Projectile[ProjectileID.StardustTowerMark].Value;
            Vector2 origin = texture.Size() / 2;
            Color trailColor = Color.Cyan with { A = 0 } * Projectile.Opacity;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.875f;
                float scale = MathHelper.SmoothStep(Projectile.scale * 0.35f, Projectile.scale * 2f, 0.02f * i);
                Vector2 drawPos = Projectile.oldPos[i] + Projectile.Size / 2 - Projectile.velocity - Main.screenPosition;
                Main.spriteBatch.Draw(texture, drawPos, null, trailColor * 0.25f, Projectile.oldRot[i], origin, scale, SpriteEffects.None, 0f);
            }

            return false;
        }
    }
}
