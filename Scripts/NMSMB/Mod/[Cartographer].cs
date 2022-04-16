//=============================================================================
// Maps, maps, and more maps.
//=============================================================================

public class Cartographer : cmk.NMS.ModScript
{
	protected override void Execute()
	{
		Log.AddInformation("Playing as: Cartographer");

		// Starting ship:
		// - shuttle, tiny
		
		// Starting Multitool
		// - vanilla (random)
		
		// Inventory
		// - vanilla		
		// - a bunch of each map type

		// - auto-mark most building locations
		Scan_Auto.EnableBaseSite         = true;
		Scan_Auto.EnableMinorSettlements = true;
		Scan_Auto.EnableMonolith         = true;
		Scan_Auto.EnablePortal           = true;
		Scan_Auto.EnableSettlements      = true;
	}
}

//=============================================================================
