//=============================================================================

public partial class RefinerRecipe
{
	public static GcRefinerRecipe CreateRefiner(
		string ID,
		string TYPE,
		string NAME,
		float  TIME,
		GcRefinerRecipeElement       RESULT,
		List<GcRefinerRecipeElement> INGREDIENTS = null,
		Log                          LOG         = null
	) => new() {
		Id          = ID,
		RecipeType  = TYPE,
		RecipeName  = NAME,
		TimeToMake  = TIME,
		Cooking     = false,
		Result      = RESULT,
		Ingredients = INGREDIENTS
	};

	//...........................................................

	public static GcRefinerRecipe CreateCooking(
		string ID,
		string NAME,
		float  TIME,
		GcRefinerRecipeElement       RESULT,
		List<GcRefinerRecipeElement> INGREDIENTS = null,
		Log                          LOG         = null
	) => new() {
		Id          = ID,
		RecipeType  = NAME,
		RecipeName  = NAME,
		TimeToMake  = TIME,
		Cooking     = true,
		Result      = RESULT,
		Ingredients = INGREDIENTS
	};
}

//=============================================================================
