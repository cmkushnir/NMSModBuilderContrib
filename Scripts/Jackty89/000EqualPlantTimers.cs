//=============================================================================

public class EqualPlantTimers : cmk.NMS.Script.ModClass
{
	public static int PlantTimer    = 3600; //time in seconds so = 1 hour
	public static int HarvestAmount = 50;
	protected override void Execute()
	{
		ChangePlantTimers();
		ChangePlantAmounts();
	}
		
	protected void ChangePlantTimers()
	{
		List <string> plantList = new List<string> () 
		{
			"FARMALBUMEN",
			"FARMBARREN",
			"FARMDEADCREATURE",
			"FARMGRAVITINO",
			"FARMLUSH",
			"FARMNIP",
			"FARMPOOP",
			"FARMRADIOACTIVE",
			"FARMSCORCHED",
			"FARMSNOW",
			"FARMTOXIC",
			"FARMVENOMSAC"
		};
		foreach(var plant in plantList)
		{
			var path         = "MODELS/PLANETS/BIOMES/COMMON/INTERACTIVEFLORA/" + plant + "/ENTITIES/PLANTINTERACTION.ENTITY.MBIN";
			var mbin         = ExtractMbin<TkAttachmentData>(path);
			var plantTrigger = mbin.Components.FindFirst<GcSimpleInteractionComponentData>();
			
			foreach(var triggerTimer in plantTrigger.BaseBuildingTriggerActions)
			{
				if(triggerTimer.Time > 500)
					triggerTimer.Time = PlantTimer;
			}
		}
	}
	
	protected void ChangePlantAmounts()
	{
		List <string> plantList = new List<string> () 
		{
			"PLANT_BARREN",
			"PLANT_LUSH",
			"PLANT_CREATURE",
			"PLANT_POOP",
			"PLANT_RADIO",
			"PLANT_SCORCHED",
			"PLANT_SNOW",
			"PLANT_TOXIC",
		};
		
		var mbin = ExtractMbin<GcRewardTable>("METADATA/REALITY/TABLES/REWARDTABLE.MBIN");
		foreach(var plantId in plantList)
		{			
			//Log.AddInformation($"Print plant  = {plantId }");
			var reward_item = mbin.GenericTable.Find(REWARD => REWARD.Id == plantId).List.List[0].Reward as GcRewardSpecificSubstance;
			reward_item.AmountMin = HarvestAmount;
			reward_item.AmountMax = HarvestAmount;
		}
	}
	//...........................................................
}
//=============================================================================