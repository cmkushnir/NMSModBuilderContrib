//=============================================================================
// Author: Jackty89
//=============================================================================

public class RealisticTimers : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		GcSettlementGlobals();
		GcFleetGlobals();
	}

	
	protected void GcSettlementGlobals()
	{
		var mbin = ExtractMbin<GcSettlementGlobals>("GCSETTLEMENTGLOBALS.MBIN");
		
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_LandingZone]     = (ulong)(1814400);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Bar]             = (ulong)(15778463);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Tower]           = (ulong)(47335389);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Market]          = (ulong)(47335389);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Small]           = (ulong)(7889231);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_SmallIndustrial] = (ulong)(6963840);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Medium]          = (ulong)(1814400);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Large]           = (ulong)(29453130);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Monument]        = (ulong)(10368000);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_SheriffsOffice]  = (ulong)(63113851);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Double]          = (ulong)(1814400);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Farm]            = (ulong)(10518975);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Factory]         = (ulong)(63113851);
		mbin.SettlementBuildingTimes[(int)BuildingClassEnum.Settlement_Clump]           = (ulong)(47335389);
	}
	
	protected void GcFleetGlobals()
	{
		var mbin = ExtractMbin<GcFleetGlobals>("GCFLEETGLOBALS.GLOBAL.MBIN");

		mbin.TimeTakenForExpeditionEvent_Easy = 47335389;
		mbin.TimeTakenForExpeditionEvent      = 63113851;
	}
}

//=============================================================================
