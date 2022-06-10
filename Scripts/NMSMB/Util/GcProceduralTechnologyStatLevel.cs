//=============================================================================

public class ProceduralTechnologyStatLevel
{
	public static GcProceduralTechnologyStatLevel Create(
		StatsTypeEnum      TYPE,
		float              MIN           = 0,
		float              MAX           = 1,
		WeightingCurveEnum CURVE         = WeightingCurveEnum.MaxIsUncommon,
		bool               ALWAYS_CHOOSE = false	
	){
		return new(){
			Stat           = new(){ StatsType = TYPE },
			ValueMin       = MIN,
			ValueMax       = MAX,
			WeightingCurve = new(){ WeightingCurve = CURVE },
			AlwaysChoose   = ALWAYS_CHOOSE
		};
	}
}

//=============================================================================

public static partial class _x_
{
	public static GcProceduralTechnologyStatLevel Add(
		this List<GcProceduralTechnologyStatLevel> LIST,
		StatsTypeEnum      TYPE,
		float              MIN           = 0,
		float              MAX           = 1,
		WeightingCurveEnum CURVE         = WeightingCurveEnum.MaxIsUncommon,
		bool               ALWAYS_CHOOSE = false	
	){
		var obj = ProceduralTechnologyStatLevel.Create(
			TYPE, MIN, MAX, CURVE, ALWAYS_CHOOSE
		);
		LIST.Add(obj);
		return obj;
	}
}

//=============================================================================
