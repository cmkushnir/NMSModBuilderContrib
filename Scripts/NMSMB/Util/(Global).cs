//=============================================================================
// Specify common usings here, so don't have to put in each utility script.
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

global using LangId = global::cmk.NMS.Game.Language.Identifier;

//=============================================================================
// libMBIN enum aliases:
//=============================================================================

global using AerialViewModeEnum              = global::libMBIN.NMS.GameComponents.GcCameraAerialViewData.AerialViewModeEnum;
global using AlienRaceEnum                   = global::libMBIN.NMS.GameComponents.GcAlienRace.AlienRaceEnum;
global using AutoStartEnum                   = global::libMBIN.NMS.GameComponents.GcGenericMissionSequence.AutoStartEnum;
global using BaseTerrainEditShapeEnum        = global::libMBIN.NMS.GameComponents.GcBaseBuildingEntry.BaseTerrainEditShapeEnum;
global using BiomeEnum                       = global::libMBIN.NMS.GameComponents.GcBiomeType.BiomeEnum;
global using BootModeEnum                    = global::libMBIN.NMS.Globals.GcDebugOptions.BootModeEnum;
global using BuildingClassEnum               = global::libMBIN.NMS.GameComponents.GcBuildingClassification.BuildingClassEnum;
global using BuildingLocationEnum            = global::libMBIN.NMS.GameComponents.GcScanEventData.BuildingLocationEnum;
global using CompletionRequirementEnum       = global::libMBIN.NMS.GameComponents.GcMaintenanceElement.CompletionRequirementEnum;
global using ConditionTestEnum               = global::libMBIN.NMS.GameComponents.GcMissionConditionTest.ConditionTestEnum;
global using CurrencyEnum                    = global::libMBIN.NMS.GameComponents.GcCurrency.CurrencyEnum;
global using DependsOnHotspotsEnum           = global::libMBIN.NMS.GameComponents.GcBaseLinkGridData.DependsOnHotspotsEnum;
global using EventStartTypeEnum              = global::libMBIN.NMS.GameComponents.GcScanEventData.EventStartTypeEnum;
global using FrigateStatTypeEnum             = global::libMBIN.NMS.GameComponents.GcFrigateStatType.FrigateStatTypeEnum;
global using FrigateTraitStrengthEnum        = global::libMBIN.NMS.GameComponents.GcFrigateTraitStrength.FrigateTraitStrengthEnum;
global using GalaxyStarTypeEnum              = global::libMBIN.NMS.GameComponents.GcGalaxyStarTypes.GalaxyStarTypeEnum;
global using InteractionTypeEnum             = global::libMBIN.NMS.GameComponents.GcInteractionType.InteractionTypeEnum;
global using InventoryClassEnum              = global::libMBIN.NMS.GameComponents.GcInventoryClass.InventoryClassEnum;
global using InventoryTypeEnum               = global::libMBIN.NMS.GameComponents.GcInventoryType.InventoryTypeEnum;
global using MaterialFlagEnum                = global::libMBIN.NMS.Toolkit.TkMaterialFlags.MaterialFlagEnum;
global using PlanetSizeEnum                  = global::libMBIN.NMS.GameComponents.GcPlanetSize.PlanetSizeEnum;
global using PlayerSpawnLocationOverrideEnum = global::libMBIN.NMS.Globals.GcDebugOptions.PlayerSpawnLocationOverrideEnum;
global using ProductCategoryEnum             = global::libMBIN.NMS.GameComponents.GcProductCategory.ProductCategoryEnum;
global using QualityEnum                     = global::libMBIN.NMS.GameComponents.GcProceduralTechnologyData.QualityEnum;
global using RaceEnum                        = global::libMBIN.NMS.GameComponents.GcAlienRace.AlienRaceEnum;
global using RewardChoiceEnum                = global::libMBIN.NMS.GameComponents.GcRewardTableItemList.RewardChoiceEnum;
global using ScanIconTypeEnum                = global::libMBIN.NMS.GameComponents.GcScannerIconTypes.ScanIconTypeEnum;
global using ScannableTypeEnum               = global::libMBIN.NMS.GameComponents.GcScannableComponentData.ScannableTypeEnum;
global using ShipClassEnum                   = global::libMBIN.NMS.GameComponents.GcSpaceshipClasses.ShipClassEnum;
global using SizeTypeEnum                    = global::libMBIN.NMS.GameComponents.GcInventoryLayoutSizeType.SizeTypeEnum;
global using SolarSystemBootEnum             = global::libMBIN.NMS.Globals.GcDebugOptions.SolarSystemBootEnum;
global using SolarSystemClassEnum            = global::libMBIN.NMS.GameComponents.GcSolarSystemClass.SolarSystemClassEnum;
global using StatMessageTypeEnum             = global::libMBIN.NMS.GameComponents.GcLeveledStatData.StatMessageTypeEnum;
global using StatsTypeEnum                   = global::libMBIN.NMS.GameComponents.GcStatsTypes.StatsTypeEnum;
global using TechnologyRarityEnum            = global::libMBIN.NMS.GameComponents.GcTechnologyRarity.TechnologyRarityEnum;
global using TradeCategoryEnum               = global::libMBIN.NMS.GameComponents.GcTradeCategory.TradeCategoryEnum;
global using UnlockableItemTreeEnum          = global::libMBIN.NMS.GameComponents.GcUnlockableItemTreeGroups.UnlockableItemTreeEnum;
global using WealthClassEnum                 = global::libMBIN.NMS.GameComponents.GcWealthClass.WealthClassEnum;
global using WeightingCurveEnum              = global::libMBIN.NMS.GameComponents.GcWeightingCurve.WeightingCurveEnum;
global using WordEnum                        = global::libMBIN.NMS.GameComponents.GcWordCategoryTableEnum.gcwordcategorytableEnumEnum;

//=============================================================================
