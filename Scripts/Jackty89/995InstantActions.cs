//=============================================================================
// Author: Jackty89
//=============================================================================

public class InstantActions : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		GcUiOptions();
	}

	//...........................................................

	protected void GcUiOptions()
	{
		var mbin = ExtractMbin<GcUIGlobals>("GCUIGLOBALS.GLOBAL.MBIN");
		mbin.RefinerPadStartTime                = 0.01f;
		mbin.FrontendConfirmTimeMouseMultiplier = 6.0f;
		mbin.FrontendConfirmTime                = 0.02f;
		mbin.FrontendConfirmTimeSlow            = 0.7f;
	}
}

//=============================================================================
