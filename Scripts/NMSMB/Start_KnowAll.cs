//=============================================================================
// Set all unlockable items as known when starting a new game.
// Works w/ Reward_Specials, which adds twitch rewards to PURCHASEABLESPECIALS.MBIN.
//=============================================================================

public class Start_KnowAll : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcDefaultSaveData>(
			"METADATA/GAMESTATE/DEFAULTSAVEDATA.MBIN"
		);
		Add_AllProducts    (mbin.State);
		Add_AllTechnologies(mbin.State);
		Add_Known_Recipes  (mbin.State);
	}

	//...........................................................

	protected void Add_AllProducts( GcPlayerStateData STATE )
	{
		var products = ExtractMbin<GcProductTable>(
			"METADATA/REALITY/TABLES/NMS_REALITY_GCPRODUCTTABLE.MBIN"
		);
		var unlockables = ExtractMbin<GcUnlockableSeasonRewards>(
			"METADATA/REALITY/TABLES/UNLOCKABLESEASONREWARDS.MBIN"
		);	
		foreach( var product in products.Table ) {
			if( product.TradeCategory.TradeCategory != TradeCategoryEnum.SpecialShop ) {
				STATE.KnownProducts.AddUnique(product.ID);
			}
			// don't add things like reward ships or multitools
			else if( product.GiveRewardOnSpecialPurchase.Value.IsNullOrEmpty() ) {
				STATE.KnownSpecials.AddUnique(product.ID);
				// unlockables also need to be in KnownProducts in order to use
				var id = product.ID.Value;
				var unlockable  = unlockables.Table.Find(UNLOCKABLE => UNLOCKABLE.ID == id);		
				if( unlockable != null ) STATE.KnownProducts.AddUnique(product.ID);
			}
		}
	}
	
	//...........................................................

	protected void Add_AllTechnologies( GcPlayerStateData STATE )
	{
		var mbin = ExtractMbin<GcTechnologyTable>(
			"METADATA/REALITY/TABLES/NMS_REALITY_GCTECHNOLOGYTABLE.MBIN"
		);
		foreach( var technology in mbin.Table ) {
			if( technology.Description.Value != "TEMPLATE_DESC" &&
			   !technology.ID.Value.Contains("_DMG")
			)	STATE.KnownTech.AddUnique(technology.ID);
		}
	}

	//...........................................................

	protected void Add_Known_Recipes( GcPlayerStateData STATE )
	{
		var mbin = Game.PCBANKS.ExtractMbin<GcRecipeTable>(
			"METADATA/REALITY/TABLES/NMS_REALITY_GCRECIPETABLE.MBIN"
		);
		foreach( var recipe in mbin.Table ) {
			STATE.KnownRefinerRecipes.AddUnique(recipe.Id);
		}
	}
}

//=============================================================================
