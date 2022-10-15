//=============================================================================

public class RewardTableItem
{
	public static GcRewardTableItem Units(
		int MIN,
		int MAX,
		float  CHANCE   = 100.0f,  // 0 - 100
		string LABEL_ID = null
	){
		if( MAX == 0 ) MAX = MIN;
		return new() {
			PercentageChance = CHANCE,
			Reward = new GcRewardMoney {
				AmountMin   = MIN,
				AmountMax   = MAX,
				RoundNumber = false,
				Currency    = new() { Currency = CurrencyEnum.Units }
			},
			LabelID = LABEL_ID ?? ""
		};
	}

	//...........................................................

	public static GcRewardTableItem Nanites(
		int MIN,
		int MAX,
		float  CHANCE   = 100.0f,  // 0 - 100
		string LABEL_ID = null
	){
		if( MAX == 0 ) MAX = MIN;
		return new() {
			PercentageChance = CHANCE,
			Reward = new GcRewardMoney {
				AmountMin   = MIN,
				AmountMax   = MAX,
				RoundNumber = false,
				Currency    = new() { Currency = CurrencyEnum.Nanites }
			},
			LabelID = LABEL_ID ?? ""
		};
	}

	//...........................................................

	public static GcRewardTableItem Specials(
		int MIN,
		int MAX,
		float  CHANCE   = 100.0f,  // 0 - 100
		string LABEL_ID = null
	){
		if( MAX == 0 ) MAX = MIN;
		return new() {
			PercentageChance = CHANCE,
			Reward = new GcRewardMoney {
				AmountMin   = MIN,
				AmountMax   = MAX,
				RoundNumber = false,
				Currency    = new() { Currency = CurrencyEnum.Specials }
			},
			LabelID = LABEL_ID ?? ""
		};
	}

	//...........................................................

	public static GcRewardTableItem TeachWord(
		RaceEnum RACE,
		wordcategorytableEnumEnum CATEGORY = wordcategorytableEnumEnum.MISC,
		float  CHANCE   = 100.0f,  // 0 - 100
		string LABEL_ID = null
	) => new() {
		PercentageChance = CHANCE,
		Reward = new GcRewardTeachWord {
			Race        = new() { AlienRace = RACE },
			UseCategory = CATEGORY != wordcategorytableEnumEnum.MISC,
			Category    = new() { wordcategorytableEnum = CATEGORY },
			AmountMin   = 1,
			AmountMax   = 1
		},
		LabelID = LABEL_ID ?? ""
	};

	//...........................................................

	// See: METADATA/REALITY/TABLES/REWARDTABLE.MBIN Id = "FREIGHT_REWARD"
	// note: untested
	public static GcRewardTableItem GcRewardOpenFreeFreighter(
		float  CHANCE = 100.0f,  // 0 - 100
		string LABEL_ID = null
	) => new() {
		PercentageChance = CHANCE,
		Reward = new GcRewardOpenFreeFreighter {
			ReinteractWhenBought       = false,
			NextInteractionIfBought    = "?FREIGHTER_END",
			NextInteractionIfNotBought = "?FREIGHTER",
		},
		LabelID = LABEL_ID ?? ""
	};

	//...........................................................

	public static GcRewardTableItem SpecificShip(
		string             NAMEOVERRIDE,
		ShipClassEnum      TYPE,
		InventoryClassEnum CLASS,
		string             FILENAME,
		long               SEED,
		int                SLOTS = 48,
		List<GcInventoryElement>       INVENTORY = null,
		List<GcInventoryBaseStatEntry> STATS     = null,
		float  CHANCE   = 100.0f,  // 0 - 100
		string LABEL_ID = null
	) => new() {
		PercentageChance = CHANCE,
		Reward = new GcRewardSpecificShip {
			ShipResource = new() {
				Filename = FILENAME,
				Seed     = new() { Seed = SEED, UseSeedValue = true }
			},
			ShipLayout    = new() { Slots = SLOTS, Seed = new() { Seed = 1, UseSeedValue = true }, Level = 1 },
			ShipInventory = new() {
				Slots          = INVENTORY,
				Class          = new() { InventoryClass = CLASS },
				BaseStatValues = STATS
			},
			ShipType     = new() { ShipClass = TYPE },
			NameOverride = NAMEOVERRIDE,
			IsGift       = true,
			IsRewardShip = true
		},
		LabelID = LABEL_ID ?? ""
	};
}

//=============================================================================
