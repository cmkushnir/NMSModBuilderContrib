//=============================================================================
// Make all refiner|cooking recipes faster to make.
//=============================================================================

public class Recipe_Fast : cmk.NMS.Script.ModClass
{	
	public ulong MaxTimeSeconds = 1;
	
	//...........................................................
	
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcRecipeTable>(
			"METADATA/REALITY/TABLES/NMS_REALITY_GCRECIPETABLE.MBIN"
		);
		foreach( var recipe in mbin.Table ) {
			if( recipe.TimeToMake > MaxTimeSeconds ) recipe.TimeToMake = MaxTimeSeconds;
		}
	}
}

//=============================================================================
