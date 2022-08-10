//=============================================================================

//=============================================================================

public class _bMoreFreightersAndSpaceBattles : cmk.NMS.Script.ModClass
{
  protected override void Execute()
  {
    GcAISpaceshipGlobals();
    GcGameplayGlobals();
  }

  //...........................................................

  protected void GcAISpaceshipGlobals()
  {
    var mbin = ExtractMbin<GcAISpaceshipGlobals>(
      "GCAISPACESHIPGLOBALS.GLOBAL.MBIN"
    );
    mbin.FreighterSpawnRate = 100;
  }

  //...........................................................

  protected void GcGameplayGlobals()
  {
    var mbin = ExtractMbin<GcGameplayGlobals>(
      "GCGAMEPLAYGLOBALS.GLOBAL.MBIN"
    );
    mbin.WarpsBetweenBattles = 0;
    mbin.HoursBetweenBattles = 0;
  }
}


//=============================================================================
