//=============================================================================
// Author: Jackty89
//=============================================================================
public class EqualPlantTimers : cmk.NMS.Script.ModClass
{
	public int PlantTimer    = 3600; //time in seconds so = 1 hour
	public int HarvestAmount = 50;

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

		foreach( var plant in plantList ) {
			var path         = "MODELS/PLANETS/BIOMES/COMMON/INTERACTIVEFLORA/" + plant + "/ENTITIES/PLANTINTERACTION.ENTITY.MBIN";
			var mbin         = ExtractMbin<TkAttachmentData>(path);
			var plantTrigger = mbin.Components.FindFirst<GcSimpleInteractionComponentData>();

			foreach( var triggerTimer in plantTrigger.BaseBuildingTriggerActions ) {
				if( triggerTimer.Time > 500 )
					triggerTimer.Time = PlantTimer;
			}
		}

		List<string> plantProductIDList = new List<string>()
		{
			"PEARLPLANT",
			"BARRENPLANT",
			"CREATUREPLANT",
			"GRAVPLANT",
			"LUSHPLANT",
			"NIPPLANT",
			"POOPPLANT",
			"RADIOPLANT",
			"SCORCHEDPLANT",
			"SNOWPLANT",
			"TOXICPLANT",
			"SACVENOMPLANT"
		};
		foreach( var plantID in plantProductIDList ) {
			AddCustomProductDescription(plantID);
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
		foreach( var plantId in plantList ) {
			//Log.AddInformation($"Print plant  = {plantId }");
			var reward_item = mbin.GenericTable.Find(REWARD => REWARD.Id == plantId).List.List[0].Reward as GcRewardSpecificSubstance;
			reward_item.AmountMin = HarvestAmount;
			reward_item.AmountMax = HarvestAmount;
		}
	}

	protected void AddCustomProductDescription( string plantID )
	{
		var prod_mbin = ExtractMbin<GcProductTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCPRODUCTTABLE.MBIN");
		var product   = prod_mbin.Table.Find(PRODUCT => PRODUCT.ID == plantID);

		var oldDescriptionID    = product.Description;
		var customDescriptionID = "C" + product.Description;

		product.Description = customDescriptionID;

		AddNewLanguageString(product.ID, customDescriptionID, oldDescriptionID);
	}

	protected void AddNewLanguageString( string foodId, string customDescriptionID, string oldDescriptionID )
	{
		var regex = new Regex(": .*<>");
		foreach( var language in NMS.Game.Language.Identifier.List ) {
			var data         = GetLanguageData(language, oldDescriptionID);
			var newDescrText = regex.Replace(data.Text, ": <IMG>CLOCK<> 01h : 00m : 00s");  // Regex.Replace(data.Text, ": .*<>", ": <IMG>CLOCK<> 01h : 00m : 00s");
			SetLanguageText(language, customDescriptionID, newDescrText);
			Log.AddSuccess(language.Text);
			Log.AddInformation(
				$"oldDescriptionID = {oldDescriptionID}\n" +
				$"customDescriptionID = {customDescriptionID}\n" +
				$"data.Text = {data.Text}\n" +
				$"newDescrText = {newDescrText}\n"
			);
		}
	}
}

//=============================================================================