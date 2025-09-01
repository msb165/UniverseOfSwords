using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class TrueTerrablade : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 6;
        }

        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 30;
            Projectile.scale = 1.3F;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 107);
            dust.noGravity = true;
            dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 107);
            dust.noGravity = true;
            dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 107);
            dust.scale = 1.0f;
            Projectile.rotation = Projectile.velocity.ToRotation() + (float)(Math.PI / 4);
        }

        public override bool PreDraw(ref Color lightColor) //this is where the animation happens
        {
            Projectile.frameCounter++; //increase the frameCounter by one
            if (Projectile.frameCounter >= 6) //once the frameCounter has reached 10 - change the 10 to change how fast the projectile animates
            {
                Projectile.frame++; //go to the next frame
                Projectile.frameCounter = 0; //reset the counter
                if (Projectile.frame > 5) //if past the last frame
                    Projectile.frame = 0; //go back to the first frame
            }
            return true;
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X + 40, Projectile.position.Y + 40, 0, 10, 132, Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X + 40, Projectile.position.Y + 40, 0, -10, 132, Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X + 40, Projectile.position.Y + 40, 10, 10, 132, Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X + 40, Projectile.position.Y + 40, -10, -10, 132, Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X + 40, Projectile.position.Y + 40, -10, 10, 132, Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X + 40, Projectile.position.Y + 40, 10, -10, 132, Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X + 40, Projectile.position.Y + 40, 20, 10, 132, Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X + 40, Projectile.position.Y + 40, -20, -10, 132, Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X + 40, Projectile.position.Y + 40, -20, 10, 132, Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X + 40, Projectile.position.Y + 40, 20, -10, 132, Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X + 40, Projectile.position.Y + 40, 40, 10, 132, Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X + 40, Projectile.position.Y + 40, -40, -10, 132, Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X + 40, Projectile.position.Y + 40, -40, 10, 132, Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X + 40, Projectile.position.Y + 40, 40, -10, 132, Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X + 40, Projectile.position.Y + 40, 80, 10, 132, Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X + 40, Projectile.position.Y + 40, -80, -10, 132, Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X + 40, Projectile.position.Y + 40, -80, 10, 132, Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X + 40, Projectile.position.Y + 40, 80, -10, 132, Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X + 40, Projectile.position.Y + 40, -120, 5, 132, Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X + 40, Projectile.position.Y + 40, 120, -5, 132, Projectile.damage, 0, Main.myPlayer, 0f, 0f);
        }
    }
}