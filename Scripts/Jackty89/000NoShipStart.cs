//=============================================================================
// Author: Jackty89
//=============================================================================

public class NoShipStart: cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{

		GcRealityManagerData();
		GcDefaultSaveData();
		GcBuildingGlobals();
		PickUGeoBays();
		ItemTrees();
		GcDebugOptions();
		CoreMissionEdits();

	}

	//...........................................................
	
	protected void CoreMissionEdits()
	{
		var mbin = ExtractMbin<GcMissionTable>(
			"METADATA/SIMULATION/MISSIONS/COREMISSIONTABLE.MBIN"
		);
		
		List <string> skipMissionIds = new List<string> () {"ACT1_STEP1", "ACT1_STEP3"};
		foreach(string missionId in skipMissionIds)
		{
			var mission = mbin.Missions.Find(ID => ID.MissionID == missionId);
			mission.AutoStart = AutoStartEnum.None;
		}		
	}
	
	protected void GcDebugOptions()
	{
		var mbin = ExtractMbin<GcDebugOptions>(
			"GCDEBUGOPTIONS.GLOBAL.MBIN"
		);
		mbin.SkipTutorial = true;
		mbin.SkipIntro 	  = true;
	}

	protected void GcRealityManagerData()
	{
		var mbin = ExtractMbin<GcRealityManagerData>(
			"METADATA/REALITY/DEFAULTREALITY.MBIN"
		);
		mbin.ShipStartingLayout.Slots = 10;
		mbin.ShipTechOnlyStartingLayout.Slots = 3;
		mbin.ShipCargoOnlyStartingLayout.Slots = 1;
	}

	//...........................................................

	protected void GcDefaultSaveData()
	{
		var mbin = ExtractMbin<GcDefaultSaveData>(
			"METADATA/GAMESTATE/DEFAULTSAVEDATA.MBIN"
		);
		mbin.State.ShipInventory.Slots.Clear();
		mbin.State.ShipLayout.Slots = 10;	
		for (int i = 0; i <= mbin.State.ShipLayout.Slots; i++)		
			mbin.State.ShipInventory.Slots.Add(Inventory.Technology("SHIPSLOT_DMG12", 100, 100));
		
		mbin.State.Inventory.Add(Inventory.Product("BUILDBEACON",1,1));
		mbin.State.Inventory.Add(Inventory.Product("GARAGE_B",1,1));
		
		mbin.State.PreviousMissionID = "TUT_FIRST_WALK";
		mbin.State.CurrentMissionID  = "NAVDATA_RECOVER";
	}

	//WORKS
	protected void GcBuildingGlobals()
	{
		var mbin = ExtractMbin<GcBuildingGlobals>(
			"GCBUILDINGGLOBALS.GLOBAL.MBIN"
		);
		// don't seem to work anymore
		mbin.StartCrashSiteMinDistance = 250000;
		mbin.StartCrashSiteMaxDistance = 1000000;
		mbin.StartShelterMinDistance   = 2500;
		mbin.StartShelterMaxDistance   = 5000;
	}
	
	protected void PickUGeoBays()
	{
		List <string> pickUpListIds = new List<string> () 
		{
			"GARAGE_B",
			"GARAGE_S",
			"GARAGE_M",
			"GARAGE_L",
			"GARAGE_MECH",
			"GARAGE_SUB"
		};
		var mbin = ExtractMbin<GcBaseBuildingTable>("METADATA/REALITY/TABLES/BASEBUILDINGOBJECTSTABLE.MBIN");
		
		
		foreach( string pickupId in pickUpListIds)
		{
			mbin.Objects.Find(ID => ID.ID == pickupId).CanPickUp = true; 
		}
	}
	
	protected void ItemTrees()
	{
		var mbin            = ExtractMbin<GcUnlockableTrees>("METADATA/REALITY/TABLES/UNLOCKABLEITEMTREES.MBIN");
		var copyTree        = CloneMbin(mbin.Trees[(int)UnlockableItemTreeEnum.BaseParts]);
		var copTreeTrees    = copyTree.Trees;
		var basiceBaseParts = mbin.Trees[(int)UnlockableItemTreeEnum.BasicBaseParts];
		//basiceBaseParts.Trees.Clear();

		foreach( var tree in copTreeTrees ) {
			basiceBaseParts.Trees.Add(tree);
		}
	}

}

//=============================================================================
