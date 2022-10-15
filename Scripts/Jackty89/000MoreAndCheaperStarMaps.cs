//=============================================================================
// Author: Jackty89
//=============================================================================

public class MoreAndCheaperStarMaps : cmk.NMS.Script.ModClass
{
	protected List<GcProductData>             Products;
	protected List<GcConsumableItem>          Consumables;
	protected List<GcGenericRewardTableEntry> Rewards;
	protected GcTradeData                     MapShopSettings;

	//...........................................................

	protected override void Execute()
	{
		StarMaps();
	}

	//...........................................................

	protected void StarMaps()
	{
		Products        = ExtractMbin<GcProductTable>       ("METADATA/REALITY/TABLES/NMS_REALITY_GCPRODUCTTABLE.MBIN").Table;
		Consumables     = ExtractMbin<GcConsumableItemTable>("METADATA/REALITY/TABLES/CONSUMABLEITEMTABLE.MBIN").Table;
		Rewards         = ExtractMbin<GcRewardTable>        ("METADATA/REALITY/TABLES/REWARDTABLE.MBIN").SpecialRewardTable;
		MapShopSettings = ExtractMbin<GcRealityManagerData> ("METADATA/REALITY/DEFAULTREALITY.MBIN").TradeSettings.MapShop;

		var starMapCrashedShipId       = "CHART_CRASHSHIP";
		var starMapCrashedShipRewardId = "R_CHART_CRSHIP";
		var scanEvent                  = "DISTRESS";
		var scanEventLabel             = "DISTRESS";

		var hiveMapId                  = "CHART_HIVE";
		var settleMentId               = "CHART_SETTLE";

		//Reduce cost of settlment map chart
		Products.Find(SETTLE => SETTLE.ID == settleMentId).RecipeCost = 1;

		//Make a copy of hive map to make custom made
		var crashedShipMap           = CloneMbin(Products.Find(PRODUCT => PRODUCT.ID == "CHART_HIVE"));
		crashedShipMap.ID            = starMapCrashedShipId;
		crashedShipMap.Icon.Filename = "TEXTURES/UI/FRONTEND/ICONS/U4PRODUCTS/PRODUCT.STARCHART.CRASHEDSHIP.dds";
		Products.Add(crashedShipMap);

		//Make a copy of hive map to make custom made
		var consumable      = CloneMbin(Consumables.Find(CONSUMABLE => CONSUMABLE.ID == "CHART_HIVE"));
		consumable.ID       = starMapCrashedShipId;
		consumable.RewardID = starMapCrashedShipRewardId;
		Consumables.Add(consumable);

		//Make a copy of hice map to make custom made
		var reward                   = CloneMbin(Rewards.Find(REWARD => REWARD.Id == "R_SHOW_HIVEONLY"));
		reward.List.List.RemoveRange(1, reward.List.List.Count - 1);  // only need 1 reward in clone
		var reward_item              = reward.List.List[0];
		var reward_scan              = reward_item.Reward as GcRewardScanEvent;
		reward.Id                    = starMapCrashedShipRewardId;
		reward_item.PercentageChance = 100;
		reward_item.LabelID          = scanEvent;
		reward_scan.Event            = scanEventLabel;
		Rewards.Add(reward);

		MapShopSettings.AlwaysPresentProducts.Add(starMapCrashedShipId);
		MapShopSettings.AlwaysPresentProducts.Add(hiveMapId); // this map already exists, just adding it to the shop

		int size = MapShopSettings.AlwaysPresentProducts.Count;
		MapShopSettings.MinItemsForSale = size;
		MapShopSettings.MaxItemsForSale = size;
	}
}

//=============================================================================
