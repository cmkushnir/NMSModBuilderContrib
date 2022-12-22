//=============================================================================
// Author: Jackty89
//=============================================================================


public class ShipStore : cmk.NMS.Script.ModClass
{

    protected class ConsumableProduct
    {
        public string ProductId;
        public List<Tuple<LanguageId, string, string>> CustomLangDescStrings;
        public string Icon;
        public int Price;
        public string ProcScene;
        public ShipClassEnum ShipClass;
    }
    private readonly List<ConsumableProduct> ConsumableProducts = new List<ConsumableProduct>()
    {
        new ConsumableProduct()
        {
            ProductId = "BUY_SHUT",
            CustomLangDescStrings = new List<Tuple<LanguageId, string, string>>()
            {
                new(LanguageId.English, "Buy fighter", "You just bought a fighter")
            },
            Icon  = "TEXTURES/UI/FRONTEND/ICONS/NOTIFICATIONS/ICON.SHIP.DDS",
            Price = 1000000,
            ProcScene = "MODELS/COMMON/SPACECRAFT/FIGHTERS/FIGHTER_PROC.SCENE.MBIN",
            ShipClass = ShipClassEnum.Fighter
        },
        new ConsumableProduct()
        {
            ProductId = "BUY_SOLA",
            CustomLangDescStrings = new List<Tuple<LanguageId, string, string>>()
            {
                new(LanguageId.English, "Buy solar", "You just bought a solar")
            },
            Icon  = "TEXTURES/UI/FRONTEND/ICONS/NOTIFICATIONS/ICON.SHIP.DDS",
            Price = 10000000,
            ProcScene = "MODELS/COMMON/SPACECRAFT/SAILSHIP/SAILSHIP_PROC.SCENE.MBIN",
            ShipClass = ShipClassEnum.Sail
        }
    };
    protected override void Execute()
    {
        foreach (ConsumableProduct product in ConsumableProducts)
        {
            
            Log.AddInformation($"Print prodID  = {product.ProductId }");
            CreateCustomConsumableProducts(product);
            CreateNewRewards(product);
        }

    }

    protected void CreateCustomConsumableProducts(ConsumableProduct product)
    {
        var prod_mbin = ExtractMbin<GcProductTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCPRODUCTTABLE.MBIN");
        var cons_mbin = ExtractMbin<GcConsumableItemTable>("METADATA/REALITY/TABLES/CONSUMABLEITEMTABLE.MBIN");
        var realitymanagerdata_mbin = ExtractMbin<GcRealityManagerData>("METADATA/REALITY/DEFAULTREALITY.MBIN").TradeSettings;

        var newConsProduct = CloneMbin(prod_mbin.Table.Find(PRODUCT => PRODUCT.ID == "SENTINEL_LOOT"));		
        
        newConsProduct.ID                    = product.ProductId;
        newConsProduct.Name                  = product.ProductId.ToUpper() + "_NAME";
        newConsProduct.NameLower             = product.ProductId.ToUpper() + "_NAME_L";
        newConsProduct.Description           = product.ProductId.ToUpper() + "_DESC";
        newConsProduct.Icon.Filename         = product.Icon;
        newConsProduct.BaseValue             = product.Price;
        newConsProduct.CraftAmountMultiplier = 1;
        newConsProduct.StackMultiplier       = 10; 
        newConsProduct.EggModifierIngredient = false;
        prod_mbin.Table.Add(newConsProduct);

        var newConsumable      = CloneMbin(cons_mbin.Table.Find(CONSUMABLE => CONSUMABLE.ID == "SENTINEL_LOOT"));

        newConsumable.ID       = product.ProductId;
        newConsumable.RewardID = "R_" + product.ProductId;
        cons_mbin.Table.Add(newConsumable);

        realitymanagerdata_mbin.SpaceStation.AlwaysPresentProducts.Add(product.ProductId);
        
        AddNewLanguageString(product.ProductId, product.CustomLangDescStrings);
    }

    protected void CreateNewRewards(ConsumableProduct product)
    {
        var reward_mbin = ExtractMbin<GcRewardTable>("METADATA/REALITY/TABLES/REWARDTABLE.MBIN");

        var copyreward = CloneMbin(reward_mbin.SeasonRewardTable1.Find(REWARD => REWARD.Id == "RS_S1_SHIP"));
        copyreward.Id = "R_" + product.ProductId;
        copyreward.List.OverrideZeroSeed = true;	
        
        var rewardData     = copyreward.List.List[0];
        var rewardShipType = rewardData.Reward as GcRewardSpecificShip;

        rewardShipType.ShipResource.Filename = product.ProcScene;
        rewardShipType.ShipResource.Seed.Seed = 0;
        rewardShipType.ShipResource.Seed.UseSeedValue = false;

        rewardShipType.ShipType.ShipClass = product.ShipClass;
        rewardShipType.IsGift             = false;
        rewardShipType.IsRewardShip       = false;

        reward_mbin.GenericTable.Add(copyreward);

    }

    protected void AddNewLanguageString(string foodId, List<Tuple<LanguageId, string, string>> languages)
    {
        foreach (var language in languages)
        {
            SetLanguageText(language.Item1, foodId.ToUpper() + "_NAME", language.Item2.ToUpper());
            SetLanguageText(language.Item1, foodId.ToUpper() + "_NAME_L", language.Item2);
            SetLanguageText(language.Item1, foodId.ToUpper() + "_DESC", language.Item3);
        }
    }

}

//=============================================================================
