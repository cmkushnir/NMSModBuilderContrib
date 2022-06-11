//=============================================================================
// Show how to create and add a custom ship reward.
// todo: hook reward to something.
//=============================================================================

public class Add_Custom_Ship : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		GcRewardTable();
	}

	//...........................................................

	protected void GcRewardTable()
	{
		var mbin = ExtractMbin<GcRewardTable>(
			"METADATA/REALITY/TABLES/REWARDTABLE.MBIN"
		);
		var ship = RewardTableItem.SpecificShip(
			 "My Custom Ship",
			ShipClassEnum.Alien, InventoryClassEnum.S,
			"MODELS/COMMON/SPACECRAFT/S-CLASS/BIOPARTS/BIOSHIP_PROC.SCENE.MBIN",
			unchecked((long)0xf437579a7a76d819), 24,
			new() {
				Inventory.Technology("SHIPJUMP_ALIEN", 200, 200, 0),
				Inventory.Technology("SHIPGUN_ALIEN",  100, 100, 0),
				Inventory.Technology("SHIELD_ALIEN",   200, 200, 0),
				Inventory.Technology("SHIPLAS_ALIEN",  100, 100, 0),
				Inventory.Technology("LAUNCHER_ALIEN", 200, 200, 0),
				Inventory.Technology("WARP_ALIEN",     120, 120, 0)
			},
			new() {
				InventoryBaseStat.Create("SHIP_DAMAGE"),
				InventoryBaseStat.Create("SHIP_SHIELD"),
				InventoryBaseStat.Create("SHIP_HYPERDRIVE"),
				InventoryBaseStat.Create("ALIEN_SHIP")
			}
		);
		var entry = GenericRewardTableEntry.Create(
			"R_CMK_SHIP_001",
			RewardChoiceEnum.GiveAll,
			new(){ ship }
		);
		mbin.GenericTable.Add(entry);		
	}
}

//=============================================================================
