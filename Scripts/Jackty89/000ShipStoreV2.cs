//=============================================================================
// Author: Jackty89
//=============================================================================


public class ShipStoreV2 : cmk.NMS.Script.ModClass
{
	private int Total_Seeds_Per_Class = 10000;
	private List<Tuple<string, LanguageId, string, string>> Custom_Language_Desccription_Strings = new List<Tuple<string,LanguageId, string, string>>
	{
		new("CL_BFREIGH", LanguageId.English, "H.G. Corp. Freighter", "H.G. Corp. Spacecraft Dynamics Freighter"),
		new("CL_BHAUL", LanguageId.English, "H.G. Corp. Hauler", "H.G. Corp. Spacecraft Dynamics Hauler"),
		new("CL_BEXPLO", LanguageId.English, "H.G. Corp. Explorer", "H.G. Corp. Spacecraft Dynamics Explorer"),
		new("CL_BSOLAR", LanguageId.English, "H.G. Corp. Solar", "H.G. Corp. Spacecraft Dynamics Solar"),
		new("CL_BFIGHT", LanguageId.English, "H.G. Corp. Fighter", "H.G. Corp. Spacecraft Dynamics Fighter"),
		new("CL_BSHUT", LanguageId.English, "H.G. Corp. Shuttle", "H.G. Corp. Spacecraft Dynamics Shuttle"),
		new("CL_BROYAL", LanguageId.English, "H.G. Corp. Exotic", "H.G. Corp. Spacecraft Dynamics Exotic"),
		new("CL_BALIEN", LanguageId.English, "H.G. Corp. Alien", "H.G. Corp. Spacecraft Dynamics Bioship"),
		new("CL_STORE", LanguageId.English, "H.G. Corp. Spacecraft Dynamics", "Spacecraft constucted by H.G. Corp."),
	};
	//Maybe add frigate(s) too
	protected ShipClassEnum[] Ship_Types = new[] {
		//ShipClassEnum.Freighter,
		ShipClassEnum.Dropship,
		ShipClassEnum.Shuttle,
		ShipClassEnum.Fighter,
		ShipClassEnum.Royal,
		ShipClassEnum.Scientific,
		ShipClassEnum.Sail,
		ShipClassEnum.Alien
	};
	protected InventoryClassEnum[] Classes = new[] {
		InventoryClassEnum.C,
		InventoryClassEnum.B,
		InventoryClassEnum.A,
		InventoryClassEnum.S
	};
	protected override void Execute()
	{
		Random random = new Random();
		List<GcInventoryBaseStatEntry> ship_stats = new List<GcInventoryBaseStatEntry>();
		List<GcProductData> consumable_products = new List<GcProductData>();
		List<GcConsumableItem> consumables = new List<GcConsumableItem>();
		List<GcGenericRewardTableEntry> generic_rewards = new List<GcGenericRewardTableEntry>();

		List<GcRewardTableItem> ships_per_class = new List<GcRewardTableItem>();


		var product_mbin = ExtractMbin<GcProductTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCPRODUCTTABLE.MBIN", false, false);
		var consumable_mbin = ExtractMbin<GcConsumableItemTable>("METADATA/REALITY/TABLES/CONSUMABLEITEMTABLE.MBIN", false, false);
		var reward_mbin = ExtractMbin<GcRewardTable>("METADATA/REALITY/TABLES/REWARDTABLE.MBIN", false, false);
		var realitymanagerdata_mbin = ExtractMbin<GcRealityManagerData>("METADATA/REALITY/DEFAULTREALITY.MBIN", false, false).TradeSettings;

		foreach( ShipClassEnum ship_type in Ship_Types ) {
			ship_stats =  GetShipStats(ship_type);
			foreach( InventoryClassEnum ship_class in Classes ) {
				//(shiptype, shipmodel, shipclass, price, langString)
				var data = GetShipData(ship_type, ship_class);
				string ship_type_from_data = data.Item1;
				string ship_model_from_data = data.Item2;
				string ship_class_from_data = data.Item3;
				int ship_price_from_data = data.Item4;
				string ship_languagestring_from_data = data.Item5;
				
				ships_per_class.Clear();

				if( (ship_type == ShipClassEnum.Royal || ship_type == ShipClassEnum.Alien) && ship_class != InventoryClassEnum.S )
					continue;
				string reward_name = data.Item1.ToUpper() + "_" + ship_class;

				consumable_products.Add(CreateCustomConsumableProducts(reward_name, ship_price_from_data, ship_languagestring_from_data, ship_class_from_data));
				consumables.Add(CreateCustomConsumable(reward_name));
				realitymanagerdata_mbin.SpaceStation.AlwaysPresentProducts.Add(reward_name);

				for( int i = 1; i <= Total_Seeds_Per_Class; i++ ) {
					if( Cancel.IsCancellationRequested ) break;
					var seed = random.NextInt64();
					int slot_number = random.Next(20, 48);

					ships_per_class.Add(CreateNewShip("CL_STORE_DESC", seed, ship_type, ship_class, ship_model_from_data, slot_number, GetShipTechnologies(ship_type), ship_stats));
				}
				generic_rewards.Add(CrateNewShipRewards(reward_name, ships_per_class));
			}
		}

		product_mbin.Table.AddRange(consumable_products);
		consumable_mbin.Table.AddRange(consumables);
		reward_mbin.GenericTable.AddRange(generic_rewards);
		AddNewLanguageString();
	}

