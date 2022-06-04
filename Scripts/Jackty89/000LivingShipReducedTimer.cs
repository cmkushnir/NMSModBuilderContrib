//=============================================================================

public class LivingShipReducedTimer : cmk.NMS.Script.ModClass
{
	
	public static float Multiplier = 0.001f;
	protected override void Execute()
	{
		
		var mbin = ExtractMbin<GcMissionTable>("METADATA/SIMULATION/MISSIONS/SPACEPOIMISSIONTABLE.MBIN");
		foreach( var mission in mbin.Missions ) {
			GcGenericMissionStage(mission.Stages);
		}
	}

	//...........................................................

	protected void GcGenericMissionStage( List<GcGenericMissionStage> STAGES )
	{
		if( !STAGES.IsNullOrEmpty() )
		foreach( var stage in STAGES ) {
			if( stage.Stage is GcMissionSequenceGroup group ) {
				GcGenericMissionStage(group.Stages);  // recurse
			}
			if( stage.Stage is GcMissionSequenceWaitRealTime timer ) {
				timer.Time = (ulong) (Multiplier * timer.Time);
			}
		}
	}
}

//=============================================================================
