//=============================================================================

public static partial class _x_  // any extension methods
{
	public static GcUnlockableItemTreeNode FindUnlockable( this GcUnlockableItemTree TREE, string UNLOCKABLE )
	=> TREE?.Root?.FindUnlockable(UNLOCKABLE);
}


//=============================================================================
