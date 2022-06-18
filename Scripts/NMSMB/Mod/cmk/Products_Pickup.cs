//=============================================================================
// Allow products to be picked up and placed in player inventory.
//=============================================================================

public class Products_Pickup : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var paths = new [] {
			"METADATA/REALITY/TABLES/BASEBUILDINGTABLE.MBIN",
			"METADATA/REALITY/TABLES/BASEBUILDINGOBJECTSTABLE.MBIN",
		};
		var products = new [] {
			"BUILDHARVESTER",  // Autonomous Mining Unit
		};
		foreach( var path in paths ) {
			var mbin = ExtractMbin<GcBaseBuildingTable>(path);
			foreach( var product in products ) {
				var entry = mbin.Objects.Find(ENTRY => ENTRY.ID == product);
				entry.CanPickUp = true;
			}
		}
	}
}

//=============================================================================
