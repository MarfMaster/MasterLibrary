using MLib.Common.Utilities;
using Terraria.ID;

namespace MLib.Common.Items;

public abstract class ModdedTrophyItem : ModdedBlockItem
{
    public override string LocalizationCategory => "Items.Tiles.Trophies";

    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.rare = ItemRarityID.Blue;
        Item.value = PriceByRarity.fromItem(Item);
    }
}