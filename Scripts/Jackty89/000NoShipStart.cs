//=============================================================================
// Author: Jackty89
//=============================================================================

public class NoShipStart: cmk.NMS.Script.ModClass
{
	public int GarageChoice             = 4;     //PickUpListIds => 4 == GARAGE_MECH

	private List <string> PickUpListIds = new List<string>()
	{
		"GARAGE_B",
		"GARAGE_S",
		"GARAGE_M",
		"GARAGE_L",
		"GARAGE_MECH",
		"GARAGE_SUB"
	};
	
	protected override void Execute()
	{
		GcRealityManagerData();
		EditStarterShipSlotsAndInventoryItems();
		EditStartShipStarterLocation();
		PickUGeoBays();
		BasePartsDelux();
	}


	//WORKS
	//force ships minimal slots not sureif this affect other ships
	protected void GcRealityManagerData()
	{
		var mbin = ExtractMbin<GcRealityManagerData>("METADATA/REALITY/DEFAULTREALITY.MBIN");
		mbin.ShipStartingLayout.Slots = 0;
		mbin.ShipTechOnlyStartingLayout.Slots = 0;
		mbin.ShipCargoOnlyStartingLayout.Slots = 0;
	}
	
	protected void EditStarterShipSlotsAndInventoryItems()
	{
		
		var defaultSaveData = ExtractMbin<GcDefaultSaveData>("METADATA/GAMESTATE/DEFAULTSAVEDATA.MBIN");
		defaultSaveData.State.ShipInventory.Slots.Clear();
		
		defaultSaveData.State.ShipLayout.Slots = 0;
		for (int i = 0; i <= defaultSaveData.State.ShipLayout.Slots; i++)		
			defaultSaveData.State.ShipInventory.Slots.Add(Inventory.Technology("SHIPSLOT_DMG12", 100, 100));

		defaultSaveData.State.KnownProducts.AddUnique("CARBON_SEAL");// quest related
		defaultSaveData.State.KnownProducts.AddUnique("BUILDBEACON");// quest related
		defaultSaveData.State.KnownProducts.AddUnique("BASE_FLAG");  // quest related
		defaultSaveData.State.KnownProducts.AddUnique("BP_ANALYSER");// quest related
		defaultSaveData.State.KnownProducts.AddUnique("ANTIMATTER"); // quest related
		defaultSaveData.State.KnownProducts.AddUnique("AM_HOUSING"); // quest related
		
		defaultSaveData.State.KnownTech.AddUnique("TERRAINEDITOR");  // quest related
		defaultSaveData.State.KnownTech.AddUnique("HYPERDRIVE");     // quest related
		defaultSaveData.State.KnownTech.AddUnique("STRONGLASER");    // quest related
		
		// might be Inventory_Cargo
		defaultSaveData.State.Inventory.Add(Inventory.Product(PickUpListIds[GarageChoice],1,1));
		defaultSaveData.State.Inventory.Add(Inventory.Product("BUILDBEACON",1,1));
		
		var gameplayGlobals = ExtractMbin<GcGameplayGlobals>("GCGAMEPLAYGLOBALS.GLOBAL.MBIN");
		
		// has been moved to gameplayglobals
		gameplayGlobals.DifficultyConfig.StartWithAllItemsKnownDisabledData.InitialWeaponInventory.Slots.Find(Element => Element.Id =="SCAN1").DamageFactor = 0;
		gameplayGlobals.DifficultyConfig.StartWithAllItemsKnownDisabledData.InitialShipInventory.Slots.Clear();
		
		gameplayGlobals.DifficultyConfig.StartWithAllItemsKnownEnabledData.InitialWeaponInventory.Slots.Find(Element => Element.Id =="SCAN1").DamageFactor = 0;
		gameplayGlobals.DifficultyConfig.StartWithAllItemsKnownEnabledData.InitialShipInventory.Slots.Clear();
	}
	protected void EditStartShipStarterLocation()
	{
		var mbin = ExtractMbin<GcBuildingGlobals>("GCBUILDINGGLOBALS.GLOBAL.MBIN");

		mbin.StartCrashSiteMinDistance = 500000;
		mbin.StartCrashSiteMaxDistance = 1000000;
		mbin.StartShelterMinDistance   = 2500;
		mbin.StartShelterMaxDistance   = 5000;
	}
	
	protected void PickUGeoBays()
	{
		var mbin = ExtractMbin<GcBaseBuildingTable>("METADATA/REALITY/TABLES/BASEBUILDINGOBJECTSTABLE.MBIN");
		foreach( string pickupId in PickUpListIds)
		{
			mbin.Objects.Find(ID => ID.ID == pickupId).CanPickUp = true; 
		}
	}
	
	protected void BasePartsDelux()
	{
		var mbin            = ExtractMbin<GcUnlockableTrees>("METADATA/REALITY/TABLES/UNLOCKABLEITEMTREES.MBIN");
		var copyTree        = CloneMbin(mbin.Trees[(int)UnlockableItemTreeEnum.BaseParts]);
		var copTreeTrees    = copyTree.Trees;
		var basiceBaseParts = mbin.Trees[(int)UnlockableItemTreeEnum.BasicBaseParts];
		basiceBaseParts.Trees.Clear();

		foreach( var tree in copTreeTrees ) {
			basiceBaseParts.Trees.Add(tree);
		}
	}
}