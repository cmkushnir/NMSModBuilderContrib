//=============================================================================
// Author: Jackty89
//=============================================================================

public class CustomModsGalore : cmk.NMS.Script.ModClass
{
    public static int MinProcModLimit        		   = 1;
	public static int RecipeCostPriceMultiplier        = 1;

    static readonly string CostTypeNanite              = "NANITES";
    static readonly string ShipRootTech                = "SHIPJUMP1";
    static readonly string SuitRootTech                = "ENERGY";
    static readonly string WeaponRootTech              = "LASER";
    static readonly string ExoRootTech                 = "VEHICLE_ENGINE";
    //static readonly string FreighterRootTech          = "FRIGATE_FUEL_1";
    //static readonly string FactoryTreeTech            = "PRODFUEL2";
    static readonly string[] Classes                   = { "C", "B", "A", "S" };
    
    static readonly Tuple <LangId , string, string>[] CustomLangNameStrings = new  Tuple<LangId, string, string>[]
    {
        new (LangId.English, "CL_WEAP_NAME",   "Custom weapon mod"),
        new (LangId.English, "CL_SHIP_NAME",   "Customship mod"),
        new (LangId.English, "CL_SHIELD_NAME", "Custom shield mod"),
        new (LangId.English, "CL_PROT_NAME",   "Custom protection mod"),

        new (LangId.English, "CL_ROCK_TECH1",  "Large Missle Tubes."),
        new (LangId.English, "CL_ROCK_TECH2",  "Missile Cooling vents."),
        new (LangId.English, "CL_ROCK_TECH3",  "High Yield Missles"),
        new (LangId.English, "CL_MINI_TECH1",  "HE Rounds."),
    };
    static readonly Tuple<LangId, string, string>[] CustomLangDescStrings = new Tuple<LangId, string, string>[]
    {
        new (LangId.English, "CL_WEAP_DESC",   "A custom procedural weapon mod"),
        new (LangId.English, "CL_SHIP_DESC",   "A custom procedural ship mod"),
        new (LangId.English, "CL_SHIELD_DESC", "A custom procedural shield mod"),
        new (LangId.English, "CL_PROT_DESC",   "A custom procedural protection mod"),
        
        new (LangId.English, "CL_ROCK_DESC1",  "These rocket pods hold more rockets, this might affect accuracry."),
        new (LangId.English, "CL_ROCK_DESC2",  "Cooling vents to missile tubes. Improves fire rate cooldown and more."),
        new (LangId.English, "CL_ROCK_DESC3",  "Increases blast radius, careful to not be caught by it."),
        new (LangId.English, "CL_MINI_DESC1",  "Blast rounds, bring the pain."),
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
            Group          = "FLAMETHROW_NAME_L",
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
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Weapon_Projectile_Rate,           2,    5,     WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Weapon_FireDOT_DPS,               10,   15,    WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Weapon_FireDOT_Duration,          5,    10,    WeightingCurveEnum.MaxIsUncommon, true)
            }
        },
        new CustomProcMod {
            BaseTechID        = "U_SHIELDBOOST",
            BaseDeploy        = "UP_SHLD",
            NewTechID         = "UC_HADM",
            TemplateName      = "TC_HAZ",
            HighestClassNo    = 4,
            lowestClassNo     = 1,
            MinStats          = 4,
            MaxStats          = 4,
            MultiplierPerRank = 0.3f,
            IconFileName      = "HEALTH.DDS",
            Name              = "CL_PROT_NAME",
            ProcName          = "UP_HAZPROT",
            Description       = "CL_PROT_DESC",
            TradeData         = "SuitTechSpecialist",
            StatBonuses       = new List<GcProceduralTechnologyStatLevel>() {
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_DamageReduce_Cold,      1.5f, 3, WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_DamageReduce_Heat,      1.5f, 3, WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_DamageReduce_Radiation, 1.5f, 3, WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_DamageReduce_Toxic,     1.5f, 3, WeightingCurveEnum.MaxIsUncommon, true)
            }
        },
        new CustomProcMod {
            BaseTechID        = "U_SHIELDBOOST",
            BaseDeploy        = "UP_SHLD",
            NewTechID         = "UC_HAPR",
            TemplateName      = "TC_HAZ",
            HighestClassNo    = 4,
            lowestClassNo     = 1,
            MinStats          = 4,
            MaxStats          = 4,
            MultiplierPerRank = 0.3f,
            IconFileName      = "HEALTH.DDS",
            Name              = "CL_PROT_NAME",
            ProcName          = "UP_HAZPROT",
            Description       = "CL_PROT_DESC",
            TradeData         = "SuitTechSpecialist",
            StatBonuses       = new List<GcProceduralTechnologyStatLevel>() {
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_Protection_Cold,      500, 1000, WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_Protection_Heat,      500, 1000, WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_Protection_Radiation, 500, 1000, WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_Protection_Toxic,     500, 1000, WeightingCurveEnum.MaxIsUncommon, true)
            }
        },
        new CustomProcMod {
            BaseTechID        = "U_SHIELDBOOST",
            BaseDeploy        = "UP_SHLD",
            NewTechID         = "UC_HADR",
            TemplateName      = "TC_HAZ",
            HighestClassNo    = 4,
            lowestClassNo     = 1,
            MinStats          = 4,
            MaxStats          = 4,
            MultiplierPerRank = 0.3f,
            IconFileName      = "HEALTH.DDS",
            Name              = "CL_PROT_NAME",
            ProcName          = "UP_HAZPROT",
            Description       = "CL_PROT_DESC",
            TradeData         = "SuitTechSpecialist",
            StatBonuses       = new List<GcProceduralTechnologyStatLevel>() {
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_Protection_ColdDrain, 5, 25, WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_Protection_HeatDrain, 5, 25, WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_Protection_RadDrain,  5, 25, WeightingCurveEnum.MaxIsUncommon, true),
                ProceduralTechnologyStatLevel.Create(StatsTypeEnum.Suit_Protection_ToxDrain,  5, 25, WeightingCurveEnum.MaxIsUncommon, true)
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
    TreeExpansion ShipTreeExpansion     = new TreeExpansion { Tree = UnlockableItemTreeEnum.ShipTech,     CostType = CostTypeNanite, RootTech = ShipRootTech };
    TreeExpansion WeaponTreeExpansion   = new TreeExpansion { Tree = UnlockableItemTreeEnum.WeapTech,     CostType = CostTypeNanite, RootTech = WeaponRootTech };
    TreeExpansion ExoCraftTreeExpansion = new TreeExpansion { Tree = UnlockableItemTreeEnum.ExocraftTech, CostType = CostTypeNanite, RootTech = ExoRootTech };
    TreeExpansion SuitTreeExpansion     = new TreeExpansion { Tree = UnlockableItemTreeEnum.SuitTech,     CostType = CostTypeNanite, RootTech = SuitRootTech };
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
    };
    List<CraftableUpgradeMod> SuitMods = new List<CraftableUpgradeMod>() {
        new CraftableUpgradeMod { UpgradeBase = "UC_HADM", HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "UC_HAPR", HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "UC_HADR", HighestClassNo = 4, LowestClassNo = 1 },
    };
    //====================================================================
    protected class RequirementPerClass
    {
        public GcTechnologyRequirement[] Materials = new GcTechnologyRequirement[3];
        public int Price;
    }
    protected RequirementPerClass[] Requirements = new[] {
		//C-Class
		new RequirementPerClass{
            Materials = new [] {
                TechnologyRequirement.Substance("EX_YELLOW", 100),
                TechnologyRequirement.Product("TECH_COMP",   1),
                TechnologyRequirement.Substance("STELLAR2",  500),
            },
            Price = 1000 * RecipeCostPriceMultiplier
        },
		//B-Class
		new RequirementPerClass{
            Materials = new [] {
                TechnologyRequirement.Substance("EX_RED",   200),
                TechnologyRequirement.Product("TECH_COMP",  2),
                TechnologyRequirement.Substance("STELLAR2", 500)
            },
            Price = 2500 * RecipeCostPriceMultiplier
        },
		//A-Class
		new RequirementPerClass{
            Materials = new [] {
                TechnologyRequirement.Substance("EX_GREEN", 300),
                TechnologyRequirement.Product("TECH_COMP",  3),
                TechnologyRequirement.Substance("STELLAR2", 500)
            },
            Price = 5000 * RecipeCostPriceMultiplier
        },
		//S-Class
		new RequirementPerClass{
            Materials = new [] {
                TechnologyRequirement.Substance("EX_BLUE",  500),
                TechnologyRequirement.Product("TECH_COMP",  5),
                TechnologyRequirement.Substance("STELLAR2", 500)
            },
            Price = 10000 * RecipeCostPriceMultiplier
        },
		//X-Class
		new RequirementPerClass{
            Materials = new [] {
                TechnologyRequirement.Substance("EX_BLUE", 300),
                TechnologyRequirement.Product("TECH_COMP", 5),
                TechnologyRequirement.Substance("EX_RED",  300)
            },
            Price = 5000 * RecipeCostPriceMultiplier
        }
    };

    //====================================================================
    protected class Data
    {
        public TreeExpansion Tree;
        public List<List<CraftableUpgradeMod>> Mods;
    }
    //====================================================================
    protected override void Execute()
    {
        Data[] AllModData = new[] {
            new Data { Tree = ShipTreeExpansion,   Mods = new(){ ShipMods }},
            new Data { Tree = WeaponTreeExpansion, Mods = new(){ WeaponMods }},
            new Data { Tree = SuitTreeExpansion,   Mods = new(){ SuitMods }},
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
            LangId language        = LanguageData.Item1;
            string languageID      = LanguageData.Item2;
            string languageIDL     = languageID + "_L";            
            string languageString  = LanguageData.Item3;
            string languageStringU = languageString.ToUpper();

            AddLanguageStrings(language, languageIDL, languageString);
            AddLanguageStrings(language, languageID, languageStringU);
        }

        foreach (var LanguageData in CustomLangDescStrings)
        {
            LangId language       = LanguageData.Item1;
            string languageID     = LanguageData.Item2;
            string languageString = LanguageData.Item3;

            AddLanguageStrings(language, languageID, languageString);
        }
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
        var mbin = ExtractMbin<GcTechnologyTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCTECHNOLOGYTABLE.MBIN"
        );

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
                string copyDeployTech  = baseDeployString + i.ToString();
                string newTechID       = newTechName + i.ToString();
            	string newTechDeployID = newTechDeployName.Replace("UPC_", "UPC_" + Classes[i-1]).ToUpper() + "_" + Classes[i-1];
                float  multiplier      = 1 + (newMultiplierPerRank * (i - 1));

                if (newMinStats < newMaxStats)
                    newMinStats++;
                
                var proc         = CloneMbin(Proc_mbin.Table.Find(PROC => PROC.ID == copyDeployTech));
                proc.ID          = newTechDeployID;
                proc.Name        = newProcgenName;
                proc.NameLower   = newNameL;
                proc.Description = newDescription;
                proc.Template    = newTemplate;
                proc.NumStatsMin = newMinStats;
                proc.NumStatsMax = newMinStats;
                proc.StatLevels.Clear();
                foreach (var statLevel in newStatBonuses)
                {
                    statLevel.ValueMin *= multiplier;
                    statLevel.ValueMax *= multiplier;
                    proc.StatLevels.Add(statLevel);
                }
                Proc_mbin.Table.Add(proc);

                // Create custum products for the tech
                var prod = CloneMbin(Prod_mbin.Table.Find(PROD => PROD.Id == copyTech));

                prod.Id          = newTechID;
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
                var prod = prod_mbin.Table.Find(PROD => PROD.Id == productBase);
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

                    var prod = prod_mbin.Table.Find(PROD => PROD.Id == product);
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
    private static GcUnlockableItemTreeNode CreateChildNode(string ProductBase, int CurrentNo, int HighestClassNo)
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

    protected void AddLanguageStrings(LangId Language, string LanugageID, string LanugageString)
    {
        SetLanguageText(Language, LanugageID, LanugageString);   
    }
}
