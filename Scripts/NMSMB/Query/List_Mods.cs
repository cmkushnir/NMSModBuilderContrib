//=============================================================================

public class List_Mods: cmk.NMS.Script.QueryClass
{
	protected override void Execute()
	{
		foreach( var project in NMS.Script.Files.Mod ) {
			Log.AddInformation($"Project: {project.Folder}");
			foreach( var script in project ) {
				Log.AddInformation($"\t{script.Path}:");
				foreach( var mod in script.FindClass<NMS.Script.ModClass>() ) {
					Log.AddInformation($"\t\t{mod}");
				}
			}
		}
		Log.AddSuccess("Finished");
	}
}

//=============================================================================
