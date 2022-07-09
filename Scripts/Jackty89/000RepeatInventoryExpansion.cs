//=============================================================================

public class RepeatInventoryExpansion : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var mbin                                 = ExtractMbin<TkAttachmentData>("MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/COMMONPARTS/CRYOCHAMBER/ENTITIES/CRYOCHAMBERINTERACTION.ENTITY.MBIN");
		var interactionComp                      = mbin.Components[2] as GcInteractionComponentData;
		interactionComp.RepeatInteraction        = true;
		interactionComp.ReseedAfterRewardSuccess = true;
		
		var triggerComp                          = mbin.Components[4] as GcTriggerActionComponentData;
		triggerComp.States.RemoveAt(2);
	}

	//...........................................................
}

//=============================================================================
