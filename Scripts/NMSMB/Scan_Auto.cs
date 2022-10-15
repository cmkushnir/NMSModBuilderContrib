//=============================================================================
// Automatically flag nearby locations when get within range, ~2,100u.
// How it works (I assume):
// When the game renders an entity it checks if it has a GcScannableComponentData
// entry, and if so checks the to see if markers should be added to the compass
// and|or entity based on the distance to the entity.  If a marker should be
// added to the entity then is uses the specified ScanIconTypeEnum value for
// the marker and the ScanName as the displayed text.
// So, we need to find appropriate entities that are rendered when outside
// on planet.  Unfortunately we are limited in the markers we can display,
// as they must come from ScanIconTypeEnum.
// We include methods to mark most entries in BuildingClassEnum as well as some others.
//=============================================================================

public class Scan_Auto : cmk.NMS.Script.ModClass
{
	public bool EnableShelter           = false;
	public bool EnableAbandoned         = false;
	public bool EnableRadioTower        = false;
	public bool EnableFactory           = false;
	public bool EnableObservatory       = false;
	public bool EnableShop              = false;
	public bool EnableOutpost           = false;
	public bool EnableSettlement        = false;
	public bool EnableBaseSite          = false;
	public bool EnableDroneHive         = false;
	public bool EnableDroneHiveDisabled = false;
	public bool EnableMonolith          = false;
	public bool EnablePortal            = false;
	public bool EnableDistress          = false;
	public bool EnableGrave             = false;
	public bool EnableBones             = false;
	public bool EnableTreasureRuin      = false;
	public bool MessageModule           = false;

	//...........................................................

	protected override void Execute()
	{
		if( EnableShelter )           Try(() => TkAttachmentData_Shelter());
		if( EnableAbandoned )         Try(() => TkAttachmentData_Abandoned());
		if( EnableRadioTower )        Try(() => TkAttachmentData_RadioTower());
		if( EnableFactory )           Try(() => TkAttachmentData_Factory());
		if( EnableObservatory )       Try(() => TkAttachmentData_Observatory());
		if( EnableShop )              Try(() => TkAttachmentData_Shop());
		if( EnableOutpost )           Try(() => TkAttachmentData_Outpost());
		if( EnableSettlement )        Try(() => TkAttachmentData_Settlement());
		if( EnableBaseSite )          Try(() => TkAttachmentData_BaseSite());
		if( EnableDroneHive )         Try(() => TkAttachmentData_DroneHive());
		if( EnableDroneHiveDisabled ) Try(() => TkAttachmentData_DroneHiveDisabled());
		if( EnableMonolith )          Try(() => TkAttachmentData_Monolith());
		if( EnablePortal )            Try(() => TkAttachmentData_Portal());
		if( EnableDistress )          Try(() => TkAttachmentData_Distress());
		if( EnableGrave )             Try(() => TkAttachmentData_Grave());
		if( EnableBones )             Try(() => TkAttachmentData_Bones());
		if( EnableTreasureRuin )      Try(() => TkAttachmentData_TreasureRuin());
		if( MessageModule )           Try(() => TkAttachmentData_MessageModule());
	}

	//...........................................................
	
	// todo see: MODELS/COMMON/PLAYER/PLACEMARKER/ENTITIES/PLACEMARKER.ENTITY.MBIN
	// for possible settings to view from any distance.
	// ScanRange = 0
	// ScanTime = 5
	// CompassRangeMultiplier = 1E+09
	// AlwaysShowRange = 0
	// MinDisplayDistanceOverride = -1
	protected GcScannableComponentData SetGcScannableComponentData(
		TkAttachmentData MBIN,
		string           NAME,      // language ID or hard-coded string
		ScanIconTypeEnum ICON_TYPE
	){
		var scannable  = MBIN.Components.FindFirst<GcScannableComponentData>();
		if( scannable == null ) {
			scannable  = new GcScannableComponentData{
				ScanName      = NAME,
				ScanTime      = 30,
				UseModelNode  = true,
				Icon          = new(){ ScanIconType = ICON_TYPE },
				ScannableType = ScannableTypeEnum.Marker,
				CompassRangeMultiplier = 1,  // 0 - 1, gives both compass icon and range
				DisableIfInBase        = true,
				DisableIfBuildingPart  = true,
			};
			MBIN.Components.Add(scannable);
		}		

		scannable.ScanRange                  = 10000;  // max is based on a lod? range ~2,100u
		scannable.AlwaysShowRange            = 10000;
		scannable.MinDisplayDistanceOverride =   240;  // hide when get this close

		scannable.CanTagIcon        = true;
		scannable.ClearTagOnArrival = true;
		
		return scannable;
	}
	
	//...........................................................
	// For scene and lsystem mbin's for all buildings:
	// METADATA/SIMULATION/ENVIRONMENT/PLANETBUILDINGTABLE.MBIN
	//...........................................................
	
