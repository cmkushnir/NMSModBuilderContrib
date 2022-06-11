//=============================================================================
// Alter lists of items in various stores.
//=============================================================================

public class Inventory_Trade : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcRealityManagerData>(
			"METADATA/REALITY/DEFAULTREALITY.MBIN"
		);
		var trade = mbin.TradeSettings;		
		#if false
		// just add some useful items to space station atm
		GcTradeDataMinor(trade.SpaceStation);
		#else
		var products = ExtractMbin<GcProductTable>(
			"METADATA/REALITY/TABLES/NMS_REALITY_GCPRODUCTTABLE.MBIN"
		);
		var substances = ExtractMbin<GcSubstanceTable>(
			"METADATA/REALITY/TABLES/NMS_REALITY_GCSUBSTANCETABLE.MBIN"
		);
		#if true
		// or, add everything as optional to various vendors
		GcTradeDataAllOptional(trade.SpaceStation, products, substances);
		GcTradeDataAllOptional(trade.Shop,         products, substances);
		GcTradeDataAllOptional(trade.Ship,         products, substances);
		GcTradeDataAllOptional(trade.ExpShip,      products, substances);
		GcTradeDataAllOptional(trade.TraShip,      products, substances);
		GcTradeDataAllOptional(trade.WarShip,      products, substances);
		GcTradeDataAllOptional(trade.LoneExp,      products, substances);
		GcTradeDataAllOptional(trade.LoneTra,      products, substances);
		GcTradeDataAllOptional(trade.LoneWar,      products, substances);
		GcTradeDataAllOptional(trade.IllegalProds, products, substances);
		GcTradeDataAllOptional(trade.Scrap,        products, substances);
		#else
		// or, just add all s-class upgrades to space station always present list
		GcTradeDataSAlwaysPresent(trade.SpaceStation, products);
		#endif
		#endif
	}

	//...........................................................
	
	protected void GcTradeDataMinor( GcTradeData DATA )
	{
		DATA.MinItemsForSale = 20;  // 15
		DATA.MaxItemsForSale = 30;  // 18
		
		// Default SpaceStation never sells:
		// - CREATURE1 (Mordite)
		// - GAS1 (Sulphurine), GAS2 (Radon), GAS3 (Nitrogen)
		// - LAUNCHSUB (Di-hydrogen), LAUNCHSUB2 (Deuterium)
		// - LAVA1 (Basalt)
		// - WATERPLANT (Cyto-Phosphate), PLANT_* (growable + kelp sac)
		// - FUEL1 (Carbon), FUEL2 (Condensed Carbon)
		// - SAND1 (Silicate Powder)
		// - RED2, EX_RED, GREEN2, EX_GREEN, BLUE2, EX_BLUE
		
		var list = DATA.AlwaysPresentSubstances;
		list.Remove("YELLOW1");       // no such substance
		list.AddUnique("LAUNCHSUB");  // Di-hydrogen
		
		list = DATA.OptionalSubstances;
		list.AddUnique("GAS1");   // Sulphurine
		list.AddUnique("GAS2");   // Radon
		list.AddUnique("GAS3");   // Nitrogen
		list.AddUnique("LAVA1");  // Basalt
		list.AddUnique("FUEL1");  // Carbon
		list.AddUnique("FUEL2");  // Condensed Carbon
		list.AddUnique("SAND1");  // Silicate Powder
	}
	
	//...........................................................

	protected void GcTradeDataAllOptional( GcTradeData DATA, GcProductTable PRODUCTS, GcSubstanceTable SUBSTANCES )
	{
		DATA.MinItemsForSale += 4;
		DATA.MaxItemsForSale += 4;
		DATA.MinAmountOfProductAvailable  [(int)WealthClassEnum.Poor] *= 2;
		DATA.MaxAmountOfProductAvailable  [(int)WealthClassEnum.Poor] *= 2;
		DATA.MinAmountOfSubstanceAvailable[(int)WealthClassEnum.Poor] *= 2;
		DATA.MaxAmountOfSubstanceAvailable[(int)WealthClassEnum.Poor] *= 2;
		
		foreach( var product in PRODUCTS.Table ) {
			switch( product.Type.ProductCategory ) {
				case ProductCategoryEnum.Curiosity:
					if( product.Name.Value.StartsWith("GEODE_") ) break;
					continue;
				case ProductCategoryEnum.Component: break;
				case ProductCategoryEnum.Consumable:
					if( product.Name.Value.StartsWith("SPEC_") ) continue;
					break;
				case ProductCategoryEnum.Tradeable: break;
				default: continue;
			}
			DATA.OptionalProducts.AddUnique(product.Id);
		}
		// add specific items from skipped categories
		DATA.OptionalProducts.AddUnique("ARTIFACT_KEY");    // Ancient Key
		DATA.OptionalProducts.AddUnique("BP_SALVAGE");      // Salvaged Data
		DATA.OptionalProducts.AddUnique("CATAPROD3");       // Destablised Sodium
		DATA.OptionalProducts.AddUnique("CAVEPROD3");       // TetraCobalt
		DATA.OptionalProducts.AddUnique("FIENDCORE");       // Larval Core
		DATA.OptionalProducts.AddUnique("FREI_INV_TOKEN");  // Cargo Bulkhead
		DATA.OptionalProducts.AddUnique("FRIG_TOKEN");      // Salvaged Frigate Module
		DATA.OptionalProducts.AddUnique("FUELPROD3");       // Carbon Crystal
		DATA.OptionalProducts.AddUnique("HEXCORE");         // Hex Core
		DATA.OptionalProducts.AddUnique("LANDPROD3");       // Rare Metal Element
		DATA.OptionalProducts.AddUnique("NAV_DATA");        // Navigation Data
		DATA.OptionalProducts.AddUnique("NAV_DATA_DROP");   // Drop Pod Coordinate Data
		DATA.OptionalProducts.AddUnique("OXYPROD3");        // Superoxide Crystal
		DATA.OptionalProducts.AddUnique("SHIP_INV_TOKEN");  // Storage Augmentation
		DATA.OptionalProducts.AddUnique("STORM_CRYSTAL");   // Storm Crystal
		DATA.OptionalProducts.AddUnique("SUIT_INV_TOKEN");  // Exosuit Expansion Unit
		DATA.OptionalProducts.AddUnique("TRIDENT_KEY");     // Trident Key
		DATA.OptionalProducts.AddUnique("WATERPROD3");      // Chloride Lattice
		DATA.OptionalProducts.AddUnique("WEAP_INV_TOKEN");  // Multi-Tool Expansion Slot
		DATA.OptionalProducts.AddUnique("WORMCORE");        // Fleshy Rope
		DATA.OptionalProducts.AddUnique("WORMPROD");        // Vile Spawn

		foreach( var substance in SUBSTANCES.Table ) {
			if( substance.Name.Value.Contains("REWARD") ||
				substance.Description == "UI_REWARDPOP_DESC"
			)	continue;
			DATA.OptionalSubstances.AddUnique(substance.ID);
		}
	}

	//...........................................................

	protected void GcTradeDataSAlwaysPresent( GcTradeData DATA, GcProductTable PRODUCTS )
	{
		var proc_mbin = ExtractMbin<GcProceduralTechnologyTable>(
			"METADATA/REALITY/TABLES/NMS_REALITY_GCPROCEDURALTECHNOLOGYTABLE.MBIN"
		);			
		foreach( var product in PRODUCTS.Table ) {
			if( product.DeploysInto.Value.IsNullOrEmpty() ) continue;  // must be proc gen
			
			var proc  = proc_mbin.Table.Find(PROC => PROC.ID == product.DeploysInto.Value);
			if( proc == null ) {
				Log.AddWarning($"{product.DeploysInto} not found in NMS_REALITY_GCPROCEDURALTECHNOLOGYTABLE");
			}
			if( proc.Quality != QualityEnum.Legendary ) continue;  // must be s-class
			
			DATA.AlwaysPresentProducts.AddUnique(product.Id);
		}
	}
}

//=============================================================================
