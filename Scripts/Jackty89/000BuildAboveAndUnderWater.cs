//=============================================================================
// Author: Jackty89
//=============================================================================

public class BuildAboveAndUnderWater : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		BuildIt();

	}
	protected void BuildIt()
	{

		var mbin = ExtractMbin<GcBaseBuildingTable>(
			"METADATA/REALITY/TABLES/BASEBUILDINGOBJECTSTABLE.MBIN"
		);
		
		
		foreach(var builldObject in	mbin.Objects)
		{
			if(builldObject.BuildableOnFreighter == false)
			{
				builldObject.BuildableAboveWater = true;
				builldObject.BuildableUnderwater = true;
			}
		}				
	}
}

//=============================================================================
