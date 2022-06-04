//=============================================================================
// Author: Jackty89
//=============================================================================

public class AntiOphidiophobia : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		GcDebugOptions();
	}

	//...........................................................

	protected void GcDebugOptions()
	{
		string [] DelCreature = {"SEASNAKE", "FLYINGSNAKE", "SANDWORM"};

		var mbinCreatureData = ExtractMbin<GcCreatureDataTable>    ("METADATA/SIMULATION/ECOSYSTEM/CREATUREDATATABLE.MBIN");
		var mbinFileName     = ExtractMbin<GcCreatureFilenameTable>("METADATA/SIMULATION/ECOSYSTEM/CREATUREFILENAMETABLE.MBIN");

		foreach( var id in DelCreature ) {
			mbinCreatureData.Table.Remove(mbinCreatureData.Table.Find(CREATURE => CREATURE.Id == id));
			mbinFileName    .Table.Remove(mbinFileName    .Table.Find(CREATURE => CREATURE.ID == id));
		}
	}
}

//=============================================================================
