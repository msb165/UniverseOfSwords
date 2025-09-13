using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
	public class SOTUV6Projectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sotu Projectile 6");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults()
		{
			Projectile.width = 15;               //The width of projectile hitbox
			Projectile.height = 75; 			//The height of projectile hitbox
			Projectile.scale = 3.0F;
			Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in)
			Projectile.light = 1f;
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
			AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
            Projectile.timeLeft = 300;
        }

        public virtual void CreateDust()
		{
            if (Main.rand.Next(1) == 0)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TreasureSparkle, 0f, 0f, 100, default, 2f);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 0.2f;
				Main.dust[dust].velocity += Projectile.velocity;
			}
		}		
    }
}