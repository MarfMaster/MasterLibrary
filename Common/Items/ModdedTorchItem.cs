using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MLib.Common.Items;

public abstract class ModdedTorchItem : ModItem
{
    public abstract int TorchTileType { get; }
    public abstract Color LightColor { get; }
    public abstract int TorchCraftAmount { get; }
    public abstract int MaterialItemType { get; }
    public abstract int SparkleDustType { get; }
    public abstract bool CanFunctionInWater { get; }
    public abstract bool CanFunctionInLava { get; }
    public abstract int Rarity { get; }
    public abstract int Value { get; }

    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;

        ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.ShimmerTorch;
        ItemID.Sets.SingleUseInGamepad[Type] = true;
        ItemID.Sets.Torches[Type] = true;
    }

    public override void SetDefaults()
    {
        // DefaultToTorch sets various properties common to torch placing items. Hover over DefaultToTorch in Visual Studio to see the specific properties set.
        // Of particular note to torches are Item.holdStyle, Item.flame, and Item.noWet. 
        Item.DefaultToTorch(TorchTileType, 0);
        Item.rare = Rarity;
        Item.value = Value;
    }

    public override void HoldItem(Player player)
    {
        // This torch cannot be used in water, so it shouldn't spawn particles or light either
        if (player.wet && !CanFunctionInWater) return;

        if (player.lavaWet && !CanFunctionInLava) return;

        // Note that due to biome select torch god's favor, the player may not actually have an ExampleTorch in their inventory when this hook is called, so no modifications should be made to the item instance.

        // Randomly spawn sparkles when the torch is held. Bigger chance to spawn them when swinging the torch.
        if (Main.rand.NextBool(player.itemAnimation > 0 ? 7 : 30))
        {
            var dust = Dust.NewDustDirect(
                new Vector2(player.itemLocation.X + (player.direction == -1 ? -16f : 6f),
                    player.itemLocation.Y - 14f * player.gravDir), 4, 4, SparkleDustType, 0f, 0f, 100);
            if (!Main.rand.NextBool(3)) dust.noGravity = true;

            dust.velocity *= 0.3f;
            dust.velocity.Y -= 1.5f;
            dust.position = player.RotatedRelativePoint(dust.position);
        }

        var position = player.RotatedRelativePoint(
            new Vector2(player.itemLocation.X + 12f * player.direction + player.velocity.X,
                player.itemLocation.Y - 14f + player.velocity.Y), true);

        Lighting.AddLight(position, LightColor.R, LightColor.G, LightColor.B);
    }

    public override void PostUpdate()
    {
        if (CanFunctionInWater && CanFunctionInLava)
        {
            Lighting.AddLight(Item.Center, LightColor.R, LightColor.G, LightColor.B);
        }
        else if (!CanFunctionInWater && CanFunctionInLava)
        {
            if (!Item.wet) Lighting.AddLight(Item.Center, LightColor.R, LightColor.G, LightColor.B);
        }
        else if (CanFunctionInWater && !CanFunctionInLava)
        {
            if (!Item.lavaWet) Lighting.AddLight(Item.Center, LightColor.R, LightColor.G, LightColor.B);
        }
        else if (!Item.lavaWet && !Item.wet)
        {
            Lighting.AddLight(Item.Center, LightColor.R, LightColor.G, LightColor.B);
        }
    }

    public override void AddRecipes()
    {
        CreateRecipe(TorchCraftAmount)
            .AddIngredient(ItemID.Torch, TorchCraftAmount)
            .AddIngredient(MaterialItemType)
            .SortAfterFirstRecipesOf(ItemID.Torch)
            .Register();
    }
}