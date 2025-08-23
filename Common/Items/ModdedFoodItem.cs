using Terraria.ModLoader;

namespace MLib.Common.Items;

public abstract class ModdedFoodItem : ModItem
{
    public override string LocalizationCategory => "Items.Consumables.Buffs";
    public abstract int Width { get; }
    public abstract int Height { get; }
    public abstract int BuffType { get; }
    public abstract int BuffTime { get; }
    public abstract bool UsesGulpSound { get; }
    public abstract int Rarity { get; }
    public abstract int Value { get; }

    public virtual void CustomSetDefaults()
    {
    }

    public override void SetDefaults()
    {
        Item.rare = Rarity;
        Item.value = Value;
        Item.DefaultToFood(Width, Height, BuffType, BuffTime, UsesGulpSound);
        CustomSetDefaults();
    }
}