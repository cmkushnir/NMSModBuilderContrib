﻿//=============================================================================
// Adjust mineral|gas extractor rates and silo storage.
//=============================================================================

public class Base_Extractor : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcBaseBuildingTable>(
			"METADATA/REALITY/TABLES/BASEBUILDINGOBJECTSTABLE.MBIN"
		);

		// Mineral Extractors mine faster, hold less (use silos)
		var item = mbin.Objects.Find(OBJECT => OBJECT.ID == "U_EXTRACTOR_S");
		item.LinkGridData.DependsOnHotspots = DependsOnHotspotsEnum.None;
		item.LinkGridData.Rate    = 200;  // 100
		item.LinkGridData.Storage = 200;  // 360,000

		// Gas Extractors extract faster, hold less (use silos)
		item = mbin.Objects.Find(OBJECT => OBJECT.ID == "U_GASEXTRACTOR");
		item.LinkGridData.DependsOnHotspots = DependsOnHotspotsEnum.None;
		item.LinkGridData.Rate    = 200;  // 100
		item.LinkGridData.Storage = 200;  // 360,000

		// Silos hold more
		item = mbin.Objects.Find(OBJECT => OBJECT.ID == "U_SILO_S");
		item.LinkGridData.Storage = 4000000;  // 1,440,000
	}
}

//=============================================================================
