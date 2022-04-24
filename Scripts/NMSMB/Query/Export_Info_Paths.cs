//=============================================================================
// Iterate through all game pak item info records.
// For each info record dump its path to a file.
// Game.PCBANKS.InfoTree is the merged tree that contains references to
// the info records from all PCBANKS/*.pak files.
//=============================================================================

public class Export_Info_Paths : cmk.NMS.Script.QueryClass
{
	protected override void Execute()
	{
		var save_path = Dialog.SaveFile(null, "Export_Info_Paths.log");
		if( save_path.IsNullOrEmpty() ) return;
		
		var file = System.IO.File.CreateText(save_path);
		
		Game.PCBANKS.InfoTree.ForEachTag(( INFO, CANCEL, LOG ) => {
			file.WriteLine($"{INFO.File.Path.Name} {INFO.Path.Full}");
		},	Cancel, Log);

		file.Flush();
		Log.AddSuccess("Finished");
	}
}

//=============================================================================
