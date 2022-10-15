//=============================================================================
// Author: Jackty89
//=============================================================================

public class InstantScan : cmk.NMS.Script.ModClass
{
	public float ScanTime = 0.0f;
	protected override void Execute()
	{
		GcGamePlayGlobals();
	}

	//...........................................................

	protected void GcGamePlayGlobals()
	{
		var mbin = ExtractMbin<GcGameplayGlobals>("GCGAMEPLAYGLOBALS.GLOBAL.MBIN");
		mbin.BinocMinScanTime      = ScanTime;
		mbin.BinocScanTime         = ScanTime;
		mbin.BinocCreatureScanTime = ScanTime;
	}
}

//=============================================================================
