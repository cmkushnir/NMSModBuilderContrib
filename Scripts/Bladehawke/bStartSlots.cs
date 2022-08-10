//=============================================================================

public class bStartSlots : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		DefaultReality();
		DefaultSave();
		DefaultInventoryBalance();
	}

	protected void DefaultReality()
	{
		var mbin = ExtractMbin<GcRealityManagerData>(
			"METADATA/REALITY/DEFAULTREALITY.MBIN"
		);
		mbin.SuitStartingSlotLayout        .Slots = 48;
		mbin.SuitTechOnlyStartingSlotLayout.Slots = 48;
		mbin.SuitTechOnlyStartingSlotLayout.Seed.UseSeedValue = false;
		mbin.SuitCargoStartingSlotLayout   .Slots = 48;
		
		mbin.ShipStartingLayout         .Slots = 48;
		mbin.ShipTechOnlyStartingLayout .Slots = 48;
		mbin.ShipCargoOnlyStartingLayout.Slots = 48;
		
	}
	//...........................................................
	
	protected void DefaultSave()
	{
		var paths = new [] {
			"METADATA/GAMESTATE/DEFAULTSAVEDATA.MBIN",
			"METADATA/GAMESTATE/DEFAULTSAVEDATACREATIVE.MBIN"
		};
		
		foreach ( var path in paths ) {
			var mbin = ExtractMbin<GcDefaultSaveData>( path );
		
			mbin.State.WeaponLayout.Slots = 48;
		}
	}
	
	protected void DefaultInventoryBalance()
	{
		// NOTE: This unlocks all 48 suit tech slots, but only the first 14
		// will be activated at start
		var paths = new[] {
			"METADATA/GAMESTATE/DEFAULTINVENTORYBALANCE.MBIN",
			"METADATA/GAMESTATE/DEFAULTINVENTORYBALANCESURVIVAL.MBIN"
		};
		
		foreach( var path in paths ) {
			var mbin = ExtractMbin<GcInventoryStoreBalance>( path );
			
			mbin.PlayerPersonalInventoryTechWidth  = 8;
			mbin.PlayerPersonalInventoryTechHeight = 6;
		}
	}
}

//=============================================================================
