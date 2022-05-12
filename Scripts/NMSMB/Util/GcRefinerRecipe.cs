//=============================================================================

public partial class RefinerRecipe
{
	protected static uint s_new_recipe_id = 0;

	//...........................................................

	static RefinerRecipe()
	{
		// Reset the id counter before each mod project execution.
		// We serialize Builds so shouldn't have any conflict between mod projects.
		NMS.Script.Files.Mod.BeforeModFilesCacheReleased += ( SENDER )
		=> s_new_recipe_id = 0;
	}

	//...........................................................

	public static GcRefinerRecipe CreateRefiner(
		string                       TYPE,
		string                       NAME,
		float                        TIME,
		GcRefinerRecipeElement       RESULT,
		List<GcRefinerRecipeElement> INGREDIENTS = null,
		Log                          LOG         = null
	){
		return new(){
			Id          = $"cmkRecipeRefiner{++s_new_recipe_id:d4}",
			RecipeType  = TYPE,
			RecipeName  = NAME,
			TimeToMake  = TIME,
			Cooking     = false,
			Result      = RESULT,
			Ingredients = INGREDIENTS,
		};
	}

	//...........................................................

	public static GcRefinerRecipe CreateCooking(
		string                       TYPE,
		string                       NAME,
		float                        TIME,
		GcRefinerRecipeElement       RESULT,
		List<GcRefinerRecipeElement> INGREDIENTS = null,
		Log                          LOG         = null
	){
		return new(){
			Id          = $"cmkRecipeCooking{++s_new_recipe_id:d4}",
			RecipeType  = TYPE,
			RecipeName  = NAME,
			TimeToMake  = TIME,
			Cooking     = true,
			Result      = RESULT,
			Ingredients = INGREDIENTS,
		};
	}
}

//=============================================================================
