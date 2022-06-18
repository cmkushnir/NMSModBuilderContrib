//=============================================================================

public class StatsBonus
{
	public static GcStatsBonus Create(
		StatsTypeEnum TYPE,
		float         BONUS = 0,
		int           LEVEL = 1
	){
		return new(){
			Stat  = new(){ StatsType = TYPE },
			Bonus = BONUS,
			Level = LEVEL,
		};
	}
}

//=============================================================================

public static partial class _x_
{
	public static GcStatsBonus Add(
		this List<GcStatsBonus> LIST,
		StatsTypeEnum           TYPE,
		float                   BONUS = 0,
		int                     LEVEL = 1
	){
		var obj = StatsBonus.Create(
			TYPE, BONUS, LEVEL
		);
		LIST.Add(obj);
		return obj;
	}
}

//=============================================================================
