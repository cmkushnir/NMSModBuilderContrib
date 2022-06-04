//=============================================================================
// Author: Jackty89
//=============================================================================

public class CraftableModules : cmk.NMS.Script.ModClass
{
    static GcInventoryType Product   = new GcInventoryType { InventoryType = InventoryTypeEnum.Product };
    static GcInventoryType Substance = new GcInventoryType { InventoryType = InventoryTypeEnum.Substance };

    Tuple<string, int>[] AlteredProductPrices = new Tuple<string, int>[] {
        new ("FREI_INV_TOKEN", 1042042),
        new ("SHIP_INV_TOKEN", 1042042),
        new ("FRIG_TOKEN",     1042042)
    };

    Tuple<string, GcTechnologyRequirement[]>[] NewRequirementsArray = new Tuple<string, GcTechnologyRequirement[]>[] {
        new("BP_SALVAGE", new [] {
            new GcTechnologyRequirement { ID = "COMPUTER",   InventoryType = Product,   Amount = 16},
            new GcTechnologyRequirement { ID = "ROBOT1",     InventoryType = Substance, Amount = 250},
            new GcTechnologyRequirement { ID = "NAV_DATA",   InventoryType = Product,   Amount = 5}
        }),
        new("NAV_DATA", new [] {
            new GcTechnologyRequirement { ID = "COMPUTER",   InventoryType = Product,   Amount = 10},
            new GcTechnologyRequirement { ID = "ROBOT1",     InventoryType = Substance, Amount = 250},
            new GcTechnologyRequirement { ID = "HYPERFUEL1", InventoryType = Product,   Amount = 5}
        }),
        new("FREI_INV_TOKEN", new [] {
            new GcTechnologyRequirement { ID = "FARMPROD9",  InventoryType = Product,   Amount = 1},
            new GcTechnologyRequirement { ID = "ROBOT1",     InventoryType = Substance, Amount = 150},
            new GcTechnologyRequirement { ID = "ASTEROID3",  InventoryType = Substance, Amount = 250}
        }),
        new("SHIP_INV_TOKEN", new [] {
            new GcTechnologyRequirement { ID = "FARMPROD9",  InventoryType = Product,   Amount = 1},
            new GcTechnologyRequirement { ID = "ROBOT1",     InventoryType = Substance, Amount = 150},
            new GcTechnologyRequirement { ID = "ASTEROID2",  InventoryType = Substance, Amount = 250}
        }),
        new("FRIG_TOKEN", new [] {
            new GcTechnologyRequirement { ID = "FARMPROD9",  InventoryType = Product,   Amount = 1},
            new GcTechnologyRequirement { ID = "ROBOT1",     InventoryType = Substance, Amount = 150},
            new GcTechnologyRequirement { ID = "ASTEROID1",  InventoryType = Substance, Amount = 250}
        }),
        new("REPAIRKIT", new [] {
            new GcTechnologyRequirement { ID = "MICROCHIP",  InventoryType = Product,   Amount = 1},
            new GcTechnologyRequirement { ID = "ROBOT1",     InventoryType = Substance, Amount = 100},
            new GcTechnologyRequirement { ID = "FUEL2",      InventoryType = Substance, Amount = 500}
        }),
        new("WEAP_INV_TOKEN", new [] {
            new GcTechnologyRequirement { ID = "COMPUTER",   InventoryType = Product,   Amount = 15},
            new GcTechnologyRequirement { ID = "ROBOT1",     InventoryType = Substance, Amount = 150},
            new GcTechnologyRequirement { ID = "CATALYST2",  InventoryType = Substance, Amount = 150}
        }),
        new("SUIT_INV_TOKEN", new [] {
            new GcTechnologyRequirement { ID = "COMPUTER",   InventoryType = Product,   Amount = 15},
            new GcTechnologyRequirement { ID = "ROBOT1",     InventoryType = Substance, Amount = 150},
            new GcTechnologyRequirement { ID = "CAVE2",      InventoryType = Substance, Amount = 250}
        })
    };

    //...........................................................

    protected class TreeExpansion
    {
        public UnlockableItemTreeEnum Tree;
        public string RootTech;
        public string CostType;
    }

    //...........................................................

    protected override void Execute()
    {
        SetCraftabletoTrueAndAddRequirements();
        AddUnlockableTrees();
    }

    //...........................................................

    protected void SetCraftabletoTrueAndAddRequirements()
    {
        var prod_mbin = ExtractMbin<GcProductTable>(
            "METADATA/REALITY/TABLES/NMS_REALITY_GCPRODUCTTABLE.MBIN"
        );

        foreach (var prod in AlteredProductPrices)
        {
            prod_mbin.Table.Find(PRODUCT => PRODUCT.Id == prod.Item1).BaseValue = prod.Item2;
        };

        foreach (var prod in NewRequirementsArray)
        {
            var productId           = prod.Item1;
            var productRequirements = prod.Item2;
            var editProd            = prod_mbin.Table.Find(PRODUCT => PRODUCT.Id == productId);
            
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
        tree.Trees[0].Root.Children.Insert(0, new GcUnlockableItemTreeNode { Unlockable = "REPAIRKIT", Children = new() });

        var branch = tree.Trees[0].Root.Children.Find(NODE => NODE.Unlockable == "REPAIRKIT");

        branch.Children.Add(new GcUnlockableItemTreeNode
        {
            Unlockable = "NAV_DATA",
            Children   = new(){
                new GcUnlockableItemTreeNode { Unlockable = "BP_SALVAGE", Children = new() }
            }
        });

        branch.Children.Add(new GcUnlockableItemTreeNode
        {
            Unlockable = "WEAP_INV_TOKEN",
            Children   = new() {
                new GcUnlockableItemTreeNode {
                    Unlockable = "SUIT_INV_TOKEN",
                    Children = new() {
                        new GcUnlockableItemTreeNode { Unlockable = "FRIG_TOKEN",     Children = new() },
                        new GcUnlockableItemTreeNode { Unlockable = "SHIP_INV_TOKEN", Children = new() },
                        new GcUnlockableItemTreeNode { Unlockable = "FREI_INV_TOKEN", Children = new() }
                    }

                }
            }
        });
    }
}

//=============================================================================
