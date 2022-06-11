//=============================================================================
// You tell me ...
//=============================================================================

public class Custom : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		Scan_Auto.EnableShelter           = false;
		Scan_Auto.EnableAbandoned         = false;
	//	Scan_Auto.EnableRadioTower        = false;
		Scan_Auto.EnableFactory           = false;
		Scan_Auto.EnableObservatory       = false;
	//	Scan_Auto.EnableShop              = false;
		Scan_Auto.EnableOutpost           = false;
		Scan_Auto.EnableSettlement        = false;
		Scan_Auto.EnableBaseSite          = false;
	//	Scan_Auto.EnableDroneHive         = false;
		Scan_Auto.EnableDroneHiveDisabled = false;
		Scan_Auto.EnableMonolith          = false;
	//	Scan_Auto.EnablePortal            = false;
	//	Scan_Auto.EnableDistress          = false;
		Scan_Auto.EnableGrave             = false;
		Scan_Auto.EnableBones             = false;
		Scan_Auto.EnableTreasureRuin      = false;
	}
}

//=============================================================================
