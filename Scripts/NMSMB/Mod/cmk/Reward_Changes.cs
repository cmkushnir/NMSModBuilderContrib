//=============================================================================
// - Add nanite rewards to plotted plants that give 10 carbon.
// - Increaase nanite|special reward values for all rewards.
//=============================================================================

public class Reward_Changes : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcRewardTable>(
			"METADATA/REALITY/TABLES/REWARDTABLE.MBIN"
		);
		
		var ListGcGenericRewardTableEntry_t = typeof(List<GcGenericRewardTableEntry>);
		
		// increase possible amount of nanites and specials receieve from all rewards
		foreach( var field in mbin.GetType().GetFields() ) {
			if( field.FieldType != ListGcGenericRewardTableEntry_t ) continue;
			var list = field.GetValue(mbin) as List<GcGenericRewardTableEntry>;
			foreach( var entry in list ) {
				foreach( var item in entry.List.List ) {
					if( item.Reward is GcRewardMoney money ) {
						if( money.Currency.Currency != CurrencyEnum.Units ) {
							money.AmountMin *=  2;
							money.AmountMax *= 10;
						}
					}
				}
			}
		}
		
		// make those otherwise useless planters w/ 10 carbon usefull.
		var plants = mbin.GenericTable.Find(ENTRY => ENTRY.Id == "INTERIORPLANTS");
		plants.List.List.Add(RewardTableItem.Nanites(100, 1000));		
	}
}

//=============================================================================
