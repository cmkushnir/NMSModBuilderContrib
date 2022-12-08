public class PrintUnlockableTechTrees: cmk.NMS.Script.QueryClass {
    protected override void Execute() {
        var Unlockable = Game.ExtractMbin<GcUnlockableTrees>("METADATA/REALITY/TABLES/UNLOCKABLEITEMTREES.MBIN");
        foreach (var tree in Enum.GetValues<UnlockableItemTreeEnum>()) {
            Log.AddHeading(Enum.GetName<UnlockableItemTreeEnum>(tree));
            PrintTree(Unlockable.Trees[(int)tree]);
        }
    }

    string Translate(string id) => Game.FindLanguageData(id)?.Text ?? id;

    void PrintTree(GcUnlockableItemTrees tree) {
        Log.AddInformation($"UnlockableItemTree: {Translate(tree.Title)} ({tree.Title})");
        foreach (var branch in tree.Trees)
            PrintTree(branch);
    }

    void PrintTree(GcUnlockableItemTree tree) {
        Log.AddInformation($"  {Translate(tree.Title)} cost={tree.CostTypeID} ({tree.Title})");
        PrintTree(tree.Root);
    }

    void PrintTree(GcUnlockableItemTreeNode node, int depth = 2) {
        Log.AddInformation(
            new StringBuilder(80)
            .Insert(0, "  ", depth)  // why this isn't part of Append I can't even
            .Append(node.Unlockable)
            .Append(" (")
            .Append(Translate(node.Unlockable))
            .Append(")")
            .ToString()
        );
        foreach (var child in node.Children)
            PrintTree(child, depth + 1);
    }
}
