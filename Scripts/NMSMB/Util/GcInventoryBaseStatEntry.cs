//=============================================================================

public partial class InventoryBaseStat
{
	public static GcInventoryBaseStatEntry Create( string ID, float VALUE = 1 )
	{
		return new(){
			BaseStatID = ID,
			Value      = VALUE
		};
	}
}

//=============================================================================
