using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace MLib.Common.Tiles.Furniture;

public abstract class ModdedWorkbenchTile : ModTile
{
    public const int MaterialAmount = 10;
    public abstract int OnMineDustType { get; }
    public abstract Color MapColor { get; }

    public override void SetStaticDefaults()
    {
        // Properties
        Main.tileTable[Type] = true;
        Main.tileSolidTop[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileFrameImportant[Type] = true;
        TileID.Sets.DisableSmartCursor[Type] = true;
        TileID.Sets.IgnoredByNpcStepUp[Type] =
            true; // This line makes NPCs not try to step up this tile during their movement. Only use this for furniture with solid tops.

        DustType = OnMineDustType;
        AdjTiles = [TileID.WorkBenches];

        // Placement
        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
        TileObjectData.newTile.CoordinateHeights = [18];
        TileObjectData.addTile(Type);

        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);

        // Etc
        AddMapEntry(MapColor, Language.GetText("ItemName.WorkBench"));
    }

    public override void NumDust(int x, int y, bool fail, ref int num)
    {
        num = fail ? 1 : 3;
    }
}