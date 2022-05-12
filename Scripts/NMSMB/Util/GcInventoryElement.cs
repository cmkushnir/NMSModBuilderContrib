//=============================================================================

public partial class Inventory
{
	protected static GcInventoryElement Element(
		InventoryTypeEnum TYPE,
		int X, int Y, string ID,
		int AMOUNT, int MAX_AMOUNT,
		float DAMAGE_FACTOR = 0
	){
		return new(){
			Type           = new(){ InventoryType = TYPE },
			Index          = new(){ X = X, Y = Y },
			FullyInstalled = true,
			Id             = ID,
			Amount         = AMOUNT,
			MaxAmount      = MAX_AMOUNT,
			DamageFactor   = DAMAGE_FACTOR,
		};
	}

	//...........................................................

	public static GcInventoryElement Substance(
		string ID,
		int AMOUNT, int MAX_AMOUNT,
		float DAMAGE_FACTOR = 0
	){
		return Element(
			InventoryTypeEnum.Substance,
			-1, -1, ID, AMOUNT, MAX_AMOUNT, DAMAGE_FACTOR
		);
	}
	
	public static GcInventoryElement Substance(
		int X, int Y, string ID,
		int AMOUNT, int MAX_AMOUNT,
		float DAMAGE_FACTOR = 0
	){
		return Element(
			InventoryTypeEnum.Substance,
			X, Y, ID, AMOUNT, MAX_AMOUNT, DAMAGE_FACTOR
		);
	}

	//...........................................................

	public static GcInventoryElement Product(
		string ID,
		int AMOUNT, int MAX_AMOUNT,
		float DAMAGE_FACTOR = 0
	){
		return Element(
			InventoryTypeEnum.Product,
			-1, -1, ID, AMOUNT, MAX_AMOUNT, DAMAGE_FACTOR
		);
	}
	
	public static GcInventoryElement Product(
		int X, int Y, string ID,
		int AMOUNT, int MAX_AMOUNT,
		float DAMAGE_FACTOR = 0
	){
		return Element(
			InventoryTypeEnum.Product,
			X, Y, ID, AMOUNT, MAX_AMOUNT, DAMAGE_FACTOR
		);
	}

	//...........................................................

	public static GcInventoryElement Technology(
		string ID,
		int AMOUNT, int MAX_AMOUNT,
		float DAMAGE_FACTOR = 0
	){
		return Element(
			InventoryTypeEnum.Technology,
			-1, -1, ID, AMOUNT, MAX_AMOUNT, DAMAGE_FACTOR
		);
	}
	
	public static GcInventoryElement Technology(
		int X, int Y, string ID,
		int AMOUNT, int MAX_AMOUNT,
		float DAMAGE_FACTOR = 0
	){
		return Element(
			InventoryTypeEnum.Technology,
			X, Y, ID, AMOUNT, MAX_AMOUNT, DAMAGE_FACTOR
		);
	}
}

//=============================================================================

public class GcInventoryElementIndexEqualityComparer
: EqualityComparer<GcInventoryElement>
{
	protected static GcInventoryIndexEqualityComparer s_index_equality_comparer = new();
	
    public override bool Equals( GcInventoryElement LHS, GcInventoryElement RHS )
	{
		return s_index_equality_comparer.Equals(LHS?.Index, RHS?.Index);
	}			
	public override int GetHashCode( GcInventoryElement ELEMENT )
	{
		return s_index_equality_comparer.GetHashCode(ELEMENT?.Index);
	}
}

//=============================================================================

public class GcInventoryElementIndexComparer
: Comparer<GcInventoryElement>
{
	protected static GcInventoryIndexComparer s_index_comparer = new();
	
    public override int Compare( GcInventoryElement LHS, GcInventoryElement RHS )
	{
		return s_index_comparer.Compare(LHS?.Index, RHS?.Index);
	}			
}

//=============================================================================
