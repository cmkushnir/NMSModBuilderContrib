//=============================================================================

public class bStellarExtractor : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var mbin = ExtractMbin<TkAttachmentData>(
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/BUILDABLEPARTS/FREIGHTERBASE/ROOMS/EXTRROOM/PARTS/FLOOR0/ENTITIES/EXTRACTORTERMINAL.ENTITY.MBIN"
		);
		// speed is in seconds, and has to be a negative value. default = -79200 which is 22 hours, -3600 is 1 hour.
		
		var EXTRACTOR_SPEED = -7200;
		var EXTRACTOR_STORAGE = 6000;
		
		// change NOW strings to change item spawned. get IDs from here: https://docs.google.com/spreadsheets/d/1J8WdrubKgo8A9hPY-hbQLq4eVrb3n3lZAgiI2J7ncAU/edit#gid=66931870
		
		var products = new( string WAS, string NOW ) [] {
			new( "STELLAR2", "ASTEROID1" ),
			new( "GAS1", "TECHFRAG" ),
			new( "GAS2", "ASTEROID3" ),
			new( "GAS3", "ASTEROID2" )
		};
		
		foreach( var component in mbin.Components) {
			
			if (component is GcGeneratorUnitComponentData data) {
				var myList = data.MaintenanceData.PreInstalledTech;
				
				foreach( var product in products ) {
					var entry = myList.Find(OLD => OLD.Id == product.WAS);

					entry.MaxCapactiy = EXTRACTOR_STORAGE;
					entry.AmountEmptyTimePeriod = EXTRACTOR_SPEED;
					entry.Id = product.NOW;
				}		
			}
		}
	}

	//...........................................................
}

//=============================================================================
