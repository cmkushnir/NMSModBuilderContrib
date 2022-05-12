//=============================================================================
// You tell me ...
//=============================================================================

public class Custom : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
	//	Scan_Auto.EnableBlackBox         = false;
		Scan_Auto.EnableBones            = false;
		Scan_Auto.EnableSettlements      = false;
	//	Scan_Auto.EnableMinorSettlements = false;
		Scan_Auto.EnableBaseSite         = false;
		Scan_Auto.EnableMonolith         = false;
		Scan_Auto.EnablePortal           = false;
		Scan_Auto.EnableGrave            = false;
	}
}

//=============================================================================
