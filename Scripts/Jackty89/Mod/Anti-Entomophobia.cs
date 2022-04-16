//=============================================================================

namespace cmk.NMS.Scripts.Mod
{
	public class AntiEntomophobia : cmk.NMS.ModScript
	{
		protected override void Execute()
		{
			GcDebugOptions();
		}

		//...........................................................

		protected void GcDebugOptions()
		{
			string [] DelCreature = new string []{"BUTTERFLY","BUTTERFLOCK","LARGEBUTTERFLY","FLYINGBEETLE","SPIDER","FLOATSPIDER","WEIRDBUTTERFLY"};
			
			var mbinCreatureData = ExtractMbin<GcCreatureDataTable>("METADATA/SIMULATION/ECOSYSTEM/CREATUREDATATABLE.MBIN");
			var mbinFileName = ExtractMbin<GcCreatureFilenameTable>("METADATA/SIMULATION/ECOSYSTEM/CREATUREFILENAMETABLE.MBIN");
			
			foreach(var id in DelCreature)
			{
				mbinCreatureData.Table.Remove(mbinCreatureData.Table.Find(CREATURE => CREATURE.Id == id));
				mbinFileName.Table.Remove(mbinFileName.Table.Find(CREATURE => CREATURE.ID == id));
			}
			
		
		}
	}
}

//=============================================================================
