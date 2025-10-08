using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;
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
            //ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("DamascusBar").Type; // What item drops after destorying the tile

            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Damascus Bar");

            AddMapEntry(new Color(246, 249, 250), name); // Colour of Tile on Map

            MinPick = 40; // What power pick minimum is needed to mine this block.
        }
    }
}