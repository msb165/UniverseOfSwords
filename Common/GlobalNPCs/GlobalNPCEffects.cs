using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Common.GlobalNPCs
{
    public class GlobalNPCEffects : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool eBlaze, slow, sVenom;

        public override void ResetEffects(NPC npc)
        {
            eBlaze = false;
            slow = false;
            sVenom = false;
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (sVenom)
            {
                modifiers.Defense.Flat -= 80;
            }
            if (eBlaze)
            {
                modifiers.Defense.Flat -= 5;
            }
        }


        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (eBlaze)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 120;
                if (damage < 30)
                {
                    damage = 30;
                }
            }
            if (sVenom)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 2000;
                if (damage < 200)
                {
                    damage = 200;
                }
            }
        }

        public override void PostAI(NPC npc)
        {
            if (slow)
            {
                npc.velocity = npc.velocity.SafeNormalize(Vector2.Zero);
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (slow)
            {
                drawColor = Color.DarkGray;
            }
            if (sVenom)
            {
                drawColor = Color.Lerp(drawColor, Color.Lime, 0.25f);
                if (Main.rand.Next(8) < 6)
                {
                    Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Clentaminator_Green, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 1f);
                    dust.noGravity = true;
                    dust.velocity *= 0.5f;
                    dust.velocity.Y -= 0.5f;
                    if (Main.rand.NextBool(8))
                    {
                        dust.noGravity = false;
                        dust.scale *= 0.7f;
                    }
                }
            }
            if (eBlaze)
            {
                if (Main.rand.Next(8) < 6)
                {
                    Dust dust = Dust.NewDustDirect(npc.position - new Vector2(2f), npc.width + 4, npc.height + 4, ModContent.DustType<Content.Dusts.EmperorBlaze>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 3.5f);
                    dust.noGravity = true;
                    dust.velocity *= 0.5f;
                    dust.velocity.Y -= 0.5f;
                    if (Main.rand.NextBool(8))
                    {
                        dust.noGravity = false;
                        dust.scale *= 0.7f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.2f, 0.7f);
            }
        }
    }
}
