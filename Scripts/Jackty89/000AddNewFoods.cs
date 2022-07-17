//=============================================================================

public class AddNewFoods : cmk.NMS.Script.ModClass
{
 	static AddNewFoods() {}
	private static IDictionary<string, string> RecipeCookingMethod = new Dictionary<string, string>
	{
		{ "Stew",       "UI_COOK_STEW" },
		{ "Drink",      "UI_COOK_DRINK" },
		{ "Eggs",       "UI_COOK_EGGS" },
		{ "Mix",        "UI_COOK_MIX" },
		{ "Cake",       "UI_COOK_CAKE" },
		{ "Ice",        "UI_COOK_ICE" },
		{ "Bait",       "UI_COOK_BAIT" },
		{ "Pie",        "UI_COOK_PIE" },
		{ "Dougnut",    "UI_COOK_DOUGNUT" },
		{ "SmokedMeat", "UI_COOK_SMOKED_MEAT" },
		{ "Meat",       "UI_COOK_MEAT" },
		{ "Veg",        "UI_COOK_VEG" }
	};

	private static IDictionary<string, string> NewFoodConsumeReward = new Dictionary<string, string>
	{
		{ "Stamina", "DE_FOOD_STAMINA" },
		{ "Energy1", "DE_FOOD_ENERGY1" },
		{ "Energy2", "DE_FOOD_ENERGY2" },
		{ "Energy3", "DE_FOOD_ENERGY3" },
		{ "Haz1",    "DE_FOOD_HAZ1" },
		{ "Haz2",    "DE_FOOD_HAZ2" },
		{ "Haz3",    "DE_FOOD_HAZ3" },
		{ "JetPack", "DE_FOOD_JETPACK" },
		{ "Health",  "DE_FOOD_HEALTH" }
	};
	protected class CustomFood
    {
        public string NewFoodID;
        public string RewardID;
        public List<GcRefinerRecipe> Recipes;
        public List<Tuple<LangId, string, string>> CustomLangDescStrings;
		public string Icon;
		public int Price;
    }
    private static List<CustomFood> CustomFoods = new List<CustomFood>()
    {
        new CustomFood()
		{
			NewFoodID = "Tea",
			RewardID  = NewFoodConsumeReward["Health"],
			Recipes   = new List<GcRefinerRecipe>()
			{
				RefinerRecipe.CreateCooking(
					RecipeCookingMethod["Drink"],
					RecipeCookingMethod["Drink"], 
					60,              
					RefinerRecipe.Product("Tea", 10),     // Activated Stellar Chip Cookies
					new(){
						RefinerRecipe.Substance("LAUNCHSUB", 5),  
						RefinerRecipe.Product("NIPNIPBUDS",  1),  // Activated Stellar Chips
					},                    
					null
				),
				RefinerRecipe.CreateCooking(
					RecipeCookingMethod["Drink"],
					RecipeCookingMethod["Drink"],                   //
					60,                                               //recipe time to cook
					RefinerRecipe.Product("Tea", 10),                 // recipe result
					new(){
						RefinerRecipe.Product("FOOD_R_BCAKEMIX", 1),  // Thick, Sweet Batter
						RefinerRecipe.Product("FOOD_SCHIPSA",    1),  // Activated Stellar Chips
					},
					null
				)
			},
			CustomLangDescStrings = new List<Tuple<LangId, string, string>>()
            {
                new(LangId.English, "Tea", "A lively and mouth-drying effect on the tongue. Not bitter, but a clean and refreshing quality. This tea is renowned across the local region, and is made fresh from whatever the recipe is.")
            },
			Icon  = "TEXTURES/UI/FRONTEND/ICONS/COOKINGPRODUCTS/PRODUCT.TEA.DDS",
			Price = 50
			
		}
	};
    
    private static List<string> ProductsForCooking = new List<string>()
	{
    	"CASING",
    	"NIPNIPBUDS"
    };
    private static List<string> SubstancesForCooking = new List<string>()
	{
    	"LAUNCHSUB"
    };

	protected override void Execute()
	{
		foreach(CustomFood food in CustomFoods)
        {
			CreateFoods(food);
		}
		
		foreach(string product in ProductsForCooking)
        {
			MakeProductsIngredient(product);
		}
		
		foreach(string substance in SubstancesForCooking)
        {
			MakeSubstanceIngredient(substance);
		}
	}
	
	protected void MakeProductsIngredient(string product)
	{
		var prod_mbin = ExtractMbin<GcProductTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCPRODUCTTABLE.MBIN");
		prod_mbin.Table.Find(PRODUCT => PRODUCT.Id == product).CookingIngredient = true;

	}
	protected void MakeSubstanceIngredient(string substance)
	{
		var sub_mbin = ExtractMbin<GcSubstanceTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCSUBSTANCETABLE.MBIN");
		sub_mbin.Table.Find(SUBSTANCE => SUBSTANCE.ID == substance).CookingIngredient = true;

	}

	protected void CreateFoods(CustomFood food)
	{
		var prod_mbin = ExtractMbin<GcProductTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCPRODUCTTABLE.MBIN");
		var cons_mbin = ExtractMbin<GcConsumableItemTable>("METADATA/REALITY/TABLES/CONSUMABLEITEMTABLE.MBIN");

		var product = CloneMbin(prod_mbin.Table.Find(PRODUCT => PRODUCT.Id == "FOOD_CM_APPLE"));

		product.Id                    = food.NewFoodID;
		product.Name                  = food.NewFoodID.ToUpper() + "_NAME";
		product.NameLower             = food.NewFoodID.ToUpper() + "_NAME_L";
		product.Description			  = food.NewFoodID.ToUpper() + "_DESC";
		product.Icon.Filename         = food.Icon;
		product.BaseValue             = food.Price;
		product.CraftAmountMultiplier = 1;
		product.EggModifierIngredient = false;		
		prod_mbin.Table.Add(product);

		var consumable      = CloneMbin(cons_mbin.Table.Find(CONSUMABLE => CONSUMABLE.ID == "FOOD_CM_APPLE"));
		consumable.ID       = food.NewFoodID;
		consumable.RewardID = food.RewardID;

		cons_mbin.Table.Add(consumable);
		AddFoodRecipes(food.Recipes);
		AddNewLanguageString(food.NewFoodID, food.CustomLangDescStrings);


	}
	protected void AddFoodRecipes(List<GcRefinerRecipe> recipes)
	{
		if (recipes.IsNullOrEmpty()) return;
		var recipe_mbin = ExtractMbin<GcRecipeTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCRECIPETABLE.MBIN");
		recipes.ForEach(RECIPE => recipe_mbin.Table.AddUnique(RECIPE));

	}
	protected void AddNewLanguageString(string foodId, List<Tuple<LangId, string, string>> languages)
	{
		foreach (var language in languages)
        {
			SetLanguageText(language.Item1, foodId.ToUpper() + "_NAME", language.Item2.ToUpper());
			SetLanguageText(language.Item1, foodId.ToUpper() + "_NAME_L", language.Item2);
			SetLanguageText(language.Item1, foodId.ToUpper() + "_DESC", language.Item3);
		}
	}
	//...........................................................
}

//=============================================================================
