using Terraria.Audio;
using Terraria.ModLoader;

namespace MLib.Common.Items;

public abstract class ModdedBasicItem : ModItem
{
    public abstract int Damage { get; }
    public abstract int UseTime { get; }
    public abstract int ItemUseAnimation { get; }
    public abstract float Knockback { get; }
    public abstract DamageClass DamageType { get; }

    /// <summary>
    ///     How this item is held/used by the player when used. Pick from the ItemUseStyleID list.
    /// </summary>
    public abstract int ItemUseStyle { get; }

    /// <summary>
    ///     Whether the items sprite should deal damage when in contact with an enemy. Set to false for things like Bows or
    ///     Staves.
    /// </summary>
    public abstract bool DealsContactDamage { get; }

    public abstract int Width { get; }
    public abstract int Height { get; }

    /// <summary>
    ///     The rarity of the item. This determines the color of the item's name.
    /// </summary>
    public abstract int Rarity { get; }

    /// <summary>
    ///     The value of the item determines how much it sells for and how much it costs to reforge. I recommend using
    ///     PriceByRarity here.
    /// </summary>
    public abstract int Value { get; }

    /// <summary>
    ///     Which sound it'll play when the item is used/swung. Default is "SoundID.Item1" for a normal swing sound, you can
    ///     also enter null for no sound.
    /// </summary>
    public abstract SoundStyle UseSound { get; }

    /// <summary>
    ///     Whether the player can turn around while the item is being used/swung. Should set this to true for swung items like
    ///     swords or pickaxes, but not for something that you aim with, like bows or staves.
    /// </summary>
    public abstract bool UseTurn { get; }

    public virtual void CustomSetDefaults()
    {
    }

    public override void SetDefaults()
    {
        Item.damage = Damage;
        Item.useTime = UseTime;
        Item.useAnimation = ItemUseAnimation;
        Item.knockBack = Knockback;
        Item.width = Width;
        Item.height = Height;
        Item.rare = Rarity;
        Item.value = Value;
        Item.DamageType = DamageType;
        Item.useStyle = ItemUseStyle;
        Item.noMelee = !DealsContactDamage;
        Item.UseSound = UseSound;
        Item.useTurn = UseTurn;
        CustomSetDefaults();
    }
}