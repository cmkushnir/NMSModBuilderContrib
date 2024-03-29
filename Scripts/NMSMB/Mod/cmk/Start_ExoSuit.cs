﻿//=============================================================================
// Starting ExoSuit settings|inventory for new games.
// If playing an expedition then goto
// C:\Users\[user]\AppData\Roaming\HelloGames\NMS\st_[...]\cache\SEASON_DATA_CACHE.JSON
// and change StartingSuitSlots, StartingSuitTechSlots, StartingSuitCargoSlots,
// and StartingSuitLayout.Slots, StartingSuitTechLayout.Slots, StartingSuitCargoLayout.Slots.
//=============================================================================

public class Start_ExoSuit : cmk.NMS.Script.ModClass
{
	public int MaxAmount => Script<Inventory_Stack>().MaxAmount;
	public int StartAmount => MaxAmount / 2;

	//...........................................................

	protected override void Execute()
	{
		Try(() => GcRealityManagerData());
		Try(() => GcDefaultSaveData());
	}

	//...........................................................

	protected void GcRealityManagerData()
	{
		var mbin = ExtractMbin<GcRealityManagerData>(
			"METADATA/REALITY/DEFAULTREALITY.MBIN"
		);
		mbin.SuitStartingSlotLayout        .Slots = 120;
		mbin.SuitTechOnlyStartingSlotLayout.Slots = 120;
		mbin.SuitCargoStartingSlotLayout   .Slots = 120;  // unused as of 4.0
	}

	//...........................................................

	protected void GcDefaultSaveData()
	{
		var mbin = ExtractMbin<GcDefaultSaveData>(
			"METADATA/GAMESTATE/DEFAULTSAVEDATA.MBIN"
		);

		mbin.State.Inventory         .Slots.Clear();
		mbin.State.Inventory_TechOnly.Slots.Clear();
		mbin.State.Inventory_Cargo   .Slots.Clear();  // unused as of 4.0

		// if you add more items to an inventory than its StartingSlotLayout the extra items are ignored.
		// if you stack more in a slot than allowed the amount is truncated to the max allowable.
		GcInventoryContainer_Main (mbin.State.Inventory);
		GcInventoryContainer_Tech (mbin.State.Inventory_TechOnly);
		GcInventoryContainer_Cargo(mbin.State.Inventory);  // 4.0, change from cargo to main inv
	}

	//...........................................................

	protected void GcInventoryContainer_Main( GcInventoryContainer CONTAINER )
	{
		CONTAINER.Add(Inventory.Product("ACCESS3", 1, MaxAmount));  // AtlasPass v3

		// see Reward_Location for custom MAP_*
		CONTAINER.Add(Inventory.Product("MAP_DISTRESS", 1000, MaxAmount));  // 
		CONTAINER.Add(Inventory.Product("MAP_PORTAL",   1000, MaxAmount));  // 

		CONTAINER.Add(Inventory.Product("NAV_DATA",    10000, MaxAmount));  // Navigation Data
		CONTAINER.Add(Inventory.Product("ABAND_LOCATOR", 200, MaxAmount));  // Emergency Broadcast Receiver
		CONTAINER.Add(Inventory.Product("POI_LOCATOR",   200, MaxAmount));  // Anomaly Detector

		CONTAINER.Add(Inventory.Product("SHIP_INV_TOKEN", 2000, MaxAmount));  // Storage Augmentation
		CONTAINER.Add(Inventory.Product("WEAP_INV_TOKEN", 1000, MaxAmount));  // Multi-Tool Expansion Slot
		CONTAINER.Add(Inventory.Product("SUIT_INV_TOKEN",   40, MaxAmount));  // Exosuit Expansion Unit, for seasonal

		CONTAINER.Add(Inventory.Product("REPAIRKIT", 2000, MaxAmount));  // Repair Kit

		CONTAINER.Add(Inventory.Product("EXP_CURIO1", 500, MaxAmount));  // Korvax Casing
		CONTAINER.Add(Inventory.Product("WAR_CURIO2", 500, MaxAmount));  // Vy'keen Dagger
		CONTAINER.Add(Inventory.Product("TRA_CURIO1", 500, MaxAmount));  // Gek Relic

		CONTAINER.Add(Inventory.Product("AMMO",       StartAmount, MaxAmount));  // Projectile Ammunition
		CONTAINER.Add(Inventory.Product("BAIT_BASIC", StartAmount, MaxAmount));  // Creature Pellets

		// use at refiner to create a void egg:
		CONTAINER.Add(Inventory.Product("EGG1",          10, MaxAmount));  // Companion Egg
		CONTAINER.Add(Inventory.Substance("TIMEDUST", 20000, MaxAmount));  // Somnal Dust
		CONTAINER.Add(Inventory.Substance("TIMEMILK", 20000, MaxAmount));  // Ancestral Memories
	}

