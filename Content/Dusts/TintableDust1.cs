using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Dusts
{
    public class TintableDust1 : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
        }

        public override bool Update(Dust dust)
        {
            dust.velocity *= 0.8f;
            dust.rotation -= dust.velocity.X * 0.4f;
            dust.velocity.X += Main.rand.Next(-10, 11) * 0.04f;
            dust.velocity.X *= 0.3f;
            dust.velocity.Y += Main.rand.Next(-10, 11) * 0.04f;
            dust.scale -= 0.05f;
            if (dust.scale < 0.25f)
            {
                dust.active = false;
            }
            return false;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            float opacity = (255 - dust.alpha) / 255f;
            return dust.color with { A = 0 } * opacity;
        }
    }
}
