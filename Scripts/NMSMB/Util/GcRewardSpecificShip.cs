//=============================================================================

public class RewardSpecificShip
{
	public static GcGenericRewardTableEntry Create(
		string                         ID,
		string                         NAMEOVERRIDE,
		ShipClassEnum                  TYPE,
		InventoryClassEnum             CLASS,
		string                         FILENAME,
		long                           SEED,
		int                            SLOTS     = 48,
		List<GcInventoryElement>       INVENTORY = null,
		List<GcInventoryBaseStatEntry> STATS     = null
	){
		var ship = new GcRewardSpecificShip();
		var item = new GcRewardTableItem(){
			PercentageChance = 100,
			Reward           = ship
		};
		var entry = new GcGenericRewardTableEntry(){
			Id    = ID,
			List  = new(){
				RewardChoice     = RewardChoiceEnum.GiveAll,
				OverrideZeroSeed = false,
				List             = new(){ item }
			}
		};

		ship.ShipResource = new(){
			Filename = FILENAME,
			Seed     = new(){ Seed = SEED, UseSeedValue = true }
		};
		ship.ShipLayout    = new(){ Slots = SLOTS, Seed = new(){ Seed = 1, UseSeedValue = true }, Level = 1 };
		ship.ShipInventory = new(){
			Slots          = INVENTORY,
			Class          = new(){ InventoryClass = CLASS },
			BaseStatValues = STATS
		};
		ship.ShipType     = new(){ ShipClass = TYPE };
		ship.NameOverride = NAMEOVERRIDE;
		ship.IsGift       = true;
		ship.IsRewardShip = true;
		
		return entry;
	}
}

//=============================================================================
