﻿//=============================================================================
// Teleport range, auto-aim all weapons, hover, ...
// Adjust distribution of ship in system, increase chance of Royals.
//=============================================================================

public class Ship : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
	//	Try(() => Squid());
		Try(() => TkAttachmentData());
		Try(() => GcGameplayGlobals());
		Try(() => GcTechnologyTable());
		Try(() => GcAISpaceshipGlobals());
		Try(() => GcRealityManagerData());
		Try(() => GcSpaceshipGlobals());
		// control spawns by changing distribution
		Try(() => GcSolarGenerationGlobals());
		// control spawns by disabling ship scenes - idea: Apex Fatality | Lenni
	//	Try(() => GcAISpaceshipManagerData());
	//	Try(() => GcExperienceSpawnTable());
	}
		
	//...........................................................

	// quick poc to create a squid ship using only the cockpit.
	// proper mod would lower cockpit to align w/ jet trails and landing gear,
	// and add as new exotic instead of replacing all exotic.
	protected void Squid()
	{
		var desc = ExtractMbin<TkModelDescriptorList>(
			"MODELS/COMMON/SPACECRAFT/S-CLASS/S-CLASS_PROC.DESCRIPTOR.MBIN"
		);
		var squid = CloneMbin(desc.List[0].Descriptors.Find(DESC => DESC.Id == "_SCLASSSHIP_SQU"));
		squid.ReferencePaths.Clear();
		squid.ReferencePaths.Add("MODELS/COMMON/SPACECRAFT/S-CLASS/SQUIDPARTS/COCKPIT/COCKPIT_A.SCENE.MBIN");
		squid.ReferencePaths.Add("MODELS/COMMON/SPACECRAFT/SHARED/WEAPONS/SHIPGUN.SCENE.MBIN");
		desc.List[0].Descriptors.Clear();
		desc.List[0].Descriptors.Add(squid);

		var scene = ExtractMbin<TkSceneNodeData>(
			"MODELS/COMMON/SPACECRAFT/S-CLASS/S-CLASS_PROC.SCENE.MBIN"
		);
		var node = scene.Children[4].Children[1].Children.Find(NODE => NODE.Name == "Cockpit_A");
		scene.Children[4].Children[1].Children.Clear();
		scene.Children[4].Children[1].Children.Add(node);
	}
	
	//...........................................................

	protected void TkAttachmentData()
	{
		var mbin = ExtractMbin<TkAttachmentData>(
			"MODELS/SPACE/BLACKHOLE/BLACKHOLE/ENTITIES/BLACKHOLE.ENTITY.MBIN"
		);
		var component = mbin.Components.FindFirst<TkAudioComponentData>();
		component.MaxDistance = 1000;
	}
	
	//...........................................................

	protected void GcGameplayGlobals()
	{
		var mbin = ExtractMbin<GcGameplayGlobals>(
			"GCGAMEPLAYGLOBALS.GLOBAL.MBIN"
		);
		// interact with ship from farther away
		mbin.ShipInteractRadius = 2000;
	}

	//...........................................................

	protected void GcTechnologyTable()
	{
		var mbin = ExtractMbin<GcTechnologyTable>(
			"METADATA/REALITY/TABLES/NMS_REALITY_GCTECHNOLOGYTABLE.MBIN"
		);
		// teleport material between exosuit and ship from farther away
		var tech    = mbin.Table.Find(TECH => TECH.ID == "SHIP_TELEPORT");
		
		// todo: 4.00 no more Ship_Teleport, bonus now Ship_CargoShield ???
		var bonus   = tech.StatBonuses.Find(BONUS => BONUS.Stat.StatsType == StatsTypeEnum.Ship_Teleport);
		bonus.Bonus = 2000;
	}

	//...........................................................

	protected void GcAISpaceshipGlobals()
	{
		var mbin = ExtractMbin<GcAISpaceshipGlobals>(
			"GCAISPACESHIPGLOBALS.GLOBAL.MBIN"
		);

		mbin.VisibleDistance                = 20000;  // 3500
		mbin.MinimumCircleTimeBeforeLanding = 1;      // 5

		var ranges = mbin.PlayerSquadronConfig.PilotRankTraitRanges;
		for( var i = 0; i < ranges.Length; ++i ) {
			ranges[i].x = 0.10f + (i * 0.20f);  // min: 0.10, 0.30, 0.50, 0.70
			ranges[i].y = 0.55f + (i * 0.15f);  // max: 0.55, 0.70, 0.85, 1.00
		}
	}

	//...........................................................

	protected void GcRealityManagerData()
	{
		var mbin = ExtractMbin<GcRealityManagerData>(
			"METADATA/REALITY/DEFAULTREALITY.MBIN"
		);
		// all ship weapons have auto-aim
		foreach( var weapon in mbin.ShipWeapons ) {
			weapon.AutoAimAngle      =  5;
			weapon.AutoAimExtraAngle = 15;
		}
	}

	//...........................................................

	protected void GcSpaceshipGlobals()
	{
		var mbin = ExtractMbin<GcSpaceshipGlobals>(
			"GCSPACESHIPGLOBALS.GLOBAL.MBIN"
		);
		
		mbin.DoPreCollision        = false;  // true
		mbin.LandingCheckBuildings = false;  // true		

		mbin.LandSlopeMax = 50;
		mbin.LandingAreaRadius = 1;
		
		mbin.GroundHeightSmoothTime = 100000;  // 0, fly underwater

		mbin.NearGroundPitchCorrectMinHeight = 1;  // 23
		mbin.NearGroundPitchCorrectRange     = 1;  // 15
		mbin.NearGroundPitchCorrectMinHeightRemote = 3;  // 30
		mbin.NearGroundPitchCorrectRangeRemote     = 3;  // 30
		
		mbin.PitchCorrectSoftDownAngle = 360;  // 25
		mbin.PitchCorrectMaxDownAngle  = 360;  // 50
		mbin.PitchCorrectMaxDownAnglePostCollision  = 360;  //  10
		mbin.PitchCorrectSoftDownAnglePostCollision = 360;  // -10
		mbin.PitchCorrectMaxDownAngleWater          = 360;  //  20
	
		mbin.GroundHeightSoft      =  7;  // 20
		mbin.GroundHeightSoftForce = 14;  // 35

		mbin.LandHeightThreshold     = 60;  // 100
		mbin.LandingPushNoseUpFactor =  0;  // 0.15

		// ship hover			
		var min_speed = 0.0001f;

		mbin.HoverMinSpeed    = min_speed;  //  1
		mbin.HoverSpeedFactor = 0.0001f;    // 20

		mbin.Control.SpaceEngine .MinSpeed = min_speed;
		mbin.Control.PlanetEngine.MinSpeed = min_speed;
		mbin.Control.CombatEngine.MinSpeed = min_speed;

		mbin.ControlLight.SpaceEngine .MinSpeed = min_speed;
		mbin.ControlLight.PlanetEngine.MinSpeed = min_speed;
		mbin.ControlLight.CombatEngine.MinSpeed = min_speed;

		mbin.ControlHeavy.SpaceEngine .MinSpeed = min_speed;
		mbin.ControlHeavy.PlanetEngine.MinSpeed = min_speed;
		mbin.ControlHeavy.CombatEngine.MinSpeed = min_speed;

		mbin.MiniWarpSpeed                = 100000;  // 30000
		mbin.MiniWarpLinesNum             = 0;      // 4
		mbin.MiniWarpFlashIntensity       = 0.1f ;  // 0.9
		mbin.MiniWarpHUDArrowAttractAngle = 2;      // 10

		mbin.FreighterBattleIgnoreFriendlyFireDistance *= 10;  // 10,000
		
		mbin.PulseDriveStationApproachSlowdownRange    /= 10;
		mbin.PulseDriveStationApproachSlowdownRangeMin /= 10;
	}

	//...........................................................

	// adjust distribution of ship types in a system
	protected void GcSolarGenerationGlobals()
	{
		var mbin = ExtractMbin<GcSolarGenerationGlobals>(
			"GCSOLARGENERATIONGLOBALS.GLOBAL.MBIN"
		);
		
		// # of ship seeds generated per system - 1
		//mbin.CivilianTraderSpaceshipsCacheCount = 39;  // 20
		
		IncreaseChanceSpecial(mbin);
		//ManySpecial(mbin);  // test
	}

	//...........................................................
	
	protected void IncreaseChanceSpecial( GcSolarGenerationGlobals MBIN )
	{		
		// used to be array indexed on mbin_gc.GcAlienRace.AlienRaceEnum, now a List<>.
		// weightings is tied to enum, but enum is specified as an attrib.
		
		var weightings = MBIN.SpaceshipWeightings[(int)AlienRaceEnum.Traders].CivilianClassWeightings;
		weightings[(int)ShipClassEnum.Shuttle] = 61;
		weightings[(int)ShipClassEnum.Royal]   = 20;
		weightings[(int)ShipClassEnum.Sail]    = 20;

		weightings = MBIN.SpaceshipWeightings[(int)AlienRaceEnum.Warriors].CivilianClassWeightings;
		weightings[(int)ShipClassEnum.Shuttle] = 61;
		weightings[(int)ShipClassEnum.Royal]   = 20;
		weightings[(int)ShipClassEnum.Sail]    = 20;

		weightings = MBIN.SpaceshipWeightings[(int)AlienRaceEnum.Explorers].CivilianClassWeightings;
		weightings[(int)ShipClassEnum.Shuttle] = 61;
		weightings[(int)ShipClassEnum.Royal]   = 20;
		weightings[(int)ShipClassEnum.Sail]    = 20;
	}

	//...........................................................
	
	// 20/21 ships are royal|alien|sail in each system
	protected void ManySpecial( GcSolarGenerationGlobals MBIN )
	{		
		// need at least 2 values to be non-0 in order to get large selection of bigger #.
		// e.g. Royal = 100, Alien = 1, all others = 0, gives large selection of Royal in system.		
		foreach( var spaceship in MBIN.SpaceshipWeightings ) {
			var civilian = spaceship.CivilianClassWeightings;
			for( var i = 0; i < civilian.Length; ++i ) civilian[i] = 0;
			civilian[(int)ShipClassEnum.Shuttle] = 1;
			// leave shuttle = 1, and set 1 other to 100
			//civilian[(int)ShipClassEnum.Royal] = 100;
			//civilian[(int)ShipClassEnum.Alien] = 100;
			civilian[(int)ShipClassEnum.Sail]  = 100;
		}
	}

	//...........................................................

	protected void GcAISpaceshipManagerData()
	{
		var mbin = ExtractMbin<GcAISpaceshipManagerData>(
			"METADATA/SIMULATION/SPACE/AISPACESHIPMANAGER.MBIN"
		);
		foreach( var data in mbin.SystemSpaceships[(int)AIFactionEnum.Civilian].Spaceships ) {
			switch( data.Class.ShipClass ) {
				case ShipClassEnum.Dropship:
				case ShipClassEnum.Shuttle:
				case ShipClassEnum.Fighter:
				case ShipClassEnum.Scientific:
				case ShipClassEnum.Alien:
				case ShipClassEnum.Sail:
				case ShipClassEnum.Freighter:
					data.Filename = "";  // don't want any of these to be able to spawn
					break;
				case ShipClassEnum.Royal:				
					break;
			}
		}
	}

	//...........................................................

	protected void GcExperienceSpawnTable()
	{
		var mbin = ExtractMbin<GcExperienceSpawnTable>(
			"METADATA/SIMULATION/SCENE/EXPERIENCESPAWNTABLE.MBIN"
		);
		mbin.OutpostSpawns[0].Count = new(50, 100);  // [1, 3], increase # of spawns
	}
}

//=============================================================================
