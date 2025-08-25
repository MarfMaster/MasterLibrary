using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace MLib.Common.Tiles
{
	/// <summary>
	/// Allows for multiple horizontal placement styles, simply add alternate style sprites at the same size as the first horizontally
	/// </summary>
	public abstract class ModdedBiomeFishingCrateTile : ModTile
	{
		public abstract Color MapColor { get; }
		public override void SetStaticDefaults() 
		{
			// Properties
			Main.tileFrameImportant[Type] = true;
			Main.tileSolidTop[Type] = true;
			Main.tileTable[Type] = true;

			// Placement
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.CoordinateHeights = [16, 18];
			TileObjectData.newTile.StyleHorizontal = true; // Optional, if you add more placeStyles for the item 
			TileObjectData.addTile(Type);

			// Etc
			LocalizedText name = CreateMapEntryName();
			AddMapEntry(MapColor, name);
		}

		public override bool CreateDust(int i, int j, ref int type) 
		{
			return false;
		}
	}
}
