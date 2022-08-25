//=============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;



//=============================================================================

namespace cmk.NMS.Scripts.Mod
{
	public class CommunityMissionDebugger : cmk.NMS.Script.ModClass
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
				
				
				foreach(var entry in CMData.CommunityMissionsData)
					{	
						entry.ShowTimeToDeadline = true;




					}


			}
	}
}

//=============================================================================
