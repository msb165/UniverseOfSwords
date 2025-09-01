using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using UniverseOfSwordsMod.Content.Tiles;

namespace UniverseOfSwordsMod.Common.GlobalNPCs
{
    public class DeathEvents : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            if (npc.type == NPCID.Plantera)
            {
                if (!UniverseOfSwordsWorld.spawnOre)
                {
                    Main.NewText("The world has been cursed with Black Ore", 41, 55, 41);
                    for (int k = 0; k < (int)((double)(GenVars.rockLayer * Main.maxTilesY) * 10E-05); k++)
                    {
                        int X = WorldGen.genRand.Next(0, Main.maxTilesX);
                        int Y = WorldGen.genRand.Next((int)GenVars.rockLayer, Main.maxTilesY - 300);
                        WorldGen.OreRunner(X, Y, WorldGen.genRand.Next(2, 3), WorldGen.genRand.Next(1, 2), (ushort)ModContent.TileType<BlackOreTile>());
                    }
                }
                UniverseOfSwordsWorld.spawnOre = true;
            }
        }
    }
}
