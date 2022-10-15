//=============================================================================

public partial class InventoryBaseStat
{
	public static GcInventoryBaseStatEntry Create( string ID, float VALUE = 1 )
	=> new() {
		BaseStatID = ID,
		Value      = VALUE
	};
}

//=============================================================================
