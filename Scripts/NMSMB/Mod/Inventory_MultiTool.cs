//=============================================================================
// New game MultiTool settings | inventory.
//=============================================================================

public class Inventory_MultiTool : cmk.NMS.ModScript
{
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcDefaultSaveData>(
			"METADATA/GAMESTATE/DEFAULTSAVEDATA.MBIN"
		);
		mbin.State.WeaponLayout.Slots = 48;
		GcInventoryContainer_Main(mbin.State.WeaponInventory);
		KnownTech(mbin.State.KnownTech);  // optional
	}

	//...........................................................
	
	protected void GcInventoryContainer_Main( GcInventoryContainer CONTAINER )
	{
		CONTAINER.Slots.Clear();
		CONTAINER.Add(Inventory.Technology("SCAN1",         100, 100));  // Scanner
		CONTAINER.Add(Inventory.Technology("SCANBINOC1",     -1, 100));  // Analysis Visor
		CONTAINER.Add(Inventory.Technology("UT_SURVEY",     100, 100));  // Survey Device
		CONTAINER.Add(Inventory.Technology("LASER",         100, 100));  // Mining Beam
		CONTAINER.Add(Inventory.Technology("STRONGLASER",   100, 100));  // Advanced Mining Laser
		CONTAINER.Add(Inventory.Technology("TERRAINEDITOR", 600, 600));  // Terrain Manipulator
		CONTAINER.Add(Inventory.Technology("TERRAIN_GREN",   20,  20));  // Geology Cannon
		CONTAINER.Add(Inventory.Technology("UT_MINER",      100, 100));  // Optical Drill
		CONTAINER.Add(Inventory.Technology("GROUND_SHIELD", 100, 100));  // Personal Forcefield
	}

	//...........................................................

	protected void KnownTech( List<nms.NMSString0x10> LIST )
	{
		LIST.AddUnique("SCAN1");
		LIST.AddUnique("SCANBINOC1");
		LIST.AddUnique("UT_SURVEY");
		LIST.AddUnique("LASER");
		LIST.AddUnique("STRONGLASER");
		LIST.AddUnique("TERRAINEDITOR");
		LIST.AddUnique("TERRAIN_GREN");
		LIST.AddUnique("UT_MINER");
		LIST.AddUnique("GROUND_SHIELD");
	}
}

//=============================================================================
