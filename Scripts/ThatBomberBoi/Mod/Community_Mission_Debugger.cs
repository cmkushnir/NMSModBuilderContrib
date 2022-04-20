//=============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

using nms = libMBIN.NMS;
using libMBIN.NMS.Globals;
using libMBIN.NMS.GameComponents;
using libMBIN.NMS.Toolkit;

//=============================================================================

namespace cmk.NMS.Scripts.Mod
{
	public class CommunityMissionDebugger : cmk.NMS.ModScript
	{
		protected override void Execute()
		{
			//GcDebugOptions();
			GcMissionCommunityData();
		}

		//...........................................................

		protected void GcDebugOptions()
		{
			var mbin = ExtractMbin<GcDebugOptions>(
				"GCDEBUGOPTIONS.GLOBAL.MBIN"
			);
		}
		
		protected void GcMissionCommunityData()
			{
				var CMData = ExtractMbin<GcMissionCommunityData>("METADATA/SIMULATION/MISSIONS/MISSIONCOMMUNITYDATA.MBIN");
				
				CMData.CommunityMissionsData[56].ShowTimeToDeadline = true;
				CMData.CommunityMissionsData[57].ShowTimeToDeadline = true;
				CMData.CommunityMissionsData[58].ShowTimeToDeadline = true;
				CMData.CommunityMissionsData[59].ShowTimeToDeadline = true;
				CMData.CommunityMissionsData[60].ShowTimeToDeadline = true;
				CMData.CommunityMissionsData[61].ShowTimeToDeadline = true;



			}
	}
}

//=============================================================================
