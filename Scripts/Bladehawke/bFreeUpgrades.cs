//=============================================================================

public class bFreeUpgrades : cmk.NMS.Script.ModClass
{
  protected override void Execute()
  {
    AlienPuzzleTable();
    RewardTable();
  }
  
  protected void AlienPuzzleTable()
  {
    var mbin = ExtractMbin<GcAlienPuzzleTable>(
      "METADATA/REALITY/TABLES/NMS_DIALOG_GCALIENPUZZLETABLE.MBIN"
    );
    
    var upgradeSlots = new (string Id, string Name)[] { 
      new( "CUSTOMISE_FREIGHTER", "UI_CUSTOM_FREIGHTER_OPT_A" ),
      new( "?SHIP_SALVAGE_UPGRADE", "UI_SALVAGE_INV_OPTA" ),
      new( "WEAPON_UPGRADE", "UI_WEAP_UPGRADE_INV_OPTA" ),
    };
    
    var upgradeClass = new (string Id, string Name)[] {
      new ( "?SHIP_SALVAGE_UPGRADE", "UI_SALVAGE_CLASS_OPT" ),
       new ( "WEAPON_UPGRADE", "UI_SALVAGE_CLASS_OPT" )
    };
    
    foreach (var item in upgradeSlots) {

      var entry = mbin.Table.Find(NAME => NAME.Id == item.Id);
      var option = entry.Options.Find(NAME => NAME.Name == item.Name);
    
      option.Name = "Upgrade Slots";
      option.Cost = "";
    };
    
    foreach (var item in upgradeClass) {
      var entry = mbin.Table.Find(NAME => NAME.Id == item.Id);
      var option = entry.Options.Find(NAME => NAME.Name == item.Name);

      option.Name = "Upgrade Class";
      option.Text = "";
      option.Cost = "";
    }
    
  }

  protected void RewardTable()
  {
    var mbin = ExtractMbin<GcRewardTable>(
      "METADATA/REALITY/TABLES/REWARDTABLE.MBIN"
    );
    
    var items = new[] { "R_FREIGHTSLOT", "R_SHIPSLOT_CASH", "R_WEAPSLOT_CASH" };
    var ListGcGenericRewardTableEntry_t = typeof(List<GcGenericRewardTableEntry>);
    
    // increase possible amount of resources to receieve from all rewards
    foreach( var field in mbin.GetType().GetFields() ) {
      if( field.FieldType != ListGcGenericRewardTableEntry_t ) continue;
      var list = field.GetValue(mbin) as List<GcGenericRewardTableEntry>;
      foreach (var item in items) {
        var entry = list.Find(NAME => NAME.Id == item);
        if (entry == null) continue;
        
        var reward = entry.List.List.FindFirst<GcRewardTableItem>();
        
        // There's got to be a more generic way of handling this, but it escapes me
        if (reward.Reward is GcRewardFreighterSlot)
          (reward.Reward as GcRewardFreighterSlot).Cost = "";
        else if (reward.Reward is GcRewardShipSlot)
          (reward.Reward as GcRewardShipSlot).Cost = "";
        else
          (reward.Reward as GcRewardWeaponSlot).Cost = "";
      }
    }
  }
  
  //...........................................................
}

//=============================================================================
