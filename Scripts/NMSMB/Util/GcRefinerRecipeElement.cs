//=============================================================================

public partial class RefinerRecipe
{
	public static GcRefinerRecipeElement Substance( string ID, int AMOUNT )
	=> new() {
		Id     = ID,
		Type   = new() { InventoryType = InventoryTypeEnum.Substance },
		Amount = AMOUNT
	};

	//...........................................................

	public static GcRefinerRecipeElement Product( string ID, int AMOUNT )
	=> new() {
		Id     = ID,
		Type   = new() { InventoryType = InventoryTypeEnum.Product },
		Amount = AMOUNT
	};

	//...........................................................

	public static GcRefinerRecipeElement Technology( string ID, int AMOUNT )
	=> new() {
		Id     = ID,
		Type   = new() { InventoryType = InventoryTypeEnum.Technology },
		Amount = AMOUNT
	};
}

//=============================================================================
