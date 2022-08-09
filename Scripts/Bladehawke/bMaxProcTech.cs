//=============================================================================

public class bMaxProcTech : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcProceduralTechnologyTable>(
			"METADATA/REALITY/TABLES/NMS_REALITY_GCPROCEDURALTECHNOLOGYTABLE.MBIN"
		);
		
		foreach(var upgrade in mbin.Table) {
			upgrade.NumStatsMin = upgrade.NumStatsMax;
			upgrade.WeightingCurve.WeightingCurve = WeightingCurveEnum.NoWeighting;
			
			foreach(var stat in upgrade.StatLevels) {
				stat.ValueMin = stat.ValueMax;
				stat.WeightingCurve.WeightingCurve = WeightingCurveEnum.NoWeighting;
			}
		}
	}

	//...........................................................
}

//=============================================================================
