//=============================================================================
// Author: Jackty89
//=============================================================================

public class PickUpGeoBays : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		PickUGeoBays();
	}
	protected void PickUGeoBays()
	{
		List <string> pickUpListIds = new List<string> () 
		{
			"GARAGE_B",
			"GARAGE_S",
			"GARAGE_M",
			"GARAGE_L",
			"GARAGE_MECH",
			"GARAGE_SUB"
		};
		var mbin = ExtractMbin<GcBaseBuildingTable>("METADATA/REALITY/TABLES/BASEBUILDINGOBJECTSTABLE.MBIN");		
		
		foreach( string pickupId in pickUpListIds)
		{
			mbin.Objects.Find(ID => ID.ID == pickupId).CanPickUp = true; 
		}
	}
	//...........................................................
}