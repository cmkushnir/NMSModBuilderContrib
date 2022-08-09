//=============================================================================

public class bSalvagedData : cmk.NMS.Script.ModClass
{
    public static int Multiplier = 3; // Multiply the amount of salvaged data dug up by this
    
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcRewardTable>(
			"METADATA/REALITY/TABLES/REWARDTABLE.MBIN"
		);
		
		var ListGcGenericRewardTableEntry_t = typeof(List<GcGenericRewardTableEntry>);
		
		// increase possible amount of resources to receieve from all rewards
		foreach( var field in mbin.GetType().GetFields() ) {
			if( field.FieldType != ListGcGenericRewardTableEntry_t ) continue;
			var list = field.GetValue(mbin) as List<GcGenericRewardTableEntry>;
			foreach( var entry in list ) {
				foreach( var item in entry.List.List ) {
					if( item.Reward is GcRewardSpecificProduct salvage ) {
						if (salvage.ID == "BP_SALVAGE") {
							salvage.AmountMin *= Multiplier;
							salvage.AmountMax *= Multiplier;
						}
					}
				}
			}
		}
	}

	//...........................................................
}

//=============================================================================
