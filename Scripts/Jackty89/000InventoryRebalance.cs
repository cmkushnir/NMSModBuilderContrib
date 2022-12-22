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
		var restricted_stacks = mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)InventoryStackLimitsDifficultyEnum.Normal];

		restricted_stacks.ProductStackLimit   = substanceAndProcductSizeLimit;
		restricted_stacks.SubstanceStackLimit = substanceAndProcductSizeLimit;

		restricted_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Default]           = SubstanceInventorySizeRestricted;
		restricted_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Personal]          = SubstanceInventorySizeRestricted;
		restricted_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.PersonalCargo]     = SubstanceCargoSizeRestricted;
		restricted_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Ship]              = SubstanceShipInventorySizeRestricted;
		restricted_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.ShipCargo]         = SubstanceShipCargoSizeRestricted;
		restricted_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Freighter]         = SubstanceFreighterInventorySizeRestricted;
		restricted_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.FreighterCargo]    = SubstanceFreighterCargoSizeRestricted;
		restricted_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Vehicle]           = SubstanceVehicleInventorySizeRestricted;
		restricted_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Chest]             = SubstanceChestAndCapSizeRestricted;
		restricted_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.BaseCapsule]       = SubstanceChestAndCapSizeRestricted;
		restricted_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.MaintenanceObject] = SubstanceInventorySizeRestricted;
		restricted_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.UIPopup]           = SubstanceInventorySizeRestricted;
		
		restricted_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Default]               = ProductInventorySizeRestricted;
		restricted_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Personal]              = ProductInventorySizeRestricted;
		restricted_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.PersonalCargo]         = ProductCargoSizeRestricted;
		restricted_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Ship]                  = ProductShipInventorySizeRestricted;
		restricted_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.ShipCargo]             = ProductShipCargoSizeRestricted;
		restricted_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Freighter]             = ProductFreighterInventorySizeRestricted;
		restricted_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.FreighterCargo]        = ProductFreighterCargoSizeRestricted;
		restricted_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Vehicle]               = ProductVehicleInventorySizeRestricted;
		restricted_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Chest]                 = ProductChestAndCapSizeRestricted;
		restricted_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.BaseCapsule]           = ProductChestAndCapSizeRestricted;
		restricted_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.MaintenanceObject]     = ProductInventorySizeRestricted;
		restricted_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.UIPopup]               = ProductInventorySizeRestricted;
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
		var standard_stacks = mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)InventoryStackLimitsDifficultyEnum.High];

		standard_stacks.ProductStackLimit   = substanceAndProcductSizeLimit;
		standard_stacks.SubstanceStackLimit = substanceAndProcductSizeLimit;
		
		standard_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Default]           = SubstanceInventorySizeStandard;
		standard_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Personal]          = SubstanceInventorySizeStandard;
		standard_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.PersonalCargo]     = SubstanceCargoSizeStandard;
		standard_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Ship]              = SubstanceInventorySizeStandard;
		standard_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.ShipCargo]         = SubstanceCargoSizeStandard;
		standard_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Freighter]         = SubstanceInventorySizeStandard;
		standard_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.FreighterCargo]    = SubstanceCargoSizeStandard;
		standard_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Vehicle]           = SubstanceInventorySizeStandard;
		standard_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Chest]             = SubstanceInventorySizeStandard;
		standard_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.BaseCapsule]       = SubstanceInventorySizeStandard;
		standard_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.MaintenanceObject] = SubstanceInventorySizeStandard;
		standard_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.UIPopup]           = SubstanceInventorySizeStandard;
		
		standard_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Default]               = ProductInventorySizeStandard;
		standard_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Personal]              = ProductInventorySizeStandard;
		standard_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.PersonalCargo]         = ProductCargoSizeStandard;
		standard_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Ship]                  = ProductInventorySizeStandard;
		standard_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.ShipCargo]             = ProductCargoSizeStandard;
		standard_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Freighter]             = ProductInventorySizeStandard;
		standard_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.FreighterCargo]        = ProductCargoSizeStandard;
		standard_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Vehicle]               = ProductInventorySizeStandard;
		standard_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Chest]                 = ProductInventorySizeStandard;
		standard_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.BaseCapsule]           = ProductInventorySizeStandard;
		standard_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.MaintenanceObject]     = ProductInventorySizeStandard;
		standard_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.UIPopup]               = ProductInventorySizeStandard;
	}
}

//=============================================================================
