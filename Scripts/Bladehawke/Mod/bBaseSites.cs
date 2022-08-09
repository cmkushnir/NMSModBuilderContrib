//=============================================================================

//=============================================================================

//=========================================================================

// Adjust startchart and random scan reward probabilities
// i.e. what kind of location you find.
// In particular it adds Base Sites and Portals as possible reward locations.
// (base sites are flat round areas w/ existing base computer)
public class bBaseSites : cmk.NMS.Script.ModClass
{
  protected override void Execute()
  {
    GcBuildingDefinitionTable();
    GcRewardTable();
  }

  //...........................................................

  public static float SecureDepot { get; set; } = 20;
  public static float SecureFactory { get; set; } = 40;
  public static float SecureHarvester { get; set; } = 20;
  public static float SecureBaseSite { get; set; } = 10;

  //...........................................................

  protected void GcBuildingDefinitionTable()
  {
    var mbin = ExtractMbin<GcBuildingDefinitionTable>(
      "METADATA/SIMULATION/ENVIRONMENT/PLANETBUILDINGTABLE.MBIN"
    );
    var data = mbin.BuildingPlacement[(int)BuildingClassEnum.Base];
    data.ClusterLayout = "SINGLE_WAYPOINT";
  }
  //...........................................................

  protected void GcRewardTable()
  {
    var mbin = ExtractMbin<GcRewardTable>(
      "METADATA/REALITY/TABLES/REWARDTABLE.MBIN"
    );
    GcRewardTable_StarChart(mbin);
    GcRewardTable_RandomScan(mbin);
  }

  //...........................................................

  protected void GcRewardTable_StarChart(GcRewardTable MBIN)
  {
    var item = MBIN.SpecialRewardTable.Find(REWARD => REWARD.Id == "R_STARCHART_A");
    RewardA(item.List.List);
  }

  //...........................................................

  protected void GcRewardTable_RandomScan(GcRewardTable MBIN)
  {
    var item = MBIN.SpecialRewardTable.Find(REWARD => REWARD.Id == "RANDOM_SCAN_A");
    RewardA(item.List.List);
  }

  //...........................................................

  // A : secure site
  // Old: Depot  6%, Factory 12%, Harvester  6%
  // New: Depot 20%, Factory 40%, Harvester 20%, Base 10%
  protected void RewardA(List<GcRewardTableItem> LIST)
  {
    var reward = UpdateReward(LIST, "Depot", SecureDepot);
    reward = UpdateReward(LIST, "Factory", SecureFactory);
    reward = UpdateReward(LIST, "Harvester", SecureHarvester);
    reward = CreateReward(LIST, SecureBaseSite,
    "Harvester", "HARVESTER",  // clone this
    "Base", "BASE",       // to make this
    BuildingClassEnum.Base
  );
  }

  //...........................................................

  protected GcRewardTableItem UpdateReward(
    List<GcRewardTableItem> LIST, string ID, float PERCENT
  )
  {
    var reward = LIST.Find(ITEM => ITEM.LabelID == ID);
    if (reward != null)
    {  // no error, not all R_STARCHART_* in RANDOM_SCAN_*
      if (PERCENT < 1) LIST.Remove(reward);
      else reward.PercentageChance = PERCENT;
    }
    return reward;
  }

  //...........................................................

  protected GcRewardTableItem CreateReward(
    List<GcRewardTableItem> LIST,
    float PERCENT,
    string SOURCE_ID,        // "Monolith", find in LIST
    string SOURCE_NAME,      // "MONOLITH", find in GcScanEventTable
    string TARGET_ID,        // "Portal",   add  to LIST
    string TARGET_NAME,      // "PORTAL",   add  to GcScanEventTable
    BuildingClassEnum CLASS  // BuildingClassEnum.Portal
  )
  {
    if (PERCENT < 1) return null;

    GcScanEventTable_Add("SCANEVENTTABLEPLANET", SOURCE_NAME, TARGET_NAME, CLASS);
    GcScanEventTable_Add("SCANEVENTTABLEVEHICLE", SOURCE_NAME, TARGET_NAME, CLASS);

    var target = LIST.Find(ITEM => ITEM.LabelID == TARGET_ID);
    if (target == null)
    {
      var source = LIST.Find(ITEM => ITEM.LabelID == SOURCE_ID);
      target = CloneMbin(source);

      target.LabelID = TARGET_ID;
      (target.Reward as GcRewardScanEvent).Event = TARGET_NAME;

      LIST.Add(target);
    }

    target.PercentageChance = PERCENT;
    return target;
  }

  //...........................................................

  protected GcScanEventData GcScanEventTable_Add(
    string TABLE,
    string SOURCE_NAME,      // "MONOLITH", find in TABLE.Events
    string TARGET_NAME,      // "PORTAL",   add  to TABLE.Events
    BuildingClassEnum CLASS  // BuildingClassEnum.Portal
  )
  {
    var mbin = ExtractMbin<GcScanEventTable>(
      "METADATA/SIMULATION/SCANNING/" + TABLE + ".MBIN"
    );
    var target = mbin.Events.Find(EVENT => EVENT.Name == TARGET_NAME);
    if (target != null) return target;  // no error, already exists

    var source = mbin.Events.Find(EVENT => EVENT.Name == SOURCE_NAME);
    target = CloneMbin(source);

    target.Name = TARGET_NAME;
    target.OSDMessage = "SIGNAL_" + TARGET_NAME;
    target.TooltipMessage = "TIP_" + TARGET_NAME;
    //	target.SurveyDistance = 100000;
    target.BuildingClass.BuildingClass = CLASS;

    mbin.Events.Add(target);
    return target;
  }
}


//=============================================================================
