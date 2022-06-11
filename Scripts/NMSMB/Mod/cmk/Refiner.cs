//=============================================================================
// Adjust personal, portable, medium refiners to have 3 inputs.
//=============================================================================

public class Refiner : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		Try(() => PersonalRefiner());
		Try(() => PortableRefiner());
		Try(() => MediumRefiner());
	}

	//...........................................................

	// - removing PreInstalledTech."MAINT_FUEL1" i.e. trying to make no fuel input
	//   causes crash when try to view refiner.
	// - changing "MAINT_FUEL1".CompletionRequirement to NoRequirement to make no cost
	//   (like Portal script) causes crash when try to view refiner.
	// - changing "MAINT_FUEL1" to "MAINT_FUEL5" i.e. to change generator type
	//   doesn't crash but still shows generator as Fuel Inverter not Kinetic Dynamo.
	// - making NumInputs > 3 causes crash when try to view refiner.
	protected void PersonalRefiner()
	{
		var mbin = ExtractMbin<TkAttachmentData>(
			"MODELS/COMMON/PLAYER/PLAYERCHARACTER/PLAYERCHARACTER/ENTITIES/PLAYERCHARACTER.ENTITY.MBIN"
		);
		var refiner = mbin.Components.FindFirst<GcRefinerUnitComponentData>();

		var maint = refiner.MaintenanceData.PreInstalledTech.Find(ELEMENT => ELEMENT.Id == "MAINT_FUEL1");
		maint.AmountEmptyTimePeriod *= 10;

        refiner.MaintenanceData.VisibleMaintenanceSlots = 5;  // 3 in, 1 fuel, 1 out
        refiner.NumInputs = 3;
	}

	//...........................................................

	protected void PortableRefiner()
	{
		var mbin = ExtractMbin<TkAttachmentData>(
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/BUILDABLEPARTS/TECH/REFINER/ENTITIES/REFINER1.ENTITY.MBIN"
		);
		var refiner = mbin.Components.FindFirst<GcRefinerUnitComponentData>();

		var maint = refiner.MaintenanceData.PreInstalledTech.Find(ELEMENT => ELEMENT.Id == "MAINT_FUEL1");
		maint.AmountEmptyTimePeriod *= 10;

        refiner.MaintenanceData.VisibleMaintenanceSlots = 5;  // 3 in, 1 fuel, 1 out
        refiner.NumInputs = 3;
	}

	//...........................................................

	protected void MediumRefiner()
	{
		var mbin = ExtractMbin<TkAttachmentData>(
			"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/BUILDABLEPARTS/TECH/REFINER/ENTITIES/REFINER2.ENTITY.MBIN"
		);
		var refiner = mbin.Components.FindFirst<GcRefinerUnitComponentData>();

        refiner.MaintenanceData.VisibleMaintenanceSlots = 4;  // 3 in, 0 fuel, 1 out
        refiner.NumInputs = 3;
	}
}

//=============================================================================
