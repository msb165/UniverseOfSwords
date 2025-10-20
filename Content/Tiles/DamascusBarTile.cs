using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;

namespace UniverseOfSwords.Content.Tiles
{
    public class DamascusBarTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true; // Is the tile solid
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1); // This tile will copy a 1 x 1 tile such as bars
            TileObjectData.addTile(Type);

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(246, 249, 250), name); // Colour of Tile on Map

            MinPick = 40; // What power pick minimum is needed to mine this block.
        }
    }
}