//=============================================================================
// Author: Jackty89
//=============================================================================

public class CraftableUpgradeMods : cmk.NMS.Script.ModClass
{
    public int RecipeCostPriceMultiplier       = 1;

    readonly string CostTypeFactory            = "FACTORY";
    readonly string CostTypeNanite             = "NANITES";

    readonly string ShipRootTech               = "SHIPJUMP1";
    readonly string SuitRootTech               = "ENERGY";
    readonly string WeaponRootTech             = "LASER";
    readonly string ExoRootTech                = "VEHICLE_ENGINE";
    readonly string FreighterRootTech          = "FRIGATE_FUEL_1";
    readonly string FactoryTreeTech            = "PRODFUEL2";

    readonly string CraftedFreighterModDescrId = "UP_FR_CRAFTED_DESC";
    readonly string[] Classes                  = { "C", "B", "A", "S" };

    protected class Data
    {
        public TreeExpansion Tree;
        public List<List<CraftableUpgradeMod>> Mods;
    }

    protected class CraftableUpgradeMod
    {
        public string UpgradeBase;
        public int HighestClassNo;
        public int LowestClassNo;
    }

    protected class TreeExpansion
    {
        public UnlockableItemTreeEnum Tree;
        public string RootTech;
        public string CostType;
    }

    protected class CustomMod
    {
        public string BaseTechName;
        public string NewTechName;
        public int HighestClassNo;
        public int LowestClassNo;
    }

