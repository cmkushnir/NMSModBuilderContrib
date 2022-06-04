//=============================================================================
// Author: Jackty89
//=============================================================================

public class MaxUpgradeFreighterSlotAllClasses : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		FreighterInventoryEdit();
	}

	//...........................................................

	protected void FreighterInventoryEdit()
	{
		var mbin = ExtractMbin<GcInventoryTable>("METADATA/REALITY/TABLES/INVENTORYTABLE.MBIN");
		for( int i = 0; i < 4; i++ ) {
			mbin.ShipInventoryMaxUpgradeSize[(int)ShipClassEnum.Freighter].MaxInventoryCapacity[i]     = 48;
			mbin.ShipInventoryMaxUpgradeSize[(int)ShipClassEnum.Freighter].MaxTechInventoryCapacity[i] = 21;
		}
	}
}

//=============================================================================
