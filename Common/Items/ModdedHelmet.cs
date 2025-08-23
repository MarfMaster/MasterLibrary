using MLib.Common.Utilities;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MLib.Common.Items;

public abstract class ModdedHelmet : ModItem
{
    public static string SetBonusTextLocation;
    public abstract int Defense { get; }
    public abstract int Rarity { get; }
    public abstract int Value { get; }

    /// <summary>
    ///     Whether this helmet belongs to an armor set and has a bonus or not. Name this something like "Chitinite" for the
    ///     Chitinite armor set.
    ///     If it has no set bonus, don't assign a value to this.
    ///     Override ArmorSetBonus to add the effects of the set bonus when the full set is worn.
    /// </summary>
    public abstract string HasArmorSetBonusName { get; }

    /// <summary>
    ///     The ItemID of the body type that belongs to this helmets armor set, set to 0 if there is none.
    /// </summary>
    public abstract int BodyType { get; }

    /// <summary>
    ///     The ItemID of the legs type that belongs to this helmets armor set, set to 0 if there is none.
    /// </summary>
    public abstract int LegsType { get; }

    public override string LocalizationCategory => "Items.Armor";
    public abstract float SetBonusStat0 { get; }
    public abstract float SetBonusStat1 { get; }
    public abstract float SetBonusStat2 { get; }
    public abstract float SetBonusStat3 { get; }

    public override void SetStaticDefaults()
    {
        if (HasArmorSetBonusName != null)
        {
            SetBonusTextLocation = LocalizationCategory + "." + HasArmorSetBonusName + "SetBonus";
            Mod.GetLocalization(SetBonusTextLocation, () => "This armor set bonus hasn't been described yet.");
        }
    }

    public override void SetDefaults()
    {
        Item.defense = Defense;
        Item.rare = Rarity;
        Item.value = Value;
    }

    /// <summary>
    ///     Override this to add any effects to this armor piece that get applied to the player when equipped.
    /// </summary>
    /// <param name="player"></param>
    public virtual void EquipEffects(Player player)
    {
    }

    public override void UpdateEquip(Player player)
    {
        EquipEffects(player);
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        if (HasArmorSetBonusName == null) return false;
        if (BodyType != 0 && LegsType != 0) return body.type == BodyType && legs.type == LegsType;
        if (BodyType != 0 && LegsType == 0) return body.type == BodyType;
        if (BodyType == 0 && LegsType != 0) return legs.type == LegsType;
        return false;
    }

    public virtual void ArmorSetBonus(Player player)
    {
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = Language.GetTextValue(Mod.GetLocalizationKey(SetBonusTextLocation), SetBonusStat0, SetBonusStat1, SetBonusStat2,
            SetBonusStat3);
        ArmorSetBonus(player);
    }
}