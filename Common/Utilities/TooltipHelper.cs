using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MLib.Common.Utilities;

public class TooltipHelper
{
       /// <summary>
       ///     Call this in any GlobalItem SetStaticDefaults function to use a list to register a dictionary of ItemIDs and the
       ///     tooltips keys' you want to add for those items, for adding localization entries which are to be displayed by your
       ///     mod.
       ///     You can then use the same lists in a for loop to display those tooltips in a ModifyTooltips function, using
       ///     SimpleTooltip.
       ///     If you want to add multiple tooltips to a single item, you'll have to use multiple calls and dictionaries.
       ///     Registering the same item type here and in the simple alternate function must be done in separate dictionaries too.
       /// </summary>
       public static void GlobalRegisterExtraTooltips(Mod mod, List<int> itemIDs, Dictionary<int, string> tooltips,
        string extraName)
    {
        foreach (var itemId in itemIDs)
        {
            ItemID.Search.TryGetName(itemId, out var itemName);
            var vanilla = !itemName.Contains(mod.Name);
            mod.GetLocalization(
                (vanilla
                    ? "Items.Vanilla." + ItemID.Search.GetName(itemId)
                    : ModContent.GetModItem(itemId).LocalizationCategory + "." + ModContent.GetModItem(itemId).Name) +
                "." + extraName, () => "This is an extra tooltip.");
            tooltips.Add(itemId,
                mod.GetLocalizationKey(
                    (vanilla
                        ? "Items.Vanilla." + ItemID.Search.GetName(itemId)
                        : ModContent.GetModItem(itemId).LocalizationCategory + "." +
                          ModContent.GetModItem(itemId).Name) + "." + extraName));
        }
    }

       /// <summary>
       ///     Call this in any GlobalItem SetStaticDefaults function to register a dictionary of item IDs and tooltip keys' you
       ///     want to add for a single item, for adding localization entries which are to be displayed by your mod.
       ///     You can then use the same list in a for loop to display those tooltips in a ModifyTooltips function, using
       ///     SimpleTooltip.
       ///     If you want to add multiple tooltips to a single item, you'll have to use multiple calls and dictionaries.
       /// </summary>
       public static void SimpleRegisterExtraTooltips(Mod mod, int itemId, Dictionary<int, string> tooltips,
        string extraName)
    {
        ItemID.Search.TryGetName(itemId, out var itemName);
        var vanilla = !itemName.Contains(mod.Name);
        mod.GetLocalization(
            (vanilla
                ? "Items.Vanilla." + ItemID.Search.GetName(itemId)
                : ModContent.GetModItem(itemId).LocalizationCategory + "." + ModContent.GetModItem(itemId).Name) + "." +
            extraName, () => "This is an extra tooltip.");
        tooltips.Add(itemId,
            mod.GetLocalizationKey(
                (vanilla
                    ? "Items.Vanilla." + ItemID.Search.GetName(itemId)
                    : ModContent.GetModItem(itemId).LocalizationCategory + "." + ModContent.GetModItem(itemId).Name) +
                "." + extraName));
    }

       /// <summary>
       ///     Adds a tooltip to a specific type of item.
       /// </summary>
       /// <param name="item"></param>
       /// <param name="itemId"></param>
       /// <param name="mod"></param>
       /// <param name="tooltips"></param>
       /// <param name="tipsToAdd"></param>
       public static void SimpleTooltip(int itemId, Item item, Mod mod, List<TooltipLine> tooltips,
        params string[] tipsToAdd)
    {
        if (item.type == itemId)
        {
            var ttindex = tooltips.FindLastIndex(t => t.Mod == "Terraria");
            if (ttindex != -1)
                for (var i = 0; i < tipsToAdd.Length; i++)
                    tooltips.Insert(ttindex + i + 1,
                        new TooltipLine(mod, "Extra", Language.GetTextValue(tipsToAdd[i])));
        }
    }

       /// <summary>
       ///     Adds a tooltip to all items. Generally, you want to restrict this by only running it if the item meets certain
       ///     criteria, since you rarely, if ever, should be adding tooltips to all items in the game.
       /// </summary>
       /// <param name="mod"></param>
       /// <param name="tooltips"></param>
       /// <param name="tipsToAdd"></param>
       public static void GlobalTooltip(Mod mod, List<TooltipLine> tooltips, params string[] tipsToAdd)
    {
        var ttindex = tooltips.FindLastIndex(t => t.Mod == "Terraria");
        if (ttindex != -1)
            for (var i = 0; i < tipsToAdd.Length; i++)
                tooltips.Insert(ttindex + i + 1, new TooltipLine(mod, "Extra", Language.GetTextValue(tipsToAdd[i])));
    }
}