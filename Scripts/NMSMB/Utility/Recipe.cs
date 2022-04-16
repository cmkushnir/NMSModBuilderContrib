//=============================================================================
// Helper methods to create recipe elements and refiner|cooking recipes.
// See: mod script Add_Recipe.
//=============================================================================

public class Recipe
{
	protected static uint s_new_recipe_id = 0;

	//...........................................................

	static Recipe()
	{
		// Reset the id counter before each execution of the mod scripts.
		NMS.ScriptFiles.Mod.BeforeResetCalled +=
			( cmk.Roslyn.AdhocDocumentFiles SENDER, ref bool CLEAR_LOGS )
			=> s_new_recipe_id = 0
		;
	}

	//...........................................................

	public static GcRefinerRecipeElement Substance( string ID, int AMOUNT )
	{
		return new GcRefinerRecipeElement {
			Id     = ID,
			Type   = new GcInventoryType { InventoryType = InventoryTypeEnum.Substance },
			Amount = AMOUNT
		};
	}
	
	//...........................................................

	public static GcRefinerRecipeElement Product( string ID, int AMOUNT )
	{
		return new GcRefinerRecipeElement {
			Id     = ID,
			Type   = new GcInventoryType { InventoryType = InventoryTypeEnum.Product },
			Amount = AMOUNT
		};
	}
	
	//...........................................................

	public static GcRefinerRecipeElement Technology( string ID, int AMOUNT )
	{
		return new GcRefinerRecipeElement {
			Id     = ID,
			Type   = new GcInventoryType { InventoryType = InventoryTypeEnum.Technology },
			Amount = AMOUNT
		};
	}
	
	//...........................................................

	/// <summary>
	/// Create new refiner recipe object.
	/// Note: any new result|ingredient substance|product
	/// must have already been added e.g. by prior script.
	/// </summary>
	public static GcRefinerRecipe CreateRefiner(
		string                       TYPE,
		string                       NAME,
		float                        TIME,
		GcRefinerRecipeElement       RESULT,
		List<GcRefinerRecipeElement> INGREDIENTS = null,
		Log                          LOG         = null
	){
		return new GcRefinerRecipe {
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

	/// <summary>
	/// Create new cooking recipe object.
	/// Note: any new result|ingredient substance|product
	/// must have already been added e.g. by prior script.
	/// </summary>
	public static GcRefinerRecipe CreateCooking(
		string                       TYPE,
		string                       NAME,
		float                        TIME,
		GcRefinerRecipeElement       RESULT,
		List<GcRefinerRecipeElement> INGREDIENTS = null,
		Log                          LOG         = null
	){
		return new GcRefinerRecipe {
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
