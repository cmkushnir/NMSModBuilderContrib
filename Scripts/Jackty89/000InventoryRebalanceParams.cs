//=============================================================================
// Author: Jackty89
//=============================================================================

public class InventoryRebalanceParams : cmk.NMS.Script.ModClass
{
	public int SubstanceDefaultStackSizeNormal = 10000; // Stacksize of substances(Original 9999)
	public int ProductDefaultStackSizeNormal   = 50;  // Stacksize of products you can craft or buy(Original 1)
	public int TechWidthNormal                 = 8; // Tech slots increase(Original 7)
	public int TechHeightNormal                = 3; // Tech slots increase(Original 2)
	public float RefundNormal                  = 1;
	protected override void Execute()
	{
		//EditBothOptions("METADATA/GAMESTATE/DEFAULTINVENTORYBALANCESURVIVAL.MBIN");
		//EditBothOptions("METADATA/GAMESTATE/DEFAULTINVENTORYBALANCE.MBIN");
	}

	//...........................................................

	//protected void EditBothOptions( string filepath )
	//{
	//	int SubstanceAndProcductStackSizeLimit     = 1000000; // (Original 9999)
	//	int StackMultiPlierExosuitAndShipNormal    = 1; // 50000 and 50 after mutiplier
	//	int StackMultiPlierFreighterAndCargoNormal = 2; // 100000 and 100 after mutiplier

	//	var mbin = ExtractMbin<GcInventoryStoreBalance>(filepath);

	//	mbin.DefaultSubstanceMaxAmount             = SubstanceDefaultStackSizeNormal;
	//	mbin.DefaultProductMaxAmount               = ProductDefaultStackSizeNormal;
	//	mbin.CargoSubstanceStorageMultiplier       = StackMultiPlierFreighterAndCargoNormal;
	//	mbin.CargoProductStorageMultiplier         = StackMultiPlierFreighterAndCargoNormal;
	//	mbin.FreighterSubstanceStorageMultiplier   = StackMultiPlierFreighterAndCargoNormal;
	//	mbin.FreighterProductStorageMultiplier     = StackMultiPlierFreighterAndCargoNormal;
	//	mbin.ShipSubstanceStorageMultiplier        = StackMultiPlierExosuitAndShipNormal;
	//	mbin.ShipProductStorageMultiplier          = StackMultiPlierExosuitAndShipNormal;
	//	mbin.ChestSubstanceStorageMultiplier       = StackMultiPlierExosuitAndShipNormal;
	//	mbin.ChestProductStorageMultiplier         = StackMultiPlierExosuitAndShipNormal;
	//	mbin.BaseCapsuleSubstanceStorageMultiplier = StackMultiPlierFreighterAndCargoNormal;
	//	mbin.BaseCapsuleProductStorageMultiplier   = StackMultiPlierFreighterAndCargoNormal;
	//	mbin.DefaultSubstanceStorageMultiplier     = StackMultiPlierExosuitAndShipNormal;
	//	mbin.DefaultProductStorageMultiplier       = StackMultiPlierExosuitAndShipNormal;
	//	mbin.SubstanceMaxAmountLimit               = SubstanceAndProcductStackSizeLimit;
	//	mbin.ProductMaxAmountLimit                 = SubstanceAndProcductStackSizeLimit;
	//	mbin.PlayerPersonalInventoryTechWidth      = TechWidthNormal;
	//	mbin.PlayerPersonalInventoryTechHeight     = TechHeightNormal;
	//	mbin.DeconstructRefundPercentage           = RefundNormal;
	//}
}