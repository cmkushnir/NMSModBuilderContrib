//=============================================================================
// Adjust startchart and random scan reward probabilities
// i.e. what kind of location you find.
// In particular it adds Base Sites and Portals as possible reward locations.
// (base sites are flat round areas w/ existing base computer)
// Requires: Add_ScanEvent script for bases and portals.
//=============================================================================

public class Reward_Location : cmk.NMS.Script.ModClass
{	
	//...........................................................
	// a value of 0 will remove the reward from the chart|scan.
	// use value > 0 and < 1 if want StarCharts script to create
	// distinct chart for location, but not find using default A-D chart.
	//...........................................................

	public float SecureDepot     = 30;  //  6
	public float SecureFactory   = 20;  // 12
	public float SecureHarvester = 20;  //  6
	public float SecureBaseSite  = 20;  // N/A

	public float DistressCrashedShip      = 90;     // 4
	public float DistressCrashedShipNPC   =  0.1f;  // 4
	public float DistressCrashedFreighter = 10;     // 4

	public float InhabitedLibrary     =  5;     //  5
	public float InhabitedShelter     =  0.1f;  //  8
	public float InhabitedOutpost     =  5;     //  5
	public float InhabitedShop        = 50;     // 13
	public float InhabitedRadioTower  = 10;     //  8
	public float InhabitedObservatory = 10;     //  6 B, 8 C
	public float InhabitedAbandoned   = 10;     // 12 B

	public float AncientRuin     = 10;  // 12 - gives words or marks treaure site (excavate)
	public float AncientPlaque   = 10;  // 12
	public float AncientMonolith = 10;  // 12
	public float AncientPortal   = 60;  // N/A

	//...........................................................

	protected override void Execute()
	{
		var mbin = ExtractMbin<GcRewardTable>(
			"METADATA/REALITY/TABLES/REWARDTABLE.MBIN"
		);
		GcRewardTable_StarChart (mbin);
		GcRewardTable_RandomScan(mbin);
	}
	
	//...........................................................
	
	protected void GcRewardTable_StarChart( GcRewardTable MBIN )
	{	
		var item = MBIN.SpecialRewardTable.Find(REWARD => REWARD.Id == "R_STARCHART_A");
		RewardA(item.List.List);

		item = MBIN.SpecialRewardTable.Find(REWARD => REWARD.Id == "R_STARCHART_B");
		RewardB(item.List.List);

		item = MBIN.SpecialRewardTable.Find(REWARD => REWARD.Id == "R_STARCHART_C");
		RewardC(item.List.List);

		item = MBIN.SpecialRewardTable.Find(REWARD => REWARD.Id == "R_STARCHART_D");
		RewardD(item.List.List);
		
		// R_CHRT_SETTLE   - 100% SETTLEMENT   (planet)
		// R_CHRT_TREASURE - 100% TREASURERUIN (planet), 100% T_CHART_USED
	}		

	//...........................................................

	protected void GcRewardTable_RandomScan( GcRewardTable MBIN )
	{	
		var item = MBIN.SpecialRewardTable.Find(REWARD => REWARD.Id == "RANDOM_SCAN_A");
		RewardA(item.List.List);

		item = MBIN.SpecialRewardTable.Find(REWARD => REWARD.Id == "RANDOM_SCAN_B");
		RewardB(item.List.List);

		item = MBIN.SpecialRewardTable.Find(REWARD => REWARD.Id == "RANDOM_SCAN_C");
		RewardC(item.List.List);

		item = MBIN.SpecialRewardTable.Find(REWARD => REWARD.Id == "RANDOM_SCAN_D");
		RewardD(item.List.List);
	}		

	//...........................................................

	// A : secure site
	// Old: Depot, Factory, Harvester
	// New: Depot, Factory, Harvester, Base
	protected void RewardA( List<GcRewardTableItem> LIST )
	{
		var reward = CreateReward(LIST, SecureBaseSite,
			"Harvester", "HARVESTER",  // clone this
			"Base",      "BASE"        // to make this
		);
		reward = UpdateReward(LIST, "Depot",     SecureDepot);
		reward = UpdateReward(LIST, "Factory",   SecureFactory);
		reward = UpdateReward(LIST, "Harvester", SecureHarvester);
	}

	//...........................................................

