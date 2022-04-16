//=============================================================================
// You tell me ...
//=============================================================================

public class Custom : cmk.NMS.ModScript
{
	protected override void Execute()
	{
		Log.AddInformation("Playing as: Custom");
		Scan_Auto.EnableBlackBox         = true;
		Scan_Auto.EnableBones            = true;
		Scan_Auto.EnableSettlements      = true;
		Scan_Auto.EnableMinorSettlements = true;
	}
}

//=============================================================================
