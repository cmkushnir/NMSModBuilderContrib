//=============================================================================
// Use helper methods to add new refiner and cooking recipes.
// If using any new products|substannces as results or ingredients then they
// need to already be added to the appropriate mbin, see: Add_Product.
//=============================================================================

public class Add_Recipe : cmk.NMS.Script.ModClass
{
	public uint NewRefinerRecipeId = 0;  // incremented for each new refiner recipe
	public uint NewCookingRecipeId = 0;  // incremented for each new cooking recipe

	//...........................................................
	
	protected override void Execute()
	{
		Try(() => {
			var recipes = CreateRefiner();
			GcRecipeTable(recipes);
			GcDefaultSaveData(recipes);
		});
		Try(() => {
			var recipes = CreateCooking();
			GcRecipeTable(recipes);
			GcDefaultSaveData(recipes);
		});
	}

	//...........................................................

	// Add new refiner recipes here:
	protected List<GcRefinerRecipe> CreateRefiner()
	{	
		var list = new List<GcRefinerRecipe>();
		list.Add(RefinerRecipe.CreateRefiner(
			// Tainted Metal:
			// Shredded fragments of a soft, grey metal. Its surface is porous,
			// and when touched it oozes ever-so-slightly.
			// Found in the creaking wreck of a derelict freighter.
			// The space station <STELLAR>Scrap Dealer<> may find this interesting...
			$"cmkRecipeRefiner{++NewRefinerRecipeId:d6}",
			"X031415 - [CLASSIFIED]",                       // RecipeType, normally a lang id else will use supplied string
			"R_CMK_X031415", 10,                            // RecipeName, not sure how game uses this, 10 sec to make
			RefinerRecipe.Substance("AF_METAL", 10),        // Result: Tainted Metal
			new(){                                          // List of ingredients:
				RefinerRecipe.Substance("SPACEGUNK3", 10),  // Rusted Metal
				RefinerRecipe.Substance("ROBOT1",      5),  // Pugneum
				RefinerRecipe.Substance("CREATURE1",   5)   // Mordite
			},
			Log
		));
		return list;
	}

	//...........................................................

	// Add new cooking recipes here:
	protected List<GcRefinerRecipe> CreateCooking()
	{
		var list = new List<GcRefinerRecipe>();
		list.Add(RefinerRecipe.CreateCooking(
			$"cmkRecipeCooking{++NewCookingRecipeId:d6}",
			"UI_COOK_CAKE", 60,                               // Assemble Baked Product, cooking recipes have RecipeType == RecipeName
			RefinerRecipe.Product("FOOD_SCHIPCOOK", 10),      // Stellar Chip Cookies
			new(){
				RefinerRecipe.Product("FOOD_R_GCAKEMIX", 1),  // Proto-Batter
				RefinerRecipe.Product("FOOD_SCHIPS",     1),  // Stellar Chips
			},
			Log
		));
		list.Add(RefinerRecipe.CreateCooking(
			$"cmkRecipeCooking{++NewCookingRecipeId:d6}",
			"UI_COOK_CAKE", 60,                               // Assemble Baked Product
			RefinerRecipe.Product("FOOD_SCHIPCOOKA", 10),     // Activated Stellar Chip Cookies
			new(){
				RefinerRecipe.Product("FOOD_R_BCAKEMIX", 1),  // Thick, Sweet Batter
				RefinerRecipe.Product("FOOD_SCHIPSA",    1),  // Activated Stellar Chips
			},
			Log
		));
		return list;
	}

	//...........................................................

	protected void GcRecipeTable( List<GcRefinerRecipe> RECIPES )
	{
		if( RECIPES.IsNullOrEmpty() ) return;
		var mbin = ExtractMbin<GcRecipeTable>(
			"METADATA/REALITY/TABLES/NMS_REALITY_GCRECIPETABLE.MBIN"
		);
		RECIPES.ForEach(RECIPE => mbin.Table.AddUnique(RECIPE));
	}

	//...........................................................

	protected void GcDefaultSaveData( List<GcRefinerRecipe> RECIPES )
	{
		if( RECIPES.IsNullOrEmpty() ) return;
		var mbin = ExtractMbin<GcDefaultSaveData>(
			"METADATA/GAMESTATE/DEFAULTSAVEDATA.MBIN"
		);
		RECIPES.ForEach(RECIPE => mbin.State.KnownRefinerRecipes.AddUnique(RECIPE.Id));
	}
}

//=============================================================================
