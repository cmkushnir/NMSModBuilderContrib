//=============================================================================

public class Camera : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		Try(() => GcCameraGlobals());
		Try(() => GcGraphicsGlobals());
	}

	//...........................................................

	protected void GcCameraGlobals()
	{
		var mbin = ExtractMbin<GcCameraGlobals>(
			"GCCAMERAGLOBALS.GLOBAL.MBIN"
		);
		
		mbin.BeaconTransition.AerialViewMode = AerialViewModeEnum.FaceDown;
		mbin.BeaconTransition.Time      = 0;
		mbin.BeaconTransition.TimeBack  = 0;
		mbin.BeaconTransition.StartTime = 0;
		mbin.BeaconTransition.PauseTime = 0;
		mbin.BeaconTransition.Distance  = 0;
		mbin.BeaconTransition.Stages    = 0;	
		
		mbin.WaypointTransition.Time      = 0;
		mbin.WaypointTransition.TimeBack  = 0;
		mbin.WaypointTransition.StartTime = 0;
		mbin.WaypointTransition.PauseTime = 0;
		mbin.WaypointTransition.Distance  = 0;
		mbin.WaypointTransition.Stages    = 0;	

		// deactivate all camera shake effects
		foreach( var data in mbin.CameraShakeTable ) {
			data.CapturedData  .Active = false;
			data.MechanicalData.Active = false;
		}
	}

	//...........................................................

	protected void GcGraphicsGlobals()
	{
		var mbin = ExtractMbin<GcGraphicsGlobals>(
			"GCGRAPHICSGLOBALS.GLOBAL.MBIN"
		);
		mbin.SunLightIntensity = 4.9f;  // 3
		mbin.LensDirt     = 0;  // 0.3
		mbin.LensDirtCave = 0;  // 0.4
		mbin.TargetTextureMemUsageMB = 5120;  // 1280
	}

}

//=============================================================================
