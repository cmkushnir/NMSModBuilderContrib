//=============================================================================
// Author: Jackty89
//=============================================================================

public class KeepTalkingChef : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcAlienPuzzleTable>("METADATA/REALITY/TABLES/NMS_DIALOG_GCALIENPUZZLETABLE.MBIN");
		var allOptions = mbin.Table.FindAll(ID => ID.Id == "EXOTIC_CHEF");
		foreach(var options in allOptions)
		{
			foreach(var option in options.Options)
			{				
				Log.AddInformation($"option.Cost.Value  = {option.Cost.Value }");
				if(option.Cost == "C_NEXUSCHEF1" || option.Cost == "C_NEXUSCHEF2" || option.Cost == "C_NEXUSCHEF3")
					option.KeepOpen = true;
			}
		}
	}
}

//=============================================================================
