//=============================================================================
// Helper methods to create new inventory items.
// See: mod scripts Inventory_ExoSuit, Inventory_MultiTool, Inventory_Ship.
//=============================================================================

public class Inventory
{
	/// <summary>
	/// Create new inventory object.
	/// </summary>
	protected static GcInventoryElement Element(
		InventoryTypeEnum TYPE,
		int X, int Y, string ID,
		int AMOUNT, int MAX_AMOUNT,
		float DAMAGE_FACTOR = 0
	){
		var item = new GcInventoryElement {
			Type           = new() { InventoryType = TYPE },
			Index          = new() { X = X, Y = Y },
			FullyInstalled = true,
			Id             = ID,
			Amount         = AMOUNT,
			MaxAmount      = MAX_AMOUNT,
			DamageFactor   = DAMAGE_FACTOR,
		};
		return item;
	}

	//...........................................................

	/// <summary>
	/// Create new substance inventory object.
	/// </summary>
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
	
	/// <summary>
	/// Create new substance inventory object.
	/// </summary>
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

	/// <summary>
	/// Create new product inventory object.
	/// </summary>
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
	
	/// <summary>
	/// Create new product inventory object.
	/// </summary>
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

	/// <summary>
	/// Create new tech inventory object.
	/// Cannot go in bulk storage e.g. exo-suit cargo, containers, ...
	/// </summary>
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
	
	/// <summary>
	/// Create new tech inventory object.
	/// Cannot go in bulk storage e.g. exo-suit cargo, containers, ...
	/// </summary>
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
