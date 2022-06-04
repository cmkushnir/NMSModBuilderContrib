//=============================================================================
// Author: Jackty89
//=============================================================================

public class BasePartsDeluxe : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		ItemTrees();
	}

	//...........................................................

	protected void ItemTrees()
	{
		var mbin = ExtractMbin<GcUnlockableTrees>(
			"METADATA/REALITY/TABLES/UNLOCKABLEITEMTREES.MBIN"
		);

		var copyTree = CloneMbin(mbin.Trees[(int)UnlockableItemTreeEnum.BaseParts]);
		var copTreeTrees = copyTree.Trees;

		var basiceBaseParts = mbin.Trees[(int)UnlockableItemTreeEnum.BasicBaseParts];
		//basiceBaseParts.Trees.Clear();

		foreach( var tree in copTreeTrees ) {
			basiceBaseParts.Trees.Add(tree);
		}
	}
}

//=============================================================================
