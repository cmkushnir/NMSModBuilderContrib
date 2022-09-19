//=============================================================================

public class Spawns_Experience : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcExperienceSpawnTable>(
			"METADATA/SIMULATION/SCENE/EXPERIENCESPAWNTABLE.MBIN"
		);
		SentinelSequences_HarderWanted(mbin);
	}

	//...........................................................
	
	protected void SentinelSequences_HarderWanted( GcExperienceSpawnTable MBIN )
	{
		foreach( var sequence in MBIN.SentinelSequences ) {
			if( !sequence.Id.Value.StartsWith("WANTED_") ) continue;
			sequence.Waves.Add(new(){
				Waves = new(){
			    	"QUAD_SET_HARD",   // 1 - Quad,   1 - SummonerDrone, 2 - MedicDrone
			    	"WALKER_SET_HARD"  // 1 - Walker, 2 - SummonerDrone, 4 - MedicDrone
			    }
			});
		}
	}
}

//=============================================================================
