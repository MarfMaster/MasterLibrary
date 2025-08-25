using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MLib.Common.Items;

/// <summary>
///     Requires for you to add a custom fishing condition in ModPlayer to be catchable.
/// </summary>
public abstract class ModdedQuestFishItem : ModItem
{
    public override string LocalizationCategory => "Items.QuestFish";

    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 2;
        ItemID.Sets.CanBePlacedOnWeaponRacks[Type] = true; // All vanilla fish can be placed in a weapon rack.

        this.GetLocalization("Description", () => "How the angler describes this fish.");
        this.GetLocalization("CatchLocation",
            () => "The conditions this fish can be caught under, seen at the bottom of the Quest popup.");
    }

    public override void SetDefaults()
    {
        // DefaultToQuestFish sets quest fish properties.
        // Of note, it sets rare to ItemRarityID.Quest, which is the special rarity for quest items.
        // It also sets uniqueStack to true, which prevents players from picking up a 2nd copy of the item into their inventory.
        Item.DefaultToQuestFish();
    }

    public override bool IsQuestFish()
    {
        return true;
        // Makes the item a quest fish
    }

    public override bool IsAnglerQuestAvailable()
    {
        return false;
        // Makes the quest only appear in hard mode. Adding a '!' before Main.hardMode makes it ONLY available in pre-hardmode.
    }

    public override void AnglerQuestChat(ref string description, ref string catchLocation)
    {
        // How the angler describes the fish to the player.
        description = Language.GetTextValue(this.GetLocalizationKey("Description"));
        // What it says on the bottom of the angler's text box of how to catch the fish.
        catchLocation = Language.GetTextValue(this.GetLocalizationKey("CatchLocation"));
    }
}