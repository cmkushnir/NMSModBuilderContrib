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
		INVENTORY?.Slots?.Sort(new GcInventoryElementIndexComparer());
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
