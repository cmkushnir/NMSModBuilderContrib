//=============================================================================
// You like playing in the dirt ...
// like an Archaeologist but without any of the delusions of grandeur.
//=============================================================================

public class Miner : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		// Starting ship:
		// - dropship, front box, side balls
		
		// Starting Multitool
		// - max mining
		
		// Inventory
		// - max suit and ship slots

		// - auto-mark rare resources
		var Scan_Auto = Script<Scan_Auto>();
		Scan_Auto.EnableBones = true;
		// todo: floating crystals, buried gems, ...
	}
}

//=============================================================================
