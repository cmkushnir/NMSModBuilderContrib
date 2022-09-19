//=============================================================================
// Search all game info records for ones with matching paths.
// Use a site like https://regex101.com/ to help with generating valid regex patterns.
//=============================================================================


public class Find_Info: cmk.NMS.Script.QueryClass
{
	protected override void Execute()
	{
		Log.AddInformation($"Searching ...");

		// various FindInfo*() methods e.g regex, predicate, startswith, endwith, contains.
		var regex = new Regex("VOXEL");
		var list  = Game.PCBANKS.FindInfo(regex);

		foreach( var info in list ) {
			Log.AddInformation(info.Path);
		}

		Log.AddSuccess($"Finished: {list.Count} found");
	}
}

//=============================================================================
