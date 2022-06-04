//=============================================================================
// Author: Jackty89
//=============================================================================

public class FastRefiners : cmk.NMS.Script.ModClass
{
	public static float TimeToMake	= 1f;
	protected override void Execute()
	{
		GcRecipeTable();
	}

	//...........................................................

	protected void GcRecipeTable()
	{
		var mbin = ExtractMbin<GcRecipeTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCRECIPETABLE.MBIN");
		foreach( var recipe in mbin.Table ) {
			recipe.TimeToMake = TimeToMake;  // seconds
		}
	}
}

//=============================================================================
