//=============================================================================

public class TechnologyRequirement
{
	public static GcTechnologyRequirement Substance( string ID, int AMOUNT )
	{
		return new(){
			ID            = ID,
			InventoryType = new(){ InventoryType = InventoryTypeEnum.Substance },
			Amount        = AMOUNT
		};
	}

	//...........................................................

	public static GcTechnologyRequirement Product( string ID, int AMOUNT )
	{
		return new(){
			ID            = ID,
			InventoryType = new(){ InventoryType = InventoryTypeEnum.Product },
			Amount        = AMOUNT
		};
	}

	//...........................................................

	public static GcTechnologyRequirement Technology( string ID, int AMOUNT )
	{
		return new(){
			ID            = ID,
			InventoryType = new(){ InventoryType = InventoryTypeEnum.Technology },
			Amount        = AMOUNT
		};
	}
}

//=============================================================================

public static partial class _x_
{
	public static GcTechnologyRequirement AddSubstance(
		this List<GcTechnologyRequirement> LIST,
		string ID, int AMOUNT
	){
		if( LIST == null || ID.IsNullOrEmpty() ) return null;
		var substance = TechnologyRequirement.Substance(ID, AMOUNT);
		LIST.Add(substance);
		return substance;
	}

	//...........................................................
	
	public static GcTechnologyRequirement AddProduct(
		this List<GcTechnologyRequirement> LIST,
		string ID, int AMOUNT
	){
		if( LIST == null || ID.IsNullOrEmpty() ) return null;
		var product = TechnologyRequirement.Product(ID, AMOUNT);
		LIST.Add(product);
		return product;
	}

	//...........................................................
	
	public static GcTechnologyRequirement AddTechnology(
		this List<GcTechnologyRequirement> LIST,
		string ID, int AMOUNT
	){
		if( LIST == null || ID.IsNullOrEmpty() ) return null;
		var technology = TechnologyRequirement.Technology(ID, AMOUNT);
		LIST.Add(technology);
		return technology;
	}
}

//=============================================================================
