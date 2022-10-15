//=============================================================================
// Adjust general scan related settings.
//=============================================================================

public class Scan : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		Try(() => GcUserSettingsData());
		Try(() => GcGameplayGlobals());
		Try(() => GcBuildingGlobals());
		Try(() => GcScanEventTable());
		Try(() => GcMissionTable());
	}
	
	//...........................................................

	protected void GcUserSettingsData()
	{
		// this may not be used, the mbin may just be a template to gen
		// Binaries/SETTINGS/GCUSERSETTINGSDATA.MXML if it's missing.
		var mbin = ExtractMbin<GcUserSettingsData>(
			"METADATA/ENGINESETTINGS/GCUSERSETTINGSDATA.MBIN"
		);
		// when looking at an unknown thing auto-discover w/o having to click-and-hold
		mbin.HoldToScanDiscoveries = false;  // true
	}

	//...........................................................

	protected void GcGameplayGlobals()
	{
		var mbin = ExtractMbin<GcGameplayGlobals>(
			"GCGAMEPLAYGLOBALS.GLOBAL.MBIN"
		);
		
		// make it easier to tell diff between scanned|unscanned rocks and veg.
		mbin.BinocularSelectedColour        = new(0, 1, 0, 1);  // bright green
		mbin.BinocularSelectedUnknownColour = new(1, 0, 0, 1);  // bright red
			
		mbin.MissionSurveyMaxGuidanceDistance =   100;  // 1500
		mbin.TerrainResourceScanRange         = 20000;  // 1200
		mbin.SurveyMaxDistance                =   100;  //  400

		// finds distress when use ship scan.
		// works but basically only finds 1 per planet
		// and disables ship scan of planets and stuff.
		//mbin.UseDebugScan             = true;   // false
		//mbin.DebugScanForceBuilding   = true;   // false
		//mbin.DebugScanForceBuildingType.BuildingClass = BuildingClassEnum.DistressSignal;  // None
	}

	//...........................................................

	protected void GcBuildingGlobals()
	{
		var mbin = ExtractMbin<GcBuildingGlobals>(
			"GCBUILDINGGLOBALS.GLOBAL.MBIN"
		);
		mbin.MaxLineLength        = 0;      //   30, vert leader line from object to icon ?
		mbin.UnknownBuildingRange = 20000;  //  600
		mbin.MaxIconRange         = 20000;  // 1200
		mbin.MinShipScanBuildings = 1;      //    0
		mbin.MaxShipScanBuildings = 3;      //    2
	}

	//...........................................................

	protected void GcScanEventTable()
	{
		var GcScanEventTable_class = Game.Mbinc.FindClass("GcScanEventTable");
		foreach( var path in GcScanEventTable_class.PakItems ) {
			var mbin = ExtractMbin<GcScanEventTable>(path);
			foreach( var data in mbin.Events ) {
				data.SurveyDistance = 0;
				if( data.BuildingClass.BuildingClass == BuildingClassEnum.Portal ) {
					continue;
				}
				switch( data.BuildingLocation ) {
					case BuildingLocationEnum.Random:
						data.BuildingLocation = BuildingLocationEnum.Nearest;
						data.ForceWideRandom  = false;
						break;
					case BuildingLocationEnum.AllNearest:
					case BuildingLocationEnum.Nearest:
						data.ForceWideRandom = false;  // does this do anything ?
						break;
					case BuildingLocationEnum.PlanetSearch:
					case BuildingLocationEnum.PlayerSettlement:
						break;
					case BuildingLocationEnum.RandomOnFarPlanet:
					case BuildingLocationEnum.RandomOnNearPlanet:
						data.ForceWideRandom = true;
						break;
				}
			}
		}
	}

	//...........................................................

	protected void GcMissionTable()
	{
		var GcMissionTable_class = Game.Mbinc.FindClass("GcMissionTable");
		foreach( var path in GcMissionTable_class.PakItems ) {
			var mbin = ExtractMbin<GcMissionTable>(path);
			foreach( var sequence in mbin.Missions ) {
				foreach( var scan in sequence.ScanEvents ) {
					scan.SurveyDistance = 0;
				}
			}
		}
	}
}

//=============================================================================
