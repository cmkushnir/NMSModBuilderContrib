//=============================================================================

public class QuickSilverCrafting : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		AddQuickSilverRecipe();
	}
	//...........................................................
	protected void AddQuickSilverRecipe()
	{
		var mbin = ExtractMbin<GcRecipeTable>(
			"METADATA/REALITY/TABLES/NMS_REALITY_GCRECIPETABLE.MBIN"
		);

		var taintedRecipe = RefinerRecipe.CreateRefiner(
			"RECIPE_FISHCORE",   			// RecipeType, normally a lang id else will use supplied string
			"RECIPE_SPACEGUNK3",            // RecipeName, not sure how game uses this
			10,                             // Time
			RefinerRecipe.Substance("QUICKSILVER", 250), //Result & AMount
			new(){
			RefinerRecipe.Substance("ASTEROID3",  250),  // Platinum
				RefinerRecipe.Substance("SENTINEL_LOOT", 1),  // Salvaged Glass
				RefinerRecipe.Substance("STORM_CRYSTAL", 1)   // storm crystal
			},
			Log
		);
		mbin.Table.Add(taintedRecipe);
	}
}
//=============================================================================