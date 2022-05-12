//=============================================================================

public class GcInventoryIndexEqualityComparer
: EqualityComparer<GcInventoryIndex>
{
    public override bool Equals( GcInventoryIndex LHS, GcInventoryIndex RHS )
	{
		return Object.ReferenceEquals(LHS, RHS) || (
			LHS  !=  null  && RHS   != null &&
			LHS.X == RHS.X && LHS.Y == RHS.Y
		);
	}		    
	public override int GetHashCode( GcInventoryIndex INDEX )
	{
		return INDEX == null ? 0 : INDEX.X << 16 & INDEX.Y;
	}
}

//=============================================================================

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
