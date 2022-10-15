//=============================================================================

public class Sample : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcDebugOptions>(
			"GCDEBUGOPTIONS.GLOBAL.MBIN"
		);
		mbin.SkipIntro = true;  // false
		mbin.SkipLogos = true;  // false
	}
}

//=============================================================================
