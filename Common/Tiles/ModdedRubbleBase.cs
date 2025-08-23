using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace MLib.Common.Tiles;

public abstract class ModdedRubbleBase : ModTile
{
    // We want both tiles to use the same texture
    public override string Texture => TexturePath;
    public abstract string TexturePath { get; }
    public abstract int OnMineDustType { get; }
    public abstract TileObjectData TileStyle { get; }
    public abstract bool Grounded { get; }
    public abstract bool Hanging { get; }
    public abstract Color MapColor { get; }

    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileNoFail[Type] = true;
        Main.tileObsidianKill[Type] = true;

        DustType = OnMineDustType;

        TileObjectData.newTile.CopyFrom(TileStyle);
        if (Grounded)
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
        if (Hanging)
        {
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
        }

        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.DrawYOffset = Grounded ? 2 : -2;

        TileObjectData.addTile(Type);

        AddMapEntry(MapColor);
    }
}

// This is the fake tile that will be placed by the Rubblemaker.
public abstract class ModdedRubbleFake : ModdedRubbleBase
{
    public abstract int MaterialItemType { get; }

    public virtual void RubblePlacementLine()
    {
    }

    public override void SetStaticDefaults()
    {
        // Call to base SetStaticDefaults. Must inherit static defaults from base type 
        base.SetStaticDefaults();

        // Add rubble variant, all existing styles, to Rubblemaker, allowing to place this tile by consuming ExampleBlock
        RubblePlacementLine();

        // Tiles placed by Rubblemaker drop the item used to place them.
        RegisterItemDrop(MaterialItemType);
    }
}

// This is the natural tile, this version is placed during world generation in the RubbleWorldGen class.
public abstract class ModdedRubbleNatural : ModdedRubbleBase
{
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();

        // Only the natural version is broken automatically by placing other tiles over it or block swapping under it.
        TileID.Sets.BreakableWhenPlacing[Type] = true;
        TileID.Sets.ReplaceTileBreakUp[Type] = true;

        // By default, the TileObjectData.Style2x1 tile we copied in Example2x1RubbleBase has LavaDeath = true. Natural rubble tiles don't have this behavior, so we want to be immune to lava.
        TileObjectData.GetTileData(Type, 0).LavaDeath = false;
    }
}