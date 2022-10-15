//=============================================================================
// Author: Jackty89
//=============================================================================

public class TaintedMetalCrafting : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		AddTaintedMetalRecipe();
	}
	//...........................................................
	protected void AddTaintedMetalRecipe()
	{
		var mbin = ExtractMbin<GcRecipeTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCRECIPETABLE.MBIN");

		var taintedRecipe = RefinerRecipe.CreateRefiner(
			"CUSTR_TAINTED",
			"RECIPE_FISHCORE",   			// RecipeType, normally a lang id else will use supplied string
			"RECIPE_SPACEGUNK3",            // RecipeName, not sure how game uses this
			10,                             // Time
			RefinerRecipe.Substance("AF_METAL", 250), //Result & AMount
			new(){
				RefinerRecipe.Substance("SPACEGUNK3",  500),  // Rusted Metal
				RefinerRecipe.Substance("SENTINEL_LOOT", 1),  // Pugneum
				RefinerRecipe.Substance("STORM_CRYSTAL", 1)   // storm crystal
			},
			Log
		);

		mbin.Table.Add(taintedRecipe);
	}
}

//=============================================================================
