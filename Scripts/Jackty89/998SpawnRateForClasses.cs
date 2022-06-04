//=============================================================================
// Author: Jackty89
//=============================================================================

public class SpawnRateForClasses : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		GcInventoryTable();
	}

	//...........................................................

	protected void GcInventoryTable()
	{
		var mbin = ExtractMbin<GcInventoryTable>("METADATA/REALITY/TABLES/INVENTORYTABLE.MBIN");
		GcInventoryClassProbabilities(mbin.ClassProbabilityData);
	}
	protected void GcInventoryClassProbabilities( GcInventoryClassProbabilities[] DATA )
	{
		var Cclass                              = (int)InventoryClassEnum.C;
		var Bclass                              = (int)InventoryClassEnum.B;
		var Aclass                              = (int)InventoryClassEnum.A;
		var Sclass                              = (int)InventoryClassEnum.S;

		var wealth_class                        = DATA[(int)WealthClassEnum.Average];
		wealth_class.ClassProbabilities[Cclass] = 40;  // 49
		wealth_class.ClassProbabilities[Bclass] = 35;  // 35
		wealth_class.ClassProbabilities[Aclass] = 23;  // 15
		wealth_class.ClassProbabilities[Sclass] = 2;  //  1

		wealth_class                            = DATA[(int)WealthClassEnum.Wealthy];
		wealth_class.ClassProbabilities[Cclass] = 30;  // 30
		wealth_class.ClassProbabilities[Bclass] = 37;  // 40
		wealth_class.ClassProbabilities[Aclass] = 28;  // 28
		wealth_class.ClassProbabilities[Sclass] = 5;  //  2
	}
}

//=============================================================================