    protected class RequirementPerClass
    {
        public GcTechnologyRequirement[] Materials = new GcTechnologyRequirement[3];
        public int Price;
    }
    //ShipMods
    List<CraftableUpgradeMod> ShipMods = new List<CraftableUpgradeMod>() {
        new CraftableUpgradeMod { UpgradeBase = "U_LAUNCH",     HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_HYPER",      HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_SHIPSHIELD", HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_PULSE",      HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_SHIPSHOT",   HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_SHIPMINI",   HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_SHIPBLOB",   HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_SHIPGUN",    HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_SHIPLAS",    HighestClassNo = 4, LowestClassNo = 1 }
    };
    //Separate page but same TreeExpansion
    List<CraftableUpgradeMod> BioShipMods = new List<CraftableUpgradeMod>() {
        new CraftableUpgradeMod { UpgradeBase = "AP_SHIPLAS",    HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "AP_LAUNCH",     HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "AP_PULSE",      HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "AP_SHIPSHIELD", HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "AP_HYPER",      HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "AP_SHIPGUN",    HighestClassNo = 4, LowestClassNo = 1 }
    };
    //SuitMods
    List<CraftableUpgradeMod> SuitMods = new List<CraftableUpgradeMod>() {
        new CraftableUpgradeMod { UpgradeBase = "U_UNW",         HighestClassNo = 3, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_TOX",         HighestClassNo = 3, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_COLDPROT",    HighestClassNo = 3, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_HOTPROT",     HighestClassNo = 3, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_RAD",         HighestClassNo = 3, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_ENERGY",      HighestClassNo = 3, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_SHIELDBOOST", HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_JETBOOST",    HighestClassNo = 4, LowestClassNo = 1 }
    };
    //WeaponMods
    List<CraftableUpgradeMod> WeaponMods = new List<CraftableUpgradeMod>() {
        new CraftableUpgradeMod { UpgradeBase = "U_RAIL",     HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_BOLT",     HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_TGRENADE", HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_LASER",    HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_SCANNER",  HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_GRENADE",  HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_SHOTGUN",  HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_SMG",      HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_CANNON",   HighestClassNo = 4, LowestClassNo = 1 }
    };
    //--ExoCraftMods
    List<CraftableUpgradeMod> ExoCraftMods = new List<CraftableUpgradeMod>() {
        new CraftableUpgradeMod { UpgradeBase = "U_EXOBOOST",   HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_EXOGUN",     HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_EXO_ENG",    HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_EXOLAS",     HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_EXO_SUB",    HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_EXO_SUBGUN", HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_MECHGUN",    HighestClassNo = 4, LowestClassNo = 2 },
        new CraftableUpgradeMod { UpgradeBase = "U_MECH_ENG",   HighestClassNo = 4, LowestClassNo = 2 },
        new CraftableUpgradeMod { UpgradeBase = "U_MECHLAS",    HighestClassNo = 4, LowestClassNo = 2 }
    };
    //--X-Class
    List<CraftableUpgradeMod> XclassMods1 = new List<CraftableUpgradeMod>() {
        new CraftableUpgradeMod { UpgradeBase = "U_HAZARDX",      HighestClassNo = 0, LowestClassNo = 0 },
        new CraftableUpgradeMod { UpgradeBase = "U_JETBOOSTX",    HighestClassNo = 0, LowestClassNo = 0 },
        new CraftableUpgradeMod { UpgradeBase = "U_SHIELDBOOSTX", HighestClassNo = 0, LowestClassNo = 0 },
        new CraftableUpgradeMod { UpgradeBase = "U_ENERGYX",      HighestClassNo = 0, LowestClassNo = 0 }
    };

    List<CraftableUpgradeMod> XclassMods2 = new List<CraftableUpgradeMod>() {
        new CraftableUpgradeMod { UpgradeBase = "U_HYPERX",      HighestClassNo = 0, LowestClassNo = 0 },
        new CraftableUpgradeMod { UpgradeBase = "U_SHIPSHIELDX", HighestClassNo = 0, LowestClassNo = 0 },
        new CraftableUpgradeMod { UpgradeBase = "U_PULSEX",      HighestClassNo = 0, LowestClassNo = 0 },
        new CraftableUpgradeMod { UpgradeBase = "U_LAUNCHX",     HighestClassNo = 0, LowestClassNo = 0 }
    };

    List<CraftableUpgradeMod> XclassMods3 = new List<CraftableUpgradeMod>() {
        new CraftableUpgradeMod { UpgradeBase = "U_SHIPGUNX",  HighestClassNo = 0, LowestClassNo = 0 },
        new CraftableUpgradeMod { UpgradeBase = "U_SHIPMINIX", HighestClassNo = 0, LowestClassNo = 0 },
        new CraftableUpgradeMod { UpgradeBase = "U_SHIPSHOTX", HighestClassNo = 0, LowestClassNo = 0 },
        new CraftableUpgradeMod { UpgradeBase = "U_SHIPLASX",  HighestClassNo = 0, LowestClassNo = 0 },
        new CraftableUpgradeMod { UpgradeBase = "U_SHIPBLOBX", HighestClassNo = 0, LowestClassNo = 0 }
    };

    List<CraftableUpgradeMod> XclassMods4 = new List<CraftableUpgradeMod>() {
        new CraftableUpgradeMod { UpgradeBase = "U_LASERX",   HighestClassNo = 0, LowestClassNo = 0 },
        new CraftableUpgradeMod { UpgradeBase = "U_SCANNERX", HighestClassNo = 0, LowestClassNo = 0 },
        new CraftableUpgradeMod { UpgradeBase = "U_SHOTGUNX", HighestClassNo = 0, LowestClassNo = 0 },
        new CraftableUpgradeMod { UpgradeBase = "U_SMGX",     HighestClassNo = 0, LowestClassNo = 0 }
    };

    List<CraftableUpgradeMod> XclassMods5 = new List<CraftableUpgradeMod>() {
        new CraftableUpgradeMod { UpgradeBase = "U_RAILX",     HighestClassNo = 0, LowestClassNo = 0 },
        new CraftableUpgradeMod { UpgradeBase = "U_BOLTX",     HighestClassNo = 0, LowestClassNo = 0 },
        new CraftableUpgradeMod { UpgradeBase = "U_TGRENADEX", HighestClassNo = 0, LowestClassNo = 0 },
        new CraftableUpgradeMod { UpgradeBase = "U_GRENADEX",  HighestClassNo = 0, LowestClassNo = 0 },
        new CraftableUpgradeMod { UpgradeBase = "U_CANNONX",   HighestClassNo = 0, LowestClassNo = 0 }
    };

    List<CraftableUpgradeMod> XclassMods6 = new List<CraftableUpgradeMod>() {
        new CraftableUpgradeMod { UpgradeBase = "U_SENTGUN",  HighestClassNo = 0, LowestClassNo = 0 },
        new CraftableUpgradeMod { UpgradeBase = "U_SENTSUIT", HighestClassNo = 0, LowestClassNo = 0 },
        new CraftableUpgradeMod { UpgradeBase = "ROGUE_CARBOX", HighestClassNo = 0, LowestClassNo = 0 },
        new CraftableUpgradeMod { UpgradeBase = "ROGUE_HAZBOX", HighestClassNo = 0, LowestClassNo = 0 },
        new CraftableUpgradeMod { UpgradeBase = "ROGUE_STARTBOX", HighestClassNo = 0, LowestClassNo = 0 }
    };

    List<CraftableUpgradeMod> CustomFreighterMods = new List<CraftableUpgradeMod>() {
        new CraftableUpgradeMod { UpgradeBase = "U_FREIG_SPE",  HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_FREIG_COM",  HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_FREIG_EXP",  HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_FREIG_FUEL", HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_FREIG_MINE", HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_FREIG_TRA",  HighestClassNo = 4, LowestClassNo = 1 },
        new CraftableUpgradeMod { UpgradeBase = "U_FREIG_HYP",  HighestClassNo = 4, LowestClassNo = 1 }
    };

    protected CustomMod[] NewFreighterMods = new[] {
        new CustomMod { BaseTechName = "U_FR_SPE",  NewTechName = "FREIG_SPE",  HighestClassNo = 4, LowestClassNo = 1 },
        new CustomMod { BaseTechName = "U_FR_COM",  NewTechName = "FREIG_COM",  HighestClassNo = 4, LowestClassNo = 1 },
        new CustomMod { BaseTechName = "U_FR_EXP",  NewTechName = "FREIG_EXP",  HighestClassNo = 4, LowestClassNo = 1 },
        new CustomMod { BaseTechName = "U_FR_FUEL", NewTechName = "FREIG_FUEL", HighestClassNo = 4, LowestClassNo = 1 },
        new CustomMod { BaseTechName = "U_FR_MINE", NewTechName = "FREIG_MINE", HighestClassNo = 4, LowestClassNo = 1 },
        new CustomMod { BaseTechName = "U_FR_TRA",  NewTechName = "FREIG_TRA",  HighestClassNo = 4, LowestClassNo = 1 },
        new CustomMod { BaseTechName = "U_FR_HYP",  NewTechName = "FREIG_HYP",  HighestClassNo = 4, LowestClassNo = 1 }
    };

    readonly GcInventoryType Product   = new GcInventoryType { InventoryType = InventoryTypeEnum.Product };
    readonly GcInventoryType Substance = new GcInventoryType { InventoryType = InventoryTypeEnum.Substance };

    protected RequirementPerClass[] Requirements;

    protected override void Execute()
    {
        TreeExpansion ShipTreeExpansion      = new TreeExpansion { Tree = UnlockableItemTreeEnum.ShipTech,      CostType = CostTypeNanite,  RootTech = ShipRootTech};
        TreeExpansion SuitTreeExpansion      = new TreeExpansion { Tree = UnlockableItemTreeEnum.SuitTech,      CostType = CostTypeNanite,  RootTech = SuitRootTech};
        TreeExpansion WeaponTreeExpansion    = new TreeExpansion { Tree = UnlockableItemTreeEnum.WeapTech,      CostType = CostTypeNanite,  RootTech = WeaponRootTech};
        TreeExpansion ExoCraftTreeExpansion  = new TreeExpansion { Tree = UnlockableItemTreeEnum.ExocraftTech,  CostType = CostTypeNanite,  RootTech = ExoRootTech};
        TreeExpansion FactoryTreeExpansion   = new TreeExpansion { Tree = UnlockableItemTreeEnum.CraftProducts, CostType = CostTypeFactory, RootTech = FactoryTreeTech};
        TreeExpansion FreighterTreeExpansion = new TreeExpansion { Tree = UnlockableItemTreeEnum.FreighterTech, CostType = CostTypeNanite,  RootTech = FreighterRootTech};

        FillRequirementsArray();
        CreateCustomFreighterMods();
        AddLanguageStrings();

        Data[] AllModData = new[] {
            new Data { Tree = ShipTreeExpansion,      Mods = new(){ ShipMods }},
            new Data { Tree = ShipTreeExpansion,      Mods = new(){ BioShipMods }},
            new Data { Tree = SuitTreeExpansion,      Mods = new(){ SuitMods }},
            new Data { Tree = WeaponTreeExpansion,    Mods = new(){ WeaponMods }},
            new Data { Tree = ExoCraftTreeExpansion,  Mods = new(){ ExoCraftMods }},
            new Data { Tree = FreighterTreeExpansion, Mods = new(){ CustomFreighterMods }},
        };

        Data[] XmodsData = new[] {
            new Data { Tree = FactoryTreeExpansion, Mods = new(){ XclassMods1, XclassMods2, XclassMods3, XclassMods4, XclassMods5, XclassMods6 }},
        };

        foreach(var ModData in AllModData)
        {
            TreeExpansion Expansion = ModData.Tree;
            List<List<CraftableUpgradeMod>> ListOfMods = ModData.Mods;
            foreach(var Mods in ListOfMods)
            {
                SetCraftabletoTrueAndAddRequirements(Mods);
                AddUnlockableTrees(Mods, Expansion);
            }
        }

        foreach(var XData in XmodsData)
        {
            TreeExpansion Expansion = XData.Tree;
            List<List<CraftableUpgradeMod>> ListOfMods = XData.Mods;
            foreach(var Mods in ListOfMods)
            {
                SetCraftabletoTrueAndAddRequirements(Mods);
            }
            AddXClassUnlockableTrees(ListOfMods, Expansion);
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
                Price = 20 * RecipeCostPriceMultiplier
            }
        };
    }

    protected void CreateCustomFreighterMods()
    {
        var Prod_mbin = ExtractMbin<GcProductTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCPRODUCTTABLE.MBIN");
        var Proc_mbin = ExtractMbin<GcProceduralTechnologyTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCPROCEDURALTECHNOLOGYTABLE.MBIN");

        foreach(CustomMod Mod in NewFreighterMods)
        {
            string BaseTechName      = Mod.BaseTechName;
            string BaseDeployString  = BaseTechName.Replace("U_FR_", "UP_FR").Substring(0, 8);
            string NewTechName       = Mod.NewTechName;
            string NewTechDeployName = Mod.NewTechName;
            NewTechDeployName        = NewTechDeployName.Replace("_", "");
            NewTechDeployName        = NewTechDeployName.Replace("FREIG", "");
            int HighestClassNo       = Mod.HighestClassNo;
            int LowestClassNo        = Mod.LowestClassNo;
            for(int i = LowestClassNo; i <= HighestClassNo; i++)
            {
                string CopyTech = BaseTechName + i.ToString();
                string CopyDeployTech = BaseDeployString + i.ToString();
                string NewTechID = "U_" + NewTechName + i.ToString();
                string NewTechDeployID = "UP_" + Classes[i - 1] + NewTechDeployName.ToLower() + "Deploy" + "_" + Classes[i - 1];

                var Proc = CloneMbin(Proc_mbin.Table.Find(PROC => PROC.ID == CopyDeployTech));
                Proc.ID = NewTechDeployID;
                Proc_mbin.Table.Add(Proc);

                var Prod = CloneMbin(Prod_mbin.Table.Find(PROD => PROD.ID == CopyTech));
                Prod.ID = NewTechID;
                Prod.DeploysInto = NewTechDeployID + "Deploy";
                Prod.Description = CraftedFreighterModDescrId;
                Prod_mbin.Table.Add(Prod);
            }
        }
    }

    //...........................................................

    protected void SetCraftabletoTrueAndAddRequirements(List<CraftableUpgradeMod> Mods)
    {
        var Prod_mbin = ExtractMbin<GcProductTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCPRODUCTTABLE.MBIN");

        foreach(var Mod in Mods)
        {
            string ProductBase = Mod.UpgradeBase;
            int HighestClassNo = Mod.HighestClassNo;
            int LowestClassNo  = Mod.LowestClassNo;

            if(Mod.HighestClassNo == 0)
            { //X-Class Mods
                var Prod = Prod_mbin.Table.Find(PROD => PROD.ID == ProductBase);
                RequirementPerClass ClassRequirement = Requirements[4];

                Prod.IsCraftable = true;
                Prod.Requirements.Add(ClassRequirement.Materials[0]);
                Prod.Requirements.Add(ClassRequirement.Materials[1]);
                Prod.Requirements.Add(ClassRequirement.Materials[2]);
                Prod.RecipeCost = ClassRequirement.Price;
            }
            else
            {
                for(int i = LowestClassNo; i <= HighestClassNo; i++)
                { //Normal Mods
                    string Product = ProductBase + i.ToString();
                    RequirementPerClass ClassRequirement = Requirements[i - 1];

                    var Prod = Prod_mbin.Table.Find(PROD => PROD.ID == Product);
                    Prod.IsCraftable = true;
                    Prod.Requirements.Add(ClassRequirement.Materials[0]);
                    Prod.Requirements.Add(ClassRequirement.Materials[1]);
                    Prod.Requirements.Add(ClassRequirement.Materials[2]);
                    Prod.RecipeCost = ClassRequirement.Price;
                }
            }
        }
    }

    //...........................................................

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

        foreach(var Mod in Mods)
        {
            string ProductBase = Mod.UpgradeBase;
            int HighestClassNo = Mod.HighestClassNo;
            int LowestClassNo  = Mod.LowestClassNo;

            Root.Children.Add(CreateChildNode(ProductBase, LowestClassNo, HighestClassNo));
        }
    }

