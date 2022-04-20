//=============================================================================

using nms = libMBIN.NMS;
using libMBIN.NMS.Globals;
using libMBIN.NMS.GameComponents;
using libMBIN.NMS.Toolkit;

//=============================================================================

namespace cmk.NMS.Scripts.Mod
{
	
	
	public class Interaction_Switcher : cmk.NMS.ModScript
	{
		protected override void Execute()
		{
			TkAttachmentData();
		}

		//...........................................................

		protected void TkAttachmentData()
		{
			var InteractionRoot1 = ExtractMbin<TkAttachmentData>(
				"MODELS/SPACE/SPACESTATION/MODULARPARTS/DOCK/BACK_SECTION/ENTITIES/TERMINALDATA.ENTITY.MBIN" // The file which will have its interaction changed
			);
			
			var StationInteract = ExtractMbin<GcInteractionComponentData>("MODELS/SPACE/SPACESTATION/MODULARPARTS/DOCK/BACK_SECTION/ENTITIES/TERMINALDATA.ENTITY.MBIN");
			
			StationInteract.InteractionType.InteractionType = GcInteractionType.InteractionTypeEnum.MPMissionGiver;
			
			
			
			//...........................................................
			
			/*var InteractionRoot2 = Mbin<mbin_tk.TkAttachmentData>(
               "MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/TERRAINRESOURCE/TERRAINRESOURCE/ENTITIES/SCANLOCATOR.ENTITY.MBIN" // All steps same as above, except that here we add an interaction to an entity without one (Cant get it to work with anything yet, since its main use was buildables and those are weird now)
			);
			
			InteractionRoot2.Components.Add(InteractionToBeChanged1);
            var InteractionToBeChanged2 = InteractionRoot2.Components[1];
            InteractionToBeChanged2.InteractionType.InteractionType = InteractionTypeEnum.WeaponUpgrade;
            
            //...........................................................
            
            var InteractionRoot3 = Mbin<mbin_tk.TkAttachmentData>(
				"MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/HOLOCOM/HOLOCOM/ENTITIES/HOLOCOM.ENTITY.MBIN" // The file which will have its interaction changed
			);
			
			
			var InteractionToBeChanged3 = InteractionRoot3.Components[1]; // InteractionToBeChanged1 is the Variable which is assigned the GcInteractionComponentData
			InteractionToBeChanged3.InteractionType.InteractionType = InteractionTypeEnum.ShipPilot; //The Interaction Type Nested Inside The variable's InteractionType is then assigned to one of the interactions in the Enum
			*/
		}
	}
}

//=============================================================================
