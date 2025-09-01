using Terraria;
using Terraria.ID;
using Terraria.ModLoader.IO;
using Terraria.ModLoader;
using Terraria.GameContent.Generation;
using System.Collections.Generic;
using Terraria.WorldBuilding;
using Terraria.IO;
using UniverseOfSwordsMod.Content.Tiles;

namespace UniverseOfSwordsMod
{
    public class UniverseOfSwordsWorld : ModSystem
    {
        public static bool spawnOre = false;

        public override void OnWorldLoad()/* tModPorter Suggestion: Also override OnWorldUnload, and mirror your worldgen-sensitive data initialization in PreWorldGen */
        {
            spawnOre = false; // needed so the value is reset from world to world
        }

        public override void SaveWorldData(TagCompound tag)/* tModPorter Suggestion: Edit tag parameter instead of returning new TagCompound */
        {
            tag.Add("spawnOre", spawnOre);
        }

        public override void LoadWorldData(TagCompound tag)
        {
            spawnOre = tag.GetBool("spawnOre"); // loads the value
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            if (ShiniesIndex != -1)
            {
                tasks.Insert(ShiniesIndex + 1, new DamascusPass("Universe Of Swords Mod Ores", 237.4298f));  
            }
        }

        public class DamascusPass(string name, float loadWeight) : GenPass(name, loadWeight)
        {
            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                progress.Message = "Generating Damascus Ores";
                for (int i = 0; i < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); i++)
                {
                    WorldGen.TileRunner(
                        WorldGen.genRand.Next(0, Main.maxTilesX), // X Coord of the tile
                        WorldGen.genRand.Next((int)GenVars.worldSurfaceLow, Main.maxTilesY), // Y Coord of the tile
                        WorldGen.genRand.Next(4, 7), // Strength (High = more)
                        WorldGen.genRand.Next(3, 7), // Steps 
                        ModContent.TileType<DamascusOreTile>(), // The tile type that will be spawned
                        false, // Add Tile ???
                        0f, // Speed X ???
                        0f, // Speed Y ???
                        false, // noYChange ??? 
                        true); // Overrides existing tiles
                }
            }
        }
    }
}