//=============================================================================

public class CraftableAlienToken : cmk.NMS.Script.ModClass
{
    readonly GcInventoryType Product   = new GcInventoryType { InventoryType = InventoryTypeEnum.Product };
    readonly GcInventoryType Substance = new GcInventoryType { InventoryType = InventoryTypeEnum.Substance };

    Tuple<string, GcTechnologyRequirement[]>[] NewRequirementsArray;
    
    protected override void Execute()
    {
        FillArray();
        SetCraftabletoTrueAndAddRequirements();
        AddUnlockableTrees();
    }

    protected void FillArray()
    {
        NewRequirementsArray = new Tuple<string, GcTechnologyRequirement[]>[] {
            new("ALIEN_INV_TOKEN", new [] {
                new GcTechnologyRequirement { ID = "FIENDCORE",   Type = Product,   Amount = 10},
                new GcTechnologyRequirement { ID = "EX_BLUE",     Type = Substance, Amount = 500},
                new GcTechnologyRequirement { ID = "CLAMPEARL",   Type = Product,   Amount = 5}
            })
        };
    }

    protected void SetCraftabletoTrueAndAddRequirements()
    {
        var prod_mbin = ExtractMbin<GcProductTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCPRODUCTTABLE.MBIN");

        foreach (var prod in NewRequirementsArray)
        {
            var productId           = prod.Item1;
            var productRequirements = prod.Item2;
            var editProd            = prod_mbin.Table.Find(PRODUCT => PRODUCT.ID == productId);
            
            editProd.IsCraftable    = true;
            editProd.Requirements.Clear();  //clearing requirement to be certain

            foreach (var req in productRequirements)
            {
                editProd.Requirements.Add(req);
            }
        }
    }

    //...........................................................

    protected void AddUnlockableTrees()
    {
        var mbin = ExtractMbin<GcUnlockableTrees>("METADATA/REALITY/TABLES/UNLOCKABLEITEMTREES.MBIN");        
        var tree = mbin.Trees[(int)UnlockableItemTreeEnum.CraftProducts];
        
        tree.Trees[0].Root.Children.Insert(0, new GcUnlockableItemTreeNode { Unlockable = "ALIEN_INV_TOKEN", Children = new() });
    }

}

//=============================================================================
