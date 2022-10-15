//=============================================================================

public class Configure : NMS.Script.ModClass
{
	protected override void Execute()
	{
		ModFiles.AfterBuildPakStream -= OnAfterBuildPakStream;
		ModFiles.AfterBuildPakStream += OnAfterBuildPakStream;

		Presets();

		Postsets();
	}

	//...........................................................

	public override void OnDispose()
	{
		ModFiles.AfterBuildPakStream -= OnAfterBuildPakStream;
	}

	//...........................................................

	protected static void OnAfterBuildPakStream( NMS.Script.ModFiles SCRIPTS, CancellationToken CANCEL )
	{
		SCRIPTS.AfterBuildPakStream -= OnAfterBuildPakStream;

		SCRIPTS.ExecuteLog.AddInformation("");
		SCRIPTS.ExecuteLog.AddHeading("List modified items extracted by multiple scripts");

		foreach( var entry in SCRIPTS.DataCache ) {
			if( CANCEL.IsCancellationRequested ) break;
			var data = entry.Data;
			var mods = entry.Mods;
			if( !data.IsEdited || mods.Count < 2 ) continue;
			SCRIPTS.ExecuteLog.AddSuccess(data.Path.Full);
			foreach( var mod in mods ) {
				SCRIPTS.ExecuteLog.AddInformation($"\t{mod.Name}");
			}
		}

		SCRIPTS.ExecuteLog.AddHeading("Finished listing modified items extracted by multiple scripts");
	}

	//...........................................................

	protected void Presets()
	{
	}

	//...........................................................

	protected void Postsets()
	{
	}
}

//=============================================================================
