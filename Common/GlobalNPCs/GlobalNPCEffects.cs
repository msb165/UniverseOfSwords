using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Dusts;

namespace UniverseOfSwordsMod.Common.GlobalNPCs
{
    public class GlobalNPCEffects : GlobalNPC
    {
        public override bool InstancePerEntity => true;


        public bool eBlaze = false;

        public override void ResetEffects(NPC npc)
        {
            eBlaze = false;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (eBlaze)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 120000;
                if (damage < 2)
                {
                    damage = 40000;
                }
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (eBlaze)
            {
                if (Main.rand.Next(8) < 6)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f), npc.width + 4, npc.height + 4, ModContent.DustType<EmperorBlaze>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 3.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 0.5f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.NextBool(8))
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.7f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.2f, 0.7f);
            }
        }
    }
}
