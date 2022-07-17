//=============================================================================

public class DerelictSpeedIncrease : cmk.NMS.Script.ModClass
{
	public static float SpeedMultiplier = 1f;

	protected override void Execute()
	{
		var mbin                 = ExtractMbin<GcPlayerGlobals>("GCPLAYERGLOBALS.GLOBAL.MBIN");
		var walkSpeed            = mbin.GroundWalkSpeed;
		var runSpeed             = mbin.GroundRunSpeed;
		
		mbin.GroundWalkSpeedLowG = walkSpeed * SpeedMultiplier;		
		mbin.GroundRunSpeedLowG  = runSpeed * SpeedMultiplier;
	}

	//...........................................................
}

//=============================================================================