	protected (string, string, string, int, string) GetShipData( ShipClassEnum ship_type, InventoryClassEnum ship_class )
	{
		string ship_class_string = "";
		string ship_type_string = "";
		string ship_model = "";
		string custom_language_string = "";
		int price = 0;
		int base_price = 0;
		int price_multiplier = 0;
		Random random = new Random();
		int rand = random.Next(0,2);
		string [] freighter_models = new []
		{
			"MODELS/COMMON/SPACECRAFT/INDUSTRIAL/CAPITALFREIGHTER_PROC.SCENE.MBIN",
			"MODELS/COMMON/SPACECRAFT/INDUSTRIAL/FREIGHTER_PROC.SCENE.MBIN",
			"MODELS/COMMON/SPACECRAFT/INDUSTRIAL/FREIGHTERSMALL_PROC.SCENE.MBIN"
		};

		switch( ship_type ) {
			case ShipClassEnum.Freighter:
				ship_type_string = "Freighter";
				ship_model = freighter_models[rand];
				base_price = 25000000;
				custom_language_string = "CL_BFREIGH";
				break;
			case ShipClassEnum.Dropship:
				ship_type_string = "Dropship";
				ship_model = "MODELS/COMMON/SPACECRAFT/DROPSHIPS/DROPSHIP_PROC.SCENE.MBIN";
				base_price = 2500000;
				custom_language_string = "CL_BHAUL";
				break;
			case ShipClassEnum.Shuttle:
				ship_type_string = "Shuttle";
				ship_model = "MODELS/COMMON/SPACECRAFT/SHUTTLE/SHUTTLE_PROC.SCENE.MBIN";
				base_price = 1000000;
				custom_language_string = "CL_BSHUT";
				break;
			case ShipClassEnum.Fighter:
				ship_type_string = "Fighter";
				ship_model = "MODELS/COMMON/SPACECRAFT/FIGHTERS/FIGHTER_PROC.SCENE.MBIN";
				base_price = 2500000;
				custom_language_string = "CL_BFIGHT";
				break;
			case ShipClassEnum.Royal:
				ship_type_string = "Royal";
				ship_model = "MODELS/COMMON/SPACECRAFT/S-CLASS/S-CLASS_PROC.SCENE.MBIN";
				base_price = 5000000;
				custom_language_string = "CL_BROYAL";
				break;
			case ShipClassEnum.Scientific:
				ship_type_string = "Scientific";
				ship_model = "MODELS/COMMON/SPACECRAFT/SCIENTIFIC/SCIENTIFIC_PROC.SCENE.MBIN";
				base_price = 1000000;
				custom_language_string = "CL_BEXPLO";
				break;
			case ShipClassEnum.Sail:
				ship_type_string = "Sail";
				ship_model = "MODELS/COMMON/SPACECRAFT/SAILSHIP/SAILSHIP_PROC.SCENE.MBIN";
				base_price = 2000000;
				custom_language_string = "CL_BSOLAR";
				break;
			case ShipClassEnum.Alien:
				ship_type_string = "Alien";
				ship_model = "MODELS/COMMON/SPACECRAFT/S-CLASS/BIOPARTS/BIOSHIP_PROC.SCENE.MBIN";
				base_price = 2500000;
				custom_language_string = "CL_BALIEN";
				break;
			default:
				ship_type_string = "Shuttle";
				ship_model = "MODELS/COMMON/SPACECRAFT/SHUTTLE/SHUTTLE_PROC.SCENE.MBIN";
				base_price = 1000000;
				custom_language_string = "CL_BSHUT";
				break;
		}

		switch( ship_class ) {
			case InventoryClassEnum.C:
				ship_class_string = "C";
				price_multiplier = 1;
				break;
			case InventoryClassEnum.B:
				ship_class_string = "B";
				price_multiplier = 2;
				break;
			case InventoryClassEnum.A:
				ship_class_string = "A";
				price_multiplier = 4;
				break;
			case InventoryClassEnum.S:
				ship_class_string = "S";
				price_multiplier = 8;
				break;
			default:
				ship_class_string = "S";
				price_multiplier = 8;
				break;
		}
		price = base_price * price_multiplier;

		return (ship_type_string, ship_model, ship_class_string, price, custom_language_string);
	}

