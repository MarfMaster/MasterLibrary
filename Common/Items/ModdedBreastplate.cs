using Terraria;
using Terraria.ModLoader;

namespace MLib.Common.Items;

public abstract class ModdedBreastplate : ModItem
{
    public abstract int Defense { get; }
    public abstract int Rarity { get; }
    public abstract int Value { get; }
    public override string LocalizationCategory => "Items.Armor";

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
}