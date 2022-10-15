//=============================================================================
// Author: Jackty89
//=============================================================================

public class FuelEconomy : cmk.NMS.Script.ModClass
{
    readonly string Tritium    = "ROCKETSUB";
    readonly string Deutrium   = "LAUNCHSUB2";
    readonly string DiHydrogen = "LAUNCHSUB";

    readonly string[] RecipeTypes    = {"RECIPE_3INPUT_LAUNCHFUEL_1", "RECIPE_GAS1_LAND"};
    readonly string[] FrigateFuelIds = {"FRIGATE_FUEL_1", "FRIGATE_FUEL_2", "FRIGATE_FUEL_3"};

    GcInventoryType Product   = new GcInventoryType { InventoryType = InventoryTypeEnum.Product };
    GcInventoryType Substance = new GcInventoryType { InventoryType = InventoryTypeEnum.Substance };

    

    protected override void Execute()
    {
        List<GcRefinerRecipe> newRecipes;
        List<GcRefinerRecipe> existingRecipes;

        newRecipes      = FillNewRecipeList();
        existingRecipes = FillExistingRecipesList();

        GcDefaultSaveData(newRecipes);
        GcRecipeTable(newRecipes);
        GcEditRecipeTable(existingRecipes);
    }

    
    protected List<GcRefinerRecipe> FillNewRecipeList()
    {
        List<GcRefinerRecipe> newRecipes = new List<GcRefinerRecipe>() {
            new GcRefinerRecipe{
                Id          = "Trit_D_H",
                Ingredients = new(){
                    new GcRefinerRecipeElement { Id = Deutrium,   Type = Substance, Amount = 1},
                    new GcRefinerRecipeElement { Id = DiHydrogen, Type = Substance, Amount = 1}
                },
                Cooking     = false,
                RecipeName  = RecipeTypes[0],
                RecipeType  = RecipeTypes[0],
                Result      = new GcRefinerRecipeElement { Id = Tritium, Type = Substance, Amount = 1},
                TimeToMake  = 5
            },
            new GcRefinerRecipe{
                Id          = "Trit_3H",
                Ingredients = new(){
                    new GcRefinerRecipeElement { Id = DiHydrogen, Type = Substance, Amount = 1},
                    new GcRefinerRecipeElement { Id = DiHydrogen, Type = Substance, Amount = 1},
                    new GcRefinerRecipeElement { Id = DiHydrogen, Type = Substance, Amount = 1},
                },
                Cooking     = false,
                RecipeName  = RecipeTypes[0],
                RecipeType  = RecipeTypes[0],
                Result      = new GcRefinerRecipeElement { Id = Tritium, Type = Substance, Amount = 1},
                TimeToMake  = 5
            },
            new GcRefinerRecipe{
                Id          = "Trit_2D",
                Ingredients = new(){
                    new GcRefinerRecipeElement { Id = Deutrium, Type = Substance, Amount = 1},
                    new GcRefinerRecipeElement { Id = Deutrium, Type = Substance, Amount = 1},
                },
                Cooking     = false,
                RecipeName  = RecipeTypes[0],
                RecipeType  = RecipeTypes[0],
                Result      = new GcRefinerRecipeElement { Id = Tritium, Type = Substance, Amount = 2},
                TimeToMake  = 5
            },
            new GcRefinerRecipe{
                Id          = "Trit_2H_T",
                Ingredients = new(){
                    new GcRefinerRecipeElement { Id = Tritium,    Type = Substance, Amount = 1},
                    new GcRefinerRecipeElement { Id = DiHydrogen, Type = Substance, Amount = 1},
                    new GcRefinerRecipeElement { Id = DiHydrogen, Type = Substance, Amount = 1}
                },
                Cooking = false,
                RecipeName  = RecipeTypes[0],
                RecipeType  = RecipeTypes[0],
                Result      = new GcRefinerRecipeElement { Id = Tritium, Type = Substance, Amount = 2},
                TimeToMake  = 5
            },
            new GcRefinerRecipe{
                Id          = "Trit_3D",
                Ingredients = new(){
                    new GcRefinerRecipeElement { Id = Deutrium, Type = Substance, Amount = 1},
                    new GcRefinerRecipeElement { Id = Deutrium, Type = Substance, Amount = 1},
                    new GcRefinerRecipeElement { Id = Deutrium, Type = Substance, Amount = 1}
                },
                Cooking = false,
                RecipeName  = RecipeTypes[0],
                RecipeType  = RecipeTypes[0],
                Result      = new GcRefinerRecipeElement { Id = Tritium, Type = Substance, Amount = 3},
                TimeToMake  = 5
            },
            new GcRefinerRecipe{
                Id          = "Deut_T_H",
                Ingredients = new(){
                    new GcRefinerRecipeElement { Id = Tritium,    Type = Substance, Amount = 1},
                    new GcRefinerRecipeElement { Id = DiHydrogen, Type = Substance, Amount = 1}
                },
                Cooking     = false,
                RecipeName  = RecipeTypes[1],
                RecipeType  = RecipeTypes[1],
                Result      = new GcRefinerRecipeElement { Id = Deutrium, Type = Substance, Amount = 3},
                TimeToMake  = 5
            },
            new GcRefinerRecipe{
                Id = "DeutRecycling",
                Ingredients = new(){
                    new GcRefinerRecipeElement { Id = Deutrium, Type = Substance, Amount = 1}
                },
                Cooking     = false,
                RecipeName  = RecipeTypes[1],
                RecipeType  = RecipeTypes[1],
                Result      = new GcRefinerRecipeElement { Id = DiHydrogen, Type = Substance, Amount = 2},
                TimeToMake  = 5
            }
        };
        return newRecipes;
    }

