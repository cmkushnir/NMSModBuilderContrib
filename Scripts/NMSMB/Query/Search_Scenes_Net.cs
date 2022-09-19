//=============================================================================
// Search all scene mbin's for TkNetReplicatedEntityComponentData attachments.
//=============================================================================

public class Search_Scenes: cmk.NMS.Script.QueryClass
{
	protected override void Execute()
	{
		var save_path = Dialog.SaveFile(null, "Search_Scenes.txt");
		if( save_path.IsNullOrEmpty() ) return;

		var file = System.IO.File.CreateText(save_path);

		var TkSceneNodeDataClass = Game.Mbinc.FindClass("TkSceneNodeData");

		var scenes = new List<string>();  // mbin_path's

		foreach( var mbin_path in TkSceneNodeDataClass.PakItems ) {
			var scene_mbin = ExtractMbin<TkSceneNodeData>(mbin_path, true, false);
			if( scene_mbin == null || scene_mbin.Children.IsNullOrEmpty() ) continue;

			scene_mbin.Visit(( NODE, PARENTS ) => {
				var attr = NODE.Attributes.Find(ATTR =>
					ATTR.Name == "ATTACHMENT" &&
					ATTR.Value.Value.Contains(".ENTITY.")
				);
				if( attr != null ) {
					var entity = ExtractMbin<TkAttachmentData>(attr.Value, true, false);
					var comp   = entity?.Components?.FindFirst<TkNetReplicatedEntityComponentData>();
					if( comp  != null ) {
						scenes.AddUnique(mbin_path);
						return false;  // stop searching scene_mbin
					}
				}
				return true;  // keep searching scene_mbin
			});
		}

		scenes.Sort();

		file.WriteLine($"Scene's with ENTITY attachment that has a TkNetReplicatedEntityComponentData:");
		foreach( var scene in scenes ) {
			file.WriteLine(scene);
		}

		file.Flush();
		Log.AddSuccess("Finished");
	}
}

//=============================================================================
