//=============================================================================
// Author: Jackty89
//=============================================================================

public class CustomModsGalore : cmk.NMS.Script.ModClass
{
    public int MinProcModLimit                  = 1;
    public int RecipeCostPriceMultiplier        = 1;

    readonly string CostTypeNanite              = "NANITES";
    readonly string ShipRootTech                = "SHIPJUMP1";
    readonly string SuitRootTech                = "ENERGY";
    readonly string WeaponRootTech              = "LASER";
    readonly string ExoRootTech                 = "VEHICLE_ENGINE";
    //readonly string FreighterRootTech          = "FRIGATE_FUEL_1";
    //readonly string FactoryTreeTech            = "PRODFUEL2";
    readonly string[] Classes                   = { "C", "B", "A", "S" };

    readonly QualityEnum[] QualityEnums = {
        QualityEnum.Normal,
        QualityEnum.Rare,
        QualityEnum.Epic,
        QualityEnum.Legendary
    };

    readonly Tuple <LanguageId , string, string>[] CustomLangNameStrings = new  Tuple<LanguageId, string, string>[]
    {
        new (LanguageId.English, "CL_WEAP_NAME", "Custom weapon"),
        new (LanguageId.English, "CL_SHOT_NAME", "Custom Scatter-blaster weapon"),
        new (LanguageId.English, "CL_JET_NAME", "Custom jetpack"),
        new (LanguageId.English, "CL_SHIP_NAME", "Customship"),
        new (LanguageId.English, "CL_SHIELD_NAME", "Custom shield mod massively increases health and shields"),
        
        new (LanguageId.English, "CL_ENGY_NAME", "Custom Hazard Module"),
		
		new (LanguageId.English, "CL_DRIFT_NAME", "Custom Wheel"),
		new (LanguageId.English, "CL_EXOB_NAME", "Custom Exocraft Boost"),
		new (LanguageId.English, "CL_COLD_NAME", "Custom Cold Protection"),
        new (LanguageId.English, "CL_HEAT_NAME", "Custom Heat Protection"),
        new (LanguageId.English, "CL_TOXIC_NAME", "Custom Toxi Protection"),
        new (LanguageId.English, "CL_RAD_NAME", "Custom Radiation Protection"),

        new (LanguageId.English, "CL_ROCK_TECH1", "Large Missle Tubes"),
        new (LanguageId.English, "CL_ROCK_TECH2", "Missile Cooling vents"),
        new (LanguageId.English, "CL_ROCK_TECH3", "High Yield Missles"),
        new (LanguageId.English, "CL_MINI_TECH1", "HE Rounds"),
    };
    readonly Tuple<LanguageId, string, string>[] CustomLangDescStrings = new Tuple<LanguageId, string, string>[]
    {
        new (LanguageId.English, "CL_WEAP_DESC", "A custom procedural weapon"),
        new (LanguageId.English, "CL_SHOT_DESC", "Add some bounce and damege to the scatter blaster"),
        new (LanguageId.English, "CL_JET_DESC", "Jetpack mod that will yeet you to space and beyond"),
		new (LanguageId.English, "CL_DRIFT_DESC", "When the eurobeat kicks in"),
		new (LanguageId.English, "CL_EXOB_DESC", "When you need a little extra oompf for the mech boost"),
		new (LanguageId.English, "CL_SHIP_DESC", "A custom procedural ship mod"),
        new (LanguageId.English, "CL_SHIELD_DESC", "A custom procedural shield mod adding armour and health beyond what has seen before"),
        new (LanguageId.English, "CL_ENGY_DESC", "A custom procedural hazard mod"),
        
        new (LanguageId.English, "CL_PRCOLD_DESC", "Protection against freezing cold"),
        new (LanguageId.English, "CL_PRHEAT_DESC", "Protection against scortching heat"),
        new (LanguageId.English, "CL_PRTOXIC_DESC", "Protection against toxic toxins"),
        new (LanguageId.English, "CL_PRRAD_DESC", "Protection against radioactive radiations"),
        
        new (LanguageId.English, "CL_ROCK_DESC1", "These rocket pods hold more rockets, this might affect accuracry"),
        new (LanguageId.English, "CL_ROCK_DESC2", "Cooling vents to missile tubes. Improves fire rate cooldown and more"),
        new (LanguageId.English, "CL_ROCK_DESC3", "Increases blast radius, careful to not be caught by it"),
        new (LanguageId.English, "CL_MINI_DESC1", "Blast rounds, bring the pain"),
    };

