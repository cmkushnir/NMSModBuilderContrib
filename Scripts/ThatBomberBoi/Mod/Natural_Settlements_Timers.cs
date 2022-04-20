//=============================================================================

using nms = libMBIN.NMS;
using libMBIN.NMS.Globals;
using libMBIN.NMS.GameComponents;
using libMBIN.NMS.Toolkit;



//=============================================================================

namespace cmk.NMS.Scripts.Mod
{
	
	
	public class Natural_Settlements_Timers : cmk.NMS.ModScript
	{
		protected override void Execute()
		{
			GcSettlementGlobals();
		}

		//...........................................................

		protected void GcSettlementGlobals()
		{
                var settlements = ExtractMbin<GcSettlementGlobals>(
                "GCSETTLEMENTGLOBALS.MBIN"
            );
            
			
            var buildingTimes = settlements.SettlementBuildingTimes;
            
            buildingTimes[37] = 30;
            buildingTimes[38] = 30;
            buildingTimes[39] = 30;
            buildingTimes[40] = 30;
            buildingTimes[41] = 30;
            buildingTimes[42] = 30;
            buildingTimes[43] = 30;
            buildingTimes[44] = 30;
            buildingTimes[45] = 30;
            buildingTimes[46] = 30;
            buildingTimes[47] = 30;
            buildingTimes[48] = 30;
            buildingTimes[49] = 30;
            buildingTimes[50] = 30;
		}

	}
     }
    
//=============================================================================
