//=============================================================================
// Specify common usings here, so don't have to put in each script.
// The utility script Assembly is included in each query script project
// and each mod project.  This utility (Global) script is also included in
// each query script project and each mod project.
//=============================================================================

global using global::System;
global using global::System.Collections;
global using global::System.Collections.Generic;
global using global::System.IO;
global using global::System.Reflection;
global using global::System.Text;
global using global::System.Text.RegularExpressions;
global using global::System.Threading;
global using global::System.Threading.Tasks;

global using global::libMBIN.NMS.Globals;
global using global::libMBIN.NMS.GameComponents;
global using global::libMBIN.NMS.Toolkit;
global using global::libMBIN.NMS.SketchNodes;

global using global::cmk;
global using global::cmk.NMS;  // required for some extension methods

global using nms = global::libMBIN.NMS;
global using NMS = global::cmk.NMS;

global using LanguageId = global::cmk.NMS.Game.Language.Identifier;

//=============================================================================
// libMBIN enum aliases:
//=============================================================================

global using AerialViewModeEnum                 = global::libMBIN.NMS.GameComponents.GcCameraAerialViewData.AerialViewModeEnum;
global using AIFactionEnum                      = global::libMBIN.NMS.GameComponents.GcRealityCommonFactions.AIFactionEnum;
global using AlienRaceEnum                      = global::libMBIN.NMS.GameComponents.GcAlienRace.AlienRaceEnum;
global using AutoStartEnum                      = global::libMBIN.NMS.GameComponents.GcGenericMissionSequence.AutoStartEnum;
global using BaseTerrainEditShapeEnum           = global::libMBIN.NMS.GameComponents.GcBaseBuildingEntry.BaseTerrainEditShapeEnum;
global using BehaviourFlagsEnum                 = global::libMBIN.NMS.GameComponents.GcProjectileData.BehaviourFlagsEnum;
global using BiomeEnum                          = global::libMBIN.NMS.GameComponents.GcBiomeType.BiomeEnum;
global using BootModeEnum                       = global::libMBIN.NMS.Globals.GcDebugOptions.BootModeEnum;
global using BuildingClassEnum                  = global::libMBIN.NMS.GameComponents.GcBuildingClassification.BuildingClassEnum;
global using BuildingDensityEnum                = global::libMBIN.NMS.GameComponents.GcBuildingDensityLevels.BuildingDensityEnum;
global using BuildingLocationEnum               = global::libMBIN.NMS.GameComponents.GcScanEventData.BuildingLocationEnum;
global using BuildingTypeEnum                   = global::libMBIN.NMS.GameComponents.GcScanEventData.BuildingTypeEnum;
global using CompletionRequirementEnum          = global::libMBIN.NMS.GameComponents.GcMaintenanceElement.CompletionRequirementEnum;
global using ConditionTestEnum                  = global::libMBIN.NMS.GameComponents.GcMissionConditionTest.ConditionTestEnum;
global using CurrencyEnum                       = global::libMBIN.NMS.GameComponents.GcCurrency.CurrencyEnum;
global using DependsOnHotspotsEnum              = global::libMBIN.NMS.GameComponents.GcBaseLinkGridData.DependsOnHotspotsEnum;
global using EventStartTypeEnum                 = global::libMBIN.NMS.GameComponents.GcScanEventData.EventStartTypeEnum;
global using FocusTypeEnum                      = global::libMBIN.NMS.Toolkit.TkModelRendererData.FocusTypeEnum;
global using FrigateStatTypeEnum                = global::libMBIN.NMS.GameComponents.GcFrigateStatType.FrigateStatTypeEnum;
global using FrigateTraitStrengthEnum           = global::libMBIN.NMS.GameComponents.GcFrigateTraitStrength.FrigateTraitStrengthEnum;
global using GalaxyStarTypeEnum                 = global::libMBIN.NMS.GameComponents.GcGalaxyStarTypes.GalaxyStarTypeEnum;
global using InteractionTypeEnum                = global::libMBIN.NMS.GameComponents.GcInteractionType.InteractionTypeEnum;
global using InventoryClassEnum                 = global::libMBIN.NMS.GameComponents.GcInventoryClass.InventoryClassEnum;
global using InventoryStackLimitsOptionDataEnum = global::libMBIN.NMS.GameComponents.GcDifficultyConfig.InventoryStackLimitsOptionDataEnum;
global using InventoryTypeEnum                  = global::libMBIN.NMS.GameComponents.GcInventoryType.InventoryTypeEnum;
global using MaterialFlagEnum                   = global::libMBIN.NMS.Toolkit.TkMaterialFlags.MaterialFlagEnum;
global using MaxSubstanceStackSizesEnum         = global::libMBIN.NMS.GameComponents.GcDifficultyInventoryStackSizeOptionData.MaxSubstanceStackSizesEnum;
global using ModelViewsEnum                     = global::libMBIN.NMS.GameComponents.GcModelViewCollection.ModelViewsEnum;
global using PlanetSizeEnum                     = global::libMBIN.NMS.GameComponents.GcPlanetSize.PlanetSizeEnum;
global using PlayerSpawnLocationOverrideEnum    = global::libMBIN.NMS.Globals.GcDebugOptions.PlayerSpawnLocationOverrideEnum;
global using ProductCategoryEnum                = global::libMBIN.NMS.GameComponents.GcProductCategory.ProductCategoryEnum;
global using QualityEnum                        = global::libMBIN.NMS.GameComponents.GcProceduralTechnologyData.QualityEnum;
global using RaceEnum                           = global::libMBIN.NMS.GameComponents.GcAlienRace.AlienRaceEnum;
global using RewardChoiceEnum                   = global::libMBIN.NMS.GameComponents.GcRewardTableItemList.RewardChoiceEnum;
global using ScanIconTypeEnum                   = global::libMBIN.NMS.GameComponents.GcScannerIconTypes.ScanIconTypeEnum;
global using ScannableTypeEnum                  = global::libMBIN.NMS.GameComponents.GcScannableComponentData.ScannableTypeEnum;
global using ScreenFilterEnum                   = global::libMBIN.NMS.GameComponents.GcScreenFilters.ScreenFilterEnum;
global using ShipClassEnum                      = global::libMBIN.NMS.GameComponents.GcSpaceshipClasses.ShipClassEnum;
global using SizeTypeEnum                       = global::libMBIN.NMS.GameComponents.GcInventoryLayoutSizeType.SizeTypeEnum;
global using SolarSystemBootEnum                = global::libMBIN.NMS.Globals.GcDebugOptions.SolarSystemBootEnum;
global using SolarSystemClassEnum               = global::libMBIN.NMS.GameComponents.GcSolarSystemClass.SolarSystemClassEnum;
global using StatMessageTypeEnum                = global::libMBIN.NMS.GameComponents.GcLeveledStatData.StatMessageTypeEnum;
global using StatsTypeEnum                      = global::libMBIN.NMS.GameComponents.GcStatsTypes.StatsTypeEnum;
global using TechnologyRarityEnum               = global::libMBIN.NMS.GameComponents.GcTechnologyRarity.TechnologyRarityEnum;
global using TradeCategoryEnum                  = global::libMBIN.NMS.GameComponents.GcTradeCategory.TradeCategoryEnum;
global using UnlockableItemTreeEnum             = global::libMBIN.NMS.GameComponents.GcUnlockableItemTreeGroups.UnlockableItemTreeEnum;
global using WealthClassEnum                    = global::libMBIN.NMS.GameComponents.GcWealthClass.WealthClassEnum;
global using WeatherEnum                        = global::libMBIN.NMS.GameComponents.GcWeatherOptions.WeatherEnum;
global using WeightingCurveEnum                 = global::libMBIN.NMS.GameComponents.GcWeightingCurve.WeightingCurveEnum;
global using wordcategorytableEnumEnum          = global::libMBIN.NMS.GameComponents.GcWordCategoryTableEnum.wordcategorytableEnumEnum;

//=============================================================================
