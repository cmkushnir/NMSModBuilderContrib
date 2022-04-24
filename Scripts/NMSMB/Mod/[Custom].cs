//=============================================================================
// You tell me ...
//=============================================================================

public class Custom : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		Scan_Auto.EnableBlackBox         = true;
		Scan_Auto.EnableBones            = true;
		Scan_Auto.EnableSettlements      = true;
		Scan_Auto.EnableMinorSettlements = true;
	}
}

//=============================================================================
