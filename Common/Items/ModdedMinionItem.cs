using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MLib.Common.Items;

public abstract class ModdedMinionItem : ModItem
{
    public override string LocalizationCategory => "Items.Weapons.Summon.Minions";
    public abstract float SlotsRequired { get; }
    public abstract int BaseDmg { get; }
    public abstract float BaseKnockback { get; }
    public abstract int ItemRarity { get; }
    public abstract int ItemValue { get; }
    public abstract int ManaCost { get; }
    public abstract SoundStyle UseSound { get; }
    public abstract int MinionBuffType { get; }
    public abstract int MinionProjectileType { get; }

    /// <summary>
    ///     How well this minion benefits from summon tag dmg. Default is 100 here, it gets divided when applied but should be
    ///     a full number for any tooltips.
    /// </summary>
    public abstract float SummonTagDmgPercentage { get; }

    public override void SetStaticDefaults()
    {
        ItemID.Sets.GamepadWholeScreenUseRange[Item.type] =
            true; // This lets the player target anywhere on the whole screen while using a controller
        ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
        ItemID.Sets.StaffMinionSlotsRequired[Item.type] = SlotsRequired;
    }

    public override void SetDefaults()
    {
        Item.damage = BaseDmg;
        Item.knockBack = BaseKnockback;
        Item.useTime = Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.rare = ItemRarity;
        Item.value = ItemValue;
        Item.mana = ManaCost;
        Item.UseSound = UseSound;
        Item.DamageType = DamageClass.Summon;
        Item.buffType = MinionBuffType;
        Item.shoot = MinionProjectileType;
        Item.noMelee = true;
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type,
        ref int damage, ref float knockback)
    {
        // Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
        position = Main.MouseWorld;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed,
        int type, int damage, float knockBack)
    {
        player.AddBuff(Item.buffType, 2);
        if (Main.myPlayer == player.whoAmI)
        {
            var p = Projectile.NewProjectile(source, position, speed, type, damage, knockBack);
            Main.projectile[p].originalDamage = Item.damage;
        }

        return false;
    }

    public virtual void CustomModifyTooltips(List<TooltipLine> tooltips)
    {
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        if (SummonTagDmgPercentage != 100)
        {
            var ttindex = tooltips.FindLastIndex(t => t.Mod == "Terraria");
            if (ttindex != -1)
                tooltips.Insert(ttindex + 1,
                    new TooltipLine(Mod, "SummonTagDmgPercent",
                        Language.GetTextValue(Mod.GetLocalizationKey("CommonItemTooltip.Summon.TagDmgMult"),
                            (int)SummonTagDmgPercentage)));
        }

        CustomModifyTooltips(tooltips);
    }
}