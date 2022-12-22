//=============================================================================
// Author: Jackty89
//=============================================================================

public class InventoryUnbalance : cmk.NMS.Script.ModClass
{
    protected override void Execute()
    {
        EditAllOptions();
    }

    //...........................................................

    protected void EditAllOptions()
    {
        int SubstanceAndProcductSizeLimit       = 9999999;

        int SubstanceInventorySizeAll           = 50000;
        int SubstanceCargoSizeAll               = 100000;
        int ProductInventorySizeAll             = 2500;
        int ProductCargoSizeAll                 = 5000;
        
        var mbin = ExtractMbin<GcGameplayGlobals>("GCGAMEPLAYGLOBALS.GLOBAL.MBIN");
		foreach ( var Diff in Enum.GetValues<InventoryStackLimitsDifficultyEnum>())
		{
			var edit_stacks = mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff];
			edit_stacks.ProductStackLimit   = SubstanceAndProcductSizeLimit;
	        edit_stacks.SubstanceStackLimit = SubstanceAndProcductSizeLimit;
	        
	        edit_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Default]           = SubstanceInventorySizeAll;
	        edit_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Personal]          = SubstanceInventorySizeAll;
	        edit_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.PersonalCargo]     = SubstanceCargoSizeAll;
	        edit_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Ship]              = SubstanceInventorySizeAll;
	        edit_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.ShipCargo]         = SubstanceCargoSizeAll;
	        edit_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Freighter]         = SubstanceInventorySizeAll;
	        edit_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.FreighterCargo]    = SubstanceCargoSizeAll;
	        edit_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Vehicle]           = SubstanceInventorySizeAll;
	        edit_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Chest]             = SubstanceInventorySizeAll;
	        edit_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.BaseCapsule]       = SubstanceInventorySizeAll;
	        edit_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.MaintenanceObject] = SubstanceInventorySizeAll;
	        edit_stacks.MaxSubstanceStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.UIPopup]           = SubstanceInventorySizeAll;
	        
	        edit_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Default]               = ProductInventorySizeAll;
	        edit_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Personal]              = ProductInventorySizeAll;
	        edit_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.PersonalCargo]         = ProductCargoSizeAll;
	        edit_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Ship]                  = ProductInventorySizeAll;
	        edit_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.ShipCargo]             = ProductCargoSizeAll;
	        edit_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Freighter]             = ProductInventorySizeAll;
	        edit_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.FreighterCargo]        = ProductCargoSizeAll;
	        edit_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Vehicle]               = ProductInventorySizeAll;
	        edit_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.Chest]                 = ProductInventorySizeAll;
	        edit_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.BaseCapsule]           = ProductInventorySizeAll;
	        edit_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.MaintenanceObject]     = ProductInventorySizeAll;
	        edit_stacks.MaxProductStackSizes[(int)GcInventoryStackSizeGroup.InventoryStackSizeGroupEnum.UIPopup]               = ProductInventorySizeAll;
		}
        
    }
}

//=============================================================================
