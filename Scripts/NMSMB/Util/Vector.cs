//=============================================================================

public static partial class _x_
{
	public static nms.Vector2f Scale (
		this nms.Vector2f LHS, double SCALE
	){
		return new(
			(float)(LHS.x * SCALE),
			(float)(LHS.y * SCALE)
		);
	}

	//...........................................................
	
	public static nms.Vector3f Scale (
		this nms.Vector3f LHS, double SCALE
	){
		return new(
			(float)(LHS.x * SCALE),
			(float)(LHS.y * SCALE),
			(float)(LHS.z * SCALE)
		);
	}

	//...........................................................
	
	public static nms.Vector4f Scale (
		this nms.Vector4f LHS, double SCALE
	){
		return new(
			(float)(LHS.x * SCALE),
			(float)(LHS.y * SCALE),
			(float)(LHS.z * SCALE),
			(float)(LHS.t * SCALE)
		);
	}
}

//=============================================================================
