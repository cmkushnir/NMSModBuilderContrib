//=============================================================================
// It's all about the base.
//=============================================================================

public class Settler : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		// Starting ship:
		// - vanilla (random)
		
		// Starting Multitool
		// - vanilla (random)
		
		// Inventory
		// - max suit and ship

		// - improved base items
		Script<Base_Extractor>().IsExecutable = true;
		Script<Base_Power>()    .IsExecutable = true;
		Script<Harvester_Rate>().IsExecutable = true;
		
		// - auto-mark base related locations
		var Scan_Auto = Script<Scan_Auto>();
		Scan_Auto.EnableShop       = true;
		Scan_Auto.EnableSettlement = true;
		Scan_Auto.EnableBaseSite   = true;
		Scan_Auto.EnableDroneHive  = true;  // can't settle until stop sentinel's
	}
}

//=============================================================================
