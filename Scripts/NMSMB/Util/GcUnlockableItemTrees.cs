//=============================================================================

public static partial class _x_  // any extension methods
{
	public static GcUnlockableItemTreeNode FindUnlockable(
		this GcUnlockableItemTrees TREES,
		string UNLOCKABLE
	){
		if( TREES?.Trees == null || UNLOCKABLE.IsNullOrEmpty() ) return null;

		foreach( var tree in TREES.Trees ) {
			var found  = tree.FindUnlockable(UNLOCKABLE);
			if( found != null ) return found;
		}

		return null;
	}
}

//=============================================================================
