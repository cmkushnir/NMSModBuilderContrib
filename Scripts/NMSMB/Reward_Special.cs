//=============================================================================
// - Make sure all character customization options are visible.
// - Remove season|stage requirements from expedition rewards.
// - Remove mission tier requirements from all purchaseable specials.
// - Add all twitch rewards to purchasable specials list.
// note:
// - items should be unlocked in Binaries/SETTINGS/GCUSERSETTINGSDATA.MXML first
// - seasonal ships allow claim and exchange.
// - twitch ships and guns only allow exchange, not claim.
//=============================================================================

public class Rewards_Special : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		Try(() => GcCustomisationDescriptorGroups());
		Try(() => GcUnlockableSeasonRewards());
		Try(() => GcPurchaseableSpecials());
		Try(() => GcUnlockableTwitchRewards());
	}

	//...........................................................

	// make sure no character customization options are hidden
	protected void GcCustomisationDescriptorGroups()
	{
		var mbin = ExtractMbin<GcCustomisationDescriptorGroups>(
			"METADATA/GAMESTATE/PLAYERDATA/CHARACTERCUSTOMISATIONDESCRIPTORGROUPSDATA.MBIN"
		);
		foreach( var group in mbin.DescriptorGroups ) {
			group.HiddenInCustomiser = false;
		}
	}
	
	//...........................................................

	// remove season|stage requirements from expedition rewards
	protected void GcUnlockableSeasonRewards()
	{
		var mbin = ExtractMbin<GcUnlockableSeasonRewards>(
			"METADATA/REALITY/TABLES/UNLOCKABLESEASONREWARDS.MBIN"
		);
		foreach( var reward in mbin.Table ) {			
			reward.MustBeUnlocked = false;
			for( var i = 0; i < reward.SeasonIds.Count; ++i ) reward.SeasonIds[i] =  0;
			for( var i = 0; i < reward.StageIds .Count; ++i ) reward.StageIds[i]  = -1;
		}
	}

	//...........................................................

	// remove mission tier requirements from all purchaseable specials
	protected void GcPurchaseableSpecials()
	{
		var mbin = ExtractMbin<GcPurchaseableSpecials>(
			"METADATA/REALITY/TABLES/PURCHASEABLESPECIALS.MBIN"
		);
		mbin.Table.ForEach(PURCHASEABLE => {
			PURCHASEABLE.MissionTier = -1;
		});
	}
	
	//...........................................................

	// add all twitch rewards to purchasable specials list
	protected void GcUnlockableTwitchRewards()
	{
		// pull ProductId from twitch rewards
		var twitches = ExtractMbin<GcUnlockableTwitchRewards>(
			"METADATA/REALITY/TABLES/UNLOCKABLETWITCHREWARDS.MBIN"
		);		

		// add twitch to purchaseable
		// adding to seasonal doesn't work, says you have to complete mission #, regardless of #
		var purchaseables = ExtractMbin<GcPurchaseableSpecials>(
			"METADATA/REALITY/TABLES/PURCHASEABLESPECIALS.MBIN"
		);
		
		// some twitch rewards may also be in unlockable season rewards e.g. EXPD_HELMET01
		// don't add those twitch rewards to purchaseable specials,
		// the specials vendor has two lists to select from: purchasables and unlockable season,
		// these will already be in the unlockable season list.
		var unlockables = ExtractMbin<GcUnlockableSeasonRewards>(
			"METADATA/REALITY/TABLES/UNLOCKABLESEASONREWARDS.MBIN"
		);
		
		// product lookup table, mainly to set purchaseable.IsConsumable
		var products = ExtractMbin<GcProductTable>(
			"METADATA/REALITY/TABLES/NMS_REALITY_GCPRODUCTTABLE.MBIN"
		);
		
		foreach( var twitch in twitches.Table ) {
			var id = twitch.ProductId.Value;
			
			var purchaseable  = purchaseables.Table.Find(PURCHASEABLE => PURCHASEABLE.ID == id);
			if( purchaseable != null ) continue;  // already purchaseable special (list 1)

			var unlocklable  = unlockables.Table.Find(UNLOCKABLE => UNLOCKABLE.ID == id);
			if( unlocklable != null ) continue;  // already purchaseable special (list 2)
			
			var product  = products.Table.Find(PRODUCT => PRODUCT.ID == id);
			if( product == null ) continue;  // no product, should never happen ?

			var consume = product.Consumable;
			if( id.Contains("_FIREW")  // fireworks
			)	consume = true;        // allows specifying # to buy
					
			purchaseables.Table.Add(new(){
				ID           = id,
				ShopNumber   =  1,
				MissionTier  = -1,
				IsConsumable = consume
			});
		}
	}
}

//=============================================================================