    //...........................................................

    private GcUnlockableItemTreeNode CreateChildNode(string ProductBase, int CurrentNo, int HighestClassNo)
    {
        string Product = ProductBase + CurrentNo.ToString();
        GcUnlockableItemTreeNode Child = new GcUnlockableItemTreeNode
        {
            Unlockable = Product,
            Children   = new()
        };

        if(CurrentNo != HighestClassNo)
        {
            Child.Children.Add(CreateChildNode(ProductBase, CurrentNo + 1, HighestClassNo));
        }

        return Child;
    }

    //...........................................................

    protected void AddXClassUnlockableTrees(List<List<CraftableUpgradeMod>> ListMods, TreeExpansion Expansion)
    {
        var Mbin                      = ExtractMbin<GcUnlockableTrees>("METADATA/REALITY/TABLES/UNLOCKABLEITEMTREES.MBIN");
        UnlockableItemTreeEnum ExTree = Expansion.Tree;
        string RootTech               = Expansion.RootTech;
        string CostType               = Expansion.CostType;
        var Tree                      = Mbin.Trees[(int)ExTree];
        string Title                  = Tree.Title;
        GcUnlockableItemTreeNode Root = new GcUnlockableItemTreeNode { Unlockable = RootTech, Children = new() };
        GcUnlockableItemTree ItemTree = new GcUnlockableItemTree { Title = Title, CostTypeID = CostType, Root = Root };

        Tree.Trees.Add(ItemTree);

        foreach(List<CraftableUpgradeMod> XMods in ListMods)
        {
            GcUnlockableItemTreeNode Parent = Root;
            string Child = Root.Unlockable;

            foreach(CraftableUpgradeMod XMod in XMods)
            {
                string ProductBase = XMod.UpgradeBase;
                Parent.Children.Add(new GcUnlockableItemTreeNode
                {
                    Unlockable = ProductBase,
                    Children   = new()
                });
                var ChildNode = Parent.Children.Find(CHILD => CHILD.Unlockable == ProductBase);
                Parent        = ChildNode;
            }
        }
    }

