//=============================================================================
// Author: Jackty89
//=============================================================================

public class SlotMaster : cmk.NMS.Script.ModClass
{

    public bool ImproveShip		 = true;
    public bool ImproveWeapon	 = true;
    public bool ImproveAlien	 = true;
    public bool ImproveVehicle	 = true;
    public bool ImproveInventory = true;
    public bool ImproveFreighter = true;
    
    public bool MaxStartingSlot  = false;
    public bool MaxSlot			 = true;
	public bool MaxSpecialSlot	 = false; //doesnt work

	public int MinUnlockedTechSlots = 60;
	public int MinUnlockedCargoSlots = 120;
	public int MaxUnlockedTechSlots = 60;
	public int MaxUnlockedCargoSlots = 120;
	public int MaxSpecialSlots = 60;


	public int MaxInventoryCapacity = 120;
	public int MaxTechInventoryCapacity = 60;

	private int TechWidth = 10;
	private int TechHeight = 6;

	private int CargoWidth = 10;
	private int CargoHeight = 12;

	protected GcInventoryLayoutSizeType.SizeTypeEnum[] AlienSizes = new[] {
        SizeTypeEnum.AlienSmall,
        SizeTypeEnum.AlienMedium,
        SizeTypeEnum.AlienLarge
    };
    
    protected GcInventoryLayoutSizeType.SizeTypeEnum[] WeaponSizes = new[] {
        SizeTypeEnum.WeaponSmall,
        SizeTypeEnum.WeaponMedium,
        SizeTypeEnum.WeaponLarge
    };

    protected GcInventoryLayoutSizeType.SizeTypeEnum[] VehicleSizes = new[] {
        SizeTypeEnum.VehicleSmall,
        SizeTypeEnum.VehicleMedium,
        SizeTypeEnum.VehicleLarge
    };
    
    protected GcInventoryLayoutSizeType.SizeTypeEnum[] FreighterSizes = new[] {
        SizeTypeEnum.FreighterSmall,
        SizeTypeEnum.FreighterMedium,
        SizeTypeEnum.FreighterLarge,
    };
    
    protected GcInventoryLayoutSizeType.SizeTypeEnum[] ShipSizes = new[] {
        SizeTypeEnum.SciSmall,
        SizeTypeEnum.SciMedium,
        SizeTypeEnum.SciLarge,
        
        SizeTypeEnum.FgtSmall,
        SizeTypeEnum.FgtMedium,
        SizeTypeEnum.FgtLarge,
        
        SizeTypeEnum.ShuSmall,
        SizeTypeEnum.ShtMedium,
        SizeTypeEnum.ShtLarge,
        
        SizeTypeEnum.DrpSmall,
        SizeTypeEnum.DrpMedium,
        SizeTypeEnum.DrpLarge,
        
        SizeTypeEnum.SailSmall,
        SizeTypeEnum.SailMedium,
        SizeTypeEnum.SailLarge,
        
        SizeTypeEnum.RoySmall,
        SizeTypeEnum.RoyMedium,
        SizeTypeEnum.FreighterLarge
    };

	protected GcInventoryLayoutSizeType.SizeTypeEnum[] PlayerSizes = new[] {
		SizeTypeEnum.Suit
	};

	//...........................................................

	protected override void Execute()
    {
		var mbin = ExtractMbin<GcInventoryTable>("METADATA/REALITY/TABLES/INVENTORYTABLE.MBIN");

		if (ImproveShip)
            ImproveShipInventory(mbin);
        if (ImproveWeapon)
            ImproveWeaponInventory(mbin);
        if (ImproveAlien)
            ImproveAlienInventory(mbin);
        if (ImproveVehicle)
            ImproveVehicleInventory(mbin);
        if (ImproveInventory)
            ImprovePlayerInventory(mbin);
        if (ImproveFreighter)
            ImproveFreighterInventory(mbin);
    }

	//...........................................................

