using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace MLib.Common.Items;

public abstract class ModdedBuffItem : ModItem
{
    public override string LocalizationCategory => "Items.Consumables.Buffs";
    public abstract int Width { get; }
    public abstract int Height { get; }
    public abstract int BuffType { get; }
    public abstract int BuffTime { get; }
    public abstract int ItemUseStyle { get; }
    public abstract SoundStyle ItemUseSound { get; }
    public abstract int Rarity { get; }
    public abstract int Value { get; }

    public virtual void CustomSetDefaults()
    {
    }

    public override void SetDefaults()
    {
        Item.width = Width;
        Item.height = Height;
        Item.rare = Rarity;
        Item.value = Value;
        Item.consumable = true;
        Item.buffType = BuffType;
        Item.buffTime = BuffTime;
        Item.useStyle = ItemUseStyle;
        Item.UseSound = ItemUseSound;
        Item.maxStack = Item.CommonMaxStack;
        Item.useTurn = true;
        Item.useAnimation = 15;
        Item.useTime = 15;
        CustomSetDefaults();
    }
}