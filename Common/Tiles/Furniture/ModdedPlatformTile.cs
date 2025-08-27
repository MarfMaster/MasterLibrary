using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace MLib.Common.Tiles.Furniture;


public abstract class ModdedPlatformTile : ModTile
{
    public abstract Color MapColor { get; }
    public abstract int OnMineDustType { get; }
    public abstract bool FunctionsInLava { get; }
    public override void SetStaticDefaults() 
    {
        // Properties
        Main.tileLighted[Type] = true;
        Main.tileFrameImportant[Type] = true;
        Main.tileSolidTop[Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileTable[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.Platforms[Type] = true;
        TileID.Sets.DisableSmartCursor[Type] = true;

        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
        AddMapEntry(MapColor);

        DustType = OnMineDustType;
        AdjTiles = [TileID.Platforms];
        VanillaFallbackOnModDeletion = TileID.Platforms;

        // Placement
        TileObjectData.newTile.CoordinateHeights = [16];
        TileObjectData.newTile.CoordinateWidth = 16;
        TileObjectData.newTile.CoordinatePadding = 2;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.StyleMultiplier = 27;
        TileObjectData.newTile.StyleWrapLimit = 27;
        TileObjectData.newTile.UsesCustomCanPlace = false;
        TileObjectData.newTile.LavaDeath = !FunctionsInLava;
        TileObjectData.newTile.LavaPlacement = FunctionsInLava ? LiquidPlacement.Allowed : LiquidPlacement.NotAllowed;
        TileObjectData.addTile(Type);
    }

    public override void PostSetDefaults() => Main.tileNoSunLight[Type] = false;

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
}