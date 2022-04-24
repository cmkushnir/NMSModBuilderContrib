//=============================================================================
// Use helper methods to add new refiner and cooking recipes.
// If using any new products|substannces as results or ingredients then they
// need to already be added to the appropriate mbin, see: Add_Product.
//=============================================================================

public class Add_Recipe : cmk.NMS.Script.ModClass
{
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
		list.Add(Recipe.CreateRefiner(
			// Tainted Metal:
			// Shredded fragments of a soft, grey metal. Its surface is porous,
			// and when touched it oozes ever-so-slightly.
			// Found in the creaking wreck of a derelict freighter.
			// The space station <STELLAR>Scrap Dealer<> may find this interesting...
			"X031415 - [CLASSIFIED]",                // RecipeType, normally a lang id else will use supplied string
			"R_CMK_X031415",                         // RecipeName, not sure how game uses this
			10,                                      // 10 sec to make
			Recipe.Substance("AF_METAL", 10),        // Result: Tainted Metal
			new(){                                   // List of ingredients:
				Recipe.Substance("SPACEGUNK3", 10),  // Rusted Metal
				Recipe.Substance("ROBOT1",      5),  // Pugneum
				Recipe.Substance("CREATURE1",   5)   // Mordite
			},
			Log
		));
		list.Add(Recipe.CreateRefiner(
			// Walker Brain:
			// Shifting nanite clusters sewn together with a pugneum filament,
			// this circuit is painfully hot to the touch.
			// A sinister purple light leaks from deep within its wiring,
			// changing in intensity as it watches its holder.
			"Neural Construction",
			"R_CMK_NEURAL_CONSTRUCTION", 120,
			Recipe.Product("WALKER_PROD", 1),         // Walker Brain
			new(){
				Recipe.Substance("TECHFRAG", 10000),  // Nanite Cluster
				Recipe.Substance("ROBOT1",    1000),  // Pugneum
				Recipe.Substance("SOULFRAG",   100),  // Fragmented Qualia
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
		list.Add(Recipe.CreateCooking(
			"UI_COOK_CAKE",                            // Assemble Baked Product
			"UI_COOK_CAKE", 60,                        // cooking recipes have RecipeType == RecipeName
			Recipe.Product("FOOD_SCHIPCOOK", 10),      // Stellar Chip Cookies
			new(){
				Recipe.Product("FOOD_R_GCAKEMIX", 1),  // Proto-Batter
				Recipe.Product("FOOD_SCHIPS",     1),  // Stellar Chips
			},
			Log
		));	
		list.Add(Recipe.CreateCooking(
			"UI_COOK_CAKE", "UI_COOK_CAKE", 60,        // Assemble Baked Product
			Recipe.Product("FOOD_SCHIPCOOKA", 10),     // Activated Stellar Chip Cookies
			new(){
				Recipe.Product("FOOD_R_BCAKEMIX", 1),  // Thick, Sweet Batter
				Recipe.Product("FOOD_SCHIPSA",    1),  // Activated Stellar Chips
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
		var paths = new [] {
			"METADATA/GAMESTATE/DEFAULTSAVEDATA.MBIN",
			"METADATA/GAMESTATE/DEFAULTSAVEDATACREATIVE.MBIN"
		};		
		foreach( var path in paths ) {
			var mbin = ExtractMbin<GcDefaultSaveData>(path);
			RECIPES.ForEach(RECIPE => mbin.State.KnownRefinerRecipes.AddUnique(RECIPE.Id));
		}
	}
}

//=============================================================================
