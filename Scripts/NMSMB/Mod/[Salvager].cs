//=============================================================================
// You derive comfort from the misfortunes of others.
//=============================================================================

public class Salvager : cmk.NMS.ModScript
{
	protected override void Execute()
	{
		Log.AddInformation("Playing as: Salvager");

		// Starting ship:
		// - dropship, front box, side x2 boxes
		
		// Starting Multitool
		// - vanilla (random)
		
		// Inventory
		// - max suit and ship slots
		// - loaded with raw materials to repair
		// - Storage Augmentation tokens
		
		// - buildable Salvage stations

		// - auto-mark crashed ships and radio towers
		Scan_Auto.EnableBlackBox = true;
	}
}

//=============================================================================
