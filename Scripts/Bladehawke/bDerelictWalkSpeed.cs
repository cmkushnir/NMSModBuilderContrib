//=============================================================================

public class bDerelictWalkSpeed : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcPlayerGlobals>(
			"GCPLAYERGLOBALS.GLOBAL.MBIN"
		);

		mbin.GroundWalkSpeedLowG = mbin.GroundWalkSpeed;
		mbin.GroundRunSpeedLowG  = mbin.GroundRunSpeed;
	}

	//...........................................................
}

//=============================================================================
