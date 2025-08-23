using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace MLib.Common.Tiles;

public abstract class ModdedWallTile : ModWall
{
    public abstract bool PlayerPlaced { get; }
    public abstract int OnMineDustType { get; }

    /// <summary>
    ///     Determines which vanilla tile to revert to if the mod is unloaded. Defaults to 0 for Dirt.
    /// </summary>
    public abstract ushort VanillaFallbackTile { get; }

    public abstract Color MapColor { get; }

    public virtual void CustomSetStaticDefaults()
    {
    }

    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = PlayerPlaced;

        DustType = OnMineDustType;
        VanillaFallbackOnModDeletion = VanillaFallbackTile;

        AddMapEntry(MapColor);
        CustomSetStaticDefaults();
    }
}