//=============================================================================

public class bSignalBooster : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		RewardTable();
		ScanTable();
		AlienPuzzle();
	}
	
	protected void RewardTable()
	{
		var mbin = ExtractMbin<GcRewardTable>(
			"METADATA/REALITY/TABLES/REWARDTABLE.MBIN"
		);
		
		var reward = CreateReward(mbin, 100, "CRASHED_SHIP", "DISTRESS",
		                         GcRewardScanEvent.ScanEventTableEnum.Planet);
		reward = CreateReward(mbin, 100, "TOOL_LOCATION", "SHOP",
		                      GcRewardScanEvent.ScanEventTableEnum.Planet);
		reward = CreateReward(mbin, 100, "PLANET_ARCHIVES", "LIBRARY",
		                      GcRewardScanEvent.ScanEventTableEnum.Planet);
		reward = CreateReward(mbin, 100, "R_GRAVE", "SE_GRAVE",
		                      GcRewardScanEvent.ScanEventTableEnum.Planet);
		reward = CreateReward(mbin, 100, "R_BASE", "SE_BASE",
		                      GcRewardScanEvent.ScanEventTableEnum.Planet);
	}
	
	protected void ScanTable()
	{
		var mbin = ExtractMbin<GcScanEventTable>(
			"METADATA/SIMULATION/SCANNING/SCANEVENTTABLEPLANET.MBIN"
		);
		
		var scanEvents = new Tuple<string, BuildingClassEnum, string, string>[] { 
			new("SE_GRAVE", BuildingClassEnum.GraveInCave, "UI_MP_PLANTKILL_GRAVE_OSD", "SCAN_GRAVE"),
			new("SE_BASE", BuildingClassEnum.Base, "UI_RECOVER_BASE_OSD", "UI_RECOVER_BASE_MARKER")
		};
		
		foreach(var scan in scanEvents) {
			var target = mbin.Events.Find(EVENT => EVENT.Name == scan.Item1);
    		if (target != null) continue;  // no error, already exists
    		
    		var source = mbin.Events.Find(EVENT => EVENT.Name == "HARVESTER");
    		target = CloneMbin(source);
    		
    		target.Name = scan.Item1;
    		target.OSDMessage = scan.Item3;
    		target.BuildingClass.BuildingClass = scan.Item2;
    		target.MarkerLabel = scan.Item4;
    		
    		mbin.Events.Add(target);
    	};
		
		var hive = mbin.Events.Find(EVENT => EVENT.Name == "DRONE_HIVE");
		hive.ReplaceEventIfAlreadyActive = true;
		hive.InterstellarOSDMessage = "SCANEVENT_ANOTHJER_SYSTEM";
	}
    		
	protected GcGenericRewardTableEntry CreateReward(
		GcRewardTable MBIN,
		float PERCENT,
		string TARGET_ID,        // "Portal",   add  to LIST
		string TARGET_EVENT,      // "PORTAL",   add  to GcScanEventTable
		GcRewardScanEvent.ScanEventTableEnum SCANTYPE  
    ){
		var clone = CloneMbin(MBIN.InteractionTable.Find(NAME => NAME.Id == "R_DIG_CLUE"));
		
		clone.Id = TARGET_ID;

		var LIST = clone.List.List;
        var reward = LIST.FindFirst<GcRewardTableItem>();
        (reward.Reward as GcRewardScanEvent).Event= TARGET_EVENT;
		
        MBIN.InteractionTable.Add(clone);
  		
        clone.List.RewardChoice = RewardChoiceEnum.SelectAlways;
        reward.PercentageChance = PERCENT;
        (reward.Reward as GcRewardScanEvent).ScanEventTable = SCANTYPE;
       	return clone;
  }
  
	protected void AlienPuzzle()
	{		
		var mbin = ExtractMbin<GcAlienPuzzleTable>(
			"METADATA/REALITY/TABLES/NMS_DIALOG_GCALIENPUZZLETABLE.MBIN"
		);
		
		var scanner = mbin.Table.Find(ENTRY => ENTRY.Id == "SIGNALSCANNER");
		var option = scanner.Options.FindFirst<GcAlienPuzzleOption>();
		while (option != null) {
			scanner.Options.Remove(option);
			option = scanner.Options.FindFirst<GcAlienPuzzleOption>();
		}
		
		AddScannerOption( scanner, "UI_PORTAL_OPT", "REVEAL_PORTAL"); // Portal
		AddScannerOption( scanner, "BUILDING_DISTRESSSIGNAL_L", "CRASHED_SHIP"); // Crashed Starship
		AddScannerOption( scanner, "NPC_TECHSHOP_CATEGORY_WEAP", "TOOL_LOCATION"); // Multi-tool Location
		AddScannerNext(mbin.Table, scanner, "?POWERSCANNER"); // Select Next Scanner Options
		
		scanner = mbin.Table.Find(ENTRY => ENTRY.Id == "?POWERSCANNER");
		
		AddScannerOption( scanner, "BUILDING_FACTORY_L", "SEC_SCN_FACT"); // Manufacturing Facility
		AddScannerOption( scanner, "UI_NAV_DROPPOD_NAME_L", "SCAN_1"); // ExoSuit DropPod
		AddScannerOption( scanner, "UI_LIBRARY_ENTRANCE_DESC", "PLANET_ARCHIVES"); // Planetary Archives
		AddScannerNext(mbin.Table, scanner, "?ATOMICSCANNER"); // Select Next Scanner Options 

		scanner = mbin.Table.Find(ENTRY => ENTRY.Id == "?ATOMICSCANNER");
		
		AddScannerOption( scanner, "UI_CORE_ACT2_STEP8_MARKER", "SHOW_CRASH_SITE"); // Crashed Freighter
		AddScannerOption( scanner, "UI_ABAND_EVENT_AREA20", "SEC_SCN_OBS"); // Observatory
		AddScannerOption( scanner, "NAV_DATA_OPTC", "RANDOM_SCAN_C"); // Scan Habitable Outposts
		AddScannerNext(mbin.Table, scanner, "?NUCLEARSCANNER"); // Select Next Scanner Options
		
		scanner = mbin.Table.Find(ENTRY => ENTRY.Id == "?NUCLEARSCANNER");

		AddScannerOption( scanner, "UI_SENTINEL_HIVE_NAME", "R_SHOW_HIVE_ONLY"); // Sentinel Pillar
		AddScannerOption( scanner, "SCAN_GRAVE", "R_GRAVE"); // Traveller Grave
		AddScannerOption( scanner, "UI_RECOVER_BASE_SUB", "R_BASE"); // "Wild" Base Computer
		AddScannerLeave( scanner);
	}

	protected void AddScannerOption(
		GcAlienPuzzleEntry scanner,
		string NAME,
		string ACTION
	){
		var target = new GcAlienPuzzleOption {
			Name = NAME,
			Cost = "C_ALLOWSCAN",
			Rewards = new() { ACTION },
			DisplayCost = true,
			Prop = new GcNPCPropTypes { NPCProp = libMBIN.NMS.GameComponents.GcNPCPropTypes.NPCPropEnum.DontCare},
			MarkInteractionComplete = true
		};
			
		scanner.Options.AddUnique(target);
	}
	
	protected void AddScannerNext(
		List<GcAlienPuzzleEntry> TABLE,
		GcAlienPuzzleEntry scanner,
		string LABEL
	){
		var ScanNext = new GcAlienPuzzleOption {
			Name = "More Options",
			Cost = "C_ALLOWSCAN",
			NextInteraction = LABEL,
			DisplayCost = true,
			Prop = new GcNPCPropTypes { NPCProp = libMBIN.NMS.GameComponents.GcNPCPropTypes.NPCPropEnum.DontCare},
			MarkInteractionComplete = false,
			KeepOpen = true
		};
		scanner.Options.Add(ScanNext);
		scanner.CustomFreighterTextIndex = -1;
		
		var NextEntry = new GcAlienPuzzleEntry {
			ProgressionIndex = -1,
			MinProgressionForSelection = 0,
			Id = LABEL,
			Type = new GcInteractionType {
				InteractionType = libMBIN.NMS.GameComponents.GcInteractionType.InteractionTypeEnum.SignalScanner
			},
			RequiresScanEvent = "",
			Race = new GcAlienRace { AlienRace = AlienRaceEnum.None },
						Prop = new GcNPCPropTypes { NPCProp = libMBIN.NMS.GameComponents.GcNPCPropTypes.NPCPropEnum.DontCare},
			Options = new List<GcAlienPuzzleOption>()
		};
		TABLE.Add(NextEntry);
	}
	
	protected void AddScannerLeave(
		GcAlienPuzzleEntry SCANNER
	){
		var AllLeave = new GcAlienPuzzleOption {
			Name = "ALL_REQUEST_LEAVE",
			//Cost = "",
			DisplayCost = true,
			Prop = new GcNPCPropTypes { NPCProp = libMBIN.NMS.GameComponents.GcNPCPropTypes.NPCPropEnum.DontCare},
			SelectedOnBackOut = false
		};
		
		SCANNER.Options.Add(AllLeave);
		SCANNER.CustomFreighterTextIndex = -1;
	}
	
	//...........................................................
}

//=============================================================================
