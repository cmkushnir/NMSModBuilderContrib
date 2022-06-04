//=============================================================================
// Author: Jackty89
//=============================================================================

public class CheapPetSlots : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		InteractionTable();
	}

	//...........................................................

	protected void InteractionTable()
	{
		var mbin = ExtractMbin<GcCostTable>(
			"METADATA/REALITY/TABLES/COSTTABLE.MBIN"
		);
		var petSlotCost = (GcCostMoneyList)mbin.InteractionTable.Find(ENTRY => ENTRY.Id == "C_PET_SLOT").Cost;
		petSlotCost.Costs[0] = 25;
		petSlotCost.Costs[1] = 50;
		petSlotCost.Costs[2] = 75;
		petSlotCost.Costs[3] = 100;
		petSlotCost.Costs[4] = 125;
		petSlotCost.Costs[5] = 150;
	}
}

//=============================================================================
