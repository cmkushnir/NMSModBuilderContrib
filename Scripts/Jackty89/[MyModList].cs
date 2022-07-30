//=============================================================================
using Microsoft.VisualBasic;

public class MyModList : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
        bool enableCustomModsGalore                                 = true;
        bool runSandXonly                                           = true;
        bool balancedInventory                                      = true;
        bool noShipStart                                            = true;
        bool challengeMode                                          = false;
        bool uninstallExtraTech                                     = false;

        Mod<AddDerelictFreighterLootToStore>().IsExecutable 		= true;
        Mod<AddExpeditionTech>().IsExecutable 						= false;
        Mod<AddNewFoods>().IsExecutable                             = false;

        Mod<BasePartsDeluxe>().IsExecutable 						= !noShipStart; // included in NoShipStart
        Mod<BuildAboveAndUnderWater>().IsExecutable 				= true;
        Mod<BurnBabyBurn>().IsExecutable 						    = !enableCustomModsGalore; // CMG also adds incinerator
        Mod<CheapPetSlots>().IsExecutable 							= true;
        Mod<CleanMultiplayer>().IsExecutable 						= true;
        Mod<CraftableModules>().IsExecutable 						= true;

        Mod<CraftableUpgradeMods>().IsExecutable 					= !runSandXonly;
        CraftableUpgradeMods.RecipeCostPriceMultiplier 				= 1; // make into a float
        Mod<CraftableUpgradeModsSandXonly>().IsExecutable 			= runSandXonly;
		CraftableUpgradeModsSandXonly.RecipeCostPriceMultiplier 	= 1;               
        Mod<CustomModsGalore>().IsExecutable 						= enableCustomModsGalore;
        if (runSandXonly)
    	{
            CustomModsGalore.MinProcModLimit 						= 4;
            CustomModsGalore.RecipeCostPriceMultiplier 				= 1;
		}
        
        Mod<CustomDebugOptions>().IsExecutable                      = false;
        Mod<CustomDescriptions>().IsExecutable                      = false;

        Mod<DerelictSpeedIncrease>().IsExecutable 					= true;
        DerelictSpeedIncrease.SpeedMultiplier                       = 0.5f;

        Mod<EqualPlantTimers>().IsExecutable 					    = true;
        EqualPlantTimers.HarvestAmount                              = 50;
        EqualPlantTimers.PlantTimer                                 = 3600; // time in seconds => 60 min

        Mod<ExocraftRechargeRate>().IsExecutable 					= true;
        ExocraftRechargeRate.RechargeRate                           = 15f;

        Mod<ExtendedExocraftAndShipScanner>().IsExecutable 			= true;
        Mod<FuelEconomy>().IsExecutable 							= true;

        //Inventory Edits
        Mod<InventoryRebalance>().IsExecutable 						= balancedInventory;	
        Mod<InventoryRebalanceParams>().IsExecutable 				= !balancedInventory; //this mod changes the values for BOTH normal AND survival/perma
        InventoryRebalanceParams.SubstanceDefaultStackSizeNormal 	= 50000; 
        InventoryRebalanceParams.ProductDefaultStackSizeNormal 		= 50;
        InventoryRebalanceParams.TechWidthNormal 					= 8;
        InventoryRebalanceParams.TechHeightNormal 					= 4; 
        InventoryRebalanceParams.RefundNormal 						= 0.75f;
        
        Mod<KeepTalkingChef>().IsExecutable 				        = true;
		
        Mod<LearnMoreWords>().IsExecutable 							= true;
        LearnMoreWords.AddWordsTotal                                = 20;
        LearnMoreWords.PercentageChance                             = 100;
        
        Mod<LivingShipReducedTimer>().IsExecutable 					= true;
        LivingShipReducedTimer.Multiplier 							= 0.001f;
        
        Mod<MaxUpgradeFreighterSlotAllClasses>().IsExecutable 		= true;
        Mod<MaxUpgradeFreighterSlotAllClasses48>().IsExecutable 	= false;
        Mod<MoreAndCheaperStarMaps>().IsExecutable 					= true;
        
        Mod<MoreSalvageData>().IsExecutable 						= true;
        MoreSalvageData.Min											= 5;
        MoreSalvageData.Max											= 15;
        
        Mod<NoLadderAutoGrab>().IsExecutable 						= true;
        Mod<NoPortalCharge>().IsExecutable 							= true;
        
        Mod<NoShipStart>().IsExecutable 							= noShipStart;
        Mod<PickUpGeoBays>().IsExecutable 							= !noShipStart; // this is already included in NoShipStart
        
        Mod<PirateTimerRedux>().IsExecutable 				        = true;
        PirateTimerRedux.Multiplier                                 = 3;

        Mod<QuickSilverCrafting>().IsExecutable 				    = true;
        Mod<QuickSilverRewards>().IsExecutable 						= true;
        
        Mod<RealisticTimers>().IsExecutable                         = challengeMode;

        Mod<ReducedPulseSpeedLines>().IsExecutable 					= true;
        Mod<RepeatInventoryExpansion>().IsExecutable     	        = true;
                
        //SettlementTimerReduction Edits
        Mod<SettlementTimerReduction>().IsExecutable 				= true;
        SettlementTimerReduction.Multiplier 						= 0.25f;  // 25% of vanilla value

        // CoreMissionEdits might not be necessary, DEBUG options edits work just fine
        Mod<SkipTutorial>().IsExecutable 				            = false; //!noShipStart; // also in no shipstart

        Mod<SlotMaster>().IsExecutable 								= true;
        SlotMaster.ImproveShip 										= false;
        SlotMaster.ImproveWeapon 									= false;
        SlotMaster.ImproveVehicle 									= false; // Already in unique exocrafts
        SlotMaster.ImproveAlien 									= true;
        SlotMaster.ImproveInventory 								= false;
        SlotMaster.ImproveFreighter 								= false;

        Mod<SustainAbility>().IsExecutable 							= true;
        Mod<TaintedMetalCrafting>().IsExecutable 					= true;
        
        Mod<UninstallCoreWeapons>().IsExecutable 					= true;
        UninstallCoreWeapons.UninstallExtra                         = uninstallExtraTech;
        
        Mod<UniqueExocrafts>().IsExecutable 						= true;
        Mod<UniqueSpaceShips>().IsExecutable 						= true;                
        Mod<InstantActions>().IsExecutable 							= true;
        Mod<InstantTextDisplay>().IsExecutable 						= true;

        Mod<InstantScan>().IsExecutable 							= true;
        InstantScan.ScanTime										= 0f;
        
        Mod<SpawnRateForClasses>().IsExecutable 					= false; // has effect on Seed value so won't use
        
        Mod<FastRefiners>().IsExecutable 							= true;
        FastRefiners.TimeToMake                                     = 1f;
    }

    //...........................................................
}

//=============================================================================
