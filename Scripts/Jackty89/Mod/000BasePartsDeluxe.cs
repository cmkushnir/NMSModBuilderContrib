//=============================================================================

namespace cmk.NMS.Scripts.Mod
{
	public class BasePartsDeluxe : cmk.NMS.ModScript
	{
		protected override void Execute()
		{
			ItemTrees();
		}

		//...........................................................

		protected void ItemTrees()
		{
			var mbin = ExtractMbin<GcUnlockableTrees>("METADATA/REALITY/TABLES/UNLOCKABLEITEMTREES.MBIN");
			
			var copyTree = CloneMbin(mbin.Trees[(int)GcUnlockableItemTreeGroups.UnlockableItemTreeEnum.BaseParts]);
			var copTreeTrees = copyTree.Trees;
			
			var basiceBaseParts = mbin.Trees[(int)GcUnlockableItemTreeGroups.UnlockableItemTreeEnum.BasicBaseParts];
			basiceBaseParts.Trees.Clear();
			
			foreach(var tree in copTreeTrees)
			{
				basiceBaseParts.Trees.Add(tree);	
			}
			
		}
	}
}

//=============================================================================
