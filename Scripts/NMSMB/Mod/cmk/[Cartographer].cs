//=============================================================================
// Maps, maps, and more maps.
//=============================================================================

public class Cartographer : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		// Starting ship:
		// - shuttle, tiny
		
		// Starting Multitool
		// - vanilla (random)
		
		// Inventory
		// - vanilla		
		// - a bunch of each map type

		// you know a guy who knows a guy who can get you specific charts
		Mod<Starcharts>().IsExecutable = true;
		
		// - auto-mark most building locations
		Scan_Auto.EnableAbandoned         = true;
		Scan_Auto.EnableRadioTower        = true;
		Scan_Auto.EnableFactory           = true;
		Scan_Auto.EnableObservatory       = true;
		Scan_Auto.EnableShop              = true;
		Scan_Auto.EnableOutpost           = true;
		Scan_Auto.EnableSettlement        = true;
		Scan_Auto.EnableDroneHive         = true;
		Scan_Auto.EnableDroneHiveDisabled = true;
		Scan_Auto.EnableMonolith          = true;
		Scan_Auto.EnablePortal            = true;
	}
}

//=============================================================================
