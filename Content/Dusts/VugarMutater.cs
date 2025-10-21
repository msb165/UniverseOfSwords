using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Dusts
{
    public class VugarMutater : ModDust
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
