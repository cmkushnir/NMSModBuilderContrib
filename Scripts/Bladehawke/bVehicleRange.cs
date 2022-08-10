//=============================================================================

public class bVehicleRange : cmk.NMS.Script.ModClass
{
    public static int Multiplier = 20;
    
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcVehicleGlobals>(
			"GCVEHICLEGLOBALS.GLOBAL.MBIN"
		);
		
		mbin.VehicleDeactivateRange *= Multiplier;
	}

	//...........................................................
}

//=============================================================================
