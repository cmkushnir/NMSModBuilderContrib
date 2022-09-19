//=============================================================================
// Search all scene mbin's for "LIGHT" nodes.
// Report translations for each, and unique list of FALLOFF values for all.
//=============================================================================

public class Search_Scenes: cmk.NMS.Script.QueryClass
{
	protected override void Execute()
	{
		var save_path = Dialog.SaveFile(null, "Search_Scenes.txt");
		if( save_path.IsNullOrEmpty() ) return;

		var file = System.IO.File.CreateText(save_path);

		var TkSceneNodeDataClass = Game.Mbinc.FindClass("TkSceneNodeData");

		var falloff_values = new List<string>();

		foreach( var mbin_path in TkSceneNodeDataClass.PakItems ) {
			var scene_mbin = ExtractMbin<TkSceneNodeData>(mbin_path, true, false);
			if( scene_mbin == null || scene_mbin.Children.IsNullOrEmpty() ) continue;

			var nodes = new List<TkSceneNodeData>();

			scene_mbin.Visit(( NODE, PARENTS ) => {
				if( NODE.Type == "LIGHT" && !NODE.Attributes.IsNullOrEmpty() ) {
					nodes.Add(NODE);
				}
				return true;
			});

			if( nodes.IsNullOrEmpty() ) continue;

			file.WriteLine($"{mbin_path}:");
			foreach( var node in nodes ) {
				file.WriteLine($"\tName = {node.Name.Value} @ [{node.Transform.TransX}, {node.Transform.TransY}, {node.Transform.TransZ}]:");
				foreach( var attrib in node.Attributes ) {
					if( attrib.Name == "FALLOFF" ) falloff_values.AddUnique(attrib.Value);
					file.WriteLine($"\t\t{attrib.Name.Value} = {attrib.Value.Value}:");
				}
			}
		}

		file.WriteLine($"\n\nFALLOFF values:");
		foreach( var falloff_value in falloff_values ) {
			file.WriteLine($"\t{falloff_value}");
		}

		file.Flush();
		Log.AddSuccess("Finished");
	}
}

//=============================================================================
