using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Dusts
{
    public class PurpleBeam : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
        }

        public override bool Update(Dust dust)
        {
            dust.velocity *= 0.8f;
            dust.velocity.X += Main.rand.Next(-20, 21) * 0.04f;
            dust.velocity.Y += Main.rand.Next(-20, 21) * 0.04f;
            dust.scale -= 0.1f;
            if (dust.scale < 0.25f)
            {
                dust.active = false;
            }
            return false;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            float opacity = (255 - dust.alpha) / 255f;
            return Color.White with { A = 0 } * opacity;
        }
    }
}
