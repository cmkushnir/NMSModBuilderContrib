//=============================================================================
using Microsoft.VisualBasic;

public class MyModList : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
        
        bool enableCustomModsGalore                                 = true;
        bool runSandXonly                                           = true;
        bool balancedInventory                                      = false;
        bool expedition                                             = false;

                                             
        Mod<AddDerelictFreighterLootToStore>().IsExecutable 		= true;
        Mod<AddExpeditionTech>().IsExecutable 						= false;
        Mod<BasePartsDeluxe>().IsExecutable 						= true;
        Mod<BuildAboveAndUnderWater>().IsExecutable 				= true;
        Mod<BurnBabyBurn>().IsExecutable 						    = !enableCustomModsGalore; // CMG also adds incinerator
        Mod<CheapPetSlots>().IsExecutable 							= true;
        Mod<CleanMultiplayer>().IsExecutable 						= true;
        Mod<CraftableModules>().IsExecutable 						= true;

        Mod<CraftableUpgradeMods>().IsExecutable 					= !runSandXonly;
        CraftableUpgradeMods.RecipeCostPriceMultiplier 				= 1;
        Mod<CraftableUpgradeModsSandXonly>().IsExecutable 			= runSandXonly;
		CraftableUpgradeModsSandXonly.RecipeCostPriceMultiplier 	= 1;               
        Mod<CustomModsGalore>().IsExecutable 						= enableCustomModsGalore;
        if (runSandXonly)
    	{
            CustomModsGalore.MinProcModLimit 						= 4;
            CustomModsGalore.RecipeCostPriceMultiplier 				= 1;
		}
        
		Mod<DerelictSpeedIncrease>().IsExecutable 					= true;
        Mod<EqualPlantTimers>().IsExecutable 					    = true;
        EqualPlantTimers.HarvestAmount                              = 50;
        EqualPlantTimers.PlantTimer                                 = 3600;
        Mod<CustomDebugOptions>().IsExecutable 						= false;
        Mod<ExocraftRechargeRate>().IsExecutable 					= true;
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
        
        Mod<LearnMoreWords>().IsExecutable 							= true;
        LearnMoreWords.AddWordsTotal = 20;
        
        Mod<LivingShipReducedTimer>().IsExecutable 					= true;
        LivingShipReducedTimer.Multiplier 							= 0.001f;
        Mod<MaxUpgradeFreighterSlotAllClasses>().IsExecutable 		= true;
        Mod<MaxUpgradeFreighterSlotAllClasses48>().IsExecutable 	= false;
        Mod<MoreAndCheaperStarMaps>().IsExecutable 					= true;
        Mod<MoreSalvageData>().IsExecutable 						= true;
        MoreSalvageData.Min											= 40;
        MoreSalvageData.Max											= 100;
        Mod<NoLadderAutoGrab>().IsExecutable 						= true;
        Mod<NoPortalCharge>().IsExecutable 							= true;
        
        Mod<NoShipStart>().IsExecutable 							= !expedition;
        Mod<PickUpGeoBays>().IsExecutable 							= expedition; // this is already included in NoShipStart
        
        Mod<PirateTimerRedux>().IsExecutable 				        = true;
        Mod<QuickSilverCrafting>().IsExecutable 				    = true;
        Mod<QuickSilverRewards>().IsExecutable 						= true;
        
        //SettlementTimerReduction Edits
        Mod<SettlementTimerReduction>().IsExecutable 				= true;
        SettlementTimerReduction.Multiplier 						= 0.25f;  // 25% of vanilla value
        Mod<SkipTutorial>().IsExecutable 				            = expedition;

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
