using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Weapons;

namespace UniverseOfSwordsMod.Common.Systems
{
    public class ChestWorldGen : ModSystem
    {
        public override void PostWorldGen()
        {
            int itemsPlaced = 0;
            int maxItemsToPlace = 6;

            for (int i = 0; i < Chest.maxItems; i++)
            {
                Chest chest = Main.chest[i];
                if (chest is null)
                {
                    continue;
                }
                Tile chestTile = Main.tile[chest.x, chest.y];
                bool isChest = chestTile.TileType == TileID.Containers;
                bool isWaterChest = isChest && chestTile.TileFrameX == 17 * 36;
                bool isWoodenChest = isChest && chestTile.TileFrameX == 0;

                if (isWoodenChest)
                {
                    for (int j = 0; j < Chest.maxItems; j++)
                    {
                        if (WorldGen.genRand.NextBool(8))
                        {
                            continue;
                        }

                        if (chest.item[j].type == ItemID.None)
                        {
                            chest.item[j].SetDefaults(ModContent.ItemType<BlowpipeSword>());
                            itemsPlaced++;
                            break;
                        }
                    }
                }

                if (isWaterChest)
                {
                    for (int j = 0; j < Chest.maxItems; j++)
                    {
                        if (WorldGen.genRand.NextBool(3))
                        {
                            continue;
                        }

                        if (chest.item[j].type == ItemID.None)
                        {
                            chest.item[j].SetDefaults(ModContent.ItemType<OceanRoar>());
                            itemsPlaced++;
                            break;
                        }
                    }
                }

                if (itemsPlaced >= maxItemsToPlace)
                {
                    break;
                }
            }
        }
    }
}
