using Terraria;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace MLib.Common.Items;
/// <summary>
/// Requires for you to add a custom fishing condition in ModPlayer to be catchable.
/// </summary>
public abstract class ModdedBiomeFishingCrate : ModItem
{
	// Basic code for a fishing crate
	// The catch code is in a separate ModPlayer class (ExampleFishingPlayer)
	// The placed tile is in a separate ModTile class
	public abstract bool IsHardmodeCrate { get; }
	public abstract int CrateTileType { get; }
	public abstract int Rarity { get; }
	public abstract int Value { get; }
		public override void SetStaticDefaults() 
		{
			// Disclaimer for both of these sets (as per their docs): They are only checked for vanilla item IDs, but for cross-mod purposes it would be helpful to set them for modded crates too
			ItemID.Sets.IsFishingCrate[Type] = true;
			ItemID.Sets.IsFishingCrateHardmode[Type] = IsHardmodeCrate; // This is a crate that mimics a pre-hardmode biome crate, so this is commented out

			Item.ResearchUnlockCount = 10;
		}

		public override void SetDefaults() 
		{
			Item.DefaultToPlaceableTile(CrateTileType);
			Item.width = 12; // The hitbox dimensions are intentionally smaller so that it looks nicer when fished up on a bobber
			Item.height = 12;
			Item.rare = Rarity;
			Item.value = Value;
		}

		public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup) 
		{
			itemGroup = ContentSamples.CreativeHelper.ItemGroup.Crates;
		}

		public override bool CanRightClick() 
		{
			return true;
		}

		public override void ModifyItemLoot(ItemLoot itemLoot) 
		{
			CustomItemDrops(itemLoot);
			// Drop coins
			itemLoot.Add(ItemDropRule.Common(ItemID.GoldCoin, 4, 5, 13));

			// Drop pre-hm ores
			int minOreAmount = 20;
			int maxOreAmount = 35;
			IItemDropRule[] oreTypes = 
			[
				ItemDropRule.Common(ItemID.CopperOre, 1, minOreAmount, maxOreAmount),
				ItemDropRule.Common(ItemID.TinOre, 1, minOreAmount, maxOreAmount),
				ItemDropRule.Common(ItemID.IronOre, 1, minOreAmount, maxOreAmount),
				ItemDropRule.Common(ItemID.LeadOre, 1, minOreAmount, maxOreAmount),
				ItemDropRule.Common(ItemID.SilverOre, 1, minOreAmount, maxOreAmount),
				ItemDropRule.Common(ItemID.TungstenOre, 1, minOreAmount, maxOreAmount),
				ItemDropRule.Common(ItemID.GoldOre, 1, minOreAmount, maxOreAmount),
				ItemDropRule.Common(ItemID.PlatinumOre, 1, minOreAmount, maxOreAmount),
			];
			itemLoot.Add(new OneFromRulesRule(7, oreTypes));

			// Drop pre-hm bars (except copper/tin)
			int minBarAmount = 6;
			int maxBarAmount = 16;
			IItemDropRule[] oreBars = [
				ItemDropRule.Common(ItemID.IronBar, 1, minBarAmount, maxBarAmount),
				ItemDropRule.Common(ItemID.LeadBar, 1, minBarAmount, maxBarAmount),
				ItemDropRule.Common(ItemID.SilverBar, 1, minBarAmount, maxBarAmount),
				ItemDropRule.Common(ItemID.TungstenBar, 1, minBarAmount, maxBarAmount),
				ItemDropRule.Common(ItemID.GoldBar, 1, minBarAmount, maxBarAmount),
				ItemDropRule.Common(ItemID.PlatinumBar, 1, minBarAmount, maxBarAmount),
			];
			itemLoot.Add(new OneFromRulesRule(4, oreBars));

			// Drop an "exploration utility" potion
			int minPotAmount = 2;
			int maxPotAmount = 4;
			IItemDropRule[] explorationPotions = [
				ItemDropRule.Common(ItemID.ObsidianSkinPotion, 1, minPotAmount, maxPotAmount),
				ItemDropRule.Common(ItemID.SpelunkerPotion, 1, minPotAmount, maxPotAmount),
				ItemDropRule.Common(ItemID.HunterPotion, 1, minPotAmount, maxPotAmount),
				ItemDropRule.Common(ItemID.GravitationPotion, 1, minPotAmount, maxPotAmount),
				ItemDropRule.Common(ItemID.MiningPotion, 1, minPotAmount, maxPotAmount),
				ItemDropRule.Common(ItemID.HeartreachPotion, 1, minPotAmount, maxPotAmount),
			];
			itemLoot.Add(new OneFromRulesRule(4, explorationPotions));

			// Drop (pre-hm) resource potion
			int minHealPotAmount = 5;
			int maxHealPotAmount = 17;
			IItemDropRule[] resourcePotions = [
				ItemDropRule.Common(ItemID.HealingPotion, 1, minHealPotAmount, maxHealPotAmount),
				ItemDropRule.Common(ItemID.ManaPotion, 1, minHealPotAmount, maxHealPotAmount),
			];
			itemLoot.Add(new OneFromRulesRule(2, resourcePotions));

			// Drop (high-end) bait
			int minBaitAmount = 2;
			int maxBaitAmount = 6;
			IItemDropRule[] highendBait = [
				ItemDropRule.Common(ItemID.JourneymanBait, 1, minBaitAmount, maxBaitAmount),
				ItemDropRule.Common(ItemID.MasterBait, 1, minBaitAmount, maxBaitAmount),
			];
			itemLoot.Add(new OneFromRulesRule(2, highendBait));
		}

		public virtual void CustomItemDrops(ItemLoot itemLoot)
		{
			// Drop a special weapon/accessory etc. specific to this crate's theme (i.e. Sky Crate dropping Fledgling Wings or Starfury)
			// this is just an example of how to do it correctly, replace the themedDrops with any number of custom items
			int[] themedDrops = 
			[
				ItemID.Flairon,
				ItemID.Tsunami,
				ItemID.BubbleGun,
				ItemID.RazorbladeTyphoon,
				ItemID.TempestStaff
			];
			itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, themedDrops));
		}
}