	//...........................................................

	protected void GcInventoryContainer_Tech( GcInventoryContainer CONTAINER )
	{
		CONTAINER.Add(Inventory.Technology("SUIT_REFINER",   100, 100));  // Personal Refiner
		CONTAINER.Add(Inventory.Technology("ENERGY",         100, 100));  // Life Support
		CONTAINER.Add(Inventory.Technology("PROTECT",        100, 100));  // Hazard Protection
		CONTAINER.Add(Inventory.Technology("POWERGLOVE",     100, 100));  // Haz-Mat Gauntlet
		CONTAINER.Add(Inventory.Technology("UT_WATER",       100, 100));  // Aeration Membrane
		CONTAINER.Add(Inventory.Technology("UT_WATERENERGY", 100, 100));  // Oxygen Rerouter
		CONTAINER.Add(Inventory.Technology("JET1",           100, 100));  // Jetpack
		CONTAINER.Add(Inventory.Technology("UT_MIDAIR",      100, 100));  // Airburst Engine
		CONTAINER.Add(Inventory.Technology("UT_WATERJET",    100, 100));  // Efficient Water Jets
	}

	//...........................................................

	// mainly geared to play-as salvager, may want to comment out items for other play-as styles.
	protected void GcInventoryContainer_Cargo( GcInventoryContainer CONTAINER )
	{
		// essential first		
		CONTAINER.Add(Inventory.Substance("LAUNCHSUB", StartAmount, MaxAmount));  // Di-hydrogen
		CONTAINER.Add(Inventory.Substance("ROCKETSUB", StartAmount, MaxAmount));  // Tritium
		CONTAINER.Add(Inventory.Substance("OXYGEN",    StartAmount, MaxAmount));  // Oxygen
		CONTAINER.Add(Inventory.Substance("FUEL1",     StartAmount, MaxAmount));  // Carbon
		CONTAINER.Add(Inventory.Substance("CATALYST1", StartAmount, MaxAmount));  // Sodium	
		CONTAINER.Add(Inventory.Substance("LAND1",     StartAmount, MaxAmount));  // Ferrite Dust
		CONTAINER.Add(Inventory.Substance("YELLOW2",   StartAmount, MaxAmount));  // Copper
		CONTAINER.Add(Inventory.Substance("CAVE1",     StartAmount, MaxAmount));  // Cobalt

		// then nice-to-have, duplicated in Inventory_Ship.GcInventoryContainer_MainSeasonal
		CONTAINER.Add(Inventory.Substance("LAUNCHSUB2", StartAmount, MaxAmount));  // Deuterium

		CONTAINER.Add(Inventory.Substance("COLD1", StartAmount, MaxAmount));  // Dioxite

		CONTAINER.Add(Inventory.Substance("FUEL2", StartAmount, MaxAmount));  // Condensed Carbon

		CONTAINER.Add(Inventory.Substance("CATALYST2", StartAmount, MaxAmount));  // Sodium Nitrate

		CONTAINER.Add(Inventory.Substance("LAND2", StartAmount, MaxAmount));  // Pure Ferrite
		CONTAINER.Add(Inventory.Substance("LAND3", StartAmount, MaxAmount));  // Magnetised Ferrite
		CONTAINER.Add(Inventory.Substance("SAND1", StartAmount, MaxAmount));  // Silicate Powder

		CONTAINER.Add(Inventory.Substance("EX_YELLOW", StartAmount, MaxAmount));  // Activated Copper
		CONTAINER.Add(Inventory.Substance("STELLAR2",  StartAmount, MaxAmount));  // Chromatic Metal

		CONTAINER.Add(Inventory.Substance("CAVE2", StartAmount, MaxAmount));  // Ionised Cobalt

		CONTAINER.Add(Inventory.Substance("ASTEROID1", StartAmount, MaxAmount));  // Silver
		CONTAINER.Add(Inventory.Substance("ASTEROID3", StartAmount, MaxAmount));  // Platinum
		CONTAINER.Add(Inventory.Substance("ASTEROID2", StartAmount, MaxAmount));  // Gold
		CONTAINER.Add(Inventory.Substance("RADIO1",    StartAmount, MaxAmount));  // Uranium		
		CONTAINER.Add(Inventory.Substance("DUSTY1",    StartAmount, MaxAmount));  // Pyrite

		CONTAINER.Add(Inventory.Substance("CREATURE1", StartAmount, MaxAmount));  // Mordite
		CONTAINER.Add(Inventory.Substance("ROBOT1",    StartAmount, MaxAmount));  // Pugneum

		CONTAINER.Add(Inventory.Substance("HOT1",  StartAmount, MaxAmount));  // Phosphorus
		CONTAINER.Add(Inventory.Substance("LUSH1", StartAmount, MaxAmount));  // Paraffinium

		CONTAINER.Add(Inventory.Substance("WATER1", StartAmount, MaxAmount));  // Salt
		CONTAINER.Add(Inventory.Substance("WATER2", StartAmount, MaxAmount));  // Chlorine
		CONTAINER.Add(Inventory.Substance("TOXIC1", StartAmount, MaxAmount));  // Ammonia

		CONTAINER.Add(Inventory.Substance("RED2",   StartAmount, MaxAmount));  // Cadmium
		CONTAINER.Add(Inventory.Substance("GREEN2", StartAmount, MaxAmount));  // Emeril
		CONTAINER.Add(Inventory.Substance("BLUE2",  StartAmount, MaxAmount));  // Indium

		CONTAINER.Add(Inventory.Product("TECH_COMP", StartAmount, MaxAmount));  // Wiring Loom
		CONTAINER.Add(Inventory.Product("MICROCHIP", StartAmount, MaxAmount));  // Microprocessor

		CONTAINER.Add(Inventory.Product("GEODE_SPACE",   StartAmount, MaxAmount));  // Tritium Hypercluster
		CONTAINER.Add(Inventory.Product("STORM_CRYSTAL", StartAmount, MaxAmount));  // Storm Crystal

		CONTAINER.Add(Inventory.Product("FARMPROD3", StartAmount, MaxAmount));  // Glass
		CONTAINER.Add(Inventory.Product("CLAMPEARL", StartAmount, MaxAmount));  // Living Pearl
		CONTAINER.Add(Inventory.Product("VENTGEM",   StartAmount, MaxAmount));  // Crystal Sulphide

		CONTAINER.Add(Inventory.Product("ANTIMATTER", StartAmount, MaxAmount));  // Antimatter
		CONTAINER.Add(Inventory.Product("HYPERFUEL1", StartAmount, MaxAmount));  // Warp Cell

		CONTAINER.Add(Inventory.Product("JELLY",       StartAmount, MaxAmount));  // Di-hydrogen Jelly
		CONTAINER.Add(Inventory.Product("NANOTUBES",   StartAmount, MaxAmount));  // Carbon Nanotubes
		CONTAINER.Add(Inventory.Product("CASING",      StartAmount, MaxAmount));  // Metal Plating
		CONTAINER.Add(Inventory.Product("CARBON_SEAL", StartAmount, MaxAmount));  // Hermetic Seal

		CONTAINER.Add(Inventory.Substance("AF_METAL", StartAmount, MaxAmount));  // Tainted Metal
	}
}

//=============================================================================
