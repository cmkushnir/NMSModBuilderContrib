//=============================================================================
// Author: Jackty89
//=============================================================================

public class CustomDebugOptions : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		GcDebugOptions();
	}

	//...........................................................

	protected void GcDebugOptions()
	{
		var mbin = ExtractMbin<GcDebugOptions>(
			"GCDEBUGOPTIONS.GLOBAL.MBIN"
		);
		mbin.DisableProfanityFilter = true;
		mbin.SkipIntro = true;
		mbin.SkipLogos = true;
		//mbin.MultiplePlayerFreightersInASystem = true; // doesnt work anymore
	}
}

//=============================================================================
