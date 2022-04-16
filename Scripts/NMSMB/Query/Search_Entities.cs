//=============================================================================
// Search all entity mbin's for specific criteria.
//=============================================================================

public class Search_Entities : cmk.NMS.QueryScript
{
	protected override void Execute()
	{
		var TkAttachmentDataClass = Game.Mbinc.FindClass("TkAttachmentData");
		
		foreach( var path in TkAttachmentDataClass.PakItems ) {
			var mbin = ExtractMbin<TkAttachmentData>(path, true, false);
			// edit search criteria as desired:
			var comp = mbin?.Components.FindFirst<GcMaintenanceComponentData>();
			var elem = comp?.PreInstalledTech.Find(ELEM =>
				ELEM.AmountEmptyTimePeriod != 0 &&
				ELEM.Type.InventoryType != InventoryTypeEnum.Technology
			);
			if( elem != null ) Log.AddInformation($"{path}");
		}
		
		Log.AddSuccess("Finished");
	}
}

//=============================================================================
