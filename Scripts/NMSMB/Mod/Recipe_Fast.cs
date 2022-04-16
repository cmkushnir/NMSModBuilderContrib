//=============================================================================
// Make all refiner|cooking recipes faster to make.
//=============================================================================

public class Recipe_Fast : cmk.NMS.ModScript
{	
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcRecipeTable>(
			"METADATA/REALITY/TABLES/NMS_REALITY_GCRECIPETABLE.MBIN"
		);
		foreach( var recipe in mbin.Table ) {
			recipe.TimeToMake = 1;  // seconds
		}
	}
}

//=============================================================================
