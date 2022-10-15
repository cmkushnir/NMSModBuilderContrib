//=============================================================================
// Author: Jackty89
//=============================================================================

public class LearnMoreWords : cmk.NMS.Script.ModClass
{
	public int AddWordsTotal = 15;
	public int PercentageChance = 100;
	protected override void Execute()
	{
		AddWords();
	}

	//...........................................................

	protected void AddWords()
	{
		var mbin = ExtractMbin<GcRewardTable>("METADATA/REALITY/TABLES/REWARDTABLE.MBIN");

		AddWordsTotal          -= 1;
		string   stoneId       = "WORD";
		string[] wordIds       = {"TRA_WORD", "EXP_WORD", "WAR_WORD", "TEACHWORD_EXP", "TEACHWORD_TRA", "TEACHWORD_WAR", "TEACHWORD_ATLAS"};
		string[] directWordIds = {"TRA_WORD_", "EXP_WORD_", "WAR_WORD_"};
		string[] directOptions = {"DIRECT", "HELP", "TRADE", "LORE", "TECH", "THREAT", "MISC"};

		AddWord(stoneId, AddWordsTotal, mbin.GenericTable);

		foreach( var wordId in wordIds ) {
			AddWord(wordId, AddWordsTotal, mbin.InteractionTable);
		}

		foreach( var wordString in directWordIds ) {
			DirectWords(wordString, directOptions, AddWordsTotal, mbin.InteractionTable);
		}
	}

	//...........................................................

	protected void AddWord( string id, int addWords, List<GcGenericRewardTableEntry> table )
	{
		var word       = table.Find(WORD => WORD.Id == id);
		var wordReward = CloneMbin(word.List.List[0]);
		wordReward.PercentageChance = PercentageChance;
		var totalWords = word.List.List.Count + addWords;

		for( int i = 0; i <= totalWords; i++ ) {
			word.List.List.Add(wordReward);
		}
	}

	//...........................................................

	protected void DirectWords( string idString, string[] directOptions, int addWords, List<GcGenericRewardTableEntry> table )
	{
		foreach( string option in directOptions ) {
			var id   = idString + option;
			var word = table.Find(WORD => WORD.Id == id);
			word.List.RewardChoice = RewardChoiceEnum.GiveAll;
			var wordReward = CloneMbin(word.List.List[1]);
			wordReward.PercentageChance = PercentageChance;
			var totalWords = word.List.List.Count + addWords - 1;

			//As the direct teachign already has 2 words (specifc and misc) changing it to GiveAll would let us learn 16 words instead of 15
			for( int i = 0; i <= totalWords; i++ ) {
				word.List.List.Add(wordReward);
			}
		}
	}
}

//=============================================================================
