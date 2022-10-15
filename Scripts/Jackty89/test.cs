//=============================================================================

public class test : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		
		var defaultSaveData = ExtractMbin<GcDefaultSaveData>("METADATA/GAMESTATE/DEFAULTSAVEDATA.MBIN");
		defaultSaveData.State.ShipInventory.Slots.Add(Inventory.Product("HYPERFUEL1", 1, 1));		
		defaultSaveData.State.ShipInventory.Slots.Add(Inventory.Product("LAUNCHFUEL", 1, 1));
		
		var gameplayGlobals = ExtractMbin<GcGameplayGlobals>("GCGAMEPLAYGLOBALS.GLOBAL.MBIN");
		gameplayGlobals.DifficultyConfig.StartWithAllItemsKnownDisabledData.InitialShipInventory.Slots.Find(Element => Element.Id =="SHIPGUN1").Id = "SHIPMINIGUN";
		gameplayGlobals.DifficultyConfig.StartWithAllItemsKnownDisabledData.InitialShipInventory.Slots.Find(Element => Element.Id =="SHIPJUMP1").Amount = 200;
		gameplayGlobals.DifficultyConfig.StartWithAllItemsKnownDisabledData.InitialShipInventory.Slots.Find(Element => Element.Id =="SHIPJUMP1").DamageFactor = 0;
		gameplayGlobals.DifficultyConfig.StartWithAllItemsKnownDisabledData.InitialShipInventory.Slots.Find(Element => Element.Id =="LAUNCHER").Amount = 200;
		gameplayGlobals.DifficultyConfig.StartWithAllItemsKnownDisabledData.InitialShipInventory.Slots.Find(Element => Element.Id =="LAUNCHER").DamageFactor = 0;
		

		gameplayGlobals.DifficultyConfig.StartWithAllItemsKnownEnabledData.InitialShipInventory.Slots.Find(Element => Element.Id =="SHIPGUN1").Id = "SHIPMINIGUN";
		gameplayGlobals.DifficultyConfig.StartWithAllItemsKnownDisabledData.InitialShipInventory.Slots.Find(Element => Element.Id =="SHIPJUMP1").Amount = 200;
		gameplayGlobals.DifficultyConfig.StartWithAllItemsKnownDisabledData.InitialShipInventory.Slots.Find(Element => Element.Id =="SHIPJUMP1").DamageFactor = 0;
		gameplayGlobals.DifficultyConfig.StartWithAllItemsKnownDisabledData.InitialShipInventory.Slots.Find(Element => Element.Id =="LAUNCHER").Amount = 200;
		gameplayGlobals.DifficultyConfig.StartWithAllItemsKnownDisabledData.InitialShipInventory.Slots.Find(Element => Element.Id =="LAUNCHER").DamageFactor = 0;
		
		var debugGlobals = ExtractMbin<GcDebugOptions>("GCDEBUGOPTIONS.GLOBAL.MBIN");
		debugGlobals.ForceInitialShip = false;
	}

	//...........................................................
}

//=============================================================================
