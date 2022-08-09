//=============================================================================

public class bFreeShipSummons : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var files = new [] {
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/COMMONPARTS/LANDINGZONE/SUMMONSHIPBEACON/ENTITIES/SUMMONSHIP.ENTITY.MBIN",
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/ENTITIES/SUMMONSHIP.ENTITY.MBIN"
		};
		
		foreach ( var file in files ) {
			var mbin = ExtractMbin<TkAttachmentData>( file );
			
			var component = mbin.Components.FindFirst<GcSimpleInteractionComponentData>();
		
			component.ActivationCost.SubstanceId = "";
			component.ActivationCost.Cost = 0;
			
		}
		
	}

	//...........................................................
}

//=============================================================================
