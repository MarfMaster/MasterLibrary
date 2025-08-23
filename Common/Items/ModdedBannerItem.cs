using Terraria.ModLoader;

namespace MLib.Common.Items;

public abstract class ModdedBannerItem : ModItem
{
    public override string LocalizationCategory => "Items.Tiles.Banners";
    public abstract int ModBannerTileType { get; }
    public abstract int StyleId { get; }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModBannerTileType, StyleId);
        Item.width = 10;
        Item.height = 24;
    }
}