    //...........................................................

    protected void AddLanguageStrings()
    {
        SetLanguageText(LanguageId.English, CraftedFreighterModDescrId, "A crafted freighter upgrade. \nCan be re-deployed into your own capital ship to improve its <TECHNOLOGY>Technology<>.");
        SetLanguageText(LanguageId.French, CraftedFreighterModDescrId, "Une amélioration de cargo fabriqué. \nPeut être redéployée dans votre propre vaisseau amiral pour améliorer sa <TECHNOLOGY>Technologie<>.");
        SetLanguageText(LanguageId.Italian, CraftedFreighterModDescrId, "Un miglioramento per il mercantile. \nPuò essere utilizzato sulla propria ammiraglia per migliorarne l'<TECHNOLOGY>Tecnologia<>.");
        SetLanguageText(LanguageId.German, CraftedFreighterModDescrId, "Ein einsetzbares Frachter-Upgrade. \nKann in dein eigenes Hauptschiff wiedereingebaut werden, um dessen <TECHNOLOGY>Technologie<> zu verbessern.");
        SetLanguageText(LanguageId.Spanish, CraftedFreighterModDescrId, "Una mejora del carguero hecha a mano. \nSe puede volver a desplegar en tu propia nave principal para mejorar su <TECHNOLOGY>Tecnología<>.");
        SetLanguageText(LanguageId.Russian, CraftedFreighterModDescrId, "Модернизация грузового корабля. \nМожет быть переоборудована в ваш собственный капитальный корабль для улучшения его <TECHNOLOGY>Technology<>.");
        SetLanguageText(LanguageId.Polish, CraftedFreighterModDescrId, "Rozmieszczalne ulepszenie frachtowca. MoÅ¼na ponownie zainstalowaÄ na wÅasnym statku gÅównym, aby ulepszyÄ jego <TECHNOLOGY>technologie<>.");
        SetLanguageText(LanguageId.Dutch, CraftedFreighterModDescrId, "Een upgrade gemaakt voor een vlaggenschip. \nKan worden geïnstalleerd in je eigen vlaggenschip om de <TECHNOLOGY>Technologie<> te verbeteren.");
        SetLanguageText(LanguageId.Portuguese, CraftedFreighterModDescrId, "Uma atualização do cargueiro implementável. \nPode ser reimplementada na sua própria nave capital para melhorar o <TECHNOLOGY>Tecnologia<>.");
        SetLanguageText(LanguageId.LatinAmericanSpanish, CraftedFreighterModDescrId, "Una mejora del carguero hecha a mano. \nSe puede volver a desplegar en tu propia nave principal para mejorar su <TECHNOLOGY>Tecnología<>.");
        SetLanguageText(LanguageId.BrazilianPortuguese, CraftedFreighterModDescrId, "Uma atualização implantável de nave cargueira. \nPode ser reimplantada na sua nave capital para melhorar o <TECHNOLOGY>Tecnologia<> dela.");
        SetLanguageText(LanguageId.SimplifiedChinese, CraftedFreighterModDescrId, "可合成的貨船升級。 \n可以被移除及重新安裝於星際貨船上，以提升<TECHNOLOGY>科技元件<>效能。");
        SetLanguageText(LanguageId.TraditionalChinese, CraftedFreighterModDescrId, "可合成的貨船升級。 \n可以被移除及重新安裝於星際貨船上，以提升<TECHNOLOGY>科技元件<>效能。");
        SetLanguageText(LanguageId.TenCentChinese, CraftedFreighterModDescrId, "可合成的貨船升級。 \n可以被移除及重新安裝於星際貨船上，以提升<TECHNOLOGY>科技元件<>效能。");
        SetLanguageText(LanguageId.USEnglish, CraftedFreighterModDescrId, "A crafted freighter upgrade. \nCan be re-deployed into your own capital ship to improve its <TECHNOLOGY>Technology<>.");
        //SetLanguageText(LanguageId.Korean, CraftedFreighterModDescrId, "");
        //SetLanguageText(LanguageId.Japanese, CraftedFreighterModDescrId, "");
    }
}

//=============================================================================
