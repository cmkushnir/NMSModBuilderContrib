//=============================================================================
// Author: Jackty89
//=============================================================================

public class InstantTextDisplay : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		ChangeTextSpeed();
	}

	//...........................................................

	protected void ChangeTextSpeed()
	{
		var mbin = ExtractMbin<GcPunctuationDelayTable>("METADATA/UI/SPECIALTEXTPUNCTUATIONDELAYDATA.MBIN");
		foreach( var punctuationDelay in mbin.PunctuationDelays ) {
			foreach( var punctuation in punctuationDelay.PunctuationList ) {
				punctuation.Delay = 0.0001f;
			}
			punctuationDelay.DefaultDelay = 0.0001f;
		}
	}
}

//=============================================================================
