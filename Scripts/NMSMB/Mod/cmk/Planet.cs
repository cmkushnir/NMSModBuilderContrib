//=============================================================================
// Adjust planet cloud and fog settings.
//=============================================================================

public class Planet : cmk.NMS.Script.ModClass
{
	public static float FogStrengthMult = 0.1f;
	
	//...........................................................

	protected override void Execute()
	{
		Try(() => GcEnvironmentGlobals());
		//Try(() => TkVoxelGeneratorSettingsArray());
		Try(() => GcSkyGlobals());
		Try(() => GcWeatherTable());
	}

	//...........................................................
	
	protected void GcEnvironmentGlobals()
	{
		var mbin = ExtractMbin<GcEnvironmentGlobals>(
			"GCENVIRONMENTGLOBALS.GLOBAL.MBIN"
		);
		mbin.CloudProperties.AnimationScale = 16;  // 50, slower clouds
	}

	//...........................................................

	protected void TkVoxelGeneratorSettingsArray()
	{
		var mbin = ExtractMbin<TkVoxelGeneratorSettingsArray>(
			"METADATA/SIMULATION/SOLARSYSTEM/VOXELGENERATORSETTINGS.MBIN"
		);						
		foreach( var setting in mbin.TerrainSettings ) {
			setting.Min.SeaLevel += 100;
			setting.Max.SeaLevel += 100;
		}
	}
	
	//...........................................................

	protected void GcSkyGlobals()
	{
		var mbin = ExtractMbin<GcSkyGlobals>(
			"GCSKYGLOBALS.GLOBALS.MBIN"
		);						
		// defaults if a weather has UseWeatherFog = false
		GcFogProperties(mbin.PlanetProperties.PlanetFog);
		GcFogProperties(mbin.PlanetProperties.PlanetFlightFog);
		GcFogProperties(mbin.PlanetProperties.PlanetExtremeFog);
		GcFogProperties(mbin.PlanetProperties.PlanetStormFog);
		GcFogProperties(mbin.PlanetPrimeProperties.PlanetFog);
		GcFogProperties(mbin.PlanetPrimeProperties.PlanetFlightFog);
		GcFogProperties(mbin.PlanetPrimeProperties.PlanetExtremeFog);
		GcFogProperties(mbin.PlanetPrimeProperties.PlanetStormFog);
	}
	
	//...........................................................

	protected void GcWeatherTable()
	{
		var weathers = ExtractMbin<GcWeatherTable>(
			"METADATA/SIMULATION/SOLARSYSTEM/WEATHER/WEATHERLIST.MBIN"
		);
		foreach( var weather in weathers.Table ) {
			GcWeatherProperties(weather);
		}		
	}

	//...........................................................

	protected void GcWeatherProperties( string PATH )
	{
		var weather = ExtractMbin<GcWeatherProperties>(PATH);
		//if( weather.UseWeatherFog ) {  // modifying Storms anyways
			GcFogProperties(weather.Fog);
			GcFogProperties(weather.FlightFog);
			GcFogProperties(weather.StormFog);
			GcFogProperties(weather.ExtremeFog);
		//}
		if( weather.Storms != null )
		foreach( var storm in weather.Storms ) {
			GcFogProperties(storm.Fog);
		}
	}

	//...........................................................

	protected void GcFogProperties( GcFogProperties FOG )
	{
		FOG.FogStrength *= FogStrengthMult;
		FOG.FogMax      *= FogStrengthMult;
		FOG.FogHeight   *= FogStrengthMult;
	}
}

//=============================================================================
