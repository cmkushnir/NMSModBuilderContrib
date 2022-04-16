//=============================================================================
// It's all about the base.
//=============================================================================

public class Settler : cmk.NMS.ModScript
{
	protected override void Execute()
	{
		Log.AddInformation("Playing as: Settler");

		// Starting ship:
		// - vanilla (random)
		
		// Starting Multitool
		// - vanilla (random)
		
		// Inventory
		// - max suit and ship
		
		// - auto-mark base related locations
		Scan_Auto.EnableBaseSite         = true;
		Scan_Auto.EnableSettlements      = true;
		Scan_Auto.EnableMinorSettlements = true;
		Scan_Auto.EnablePortal           = true;
	}
}

//=============================================================================
