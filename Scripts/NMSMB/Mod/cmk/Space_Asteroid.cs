//=============================================================================
// Adjust asteroid spacing and density.
//=============================================================================

public class Space_Asteroid : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcSolarGenerationGlobals>(
			"GCSOLARGENERATIONGLOBALS.GLOBAL.MBIN"
		);

		var asteroid_settings = mbin.AsteroidSettings[0];
		asteroid_settings.CommonAsteroidData.Spacing *= 2;  //  645
		asteroid_settings.RingAsteroidData  .Spacing *= 2;  //  200
		asteroid_settings.LargeAsteroidData .Spacing *= 2;  // 9000
		asteroid_settings.RareAsteroidData  .Spacing *= 2;  //  850

		var GcSolarGenerationGlobals_t = typeof(GcSolarGenerationGlobals);
		
		foreach( var field in GcSolarGenerationGlobals_t.GetFields() ) {
			if( field.Name.Contains("AsteroidNoiseRange") &&
			    field.GetValue(mbin) is nms.Vector2f vector
			) {
				vector.x /= 2;
				vector.y /= 2;
			}
		}
	}
}

//=============================================================================
