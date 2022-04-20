//=============================================================================

using nms = libMBIN.NMS;
using libMBIN.NMS.Globals;
using libMBIN.NMS.GameComponents;
using libMBIN.NMS.Toolkit;

//=============================================================================

namespace cmk.NMS.Scripts.Mod
{
	
	
	public class Natural_Settlements_Production : cmk.NMS.ModScript
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
            
            var vykeenProductionElements = settlements.VykeenProductionElements;
            
            vykeenProductionElements[0].Value = "WAR_CURIO2";
            vykeenProductionElements[1].Value = "U_SHOTGUNX";
            vykeenProductionElements[2].Value = "SCRAP_WEAP";
            vykeenProductionElements[3].Value = "U_BOLTX";
            vykeenProductionElements[4].Value = "ULTRAPROD1";
			
            var gekProductionElements = settlements.GekProductionElements;
			
            gekProductionElements[0].Value = "TRA_CURIO2";
            gekProductionElements[1].Value = "FOOD_V_GEK";
            gekProductionElements[2].Value = "NIPNIPBUDS";
            gekProductionElements[3].Value = "TRA_EXOTICS3";
            gekProductionElements[4].Value = "ULTRAPROD2";

            var korvaxProductionElements = settlements.KorvaxProductionElements;
			
            korvaxProductionElements[0].Value = "HYPERFUEL2";
            korvaxProductionElements[1].Value = "U_HYPERX";
            korvaxProductionElements[2].Value = "U_HAZARDX";
            korvaxProductionElements[3].Value = "U_ENERGYX";
            korvaxProductionElements[4].Value = "SALVAGE_TECH10";            	
		}
	}
}

//=============================================================================
