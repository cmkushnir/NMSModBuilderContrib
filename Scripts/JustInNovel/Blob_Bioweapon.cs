//=============================================================================
// Adjust max creature size and spawn counts.
// The Blob Bioweapon effectively deletes all Blobs from the wild
// They will not spawn... as this bioweapon has killed them all.
//=============================================================================

public class Blob_Bioweapon : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		Try(() => GcCreatureDataTable());
	}

	//...........................................................



	//...........................................................
	protected void GcCreatureDataTable()
	{
		var mbin = ExtractMbin<GcCreatureDataTable>(
			"METADATA/SIMULATION/ECOSYSTEM/CREATUREDATATABLE.MBIN"
		);

		// alter properties of Blobs so that they do not spawn in the wild
		var blob = mbin.Table.Find(CREATURE => CREATURE.Id == "BLOB");
		blob.EcoSystemCreature = false;
		

		mbin.Table.ForEach(CREATURE => {
            // Check for the blob creature based on a specific property or name
            if (CREATURE.Id == "BLOB")
            {
            	CREATURE.EcoSystemCreature = false;
            	CREATURE.Rarity.CreatureRarity = libMBIN.NMS.GameComponents.GcCreatureRarity.CreatureRarityEnum.SuperRare;
            	CREATURE.OnlySpawnWhenIdIsForced = true;
            	CREATURE.ForceType.CreatureType = libMBIN.NMS.GameComponents.GcCreatureTypes.CreatureTypeEnum.None;
            	CREATURE.HerbivoreProbabilityModifier.CreatureRoleFrequencyModifier = libMBIN.NMS.GameComponents.GcCreatureRoleFrequencyModifier.CreatureRoleFrequencyModifierEnum.Never;
            }
        });
		
	} // end of GcCreatureDataTable
	
	
}

//=============================================================================