	protected void SetUnlockedSlots( GcInventoryTable inventoryTable, GcInventoryLayoutSizeType.SizeTypeEnum[] sizes )
	{
		foreach( var size in sizes ) {
			inventoryTable.GenerationData.GenerationDataPerSizeType[(int)size].MinSlots     = MinUnlockedCargoSlots;
			inventoryTable.GenerationData.GenerationDataPerSizeType[(int)size].MaxSlots     = MaxUnlockedCargoSlots;

			inventoryTable.GenerationData.GenerationDataPerSizeType[(int)size].MinTechSlots = MinUnlockedTechSlots;
			inventoryTable.GenerationData.GenerationDataPerSizeType[(int)size].MaxTechSlots = MaxUnlockedTechSlots;
		}
	}
	protected void SetUnlockedSpecialTechSlots( GcInventoryTable inventoryTable, GcInventoryLayoutSizeType.SizeTypeEnum[] sizes )
	{
		foreach( var size in sizes ) {
			inventoryTable.GenerationData.GenerationDataPerSizeType[(int)size].MaxNumSpecialTechSlots = MaxSpecialSlots;
			inventoryTable.GenerationData.GenerationDataPerSizeType[(int)size].SpecialTechSlotMaxIndex.X = 6;
			inventoryTable.GenerationData.GenerationDataPerSizeType[(int)size].SpecialTechSlotMaxIndex.Y = 10;
		}
		//var mbin = ExtractMbin<GcDefaultSaveData>("METADATA/GAMESTATE/DEFAULTSAVEDATA.MBIN");

		//for( int i = 0; i <= 6; i++ )
		//	for( int j = 0; j <= 10; j++ )
		//		mbin.State.Inventory_TechOnly.SpecialSlots.Add(GcInventorySpecialSlot.);
		
	}
	protected void SetGridSizes( GcInventoryTable inventoryTable, GcInventoryLayoutSizeType.SizeTypeEnum[] sizes )
	{
		foreach( var size in sizes ) {
			inventoryTable.GenerationData.GenerationDataPerSizeType[(int)size].Bounds.MaxWidthSmall     = CargoWidth; //7
			inventoryTable.GenerationData.GenerationDataPerSizeType[(int)size].Bounds.MaxHeightSmall    = CargoHeight; //5
			inventoryTable.GenerationData.GenerationDataPerSizeType[(int)size].Bounds.MaxWidthStandard  = CargoWidth; //10 
			inventoryTable.GenerationData.GenerationDataPerSizeType[(int)size].Bounds.MaxHeightStandard = CargoHeight; //5
			inventoryTable.GenerationData.GenerationDataPerSizeType[(int)size].Bounds.MaxWidthLarge     = CargoWidth; //10
			inventoryTable.GenerationData.GenerationDataPerSizeType[(int)size].Bounds.MaxHeightLarge    = CargoHeight; // 12

			inventoryTable.GenerationData.GenerationDataPerSizeType[(int)size].TechBounds.MaxWidthSmall     = TechWidth; //6
			inventoryTable.GenerationData.GenerationDataPerSizeType[(int)size].TechBounds.MaxHeightSmall    = TechHeight; //3
			inventoryTable.GenerationData.GenerationDataPerSizeType[(int)size].TechBounds.MaxWidthStandard  = TechWidth; //10
			inventoryTable.GenerationData.GenerationDataPerSizeType[(int)size].TechBounds.MaxHeightStandard = TechHeight; //3
			inventoryTable.GenerationData.GenerationDataPerSizeType[(int)size].TechBounds.MaxWidthLarge     = TechWidth; //10
			inventoryTable.GenerationData.GenerationDataPerSizeType[(int)size].TechBounds.MaxHeightLarge    = TechHeight; //6	        
		}
	}

	protected void ImproveShipInventory(GcInventoryTable mbin)
    {
        foreach( var inventoryType in mbin.ShipInventoryMaxUpgradeSize ) {
            if (inventoryType == mbin.ShipInventoryMaxUpgradeSize[(int)ShipClassEnum.Freighter])
                continue;
            for ( int i = 0; i < 4; i++ ) {
                inventoryType.MaxInventoryCapacity[i]      = MaxInventoryCapacity;
                inventoryType.MaxTechInventoryCapacity[i]  = MaxTechInventoryCapacity;
            }
        }
        ImproveShipOrFreighterGrid(mbin, ShipSizes);
    }

