//=============================================================================

public class GenericRewardTableEntry
{
	public static GcGenericRewardTableEntry Create(
		string                  ID,
		RewardChoiceEnum        CHOICE             = RewardChoiceEnum.GiveAll,
		List<GcRewardTableItem> LIST               = null,
		bool                    OVERRIDE_ZERO_SEED = false
	) => new() {
		Id   = ID,
		List = new() {
			RewardChoice     = CHOICE,
			OverrideZeroSeed = OVERRIDE_ZERO_SEED,
			List             = LIST ?? new()
		}
	};
}

//=============================================================================
