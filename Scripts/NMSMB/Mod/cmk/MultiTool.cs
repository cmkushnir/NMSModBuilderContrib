//=============================================================================
// Increase selection of multitools per station|planet.
//=============================================================================

public class MultiTool : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		Try(() => IncreaseSelection());
	}

	//...........................................................

	protected void IncreaseSelection( )
	{
		var mbin = ExtractMbin<GcSimulationGlobals>(
			"GCSIMULATIONGLOBALS.GLOBAL.MBIN"
		);

		var normal = mbin.MultitoolPool[0];
		var royal  = mbin.MultitoolPool[1];

		normal.MinDraw = 4;  // 2
		normal.MaxDraw = 8;  // 4

		royal.MinDraw = 2;  // 1
		royal.MaxDraw = 4;  // 1
		royal.PoolIsSecret = false;  // true
	}
}

//=============================================================================
