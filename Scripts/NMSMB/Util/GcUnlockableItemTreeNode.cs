//=============================================================================

public static partial class _x_  // any extension methods
{
	public static GcUnlockableItemTreeNode FindUnlockable(
		this GcUnlockableItemTreeNode NODE,
		string UNLOCKABLE
	){
		if( NODE == null || UNLOCKABLE.IsNullOrEmpty() ) return null;
		if( NODE.Unlockable == UNLOCKABLE ) return NODE;

		if( NODE.Children.IsNullOrEmpty() ) return null;
		foreach( var child in NODE.Children ) {
			var found  = FindUnlockable(child, UNLOCKABLE);
			if( found != null ) return found;
		}

		return null;
	}
}

//=============================================================================