	protected List<GcInventoryElement> GetShipTechnologies( ShipClassEnum ship_type )
	{
		Random random = new Random();
		List<GcInventoryElement> ship_technologies = new List<GcInventoryElement>();
		string [] ship_weapons = new []
		{
			"SHIPGUN1",
			"SHIPLAS1",
			"SHIPMINIGUN",
			"SHIPPLASMA",
			"SHIPROCKETS",
			"SHIPSHOTGUN"
		};
		int rand = random.Next(4);
		switch( ship_type ) {
			case ShipClassEnum.Freighter:
				ship_technologies = new() {
					Inventory.Technology("F_HYPERDRIVE", 200, 200, 0),
				};
				break;
			case ShipClassEnum.Alien:
				ship_technologies = new() {
					Inventory.Technology("SHIPJUMP_ALIEN", 200, 200, 0),
					Inventory.Technology("SHIPGUN_ALIEN", 100, 100, 0),
					Inventory.Technology("SHIELD_ALIEN", 200, 200, 0),
					Inventory.Technology("SHIPLAS_ALIEN", 100, 100, 0),
					Inventory.Technology("LAUNCHER_ALIEN", 200, 200, 0),
					Inventory.Technology("WARP_ALIEN", 120, 120, 0)
				};
				break;
			default:
				ship_technologies = new() {
					Inventory.Technology("SHIPJUMP1", 200, 200, 0),
					Inventory.Technology("SHIPSHIELD", 100, 100, 0),
					Inventory.Technology("LAUNCHER", 200, 200, 0),
					Inventory.Technology("HYPERDRIVE", 100, 100, 0),
					Inventory.Technology(ship_weapons[rand], 200, 200, 0)
				};
				break;
		}
		return ship_technologies;
	}

