//=============================================================================

public class Biome : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		Try(() => GcBiomeListPerStarType());
		Try(() => GcBiomeFileList());
		Try(() => GcBuildingDefinitionTable());
	}
	
	//...........................................................
	
	// increase chance of fnding a lush planet in yellow star systems
	protected void GcBiomeListPerStarType()
	{
		var mbin = ExtractMbin<GcBiomeListPerStarType>(
			"METADATA/SIMULATION/SOLARSYSTEM/BIOMES/BIOMELISTPERSTARTYPE.MBIN"
		);

		var list = mbin.StarType[(int)GalaxyStarTypeEnum.Yellow];
		list.BiomeProbability     [(int)BiomeEnum.Lush] += 2f;  // 2
		list.PrimeBiomeProbability[(int)BiomeEnum.Lush] += 2f;  // 2
		
		list = mbin.LushYellow;
		list.BiomeProbability     [(int)BiomeEnum.Lush] += 2f;  // 4
		list.PrimeBiomeProbability[(int)BiomeEnum.Lush] += 2f;  // 4

		list = mbin.AbandonedYellow;
		list.BiomeProbability     [(int)BiomeEnum.Lush] += 2f;  // 1
		list.PrimeBiomeProbability[(int)BiomeEnum.Lush] += 2f;  // 1
	}

	//...........................................................
	
	protected void GcBiomeFileList()
	{
		var mbin = ExtractMbin<GcBiomeFileList>(
			"METADATA/SIMULATION/SOLARSYSTEM/BIOMES/BIOMEFILENAMES.MBIN"
		);
		
		// allow start on any biome
		mbin.ValidStartPlanetBiome.Clear();
		foreach( var biome in Enum.GetValues<BiomeEnum>() ) {
			mbin.ValidStartPlanetBiome.Add(new(){ Biome = biome });
		}
			
		var files = mbin.BiomeFiles[(int)BiomeEnum.Lush].FileOptions;
		foreach( var file in files ) {
			file.Weight = 1;  // all lush subtypes equally probable
			var biome = ExtractMbin<GcBiomeData>(file.Filename);
			foreach( var weather in biome.WeatherOptions ) {
				//for( var i = 0; i < weather.WeatherWeightings.Length; ++i ) {
				//	weather.WeatherWeightings[i] *= 2;  // most 0, humid = 1
				//}
				weather.WeatherWeightings[(int)WeatherEnum.Clear] = 1;  // 0, add chance for clear lush biome
			}
			foreach( var filter in biome.FilterOptions ) {
				if( filter.Filter.ScreenFilter != ScreenFilterEnum.Default ) {
					filter.Weight = 0;  // only default screen filter
				}
			}
		}
	}
	
	//...........................................................

	// find more crashed ships to salvage on some planet types
	protected void GcBuildingDefinitionTable()
	{
		var mbin = ExtractMbin<GcBuildingDefinitionTable>(
			"METADATA/SIMULATION/ENVIRONMENT/PLANETBUILDINGTABLE.MBIN"
		);
	
		var defn = mbin.BuildingPlacement[(int)BuildingClassEnum.DistressSignal];
		defn.Density[(int)BuildingDensityEnum.Dead]  = 10;
		defn.Density[(int)BuildingDensityEnum.Weird] = 10;
		defn.Density[(int)BuildingDensityEnum.HalfWeird] = 5;
	
		defn = mbin.BuildingPlacement[(int)BuildingClassEnum.WaterDistressSignal];
		defn.Density[(int)BuildingDensityEnum.Dead]  = 10;
		defn.Density[(int)BuildingDensityEnum.Weird] = 10;
		defn.Density[(int)BuildingDensityEnum.HalfWeird] = 5;
	}	
}

//=============================================================================
