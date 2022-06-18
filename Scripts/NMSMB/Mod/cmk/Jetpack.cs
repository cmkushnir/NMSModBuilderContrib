//=============================================================================
// Adjust jetpack settings.
//=============================================================================

public class Jetpack : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcPlayerGlobals>(
			"GCPLAYERGLOBALS.GLOBAL.MBIN"
		);
		mbin.JetpackDrainHorizontalFactor = 0.2f;  // 2.5
		mbin.JetpackForce = 40.0f;  // 31
		mbin.JetpackBrake =  4.0f;  //  2.2
		mbin.JetpackMaxSpeed   = 15.0f;  //  5 
		mbin.JetpackMaxUpSpeed = 40.0f;  // 30
		mbin.JetpackIgnitionForce   = 90.0f;  // 60
		mbin.JetpackIgnitionTime    = 0.10f;  //  0.4
		mbin.JetpackMinIgnitionTime = 0.01f;  //  0.2
		mbin.JetpackTankTimes[0] = 12.0f;  // 4
		mbin.JetpackTankTimes[1] = 18.0f;  // 6
		mbin.JetpackTankTimes[2] = 24.0f;  // 8
		mbin.JetpackFillRate         = 1.5f;  // 0.5
		mbin.JetpackFillRateHardMode = 1.0f;  // 0.2
		mbin.JetpackUpForceDeadPlanetExtra       = 15.0f;  // 10
		mbin.JetpackForceDeadPlanetExtra         = 70.0f;  // 45
		mbin.JetpackIgnitionForceDeadPlanetExtra =  0.0f;  // 45
	}
}

//=============================================================================
