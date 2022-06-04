//=============================================================================

public class MoreSalvageData : cmk.NMS.Script.ModClass
{
	public static int Min = 10;
	public static int Max = 20;
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcRewardTable>("METADATA/REALITY/TABLES/REWARDTABLE.MBIN");
		var reward_item       = mbin.SpecialRewardTable.Find(REWARD => REWARD.Id == "BP_SALVAGE").List.List[0].Reward as GcRewardSpecificProduct;
		reward_item.AmountMin = Min;
		reward_item.AmountMax = Max;
	}

	//...........................................................
}

//=============================================================================
