//=============================================================================
// Proof-of-concept.  Replace shader main.
// View shader in generated pak to view diffs and verify identical other than change.
//=============================================================================

public class Shader_Replace : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var data = ExtractData<NMS.PAK.SPV.Data>(
			"SHADERS/CODE/BIN/PC/ATMOSPHERE_VERT_REFLECT_WATER_0.SPV"
		);
		
		// get existing "void main() { ... }" string
		var main_old = data.Main();
		// can manip main_old and feed back to change main ...
		
		// ... or fully replace main
		var text_new = data.Main(@"
			void main()
			{
				vec4 _198 = _54.lUniforms.mpCommonPerMesh.gWorldMat4 * vec4(mkLocalPositionVec4.xyz, 1.0);
				Out.mWorldPositionVec4 = _198;
				vec3 _86 = normalize(_198.xyz - _54.lUniforms.mpCommonPerMesh.gPlanetPositionVec4.xyz);
				Out.mWorldNormalVec3_mfDistanceFromPlanet = vec4(_86.x, _86.y, _86.z, Out.mWorldNormalVec3_mfDistanceFromPlanet.w);
				Out.mWorldNormalVec3_mfDistanceFromPlanet.w =
					length(_54.lUniforms.mpPerFrame.gViewPositionVec3 - _54.lUniforms.mpCommonPerMesh.gPlanetPositionVec4.xyz)
					- _54.lUniforms.mpCommonPerMesh.gPlanetPositionVec4.w
					+ 1  // adding this is the only change
				;
				Out.mTexCoordsVec4 = mkTexCoordsVec4;
				vec4 _202 = _54.lUniforms.mpPerFrame.gViewProjectionMat4 * _198;
				Out.mScreenSpacePositionVec4 = _202;
				gl_Position = _202;
			}
		");
	}
}

//=============================================================================
