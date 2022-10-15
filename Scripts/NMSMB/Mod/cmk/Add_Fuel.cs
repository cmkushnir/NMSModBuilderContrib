//=============================================================================
// Add new substance(s) that can be used as fuel.
//=============================================================================

[NMSScriptDisabled]
public class Add_Fuel : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcSubstanceTable>(
			"METADATA/REALITY/TABLES/NMS_REALITY_GCSUBSTANCETABLE.MBIN"
		);
		// main thing is substance.Category.SubstanceCategory = SubstanceCategoryEnum.Fuel
		var substance = CloneMbin(mbin.Table.Find(SUBSTANCE => SUBSTANCE.ID == "FUEL1"));
		substance.ID          = "FUEL_ROD";
		substance.Name        = "FUEL ROD";
		substance.NameLower   = "Fuel Rod";
		substance.Description = "Nuclear fuel rod";
		mbin.Table.Add(substance);
	}
}

//=============================================================================
