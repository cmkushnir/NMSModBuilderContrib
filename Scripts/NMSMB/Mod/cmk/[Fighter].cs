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

		// - your old contacts can get you BFG level upgrades
		Mod<Add_Tech_Rockets>().IsExecutable   = true;
	//	Add_Tech_Rockets.AddShipRocketTech     = true;  // fixed
		Add_Tech_Rockets.AddShipRocketProcTech = true;	// proc-gen

		// - auto-mark battle targets
		Scan_Auto.EnableDroneHive = true;
		// todo: Depot
	}
}

//=============================================================================
