using Terraria.ID;
using Terraria.ModLoader;

namespace MLib.Common.Items;


public abstract class ModdedBossRelicItem : ModdedTrophyItem
{
    public override void SetDefaults() 
    {
        base.SetDefaults();

        Item.width = 30;
        Item.height = 40;
        Item.rare = ItemRarityID.Master;
        Item.master = true; // This makes sure that "Master" displays in the tooltip, as the rarity only changes the item name color
    }
}