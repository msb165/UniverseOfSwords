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
    public class GoldCoin : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            float opacity = (255 - dust.alpha) / 255f;
            return Color.White with { A = 0 } * opacity;
        }
    }
}
