using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UniverseOfSwordsMod.Content.Dusts;

namespace UniverseOfSwordsMod.Content.Tiles
{
    public class DamascusOreTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true; // Is the tile solid
            Main.tileMergeDirt[Type] = true; // Will tile merge with dirt?
            Main.tileLighted[Type] = true; // ???
            Main.tileBlockLight[Type] = true; // Emits Light
		    HitSound = SoundID.Tink;
			DustType = ModContent.DustType<DamascusSparkle>();
			
			//ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("DamascusOre").Type; // What item drops after destorying the tile
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Damascus Ore");
            AddMapEntry(new Color(246, 249, 250), name); // Colour of Tile on Map
            MinPick = 40; // What power pick minimum is needed to mine this block.
        }
		
		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 2 : 4;
		}

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.46f;
            g = 0.49f;
            b = 0.50f;
        }
    }
}