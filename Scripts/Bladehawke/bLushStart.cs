//=============================================================================

//=============================================================================


public class LushStart : cmk.NMS.Script.ModClass
{
  protected override void Execute()
  {
    GcBiomeFileList();
  }
  //..........................................................................

  protected void GcBiomeFileList()
  {
    var mbin = ExtractMbin<GcBiomeFileList>(
        "METADATA/SIMULATION/SOLARSYSTEM/BIOMES/BIOMEFILENAMES.MBIN"
    );

    var ValidStart = mbin.ValidStartPlanetBiome as List<GcBiomeType>;
    ValidStart.Clear();

    var Lush = new GcBiomeType
    {
      Biome = GcBiomeType.BiomeEnum.Lush
    };
    ValidStart.Add(Lush);
  }
}

