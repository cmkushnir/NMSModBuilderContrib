//=============================================================================

public static partial class _x_
{
	/// <summary>
	/// Call VISTOR for NODE and all it's Children recursively.
	/// If VISITOR returns false then stops iterating through NODE.Children.
	/// </summary>
	public static bool Visit (
		this  TkSceneNodeData  NODE,
		Func <TkSceneNodeData, Stack<TkSceneNodeData>, bool> VISITOR
	){
		if( NODE == null || VISITOR == null ) return false;

		var parents = new Stack<TkSceneNodeData>();
		if( !VISITOR(NODE, parents) ) return false;
		
		foreach( var child in NODE.Children ) {
			parents.Push(NODE);
			var result = Visit(child, parents, VISITOR);
			parents.Pop();
			if( !result ) break;
		}
		
		return true;
	}
	
	//...........................................................
	
	/// <summary>
	/// Call VISTOR for NODE and all it's Children recursively.
	/// If VISITOR returns false then stops iterating through NODE.Children.
	/// Usually call other Visit, which creates a parents stack and calls this method.
	/// </summary>
	public static bool Visit (
		this  TkSceneNodeData  NODE,
		Stack<TkSceneNodeData> PARENTS,  // can be null if don't care
		Func <TkSceneNodeData, Stack<TkSceneNodeData>, bool> VISITOR
	){
		if( NODE == null || VISITOR == null || !VISITOR(NODE, PARENTS) ) return false;
		
		foreach( var child in NODE.Children ) {
			if( PARENTS != null ) PARENTS.Push(NODE);
			var result = Visit(child, PARENTS, VISITOR);  // recurse
			if( PARENTS != null ) PARENTS.Pop();
			if( !result ) break;
		}
		
		return true;
	}
}

//=============================================================================
