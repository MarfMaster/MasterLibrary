using Terraria.ModLoader;

namespace MLib.Common.Items;

public abstract class ModdedBlockItem : ModItem
{
    public override string LocalizationCategory => "Items.Tiles.Blocks";
    public abstract int TileType { get; }

    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(TileType);
    }
}