    protected void ImproveFreighterInventory(GcInventoryTable mbin)
    {
        for (int i = 0; i < 4; i++)
        {
            mbin.ShipInventoryMaxUpgradeSize[(int)ShipClassEnum.Freighter].MaxInventoryCapacity[i]     = MaxInventoryCapacity;
            mbin.ShipInventoryMaxUpgradeSize[(int)ShipClassEnum.Freighter].MaxTechInventoryCapacity[i] = MaxTechInventoryCapacity;
        }
        ImproveShipOrFreighterGrid(mbin, FreighterSizes);
    }

    protected void ImproveWeaponInventory(GcInventoryTable mbin)
    {
        for (int i = 0; i < 4; i++)
        {
            mbin.WeaponInventoryMaxUpgradeSize.MaxInventoryCapacity[i] = MaxTechInventoryCapacity;
        }
        ImproveWeaponGridAndSlot(mbin);
    }
    
    protected void ImproveShipOrFreighterGrid( GcInventoryTable inventoryTable, GcInventoryLayoutSizeType.SizeTypeEnum[] sizes)
    {
        foreach( var size in sizes ) 
        {
            if(MaxSlot) {
				SetUnlockedSlots(inventoryTable, sizes);
            }
			if( MaxSpecialSlot ) {
				SetUnlockedSpecialTechSlots(inventoryTable, sizes);
			}
			SetGridSizes(inventoryTable, sizes);
		}
    }



    protected void ImproveVehicleInventory(GcInventoryTable inventoryTable )
    {
		SetUnlockedSlots(inventoryTable, VehicleSizes);
		SetGridSizes(inventoryTable, VehicleSizes);
		if( MaxSpecialSlot ) {
			SetUnlockedSpecialTechSlots(inventoryTable, VehicleSizes);
		}
	}
    
    protected void ImproveWeaponGridAndSlot(GcInventoryTable inventoryTable )
    {
		if( MaxSpecialSlot ) {
			SetUnlockedSpecialTechSlots(inventoryTable, WeaponSizes);
		}
		SetGridSizes(inventoryTable, VehicleSizes);

		foreach( var weaponSize in WeaponSizes)
        {
            if(MaxSlot)
            {
				inventoryTable.GenerationData.GenerationDataPerSizeType[(int)weaponSize].MinSlots     = MinUnlockedTechSlots;
				inventoryTable.GenerationData.GenerationDataPerSizeType[(int)weaponSize].MaxSlots     = MaxUnlockedTechSlots;

				inventoryTable.GenerationData.GenerationDataPerSizeType[(int)weaponSize].MinTechSlots = MinUnlockedTechSlots;
				inventoryTable.GenerationData.GenerationDataPerSizeType[(int)weaponSize].MaxTechSlots = MaxUnlockedTechSlots;
            }
        }
    }

    protected void ImproveAlienInventory(GcInventoryTable inventoryTable )
    {
		if( MaxSlot ) {
			SetUnlockedSlots(inventoryTable, AlienSizes);
		}
		if( MaxSpecialSlot ) {
			SetUnlockedSpecialTechSlots(inventoryTable, AlienSizes);
		}
		SetGridSizes(inventoryTable, AlienSizes);
	}

    protected void ImprovePlayerInventory(GcInventoryTable inventoryTable )
    {

		if(MaxSlot) {
			SetUnlockedSlots(inventoryTable, PlayerSizes);
		}
		if(MaxSpecialSlot) {
			SetUnlockedSpecialTechSlots(inventoryTable, PlayerSizes);
		}
    	if(MaxStartingSlot) {
    		NewSaveStartingSlots();
    	}
		SetGridSizes(inventoryTable, PlayerSizes);
    }

	protected void NewSaveStartingSlots()
	{
		var mbin = ExtractMbin<GcRealityManagerData>("METADATA/REALITY/DEFAULTREALITY.MBIN");
		mbin.SuitStartingSlotLayout.Slots = 120;
		mbin.SuitTechOnlyStartingSlotLayout.Slots = 60;
		mbin.ShipStartingLayout.Slots = 120;
		mbin.ShipTechOnlyStartingLayout.Slots = 60;
	}
}