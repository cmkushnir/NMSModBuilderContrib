//=============================================================================
// Author: Jackty89
//=============================================================================

public class NoShipStart: cmk.NMS.Script.ModClass
{
	public static int GarageChoice             = 4;     //PickUpListIds => 4 == GARAGE_MECH
	public static bool Challenge               = false; //if you want an even harder challenge
	
	private static List <string> PickUpListIds = new List<string>()
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
		GcDebugOptions();
		EditLaserAndScanner();
		
		if (!Challenge)
			BasePartsDelux();
		if (Challenge)
			MeleeDamage();
		
		//CoreMissionEdits();
	}

	// not sure if this works
	protected void MeleeDamage()
	{
		var mbin = ExtractMbin<GcPlayerGlobals>("GCPLAYERGLOBALS.GLOBAL.MBIN");
		mbin.MeleeDamageScale      = 0;
		mbin.MeleeSpeedDamageBoost = 0;
		mbin.MeleeCooldown         = 1; //reduces melee speed
		mbin.MeleeCooldownAlt      = 1; //reduces melee speed
		
		mbin.TerrainResourceMeleeCollect.PlayerOffset   = 0;
		mbin.TerrainResourceMeleeCollect.SizeMin        = 0;
		mbin.TerrainResourceMeleeCollect.SizeMax        = 0;
		mbin.TerrainResourceMeleeCollect.RotateMin      = 0;
		mbin.TerrainResourceMeleeCollect.RotateMax      = 0;
		mbin.TerrainResourceMeleeCollect.StartOffsetMin = 0;
		mbin.TerrainResourceMeleeCollect.StartOffsetMax = 0;
		mbin.TerrainResourceMeleeCollect.TimeMin        = 0;
		mbin.TerrainResourceMeleeCollect.TimeMax        = 0;
		mbin.TerrainResourceMeleeCollect.StartSpeedMin  = 0;
		mbin.TerrainResourceMeleeCollect.StartSpeedMax  = 0;
		mbin.TerrainResourceMeleeCollect.OffsetMin      = 0;
		mbin.TerrainResourceMeleeCollect.OffsetMax      = 0;
	}
	
	//WORKS
	// skips the intialize/tut mission => after a 2nd space visit awekening should start
	protected void GcDebugOptions()
	{
		var mbin = ExtractMbin<GcDebugOptions>("GCDEBUGOPTIONS.GLOBAL.MBIN");
		mbin.SkipTutorial = true;
		mbin.SkipIntro 	  = true;;
		mbin.SkipLogos 	  = true;
	}
	//force ships minimal slots not sureif this affect other ships
	protected void GcRealityManagerData()
	{
		var mbin = ExtractMbin<GcRealityManagerData>("METADATA/REALITY/DEFAULTREALITY.MBIN");
		mbin.ShipStartingLayout.Slots = 10;
		mbin.ShipTechOnlyStartingLayout.Slots = 3;
		mbin.ShipCargoOnlyStartingLayout.Slots = 1;
		
		if(Challenge)
		{			
			foreach( var damageMP in mbin.DamageMultiplierTable)
			{
				foreach( var multiplier in damageMP.Multipliers)
				{
					if(multiplier.Type.DamageType == GcDamageType.DamageTypeEnum.Melee)
						multiplier.Multiplier = 0;
				}
			}
		}
	}
	// disables the starting missions of starting
	protected void CoreMissionEdits()
	{
		var mbin = ExtractMbin<GcMissionTable>("METADATA/SIMULATION/MISSIONS/COREMISSIONTABLE.MBIN");
		
		List <string> skipMissionIds = new List<string> () {"ACT1_STEP1", "ACT1_STEP3"};
		foreach(string missionId in skipMissionIds)
		{
			var mission       = mbin.Missions.Find(ID => ID.MissionID == missionId);
			mission.AutoStart = AutoStartEnum.None;
		}
	}
	protected void EditStarterShipSlotsAndInventoryItems()
	{
		var mbin = ExtractMbin<GcDefaultSaveData>("METADATA/GAMESTATE/DEFAULTSAVEDATA.MBIN");
		mbin.State.ShipInventory.Slots.Clear();

		mbin.State.ShipLayout.Slots = 10;	
		for (int i = 0; i <= mbin.State.ShipLayout.Slots; i++)		
			mbin.State.ShipInventory.Slots.Add(Inventory.Technology("SHIPSLOT_DMG12", 100, 100));

		
		mbin.State.KnownProducts.AddUnique("CARBON_SEAL");// quest related
		mbin.State.KnownProducts.AddUnique("BUILDBEACON");// quest related
		mbin.State.KnownProducts.AddUnique("BASE_FLAG");  // quest related
		mbin.State.KnownProducts.AddUnique("BP_ANALYSER");// quest related
		mbin.State.KnownProducts.AddUnique("ANTIMATTER"); // quest related
		mbin.State.KnownProducts.AddUnique("AM_HOUSING"); // quest related
		
		mbin.State.KnownTech.AddUnique("TERRAINEDITOR");  // quest related
		mbin.State.KnownTech.AddUnique("HYPERDRIVE");     // quest related
		mbin.State.KnownTech.AddUnique("STRONGLASER");    // quest related

		mbin.State.WeaponInventory.Slots.Find(ID => ID.Id =="SCAN1").DamageFactor = 0;

		if(Challenge)
		{
			mbin.State.WeaponInventory.Slots.RemoveAt(0);
			mbin.State.WeaponInventory.Slots.Add(Inventory.Technology("BOLT", 100, 100));			
			mbin.State.Inventory.Add(Inventory.Product("AMMO",20,20));
		}

		if(!Challenge)
		{
			mbin.State.Inventory.Add(Inventory.Product(PickUpListIds[GarageChoice],1,1));
			mbin.State.Inventory.Add(Inventory.Product("BUILDBEACON",1,1));
		}
	}
	protected void EditStartShipStarterLocation()
	{
		var mbin = ExtractMbin<GcBuildingGlobals>("GCBUILDINGGLOBALS.GLOBAL.MBIN");

		mbin.StartCrashSiteMinDistance = 250000;
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
	
	//should technically make it so that weaposn dont have these 2 upgrades
	protected void EditLaserAndScanner()
	{
		var mbin                       = ExtractMbin<GcTechnologyTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCTECHNOLOGYTABLE.MBIN");
		var laser                      = mbin.Table.Find(ID => ID.ID == "LASER");
		laser.Rarity                   = new GcTechnologyRarity { TechnologyRarity = TechnologyRarityEnum.Impossible };
		laser.TechShopRarity           = new GcTechnologyRarity { TechnologyRarity = TechnologyRarityEnum.Impossible };
		
		var strongLaser                = mbin.Table.Find(ID => ID.ID == "STRONGLASER");
		strongLaser.Rarity             = new GcTechnologyRarity { TechnologyRarity = TechnologyRarityEnum.Impossible };
		strongLaser.TechShopRarity     = new GcTechnologyRarity { TechnologyRarity = TechnologyRarityEnum.Impossible };
		strongLaser.Value              = 250000;
		
		var scanner                    = mbin.Table.Find(ID => ID.ID == "SCAN1");
		scanner.Requirements[0].Amount = 1;		

	}
}