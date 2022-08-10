//=============================================================================

public class bFasterFarming : cmk.NMS.Script.ModClass
{
  public static int GrowTime = 3600; // seconds
  
  protected override void Execute()
  {
	var entities = Game.Mbinc.FindClass("TkAttachmentData");
        foreach (var path in entities.PakItems)
        {  	
    		if (!path.StartsWith("MODELS/PLANETS/BIOMES/COMMON/INTERACTIVEFLORA/FARM") ||
        	    !path.EndsWith("/ENTITIES/PLANTINTERACTION.ENTITY.MBIN"))
        	    continue;
        	    
      		var mbin = ExtractMbin<TkAttachmentData>(path);

      		var data = mbin.Components.FindFirst<GcSimpleInteractionComponentData>();
      
      		var index = data.BaseBuildingTriggerActions.FindIndex(STEP => STEP.TriggerAction == "STEP1_ENTER");
      		data.BaseBuildingTriggerActions[index++].Time = (GrowTime / 2) - 1;
      		data.BaseBuildingTriggerActions[index++].Time = GrowTime / 2;
      		data.BaseBuildingTriggerActions[index++].Time = GrowTime - 1;
      		data.BaseBuildingTriggerActions[index].Time = GrowTime;
    }
  }

  //...........................................................
}

//=============================================================================
