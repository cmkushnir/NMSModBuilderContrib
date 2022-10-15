//=============================================================================
// Author: Jackty89
//=============================================================================

public class UninstallCoreWeapons : cmk.NMS.Script.ModClass
{
	public bool UninstallExtra = false;
	
	protected override void Execute()
	{
		GcTechnologyTable();
	}

	//...........................................................

	protected void GcTechnologyTable()
	{
		var Mbin = ExtractMbin<GcTechnologyTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCTECHNOLOGYTABLE.MBIN");
		Mbin.Table.Find(PRODUCT => PRODUCT.ID == "SHIPGUN1").Core = false;
		Mbin.Table.Find(PRODUCT => PRODUCT.ID == "LASER")   .Core = false;
		if(UninstallExtra)
		{
			Mbin.Table.Find(PRODUCT => PRODUCT.ID == "SHIPJUMP1").Core = false;
			Mbin.Table.Find(PRODUCT => PRODUCT.ID == "HYPERDRIVE").Core = false;
		}			
	}
}

//=============================================================================
