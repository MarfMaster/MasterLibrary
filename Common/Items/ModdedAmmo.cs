using Terraria;
using Terraria.ModLoader;

namespace MLib.Common.Items;

public abstract class ModdedAmmo : ModItem
{
    public abstract int ProjectileType { get; }
    public abstract int AmmoType { get; }
    public abstract int Rarity { get; }
    public abstract int Value { get; }

    public virtual void CustomSetStaticDefaults()
    {
    }

    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
        CustomSetStaticDefaults();
    }

    public virtual void CustomSetDefaults()
    {
    }

    public override void SetDefaults()
    {
        Item.shoot = ProjectileType;
        Item.ammo = AmmoType;
        Item.rare = Rarity;
        Item.value = Value;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        CustomSetDefaults();
    }
}