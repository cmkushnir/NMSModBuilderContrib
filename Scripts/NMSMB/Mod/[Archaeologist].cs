//=============================================================================
// The offical line is you're unearthing ancient knowledge,
// the reality is you just like playing in the dirt.
//=============================================================================

public class Archaeologist : cmk.NMS.ModScript
{
	protected override void Execute()
	{
		Log.AddInformation("Playing as: Archaeologist");

		// Starting ship:
		// - dropship, x2 side boxes
		
		// Starting Multitool
		// - vanilla (random)
		
		// Inventory
		// - vanilla
		
		// - auto-mark buried items and ruins
		Scan_Auto.EnableBones = true;
		Scan_Auto.EnableGrave = true;
	}
}

//=============================================================================
