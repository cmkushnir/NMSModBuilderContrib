//=============================================================================

public class FastOrganicFrigates : cmk.NMS.Script.ModClass
{
    protected override void Execute()
    {
        var mbin = ExtractMbin<GcMissionTable>(
            "METADATA/SIMULATION/MISSIONS/SPACEPOIMISSIONTABLE.MBIN"
        );
        var squids = mbin.Missions.Find(NAME => NAME.MissionID == "BIO_FRIG");
        squids.RestartOnCompletion = true;
        squids.CancelingConditions.Clear();
		
        var encounter = stageSearch(squids.Stages);
        encounter.PulseEncounterID = "BIO_FRIG";
    }

    protected GcMissionSequenceCreateSpecificPulseEncounter stageSearch(
        List<GcGenericMissionStage> STAGES
    )
    {
        if (!STAGES.IsNullOrEmpty())
        {
            foreach (var stage in STAGES)
            {

                if (stage.Stage is GcMissionSequenceGroup recurse)
                {
                    return stageSearch(recurse.Stages);
                }
                if (stage.Stage is GcMissionSequenceCreateSpecificPulseEncounter foundIt)
                {
                    return foundIt;
                } 
            }
        }
        return null;
    }
    //...........................................................
}

//=============================================================================
