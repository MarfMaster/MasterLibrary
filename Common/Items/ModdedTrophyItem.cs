using MLib.Common.Utilities;
using Terraria.ID;

namespace MLib.Common.Items;

public abstract class ModdedTrophyItem : ModdedFurnitureItem
{
    public override string LocalizationCategory => "Items.Tiles.Trophies";
    public override bool Craftable => false;
    public override int MaterialType => 0;
    public override int MaterialAmount => 0;

    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.rare = ItemRarityID.Blue;
        Item.value = 50000;
    }
}