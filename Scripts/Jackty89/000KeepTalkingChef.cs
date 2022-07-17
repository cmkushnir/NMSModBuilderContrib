//=============================================================================

public class KeepTalkingChef : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcAlienPuzzleTable>("METADATA/REALITY/TABLES/NMS_DIALOG_GCALIENPUZZLETABLE.MBIN");
		var options = mbin.Table.Find(ID => ID.Id == "EXOTIC_CHEF").Options;
		foreach(var option in options)
		{
			option.KeepOpen = true;
		}
	}

	//...........................................................
}

//=============================================================================