    protected class CustomTemplate
    {
        public string TemplateBaseID;
        public string TemplateID;
        public string RequiredTech;
        public string IconFileName;
        public string Group;
        public StatsTypeEnum StatsType;
    }
    List<CustomTemplate> CustomTemplates = new List<CustomTemplate>()
    {
        new CustomTemplate{
            TemplateBaseID = "T_SHIPGUN",
            TemplateID     = "TC_SHIPROCKETS",
            RequiredTech   = "SHIPROCKETS",
            IconFileName   = "TEXTURES/UI/FRONTEND/ICONS/TECHNOLOGY/RENDER.ROCKETMOD.DDS",
            Group          = "SHIP_ROCKETS_NAME_L",
            StatsType      = StatsTypeEnum.Ship_Weapons_Rockets
        },
        new CustomTemplate{
            TemplateBaseID = "T_BOLT",
            TemplateID     = "TC_FLAME",
            RequiredTech   = "FLAME",
            IconFileName   = "TEXTURES/UI/FRONTEND/ICONS/TECHNOLOGY/RENDER.FLAMETHROWER.DDS",
            Group          = "FLAMETHROW_NAME_L",
            StatsType      = StatsTypeEnum.Weapon_Flame
        },
        new CustomTemplate{
            TemplateBaseID = "T_HAZ",
            TemplateID     = "TC_HAZ",
            RequiredTech   = "",
            IconFileName   = "TEXTURES/UI/FRONTEND/ICONS/TECHNOLOGY/RENDER.PROTECTGENERICMOD.DDS",
            Group          = "UI_HAZARD_NAME_CORE_L",
            StatsType      = StatsTypeEnum.Suit_Protection
        }
    };
    //====================================================================
    protected class CustomUpgradeTech
    {
        public string BaseTechID;
        public string NewTechID;
        public string RequiredTech;
        public string Name;
        public string Description;
        public int FragmentCost;
        public TechnologyRarityEnum TechnologyRarity;
        public GcTechnologyCategory.TechnologyCategoryEnum TechnologyCategory;
        public string FileName;
        public List<GcStatsBonus> StatBonuses;
        public List<GcTechnologyRequirement> Requirements;

    }
    List<CustomUpgradeTech> CustomTechnology = new List<CustomUpgradeTech>(){
        new CustomUpgradeTech{
            BaseTechID         = "UT_ROCKETS", //from what tech we will we copy  as base
            NewTechID          = "UT_ROCKETS_MISS",
            RequiredTech       = "SHIPROCKETS", 
            Name               = "CL_ROCK_TECH1",
            Description        = "CL_ROCK_DESC2",
            FragmentCost       = 400,
            TechnologyRarity   = TechnologyRarityEnum.VeryRare,
            TechnologyCategory = GcTechnologyCategory.TechnologyCategoryEnum.Ship,
            FileName           = "TEXTURES/UI/FRONTEND/ICONS/TECHNOLOGY/RENDER.ROCKET.DDS",
            StatBonuses        = new List<GcStatsBonus>() {
                StatsBonus.Create(StatsTypeEnum.Ship_Weapons_Guns_BulletsPerShot, 3, 2),
                StatsBonus.Create(StatsTypeEnum.Ship_Weapons_Guns_Dispersion, 7, 1),
            },
            Requirements       = new List<GcTechnologyRequirement>(){
                TechnologyRequirement.Product("TECH_COMP", 5),
                TechnologyRequirement.Substance("RED2", 100),
                TechnologyRequirement.Product("GRENFUEL1", 10)
            }
        },
        new CustomUpgradeTech{
            BaseTechID         = "UT_ROCKETS", //from what tech we will we copy  as base
            NewTechID          = "UT_ROCKETS_COOL",
            RequiredTech       = "SHIPROCKETS",
            Name               = "CL_ROCK_TECH2",
            Description        = "CL_ROCK_DESC2",
            FragmentCost       = 600,
            TechnologyRarity   = TechnologyRarityEnum.VeryRare,
            TechnologyCategory = GcTechnologyCategory.TechnologyCategoryEnum.Ship,
            FileName           = "TEXTURES/UI/FRONTEND/ICONS/TECHNOLOGY/RENDER.ROCKET.DDS",
            StatBonuses        = new List<GcStatsBonus>() {
                StatsBonus.Create(StatsTypeEnum.Ship_Weapons_Guns_HeatTime, 1.5f, 3),
                StatsBonus.Create(StatsTypeEnum.Ship_Weapons_Guns_CoolTime, 1.3f, 3),
                StatsBonus.Create(StatsTypeEnum.Ship_Weapons_Guns_Rate, 1.25f, 1)
            },
            Requirements       = new List<GcTechnologyRequirement>(){
                TechnologyRequirement.Product("TECH_COMP", 5),
                TechnologyRequirement.Substance("GREEN2", 100),
                TechnologyRequirement.Product("GRENFUEL1", 10)
            }
        },
        new CustomUpgradeTech{
            BaseTechID         = "UT_ROCKETS", //from what tech we will we copy  as base
            NewTechID          = "UT_ROCKETS_BLAS",
            RequiredTech       = "SHIPROCKETS", 
            Name               = "CL_ROCK_TECH3", 
            Description        = "CL_ROCK_DESC3", 
            FragmentCost       = 800,
            TechnologyRarity   = TechnologyRarityEnum.VeryRare,
            TechnologyCategory = GcTechnologyCategory.TechnologyCategoryEnum.Ship,
            FileName           = "TEXTURES/UI/FRONTEND/ICONS/TECHNOLOGY/RENDER.ROCKET.DDS",
            StatBonuses        = new List<GcStatsBonus>() {
                StatsBonus.Create(StatsTypeEnum.Ship_Weapons_Guns_Damage_Radius, 10, 4)
            },
            Requirements       = new List<GcTechnologyRequirement>(){
                TechnologyRequirement.Product("TECH_COMP", 5),
                TechnologyRequirement.Substance("BLUE2", 100),
                TechnologyRequirement.Product("GRENFUEL1", 10)
            }
        },
        new CustomUpgradeTech{
            BaseTechID         = "UT_SHIPMINI", //from what tech we will we copy  as base
            NewTechID          = "UT_INFRA_BLAS",
            RequiredTech       = "SHIPMINIGUN", 
            Name               = "CL_MINI_TECH1", 
            Description        = "CL_MINI_DESC1", 
            FragmentCost       = 500,
            TechnologyRarity   = TechnologyRarityEnum.VeryRare,
            TechnologyCategory = GcTechnologyCategory.TechnologyCategoryEnum.Ship,
            FileName           = "TEXTURES/UI/FRONTEND/ICONS/TECHNOLOGY/RENDER.PHOTONACCELMOD.DDS",
            StatBonuses        = new List<GcStatsBonus>() 
            {
                StatsBonus.Create(StatsTypeEnum.Ship_Weapons_Guns_Damage_Radius, 10, 3)
            },
            Requirements       = new List<GcTechnologyRequirement>(){
                TechnologyRequirement.Product("TECH_COMP", 5),
                TechnologyRequirement.Substance("BLUE2", 100),
                TechnologyRequirement.Product("GRENFUEL1", 10)
            }
        }
    };
    //====================================================================
    protected class CustomProcMod
    {
        public string BaseTechID; //from what tech we will we copy  as base
        public string BaseDeploy;
        public bool StaticDeloy;
        public string NewTechID;
        public string TemplateName;
        public int HighestClassNo;
        public int lowestClassNo;
        public int MinStats;
        public int MaxStats;
        public float MultiplierPerRank;
        public string IconFileName;
        public string Name;
        public string ProcName;
        public string Description;
        public string TradeData;
        public List<GcProceduralTechnologyStatLevel> StatBonuses;
    }
    List<CustomProcMod> CustomProceduralMods = new List<CustomProcMod>() {
        new CustomProcMod {
            BaseTechID        = "U_SHIPGUN",
            BaseDeploy        = "UP_SGUN",
            NewTechID         = "UC_ROCKET",
            TemplateName      = "TC_SHIPROCKETS",
            HighestClassNo    = 4,
            lowestClassNo     = 1,
            MinStats          = 1,
            MaxStats          = 4,
            MultiplierPerRank = 0.2f,
            IconFileName      = "GRENADE.DDS",
            Name              = "CL_SHIP_NAME",
            ProcName          = "UP_SHIPSHOT",
            Description       = "CL_SHIP_DESC",
            TradeData         = "ShipTechSpecialist",
            StatBonuses       = new List<GcProceduralTechnologyStatLevel>() {
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Ship_Weapons_Guns_Damage,         1000,  2500,  WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Ship_Weapons_Guns_Damage_Radius,  1.25f, 1.50f, WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Ship_Weapons_Guns_BulletsPerShot, 4,     5,     WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Ship_Weapons_Guns_CoolTime,       0.5f,  0.8f,  WeightingCurveEnum.MaxIsRare, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Ship_Weapons_Guns_Dispersion,     1.15f,  1.4f, WeightingCurveEnum.MaxIsUncommon, true)
            }
        },
        new CustomProcMod {
            BaseTechID        = "U_BOLT",
            BaseDeploy        = "UP_BOLT",
            NewTechID         = "UC_FLAME",
            TemplateName      = "TC_FLAME",
            HighestClassNo    = 4,
            lowestClassNo     = 1,
            MinStats          = 1,
            MaxStats          = 4,
            MultiplierPerRank = 0.2f,
            IconFileName      = "HEAT.DDS",
            Name              = "CL_WEAP_NAME",
            ProcName          = "UP_SHOT",
            Description       = "CL_WEAP_DESC",
            TradeData         = "WeapTechSpecialist",
            StatBonuses       = new List<GcProceduralTechnologyStatLevel>() {
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Weapon_Projectile_BulletsPerShot, 0.5f, 0.75f, WeightingCurveEnum.MaxIsRare, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Weapon_Projectile_Damage,         25,   50,    WeightingCurveEnum.MaxIsRare, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Weapon_Projectile_Rate,           1.25f,    1.5f,     WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Weapon_FireDOT_DPS,               10,   15,    WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Weapon_FireDOT_Duration,          5,    10,    WeightingCurveEnum.MaxIsUncommon, true)
            }
        },
        new CustomProcMod {
			BaseTechID        = "U_ENERGY",
			BaseDeploy        = "UP_ENGY",
			StaticDeloy       = false,
			NewTechID         = "UC_ENGY",
			TemplateName      = "T_ENERGY",
			HighestClassNo    = 3,
			lowestClassNo     = 1,
			MinStats          = 2,
			MaxStats          = 2,
			MultiplierPerRank = 0.3f,
			IconFileName      = "LIFESUPPORT.DDS",
			Name              = "CL_ENGY_NAME",
			ProcName          = "UP_LIFEBOOST",
			Description       = "CL_ENGY_DESC",
			TradeData         = "SuitTechSpecialist",
			StatBonuses       = new List<GcProceduralTechnologyStatLevel>() {
				ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_Energy, 1.5f, 2.5f, WeightingCurveEnum.MaxIsUncommon, true),
				ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_Energy_Regen, 1.5f, 2.5f, WeightingCurveEnum.MaxIsUncommon, true),
			}
		},
		new CustomProcMod {
            BaseTechID        = "U_COLDPROT",
            BaseDeploy        = "UP_COLD",
            StaticDeloy       = false,
            NewTechID         = "UC_COLD",
            TemplateName      = "T_COLDPROT",
            HighestClassNo    = 3,
            lowestClassNo     = 1,
            MinStats          = 3,
            MaxStats          = 3,
            MultiplierPerRank = 0.3f,
            IconFileName      = "COLD.DDS",
            Name              = "CL_COLD_NAME",
            ProcName          = "UP_COLDPROT",
            Description       = "CL_PRCOLD_DESC",
            TradeData         = "SuitTechSpecialist",
            StatBonuses       = new List<GcProceduralTechnologyStatLevel>() {
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_Protection_Cold, 400, 500, WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_DamageReduce_Cold, 1.5f, 2.5f, WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_Protection_ColdDrain, 2, 5, WeightingCurveEnum.MaxIsUncommon, true)
            }
        },
        new CustomProcMod {
            BaseTechID        = "U_HOTPROT",
            BaseDeploy        = "UP_HOT",
            StaticDeloy       = false,
            NewTechID         = "UC_HEAT",
            TemplateName      = "T_HOTPROT",
            HighestClassNo    = 3,
            lowestClassNo     = 1,
            MinStats          = 3,
            MaxStats          = 3,
            MultiplierPerRank = 0.3f,
            IconFileName      = "HEAT.DDS",
            Name              = "CL_HEAT_NAME",
            ProcName          = "UP_HOTPROT",
            Description       = "CL_PRHEAT_DESC",
            TradeData         = "SuitTechSpecialist",
            StatBonuses       = new List<GcProceduralTechnologyStatLevel>() {
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_Protection_Heat, 400, 500, WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_DamageReduce_Heat, 1.5f, 2.5f, WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_Protection_HeatDrain, 2, 5, WeightingCurveEnum.MaxIsUncommon, true)
            }
        },
        new CustomProcMod {
            BaseTechID        = "U_TOX",
            BaseDeploy        = "UP_TOX",
            StaticDeloy       = false,
            NewTechID         = "UC_TOXIC",
            TemplateName      = "T_TOX",
            HighestClassNo    = 3,
            lowestClassNo     = 1,
            MinStats          = 3,
            MaxStats          = 3,
            MultiplierPerRank = 0.3f,
            IconFileName      = "TOXIC.DDS",
            Name              = "CL_TOXIC_NAME",
            ProcName          = "UP_TOXPROT",
            Description       = "CL_PRTOXIC_DESC",
            TradeData         = "SuitTechSpecialist",
            StatBonuses       = new List<GcProceduralTechnologyStatLevel>() {
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_Protection_Radiation, 400, 500, WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_DamageReduce_Radiation, 1.5f, 2.5f, WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_Protection_RadDrain, 2, 5, WeightingCurveEnum.MaxIsUncommon, true)
            }
        },
        new CustomProcMod {
            BaseTechID        = "U_RAD",
            BaseDeploy        = "UP_RAD",
            StaticDeloy       = false,
            NewTechID         = "UC_RAD",
            TemplateName      = "T_RAD",
            HighestClassNo    = 3,
            lowestClassNo     = 1,
            MinStats          = 3,
            MaxStats          = 3,
            MultiplierPerRank = 0.3f,
            IconFileName      = "RADIOACTIVE.DDS",
            Name              = "CL_RAD_NAME",
            ProcName          = "UP_RADPROT",
            Description       = "CL_PRRAD_DESC",
            TradeData         = "SuitTechSpecialist",
            StatBonuses       = new List<GcProceduralTechnologyStatLevel>() {
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_Protection_Toxic, 400, 500, WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_DamageReduce_Toxic, 1.5f, 2.5f, WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_Protection_ToxDrain, 2, 5, WeightingCurveEnum.MaxIsUncommon, true)
            }
        },
        new CustomProcMod {
            BaseTechID        = "U_SHIELDBOOST",
            BaseDeploy        = "UP_SHLD",
            StaticDeloy       = false,
            NewTechID         = "UC_SHLD",
            TemplateName      = "T_SHIELD",
            HighestClassNo    = 4,
            lowestClassNo     = 1,
            MinStats          = 2,
            MaxStats          = 2,
            MultiplierPerRank = 0.25f,
            IconFileName      = "HEALTH.DDS",
            Name              = "CL_SHIELD_NAME",
            ProcName          = "UP_SHIELDBOOST",
            Description       = "CL_SHIELD_DESC",
            TradeData         = "SuitTechSpecialist",
            StatBonuses       = new List<GcProceduralTechnologyStatLevel>() {
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_Armour_Shield_Strength, 0.5f, 1, WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_Armour_Health, 50, 50, WeightingCurveEnum.MaxIsUncommon, true),
            }
        },
        new CustomProcMod {
            BaseTechID        = "U_SHOTGUN",
            BaseDeploy        = "UP_SHOT",
            StaticDeloy       = false,
            NewTechID         = "UC_SHOT",
            TemplateName      = "T_SHOTGUN",
            HighestClassNo    = 4,
            lowestClassNo     = 1,
            MinStats          = 1,
            MaxStats          = 3,
            MultiplierPerRank = 0.2f,
            IconFileName      = "SHOTGUN.DDS",
            Name              = "CL_SHOT_NAME",
            ProcName          = "UP_SHOT",
            Description       = "CL_SHOT_DESC",
            TradeData         = "WeapTechSpecialist",
            StatBonuses       = new List<GcProceduralTechnologyStatLevel>() {
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Weapon_Projectile_BulletsPerShot, 0.5f, 0.75f, WeightingCurveEnum.MaxIsRare, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Weapon_Projectile_Damage,         100,   250,    WeightingCurveEnum.MaxIsRare, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Weapon_Projectile_Bounce,         5,    10,    WeightingCurveEnum.MaxIsUncommon, true)
            }
        },
        new CustomProcMod {
            BaseTechID        = "U_SHIELDBOOST",
            BaseDeploy        = "UP_JET",
            StaticDeloy       = false,
            NewTechID         = "UC_JET",
            TemplateName      = "T_JET",
            HighestClassNo    = 4,
            lowestClassNo     = 1,
            MinStats          = 2,
            MaxStats          = 4,
            MultiplierPerRank = 0.25f,
            IconFileName      = "JETPACK.DDS",
            Name              = "CL_JET_NAME",
            ProcName          = "UP_JETBOOST",
            Description       = "CL_JET_DESC",
            TradeData         = "SuitTechSpecialist",
            StatBonuses       = new List<GcProceduralTechnologyStatLevel>() {
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_Jetpack_Refill, 1.75f, 3f, WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_Jetpack_Tank, 5, 10, WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_Jetpack_Ignition, 1.5f, 2.5f, WeightingCurveEnum.MaxIsUncommon, true),
            }
        },
		new CustomProcMod {
			BaseTechID        = "U_EXOBOOST",
			BaseDeploy        = "UP_BOOST",
			StaticDeloy       = false,
			NewTechID         = "UC_DRIFT",
			TemplateName      = "T_BOOST",
			HighestClassNo    = 4,
			lowestClassNo     = 1,
			MinStats          = 1,
			MaxStats          = 3,
			MultiplierPerRank = 0.25f,
			IconFileName      = "VEHICLEBOOST.DDS",
			Name              = "CL_DRIFT_NAME",
			ProcName          = "UP_JETBOOST",
			Description       = "CL_DRIFT_DESC",
			TradeData         = "VehicleTechSpecialist",
			StatBonuses       = new List<GcProceduralTechnologyStatLevel>() {
				ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Vehicle_SkidGrip, -0.05f, -0.1f, WeightingCurveEnum.MaxIsUncommon, true),
				ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Vehicle_BoostTanks, 0.6f, 0.7f, WeightingCurveEnum.MaxIsUncommon, true),
				ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Vehicle_BoostSpeed, 0.75f, 0.95f, WeightingCurveEnum.MaxIsUncommon, true),
			}
		},
		new CustomProcMod {
			BaseTechID        = "U_EXOBOOST",
			BaseDeploy        = "UP_BOOST",
			StaticDeloy       = false,
			NewTechID         = "UC_EXOB",
			TemplateName      = "T_BOOST",
			HighestClassNo    = 4,
			lowestClassNo     = 1,
			MinStats          = 1,
			MaxStats          = 3,
			MultiplierPerRank = 0.25f,
			IconFileName      = "VEHICLEBOOST.DDS",
			Name              = "CL_EXOB_NAME",
			ProcName          = "UP_JETBOOST",
			Description       = "CL_EXOB_DESC",
			TradeData         = "VehicleTechSpecialist",
			StatBonuses       = new List<GcProceduralTechnologyStatLevel>() {
				ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Vehicle_BoostTanks, 2.5f, 5, WeightingCurveEnum.MaxIsUncommon, true),
				ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Vehicle_BoostSpeed, 1, 2, WeightingCurveEnum.MaxIsUncommon, true)
			}
		}

	};
    //====================================================================
    protected class TreeExpansion
    {
        public GcUnlockableItemTreeGroups.UnlockableItemTreeEnum Tree;
        public string RootTech;
        public string CostType;
    }
    //====================================================================
    protected class CraftableUpgradeMod
    {
        public string UpgradeBase;
        public int HighestClassNo;
        public int LowestClassNo;
    }
    List<CraftableUpgradeMod> ShipMods = new List<CraftableUpgradeMod>() {
        new CraftableUpgradeMod { UpgradeBase = "UC_ROCKET", HighestClassNo = 4, LowestClassNo = 1 },
    };
    List<CraftableUpgradeMod> WeaponMods = new List<CraftableUpgradeMod>() {
        new CraftableUpgradeMod { UpgradeBase = "UC_FLAME", HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "UC_SHOT", HighestClassNo = 4, LowestClassNo = 1 }
    };
    List<CraftableUpgradeMod> SuitMods = new List<CraftableUpgradeMod>() {
        new CraftableUpgradeMod { UpgradeBase = "UC_ENGY", HighestClassNo = 3, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "UC_COLD", HighestClassNo = 3, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "UC_HEAT", HighestClassNo = 3, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "UC_TOXIC", HighestClassNo = 3, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "UC_RAD", HighestClassNo = 3, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "UC_SHLD", HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "UC_JET", HighestClassNo = 4, LowestClassNo = 1 }

    };
    List<CraftableUpgradeMod> VehicleMods = new List<CraftableUpgradeMod>() {
        new CraftableUpgradeMod { UpgradeBase = "UC_DRIFT", HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "UC_EXOB", HighestClassNo = 4, LowestClassNo = 1 }
    };
    //====================================================================
    protected class RequirementPerClass
    {
        public GcTechnologyRequirement[] Materials = new GcTechnologyRequirement[3];
        public int Price;
    }

    readonly GcInventoryType Product   = new GcInventoryType { InventoryType = InventoryTypeEnum.Product };
    readonly GcInventoryType Substance = new GcInventoryType { InventoryType = InventoryTypeEnum.Substance };

    protected RequirementPerClass[] Requirements;

    //====================================================================
    protected class Data
    {
        public TreeExpansion Tree;
        public List<List<CraftableUpgradeMod>> Mods;
    }
    //====================================================================
    protected override void Execute()
    {
        TreeExpansion ShipTreeExpansion     = new TreeExpansion { Tree = UnlockableItemTreeEnum.ShipTech,     CostType = CostTypeNanite, RootTech = ShipRootTech };
        TreeExpansion WeaponTreeExpansion   = new TreeExpansion { Tree = UnlockableItemTreeEnum.WeapTech,     CostType = CostTypeNanite, RootTech = WeaponRootTech };
        TreeExpansion ExoCraftTreeExpansion = new TreeExpansion { Tree = UnlockableItemTreeEnum.ExocraftTech, CostType = CostTypeNanite, RootTech = ExoRootTech };
        TreeExpansion SuitTreeExpansion     = new TreeExpansion { Tree = UnlockableItemTreeEnum.SuitTech,     CostType = CostTypeNanite, RootTech = SuitRootTech };

        FillRequirementsArray();

        Data[] AllModData = new[] {
            new Data { Tree = ShipTreeExpansion,   Mods = new(){ ShipMods }},
            new Data { Tree = WeaponTreeExpansion, Mods = new(){ WeaponMods }},
            new Data { Tree = SuitTreeExpansion,   Mods = new(){ SuitMods }},
            new Data { Tree = ExoCraftTreeExpansion,   Mods = new(){ VehicleMods }},
        };
        CreateCustomTempates();
        CreateCustomTech();
        CreateCustomProceduralMods();
        GcUnlockableTrees();
        EditExistingTech();

        foreach (var ModData in AllModData)
        {
            TreeExpansion Expansion                    = ModData.Tree;
            List<List<CraftableUpgradeMod>> ListOfMods = ModData.Mods;
            foreach (var Mods in ListOfMods)
            {
                SetCraftabletoTrueAndAddRequirements(Mods);
                AddUnlockableTrees(Mods, Expansion);
            }
        }

        foreach (var LanguageData in CustomLangNameStrings)
        {
            LanguageId language    = LanguageData.Item1;
            string languageID      = LanguageData.Item2;
            string languageIDL     = languageID + "_L";            
            string languageString  = LanguageData.Item3;
            string languageStringU = languageString.ToUpper();

            AddLanguageStrings(language, languageIDL, languageString);
            AddLanguageStrings(language, languageID, languageStringU);
        }

        foreach (var LanguageData in CustomLangDescStrings)
        {
            LanguageId language   = LanguageData.Item1;
            string languageID     = LanguageData.Item2;
            string languageString = LanguageData.Item3;

            AddLanguageStrings(language, languageID, languageString);
        }
    }

    protected void FillRequirementsArray()
    {
        Requirements = new []{
            //C-Class
            new RequirementPerClass{
                Materials = new [] {
                    new GcTechnologyRequirement { ID = "EX_YELLOW", Type = Substance, Amount = 100},
                    new GcTechnologyRequirement { ID = "TECH_COMP", Type = Product,   Amount = 1 },
                    new GcTechnologyRequirement { ID = "STELLAR2",  Type = Substance, Amount = 500}
                },
                Price = 1000 * RecipeCostPriceMultiplier
            },
            //B-Class
            new RequirementPerClass{
                Materials = new [] {
                    new GcTechnologyRequirement { ID = "EX_RED",    Type = Substance, Amount = 200},
                    new GcTechnologyRequirement { ID = "TECH_COMP", Type = Product,   Amount = 2},
                    new GcTechnologyRequirement { ID = "STELLAR2",  Type = Substance, Amount = 500}
                },
                Price = 2500 * RecipeCostPriceMultiplier
            },
            //A-Class
            new RequirementPerClass{
                Materials = new [] {
                    new GcTechnologyRequirement { ID = "EX_GREEN",  Type = Substance, Amount = 300},
                    new GcTechnologyRequirement { ID = "TECH_COMP", Type = Product,   Amount = 3},
                    new GcTechnologyRequirement { ID = "STELLAR2",  Type = Substance, Amount = 500}
                },
                Price = 5000 * RecipeCostPriceMultiplier
            },
            //S-Class
            new RequirementPerClass{
                Materials = new [] {
                    new GcTechnologyRequirement { ID = "EX_BLUE",   Type = Substance, Amount = 500},
                    new GcTechnologyRequirement { ID = "TECH_COMP", Type = Product,   Amount = 5},
                    new GcTechnologyRequirement { ID = "STELLAR2",  Type = Substance, Amount = 500}
                },
                Price = 10000 * RecipeCostPriceMultiplier
            },
            //X-Class
            new RequirementPerClass{
                Materials = new [] {
                    new GcTechnologyRequirement { ID = "EX_RED",    Type = Substance, Amount = 300},
                    new GcTechnologyRequirement { ID = "EX_BLUE",   Type = Substance, Amount = 300},
                    new GcTechnologyRequirement { ID = "TECH_COMP", Type = Product,   Amount = 5}
                },
                Price = 5000 * RecipeCostPriceMultiplier
            }
        };
    }

    // This will add extra statbonus/re
    protected void EditExistingTech()    
    {
        var techMbin    = ExtractMbin<GcTechnologyTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCTECHNOLOGYTABLE.MBIN");
        var shipRockets = techMbin.Table.Find(TECH => TECH.ID == "SHIPROCKETS");

        shipRockets.StatBonuses.Find(STAT => STAT.Stat.StatsType == StatsTypeEnum.Ship_Weapons_Guns_CoolTime).Bonus   = 1;
        shipRockets.StatBonuses.Find(STAT => STAT.Stat.StatsType == StatsTypeEnum.Ship_Weapons_Guns_HeatTime).Bonus   = 1;
        shipRockets.StatBonuses.Find(STAT => STAT.Stat.StatsType == StatsTypeEnum.Ship_Weapons_Guns_Dispersion).Bonus = 1;
        shipRockets.StatBonuses.Add(new() {
            Stat = new GcStatsTypes{ StatsType = StatsTypeEnum.Ship_Weapons_Guns_BulletsPerShot  },
            Bonus = 1,  
            Level = 1
        });
        
        var addBounce       = new GcStatsBonus { Stat = new GcStatsTypes { StatsType = StatsTypeEnum.Weapon_Projectile_Bounce }, Bonus = 2,   Level = 3 };
        var moreProjectiles = new GcStatsBonus { Stat = new GcStatsTypes { StatsType = StatsTypeEnum.Weapon_Projectile },        Bonus = 1,   Level = 1 };
        var addDot          = new GcStatsBonus { Stat = new GcStatsTypes { StatsType = StatsTypeEnum.Weapon_FireDOT },           Bonus = 50, Level = 1 };
        var addDotDuratiom  = new GcStatsBonus { Stat = new GcStatsTypes { StatsType = StatsTypeEnum.Weapon_FireDOT_Duration },  Bonus = 10,  Level = 1 };
        var addDotDPS       = new GcStatsBonus { Stat = new GcStatsTypes { StatsType = StatsTypeEnum.Weapon_FireDOT_DPS },       Bonus = 50, Level = 3 };

        var flame           = techMbin.Table.Find(TECH => TECH.ID == "FLAME");
        flame.StatBonuses.Find(STAT => STAT.Stat.StatsType == StatsTypeEnum.Weapon_Projectile_Dispersion).Bonus     = 15;
        flame.StatBonuses.Find(STAT => STAT.Stat.StatsType == StatsTypeEnum.Weapon_Projectile_Range).Bonus          = 250;
        flame.StatBonuses.Find(STAT => STAT.Stat.StatsType == StatsTypeEnum.Weapon_Projectile_BulletsPerShot).Bonus = 5;
        flame.StatBonuses.Find(STAT => STAT.Stat.StatsType == StatsTypeEnum.Weapon_Projectile_Damage).Bonus         = 100;
        flame.StatBonuses.Find(STAT => STAT.Stat.StatsType == StatsTypeEnum.Weapon_Projectile_Rate).Bonus           = 12;

        flame.StatBonuses.Add(addBounce);
        flame.StatBonuses.Add(moreProjectiles);
        flame.StatBonuses.Add(addDot);
        flame.StatBonuses.Add(addDotDPS);
        flame.StatBonuses.Add(addDotDuratiom);

    }

    protected void CreateCustomTempates()
    {
        var techMbin = ExtractMbin<GcTechnologyTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCTECHNOLOGYTABLE.MBIN");

        foreach (var customTemplate in CustomTemplates)
        {
            var template                = CloneMbin(techMbin.Table.Find(TECH => TECH.ID == customTemplate.TemplateBaseID));
            template.ID                 = customTemplate.TemplateID;
            template.Group              = customTemplate.Group;
            template.RequiredTech       = customTemplate.RequiredTech;
            template.Icon.Filename      = customTemplate.IconFileName;
            template.BaseStat.StatsType = customTemplate.StatsType;
            techMbin.Table.Add(template);
        }
    }
    protected void CreateCustomTech()
    {
        var mbin = ExtractMbin<GcTechnologyTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCTECHNOLOGYTABLE.MBIN");

        foreach (var customTech in CustomTechnology)
        {
            var tech                             = CloneMbin(mbin.Table.Find(TECH => TECH.ID == customTech.BaseTechID));  // clone Large Rocket Tubes
            tech.ID                              = customTech.NewTechID;
            
            tech.Name                            = customTech.Name;
            tech.NameLower                       = customTech.Name +"_L";
            
            tech.Description                     = customTech.Description;
            tech.RequiredTech                    = customTech.RequiredTech;
            tech.Rarity.TechnologyRarity         = customTech.TechnologyRarity;
            tech.TechShopRarity.TechnologyRarity = customTech.TechnologyRarity;
            tech.FragmentCost                    = customTech.FragmentCost;
            tech.Category.TechnologyCategory     = customTech.TechnologyCategory;
            tech.Icon.Filename                   = customTech.FileName;
            
            tech.Requirements.Clear();
            foreach (var requirement in customTech.Requirements)
            {
                tech.Requirements.Add(requirement);
            }

            tech.StatBonuses.Clear();
            foreach (var statBonus in customTech.StatBonuses)
            {
                tech.StatBonuses.Add(statBonus);
            }
            mbin.Table.Add(tech);
        }
    }
    //DONE
    protected void CreateCustomProceduralMods()
    {
        var Prod_mbin    = ExtractMbin<GcProductTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCPRODUCTTABLE.MBIN");
        var Proc_mbin    = ExtractMbin<GcProceduralTechnologyTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCPROCEDURALTECHNOLOGYTABLE.MBIN");
        var Reality_mbin = ExtractMbin<GcRealityManagerData>("METADATA/REALITY/DEFAULTREALITY.MBIN");

        foreach (CustomProcMod Mod in CustomProceduralMods)
        {
            string baseTechName                                  = Mod.BaseTechID;
            string baseDeployString                              = Mod.BaseDeploy;
            bool staticDeploy                                    = Mod.StaticDeloy;
            string newTechName                                   = Mod.NewTechID;
            string newTechDeployName                             = Mod.NewTechID.Replace("UC_", "UPC_"); ;
            string newTemplate                                   = Mod.TemplateName;
            int newMinStats                                      = Mod.MinStats;
            int newMaxStats                                      = Mod.MaxStats;
            float newMultiplierPerRank                           = Mod.MultiplierPerRank;
            string newIconFileName                               = Mod.IconFileName;
            string newName                                       = Mod.Name;
            string newNameL                                      = Mod.Name + "_L";
            string newDescription                                = Mod.Description;
            string newTradeData                                  = Mod.TradeData;
            string newProcgenName                                = Mod.ProcName;
            List<GcProceduralTechnologyStatLevel> newStatBonuses = Mod.StatBonuses;

            int highestClassNo = Mod.HighestClassNo;
            int lowestClassNo  = Mod.lowestClassNo;
            
            if (lowestClassNo != MinProcModLimit)
                lowestClassNo = MinProcModLimit;

            var field = Reality_mbin.TradeSettings.GetType().GetField(newTradeData);
            GcTradeData addTradePorduct = field.GetValue(Reality_mbin.TradeSettings) as GcTradeData;

            for (int i = lowestClassNo; i <= highestClassNo; i++)
            {                
                string copyTech        = baseTechName + i.ToString();
                string copyDeployTech  = baseDeployString;
                if( !staticDeploy )
                    copyDeployTech  = baseDeployString + i.ToString();

                string newTechID       = newTechName + i.ToString();
                string newTechDeployID = newTechDeployName.Replace("UPC_", "UPC_" + Classes[i-1]).ToUpper() + "_" + Classes[i-1];
                float  multiplier      = 1 + (newMultiplierPerRank * (i - 1));

                if (newMinStats < newMaxStats)
                    newMinStats++;
                
                Log.AddInformation($"Print copyDeployTech  = {copyDeployTech}");
                var proc         = CloneMbin(Proc_mbin.Table.Find(PROC => PROC.ID == copyDeployTech));
                proc.ID          = newTechDeployID;
                proc.Name        = newProcgenName;
                proc.NameLower   = newNameL;
                proc.Description = newDescription;
                proc.Template    = newTemplate;
                proc.NumStatsMin = newMinStats;
                proc.NumStatsMax = newMinStats;
                if( staticDeploy )
                    proc.Quality = QualityEnums[i-1];

                proc.StatLevels.Clear();
                foreach (var statLevel in newStatBonuses)
                {
                    statLevel.ValueMin *= multiplier;
                    statLevel.ValueMax *= multiplier;
                    proc.StatLevels.Add(statLevel);
                }
                Proc_mbin.Table.Add(proc);

                // Create custum products for the tech
                Log.AddInformation($"Print copyTech  = {copyTech}");

                var prod = CloneMbin(Prod_mbin.Table.Find(PROD => PROD.ID == copyTech));

                prod.ID          = newTechID;
                prod.Name        = newName;
                prod.NameLower   = newNameL;
                prod.Description = newDescription;
                //0........................................................58 
                //TEXTURES/UI/FRONTEND/ICONS/U4PRODUCTS/PROCTECH/PROCTECH.B. LASER.DDS
                var iconFilePath   = prod.Icon.Filename.Value.Substring(0, 58);
                prod.Icon.Filename = iconFilePath + newIconFileName;
                prod.DeploysInto   = newTechDeployID;
                Prod_mbin.Table.Add(prod);
                
                addTradePorduct.OptionalProducts.AddUnique(newTechID);
            }
        }
    }
    protected void GcUnlockableTrees()
    {
        var mbin        = ExtractMbin<GcUnlockableTrees>("METADATA/REALITY/TABLES/UNLOCKABLEITEMTREES.MBIN");
        var shipTree    = mbin.Trees[(int)GcUnlockableItemTreeGroups.UnlockableItemTreeEnum.ShipTech];
        var weapTree    = mbin.Trees[(int)UnlockableItemTreeEnum.WeapTech];

        var ship_guns   = shipTree.Trees[0].Root.Children.Find(UNLOCKABLE => UNLOCKABLE.Unlockable == "SHIPGUN1");
        var ship_rocket = ship_guns.Children.Find(UNLOCKABLE => UNLOCKABLE.Unlockable == "SHIPROCKETS");
        ship_rocket.Children.Add(new GcUnlockableItemTreeNode
        {
            Unlockable = "UT_ROCKETS_MISS",
            Children = new() 
            { 
                new GcUnlockableItemTreeNode 
                { 
                    Unlockable = "UT_ROCKETS_COOL",
                    Children = new()
                    {
                        new GcUnlockableItemTreeNode
                        {
                            Unlockable = "UT_ROCKETS_BLAS",
                            Children = new()
                        }
                    }
                } 
            }
        });

        var ship_infra = ship_guns.Children.Find(UNLOCKABLE => UNLOCKABLE.Unlockable == "SHIPMINIGUN");
        ship_infra.Children.Add(new GcUnlockableItemTreeNode
        {
            Unlockable = "UT_INFRA_BLAS",
            Children   = new()
        });

        weapTree.Trees[0].Root.Children.Insert(0, new GcUnlockableItemTreeNode
        {
            Unlockable = "FLAME",
            Children   = new()
        });


    }
    protected void SetCraftabletoTrueAndAddRequirements(List<CraftableUpgradeMod> Mods)
    {
        var prod_mbin = ExtractMbin<GcProductTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCPRODUCTTABLE.MBIN");

        foreach (var Mod in Mods)
        {
            string productBase = Mod.UpgradeBase;
            int highestClassNo = Mod.HighestClassNo;
            int lowestClassNo  = Mod.LowestClassNo;
            
            if (lowestClassNo != MinProcModLimit)
                lowestClassNo = MinProcModLimit;

            if (Mod.HighestClassNo == 0)
            {
                var prod = prod_mbin.Table.Find(PROD => PROD.ID == productBase);
                RequirementPerClass classRequirement = Requirements[4];

                prod.IsCraftable = true;
                prod.Requirements.Add(classRequirement.Materials[0]);
                prod.Requirements.Add(classRequirement.Materials[1]);
                prod.Requirements.Add(classRequirement.Materials[2]);
                prod.RecipeCost = classRequirement.Price;
            }
            else
            {
                for (int i = lowestClassNo; i <= highestClassNo; i++)
                {
                    string product = productBase + i.ToString();
                    RequirementPerClass classRequirement = Requirements[i - 1];

                    var prod = prod_mbin.Table.Find(PROD => PROD.ID == product);
                    prod.IsCraftable = true;
                    prod.Requirements.Add(classRequirement.Materials[0]);
                    prod.Requirements.Add(classRequirement.Materials[1]);
                    prod.Requirements.Add(classRequirement.Materials[2]);
                    prod.RecipeCost = classRequirement.Price;
                }
            }
        }
    }
    protected void AddUnlockableTrees(List<CraftableUpgradeMod> Mods, TreeExpansion Expansion)
    {
        var Mbin                        = ExtractMbin<GcUnlockableTrees>("METADATA/REALITY/TABLES/UNLOCKABLEITEMTREES.MBIN");
        UnlockableItemTreeEnum ExTree   = Expansion.Tree;
        string RootTech                 = Expansion.RootTech;
        string CostType                 = Expansion.CostType;

        var Tree                        = Mbin.Trees[(int)ExTree];
        string Title                    = Tree.Title;

        GcUnlockableItemTreeNode Root   = new GcUnlockableItemTreeNode { Unlockable = RootTech, Children = new() };
        GcUnlockableItemTree ItemTree   = new GcUnlockableItemTree { Title = Title, CostTypeID = CostType, Root = Root };
        //GcUnlockableItemTreeNode Parent = Root;

        Tree.Trees.Add(ItemTree);

        foreach (var Mod in Mods)
        {
            string productBase = Mod.UpgradeBase;
            int highestClassNo = Mod.HighestClassNo;
            int lowestClassNo  = Mod.LowestClassNo;
            
            if (lowestClassNo != MinProcModLimit)
                lowestClassNo = MinProcModLimit;

            Root.Children.Add(CreateChildNode(productBase, lowestClassNo, highestClassNo));
        }
    }
    private GcUnlockableItemTreeNode CreateChildNode(string ProductBase, int CurrentNo, int HighestClassNo)
    {
        string Product                 = ProductBase + CurrentNo.ToString();
        GcUnlockableItemTreeNode Child = new GcUnlockableItemTreeNode
        {
            Unlockable = Product,
            Children = new()
        };

        if (CurrentNo != HighestClassNo)
        {
            Child.Children.Add(CreateChildNode(ProductBase, CurrentNo + 1, HighestClassNo));
        }

        return Child;
    }

    protected void AddLanguageStrings(LanguageId Language, string LanugageID, string LanugageString)
    {
        SetLanguageText(Language, LanugageID, LanugageString);   
    }
}
