//=============================================================================

public class NoPortalCharge : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var mbin                 = ExtractMbin<TkAttachmentData>("MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PORTAL/PORTAL/ENTITIES/BUTTON.ENTITY.MBIN");
		var item                 = mbin.Components[4] as GcMaintenanceComponentData;
		item.AutoCompleteOnStart = true;
	}

	//...........................................................
}

//=============================================================================
