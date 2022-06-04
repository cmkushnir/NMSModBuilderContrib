//=============================================================================

public class SkipTutorial : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		GcDebugOptions();
		CoreMissionEdits();
	}

	protected void GcDebugOptions()
	{
		var mbin = ExtractMbin<GcDebugOptions>("GCDEBUGOPTIONS.GLOBAL.MBIN");
		mbin.SkipTutorial         = true;
		mbin.SkipIntro 	  	      = true;
		mbin.MissionSurveyEnabled = false;
	}
	protected void CoreMissionEdits()
	{
		var mbin = ExtractMbin<GcMissionTable>("METADATA/SIMULATION/MISSIONS/COREMISSIONTABLE.MBIN");
		
		List <string> skipMissionIds = new List<string> () {"ACT1_STEP1", "ACT1_STEP3"};
		foreach(string missionId in skipMissionIds)
		{
			var mission = mbin.Missions.Find(ID => ID.MissionID == missionId);
			mission.AutoStart = AutoStartEnum.None;
		}		
	}

}