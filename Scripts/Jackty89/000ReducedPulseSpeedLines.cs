//=============================================================================

public class ReducedPulseSpeedLines : cmk.NMS.Script.ModClass
{
	private static string StringBase = "MODELS/EFFECTS/SPEEDLINES/";
	
	protected override void Execute()
	{
		Tuple<string, int, float, float, float, float, float>[] speedLineData = new Tuple<string, int, float, float, float, float, float>[] 
		{
			new ("MINIJUMPSPEEDLINES.SPEEDLINE.MBIN",  600,  1100f, 1700f, 10f,  0.5f, 200f),
			new ("MINIJUMPSPEEDLINES2.SPEEDLINE.MBIN", 800,  2000f, 1300f, 25f,  0.4f, 250f),
			new ("SPACE.SPEEDLINE.MBIN",               1000, 400f,  6f,    1f,   0.4f, 5f),
			new ("SPACE2.SPEEDLINE.MBIN",              800,  220f,  3.5f,  0.4f, 0.5f, 10f),
			new ("SPACEBIG.SPEEDLINE.MBIN",            500,  300f,  4f,    4f,   0.5f, 15f)
	    };

		foreach(var sourcFile in speedLineData)
		{
			ReduceSpeedLines(sourcFile);
		}
		
	}
	
	protected void ReduceSpeedLines(Tuple<string, int, float, float, float, float, float> sourceData)
	{
		
		string sourceFile          = StringBase + sourceData.Item1;		
		int particles              = sourceData.Item2;
		float radius               = sourceData.Item3;
		float length               = sourceData.Item4;
		float width                = sourceData.Item5;
		float alpha                = sourceData.Item6;
		float removeCylinderRadius = sourceData.Item7;
		
		var mbin = ExtractMbin<TkSpeedLineData>(sourceFile);
		
		mbin.NumberOfParticles    = particles;
        mbin.Radius               = radius;
        mbin.Length               = length;
        mbin.Width                = width;
        mbin.Alpha                = alpha;
        mbin.RemoveCylinderRadius = removeCylinderRadius;
	}

	//...........................................................
}

//=============================================================================
