//=============================================================================
using Microsoft.VisualBasic;

public class MyModList : cmk.NMS.Script.ModClass
{
     protected override void Execute()
     {
        bool enableCustomModsGalore                                 = true;
        bool runSandXonly                                           = true;
        bool balancedInventory                                      = true;
        bool noShipStart                                            = true; //X
        bool challengeMode                                          = false;
        bool uninstallExtraTech                                     = false;

        Script<AddDerelictFreighterLootToStore>().IsExecutable      = true;
        Script<AddExpeditionTech>().IsExecutable                    = false;
        Script<AddNewFoods>().IsExecutable                          = false;

        Script<BasePartsDeluxe>().IsExecutable                      = !noShipStart; // included in NoShipStart
        Script<BuildAboveAndUnderWater>().IsExecutable              = true;
        Script<BurnBabyBurn>().IsExecutable                         = !enableCustomModsGalore; // CMG also adds incinerator
        Script<CheapPetSlots>().IsExecutable                        = true;
        Script<CleanMultiplayer>().IsExecutable                     = true;
        Script<CraftableAlienToken>().IsExecutable                  = true;
        Script<CraftableModules>().IsExecutable                     = true;

        var CraftableUpgradeMods = Script<CraftableUpgradeMods>();
        CraftableUpgradeMods.IsExecutable                           = !runSandXonly;
        CraftableUpgradeMods.RecipeCostPriceMultiplier              = 1; // make into a float

        var CraftableUpgradeModsSandXonly = Script<CraftableUpgradeModsSandXonly>();
        CraftableUpgradeModsSandXonly.IsExecutable                  = runSandXonly;
        CraftableUpgradeModsSandXonly.RecipeCostPriceMultiplier     = 1;
		
        var CustomModsGaloreNuke = Script<CustomModsGaloreNuke>();
        CustomModsGaloreNuke.IsExecutable                               = false;
        if (runSandXonly)
        {
            CustomModsGaloreNuke.MinProcModLimit                        = 4;
            CustomModsGaloreNuke.RecipeCostPriceMultiplier              = 1;
        }
        var CustomModsGalore = Script<CustomModsGalore>();
        CustomModsGalore.IsExecutable                               = enableCustomModsGalore;
        if (runSandXonly)
        {
            CustomModsGalore.MinProcModLimit                        = 4;
            CustomModsGalore.RecipeCostPriceMultiplier              = 1;
        }

        Script<CustomDebugOptions>().IsExecutable                   = false;
        Script<CustomDescriptions>().IsExecutable                   = false;

        var  DerelictSpeedIncrease = Script<DerelictSpeedIncrease>();
        DerelictSpeedIncrease.IsExecutable                          = true;
        DerelictSpeedIncrease.SpeedMultiplier                       = 0.5f;

        var EqualPlantTimers = Script<EqualPlantTimers>();
        EqualPlantTimers.IsExecutable                               = true;
        EqualPlantTimers.HarvestAmount                              = 50;
        EqualPlantTimers.PlantTimer                                 = 3600; // time in seconds => 60 min

        var ExocraftRechargeRate = Script<ExocraftRechargeRate>();
        ExocraftRechargeRate.IsExecutable                           = true;
        ExocraftRechargeRate.RechargeRate                           = 15f;

        Script<ExtendedExocraftAndShipScanner>().IsExecutable       = true;
        Script<FuelEconomy>().IsExecutable                          = true;
        Script<GalaxyMapUpgrade>().IsExecutable                     = false;

        //Inventory Edits
        Script<InventoryRebalance>().IsExecutable                   = balancedInventory;
        Script<InventoryUnbalance>().IsExecutable                   = false;

        var InventoryRebalanceParams = Script<InventoryRebalanceParams>();
        InventoryRebalanceParams.IsExecutable                       = !balancedInventory; //this mod changes the values for BOTH normal AND survival/perma
        InventoryRebalanceParams.SubstanceDefaultStackSizeNormal    = 50000;
        InventoryRebalanceParams.ProductDefaultStackSizeNormal      = 50;
        InventoryRebalanceParams.TechWidthNormal                    = 8;
        InventoryRebalanceParams.TechHeightNormal                   = 4;
        InventoryRebalanceParams.RefundNormal                       = 0.75f;

        Script<KeepTalkingChef>().IsExecutable                      = true;

        var LearnMoreWords = Script<LearnMoreWords>();
        LearnMoreWords.IsExecutable                                 = true;
        LearnMoreWords.AddWordsTotal                                = 20;
        LearnMoreWords.PercentageChance                             = 100;
        
        var LivingShipReducedTimer = Script<LivingShipReducedTimer>();
        LivingShipReducedTimer.IsExecutable                         = true;
        LivingShipReducedTimer.Multiplier                           = 0.001f;

        Script<MaxUpgradeFreighterSlotAllClasses>().IsExecutable    = true;
        Script<MaxUpgradeFreighterSlotAllClasses48>().IsExecutable  = false;
        Script<MoreAndCheaperStarMaps>().IsExecutable               = true;

        var MoreSalvageData = Script<MoreSalvageData>();
        MoreSalvageData.IsExecutable                                = true;
        MoreSalvageData.Min                                         = 5;
        MoreSalvageData.Max                                         = 15;

        Script<NoLadderAutoGrab>().IsExecutable                     = true;
        Script<NoPortalCharge>().IsExecutable                       = true;
        
        Script<NoShipStart>().IsExecutable                          = noShipStart;
        Script<PickUpGeoBays>().IsExecutable                        = !noShipStart; // this is already included in NoShipStart

        var PirateTimerRedux = Script<PirateTimerRedux>();
        PirateTimerRedux.IsExecutable                               = true;
        PirateTimerRedux.Multiplier                                 = 3;

        Script<QuickSilverRewards>().IsExecutable                   = true;
        
        Script<RealisticTimers>().IsExecutable                      = challengeMode;

        Script<ReducedPulseSpeedLines>().IsExecutable               = true;
        Script<RepeatInventoryExpansion>().IsExecutable             = true;

        //SettlementTimerReduction Edits
        var SettlementTimerReduction = Script<SettlementTimerReduction>();
        SettlementTimerReduction.IsExecutable                       = true;
        SettlementTimerReduction.Multiplier                         = 0.25f;  // 25% of vanilla value

        // CoreMissionEdits might not be necessary, DEBUG options edits work just fine
        Script<SkipTutorial>().IsExecutable                         = false; //!noShipStart; // also in no shipstart

        var SlotMaster = Script<SlotMaster>();
        SlotMaster.IsExecutable                                     = true;
        SlotMaster.ImproveShip                                      = false;
        SlotMaster.ImproveWeapon                                    = false;
        SlotMaster.ImproveVehicle                                   = false; // Already in unique exocrafts
        SlotMaster.ImproveAlien                                     = true;
        SlotMaster.ImproveInventory                                 = false;
        SlotMaster.ImproveFreighter                                 = false;

        Script<SustainAbility>().IsExecutable                       = true;
        Script<TaintedMetalCrafting>().IsExecutable                 = true;

        var UninstallCoreWeapons = Script<UninstallCoreWeapons>();
        UninstallCoreWeapons.IsExecutable                           = true;
        UninstallCoreWeapons.UninstallExtra                         = uninstallExtraTech;
        
        Script<UniqueExocrafts>().IsExecutable                      = true;
        Script<UniqueSpaceShips>().IsExecutable                     = true;
        Script<InstantActions>().IsExecutable                       = true;
        Script<InstantTextDisplay>().IsExecutable                   = true;

        var InstantScan = Script<InstantScan>();
        InstantScan.IsExecutable                                    = true;
        InstantScan.ScanTime                                        = 0f;

        Script<SpawnRateForClasses>().IsExecutable                  = false; // has effect on Seed value so won't use
        
        var FastRefiners = Script<FastRefiners>();
        FastRefiners.IsExecutable                                   = true;
        FastRefiners.TimeToMake                                     = 1f;
        
        Script<test>().IsExecutable                                 = false;
    }
}
//..............PickUpGeoBaysAndHarvesters.............................................
