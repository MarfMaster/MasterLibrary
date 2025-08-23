using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace MLib.Common.Tiles;

public abstract class ModdedSandBlockTile : ModdedBlockTile
{
    public abstract int SandballProjectileType { get; }

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();

        Main.tileBrick[Type] = true;
        Main.tileSand[Type] = true;
        TileID.Sets.Conversion.Sand[Type] = true;
        TileID.Sets.ForAdvancedCollision.ForSandshark[Type] = true;
        TileID.Sets.CanBeDugByShovel[Type] = true;
        TileID.Sets.Falling[Type] = true;
        TileID.Sets.Suffocate[Type] = true;
        TileID.Sets.FallingBlockProjectile[Type] = new TileID.Sets.FallingBlockProjectileInfo(SandballProjectileType);
        TileID.Sets.isDesertBiomeSand[Type] = true;
        TileID.Sets.SandBiome[Type] = 1;

        TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
        TileID.Sets.GeneralPlacementTiles[Type] = false;
        TileID.Sets.ChecksForMerge[Type] = true;

        MineResist *= 0.5f;
    }

    public override bool HasWalkDust()
    {
        return true;
    }

    public override void WalkDust(ref int dustType, ref bool makeDust, ref Color color)
    {
        makeDust = Main.rand.NextBool(3);
        dustType = OnMineDustType;
    }
}