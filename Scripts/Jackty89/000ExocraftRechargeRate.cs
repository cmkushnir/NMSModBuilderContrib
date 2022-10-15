//=============================================================================
// Author: Jackty89
//=============================================================================

public class ExocraftRechargeRate : cmk.NMS.Script.ModClass
{
	public float RechargeRate = 15f;

	protected override void Execute()
	{
		GcDebugOptions();
	}

	protected void GcDebugOptions()
	{
		var mbin = ExtractMbin<GcTechnologyTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCTECHNOLOGYTABLE.MBIN");
		var exoRecharge = mbin.Table.Find(TECH => TECH.ID == "EXO_RECHARGE");
		exoRecharge.StatBonuses.Find(STAT => STAT.Stat.StatsType == GcStatsTypes.StatsTypeEnum.Vehicle_FuelRegen).Bonus = RechargeRate;
	}
}

//=============================================================================
