using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UniverseOfSwordsMod.Content.Tiles
{
    public class BlackOreTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true; // Is the tile solid
            Main.tileMergeDirt[Type] = true; // Will tile merge with dirt?
            Main.tileLighted[Type] = true; // ???
            Main.tileBlockLight[Type] = true; // Emits Light
			DustType = Mod.Find<ModDust>("BlackOre").Type;
		    HitSound = SoundID.Tink;
			
			//ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("BlackOre").Type; // What item drops after destorying the tile
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Black Ore");
            AddMapEntry(new Color(000, 000, 000), name); // Colour of Tile on Map
            MinPick = 200; // What power pick minimum is needed to mine this block.
        }
		
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.22f;
            g = 0.32f;
            b = 0.22f;
        }
    }
}