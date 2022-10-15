//=============================================================================
// Author: Jackty89
//=============================================================================

public class MaxUpgradeFreighterSlotAllClasses : cmk.NMS.Script.ModClass
{
	public bool MaxSlot			 = false;
	
	protected GcInventoryLayoutSizeType.SizeTypeEnum[] FreighterSizes = new[] {
		SizeTypeEnum.FreighterSmall,
		SizeTypeEnum.FreighterMedium,
		SizeTypeEnum.FreighterLarge,
	};
	
	protected override void Execute()
	{
		FreighterInventoryEdit();
	}

	//...........................................................

	protected void FreighterInventoryEdit()
	{
		var mbin = ExtractMbin<GcInventoryTable>("METADATA/REALITY/TABLES/INVENTORYTABLE.MBIN");
		for (int i = 0; i < 4; i++)
		{
			mbin.ShipInventoryMaxUpgradeSize[(int)ShipClassEnum.Freighter].MaxInventoryCapacity[i]     = 120;
			mbin.ShipInventoryMaxUpgradeSize[(int)ShipClassEnum.Freighter].MaxTechInventoryCapacity[i] = 60;
		}
		ImproveShipOrFreighterGrid(FreighterSizes);
	}
		
	protected void ImproveShipOrFreighterGrid(GcInventoryLayoutSizeType.SizeTypeEnum[] sizes)
	{
        var invTable = ExtractMbin<GcInventoryTable>("METADATA/REALITY/TABLES/INVENTORYTABLE.MBIN");
		foreach( var vehicleSize in sizes ) 
		{
        	if(MaxSlot)
        	{
	        	invTable.GenerationData.GenerationDataPerSizeType[(int)vehicleSize].MinSlots     = 120;
	        	invTable.GenerationData.GenerationDataPerSizeType[(int)vehicleSize].MaxSlots     = 120;
	        
	        	invTable.GenerationData.GenerationDataPerSizeType[(int)vehicleSize].MinTechSlots = 60;
	        	invTable.GenerationData.GenerationDataPerSizeType[(int)vehicleSize].MaxTechSlots = 60;
			}
        	
	        invTable.GenerationData.GenerationDataPerSizeType[(int)vehicleSize].Bounds.MaxWidthSmall     = 10; //7
	    	invTable.GenerationData.GenerationDataPerSizeType[(int)vehicleSize].Bounds.MaxHeightSmall    = 12; //5
	    	invTable.GenerationData.GenerationDataPerSizeType[(int)vehicleSize].Bounds.MaxWidthStandard  = 10; //10 
	    	invTable.GenerationData.GenerationDataPerSizeType[(int)vehicleSize].Bounds.MaxHeightStandard = 12; //5
	    	invTable.GenerationData.GenerationDataPerSizeType[(int)vehicleSize].Bounds.MaxWidthLarge     = 10; //10
	    	invTable.GenerationData.GenerationDataPerSizeType[(int)vehicleSize].Bounds.MaxHeightLarge    = 12; // 12
	    	
	    	invTable.GenerationData.GenerationDataPerSizeType[(int)vehicleSize].TechBounds.MaxWidthSmall     = 10; //6
	    	invTable.GenerationData.GenerationDataPerSizeType[(int)vehicleSize].TechBounds.MaxHeightSmall    = 6; //3
	    	invTable.GenerationData.GenerationDataPerSizeType[(int)vehicleSize].TechBounds.MaxWidthStandard  = 10; //10
	    	invTable.GenerationData.GenerationDataPerSizeType[(int)vehicleSize].TechBounds.MaxHeightStandard = 6; //3
	    	invTable.GenerationData.GenerationDataPerSizeType[(int)vehicleSize].TechBounds.MaxWidthLarge     = 10; //10
	    	invTable.GenerationData.GenerationDataPerSizeType[(int)vehicleSize].TechBounds.MaxHeightLarge    = 6; //6	        
		}
	}
}

//=============================================================================
