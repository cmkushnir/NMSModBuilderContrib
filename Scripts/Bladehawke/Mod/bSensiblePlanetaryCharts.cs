//=============================================================================


//=============================================================================

//using BuildingClassEnum = GcBuildingClassification.BuildingClassEnum;

	//=========================================================================
	
	// Adjust startchart and random scan reward probabilities
	// i.e. what kind of location you find.
	// In particular it adds Base Sites and Portals as possible reward locations.
	// (base sites are flat round areas w/ existing base computer)
	public class bSensiblePlanetaryCharts : cmk.NMS.Script.ModClass
	{	
		protected override void Execute()
		{
			// Deprecated
			// GcBuildingDefinitionTable();
			GcRewardTable();
		}
		
		//...........................................................

		public static float DistressCrashedShip      { get; set; } = 65;
		public static float DistressCrashedShipNPC   { get; set; } = 5;
		public static float DistressCrashedFreighter { get; set; } = 25;
		public static float DistressRadioTower       { get; set; } = 5;

		public static float AncientRuin        { get; set; } = 40;
		public static float AncientPlaque      { get; set; } = 20;
		public static float AncientMonolith    { get; set; } = 20;
		public static float AncientPortal      { get; set; } = 10;
		public static float AncientObservatory { get; set; } =  5;
		public static float AncientAbandoned   { get; set; } =  5;
		

		//...........................................................

		/* Deprecated
		protected void GcBuildingDefinitionTable()
		{
			var mbin = Mbin<mbin_gc.GcBuildingDefinitionTable>(
				"METADATA/SIMULATION/ENVIRONMENT/PLANETBUILDINGTABLE.MBIN"
			);
			var data = mbin.BuildingPlacement[(int)BuildingClassEnum.Base];
			data.AddWaypoint = true;
		}
		*/
		//...........................................................

		protected void GcRewardTable()
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
			var item = MBIN.SpecialRewardTable.Find(REWARD => REWARD.Id == "R_STARCHART_B");
			RewardB(item.List.List);

			item = MBIN.SpecialRewardTable.Find(REWARD => REWARD.Id == "R_STARCHART_D");
			RewardD(item.List.List);
		}		

		//...........................................................

		protected void GcRewardTable_RandomScan( GcRewardTable MBIN )
		{	
			var item = MBIN.SpecialRewardTable.Find(REWARD => REWARD.Id == "RANDOM_SCAN_B");
			RewardB(item.List.List);

			item = MBIN.SpecialRewardTable.Find(REWARD => REWARD.Id == "RANDOM_SCAN_D");
			RewardD(item.List.List);
		}		

		//...........................................................

		// B : distress signal
		// Old: Abandoned 12%, Distress  4%, Distress with NPC 4%, Crashed Freighter  4%, Observatory 6%
		// New:                Distress 65%, Distress with NPC 10%, Crashed Freighter 25%
		protected void RewardB( List<GcRewardTableItem> LIST )
		{		
			var reward = UpdateReward(LIST, "Abandoned",         0);  // move to D
			    reward = UpdateReward(LIST, "Distress",          DistressCrashedShip);
			    reward = UpdateReward(LIST, "Distress with NPC", DistressCrashedShipNPC);
			    reward = UpdateReward(LIST, "Crashed Freighter", DistressCrashedFreighter);
			    reward = ChangeReward(LIST, "Observatory", 
			                          "RadioTower", "RADIOTOWER",
			                          DistressRadioTower);  // Make Observatories found into Towers
		}

		//...........................................................

		// D : ancient artifact site
		// Old: Plaque 12%, Monolith 12%, Ruin 12%
		// Old: Plaque 20%, Monolith 20%, Ruin 40%, Potal 10%
		// Requires Portal to be added to SCANEVENTTABLEPLANET, see Scan_Auto.
		// Added Portal to compensate for game only allowing Monoliths to find 1 Portal in System instead of per Planet.
		protected void RewardD( List<GcRewardTableItem> LIST )
		{
			var reward = UpdateReward(LIST, "Ruin",     AncientRuin);
			    reward = UpdateReward(LIST, "Plaque",   AncientPlaque);
			    reward = UpdateReward(LIST, "Monolith", AncientMonolith);
			    reward = CreateReward(LIST, AncientPortal,
					"Monolith",   "MONOLITH",
					"Portal",     "PORTAL",
					BuildingClassEnum.Portal
				);
			    reward = CreateReward(LIST, AncientObservatory,
					"Monolith",   "MONOLITH",
					"Observatory","OBSERVATORY",   
					BuildingClassEnum.Observatory
				);
			    reward = CreateReward(LIST, AncientAbandoned,
					"Monolith",    "MONOLITH",
					"Abandoned",   "ABANDONED",
					BuildingClassEnum.Portal
				);
		}

		//...........................................................

		protected GcRewardTableItem UpdateReward(
			List<GcRewardTableItem> LIST, string ID, float PERCENT
		){
			var reward  = LIST.Find(ITEM => ITEM.LabelID == ID);
			if( reward != null ) {  // no error, not all R_STARCHART_* in RANDOM_SCAN_*
				if( PERCENT < 1 ) LIST.Remove(reward);
				else reward.PercentageChance = PERCENT;
			}
			return reward;
		}	
		
		//-----------------------------------------------------------
		
		protected GcRewardTableItem ChangeReward(
			List<GcRewardTableItem> LIST, string ID1, string ID2, string NAME2, float PERCENT
		){
			var reward  = LIST.Find(ITEM => ITEM.LabelID == ID1);
			if( reward != null ) {  // no error, not all R_STARCHART_* in RANDOM_SCAN_*
				if( PERCENT < 1 ) LIST.Remove(reward);
				else {
					reward.PercentageChance = PERCENT;
					reward.LabelID = ID2;
					(reward.Reward as GcRewardScanEvent).Event = NAME2;
				}
			}
			return reward;
		}
		
		//...........................................................

		protected GcRewardTableItem CreateReward(
			List<GcRewardTableItem> LIST,
			float  PERCENT,
			string SOURCE_ID,        // "Monolith", find in LIST
			string SOURCE_NAME,      // "MONOLITH", find in GcScanEventTable
			string TARGET_ID,        // "Portal",   add  to LIST
			string TARGET_NAME,      // "PORTAL",   add  to GcScanEventTable
			BuildingClassEnum CLASS  // BuildingClassEnum.Portal
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
