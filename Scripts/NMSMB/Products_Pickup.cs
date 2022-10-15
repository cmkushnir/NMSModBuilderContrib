//=============================================================================
// Allow products to be picked up and placed in player inventory.
//=============================================================================

public class Products_Pickup : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var products = new [] {
			"BUILDHARVESTER",  // Autonomous Mining Unit
		};
		var mbin = ExtractMbin<GcBaseBuildingTable>(
			"METADATA/REALITY/TABLES/BASEBUILDINGOBJECTSTABLE.MBIN"
		);
		foreach( var product in products ) {
			var entry = mbin.Objects.Find(ENTRY => ENTRY.ID == product);
			entry.CanPickUp = true;
		}
	}
}

//=============================================================================