	// B : distress signal
	// Old: Abandoned, Distress, Distress with NPC, Crashed Freighter, Observatory
	// New:            Distress, Distress with NPC, Crashed Freighter
	protected void RewardB( List<GcRewardTableItem> LIST )
	{		
		var reward = UpdateReward(LIST, "Abandoned",         0);  // move to C
		    reward = UpdateReward(LIST, "Distress",          DistressCrashedShip);
		    reward = UpdateReward(LIST, "Distress with NPC", DistressCrashedShipNPC);
		    reward = UpdateReward(LIST, "Crashed Freighter", DistressCrashedFreighter);
		    reward = UpdateReward(LIST, "Observatory",       0);  // already in C
	}

	//...........................................................

	// C : inhabited outpost
	// Old: Library, Shelter, Outpost, Shop, RadioTower, Observatory
	// New: Library, Shelter, Outpost, Shop, RadioTower, Observatory, Abandoned
	protected void RewardC( List<GcRewardTableItem> LIST )
	{
		// error in game file, LabelID == "" for LIBRARY
		var reward  = LIST.Find(ITEM => ITEM.LabelID == "");
		if( reward != null ) reward.LabelID = "Library";

		// yes, yes, not inhabited if abandoned,
		// but _was_ inhabited when chart created.
	    reward = CreateReward(LIST, InhabitedAbandoned,
			"Observatory", "OBSERVATORY",
			"Abandoned",   "ABANDONED"
		);
		
		reward = UpdateReward(LIST, "Library",     InhabitedLibrary);
		reward = UpdateReward(LIST, "Shelter",     InhabitedShelter);
		reward = UpdateReward(LIST, "Outpost",     InhabitedOutpost);
		reward = UpdateReward(LIST, "Shop",        InhabitedShop);
		reward = UpdateReward(LIST, "RadioTower",  InhabitedRadioTower);
		reward = UpdateReward(LIST, "Observatory", InhabitedObservatory);
	}

	//...........................................................

	// D : ancient artifact site
	// Old: Plaque, Monolith, Ruin
	// Old: Plaque, Monolith, Ruin, Potal
	// Requires Portal to be added to SCANEVENTTABLEPLANET, see Scan_Auto.
	// Added Portal to compensate for game only allowing Monoliths to find 1 Portal in System instead of per Planet.
	protected void RewardD( List<GcRewardTableItem> LIST )
	{
		var reward = CreateReward(LIST, AncientPortal,
			"Monolith", "MONOLITH",
			"Portal",   "PORTAL"
		);
		reward = UpdateReward(LIST, "Ruin",     AncientRuin);
		reward = UpdateReward(LIST, "Plaque",   AncientPlaque);
		reward = UpdateReward(LIST, "Monolith", AncientMonolith);
	}

	//...........................................................

	protected GcRewardTableItem UpdateReward(
		List<GcRewardTableItem> LIST, string ID, float PERCENT
	){
		var reward  = LIST.Find(ITEM => ITEM.LabelID == ID);
		if( reward != null ) {  // no error, not all R_STARCHART_* in RANDOM_SCAN_*
			     if( PERCENT <= 0 ) LIST.Remove(reward);
			else if( PERCENT <= 1 ) PERCENT = 0;
			reward.PercentageChance = PERCENT;
		}
		return reward;
	}	
	
	//...........................................................

	protected GcRewardTableItem CreateReward(
		List<GcRewardTableItem> LIST,
		float  PERCENT,
		string SOURCE_ID,    // "Monolith", find in LIST
		string SOURCE_NAME,  // "MONOLITH", find in GcScanEventTable
		string TARGET_ID,    // "Portal",   add  to LIST
		string TARGET_NAME   // BuildingClassEnum.Portal
	){
		if( PERCENT < 1 ) return null;

		var target  = LIST.Find(ITEM => ITEM.LabelID == TARGET_ID);
		if( target == null ) {
			var source = LIST.Find(ITEM => ITEM.LabelID == SOURCE_ID);
			    target = CloneMbin(source);
		
			 target.LabelID = TARGET_ID;
			(target.Reward as GcRewardScanEvent).Event = TARGET_NAME;
		
			LIST.Add(target);
		}
		
		target.PercentageChance = PERCENT;
		return target;	
	}
}

//=============================================================================
