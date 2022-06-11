//=============================================================================
// Starting MultiTool settings|inventory for new games.
// If playing an expedition then goto
// C:\Users\[user]\AppData\Roaming\HelloGames\NMS\st_[...]\cache\SEASON_DATA_CACHE.JSON
// and change WeaponInventoryLayout.Slots.
//=============================================================================

public class Start_MultiTool : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcDefaultSaveData>(
			"METADATA/GAMESTATE/DEFAULTSAVEDATA.MBIN"
		);
		mbin.State.WeaponLayout.Slots = 48;
		mbin.State.WeaponInventory.Slots.Clear();
		mbin.State.WeaponInventory.Add(Inventory.Technology("SCAN1",         100, 100));  // Scanner
		mbin.State.WeaponInventory.Add(Inventory.Technology("SCANBINOC1",     -1, 100));  // Analysis Visor
		mbin.State.WeaponInventory.Add(Inventory.Technology("UT_SURVEY",     100, 100));  // Survey Device
		mbin.State.WeaponInventory.Add(Inventory.Technology("LASER",         100, 100));  // Mining Beam
		mbin.State.WeaponInventory.Add(Inventory.Technology("STRONGLASER",   100, 100));  // Advanced Mining Laser
		mbin.State.WeaponInventory.Add(Inventory.Technology("TERRAINEDITOR", 600, 600));  // Terrain Manipulator
		mbin.State.WeaponInventory.Add(Inventory.Technology("TERRAIN_GREN",   20,  20));  // Geology Cannon
		mbin.State.WeaponInventory.Add(Inventory.Technology("UT_MINER",      100, 100));  // Optical Drill
		mbin.State.WeaponInventory.Add(Inventory.Technology("GROUND_SHIELD", 100, 100));  // Personal Forcefield
	}
}

//=============================================================================
