//=============================================================================
// Extension methods to add substances|products to requirements lists.
// See: mod script Recipe_Products.
//=============================================================================

public static partial class _x_
{
	public static GcTechnologyRequirement AddSubstance(
		this List<GcTechnologyRequirement> LIST,
		string ID, int AMOUNT
	){
		if( LIST == null || ID.IsNullOrEmpty() ) return null;
		var substance = new GcTechnologyRequirement{
			ID            = ID,
			InventoryType = new GcInventoryType{ InventoryType = InventoryTypeEnum.Substance },
			Amount        = AMOUNT
		};
		LIST.Add(substance);
		return substance;
	}

	//...........................................................
	
	public static GcTechnologyRequirement AddProduct(
		this List<GcTechnologyRequirement> LIST,
		string ID, int AMOUNT
	){
		if( LIST == null || ID.IsNullOrEmpty() ) return null;
		var product = new GcTechnologyRequirement{
			ID            = ID,
			InventoryType = new GcInventoryType{ InventoryType = InventoryTypeEnum.Product },
			Amount        = AMOUNT
		};
		LIST.Add(product);
		return product;
	}
}	

//=============================================================================
