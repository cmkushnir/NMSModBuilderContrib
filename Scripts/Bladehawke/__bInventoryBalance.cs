//=============================================================================

public class _bInventoryBalance : cmk.NMS.Script.ModClass
{
  protected override void Execute()
  {
    setSurvivalOptions();
	setNormalOptions();
  }

  protected void setSurvivalOptions()
  {
		var mbin = ExtractMbin<GcInventoryStoreBalance>(
			"METADATA/GAMESTATE/DEFAULTINVENTORYBALANCESURVIVAL.MBIN"
		);

    var SubstanceDefaultStackSize = 500; // Stacksize of substances(Original 250)
    var ProductDefaultStackSize = 1;   // Stacksize of products you can craft or buy(Original 1)

    var StackMultiPlierExosuitInventory = 2; // Inventory stack size  500 x 2 = 1000
    var StackMultiPlierExosuitCargo = 4; // CargoInventory 500 x 4 = 2000

    var StackMultiPlierShip = 8;// Ship Inventory 500 x 8 = 4000
    var StackMultiPlierFreighterAndContainer = 20; // Freighter and Container 500 x 20 = 10000

    var ProductStackMultiPlierExosuitInventory = 5;  // Product Stack Size = 5 ExoSuitInventory
    var ProductStackMutiplierExosuitCargoAndShip = 10; // Product Stack Size = 10 ExoSuitCargo and ShipInventory
    var ProductStackMutiplierFreighterAndContainer = 25; // Product Stack Size = 25 Freighter and BaseContainer

    // ExoSuit Tech Slot 8 x 3 = 24
    var TechWidth = 8; // Exosuit Tech slots increase(Original 7) (8 is max)
    var TechHeight = 3; // Exosuit Tech slots increase(Original 2)	(6 is max)
    var Refund = 0.5f; // Refund from base Deconstruct(Original 0.5).This will refund 50 % when deconstructing
    var SubstanceAndProcductStackSizeLimit = 1000000; //(Original 9999)

    
    mbin.DefaultSubstanceMaxAmount = SubstanceDefaultStackSize;
    mbin.DefaultProductMaxAmount = ProductDefaultStackSize;
    mbin.CargoSubstanceStorageMultiplier = StackMultiPlierExosuitCargo;
    mbin.CargoProductStorageMultiplier = ProductStackMutiplierExosuitCargoAndShip;
    mbin.FreighterSubstanceStorageMultiplier = StackMultiPlierFreighterAndContainer;
    mbin.FreighterProductStorageMultiplier = ProductStackMutiplierFreighterAndContainer;
    mbin.ShipSubstanceStorageMultiplier = StackMultiPlierShip;
    mbin.ShipProductStorageMultiplier = ProductStackMutiplierExosuitCargoAndShip;
    mbin.ChestSubstanceStorageMultiplier = StackMultiPlierFreighterAndContainer;
    mbin.ChestProductStorageMultiplier = ProductStackMutiplierFreighterAndContainer;
    mbin.BaseCapsuleSubstanceStorageMultiplier = StackMultiPlierFreighterAndContainer;
    mbin.BaseCapsuleProductStorageMultiplier = ProductStackMutiplierFreighterAndContainer;
    mbin.DefaultSubstanceStorageMultiplier = StackMultiPlierExosuitInventory;
    mbin.DefaultProductStorageMultiplier = ProductStackMultiPlierExosuitInventory;
    mbin.SubstanceMaxAmountLimit = SubstanceAndProcductStackSizeLimit;
    mbin.ProductMaxAmountLimit = SubstanceAndProcductStackSizeLimit;
    mbin.PlayerPersonalInventoryTechWidth = TechWidth;
    mbin.PlayerPersonalInventoryTechHeight = TechHeight;
    mbin.DeconstructRefundPercentage = Refund;
    
  }

	protected void setNormalOptions()
	{
    var mbin = ExtractMbin<GcInventoryStoreBalance>(
      "METADATA/GAMESTATE/DEFAULTINVENTORYBALANCE.MBIN"
    );

    var SubstanceAndProcductStackSizeLimit = 1000000; // (Original 9999)
    var SubstanceDefaultStackSize = 50000; // Stacksize of substances(Original 9999)
    var ProductDefaultStackSize = 50;    // Stacksize of products you can craft or buy(Original 1)
    var StackMultiPlierExosuitAndShip = 1; // 50000 and 50 after mutiplier
    var StackMultiPlierFreighterAndCargo = 2; // 100000 and 100 after mutiplier
    var Refund = 1; // Refund from base Deconstruct(Original 0.5).This will give you a 100 % refund
    // ExoSuit Tech Slot 8 x 6 = 48
    var TechWidth = 8; // Tech slots increase(Original 7)
    var TechHeight = 6; // Tech slots increase(Original 2)

    mbin.DefaultSubstanceMaxAmount = SubstanceDefaultStackSize;
    mbin.DefaultProductMaxAmount = ProductDefaultStackSize;
    mbin.CargoSubstanceStorageMultiplier = StackMultiPlierFreighterAndCargo;
    mbin.CargoProductStorageMultiplier = StackMultiPlierFreighterAndCargo;
    mbin.FreighterSubstanceStorageMultiplier = StackMultiPlierFreighterAndCargo;
    mbin.FreighterProductStorageMultiplier = StackMultiPlierFreighterAndCargo;
    mbin.ShipSubstanceStorageMultiplier = StackMultiPlierExosuitAndShip;
    mbin.ShipProductStorageMultiplier = StackMultiPlierExosuitAndShip;
    mbin.ChestSubstanceStorageMultiplier = StackMultiPlierExosuitAndShip;
    mbin.ChestProductStorageMultiplier = StackMultiPlierExosuitAndShip;
    mbin.BaseCapsuleSubstanceStorageMultiplier = StackMultiPlierFreighterAndCargo;
    mbin.BaseCapsuleProductStorageMultiplier = StackMultiPlierFreighterAndCargo;
    mbin.DefaultSubstanceStorageMultiplier = StackMultiPlierExosuitAndShip;
    mbin.DefaultProductStorageMultiplier = StackMultiPlierExosuitAndShip;
    mbin.SubstanceMaxAmountLimit = SubstanceAndProcductStackSizeLimit;
    mbin.ProductMaxAmountLimit = SubstanceAndProcductStackSizeLimit;
    mbin.PlayerPersonalInventoryTechWidth = TechWidth;
    mbin.PlayerPersonalInventoryTechHeight = TechHeight;
    mbin.DeconstructRefundPercentage = Refund;
  }
}
//...........................................................

//=============================================================================
