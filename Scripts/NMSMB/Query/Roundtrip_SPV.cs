//=============================================================================

public class Roundtrip_SPV : cmk.NMS.Script.QueryClass
{
	protected override void Execute()
	{
		var root  = "X:/00/";
		var paths = new [] {
			"SHADERS/CODE/BIN/PC/ATMOSPHERE_FRAG_CLOUD_0.SPV",
			"SHADERS/CODE/BIN/PC/ATMOSPHERE_VERT_CLOUD_0.SPV",
			"SHADERS/CODE/BIN/PC/UI_COMP_SCREENEFFECT_0.SPV"
		};	
		foreach( var path in paths ) {
			var data = ExtractData<NMS.PAK.SPV.Data>(path);
			
			var name = data.Path.Name;
			
			var text1   = data.RawToTextSpirvCross(data.Raw, Log);
			var stream1 = data.TextToRawVeldrid(text1, Log);
			
			var text2   = data.RawToTextSpirvCross(stream1, Log);
			var stream2 = data.TextToRawVeldrid(text2, Log);

			var text3   = data.RawToTextSpirvCross(stream2, Log);
			var stream3 = data.TextToRawVeldrid(text3, Log);
			
			// text1 has no debug info, var names like _54.
			// text2 == text3, both decompiled from Veldrid compiled spv,
			// var names like _RESERVED_IDENTIFIER_FIXUP_54.
			
			System.IO.File.WriteAllText($"{root}1_{name}.glsl", text1);
			System.IO.File.WriteAllText($"{root}2_{name}.glsl", text2);			
			System.IO.File.WriteAllText($"{root}3_{name}.glsl", text2);			
		}	
		Log.AddSuccess("Finished");
	}
}

//=============================================================================
