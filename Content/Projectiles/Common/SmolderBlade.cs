using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Base;
using UniverseOfSwords.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class SmolderBlade : BaseShortSword
    {
        public override string Texture => ModContent.GetInstance<Items.Weapons.SmolderBlade>().Texture;

        public override void AI()
        {
            base.AI();
            if (Projectile.localAI[1] > 0f)
            {
                Projectile.localAI[1]--;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target) && Projectile.CanHitWithMeleeWeapon(target))
            {
                Projectile.localNPCImmunity[target.whoAmI] = 6;
                target.immune[Projectile.owner] = 4;

                if (Projectile.localAI[1] <= 0f)
                {
                    Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.Center, Vector2.Zero, ProjectileID.Volcano, Projectile.damage, 10f, Projectile.owner, 0f, 0.85f + Main.rand.NextFloat() * 1.15f);
                }
                Projectile.localAI[1] = 4f;
            }
        }
    }
}
