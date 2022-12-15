//=============================================================================
//
// Based on a Lua script by AcThPaU
// 
// "Destroy RNG, everything S class and best stat, best rewards, and more",
//
//=============================================================================
public class Derandomizer : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		Try(() => InventoryTable());
		Try(() => ProceduralTechnologyTable());
		Try(() => RewardTable());
		Try(() => ProceduralProductTable());
		Try(() => GcSettlementGlobals());
		Try(() => GcFleetGlobals());
		Try(() => ExperienceSpawnTable());
		Try(() => FrigateTraitTable());
	}

	protected void InventoryTable()
	{
		var mbin = ExtractMbin<GcInventoryTable>(
			"METADATA/REALITY/TABLES/INVENTORYTABLE.MBIN"
		);

		foreach( var sizeType in mbin.GenerationData.GenerationDataPerSizeType ) {
			sizeType.MinSlots = sizeType.MaxSlots;
			sizeType.MinTechSlots = sizeType.MaxTechSlots;
			sizeType.MinCargoSlots = sizeType.MaxCargoSlots;
		}

		foreach( var wealthClass in mbin.ClassProbabilityData ) {
			for( var inventoryClass = (int)InventoryClassEnum.C; inventoryClass < (int)InventoryClassEnum.S; ++inventoryClass ) {
				wealthClass.ClassProbabilities[inventoryClass] = 0;
			}
			wealthClass.ClassProbabilities[(int)InventoryClassEnum.S] = 100;
		}

		MinToMax(mbin.ShipBaseStatsData);
		MinToMax(mbin.WeaponBaseStatsData);

		foreach( var shipClass in mbin.ShipCostData.InventoryCostData ) {
			MaxSlots(shipClass);
		}

		foreach( var weapClass in mbin.WeaponCostData ) {
			MaxSlots(weapClass);
		}

		MaxSlots(mbin.VehicleCostData);

		foreach( var entry in mbin.Table ) {
			entry.MinSize = entry.MaxSize;
		}
	}

	protected void MinToMax( GcInventoryGenerationBaseStatData[] ITEMCLASSES )
	{
		foreach( var itemClass in ITEMCLASSES ) {
			foreach( var inventoryClass in itemClass.BaseStatsPerClass ) {
				foreach( var entry in inventoryClass.BaseStats ) {
					entry.Min = entry.Max;
				}
			}
		}
	}

	protected void MaxSlots( GcInventoryCostDataEntry OBJECT )
	{
		OBJECT.MinSlots = OBJECT.MaxSlots;
	}

	protected void ProceduralTechnologyTable()
	{
		var mbin = ExtractMbin<GcProceduralTechnologyTable>(
			"METADATA/REALITY/TABLES/NMS_REALITY_GCPROCEDURALTECHNOLOGYTABLE.MBIN"
		);
		var shields = new[] { "UP_SHLD1", "UP_SHLD2", "UP_SHLD3", "UP_SHLD4", "UP_SHLDX", "UP_SNSUIT" };
		var fuels   = new[] { "UP_FRFUE1", "UP_FRFUE2", "UP_FRFUE3", "UP_FRFUE4" };

		foreach( var entry in mbin.Table ) {
			entry.NumStatsMin = entry.NumStatsMax;
		}
		// --------------- Correction ----------------
		foreach( var shield in shields ) {
			var entry = mbin.Table.Find(ENTRY => ENTRY.ID == shield);
			foreach( var statLevel in entry.StatLevels ) {
				if( statLevel.Stat.StatsType == StatsTypeEnum.Suit_Armour_Shield_Strength ) {
					statLevel.ValueMin = statLevel.ValueMax;
				}
			}
		}

		foreach( var fuel in fuels ) {
			var entry = mbin.Table.Find(ENTRY => ENTRY.ID == fuel);
			foreach( var statLevel in entry.StatLevels ) {
				if( statLevel.Stat.StatsType == StatsTypeEnum.Freighter_Fleet_Fuel ) {
					statLevel.ValueMin = statLevel.ValueMax;
				}
			}
		}

		// ---------------- End Correction -----------

		foreach( var entry in mbin.Table ) {
			foreach( var statLevel in entry.StatLevels ) {
				if( statLevel.WeightingCurve.WeightingCurve == WeightingCurveEnum.MinIsSuperRare ||
					statLevel.WeightingCurve.WeightingCurve == WeightingCurveEnum.MinIsRare ||
					statLevel.WeightingCurve.WeightingCurve == WeightingCurveEnum.MinIsUncommon ) {
					statLevel.ValueMax = statLevel.ValueMin;
				}
			}
		}

		foreach( var entry in mbin.Table ) {
			foreach( var statLevel in entry.StatLevels ) {
				statLevel.ValueMin = statLevel.ValueMax;
			}
		}
	}

	protected void RewardTable()
	{
		var mbin = ExtractMbin<GcRewardTable>(
			"METADATA/REALITY/TABLES/REWARDTABLE.MBIN"
		);

		var ListGcGenericRewardTableEntry_t = typeof(List<GcGenericRewardTableEntry>);

		var fields = mbin.GetType().GetFields();
		foreach( var field in fields ) {
			if( field.FieldType != ListGcGenericRewardTableEntry_t ) continue;
			var table = field.GetValue(mbin) as List<GcGenericRewardTableEntry>;

			foreach( var entry in table ) {
				foreach( var item in entry.List.List ) {

					if( item.Reward is GcRewardMultiSpecificItems reward ) {

						foreach( var myItem in reward.Items ) {
							if( myItem.MultiItemRewardType == GcMultiSpecificItemEntry.MultiItemRewardTypeEnum.ProcTech ) {
								myItem.ProcTechQuality  = (int)QualityEnum.Legendary;
							}
						}
					}
					else if( item.Reward is GcRewardProcTechProduct product ) {

						product.WeightedChanceNormal = 0;
						product.WeightedChanceRare = 0;
						product.WeightedChanceEpic = 0;
						product.WeightedChanceLegendary = 100;
					}
				}
			}
		}
		var myTable = mbin.Table;
		foreach( var entry in myTable ) {
			foreach( var rarity in entry.Rarities ) {
				foreach( var size in rarity.Sizes ) {
					foreach( var item in size.List ) {
						if( item.Reward is GcRewardProcTechProduct product ) {
							product.WeightedChanceNormal = 0;
							product.WeightedChanceRare = 0;
							product.WeightedChanceEpic = 0;
							product.WeightedChanceLegendary = 100;
						}
					}
				}
			}
		}
	}

	protected void ProceduralProductTable()
	{
		var mbin = ExtractMbin<GcProceduralProductTable>(
			"METADATA/REALITY/TABLES/PROCEDURALPRODUCTTABLE.MBIN"
		);

		for( int i = 0; i < mbin.Table.Length; ++i ) {
			mbin.Table[i].DeployableProductID = Regex.Replace(mbin.Table[i].DeployableProductID, "(U_FR_[A-Z]*)%d", "$+4");
		}
	}

	protected void GcSettlementGlobals()
	{
		var mbin = ExtractMbin<GcSettlementGlobals>("GCSETTLEMENTGLOBALS.MBIN");

		mbin.SettlementMiniExpeditionSuccessChance = 1;
		mbin.ConflictJudgementSelector = 0;
	}

	protected void GcFleetGlobals()
	{
		var mbin = ExtractMbin<GcFleetGlobals>("GCFLEETGLOBALS.GLOBAL.MBIN");

		mbin.PercentChangeOfFrigateBeingPurchasable = 100;
		mbin.PercentChanceOfFrigateAdditionalSpawnedTrait = 100;

		foreach( var frigateClass in mbin.FrigateInitialStats.FrigateClass ) {
			frigateClass.Stats[(int)FrigateStatTypeEnum.FuelBurnRate].Maximum = frigateClass.Stats[(int)FrigateStatTypeEnum.FuelBurnRate].Minimum;
			foreach( var stat in frigateClass.Stats ) {
				stat.Minimum = stat.Maximum;
			}
		}
	}

	protected void ExperienceSpawnTable()
	{
		var mbin = ExtractMbin<GcExperienceSpawnTable>(
			"METADATA/SIMULATION/SCENE/EXPERIENCESPAWNTABLE.MBIN"
		);

		var first = mbin.AmbientSpawns.FindFirst<GcAIShipSpawnData>();
		first.WarpIn = true;
		first.Role.AIShipRole = GcAISpaceshipRoles.AIShipRoleEnum.CapitalFreighter;
	}

	
	protected void FrigateTraitTable()
	{
		var mbin = ExtractMbin<GcFrigateTraitTable>(
			"METADATA/REALITY/TABLES/FRIGATETRAITTABLE.MBIN"
		);

		foreach( var trait in mbin.Traits ) {
			if( trait.Strength.FrigateTraitStrength <= FrigateTraitStrengthEnum.NegativeSmall ) {
				for( var i = 0; i <= (int)GcFrigateClass.FrigateClassEnum.DeepSpaceCommon; ++i ) {
					trait.ChanceOfBeingOffered[i] = 0;
				}
			}
		}
	}
	//...........................................................
}

//=============================================================================
