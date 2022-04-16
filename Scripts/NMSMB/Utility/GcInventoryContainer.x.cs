//=============================================================================
// Extension methods to add|set items in inventory slots.
// See: mod scripts Inventory_ExoSuit, Inventory_MultiTool, Inventory_Ship.
//=============================================================================

public class GcInventoryIndexEqualityComparer
: EqualityComparer<GcInventoryIndex>
{
    public override bool Equals( GcInventoryIndex LHS, GcInventoryIndex RHS )
	{
		return
			LHS  !=  null  && RHS   != null &&
			LHS.X == RHS.X && LHS.Y == RHS.Y
		;
	}		    
	public override int GetHashCode( GcInventoryIndex INDEX )
	{
		return INDEX == null ? 0 : INDEX.X << 16 & INDEX.Y;
	}
}

public class GcInventoryIndexComparer
: Comparer<GcInventoryIndex>
{
    public override int Compare( GcInventoryIndex LHS, GcInventoryIndex RHS )
	{
    	if( Object.ReferenceEquals(LHS, RHS) ) return 0;
		if( LHS == null )   return -1;
		if( RHS == null )   return  1;
		if( LHS.X > RHS.X ) return  1;
		if( LHS.X < RHS.X ) return -1;
		if( LHS.Y > RHS.Y ) return  1;
		if( LHS.Y < RHS.Y ) return -1;
		return 0;
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

public static partial class _x_
{
	/// <summary>
	/// Sort INVENTORY.Slots and INVENTORY.ValidSlotIndices by Index.
	/// Call after finished Add's and Set's.
	/// </summary>
	public static void Sort(
		this GcInventoryContainer INVENTORY
	){		
		INVENTORY?.Slots?           .Sort(new GcInventoryElementIndexComparer());
		INVENTORY?.ValidSlotIndices?.Sort(new GcInventoryIndexComparer());
	}

	//...........................................................

	/// <summary>
	/// ELEMENT.Index is set to (-1, -1).
	/// </summary>
	public static GcInventoryElement Add(
		this GcInventoryContainer INVENTORY,
		     GcInventoryElement   ELEMENT
	){
		ELEMENT.Index.X = -1;
		ELEMENT.Index.Y = -1;
		INVENTORY.Slots.Add(ELEMENT);
		return ELEMENT;
	}

	//...........................................................

	/// <summary>
	/// Set the contents of a specific slot, overwrite any existing.
	/// ELEMENT.Index must be valid else no-op and returns null.
	/// </summary>
	public static GcInventoryElement Set(
		this GcInventoryContainer INVENTORY,
		     GcInventoryElement   ELEMENT
	){
		var index = ELEMENT.Index;
		if( index.X < 0 || index.Y < 0 ) return null;

		var index_equality_comparer = new GcInventoryIndexEqualityComparer();		
		
		// is slot already used ? overwrite existing else add
		var existing = INVENTORY.Slots.FindIndex(
			ELEMENT => index_equality_comparer.Equals(index, ELEMENT.Index)
		);
		if( existing < 0 ) INVENTORY.Slots.Add(ELEMENT);
		else               INVENTORY.Slots[existing] = ELEMENT;

		// mark slot as valid (not sure this is needed)
		existing = INVENTORY.ValidSlotIndices.FindIndex(
			INDEX => index_equality_comparer.Equals(index, INDEX)
		);
		if( existing < 0 ) INVENTORY.ValidSlotIndices.Add(index);

		return ELEMENT;
	}
}

//=============================================================================
