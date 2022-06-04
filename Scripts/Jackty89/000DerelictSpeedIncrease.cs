//=============================================================================

public class DerelictSpeedIncrease : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcPlayerGlobals>("GCPLAYERGLOBALS.GLOBAL.MBIN");
		var walkSpeed = mbin.GroundWalkSpeed;
		var runSpeed  = mbin.GroundRunSpeed;
		
		mbin.GroundWalkSpeedLowG = walkSpeed;		
		mbin.GroundRunSpeedLowG  = runSpeed;
	}

	//...........................................................
}

//=============================================================================
