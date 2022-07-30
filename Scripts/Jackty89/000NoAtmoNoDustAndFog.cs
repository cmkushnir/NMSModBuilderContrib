//=============================================================================
// Author: Jackty89
//=============================================================================

public class NoAtmoNoDustAndFog : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		NoFog();
		NoDust();
	}
	
	protected void NoFog()
	{
		var mbin = ExtractMbin<GcSkyGlobals>("GCSKYGLOBALS.GLOBALS.MBIN");
		mbin.NoAtmosphereFogStrength = 0;
		mbin.NoAtmosphereFogMax      = 0;
	}
	
	protected void NoDust()
	{
		var mbin = ExtractMbin<GcWeatherProperties>("METADATA/SIMULATION/SOLARSYSTEM/WEATHER/CLEARCOLD.MBIN");
		mbin.HeavyAir.Clear();
       
		mbin = ExtractMbin<GcWeatherProperties>("METADATA/SIMULATION/SOLARSYSTEM/WEATHER/CLEARWEATHER.MBIN");
		mbin.HeavyAir.Clear();
	}
	

	//...........................................................
}

//=============================================================================
