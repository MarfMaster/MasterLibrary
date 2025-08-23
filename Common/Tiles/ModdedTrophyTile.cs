using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace MLib.Common.Tiles;

public abstract class ModdedTrophyTile : ModTile
{
    /// <summary>
    ///     Determines which vanilla tile to revert to if the mod is unloaded. Defaults to 0 for Dirt.
    /// </summary>
    public abstract ushort VanillaFallbackTile { get; }

    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.FramesOnKillWall[Type] = true;
        VanillaFallbackOnModDeletion = VanillaFallbackTile;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
        TileObjectData.addTile(Type);

        AddMapEntry(new Color(120, 85, 60), Language.GetText("MapObject.Trophy"));
        DustType = 7;
    }
}