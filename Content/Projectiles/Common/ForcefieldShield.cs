using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class ForcefieldShield : ModProjectile
    {
        public override string Texture => UniverseUtils.TexturesPath + "Empty";
        public override bool? CanDamage() => false;

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 200;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 18000;
        }

        Player Player => Main.player[Projectile.owner];
        public override void AI()
        {
            for (int i = 0; i < 4; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);
                dust.position = Projectile.Center - Main.rand.NextVector2CircularEdge(10, 100);
                dust.velocity *= 0.1f;
                dust.noGravity = true;
                dust.scale = 1f;
            }
            if (Projectile.owner == Main.myPlayer)
            {
                foreach (Projectile proj in Main.ActiveProjectiles)
                {
                    var globalProj = proj.TryGetGlobalProjectile(out UniverseGlobalProjectile ugp);
                    if (!globalProj)
                    {
                        continue;
                    }

                    if (proj.whoAmI != Projectile.whoAmI && Projectile.ai[0] == 0f && proj.Hitbox.Intersects(Projectile.Hitbox) && proj.hostile && !ugp.decreasedDamage)
                    {
                        float decreasePercent = Main.rand.Next(80, 100) * 0.01f;

                        CombatText.NewText(new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Player.width, Player.height), CombatText.HealLife, $"{MathF.Ceiling(decreasePercent * 100)}%");
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            NetMessage.SendData(MessageID.PlayerHeal, -1, -1, null, proj.owner, decreasePercent);
                        }

                        proj.damage = (int)(proj.damage * decreasePercent);
                        ugp.decreasedDamage = true;
                    }
                }
            }
        }
    }
}
