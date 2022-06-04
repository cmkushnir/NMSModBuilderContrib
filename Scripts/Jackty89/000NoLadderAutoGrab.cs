//=============================================================================
// Author: Jackty89
//=============================================================================

public class NoLadderAutoGrab: cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		GcCharacterGlobals();
	}

	//...........................................................

	protected void GcCharacterGlobals()
	{
		var mbin = ExtractMbin<GcCharacterGlobals>("GCCHARACTERGLOBALS.GLOBAL.MBIN");
		mbin.LadderDistanceToAutoMount = -1;
		mbin.SitPostureChangeTimeMin   = 0.1f;
		mbin.SitPostureChangeTimeMax   = 1;
	}
}

//=============================================================================
