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
		foreach ( var Diff in Enum.GetValues<GcDifficultyConfig.InventoryStackLimitsOptionDataEnum>())
		{
			mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].ProductStackLimit   = SubstanceAndProcductSizeLimit;
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].SubstanceStackLimit = SubstanceAndProcductSizeLimit;
	        
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.Default]           = SubstanceInventorySizeAll;
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.Personal]          = SubstanceInventorySizeAll;
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.PersonalCargo]     = SubstanceCargoSizeAll;
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.Ship]              = SubstanceInventorySizeAll;
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.ShipCargo]         = SubstanceCargoSizeAll;
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.Freighter]         = SubstanceInventorySizeAll;
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.FreighterCargo]    = SubstanceCargoSizeAll;
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.Vehicle]           = SubstanceInventorySizeAll;
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.Chest]             = SubstanceInventorySizeAll;
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.BaseCapsule]       = SubstanceInventorySizeAll;
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.MaintenanceObject] = SubstanceInventorySizeAll;
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].MaxSubstanceStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum.UIPopup]           = SubstanceInventorySizeAll;
	        
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.Default]               = ProductInventorySizeAll;
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.Personal]              = ProductInventorySizeAll;
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.PersonalCargo]         = ProductCargoSizeAll;
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.Ship]                  = ProductInventorySizeAll;
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.ShipCargo]             = ProductCargoSizeAll;
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.Freighter]             = ProductInventorySizeAll;
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.FreighterCargo]        = ProductCargoSizeAll;
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.Vehicle]               = ProductInventorySizeAll;
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.Chest]                 = ProductInventorySizeAll;
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.BaseCapsule]           = ProductInventorySizeAll;
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.MaintenanceObject]     = ProductInventorySizeAll;
	        mbin.DifficultyConfig.InventoryStackLimitsOptionData[(int)Diff].MaxProductStackSizes[(int)GcDifficultyInventoryStackSizeOptionData.MaxProductStackSizesEnum.UIPopup]               = ProductInventorySizeAll;
		}
        
    }
}

//=============================================================================
