//=============================================================================
// You derive comfort from the misfortunes of others.
//=============================================================================

public class Salvager : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		// Starting ship:
		// - dropship, front box, side x2 boxes
		
		// Starting Multitool
		// - vanilla (random)
		
		// Inventory
		// - max suit and ship slots
		// - loaded with raw materials to repair
		// - Storage Augmentation tokens
		
		// - buildable Salvage stations
		Mod<Placeable_Salvage>().IsExecutable = true;

		// - auto-mark crashed ships and such
		Scan_Auto.EnableRadioTower = true;
		Scan_Auto.EnableDistress   = true;
	}
}

//=============================================================================
