//=============================================================================
// Author: Jackty89
//=============================================================================

public class SettlementTimerReduction : cmk.NMS.Script.ModClass
{

	public static float  Multiplier = 0.1f;
	protected override void Execute()
	{
		GcSettlementGlobals();
	}
	//...........................................................`
	protected void GcSettlementGlobals()
	{
		var mbin = ExtractMbin<GcSettlementGlobals>("GCSETTLEMENTGLOBALS.MBIN");

		//Math.Round(val, decimals) might be useable			
		mbin.BuildingUpgradeTimeInSeconds     = (ulong)(Multiplier * mbin.BuildingUpgradeTimeInSeconds);
		mbin.BuildingFreeUpgradeTimeInSeconds = (ulong)(Multiplier * mbin.BuildingFreeUpgradeTimeInSeconds);
		mbin.BuildingUpgradeTimeInSeconds     = (ulong)(Multiplier * mbin.BuildingUpgradeTimeInSeconds);
		mbin.JudgementWaitTimeMin             = (int)  (Multiplier * mbin.JudgementWaitTimeMin);
		mbin.JudgementWaitTimeMax             = (int)  (Multiplier * mbin.JudgementWaitTimeMax);

		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_LandingZone]     = (ulong)(Multiplier * mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_LandingZone]);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Bar]             = (ulong)(Multiplier * mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Bar]);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Tower]           = (ulong)(Multiplier * mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Tower]);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Market]          = (ulong)(Multiplier * mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Market]);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Small]           = (ulong)(Multiplier * mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Small]);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_SmallIndustrial] = (ulong)(Multiplier * mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_SmallIndustrial]);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Medium]          = (ulong)(Multiplier * mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Medium]);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Large]           = (ulong)(Multiplier * mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Large]);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Monument]        = (ulong)(Multiplier * mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Monument]);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_SheriffsOffice]  = (ulong)(Multiplier * mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_SheriffsOffice]);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Double]          = (ulong)(Multiplier * mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Double]);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Farm]            = (ulong)(Multiplier * mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Farm]);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Factory]         = (ulong)(Multiplier * mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Factory]);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Clump]           = (ulong)(Multiplier * mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Clump]);
	}
}

//=============================================================================
