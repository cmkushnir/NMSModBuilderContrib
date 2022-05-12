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
		Mod<Base_Extractor>().IsExecutable = true;
		Mod<Base_Power>()    .IsExecutable = true;
		Mod<Harvester_Rate>().IsExecutable = true;
		
		// - auto-mark base related locations
		Scan_Auto.EnableBaseSite         = true;
		Scan_Auto.EnableSettlements      = true;
		Scan_Auto.EnableMinorSettlements = true;
	}
}

//=============================================================================