	// BUILDING_SHELTER_L "Shelter"
	protected void TkAttachmentData_Shelter()
	{
		var mbin = ExtractMbin<TkAttachmentData>(
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/SHACK/SHACK/ENTITIES/SHACKBUILDING.ENTITY.MBIN"
		);
		var scannable = SetGcScannableComponentData(mbin, "BUILDING_SHELTER_L", ScanIconTypeEnum.FreighterDoor);
	}

	//...........................................................

	// BUILDING_ABANDONED_L "Abandoned Building"
	protected void TkAttachmentData_Abandoned()
	{
		var mbin = ExtractMbin<TkAttachmentData>(
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/COMMONPARTS/ABANDONEDTERMINAL/ENTITIES/ABANDONEDTERMINAL.ENTITY.MBIN"
		);
		var scannable = SetGcScannableComponentData(mbin, "BUILDING_ABANDONED_L", ScanIconTypeEnum.HazardEgg);
	}
	
	//...........................................................

	// BUILDING_RADIOTOWER_L "Transmission Tower"
	protected void TkAttachmentData_RadioTower()
	{
		var paths = new [] {
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/RADIOTOWER/SCIENTIFICRADIOTOWERPARTS/SCIENTIFICTERMINAL/ENTITIES/SCIENTIFICRADIOTOWER.ENTITY.MBIN"
		};
		foreach( var path in paths ) {
			var mbin = ExtractMbin<TkAttachmentData>(path);
			var scannable = SetGcScannableComponentData(mbin, "BUILDING_RADIOTOWER_L", ScanIconTypeEnum.SignalBooster);
		}
	}
	
	//...........................................................

	// BUILDING_FACTORY_L "Manufacturing Facility"
	protected void TkAttachmentData_Factory()
	{
		var paths = new [] {
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/FACTORY/FACTORYSCIENTIFIC/ENTITIES/SCIENTIFICFACTORY.ENTITY.MBIN",
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/FACTORY/FACTORYTRADER/ENTITIES/FACTORYTRADER.ENTITY.MBIN",
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/FACTORY/FACTORYWARRIOR/ENTITIES/FACTORY.ENTITY.MBIN"
		};
		foreach( var path in paths ) {
			var mbin = ExtractMbin<TkAttachmentData>(path);
			var scannable = SetGcScannableComponentData(mbin, "BUILDING_FACTORY_L", ScanIconTypeEnum.FreighterDoor);
		}
	}
	
	//...........................................................

	// BUILDING_OBSERVATORY_L "Observatory"
	protected void TkAttachmentData_Observatory()
	{
		var paths = new [] {
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/OBSERVATORY/OBSERVATORYSCIENTIFIC/ENTITIES/OBSERVATORYINTERACTION.ENTITY.MBIN",
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/OBSERVATORY/OBSERVATORYTRADER/ENTITIES/OBSERVATORYINTERACTION.ENTITY.MBIN",
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/OBSERVATORY/OBSERVATORYWARRIOR/ENTITIES/OBSERVATORYINTERACTION.ENTITY.MBIN"
		};
		foreach( var path in paths ) {
			var mbin = ExtractMbin<TkAttachmentData>(path);
			var scannable = SetGcScannableComponentData(mbin, "BUILDING_OBSERVATORY_L", ScanIconTypeEnum.FreighterDoor);
		}
	}

	//...........................................................

	// BUILDING_SHOP_L "Minor Settlement"
	protected void TkAttachmentData_Shop()
	{		
		var mbin = ExtractMbin<TkAttachmentData>(
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/TRADERPARTS/LANDINGPAD/ENTITIES/LANDINGPAD.ENTITY.MBIN" // landing pad w/ walkway
		);
		var scannable = SetGcScannableComponentData(mbin, "BUILDING_SHOP_L", ScanIconTypeEnum.FreighterDoor);
	}

	//...........................................................

	// BUILDING_OUTPOST_L "Trading Post"
	protected void TkAttachmentData_Outpost()
	{
		var mbin = ExtractMbin<TkAttachmentData>(
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/TRADINGPOST/TRADINGPOST/ENTITIES/OUTPOST.ENTITY.MBIN"
		);
		var scannable = SetGcScannableComponentData(mbin, "BUILDING_OUTPOST_L", ScanIconTypeEnum.LandedPilot);
	}
	
	//...........................................................

	protected void TkAttachmentData_Settlement()
	{
		var mbin = ExtractMbin<TkAttachmentData>(
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/SETTLEMENT/TOWER_STONE_PLACEMENT/ENTITIES/PLACEMENTDATA.ENTITY.MBIN"
		);	
		var scannable = SetGcScannableComponentData(mbin, "UI_SETTLEMENT_LOCATED_OSD", ScanIconTypeEnum.FreighterTerminal);
	}

	//...........................................................

	// UI_RECOVER_BASE_MARKER "Suitable Base Site"
	protected void TkAttachmentData_BaseSite()
	{
		var mbin = ExtractMbin<TkAttachmentData>(
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/BUILDABLEPARTS/BASECOMPUTER/ENTITIES/BASECOMPUTER.ENTITY.MBIN"
		);
		var scannable = SetGcScannableComponentData(mbin, "UI_RECOVER_BASE_MARKER", ScanIconTypeEnum.FreighterTerminal);
	}