	protected List<GcInventoryBaseStatEntry> GetShipStats( ShipClassEnum ship_type )
	{
		List<GcInventoryBaseStatEntry> ship_stats = new List<GcInventoryBaseStatEntry>();
		switch( ship_type ) {
			case ShipClassEnum.Freighter:
				ship_stats = new() {
				};
				break;
			case ShipClassEnum.Alien:
				ship_stats = new() {
					InventoryBaseStat.Create("SHIP_DAMAGE"),
					InventoryBaseStat.Create("SHIP_SHIELD"),
					InventoryBaseStat.Create("SHIP_HYPERDRIVE"),
					InventoryBaseStat.Create("ALIEN_SHIP")
				};
				break;
			default:
				ship_stats = new() {
					InventoryBaseStat.Create("SHIP_DAMAGE"),
					InventoryBaseStat.Create("SHIP_SHIELD"),
					InventoryBaseStat.Create("SHIP_HYPERDRIVE"),
				};
				break;
		}
		return ship_stats;
	}

	protected GcProductData CreateCustomConsumableProducts( string product_name, int price, string custom_language_name, string ship_class )
	{
		var prod_mbin = ExtractMbin<GcProductTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCPRODUCTTABLE.MBIN", false, false);
		var new_consumable_product = CloneMbin(prod_mbin.Table.Find(PRODUCT => PRODUCT.ID == "SENTINEL_LOOT"));

		new_consumable_product.ID = product_name;
		new_consumable_product.Name = custom_language_name.ToUpper() + "_NAME";
		new_consumable_product.NameLower = custom_language_name.ToUpper() + "_NAME_L";
		new_consumable_product.Description = custom_language_name.ToUpper() + "_DESC";
		new_consumable_product.Subtitle = custom_language_name.ToUpper() + "_DESC";
		new_consumable_product.Icon.Filename = "TEXTURES/UI/FRONTEND/ICONS/EXPEDITION/PATCH.SHIPCLASS"+ ship_class.ToUpper() +".DDS";
		new_consumable_product.BaseValue = price;

		new_consumable_product.CraftAmountMultiplier = 1;
		new_consumable_product.StackMultiplier = 1;
		new_consumable_product.EggModifierIngredient = false;

		return new_consumable_product;
	}

	protected GcConsumableItem CreateCustomConsumable( string product_name )
	{
		var cons_mbin = ExtractMbin<GcConsumableItemTable>("METADATA/REALITY/TABLES/CONSUMABLEITEMTABLE.MBIN", false, false);
		var new_consumable = CloneMbin(cons_mbin.Table.Find(CONSUMABLE => CONSUMABLE.ID == "SENTINEL_LOOT"));

		new_consumable.ID = product_name;
		new_consumable.RewardID = "R_" + product_name;
		return new_consumable;
	}

	protected GcRewardTableItem CreateNewShip( string ship_name, long ship_seed, ShipClassEnum ship_type, InventoryClassEnum ship_class, string ship_model, int ship_number_of_slots, List<GcInventoryElement> ship_technologies, List<GcInventoryBaseStatEntry> ship_stats )
	{
		var ship = RewardTableItem.SpecificShip(
			ship_name,
			ship_type,
			ship_class,
			ship_model,
			ship_seed,
			ship_number_of_slots,
			ship_technologies,
			ship_stats
		);
		return ship;
	}

	protected GcGenericRewardTableEntry CrateNewShipRewards( string reward_name, List<GcRewardTableItem> ships_per_class )
	{
		var entry = GenericRewardTableEntry.Create(
			"R_" + reward_name ,
			RewardChoiceEnum.Select,
			new(){}
		);
		foreach( var ship in ships_per_class ) {
			entry.Add(ship);
		}
		return entry;
	}

	protected void AddNewLanguageString()
	{
		foreach( var language in Custom_Language_Desccription_Strings ) {
			SetLanguageText(language.Item2, language.Item1 + "_NAME", language.Item3.ToUpper());
			SetLanguageText(language.Item2, language.Item1 + "_NAME_L", language.Item3);
			SetLanguageText(language.Item2, language.Item1 + "_DESC", language.Item4);
		}
	}

}