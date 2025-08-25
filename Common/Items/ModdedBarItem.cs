namespace MLib.Common.Items;

public abstract class ModdedBarItem : ModdedBlockItem
{
    public override string LocalizationCategory => "Items.Tiles.Bars";

    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }
}