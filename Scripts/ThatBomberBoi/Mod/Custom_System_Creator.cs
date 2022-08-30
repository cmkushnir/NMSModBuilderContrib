//=============================================================================

public class Custom_System_Creator : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		handleMBINs();
		setDebugSettings();
	}
	
	private void handleMBINs()
	{
		//Create Cloned Files
		var mainSettings = CloneMbin<GcSceneSettings>("SCENES/DEMOS/MARCH19VR/MAINSETTINGS.MBIN", "CUSTOMDEMOS/TEST1/MAINSETTINGS.MBIN");
		var emptyScene = CloneMbin<TkSceneNodeData>("SCENES/DEMOS/MARCH19VR/EMPTY.SCENE.MBIN", "CUSTOMDEMOS/TEST1/EMPTY.SCENE.MBIN");
		var solarSystem = CloneMbin<GcSolarSystemData>("SCENES/DEMOS/MARCH19VR/SOLARSYSTEMS/SYSTEM_18695887738244.MBIN", "CUSTOMDEMOS/TEST1/SOLARSYSTEMS/SYSTEM_1.MBIN");
		var planet1 = CloneMbin<GcPlanetData>("SCENES/DEMOS/MARCH19VR/PLANETS/PLANET1654862369509959555.MBIN", "CUSTOMDEMOS/TEST1/PLANETS/PLANET_1.MBIN");
		var planet2 = CloneMbin<GcPlanetData>("SCENES/DEMOS/MARCH19VR/PLANETS/PLANET2541918518751164369.MBIN", "CUSTOMDEMOS/TEST1/PLANETS/PLANET_2.MBIN");
		var planet3 = CloneMbin<GcPlanetData>("SCENES/DEMOS/MARCH19VR/PLANETS/PLANET7870535461844554869.MBIN", "CUSTOMDEMOS/TEST1/PLANETS/PLANET_3.MBIN");
		var planet4 = CloneMbin<GcPlanetData>("SCENES/DEMOS/MARCH19VR/PLANETS/PLANET17184334768048820473.MBIN", "CUSTOMDEMOS/TEST1/PLANETS/PLANET_4.MBIN");
	
		//Setup Scene
		mainSettings.SceneFile = "CUSTOMDEMOS/TEST1/EMPTY.SCENE.MBIN";	
		mainSettings.SolarSystemFile = "CUSTOMDEMOS/TEST1/SOLARSYSTEMS/SYSTEM_1.MBIN";
		mainSettings.PlanetFiles[0] = "CUSTOMDEMOS/TEST1/PLANETS/PLANET_1.MBIN";
		mainSettings.PlanetFiles[1] = "CUSTOMDEMOS/TEST1/PLANETS/PLANET_2.MBIN";
		mainSettings.PlanetFiles[2] = "CUSTOMDEMOS/TEST1/PLANETS/PLANET_3.MBIN";
		mainSettings.PlanetFiles[3] = "CUSTOMDEMOS/TEST1/PLANETS/PLANET_4.MBIN";
		
		//Modify Objects (Test, can be modified as needed)
		solarSystem.PlanetGenerationInputs[0].Biome.Biome = BiomeEnum.Lush;
		solarSystem.PlanetGenerationInputs[0].BiomeSubType.BiomeSubType = GcBiomeSubType.BiomeSubTypeEnum.HugeLush;
		solarSystem.PlanetGenerationInputs[0].Prime = true;
		
		solarSystem.PlanetGenerationInputs[1].Biome.Biome = BiomeEnum.Frozen;
		solarSystem.PlanetGenerationInputs[1].BiomeSubType.BiomeSubType = GcBiomeSubType.BiomeSubTypeEnum.Variant_A;
		
		solarSystem.PlanetGenerationInputs[2].Biome.Biome = BiomeEnum.Barren;
		solarSystem.PlanetGenerationInputs[2].BiomeSubType.BiomeSubType = GcBiomeSubType.BiomeSubTypeEnum.HighQuality;
		
		solarSystem.PlanetGenerationInputs[3].Biome.Biome = BiomeEnum.Dead;
		solarSystem.PlanetGenerationInputs[3].BiomeSubType.BiomeSubType = GcBiomeSubType.BiomeSubTypeEnum.Variant_D;
		
		solarSystem.Name = "Grafningard";
		solarSystem.PrimePlanets = 1;
		
		solarSystem.Light.SunColour.R = 0.922f;
		solarSystem.Light.SunColour.G = 0.835f;
		solarSystem.Light.SunColour.B = 0.075f;
		solarSystem.Light.SunColour.A =	1.000f;
		
		solarSystem.Light.LightColour.R = 0.922f;
		solarSystem.Light.LightColour.G = 0.835f;
		solarSystem.Light.LightColour.B = 0.075f;
		solarSystem.Light.LightColour.A =	1.000f;
		
		solarSystem.Light.BounceColour.R = 0.922f;
		solarSystem.Light.BounceColour.G = 0.835f;
		solarSystem.Light.BounceColour.B = 0.075f;
		solarSystem.Light.BounceColour.A =	1.000f;
		
		solarSystem.ScreenFilter.ScreenFilter = GcScreenFilters.ScreenFilterEnum.Vintage;
		solarSystem.InhabitingRace.AlienRace = GcAlienRace.AlienRaceEnum.Explorers;
		
		solarSystem.TradingData.WealthClass.WealthClass = GcWealthClass.WealthClassEnum.Wealthy;
		solarSystem.TradingData.TradingClass.TradingClass = GcTradingClass.TradingClassEnum.Scientific;	
		solarSystem.ConflictData.ConflictLevel = GcPlayerConflictData.ConflictLevelEnum.Low;
		
		var systemUE = ExtractMbin<GcSolarSystemData>("SCENES/DEMOS/USEREXPERIENCE/MAINSETTINGS/SOLARSYSTEMS/SOLARSYSTEM1.MBIN");
		for(var i=0; i<24; i++)
			{
				solarSystem.Colours.Palettes[i] = systemUE.Colours.Palettes[i];

			}
		
		solarSystem.Light.SunColour = systemUE.Light.SunColour;
		solarSystem.Light.LightColour = systemUE.Light.LightColour;
		solarSystem.Light.BounceColour = systemUE.Light.BounceColour;

		solarSystem.Sky.PlanetSkyColour = systemUE.Sky.PlanetSkyColour;
		solarSystem.Sky.PlanetHorizonColour = systemUE.Sky.PlanetHorizonColour;
		
		for(var i=0; i<3; i++)
			{
				solarSystem.PlanetGenerationInputs[i].Seed = systemUE.PlanetGenerationInputs[i].Seed;
			}
		
		for(var i=0; i<7; i++)
			{
				solarSystem.PlanetOrbits[i] = 0;


			}
	}
	
	
	private void setDebugSettings()
	{
		//Setup Requirements
		var debugOptions = ExtractMbin<GcDebugOptions>("GCDEBUGOPTIONS.GLOBAL.MBIN");
		
		debugOptions.PlayerSpawnLocationOverride = PlayerSpawnLocationOverrideEnum.FromSettings;
		debugOptions.SolarSystemBoot = SolarSystemBootEnum.FromSettings;
		debugOptions.SceneSettings = "CUSTOMDEMOS/TEST1/MAINSETTINGS.MBIN";
		debugOptions.WorkingDirectory = "CUSTOMDEMOS/TEST1";


	}

	//...........................................................
}

//=============================================================================
