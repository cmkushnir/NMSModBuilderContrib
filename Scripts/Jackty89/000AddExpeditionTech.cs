//=============================================================================
// Author: Jackty89
//=============================================================================

public class AddExpeditionTech : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		string [] ids = {"LAUNCHER_SPEC", "SHIPJUMP_SPEC", "HYPERDRIVE_SPEC"};

		var technology = ExtractMbin<GcTechnologyTable>(
			"METADATA/REALITY/TABLES/NMS_REALITY_GCTECHNOLOGYTABLE.MBIN"
		);

		foreach( var id in ids ) {
			AddTech(technology, id);
			AddTechToTree(id);
		}
	}

	//...........................................................

	protected void AddTech( GcTechnologyTable technology, string id )
	{
		var tech = technology.Table.Find(TECH => TECH.ID == id);
		tech.WikiEnabled  = true; //enable tech in wiki
		tech.FragmentCost = 2500; //add cost of 2.5k 
								  //tech.TechShopRarity = new GcTechnologyRarity { TechnologyRarity = GcTechnologyRarity.TechnologyRarityEnum.Common };
								  //tech.Rarity = new GcTechnologyRarity { TechnologyRarity = GcTechnologyRarity.TechnologyRarityEnum.Common };
	}

	//...........................................................

	protected void AddTechToTree( string id )
	{
		var tree = ExtractMbin<GcUnlockableTrees>(
			"METADATA/REALITY/TABLES/UNLOCKABLEITEMTREES.MBIN"
		);
		var weapTree = tree.Trees[(int)UnlockableItemTreeEnum.ShipTech];
		weapTree.Trees[0].Root.Children.Insert(0, new GcUnlockableItemTreeNode {
			Unlockable = id,
			Children = new()
		});
	}
}

//=============================================================================
