//=============================================================================

using nms = libMBIN.NMS;
using libMBIN.NMS.Globals;
using libMBIN.NMS.GameComponents;
using libMBIN.NMS.Toolkit;

//=============================================================================

namespace cmk.NMS.Scripts.Mod
{
	public class Natural_Settlements_Decisions : cmk.NMS.ModScript
	{
	protected override void Execute()
		{
			GcSettlementGlobals();
		}

		//...........................................................

		protected void GcSettlementGlobals()
		{
			var mbin = ExtractMbin<GcSettlementGlobals>(
				"GCSETTLEMENTGLOBALS.MBIN"
			);
			
			mbin.JudgementWaitTimeMin = 90;
			mbin.JudgementWaitTimeMax = 320;
		}
	}
}

//=============================================================================
