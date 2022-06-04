//=============================================================================
// Author: Jackty89
//=============================================================================

public class SlotMaster : cmk.NMS.Script.ModClass
{

	public static bool ImproveShip		= true;
	public static bool ImproveWeapon	= true;
	public static bool ImproveAlien		= true;
	public static bool ImproveVehicle	= true;
	public static bool ImproveInventory = true;
	public static bool ImproveFreighter = true;

	protected GcInventoryLayoutSizeType.SizeTypeEnum[] AlienSizes = new[] {
		SizeTypeEnum.AlienSmall,
		SizeTypeEnum.AlienMedium,
		SizeTypeEnum.AlienLarge
	};

	protected GcInventoryLayoutSizeType.SizeTypeEnum[] VehicleSizes = new[] {
		SizeTypeEnum.VehicleSmall,
		SizeTypeEnum.VehicleMedium,
		SizeTypeEnum.VehicleLarge
	};

	//...........................................................

	protected override void Execute()
	{
		if (ImproveShip)
			ImproveShipInventory();
		if (ImproveWeapon)
			ImproveWeaponInventory();
		if (ImproveAlien)
			ImproveAlienInventory();
		if (ImproveVehicle)
			ImproveVehicleInventory();
		if (ImproveInventory)
			ImprovePlayerInventory();
		if (ImproveFreighter)
			ImproveFreighterInventory();

	}

	//...........................................................

	protected void ImproveShipInventory()
	{
		var mbin = ExtractMbin<GcInventoryTable>("METADATA/REALITY/TABLES/INVENTORYTABLE.MBIN");

		foreach( var inventoryType in mbin.ShipInventoryMaxUpgradeSize ) {
            if (inventoryType == mbin.ShipInventoryMaxUpgradeSize[(int)ShipClassEnum.Freighter])
                continue;
            for ( int i = 0; i < 4; i++ ) {
				inventoryType.MaxInventoryCapacity[i]      = 48;
				inventoryType.MaxTechInventoryCapacity[i]  = 48;
				inventoryType.MaxCargoInventoryCapacity[i] = 48;
			}
		}
	}

	protected void ImproveFreighterInventory()
	{
		var mbin = ExtractMbin<GcInventoryTable>("METADATA/REALITY/TABLES/INVENTORYTABLE.MBIN");
		for (int i = 0; i < 4; i++)
		{
			mbin.ShipInventoryMaxUpgradeSize[(int)ShipClassEnum.Freighter].MaxInventoryCapacity[i] = 48;
			mbin.ShipInventoryMaxUpgradeSize[(int)ShipClassEnum.Freighter].MaxTechInventoryCapacity[i] = 48;
		}
	}

	protected void ImproveWeaponInventory()
	{
		var mbin = ExtractMbin<GcInventoryTable>("METADATA/REALITY/TABLES/INVENTORYTABLE.MBIN");
			
		for (int i = 0; i < 4; i++)
		{
			mbin.WeaponInventoryMaxUpgradeSize.MaxInventoryCapacity[i] = 48;
		}
	}

	protected void ImproveVehicleInventory()
	{
		var mbin = ExtractMbin<GcInventoryTable>("METADATA/REALITY/TABLES/INVENTORYTABLE.MBIN");
		foreach( var vehicleSize in VehicleSizes ) {
			mbin.GenerationData.GenerationDataPerSizeType[(int)vehicleSize].MinSlots = 48;
			mbin.GenerationData.GenerationDataPerSizeType[(int)vehicleSize].MaxSlots = 48;
			//mbin.GenerationData.GenerationDataPerSizeType[(int)vehicleSize].MinTechSlots = 48;
			//mbin.GenerationData.GenerationDataPerSizeType[(int)vehicleSize].MaxTechSlots = 48;                
			//mbin.GenerationData.GenerationDataPerSizeType[(int)vehicleSize].MinCargoSlots = 48;
			//mbin.GenerationData.GenerationDataPerSizeType[(int)vehicleSize].MaxCargoSlots = 48;
		}
	}

	protected void ImproveAlienInventory()
	{
		var mbin = ExtractMbin<GcInventoryTable>("METADATA/REALITY/TABLES/INVENTORYTABLE.MBIN");

		foreach (var alienSize in AlienSizes)
		{
			mbin.GenerationData.GenerationDataPerSizeType[(int)alienSize].MinSlots = 48;
			mbin.GenerationData.GenerationDataPerSizeType[(int)alienSize].MaxSlots = 48;
			mbin.GenerationData.GenerationDataPerSizeType[(int)alienSize].MinTechSlots = 48;
			mbin.GenerationData.GenerationDataPerSizeType[(int)alienSize].MaxTechSlots = 48;
			mbin.GenerationData.GenerationDataPerSizeType[(int)alienSize].MinCargoSlots = 48;
			mbin.GenerationData.GenerationDataPerSizeType[(int)alienSize].MaxCargoSlots = 48;
		}
	}

	protected void ImprovePlayerInventory()
	{
		var paths = new string[] {
			"METADATA\\GAMESTATE\\DEFAULTINVENTORYBALANCESURVIVAL.MBIN",
			"METADATA\\GAMESTATE\\DEFAULTINVENTORYBALANCE.MBIN"
		};
		foreach( var path in paths ) {
			var mbin = ExtractMbin<GcInventoryStoreBalance>(path);
			mbin.PlayerPersonalInventoryTechWidth = 8;
			mbin.PlayerPersonalInventoryTechHeight = 6;

		}
	}
}

//=============================================================================
