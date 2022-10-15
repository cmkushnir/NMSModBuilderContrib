//=============================================================================
// Reduce visual clutter due to messenger beacons left by other users.
//=============================================================================

public class Messenger : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		Try(() => GcScannerIcons());
		Try(() => TkAttachmentData());
	}
	
	//...........................................................

	protected void GcScannerIcons()
	{
		var mbin = ExtractMbin<GcScannerIcons>(
			"METADATA/UI/HUD/SCANNERICONS.MBIN"
		);
	//	mbin.MessageBeacon.Main .Filename = "";
		mbin.MessageBeacon.Small.Filename = "";
	//	mbin.MessageBeaconSmall.Main .Filename = "";
		mbin.MessageBeaconSmall.Small.Filename = "";
	}
	
	//...........................................................
	
	protected void TkAttachmentData()
	{
		var mbin = ExtractMbin<TkAttachmentData>(
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PROPS/MESSENGER/ENTITIES/MESSENGER.ENTITY.MBIN"
		);

		var scannable  = mbin.Components.FindFirst<GcScannableComponentData>();
		if( scannable == null ) {
			scannable  = new GcScannableComponentData{
				ScanName      = "SCAN_WAYPOINT",
				ScanTime      = 30,
				UseModelNode  = true,
				Icon          = new(){ ScanIconType = ScanIconTypeEnum.SignalBooster },
				ScannableType = ScannableTypeEnum.Binoculars,
				CompassRangeMultiplier = 0,  // 0 - 1, gives both compass icon and range
				DisableIfInBase        = true,
				DisableIfBuildingPart  = true,
			};
			mbin.Components.Add(scannable);
		}		

		scannable.ScanRange                  = 300;  // max is based on a lod? range ~2,100u
		scannable.AlwaysShowRange            = 300;
		scannable.MinDisplayDistanceOverride = 200;  // hide when get this close

		scannable.CanTagIcon        = true;
		scannable.ClearTagOnArrival = true;
	}
}

//=============================================================================
