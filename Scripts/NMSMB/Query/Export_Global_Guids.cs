//=============================================================================

public class Export_Global_Guids : cmk.NMS.Script.QueryClass
{
	protected override void Execute()
	{
		var save_path = Dialog.SaveFile(null, "Export_Global_Guids.log");
		if( save_path.IsNullOrEmpty() ) return;

		var file = System.IO.File.CreateText(save_path);
		var list = new List<NMS.PAK.Item.Info>();
		
		Game.PCBANKS.ForEachInfo((INFO, LOG, CANCEL) => {
			if( INFO.Path.Full.Contains("GLOBAL")
				&& INFO.MbinHeader != null
			) {
		    	lock( list ) list.Add(INFO);
		    }
		});
		
		file.WriteLine($"Class GUID         MBIN GUID          Path");
		list.Sort();
		foreach( var info in list ) {
			file.WriteLine($"0x{info.MbinHeader.ClassGuid:x16} 0x{info.MbinHeader.RawGuid:x16} {info.Path.Full}");
		}

		file.Flush();
		Log.AddSuccess("Finished");
	}
}

//=============================================================================