    protected List<GcRefinerRecipe> FillExistingRecipesList()
    {
        List<GcRefinerRecipe> existingRecipes = new List<GcRefinerRecipe>() {
            new GcRefinerRecipe{
                Id          = "REFINERECIPE_56",
                Ingredients = new(){
                    new GcRefinerRecipeElement { Id = Tritium, Type = Substance, Amount = 1},
                },
                Cooking     = false,
                RecipeName  = RecipeTypes[0],
                RecipeType  = RecipeTypes[0],
                Result      = new GcRefinerRecipeElement { Id = DiHydrogen, Type = Substance, Amount = 3},
                TimeToMake  = 7
            },
            new GcRefinerRecipe{
                Id          = "REFINERECIPE_92",
                Ingredients = new(){
                    new GcRefinerRecipeElement { Id = DiHydrogen, Type = Substance, Amount = 1},
                    new GcRefinerRecipeElement { Id = DiHydrogen, Type = Substance, Amount = 1}
                },
                Cooking     = false,
                RecipeName  = RecipeTypes[0],
                RecipeType  = RecipeTypes[0],
                Result      = new GcRefinerRecipeElement { Id = Deutrium, Type = Substance, Amount = 1},
                TimeToMake  = 5
            },
        };
        return existingRecipes;
    }

    protected void GcRecipeTable( List<GcRefinerRecipe> Recipes )
    {
        if( Recipes.IsNullOrEmpty() ) return;
        var mbin = ExtractMbin<GcRecipeTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCRECIPETABLE.MBIN");
        foreach( var recipe in Recipes ) {
            mbin.Table.Add(recipe);
        }
    }

    protected void GcEditRecipeTable( List<GcRefinerRecipe> Recipes )
    {
        if( Recipes.IsNullOrEmpty() ) return;
        var mbin = ExtractMbin<GcRecipeTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCRECIPETABLE.MBIN");
        foreach( var recipe in Recipes ) {
            var existingRecipe = mbin.Table.Find(RECIPE => RECIPE.Id.Value == recipe.Id.Value);
            existingRecipe.Ingredients.Clear();
            existingRecipe.Result = recipe.Result;
            foreach( var ingredient in recipe.Ingredients ) {
                existingRecipe.Ingredients.Add(ingredient);
            }
        }
    }

    protected void GcDefaultSaveData( List<GcRefinerRecipe> Recipes )
    {
        if( Recipes.IsNullOrEmpty() ) return;

        var mbin = ExtractMbin<GcDefaultSaveData>("METADATA/GAMESTATE/DEFAULTSAVEDATA.MBIN");
        Recipes.ForEach(RECIPE => mbin.State.KnownRefinerRecipes.AddUnique(RECIPE.Id));
    }
}