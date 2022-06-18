//=============================================================================
// Misc. usabilty tweaks.
//=============================================================================

public class Globals : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		Try(() => GcDebugOptions());
		Try(() => GcCharacterGlobals());
	}

	//...........................................................

	protected void GcDebugOptions()
	{
		var mbin = ExtractMbin<GcDebugOptions>(
			"GCDEBUGOPTIONS.GLOBAL.MBIN"
		);				
		mbin.SkipIntro = true;
		mbin.SkipLogos = true;
		mbin.BootMusic = false;
		mbin.DisableSaveSlotSorting     = true;
		mbin.UseHeavyAir                = false;
		mbin.SpecialsShop               = true;
		mbin.DisableProfanityFilter     = true;
		mbin.GenerateFarLodBuildingDist = 4000;  // 1000
	}

	//...........................................................

	protected void GcCharacterGlobals()
	{
		var mbin = ExtractMbin<GcCharacterGlobals>(
			"GCCHARACTERGLOBALS.GLOBAL.MBIN"
		);
		// prevent ladder auto-mount, useful w/ ladders in bases
		mbin.LadderDistanceToAutoMount = 0.05f;  // 0.4
	}
}

//=============================================================================
