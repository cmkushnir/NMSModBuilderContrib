//=============================================================================

public class PirateTimerRedux : cmk.NMS.Script.ModClass
{
	public static int Multiplier = 3;
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcGameplayGlobals>("GCGAMEPLAYGLOBALS.GLOBAL.MBIN");
		mbin.PirateEarlySpawnTime              *= Multiplier;
		
		mbin.PlanetPirateTimers.Low 	       = mbin.PlanetPirateTimers.Low.Scale(Multiplier);
		mbin.PlanetPirateTimers.Normal         = mbin.PlanetPirateTimers.Normal.Scale(Multiplier);
		mbin.PlanetPirateTimers.High           = mbin.PlanetPirateTimers.High.Scale(Multiplier);
		mbin.PlanetPirateTimers.LowChance      *= Multiplier;
		mbin.PlanetPirateTimers.HighChance     *= Multiplier;

		mbin.HardPlanetPirateTimers.Low        = mbin.HardPlanetPirateTimers.Low.Scale(Multiplier);
		mbin.HardPlanetPirateTimers.Normal     = mbin.HardPlanetPirateTimers.Normal.Scale(Multiplier);
		mbin.HardPlanetPirateTimers.High       = mbin.HardPlanetPirateTimers.High.Scale(Multiplier);
		mbin.HardPlanetPirateTimers.LowChance  *= Multiplier;
		mbin.HardPlanetPirateTimers.HighChance *= Multiplier;
		
		mbin.SpacePirateTimers.Low             = mbin.SpacePirateTimers.Low.Scale(Multiplier);
		mbin.SpacePirateTimers.Normal          = mbin.SpacePirateTimers.Normal.Scale(Multiplier);
		mbin.SpacePirateTimers.High            = mbin.SpacePirateTimers.High.Scale(Multiplier);
		mbin.SpacePirateTimers.LowChance       *= Multiplier;
		mbin.SpacePirateTimers.HighChance      *= Multiplier;	

		mbin.HardSpacePirateTimers.Low         = mbin.HardSpacePirateTimers.Low.Scale(Multiplier);
		mbin.HardSpacePirateTimers.Normal      = mbin.HardSpacePirateTimers.Normal.Scale(Multiplier);
		mbin.HardSpacePirateTimers.High        = mbin.HardSpacePirateTimers.High.Scale(Multiplier);
		mbin.HardSpacePirateTimers.LowChance   *= Multiplier;
		mbin.HardSpacePirateTimers.HighChance  *= Multiplier;			

	}

	//...........................................................
}

//=============================================================================
