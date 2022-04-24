//=============================================================================
// New game Ship settings | inventory.
//=============================================================================

public class Inventory_Ship : cmk.NMS.Script.ModClass
{
	public static int MaxAmount   { get; set; } = Inventory_.MaxAmount;
	public static int StartAmount { get; set; } = MaxAmount / 2;	
	
	//...........................................................
	
	protected override void Execute()
	{
		Try(() => GcRealityManagerData());
		Try(() => GcDefaultSaveData());
	}

	//...........................................................
	
	// if playing an expedition then goto
	// C:\Users\[user]\AppData\Roaming\HelloGames\NMS\st_[...]\cache\SEASON_DATA_CACHE.JSON
	// and change StartingSuitLayout.Slots, StartingSuitTechLayout.Slots, StartingSuitCargoLayout.Slots.
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
		GcInventoryContainer_Main(mbin.State.ShipInventory);
		KnownTech(mbin.State.KnownTech);  // optional
	}

	//...........................................................

	protected void GcInventoryContainer_Main( GcInventoryContainer CONTAINER )
	{
		CONTAINER.Slots.Clear();
		CONTAINER.Add(Inventory.Technology("LAUNCHER",        100, 100));  // Launch Thruster
		CONTAINER.Add(Inventory.Technology("UT_LAUNCHCHARGE", 100, 100));  // Launch System Recharger		
		CONTAINER.Add(Inventory.Technology("SHIPMINIGUN",     100, 100));  // Infra-Knife Accelerator
		CONTAINER.Add(Inventory.Technology("SHIPSHIELD",      100, 100));  // Deflector Shield
		CONTAINER.Add(Inventory.Technology("UT_SHIPSHIELD",   100, 100));  // Ablative Armour
		CONTAINER.Add(Inventory.Technology("SHIPJUMP1",       100, 100));  // Pulse Engine
		CONTAINER.Add(Inventory.Technology("HYPERDRIVE",      100, 100));  // Hyperdrive
		CONTAINER.Add(Inventory.Technology("UT_QUICKWARP" ,   100, 100));  // Emergency Warp Unit
		CONTAINER.Add(Inventory.Technology("SHIP_TELEPORT",   100, 100));  // Teleport Receiver
		CONTAINER.Add(Inventory.Technology("SHIPSCAN_ECON",   100, 100));  // Economy  Scanner
		CONTAINER.Add(Inventory.Technology("SHIPSCAN_COMBAT", 100, 100));  // Conflict Scanner
	}

	//...........................................................

	// optionally, make sure we know how to re-add all items in inventory
	protected void KnownTech( List<nms.NMSString0x10> LIST )
	{
		LIST.AddUnique("LAUNCHER");
		LIST.AddUnique("UT_LAUNCHCHARGE");
		LIST.AddUnique("SHIPMINIGUN");
		LIST.AddUnique("SHIPSHIELD");
		LIST.AddUnique("UT_SHIPSHIELD");
		LIST.AddUnique("SHIPJUMP1");
		LIST.AddUnique("HYPERDRIVE");
		LIST.AddUnique("UT_QUICKWARP");
		LIST.AddUnique("SHIP_TELEPORT");
		LIST.AddUnique("SHIPSCAN_ECON");
		LIST.AddUnique("SHIPSCAN_COMBAT");
	}
}

//=============================================================================
