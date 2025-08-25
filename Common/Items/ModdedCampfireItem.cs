using Terraria.ID;
using Terraria.ModLoader;

namespace MLib.Common.Items;

public abstract class ModdedCampfireItem : ModdedBlockItem
{
    public abstract int TorchItemType { get; }
    public override string LocalizationCategory => "Items.Tiles.Furniture";
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
        CustomSetStaticDefaults();
    }

    public override void AddRecipes() 
    {
        CreateRecipe()
            .AddRecipeGroup(RecipeGroupID.Wood, 10)
            .AddIngredient(TorchItemType,5)
            .Register();
    }
}