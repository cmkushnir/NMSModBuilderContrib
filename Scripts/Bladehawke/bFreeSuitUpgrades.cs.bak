//=============================================================================

public class bFreeSuitUpgrades : cmk.NMS.Script.ModClass
{
  protected override void Execute()
  {
    var mbin = ExtractMbin<TkAttachmentData>(
      "MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/COMMONPARTS/CRYOCHAMBER/ENTITIES/CRYOCHAMBERINTERACTION.ENTITY.MBIN"
    );
    var original = CloneList(mbin.Components);
    
    GcMaintenanceComponentData data = new() {
      VisibleMaintenanceSlots = -1,
      AllowRepair = true,
      UseBoundsForIconCentre = true,
      Interactable = GcMaintenanceComponentData.InteractableEnum.OnlyWhenNotComplete,
      CompletedTransitionDelay = 1,
      ShareInteractionModelRender = true,
      
      ModelRendererData = new() {
        Camera = new() {
          Distance = 1.5f,
          LightPitch = 45,
          LightRotate = 45,
          Wander = new() {
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

      BroadcastLevel = new() {
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
    
    foreach(var item in original) {
      mbin.Components.Add(item);
    };
    
    var interaction = mbin.Components.FindFirst<GcInteractionComponentData>();
    
    interaction.RepeatInteraction = true;
    interaction.ReseedAfterRewardSuccess = true;
    
    mbin = ExtractMbin<TkAttachmentData>(
      "MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/COMMONPARTS/CRYOCHAMBERPOD/ENTITIES/CRYOCHAMBERPOD.ENTITY.MBIN"
    );

    original = new();

    foreach(var component in mbin.Components) {
      if (component is GcMaintenanceComponentData) continue;
      original.Add(component);
    }
    
    mbin.Components.Clear();
    foreach(var item in original) {
      mbin.Components.Add(item);
    };

    interaction = mbin.Components.FindFirst<GcInteractionComponentData>();
    
    interaction.RepeatInteraction = true;
    interaction.ReseedAfterRewardSuccess = true;		
  }

  // This could probably go into Util, it's potentially handy	
  protected List<T> CloneList<T>(List<T> source) where T: class
  {
    return new List<T>(source);
  }
  
  //...........................................................
}

//=============================================================================
