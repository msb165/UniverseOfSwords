using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class Armageddon : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;               //The width of projectile hitbox
            Projectile.height = 13;          //The height of projectile hitbox
            Projectile.scale = 1.0F;
            Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true;          //Can the projectile collide with tiles?
            AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
        }

        public override void PostAI()
        {
            if (Main.rand.Next(1) == 0)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 127);
                dust.noGravity = true;
                dust.scale = 2.0f;
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 1; k++)
            {
                int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 127, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
                dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 127, Projectile.oldVelocity.X * 0.10f, Projectile.oldVelocity.Y * 0.10f);
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X, Projectile.position.Y, 0, 0, 296, (int)(Projectile.damage * 1.0), Projectile.knockBack, Main.myPlayer);
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Daybreak, 500);
        }
    }
}