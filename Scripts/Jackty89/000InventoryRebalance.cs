//=============================================================================
// Author: Jackty89
//=============================================================================

public class InventoryRebalance : cmk.NMS.Script.ModClass
{
    protected override void Execute()
    {
        int substanceAndProcductSizeLimit = 9999999;
        EditRestrictedOptions(substanceAndProcductSizeLimit);
        EditStandardOptions(substanceAndProcductSizeLimit);
        //EditGrid();
    }

    //...........................................................
	protected void EditGrid()
    {
        var invTable = ExtractMbin<GcInventoryTable>("METADATA/REALITY/TABLES/INVENTORYTABLE.MBIN");
        //invTable.GenerationData.GenerationDataPerSizeType[(int)SizeTypeEnum.Suit].MinSlots = 20;
        //invTable.GenerationData.GenerationDataPerSizeType[(int)SizeTypeEnum.Suit].MaxSlots = 20;
        
        //invTable.GenerationData.GenerationDataPerSizeType[(int)SizeTypeEnum.Suit].Bounds.MaxWidthSmall     = 10; //7
    	//invTable.GenerationData.GenerationDataPerSizeType[(int)SizeTypeEnum.Suit].Bounds.MaxHeightSmall    = 12; //5
    	//invTable.GenerationData.GenerationDataPerSizeType[(int)SizeTypeEnum.Suit].Bounds.MaxWidthStandard  = 10; //10 
    	//invTable.GenerationData.GenerationDataPerSizeType[(int)SizeTypeEnum.Suit].Bounds.MaxHeightStandard = 12; //5
    	invTable.GenerationData.GenerationDataPerSizeType[(int)SizeTypeEnum.Suit].Bounds.MaxWidthLarge     = 10; //10
    	invTable.GenerationData.GenerationDataPerSizeType[(int)SizeTypeEnum.Suit].Bounds.MaxHeightLarge    = 12; // 12
    	
    	//invTable.GenerationData.GenerationDataPerSizeType[(int)SizeTypeEnum.Suit].TechBounds.MaxWidthSmall     = 10; //6
    	//invTable.GenerationData.GenerationDataPerSizeType[(int)SizeTypeEnum.Suit].TechBounds.MaxHeightSmall    = 10; //3
    	//invTable.GenerationData.GenerationDataPerSizeType[(int)SizeTypeEnum.Suit].TechBounds.MaxWidthStandard  = 10; //10
    	//invTable.GenerationData.GenerationDataPerSizeType[(int)SizeTypeEnum.Suit].TechBounds.MaxHeightStandard = 10; //3
    	invTable.GenerationData.GenerationDataPerSizeType[(int)SizeTypeEnum.Suit].TechBounds.MaxWidthLarge     = 10; //10
    	invTable.GenerationData.GenerationDataPerSizeType[(int)SizeTypeEnum.Suit].TechBounds.MaxHeightLarge    = 12; //6
        
	}
    protected void EditRestrictedOptions( int substanceAndProcductSizeLimit )
    {
        // -- Restricted == Survival/Perma
        int SubstanceInventorySizeRestricted          = 500;
        int SubstanceCargoSizeRestricted              = 1000;
        int SubstanceShipInventorySizeRestricted      = 2000;
        int SubstanceShipCargoSizeRestricted          = 2000;
        int SubstanceFreighterInventorySizeRestricted = 5000;
        int SubstanceFreighterCargoSizeRestricted     = 5000;
        int SubstanceVehicleInventorySizeRestricted   = 2000;
        int SubstanceChestAndCapSizeRestricted        = 5000;

        int ProductInventorySizeRestricted           = 5;
        int ProductCargoSizeRestricted               = 10;
        int ProductShipInventorySizeRestricted       = 10;
        int ProductShipCargoSizeRestricted           = 10;
        int ProductFreighterInventorySizeRestricted  = 25;
        int ProductFreighterCargoSizeRestricted      = 25;
        int ProductVehicleInventorySizeRestricted    = 10;
        int ProductChestAndCapSizeRestricted         = 25;

        var mbin = ExtractMbin<GcGameplayGlobals>("GCGAMEPLAYGLOBALS.GLOBAL.MBIN");

        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].ProductStackLimit   = substanceAndProcductSizeLimit;
        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].SubstanceStackLimit = substanceAndProcductSizeLimit;
        
        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.Default]           = SubstanceInventorySizeRestricted;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.Personal]          = SubstanceInventorySizeRestricted;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.PersonalCargo]     = SubstanceCargoSizeRestricted;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.Ship]              = SubstanceShipInventorySizeRestricted;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.ShipCargo]         = SubstanceShipCargoSizeRestricted;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.Freighter]         = SubstanceFreighterInventorySizeRestricted;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.FreighterCargo]    = SubstanceFreighterCargoSizeRestricted;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.Vehicle]           = SubstanceVehicleInventorySizeRestricted;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.Chest]             = SubstanceChestAndCapSizeRestricted;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.BaseCapsule]       = SubstanceChestAndCapSizeRestricted;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.MaintenanceObject] = SubstanceInventorySizeRestricted;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.UIPopup]           = SubstanceInventorySizeRestricted;
		
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.Default]               = ProductInventorySizeRestricted;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.Personal]              = ProductInventorySizeRestricted;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.PersonalCargo]         = ProductCargoSizeRestricted;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.Ship]                  = ProductShipInventorySizeRestricted;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.ShipCargo]             = ProductShipCargoSizeRestricted;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.Freighter]             = ProductFreighterInventorySizeRestricted;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.FreighterCargo]        = ProductFreighterCargoSizeRestricted;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.Vehicle]               = ProductVehicleInventorySizeRestricted;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.Chest]                 = ProductChestAndCapSizeRestricted;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.BaseCapsule]           = ProductChestAndCapSizeRestricted;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.MaintenanceObject]     = ProductInventorySizeRestricted;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.Normal].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.UIPopup]               = ProductInventorySizeRestricted;
    }

    //...........................................................

    protected void EditStandardOptions( int substanceAndProcductSizeLimit )
    {
        // -- Standard == NORMAL
        int SubstanceInventorySizeStandard           = 50000;
        int SubstanceCargoSizeStandard               = 100000;
        int ProductInventorySizeStandard             = 50;
        int ProductCargoSizeStandard                 = 100;
        
        var mbin = ExtractMbin<GcGameplayGlobals>("GCGAMEPLAYGLOBALS.GLOBAL.MBIN");

        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].ProductStackLimit   = substanceAndProcductSizeLimit;
        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].SubstanceStackLimit = substanceAndProcductSizeLimit;
        
        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.Default]           = SubstanceInventorySizeStandard;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.Personal]          = SubstanceInventorySizeStandard;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.PersonalCargo]     = SubstanceCargoSizeStandard;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.Ship]              = SubstanceInventorySizeStandard;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.ShipCargo]         = SubstanceCargoSizeStandard;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.Freighter]         = SubstanceInventorySizeStandard;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.FreighterCargo]    = SubstanceCargoSizeStandard;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.Vehicle]           = SubstanceInventorySizeStandard;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.Chest]             = SubstanceInventorySizeStandard;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.BaseCapsule]       = SubstanceInventorySizeStandard;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.MaintenanceObject] = SubstanceInventorySizeStandard;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.UIPopup]           = SubstanceInventorySizeStandard;
		
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.Default]               = ProductInventorySizeStandard;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.Personal]              = ProductInventorySizeStandard;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.PersonalCargo]         = ProductCargoSizeStandard;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.Ship]                  = ProductInventorySizeStandard;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.ShipCargo]             = ProductCargoSizeStandard;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.Freighter]             = ProductInventorySizeStandard;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.FreighterCargo]        = ProductCargoSizeStandard;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.Vehicle]               = ProductInventorySizeStandard;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.Chest]                 = ProductInventorySizeStandard;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.BaseCapsule]           = ProductInventorySizeStandard;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.MaintenanceObject]     = ProductInventorySizeStandard;
		mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)GcDifficultyConfig.InventoryStackLimitsOptionDataEnum.High].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.UIPopup]               = ProductInventorySizeStandard;
    }
}

//=============================================================================
