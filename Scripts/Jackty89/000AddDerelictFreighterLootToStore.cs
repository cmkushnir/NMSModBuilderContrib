//=============================================================================
// Author: Jackty89
//=============================================================================

public class AddDerelictFreighterLootToStore : cmk.NMS.Script.ModClass
{
	
	readonly string TreeCostType = "SALVAGE";
	readonly string TreeRootTech = "SLIME_MED";
	
	protected class TreeExpansion
	{
		public UnlockableItemTreeEnum Tree;
		public string RootTech;
		public string CostType;
	}
	
	List<List<string>> ItemLists = new List<List<string>>()
	{
		new List<string>{
			"MEDTUBE",
			"HEATER",
			"FOORLIGHT",
			"PLANTTUBE"
		},
		new List<string>{    
			"ABAND_SHELF",
			"ABAND_CRATE_M",
			"ABAND_CRATE_L",
			"ABAND_CRATE_XL"
		},
		new List<string>{
			"ABAND_CASE",
			"FOOTLOCKER",
			"ABAND_BENCH",
			"PALLET"
		},
		new List<string>{
			"LOCKER2",
			"ABAND_BARREL"
		}
	};
		
	protected override void Execute()
	{
		TreeExpansion MainTreeExpansion = new TreeExpansion { Tree = UnlockableItemTreeEnum.BaseParts, CostType = TreeCostType, RootTech = TreeRootTech };
		ChangeScrapDealer();
		AddXClassUnlockableTrees(ItemLists, MainTreeExpansion);
	}

	//...........................................................

	protected void ChangeScrapDealer()
	{
		var scrapDealer = ExtractMbin<GcRealityManagerData>("METADATA/REALITY/DEFAULTREALITY.MBIN").TradeSettings.Scrap;

		string [] listOfIds = {
			"MEDTUBE",
			"HEATER",
			"FOORLIGHT",
			"PLANTTUBE",
			"LOCKER2",
			"ABAND_SHELF",
			"ABAND_CRATE_M",
			"ABAND_CRATE_L",
			"ABAND_CRATE_XL",
			"ABAND_CASE",
			"FOOTLOCKER",
			"ABAND_BENCH",
			"PALLET",
			"ABAND_BARREL"
		};

		foreach( string id in listOfIds ) {
			scrapDealer.OptionalProducts.Remove(scrapDealer.OptionalProducts.Find(PRODUCT => PRODUCT.Value == id));
			scrapDealer.AlwaysPresentProducts.Add(id);
		}

		scrapDealer.MinItemsForSale = scrapDealer.AlwaysPresentProducts.Count + 1;
		scrapDealer.MaxItemsForSale = scrapDealer.AlwaysPresentProducts.Count + 1;
	}
	
	
	protected void AddXClassUnlockableTrees(List<List<string>> ItemLists, TreeExpansion Expansion)
	{
		var Mbin = ExtractMbin<GcUnlockableTrees>("METADATA/REALITY/TABLES/UNLOCKABLEITEMTREES.MBIN");

		UnlockableItemTreeEnum ExTree = Expansion.Tree;
		string RootTech = Expansion.RootTech;
		string CostType = Expansion.CostType;
		
		var Tree = Mbin.Trees[(int)ExTree];
		string Title = Tree.Title;

		GcUnlockableItemTreeNode Root = new GcUnlockableItemTreeNode { Unlockable = RootTech, Children = new() };
		GcUnlockableItemTree ItemTree = new GcUnlockableItemTree { Title = Title, CostTypeID = CostType, Root = Root };

		Tree.Trees.Add(ItemTree);

		foreach (List<string> Items in ItemLists)
		{
			GcUnlockableItemTreeNode Parent = Root;
			string Child = Root.Unlockable;

			foreach (string Item in Items)
			{
				Parent.Children.Add(new GcUnlockableItemTreeNode
				{
					Unlockable = Item,
					Children = new()
				});
				var ChildNode = Parent.Children.Find(CHILD => CHILD.Unlockable == Item);
				Parent = ChildNode;
			}
		}
	}
	

}

//=============================================================================
