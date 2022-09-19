//=============================================================================

public class Bobbleheads: cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var infos = Game.PCBANKS.FindInfoRegex(
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PROPS/BOBBLEHEADS/.*.SCENE.MBIN"
		);
		foreach( var info in infos ) {
			if( info.Path.Name.StartsWith("BOBBLEHEAD") ) continue;
			var mbin = ExtractMbin<TkSceneNodeData>(info.Path);
			mbin.Transform.ScaleZ *= 5;
		}
	}
}

//=============================================================================
