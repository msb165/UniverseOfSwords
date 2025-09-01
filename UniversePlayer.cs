using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;

namespace UniverseOfSwordsMod
{
    public class UniversePlayer : ModPlayer
    {
        public int meleeCD;
        public bool eBlaze = false;

        public override void ResetEffects()
        {
            eBlaze = false;
        }

        public override void PostUpdate()
        {
            if (meleeCD > 0)
            {
                meleeCD--;
            }
            //Main.NewText(meleeCD);
        }


        public override void UpdateBadLifeRegen()
        {
            if (eBlaze)  // make sure you add the right bool
            {
                //Player.lifeRegen -= 40000; //this make so the player take damage, the highter is the value the more life losing.
                Player.lifeRegen -= 4; //this make so the player take damage, the highter is the value the more life losing.
            }
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (eBlaze)
            {
                if (Main.rand.NextBool(8) && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.Position - Vector2.One * 2f, Player.width + 4, Player.height + 4, Mod.Find<ModDust>("EmperorBlaze").Type, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default, 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 0.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    //Main.playerDrawDust.Add(dust);
                }
                r *= 1.0f;
                g *= 0.5f;
                b *= 0.0f;
                fullBright = true;
            }
        }

        public virtual bool ConsumeAmmo(Item weapon, Item ammo)
        {
            return true;
        }
    }
}