	//...........................................................

	// UI_MP_HIVE_LABEL "Sentinel Pillar"
	protected void TkAttachmentData_DroneHive()
	{
		var mbin = ExtractMbin<TkAttachmentData>(
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/SENTINELHIVE/SENTINELHIVEDESTRUCTIBLE/ENTITIES/SENTINELHIVEDESTRUCTIBLE.ENTITY.MBIN"
		);
		var scannable = SetGcScannableComponentData(mbin, "UI_MP_HIVE_LABEL", ScanIconTypeEnum.Drone);
	}

	//...........................................................

	// UI_MP_HIVE_LABEL "Sentinel Pillar"
	protected void TkAttachmentData_DroneHiveDisabled()
	{
		var mbin = ExtractMbin<TkAttachmentData>(
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/SENTINELHIVE/SENTINELHIVEDESTRUCTIBLE_DESTROYED/ENTITIES/DEBRIS.ENTITY.MBIN"
		);
		var scannable = SetGcScannableComponentData(mbin, "UI_MP_HIVE_LABEL", ScanIconTypeEnum.FriendlyDrone);
	}

	//...........................................................

	// BUILDING_MONOLITH_L "Monolith"
	protected void TkAttachmentData_Monolith()
	{
		var mbin = ExtractMbin<TkAttachmentData>(
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/RUINPARTS/CENTERPIECE/INTERACTIONPLATFORM/ENTITIES/INTERACTIONPLATFORM.ENTITY.MBIN"
		);
		var scannable = SetGcScannableComponentData(mbin, "BUILDING_MONOLITH_L", ScanIconTypeEnum.Artifact);
	}

	//...........................................................

	// BUILDING_PORTAL_L "Portal"
	protected void TkAttachmentData_Portal()
	{
		var mbin = ExtractMbin<TkAttachmentData>(
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PORTAL/PORTAL/ENTITIES/PORTAL.ENTITY.MBIN"
		);
		var scannable = SetGcScannableComponentData(mbin, "BUILDING_PORTAL_L", ScanIconTypeEnum.Artifact);
	}

	//...........................................................

	// BUILDING_DISTRESSSIGNAL_L: "Crashed Ship"
	protected void TkAttachmentData_Distress()
	{
		var mbin = ExtractMbin<TkAttachmentData>(
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/DISTRESSSIGNAL/PARTS/BLACKBOX/ENTITIES/BLACKBOX.ENTITY.MBIN"
		);
		var scannable = SetGcScannableComponentData(mbin, "BUILDING_DISTRESSSIGNAL_L", ScanIconTypeEnum.HazardPlant);
	}

	//...........................................................

	// BUILDING_GRAVEINCAVE "Unknown Grave"
	protected void TkAttachmentData_Grave()
	{
		var mbin = ExtractMbin<TkAttachmentData>(
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/GRAVEINCAVE/GRAVEINCAVE/ENTITIES/GRAVEINCAVE.ENTITY.MBIN"
		);
		var scannable = SetGcScannableComponentData(mbin, "BUILDING_GRAVEINCAVE", ScanIconTypeEnum.Grave);
	}

	//...........................................................

	// UI_UNDERGROUND_BONES_NAME_L "Natural Burial Site"
	protected void TkAttachmentData_Bones()
	{
		var mbin = ExtractMbin<TkAttachmentData>(
			"MODELS/PLANETS/BIOMES/COMMON/RARERESOURCE/GROUND/BONEPILE/ENTITIES/BONEPILE.ENTITY.MBIN"
		);
		var scannable = SetGcScannableComponentData(mbin, "UI_UNDERGROUND_BONES_NAME_L", ScanIconTypeEnum.BuriedRare);
	}

	//...........................................................

	// PLANT_FOOD_38 "Buried Treasure"
	protected void TkAttachmentData_TreasureRuin()
	{
		var mbin = ExtractMbin<TkAttachmentData>(
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/RUINS/UNDERGROUNDRUINS/ENTITIES/TRIGGERVOLUME.ENTITY.MBIN"
		);
		var scannable = SetGcScannableComponentData(mbin, "PLANT_FOOD_38", ScanIconTypeEnum.ArtifactCrate);
	}

	//...........................................................

	// BLD_MESSAGEMODULE_NAME_L "Message Module"
	protected void TkAttachmentData_MessageModule()
	{
		var mbin = ExtractMbin<TkAttachmentData>(
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/BUILDABLEPARTS/TECH/MESSAGEMODULE/ENTITIES/MESSAGEMODULE.ENTITY.MBIN"
		);
		var scannable = SetGcScannableComponentData(mbin, "BLD_MESSAGEMODULE_NAME_L", ScanIconTypeEnum.Grave);
	}
}

//=============================================================================
