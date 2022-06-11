//=============================================================================
// Starting Ship settings|inventory for new games.
// If playing an expedition then goto
// C:\Users\[user]\AppData\Roaming\HelloGames\NMS\st_[...]\cache\SEASON_DATA_CACHE.JSON
// and change StartingSuitLayout.Slots, StartingSuitTechLayout.Slots, StartingSuitCargoLayout.Slots.
//=============================================================================

public class Start_Ship : cmk.NMS.Script.ModClass
{
	public static int MaxAmount   => Inventory_Stack.MaxAmount;
	public static int StartAmount => MaxAmount / 2;	
	
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
		mbin.ShipStartingLayout         .Slots = 48;
		mbin.ShipTechOnlyStartingLayout .Slots = 48;
		mbin.ShipCargoOnlyStartingLayout.Slots = 48;
	}

	//...........................................................

	protected void GcDefaultSaveData()
	{
		var mbin = ExtractMbin<GcDefaultSaveData>(
			"METADATA/GAMESTATE/DEFAULTSAVEDATA.MBIN"
		);
		mbin.State.ShipInventory.Slots.Clear();
		mbin.State.ShipInventory.Add(Inventory.Technology("LAUNCHER",        100, 100));  // Launch Thruster
		mbin.State.ShipInventory.Add(Inventory.Technology("UT_LAUNCHCHARGE", 100, 100));  // Launch System Recharger		
		mbin.State.ShipInventory.Add(Inventory.Technology("SHIPMINIGUN",     100, 100));  // Infra-Knife Accelerator
		mbin.State.ShipInventory.Add(Inventory.Technology("SHIPSHIELD",      100, 100));  // Deflector Shield
		mbin.State.ShipInventory.Add(Inventory.Technology("UT_SHIPSHIELD",   100, 100));  // Ablative Armour
		mbin.State.ShipInventory.Add(Inventory.Technology("SHIPJUMP1",       100, 100));  // Pulse Engine
		mbin.State.ShipInventory.Add(Inventory.Technology("HYPERDRIVE",      100, 100));  // Hyperdrive
		mbin.State.ShipInventory.Add(Inventory.Technology("UT_QUICKWARP" ,   100, 100));  // Emergency Warp Unit
		mbin.State.ShipInventory.Add(Inventory.Technology("SHIP_TELEPORT",   100, 100));  // Teleport Receiver
		mbin.State.ShipInventory.Add(Inventory.Technology("SHIPSCAN_ECON",   100, 100));  // Economy  Scanner
		mbin.State.ShipInventory.Add(Inventory.Technology("SHIPSCAN_COMBAT", 100, 100));  // Conflict Scanner
	}
}

//=============================================================================
