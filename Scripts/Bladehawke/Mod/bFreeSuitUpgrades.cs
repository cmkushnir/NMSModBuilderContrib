//=============================================================================

public class bFreeSuitUpgrades : cmk.NMS.Script.ModClass
{
  protected override void Execute()
  {
    var mbin = ExtractMbin<TkAttachmentData>(
      "MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/COMMONPARTS/CRYOCHAMBER/ENTITIES/CRYOCHAMBERINTERACTION.ENTITY.MBIN"
    );
    var original = CloneList(mbin.Components);
    
        var data = new GcMaintenanceComponentData
        {
      VisibleMaintenanceSlots = -1,
      AllowRepair = true,
      UseBoundsForIconCentre = true,
            Interactable = GcMaintenanceComponentData.InteractableEnum.Always,
      CompletedTransitionDelay = 1,
      ShareInteractionModelRender = true,
            ModelRendererData = new TkModelRendererData
            {
                Camera = new TkModelRendererCameraData
                {
          Distance = 1.5f,
          LightPitch = 45,
          LightRotate = 45,
                    Wander = new TkCameraWanderData
                    {
            CamWanderPhase = 0.003f,
            CamWanderAmplitude = 0.5f,
          },
        },
        Fov = 30,
        AspectRatio = 1.777f,
        ThumbnailMode = TkModelRendererData.ThumbnailModeEnum.None,
        FocusType = TkModelRendererData.FocusTypeEnum.ResourceBounds,
        BlendInTime = 0.8f,
        HeightOffset = 0.5f,
        UsePlayerCameraInHmd = true,
      },
      ModelRendererResource = GcMaintenanceComponentData.ModelRendererResourceEnum.ModelNode,
            BroadcastLevel = new GcBroadcastLevel
            {
        BroadcastLevel = GcBroadcastLevel.BroadcastLevelEnum.LocalModel,
      },
      Title = "UI_DROPPOD_MAINT_TITLE",
      Description = "UI_DROPPOD_MAINT_SUB",
      ActionButtonOverride = "REPAIR",
      ActionDescriptionOverride = "REPAIR_DESC",
      ActionWarningOverride = "WARN_TECH_DAMAGED",
    };
        
    data.ModelRendererDataAlt = data.ModelRendererData;

    mbin.Components.Clear();
    
    mbin.Components.Add(data);
    
        foreach (var item in original)
        {
      mbin.Components.Add(item);
        }
    
    var interaction = mbin.Components.FindFirst<GcInteractionComponentData>();
    
    interaction.RepeatInteraction = true;
    interaction.ReseedAfterRewardSuccess = true;
    
        var trigger = mbin.Components.FindFirst<GcTriggerActionComponentData>();
        trigger.States.Remove(trigger.States.Find(NAME => NAME.StateID == "INTERACTOPTION"));

    mbin = ExtractMbin<TkAttachmentData>(
      "MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/COMMONPARTS/CRYOCHAMBERPOD/ENTITIES/CRYOCHAMBERPOD.ENTITY.MBIN"
    );

    original = new();

        foreach (var component in mbin.Components)
        {
            if (component is GcMaintenanceComponentData)
                continue;
      original.Add(component);
    }
    
    mbin.Components.Clear();
        mbin.Components.Add(data);

        foreach (var component in original)
        {
            mbin.Components.Add(component);
        }
        ;

    interaction = mbin.Components.FindFirst<GcInteractionComponentData>();
    
    interaction.RepeatInteraction = true;
    interaction.ReseedAfterRewardSuccess = true;		

        trigger = mbin.Components.FindFirst<GcTriggerActionComponentData>();
        trigger.States.Remove(trigger.States.Find(NAME => NAME.StateID == "INTERACTOPTION"));
  }

  // This could probably go into Util, it's potentially handy	
    protected List<T> CloneList<T>(List<T> source) where T : class
  {
    return new List<T>(source);
  }
  
  //...........................................................
}

//=============================================================================
