using Terraria;
using Terraria.ModLoader;

namespace MLib.Common.Items;

public abstract class ModdedMaterial : ModItem
{
    public override string LocalizationCategory => "Items.Materials";
    public abstract int Rarity { get; }
    public abstract int Value { get; }

    public override void SetDefaults()
    {
        Item.rare = Rarity;
        Item.value = Value;
        Item.maxStack = Item.CommonMaxStack;
    }
}