//=============================================================================
// I come in peace.
//=============================================================================

public class Explorer : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		// Starting ship:
		// - explorer, big engines
		// - max warp drives
		// - max shields
		// - supply of warp cores
		
		// Starting Multitool
		// - vanilla (random)
		
		// Inventory
		// - vanilla

		// - auto-mark portals
		var Scan_Auto = Script<Scan_Auto>();
		Scan_Auto.EnablePortal = true;
	}
}

//=============================================================================
