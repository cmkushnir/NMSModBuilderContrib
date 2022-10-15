//=============================================================================
// Regardless of what side of the law you are on, you love the pew-pew bang!.
//=============================================================================

public class Fighter : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		// Starting ship:
		// - fighter
		// - max weapons
		
		// Starting Multitool
		// - rifle
		// - max weapons
		
		// Inventory
		// - vanilla
		
		// - increase chance for post-warp freighter battles
		// - increase chance for pirate attacks

		// steady hand
		Script<MultiTool_Recoil>().IsExecutable = true;

		// - your old contacts can get you BFG level upgrades
		var Add_Tech_Rockets = Script<Add_Tech_Rockets>();
		Add_Tech_Rockets.IsExecutable   = true;
	//	Add_Tech_Rockets.AddShipRocketTech     = true;  // fixed
		Add_Tech_Rockets.AddShipRocketProcTech = true;	// proc-gen

		// - auto-mark battle targets
		var Scan_Auto = Script<Scan_Auto>();
		Scan_Auto.EnableDroneHive = true;
		// todo: Depot
	}
}

//=============================================================================
