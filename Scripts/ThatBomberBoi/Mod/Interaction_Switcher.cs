//=============================================================================

/* using nms = libMBIN.NMS;
using libMBIN.NMS.Globals;
using libMBIN.NMS.GameComponents;
using libMBIN.NMS.Toolkit; */

//=============================================================================

namespace cmk.NMS.Scripts.Mod
{
	
	
	public class Interaction_Switcher : cmk.NMS.Script.ModClass
	{
		protected override void Execute()
		{
			TkAttachmentData();
		}

		//...........................................................

		protected void TkAttachmentData()
		{
			var stationEntity = ExtractMbin<TkAttachmentData>("MODELS/SPACE/SPACESTATION/MODULARPARTS/DOCK/BACK_SECTION/ENTITIES/TERMINALDATA.ENTITY.MBIN");
			var interactionComponent = stationEntity.Components.FindFirst<GcInteractionComponentData>();
			
			//Adding Interact
			interactionComponent.InteractionType.InteractionType = GcInteractionType.InteractionTypeEnum.WeaponTerminal;
			
			//Adding Simple Interaction
			/*
			stationEntity.Components.Remove(interactionComponent);
			var simpleInteractionComponent = new GcSimpleInteractionComponentData();
						simpleInteractionComponent.SimpleInteractionType = GcSimpleInteractionComponentData.SimpleInteractionTypeEnum.Portal;
			stationEntity.Components.Add(simpleInteractionComponent);	
			*/		
			
			
			//...........................................................
			
			
		}
	}
}

//=============================================================================
